// -----------------------------------------------------------------------------
// <copyright>
//   Copyright (C) 2013 Claes Ryom, All Rights Reserved. 
//   
//   No change, use, reuse or copy to any media or print of this code is allowed  
//   without the authors specific written permission.
// </copyright>
// -----------------------------------------------------------------------------
namespace CodeAnalyzer.Output
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Drawing;
	using System.IO;
	using System.Linq;
	using Fbb.Output;
	using Mediator;
	using Mediator.Configuration;
	using Mediator.Exceptions.Output;
	using Mediator.Identifiers;
	using Mediator.Matches;
	using OfficeOpenXml;
	using OfficeOpenXml.Table.PivotTable;
	using OfficeOpenXml.Drawing.Chart;
	using Yogesh.ExcelXml;


	internal class ExcelOutput : AbstractOutput
	{
		#region Instance Variable(s) ...............................................
		private readonly Zone _Log = null;
		#endregion ............................................ Instance Variable(s)


		#region Auto properties ....................................................
		private string OutputDir  { get; set; }
		private string OutputFile { get; set; }
		#endregion ................................................. Auto properties


		#region Construction .......................................................
		public ExcelOutput() : base()
		{
		}
		#endregion .................................................... Construction


		#region Interface IOutput impl. ............................................
		public override void GenerateOutput(string outputRootDir)
		{
			#region Input validation
			if (string.IsNullOrWhiteSpace(outputRootDir))
				throw new ArgumentNullException(nameof(outputRootDir));

			if (!Directory.Exists(outputRootDir))
				throw new ArgumentException($"Unable to find directory {outputRootDir}.");
			#endregion

			#region Create output DIR and file
			OutputDir = Path.Combine(outputRootDir, Ids.EXCEL_DIR);
			if (!Directory.Exists(Directory.CreateDirectory(OutputDir).FullName))
				throw new OutputComponentException($"Unable to create output directory {OutputDir}.");

			OutputFile = Path.Combine(outputRootDir, Ids.EXCEL_DIR, Ids.EXCEL_FILE);
			FileInfo newFile = new FileInfo(OutputFile);
			#endregion

			IMatchProxy matchProxy = ProxyHome.Instance.RetrieveMatchProxy(OutputKeyKeeper.Instance.AccessKey);
			
      using (ExcelPackage pck = new ExcelPackage(newFile))
      {
        var wsData = pck.Workbook.Worksheets.Add("Matches");

        var dataRange = wsData.Cells["A1"].LoadFromCollection(
            from m in ConvertMatchesToExcelFormat(matchProxy.Matches())
						orderby m.Timestamp
            select m, 
            true, OfficeOpenXml.Table.TableStyles.Medium2);                
                
        wsData.Cells[2, 2, dataRange.End.Row, 2].Style.Numberformat.Format   = "dd-mm-yyyy - hh:mm:ss";
                
        dataRange.AutoFitColumns();

        //var wsPivot = pck.Workbook.Worksheets.Add("PivotSimple");
        //var pivotTable1 = wsPivot.PivotTables.Add(wsPivot.Cells["A1"], dataRange, "PerEmploee");

        //pivotTable1.RowFields.Add(pivotTable1.Fields[4]);
        //var dataField = pivotTable1.DataFields.Add(pivotTable1.Fields[6]);
        //dataField.Format="#,##0";
        //pivotTable1.DataOnRows = true;

        //var chart = wsPivot.Drawings.AddChart("PivotChart", eChartType.Pie, pivotTable1);
        //chart.SetPosition(1, 0, 4, 0);
        //chart.SetSize(600, 400);
                    
        //var wsPivot2 = pck.Workbook.Worksheets.Add("PivotDateGrp");
        //var pivotTable2 = wsPivot2.PivotTables.Add(wsPivot2.Cells["A3"], dataRange, "PerEmploeeAndQuarter");

        //pivotTable2.RowFields.Add(pivotTable2.Fields["Name"]);
                
        ////Add a rowfield
        //var rowField = pivotTable2.RowFields.Add(pivotTable2.Fields["OrderDate"]);
        ////This is a date field so we want to group by Years and quaters. This will create one additional field for years.
        //rowField.AddDateGrouping(eDateGroupBy.Years | eDateGroupBy.Quarters);
        ////Get the Quaters field and change the texts
        //var quaterField = pivotTable2.Fields.GetDateGroupField(eDateGroupBy.Quarters);
        //quaterField.Items[0].Text = "<"; //Values below min date, but we use auto so its not used
        //quaterField.Items[1].Text = "Q1";
        //quaterField.Items[2].Text = "Q2";
        //quaterField.Items[3].Text = "Q3";
        //quaterField.Items[4].Text = "Q4";
        //quaterField.Items[5].Text = ">"; //Values above max date, but we use auto so its not used
                
        ////Add a pagefield
        //var pageField = pivotTable2.PageFields.Add(pivotTable2.Fields["Title"]);
                
        ////Add the data fields and format them
        //dataField = pivotTable2.DataFields.Add(pivotTable2.Fields["SubTotal"]);
        //dataField.Format = "#,##0";
        //dataField = pivotTable2.DataFields.Add(pivotTable2.Fields["Tax"]);
        //dataField.Format = "#,##0";
        //dataField = pivotTable2.DataFields.Add(pivotTable2.Fields["Freight"]);
        //dataField.Format = "#,##0";
                
        ////We want the datafields to appear in columns
        //pivotTable2.DataOnRows = false;

        pck.Save();
      }
		}

		private List<MatchMappedToExcel> ConvertMatchesToExcelFormat(List<IMatch> matches)
		{
			List<MatchMappedToExcel> list = new List<MatchMappedToExcel>();

			foreach (IMatch m in matches)
			{
				list.Add(new MatchMappedToExcel()
				{
					BatchId             = m.Batch.Id.ToString(),
					Timestamp           = m.Batch.TimeStamp,
					ProjectName         = m.ProjectDefinitionRef.Name,
					MatchId             = m.Id.ToString(),
					Language            = m.LanguageDeclarationRef.Name,
					Severity            = $"{RuleSeverityMapper.Int2RuleSeverity((int)m.Severity)}",
					Category            = m.CategoryDeclarationRef.Name,
					CategoryDescription = m.CategoryDeclarationRef.Description,
					Rule                = m.RuleDeclarationRef.Name,
					RuleDescription     = m.RuleDeclarationRef.Description,
					RuleExpression      = m.RuleDeclarationRef.Expression,
					Filename            = m.Filename,
					MatchOnLine         = m.LineNumber.ToString(),
					CodeExtract         = m.CodeExtract
				});
			}
			return list;
		}
		
		public void GenerateOutput_Backup(string outputRootDir)
		{
			#region Input validation
			if (string.IsNullOrWhiteSpace(outputRootDir))
				throw new ArgumentNullException(nameof(outputRootDir));

			if (!Directory.Exists(outputRootDir))
				throw new ArgumentException($"Unable to find directory {outputRootDir}.");
			#endregion


			#region Create output DIR and file
			OutputDir = Path.Combine(outputRootDir, Ids.EXCEL_DIR);
			if (!Directory.Exists(Directory.CreateDirectory(OutputDir).FullName))
				throw new OutputComponentException($"Unable to create output directory {OutputDir}.");

			OutputFile = Path.Combine(outputRootDir, Ids.EXCEL_DIR, Ids.EXCEL_FILE);
//			File.Create(OutputFile);
//			if (!File.Exists(OutputFile))
//				throw new OutputComponentException($"Unable to create output file {OutputFile}.");
			#endregion

			
      #region Define row 'n' columns values...
			int columns = 13;

      int firstHeaderRow  = 0;
      int secondHeaderRow = firstHeaderRow + 1;
      int timestampRow    = secondHeaderRow + 1;
      int tableHeaderRow  = 0;
      int row             = tableHeaderRow + 1;
      int freezeRow       = tableHeaderRow + 1;
      #endregion


      #region Create the Excel xml workbook...
      // Create a instance...
      ExcelXmlWorkbook book = new ExcelXmlWorkbook();

      // Many such properties exist. Details can be found in the documentation
      // The author of the document
      book.Properties.Author = "Code Analyzer";

      // This returns the first worksheet.
      // Note that we have not declared a instance of a new worksheet
      // All the dirty work is done by the library.
      Worksheet dashboardSheet = book[0];
      Worksheet matchesSheet   = book[1];

			// Name is the name of the sheet. If not set, the default name
			// style is "sheet" + sheet number, like sheet1, sheet2
			dashboardSheet.Name = "Dashboard";
      matchesSheet.Name   = "Matches";

			// More on this in documentation
			matchesSheet.FreezeTopRows = freezeRow;

      // and this too...
      dashboardSheet.PrintOptions.Orientation = PageOrientation.Landscape;
			dashboardSheet.PrintOptions.SetMargins(0.5, 0.4, 0.5, 0.4);
      #endregion


      #region Creating the header...
      // Text for the header...
      dashboardSheet[0, firstHeaderRow].Value  = "Octopus | Code Analyzer";
			dashboardSheet[0, secondHeaderRow].Value = "The Octopus Code Analyzer - the simple way to keep track of your code base";

      // Setting the background color for the header...
      new Range(dashboardSheet[0, firstHeaderRow],  dashboardSheet[30, firstHeaderRow]).Interior.Color    = Color.FromArgb(22, 54, 92);
      new Range(dashboardSheet[0, secondHeaderRow], dashboardSheet[30, secondHeaderRow]).Interior.Color   = Color.FromArgb(22, 54, 92);
      new Range(dashboardSheet[0, secondHeaderRow], dashboardSheet[30, secondHeaderRow]).Border.Sides     = BorderSides.Bottom;
      new Range(dashboardSheet[0, secondHeaderRow], dashboardSheet[30, secondHeaderRow]).Border.LineStyle = Borderline.Double;
      new Range(dashboardSheet[0, secondHeaderRow], dashboardSheet[30, secondHeaderRow]).Border.Color     = Color.FromArgb(255, 255, 255);
      new Range(dashboardSheet[0, secondHeaderRow], dashboardSheet[30, secondHeaderRow]).Border.Weight    = 1;
      
      // Setting the foreground color for the header text...
      new Range(dashboardSheet[0, firstHeaderRow],  dashboardSheet[8, firstHeaderRow]).Font.Color  = Color.FromArgb(255, 255, 255);
      new Range(dashboardSheet[0, secondHeaderRow], dashboardSheet[8, secondHeaderRow]).Font.Color = Color.FromArgb(255, 255, 255);

      // Setting the font for the header text...
      new Range(dashboardSheet[0, firstHeaderRow],  dashboardSheet[8, firstHeaderRow]).Font.Size  = 18;
      new Range(dashboardSheet[0, firstHeaderRow],  dashboardSheet[8, firstHeaderRow]).Font.Bold  = true;
      new Range(dashboardSheet[0, secondHeaderRow], dashboardSheet[8, secondHeaderRow]).Font.Size = 8;
      new Range(dashboardSheet[0, secondHeaderRow], dashboardSheet[8, secondHeaderRow]).Font.Bold = true;

      dashboardSheet[0, timestampRow].Value     = "Exported on: " + DateTime.Now.ToShortDateString();
      dashboardSheet[0, timestampRow].Font.Bold = true;
			dashboardSheet[0, timestampRow].Font.Size = 8;
      #endregion


      #region Create the table header for all the data...
      // Setting the width of the columns...
      matchesSheet.Columns(0).Width  = 100;
      matchesSheet.Columns(1).Width  = 100;
      matchesSheet.Columns(2).Width  = 100;
      matchesSheet.Columns(3).Width  = 100;
      matchesSheet.Columns(4).Width  = 100;
			matchesSheet.Columns(5).Width  = 100;
			matchesSheet.Columns(6).Width  = 100;
			matchesSheet.Columns(7).Width  = 100;
			matchesSheet.Columns(8).Width  = 100;
			matchesSheet.Columns(9).Width  = 100;
			matchesSheet.Columns(10).Width = 100;
			matchesSheet.Columns(11).Width = 100;
			matchesSheet.Columns(12).Width = 100;
			matchesSheet.Columns(13).Width = 900;

      // Inserting headers and setting them to bold...
      matchesSheet[0,  tableHeaderRow].Value = "BatchId Id";
      matchesSheet[1,  tableHeaderRow].Value = "Timestamp";
      matchesSheet[2,  tableHeaderRow].Value = "Match Id";
      matchesSheet[3,  tableHeaderRow].Value = "Severity";
      matchesSheet[4,  tableHeaderRow].Value = "Project";
      matchesSheet[5,  tableHeaderRow].Value = "Language";
      matchesSheet[6,  tableHeaderRow].Value = "Category";
      matchesSheet[7,  tableHeaderRow].Value = "Category description";
      matchesSheet[8,  tableHeaderRow].Value = "Rule";
			matchesSheet[9,  tableHeaderRow].Value = "Rule description";
			matchesSheet[10, tableHeaderRow].Value = "Rule Expression";
			matchesSheet[11, tableHeaderRow].Value = "File name";
			matchesSheet[12, tableHeaderRow].Value = "Match on line";
			matchesSheet[13, tableHeaderRow].Value = "Code extract";

			new Range(matchesSheet[0, tableHeaderRow], matchesSheet[columns, tableHeaderRow]).Font.Bold = true;
      new Range(matchesSheet[0, tableHeaderRow], matchesSheet[columns, tableHeaderRow]).AutoFilter();
      new Range(matchesSheet[0, tableHeaderRow], matchesSheet[columns, tableHeaderRow]).Border.Sides     = BorderSides.Bottom;
      new Range(matchesSheet[0, tableHeaderRow], matchesSheet[columns, tableHeaderRow]).Border.LineStyle = Borderline.Continuous;
      new Range(matchesSheet[0, tableHeaderRow], matchesSheet[columns, tableHeaderRow]).Border.Weight    = 2;
			#endregion


			#region Insert data...
			// Insert data...
			IMatchProxy matchProxy = ProxyHome.Instance.RetrieveMatchProxy(OutputKeyKeeper.Instance.AccessKey);

			foreach (IMatch match in matchProxy.Matches())
			{
				if ((row % 2) == 0)
          new Range(matchesSheet[0, row], matchesSheet[columns, row]).Interior.Color = Color.FromArgb(220, 230, 241);
        else
          new Range(matchesSheet[0, row], matchesSheet[columns, row]).Interior.Color = Color.FromArgb(197, 217, 241);

				matchesSheet[0,  row].Value = match.Batch.Id;
				matchesSheet[1,  row].Value = $"{match.Batch.TimeStamp.ToShortDateString()} - {match.Batch.TimeStamp.ToShortTimeString()}";
				matchesSheet[2,  row].Value = match.Id;
				matchesSheet[3,  row].Value = $"{RuleSeverityMapper.Int2RuleSeverity((int)match.Severity)}";
				matchesSheet[4,  row].Value = match.ProjectDefinitionRef.Name;
				matchesSheet[5,  row].Value = match.LanguageDeclarationRef.Name;
				matchesSheet[6,  row].Value = match.CategoryDeclarationRef.Name;
				matchesSheet[7,  row].Value = match.CategoryDeclarationRef.Description;
        matchesSheet[8,  row].Value = match.RuleDeclarationRef.Name;
        matchesSheet[9,  row].Value = match.RuleDeclarationRef.Description;
        matchesSheet[10, row].Value = match.RuleDeclarationRef.Expression;
				matchesSheet[11, row].Value = match.Filename;
				matchesSheet[12, row].Value = match.LineNumber;
				matchesSheet[13, row].Value = match.CodeExtract;
				row++;
      }
      #endregion


      #region Export the Excel xml workbook...
      book.Export(OutputFile);
      #endregion
		}
		#endregion ......................................... Interface IOutput impl.
	}
}