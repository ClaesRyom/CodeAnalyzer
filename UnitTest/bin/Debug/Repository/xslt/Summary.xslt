<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <head>
        <title>Code Analyser Report</title>
        <link rel="Stylesheet" type="text/css" href="css/common.css" media="screen" />
      </head>

      <script type="text/javascript" src="javascript/common.js"></script>

      <body bgcolor="#FFFFFF">

        <script type="text/javascript">
          function WriteToWindowName()
          {
          window.localStorage.setItem("key", "summary.xslt")
          //window.sessionStorage.setItem('name', "This message will survive between page loads.");
          }
        </script>


        <div class="floatL" width="10px">
        </div>

        <!-- LET'S JUST CALL THE TEMPLATES AND LET THE XSLT PARSER DECIDE WHICH TEMPLATE TO ACTIVATE. -->
        <xsl:apply-templates/>

      </body>

    </html>
  </xsl:template>


  <xsl:template match="Summary">
    <div>
      <table align="center" border="0" width="95%" cellpadding="0" cellspacing="0" summary="">

        <tr >
          <td style="width:100%;">
            <div class="Title">
              Report Summary
            </div>
          </td>
        </tr>

        <tr>

          <!-- REPORT CREATION TIMESTAMP... -->
          <div class="floatL" style="padding-top:15px;padding-left:5px;width:100%;">
            <table id="border-blue" bgcolor="#FFFFFF" align="left" border="0" width="50%" cellpadding="0" cellspacing="3" summary="" >

              <!-- ROW: Creation... -->
              <tr>
                <td width="150px" valign="top" style="padding-top:5px; padding-left:7px; font-weight: bold;">
                  Report was created
                </td>
                <td width="5px" valign="top" style="padding-top:5px; font-weight: bold;">
                  :
                </td>
                <td >
                  <div style="padding-top:5px; padding-left:5px; font-weight: bold;">
                    <xsl:value-of select="Creation" disable-output-escaping="yes" />
                  </div>
                </td>
              </tr>

              <!-- ROW: Number of files found... -->
              <tr>
                <td valign="top" style="padding-left:7px; padding-bottom:7px; font-weight: bold;">
                  Files examined
                </td>
                <td width="5px" valign="top" style="padding-bottom:7px; font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px; padding-bottom:7px; font-weight: bold;">
                    <a  href="FilesSearched.xml">
                      <xsl:value-of select="NumberOfFilesSearched" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>

            </table>
          </div>



          <!-- DECLARATIONS... -->
          <div class="floatL" style="padding-top:7px;padding-left:5px; width:100%;">
            <table id="border-blue" bgcolor="#FFFFFF" align="left" border="0" width="50%" cellpadding="0" cellspacing="3" summary="" >

              <!-- ROW: Declarations... -->
              <tr>
                <td width="150px" valign="top" style="padding-top:5px; padding-left:7px;font-weight: bold;">
                  Declarations
                </td>
              </tr>


              <!-- ROW: Number of languages declared... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Language declarations
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px; font-weight: bold;">
                    <a onclick="WriteToWindowName()"  href="languagesdeclarations.xml">
                      <xsl:value-of select="NumberOfLanguageDeclarations" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of categories declared... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Category declarations
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px; font-weight: bold;">
                    <a  href="categorydeclarations.xml">
                      <xsl:value-of select="NumberOfCategoryDeclarations" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of rules declared... -->
              <tr>
                <td valign="top" style="padding-left:17px;padding-bottom:7px;">
                  - Rule declarations
                </td>
                <td width="5px" valign="top" style="padding-bottom:7px;font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px;padding-bottom:7px;font-weight: bold;">
                    <a  href="ruledeclarations.xml">
                      <xsl:value-of select="NumberOfRuleDeclarations" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>
            </table>
          </div>



          <!-- DEFINITIONS... -->
          <div class="floatL" style="padding-top:7px;padding-left:5px; width:100%;">
            <table id="border-blue" bgcolor="#FFFFFF" align="left" border="0" width="50%" cellpadding="0" cellspacing="3" summary="" >
              <!-- ROW: Number of projects defined... -->
              <tr>
                <td width="150px" valign="top" style="padding-top:5px; padding-left:8px;font-weight: bold;">
                  Project definitions
                </td>
                <td width="2px" valign="top" style="padding-top:5px;font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-top:5px; padding-left:5px; font-weight: bold;">
                    <a  href="projectdefinitions.xml">
                      <xsl:value-of select="NumberOfTotalProjectDefinitions" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of active projects... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Active projects
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px; ">
                    <xsl:value-of select="NumberOfActiveProjectDefinitions" disable-output-escaping="yes" />
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of inactive projects... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Inactive projects
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px; ">
                    <xsl:value-of select="NumberOfInactiveProjectDefinitions" disable-output-escaping="yes" />
                  </div>
                </td>
              </tr>




              <!-- ROW: Number of categories defined... -->
              <tr>
                <td valign="top" style="padding-top:10px; padding-left:8px; font-weight: bold;">
                  Category definitions
                </td>
                <td width="2px" valign="top" style="padding-top:10px; font-weight: bold;">
                  :
                </td>
                <td >
                  <div style="padding-top:5px; padding-left:5px; font-weight: bold;">
                    <a  href="categorydefinitions.xml">
                      <xsl:value-of select="NumberOfTotalCategoryDefinitions" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of active categories... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Active categories
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px; ">
                    <xsl:value-of select="NumberOfActiveCategoryDefinitions" disable-output-escaping="yes" />
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of inactive categories... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Inactive categories
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px; ">
                    <xsl:value-of select="NumberOfInactiveCategoryDefinitions" disable-output-escaping="yes" />
                  </div>
                </td>
              </tr>




              <!-- ROW: Number of rules defined... -->
              <tr>
                <td valign="top" style="padding-top:10px; padding-left:8px;font-weight: bold;">
                  Rule definitions
                </td>
                <td width="2px" valign="top" style="padding-top:10px;font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-top:5px; padding-left:5px; font-weight: bold;">
                    <a  href="ruledefinitions.xml">
                      <xsl:value-of select="NumberOfTotalRuleDefinitions" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of active rules... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Active rules
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px; ">
                    <xsl:value-of select="NumberOfActiveRuleDefinitions" disable-output-escaping="yes" />
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of inactive rules... -->
              <tr>
                <td valign="top" style="padding-left:17px;padding-bottom:7px;">
                  - Inactive rules
                </td>
                <td width="5px" valign="top" style="padding-bottom:7px;font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px;padding-bottom:7px;">
                    <xsl:value-of select="NumberOfInactiveRuleDefinitions" disable-output-escaping="yes" />
                  </div>
                </td>
              </tr>
            </table>
          </div>


          <!-- MATCHES... -->
          <div class="floatL" style="padding-top:7px;padding-left:5px; width:100%;">
            <table id="border-blue" bgcolor="#FFFFFF" align="left" border="0" width="50%" cellpadding="0" cellspacing="3" summary="" >

              <!-- ROW: Number of total matches found... -->
              <tr>
                <td width="150px" valign="top" style="padding-top:5px; padding-left:8px;font-weight: bold;">
                  Matches
                </td>
                <td width="5px" valign="top" style="padding-top:5px;font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-top:5px; padding-left:5px; font-weight: bold;">
                    <a href="allmatches.xml?type=All">
                      <xsl:value-of select="NumberOfTotalMatches" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of Info matches found... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Info matches
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px; ">
                    <a href="infomatches.xml?type=Info">
                      <xsl:value-of select="NumberOfInfoMatches" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of Warning matches found... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Warning matches
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px;">
                    <a href="warningmatches.xml?type=Warning">
                      <xsl:value-of select="NumberOfWarningMatches" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of Critical matches found... -->
              <tr>
                <td valign="top" style="padding-left:17px;">
                  - Critical matches
                </td>
                <td width="5px" valign="top" style="font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px;">
                    <a href="criticalmatches.xml?type=Critical">
                      <xsl:value-of select="NumberOfCriticalMatches" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>


              <!-- ROW: Number of Fatal matches found... -->
              <tr>
                <td valign="top" style="padding-left:17px;padding-bottom:7px;">
                  - Fatal matches
                </td>
                <td width="5px" valign="top" style="padding-bottom:7px;font-weight: bold;">
                  :
                </td>
                <td>
                  <div style="padding-left:5px;padding-bottom:7px;">
                    <a href="fatalmatches.xml?type=Fatal">
                      <xsl:value-of select="NumberOfFatalMatches" disable-output-escaping="yes" />
                    </a>
                  </div>
                </td>
              </tr>
            </table>
          </div>


          <!-- COPYRIGHT... -->
          <div class="floatL" style="padding-top:7px;padding-left:5px; width:100%;">
            <table align="left" border="0" width="100%" cellpadding="0" cellspacing="3" summary="" >
              <tr>
                This report was generated by <b>CLR | Code Analyzer</b>. Copyright © 2013 <b>Claes Ryom</b>, All Rights Reserved.
                
                <a href="ruledefinitions.xml#13a45814-8fa5-4149-84ed-08c5753fe2ad">Rule definition "Exception vs. ApplicationException".</a>
              </tr>
            </table>
          </div>

        </tr>
      </table>
    </div>
  </xsl:template>
</xsl:stylesheet>