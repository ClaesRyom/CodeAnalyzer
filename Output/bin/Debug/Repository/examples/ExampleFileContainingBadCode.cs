using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalyzer.Repository.examples
{
  public class ExampleFileContainingBadCode
  {
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
}
