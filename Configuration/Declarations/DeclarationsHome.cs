// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Configuration.Declarations
{
  using System;
  using System.Collections.Generic;

  using Fbb.Output;
  using Mediator.Configuration.Declarations;
  using Mediator.Identifiers;
  using Version;


  internal sealed class DeclarationsHome : IDeclarationsHome
  {
    #region Instance Variables .................................................
    private readonly Zone   _log  = null;
    private readonly object _lock = new object();
    #endregion .............................................. Instance Variables


    #region Private auto properties ............................................
    private bool Disposed { get; set; }
    #endregion ......................................... Private auto properties


    #region Public auto properties .............................................
    public string                                Version    { get; set; }
    public Dictionary<int, ILanguageDeclaration> Languages  { get; set; }
    public Dictionary<int, ICategoryDeclaration> Categories { get; set; }
    public Dictionary<int, IRuleDeclaration>     Rules      { get; set; }
    #endregion .......................................... Public auto properties


    #region Properties .........................................................
    private Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_CONFIG, GetType().Name); }
    }
    #endregion ...................................................... Properties

    
    #region Construction .......................................................
    public DeclarationsHome()
    {
      Version    = Vers.Application;
      Languages  = new Dictionary<int, ILanguageDeclaration>();
      Categories = new Dictionary<int, ICategoryDeclaration>();
      Rules      = new Dictionary<int, IRuleDeclaration>();
    }
    #endregion .................................................... Construction


    #region Implementation of IDisposable ......................................
    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }

    // Dispose(bool disposing) executes in two distinct scenarios. 
    // If disposing equals true, the method has been called directly 
    // or indirectly by a user's code. Managed and unmanaged resources 
    // can be disposed. 
    // If disposing equals false, the method has been called by the  
    // runtime from inside the finalizer and you should not reference  
    // other objects. Only unmanaged resources can be disposed. 
    private void Dispose(bool disposing)
    {
      // If you need thread safety, use a lock around these  
      // operations, as well as in your methods that use the resource. 
      if (Disposed)
        return;

      // If disposing equals true, dispose all managed  
      // and unmanaged resources. 
      //if (disposing)
      //{
      //  // Dispose managed resources.
      //  //if (CategoryDeclarations != null)
      //  //  CategoryDeclarations.Dispose();

      //  // Call the appropriate methods to clean up  
      //  // unmanaged resources here. 
      //  // If disposing is false,  
      //  // only the following code is executed.
      //}

      // Indicate that the instance has been disposed.
      //Declarations     = null;
      Disposed = true;
    }
    #endregion ................................... Implementation of IDisposable


    #region Validation .........................................................
    public bool IsSettingsValid()
    {
      if (!ValidateLanguageDeclarations(Languages))
        return false;


      return true;
    }

    private bool ValidateLanguageDeclarations(Dictionary<int, ILanguageDeclaration> languageDeclarations)
    {
      foreach (KeyValuePair<int, ILanguageDeclaration> keyValuePair in languageDeclarations)
      {
        if (!ValidateLanguage(keyValuePair.Value))
          return false;
      }
      return true;
    }

    private bool ValidateLanguage(ILanguageDeclaration languageDeclaration)
    {
      if (string.IsNullOrEmpty(languageDeclaration.Extension))
      {
        Log.Error(string.Format("No 'Extension' defined for language '{0}' with ID='{1}'.", languageDeclaration.Name, languageDeclaration.Id));
        return false;
      }

      if (!languageDeclaration.Extension.StartsWith("*."))
      {
        Log.Error(string.Format("The 'Extension' must start with '*.' and it didn't for language '{0}' with ID='{1}'.", languageDeclaration.Name, languageDeclaration.Id));
        return false;
      }

      if (languageDeclaration.Extension.Length < 2)
      {
        Log.Error(string.Format("The 'Extension' must be at least 2 characters long and it wasn't for language '{0}' with ID='{1}'.", languageDeclaration.Name, languageDeclaration.Id));
        return false;
      }
      return true;
    }

    public bool IsCategoryDeclared(string referenceName, int referenceId)
    {
      if (!Categories.ContainsKey(referenceId))
        return false;

      ICategoryDeclaration declaration = Categories[referenceId];
      if (string.Compare(referenceName, declaration.Name, StringComparison.OrdinalIgnoreCase) != 0)
        return false;
      return true;
    }
    #endregion ...................................................... Validation
  }
}