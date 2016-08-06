// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------

using Fbb.Exceptions.Base;


namespace CodeAnalyzer.ConsoleApp
{
  using System;
  using System.Text;

  using Coordination;
  using Fbb.Output;
  using Fbb.Util.TimeTracking;
  using Mediator;
  using Mediator.Cryptography;
  using Mediator.Identifiers;


  public class ConsoleAppStarter
  {
    #region Instance Variables .................................................
    private static Zone _Log            = null;
    private static bool _ConsoleEnabled = true;
    #endregion .............................................. Instance Variables


    #region Properties .........................................................
    public static Zone Log
    {
      get { return _Log ?? (_Log = Out.ZoneFactory(Ids.REGION_APP_STARTER, "ConsoleAppStarter")); }
    }

    private static bool ConsoleEnabled
    {
      get { return _ConsoleEnabled; }
      set { _ConsoleEnabled = value; }
    }
    #endregion ...................................................... Properties


    #region Application starting point .........................................
    static void Main(string[] args)
    {
			#region Testing encryption/decryption

      bool shouldTest = false;
      if (shouldTest)
			{
				AesCryptography aes = new AesCryptography();

				var password = "1234567890!";
				var salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
				var ct1 = aes.Encrypt(password, salt, Encoding.UTF8.GetBytes("Alice; Bob; Eve;: PerformAct1"));
				Console.WriteLine(Convert.ToBase64String(ct1));

				var ct2 = aes.Encrypt(password, salt, Encoding.UTF8.GetBytes("Alice; Bob; Eve;: PerformAct2"));
				Console.WriteLine(Convert.ToBase64String(ct2));

				var pt1 = aes.Decrypt(password, salt, ct1);
				Console.WriteLine(Encoding.UTF8.GetString(pt1));

				var pt2 = aes.Decrypt(password, salt, ct2);
				Console.WriteLine(Encoding.UTF8.GetString(pt2));

				// Now check tampering
				try
				{
					ct1[30]++;
					aes.Decrypt(password, salt, ct1);
					Console.WriteLine("Error: tamper detection failed.");
				}
				catch (Exception ex)
				{
					Console.WriteLine("Success: tampering detected.");
					Console.WriteLine(ex.ToString());
				}
				Console.ReadLine();
			}
			#endregion


      string COMPARE_SILENT = "SILENT";
      string COMPARE_HELP   = "HELP";

      string[] cmdArgs = Environment.GetCommandLineArgs();
      foreach (string arg in cmdArgs)
      {
        if (arg.ToUpper() == COMPARE_SILENT)
        {
          ConsoleEnabled = false;
        }

        if (arg.ToUpper() == COMPARE_HELP)
        {
          ConsoleEnabled = true;

          #region Help output to console
          //      12345678901234567890123456789012345678901234567890123456789012345678901234567890
          Output("╔══════════════════════════════════════════════════════════════════════════════╗");
          Output("║                                  Code Analyser                               ║");
          Output("╠══════════════════════════════════════════════════════════════════════════════╣");
          Output("║                                                                              ║");
          Output("║ Purpose   The application is designed to search files for suspect code       ║");
          Output("║           constructions, i.e. try-catch statements suppressing exceptions    ║");
          Output("║           from being handled.                                                ║");
          Output("║                                                                              ║");
          Output("║           The type of code constructions that are matched during the search  ║");
          Output("║           is specified through regular expressions in the applications       ║");
          Output("║           configuration file. Multiple regular expressions can be added to   ║");
          Output("║           the configuration file as well as what directories the search      ║");
          Output("║           should include, what directories should be excluded, the type of   ║");
          Output("║           files to include in the search.                                    ║");
          Output("║                                                                              ║");
          Output("║                                                                              ║");
          Output("║  Result:  A resulting xml file containing the result of the search will be   ║");
          Output("║           created in the execution directory, 'Analyser.xml'. Just open it   ║");
          Output("║           in a browser - it will be transformed into html by the associated  ║");
          Output("║           xslt file.                                                         ║");
          Output("║                                                                              ║");
          Output("║                                                                              ║");
          Output("║  How to:  The application can be run with no arguments. The following        ║");
          Output("║           arguments are allowed:                                             ║");
          Output("║                                                                              ║");
          Output("║           <help>   Will show this dialog.                                    ║");
          Output("║                                                                              ║");
          Output("║           <silent> Indicates whether output from the client should be        ║");
          Output("║                    enabled. Adding the argument 'silent' will disable        ║");
          Output("║                    output to the command line.                               ║");
          Output("║                                                                              ║");
          Output("║           NOTE:    Using the 'silent' argument will not disable output from  ║");
          Output("║                    the log system to the 'Console' target! If all messages   ║");
          Output("║                    to the command line should be completely disabled then    ║");
          Output("║                    disable the 'Console' target in the log system            ║");
          Output("║                    configuration file as well.                               ║");
          Output("║                                                                              ║");
          Output("║                                                                              ║");
          Output("║   Setup:  Two configuration files (.config) are needed in order to execute   ║");
          Output("║           the application. Both files is expected to be located in the       ║");
          Output("║           applications execution directory. If not placed here the           ║");
          Output("║           application will fail.                                             ║");
          Output("║                                                                              ║");
          Output("║           <hunter> Configuration file for setting up the include             ║");
          Output("║                    directories, regular expressions etc.                     ║");
          Output("║                                                                              ║");
          Output("║           <log>    Configuration file for setting up the log system that the ║");
          Output("║                    application uses.                                         ║");
          Output("║                                                                              ║");
          Output("╚══════════════════════════════════════════════════════════════════════════════╝");
          #endregion

          ConsoleEnabled = false;
          return;
        }
      }

	    try
	    {
		    DirHandler.Instance.CurrentDirectory = Environment.CurrentDirectory;
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine(BaseException.Format(null, -1, @"Failed to initialize 'Directory Handler' with current DIR? Unable to continue.", e));
		    Console.ReadLine();
		    return;
	    }


	    ApplicationManager am = null;
	    try
	    {
		    am = new ApplicationManager();
	    }
	    catch (Exception e)
	    {
				Console.WriteLine(BaseException.Format(null, -1, @"Failed to construct the 'Application Manager'? Unable to continue.", e));
		    Console.ReadLine();
		    return;
	    }


      try
      {
        am.Start();
      }
      catch (CoordinationException ce)
      {
        Console.WriteLine(ce.ExceptionSummary());
	      Console.ReadLine();
        return;
      }

      Output(ProxyHome.Instance.StatisticsProxy.ExtractTimerMeasurings());


      // Shutdown the log system - should also empty all the queues before stopping.
      Out.Stop();
      Console.ReadLine();
    }
    #endregion ...................................... Application starting point


    #region Output .............................................................
    private static void Output(string s, params string[] list)
    {
      if (string.IsNullOrEmpty(s))
        return;

      if (!ConsoleEnabled)
        return;

      if (list != null  &&  list.Length > 0)
      {
        // TODO: Do not use the Console - use the log framework!!!
        //Console.WriteLine(s, list);
	      Log.Info(s, list);
        return;
      }
      // TODO: Do not use the Console - use the log framework!!!
      //Console.WriteLine(s);
	    Log.Info(s);
    }
		#endregion .......................................................... Output
	}
}
