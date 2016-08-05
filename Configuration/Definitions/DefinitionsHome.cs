// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Configuration.Definitions
{
  using System;
  using System.Collections.Generic;
  using System.IO;

  using Fbb.Output;
  using Mediator.Configuration.Declarations;
  using Mediator.Configuration.Definitions;
  using Mediator.Identifiers;


  internal sealed class DefinitionsHome : IDefinitionsHome
  {
    #region Instance Variables .................................................
    private readonly Zone   _log  = null;
    private readonly object _lock = new object();
    #endregion .............................................. Instance Variables


    #region Private auto properties ............................................
    private bool              Disposed     { get; set; }
    private IDeclarationsHome Declarations { get; set; }
    #endregion ......................................... Private auto properties


    #region Public auto properties .............................................
    public Dictionary<int, IProjectDefinition>  Projects   { get; set; }
    public Dictionary<int, ICategoryDefinition> Categories { get; set; }
    public Dictionary<int, IRuleDefinition>     Rules      { get; set; }
    #endregion .......................................... Public auto properties


    #region Properties .........................................................
    private Zone Log
    {
      get { return _log ?? Out.ZoneFactory(Ids.REGION_CONFIG, GetType().Name); }
    }
    #endregion ...................................................... Properties

    
    #region Construction .......................................................
    public DefinitionsHome(IDeclarationsHome declarations)
    {
      Declarations = declarations;
      Projects     = new Dictionary<int, IProjectDefinition>();
      Categories   = new Dictionary<int, ICategoryDefinition>();
      Rules        = new Dictionary<int, IRuleDefinition>();
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
      return ValidateProjectSettings();
    }

    private bool ValidateProjectSettings()
    {
      bool validationResult = true;
      foreach (KeyValuePair<int, IProjectDefinition> keyValuePair in Projects)
      {
        validationResult = ValidateProject(keyValuePair.Value);
        if (!validationResult)
          return false;
      }
      return true;
    }

    private bool ValidateProject(IProjectDefinition projectDefinition)
    {
      #region Check the existence of files and directories
      // Check for whether or not the directories actually exists on desk
      // if they don't exist then move the non existing directories to the list 
      // invalid directories.
      CheckDirectories(projectDefinition.Directories, projectDefinition.InvalidDirectories);
      CheckDirectories(projectDefinition.ExcludedDirectories, projectDefinition.InvalidDirectories);

      // Let's check the files the same way as we just did with the directories above.
      CheckFiles(projectDefinition.Files, projectDefinition.InvalidFiles);
      CheckFiles(projectDefinition.ExcludedFiles, projectDefinition.InvalidFiles);

      if (projectDefinition.Directories.Count == 0)
      {
        Log.Warning("No valid 'Directories' was defined in project '{0}' with ID='{1}'.", projectDefinition.Name, projectDefinition.Id);
        return false;
      }
      #endregion


      #region Cross check reference ID's for both categories and rules
      if (projectDefinition.Categories.Count == 0)
      {
        Log.Error(string.Format("No 'CategoryDeclarations' was defined project '{0}' with ID='{1}'.", projectDefinition.Name, projectDefinition.Id));
        return false;
      }

      bool validationResult = true;
      foreach (KeyValuePair<int, ICategoryDefinition> keyValuePair in projectDefinition.Categories)
      {
        validationResult = Declarations.IsCategoryDeclared(keyValuePair.Value.CategoryDeclarationReferenceName, keyValuePair.Value.CategoryDeclarationReferenceId);
        if (!validationResult)
        {
          Log.Error(string.Format("The cross references for project with refName='{0}' and with refId='{1}' are invalid.", keyValuePair.Value.CategoryDeclarationReferenceName, keyValuePair.Value.CategoryDeclarationReferenceId));
          return false;
        }
      }
      #endregion

      return true;
    }

    private void CheckDirectories(List<IDirectoryDefinition> directorisToCheck, List<IDirectoryDefinition> invalidDirectories)
    {
      List<IDirectoryDefinition> removers = new List<IDirectoryDefinition>();

      foreach (IDirectoryDefinition dirDef in directorisToCheck)
      {
        if (!Directory.Exists(dirDef.Path))
        {
          invalidDirectories.Add(dirDef);
        }
      }

      foreach (IDirectoryDefinition r in removers)
      {
        directorisToCheck.Remove(r);
        Log.Debug(@"Directory '{0}' could not be found and is therefore treated as an invalid directory.", r.Path);
      }
    }

    private void CheckFiles(List<IFileDefinition> filesToCheck, List<IFileDefinition> invalidFiles)
    {
      List<IFileDefinition> removers = new List<IFileDefinition>();

      foreach (IFileDefinition fileDef in filesToCheck)
      {
        if (!File.Exists(fileDef.Path))
        {
          invalidFiles.Add(fileDef);
        }
      }

      foreach (IFileDefinition r in removers)
      {
        filesToCheck.Remove(r);
        Log.Debug(@"File '{0}' could not be found and is therefore treated as an invalid file.", r.Path);
      }
    }
    #endregion ...................................................... Validation
  }
}