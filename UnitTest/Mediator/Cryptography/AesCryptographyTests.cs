// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Traen A/S, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.UnitTest.Mediator.Cryptography
{
	using System;
	using System.Security.Cryptography;
	using System.Text;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using CodeAnalyzer.Mediator.Cryptography;
	using CodeAnalyzer.Mediator.Security;


	[TestClass]
	public class AesCryptographyTests
	{
		[TestClass]
		public class CreateAesTests
		{
			#region Instance Variable(s) .............................................
			private const string TO_SHORT_PASSWORD = @"passw";
			private const string TO_SHORT_SALT     = @"salt";
			private const string LONG_PASSWORD    = @"ThisIsALongPassword";
			private const string LONG_SALT        = @"ThisIsALongSalt";
			#endregion .......................................... Instance Variable(s)
			
			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			public void CreateAes_ValidPasswordAndValidSalt_ReturnsAes()
			{
				AesCryptography aes = new AesCryptography();
				Assert.IsNotNull(aes.CreateAes(LONG_PASSWORD, Encoding.UTF8.GetBytes(LONG_SALT)));
			}

			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			[ExpectedException(typeof(ArgumentException), "Salt must be at least 8 bytes.")]
			public void CreateAes_ValidPasswordAndInvalidShortSalt_ThrowsArgumentException()
			{
				AesCryptography aes = new AesCryptography();
				aes.CreateAes(LONG_PASSWORD, Encoding.UTF8.GetBytes(TO_SHORT_SALT));
			}

			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			[ExpectedException(typeof(ArgumentException), "Password must be at least 8 characters.")]
			public void CreateAes_InvalidShortPasswordAndValidSalt_ThrowsArgumentException()
			{
				AesCryptography aes = new AesCryptography();
				aes.CreateAes(TO_SHORT_PASSWORD, Encoding.UTF8.GetBytes(LONG_SALT));
			}

			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			[ExpectedException(typeof(ArgumentException), "Password cannot be null or whitespace.")]
			public void CreateAes_InvalidNullPasswordAndValidSalt_ThrowsArgumentException()
			{
				AesCryptography aes = new AesCryptography();
				aes.CreateAes(null, Encoding.UTF8.GetBytes(LONG_SALT));
			}

			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			[ExpectedException(typeof(ArgumentException), "Salt cannot be null.")]
			public void CreateAes_ValidPasswordAndInvalidNullSalt_ThrowsArgumentException()
			{
				AesCryptography aes = new AesCryptography();
				aes.CreateAes(LONG_PASSWORD, null);
			}
		}

		[TestClass]
		public class CompareBytesTests
		{
			#region Instance Variable(s) .............................................
			private const string BYTE_STREAM1                     = @"This is input to byte stream one.";
			private const string BYTE_STREAM2                     = @"This is input to byte stream two.";
			private const string BYTE_STREAM_IDENTICAL_TO_STREAM1 = @"This is input to byte stream one.";
			private const string LONGER_BYTE_STREAM               = @"This is a longer input to byte stream three than the input to byte stream one and two.";
			#endregion .......................................... Instance Variable(s)

			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			public void CompareBytes_DifferentLengthOfArguments_ReturnsFalse()
			{
				AesCryptography aes = new AesCryptography();

				// Test that different length of inputs will result in 'false' being returned.
				Assert.IsFalse(aes.CompareBytes(Encoding.UTF8.GetBytes(BYTE_STREAM1), Encoding.UTF8.GetBytes(LONGER_BYTE_STREAM)));
			}

			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			public void CompareBytes_DifferentInputInArguments_ReturnsFalse()
			{
				AesCryptography aes = new AesCryptography();

				// Test that different inputs will result in 'false' being returned eventhough the input length are identical.
				Assert.IsFalse(aes.CompareBytes(Encoding.UTF8.GetBytes(BYTE_STREAM1), Encoding.UTF8.GetBytes(BYTE_STREAM2)));

				// Test that identical inputs will result in 'true' being returned.
				Assert.IsTrue(aes.CompareBytes(Encoding.UTF8.GetBytes(BYTE_STREAM1), Encoding.UTF8.GetBytes(BYTE_STREAM_IDENTICAL_TO_STREAM1)));
			}

			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			public void CompareBytes_IdenticalInput_ReturnsTrue()
			{
				AesCryptography aes = new AesCryptography();

				// Test that identical inputs will result in 'true' being returned.
				Assert.IsTrue(aes.CompareBytes(Encoding.UTF8.GetBytes(BYTE_STREAM1), Encoding.UTF8.GetBytes(BYTE_STREAM_IDENTICAL_TO_STREAM1)));
			}
		}


		[TestClass]
		public class EncryptDecryptTests
		{
			#region Instance Variable(s) .............................................
			private const string CLEAR_TEXT = @"Alex Bech Jensen, Jeppe Dalgaard Jensen and Claes Ryom;: CLEAR TEXT";
			#endregion .......................................... Instance Variable(s)

			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			public void EncryptDecrypt_ValidPasswordSalt_AreEqual()
			{
				AesCryptography aes = new AesCryptography();
				
				byte[] cipherText  = aes.Encrypt(SecurityIds.Password, Encoding.UTF8.GetBytes(SecurityIds.Salt), Encoding.UTF8.GetBytes(CLEAR_TEXT));
				byte[] inClearText = aes.Decrypt(SecurityIds.Password, Encoding.UTF8.GetBytes(SecurityIds.Salt), cipherText);

				Assert.AreEqual(CLEAR_TEXT, Encoding.UTF8.GetString(inClearText));

				try
				{
					// Now let's do some tampering...
					cipherText[30]++;

					aes.Decrypt(SecurityIds.Password, Encoding.UTF8.GetBytes(SecurityIds.Salt), cipherText);
					Assert.Fail("The 'Decrypt' method did not throw an exception eventhough data was tamered with!");
				}
				catch (Exception ex)
				{
					Assert.IsTrue(ex is CryptographicException);
				}
			}

			[TestMethod]
			[TestCategory("Mediator.Cryptography")]
			[ExpectedException(typeof(CryptographicException), "Message hash incorrect.")]
			public void EncryptDecrypt_TamperingEncryptedData_ThrowsCryptographicException()
			{
				AesCryptography aes = new AesCryptography();
				
				byte[] cipherText  = aes.Encrypt(SecurityIds.Password, Encoding.UTF8.GetBytes(SecurityIds.Salt), Encoding.UTF8.GetBytes(CLEAR_TEXT));
				byte[] inClearText = aes.Decrypt(SecurityIds.Password, Encoding.UTF8.GetBytes(SecurityIds.Salt), cipherText);

				// Now let's do some tampering...
				cipherText[30]++;

				aes.Decrypt(SecurityIds.Password, Encoding.UTF8.GetBytes(SecurityIds.Salt), cipherText);
			}
		}
	}
}
