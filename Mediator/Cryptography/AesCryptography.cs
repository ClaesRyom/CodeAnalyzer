// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Mediator.Cryptography
{
  using System;
  using System.Security.Cryptography;


  /// <summary>
  /// This class is build on the following blog: 
  /// 
  ///   http://stackoverflow.com/questions/667887/aes-in-asp-net-with-vb-net
  ///  
  /// Don't do it! Writing your own crypto system can easily lead to making mistakes. 
  /// It's best to use an existing system, or if not, get someone who knows cryptography 
  /// to do it. If you have to do it yourself, read Practical Cryptography.
  /// 
  /// And please, remember: 
  /// 
  ///   "We already have enough fast, insecure systems." 
  ///     (Bruce Schneier) -- Do things correct and worry about performance later.
  /// 
  /// That said, if you are stuck on using AES to roll your own, here are a few pointers.
  /// 
  /// 
  /// Initialization Vector:
  /// ======================
  /// AES is a block cipher. Given a key and a block of plaintext, it converts it to a 
  /// specific ciphertext. The problem with this is that the same blocks of data will 
  /// generate the same ciphertext with the same key, every time. So suppose you send data 
  /// like this:
  /// 
  ///   user=Encrypt(Username)&roles=Encrypt(UserRoles)
  /// 
  /// They're two separate blocks, and the UserRoles encryption will have the same 
  /// ciphertext each time, regardless of the name. All I need is the ciphertext for an 
  /// admin, and I can drop it right in with my cipher'd username. Oops.
  /// 
  /// So, there are cipher operation modes. The main idea is that you'll take the ciphertext 
  /// of one block, and XOR it into the ciphertext of the next block. That way we'll do 
  /// Encrypt(UserRoles, Username), and the Username ciphertext is affected by the UserRoles.
  /// 
  /// The problem is that the first block is still vulnerable - just by seeing someone's 
  /// ciphertext, I might know their roles. Enter the initialization vector. The IV "starts up" 
  /// the cipher and ensures it has random data to encrypt the rest of the stream. So now the 
  /// UserRoles ciphertext has the ciphertext of the random IV XOR'd in. Problem solved.
  /// 
  /// So, make sure you generate a random IV for each message. The IV is not sensitive and 
  /// can be sent plaintext with the ciphertext. Use an IV large enough -- the size of the 
  /// block should be fine for many cases.
  /// 
  /// 
  /// Integrity:
  /// ==========
  /// AES doesn't provide integrity features. Anyone can modify your ciphertext, and the decrypt 
  /// will still work. It's unlikely it'll be valid data in general, but it might be hard to 
  /// know what valid data is. For instance, if you're transmitting a GUID encrypted, it'd be 
  /// easy to modify some bits and generate a completely different one. That could lead to 
  /// application errors and so on.
  /// 
  /// The fix there is to run a hash algorithm (use SHA256 or SHA512) on the plaintext, and 
  /// include that in the data you transmit. So if my message is (UserName, Roles), you'll 
  /// send (UserName, Roles, Hash(UserName, Roles)). Now if someone tampers with the ciphertext 
  /// by flipping a bit, the hash will no longer compute and you can reject the message.
  /// 
  /// 
  /// Key derivation:
  /// ===============
  /// If you need to generate a key from a password, use the built-in class: 
  /// 
  ///   System.Security.Cryptography.PasswordDeriveBytes. 
  /// 
  /// This provides salting and iterations, which can improve the strength of derived keys and 
  /// reduce the chance of discovering the password if the key is compromised.
  /// 
  /// 
  /// Timing/replay:
  /// ==============
  /// Edit: Sorry for not mentioning this earlier :P. You also need to make sure you have an 
  /// anti-replay system. If you simply encrypt the message and pass it around, anyone who gets 
  /// the message can just resend it. To avoid this, you should add a timestamp to the message. 
  /// If the timestamp is different by a certain threshold, reject the message. You may also want 
  /// to include a one-time ID with it (this could be the IV) and reject time-valid messages that 
  /// come from other IPs using the same ID.
  /// 
  /// It's important to make sure you do the hash verification when you include the timing information. 
  /// Otherwise, someone could tamper with a bit of the ciphertext and potentially generate a valid 
  /// timestamp if you don't detect such brute force attempts.
  /// </summary>
  internal sealed class AesCryptography
  {
    const int HASH_SIZE = 32; //SHA256

    /// <summary>
    /// Performs encryption with random IV (prepended to output), and includes hash of plaintext for verification.
    /// </summary>
    public byte[] Encrypt(string password, byte[] passwordSalt, byte[] plainText)
    {
      // Construct message with hash
      var msg  = new byte[HASH_SIZE + plainText.Length];
      var hash = ComputeHash(plainText, 0, plainText.Length);

      Buffer.BlockCopy(hash, 0, msg, 0, HASH_SIZE);
      Buffer.BlockCopy(plainText, 0, msg, HASH_SIZE, plainText.Length);

      // Encrypt
      using (var aes = CreateAes(password, passwordSalt))
      {
        aes.GenerateIV();
        using (var enc = aes.CreateEncryptor())
        {
          var encBytes = enc.TransformFinalBlock(msg, 0, msg.Length);
          // Prepend IV to result
          var res = new byte[aes.IV.Length + encBytes.Length];
          Buffer.BlockCopy(aes.IV, 0, res, 0, aes.IV.Length);
          Buffer.BlockCopy(encBytes, 0, res, aes.IV.Length, encBytes.Length);
          return res;
        }
      }
    }

    public byte[] Decrypt(string password, byte[] passwordSalt, byte[] cipherText)
    {
      using (var aes = CreateAes(password, passwordSalt))
      {
        var iv = new byte[aes.IV.Length];
        Buffer.BlockCopy(cipherText, 0, iv, 0, iv.Length);
        aes.IV = iv; // Probably could copy right to the byte array, but that's not guaranteed

        using (var dec = aes.CreateDecryptor())
        {
          var decBytes = dec.TransformFinalBlock(cipherText, iv.Length, cipherText.Length - iv.Length);

          // Verify hash
          var hash = ComputeHash(decBytes, HASH_SIZE, decBytes.Length - HASH_SIZE);
          var existingHash = new byte[HASH_SIZE];
          Buffer.BlockCopy(decBytes, 0, existingHash, 0, HASH_SIZE);
          if (!CompareBytes(existingHash, hash))
          {
            throw new CryptographicException("Message hash incorrect.");
          }

          // Hash is valid, we're done
          var res = new byte[decBytes.Length - HASH_SIZE];
          Buffer.BlockCopy(decBytes, HASH_SIZE, res, 0, res.Length);
          return res;
        }
      }
    }

	  internal bool CompareBytes(byte[] a1, byte[] a2)
    {
      if (a1.Length != a2.Length)
        return false;

      for (int i = 0; i < a1.Length; i++)
      {
        if (a1[i] != a2[i])
          return false;
      }
      return true;
    }

	  internal Aes CreateAes(string password, byte[] salt)
    {
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("Password cannot be null or whitespace.", "password");

			if (salt == null)
				throw new ArgumentException("Salt cannot be null.", "salt");

      // Salt may not be needed if password is safe
      if (password.Length < 8)
        throw new ArgumentException("Password must be at least 8 characters.", "password");

      if (salt.Length < 8)
        throw new ArgumentException("Salt must be at least 8 bytes.", "salt");

      var pdb = new PasswordDeriveBytes(password, salt, "SHA512", 129);
      var key = pdb.GetBytes(16);

      var aes  = Aes.Create();
      aes.Mode = CipherMode.CBC;
      aes.Key  = pdb.GetBytes(aes.KeySize / 8);
      return aes;
    }

    private byte[] ComputeHash(byte[] data, int offset, int count)
    {
      using (var sha = SHA256.Create())
      {
        return sha.ComputeHash(data, offset, count);
      }
    }

    //public static void Main()
    //{
    //  var password = "1234567890!";
    //  var salt     = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
    //  var ct1      = Encrypt(password, salt, Encoding.UTF8.GetBytes("Alice; Bob; Eve;: PerformAct1"));
    //  Console.WriteLine(Convert.ToBase64String(ct1));

    //  var ct2 = Encrypt(password, salt, Encoding.UTF8.GetBytes("Alice; Bob; Eve;: PerformAct2"));
    //  Console.WriteLine(Convert.ToBase64String(ct2));

    //  var pt1 = Decrypt(password, salt, ct1);
    //  Console.WriteLine(Encoding.UTF8.GetString(pt1));
      
    //  var pt2 = Decrypt(password, salt, ct2);
    //  Console.WriteLine(Encoding.UTF8.GetString(pt2));

    //  // Now check tampering
    //  try
    //  {
    //    ct1[30]++;
    //    Decrypt(password, salt, ct1);
    //    Console.WriteLine("Error: tamper detection failed.");
    //  }
    //  catch (Exception ex)
    //  {
    //    Console.WriteLine("Success: tampering detected.");
    //    Console.WriteLine(ex.ToString());
    //  }
    //}
  }
}
