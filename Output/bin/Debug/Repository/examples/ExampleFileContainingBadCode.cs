// Match minus 5.
// Match minus 4.
// Match minus 3.
// Match minus 2.
// Match minus 1.
// --<< HERE WE HAVE THE MATCH >>--
// Match plus 1.
// Match plus 2.
// Match plus 3.
// Match plus 4.
// Match plus 5.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalyzer.Repository.examples
{
  public class ExampleFileContainingBadCode {
    public readonly static string _SomeStringName = "";

    public void Test()
    {
      try
      {
        // something...
      } 
      catch { }

      try
      {
        // something...
      }
      catch (InvalidOperationException someException)
      {
        throw someException ;
      }

    }
  }

	public class ExampleFileContainingBadCode1 {
	}

	public class ExampleFileContainingBadCode2 { }

	public class ExampleFileContainingBadCode3 { }
}
