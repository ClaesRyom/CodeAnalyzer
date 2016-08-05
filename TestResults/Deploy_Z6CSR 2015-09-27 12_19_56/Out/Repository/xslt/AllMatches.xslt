<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <head>
        <title>Code Analyser Report</title>
        <link rel="Stylesheet" type="text/css" href="css/common.css" media="screen" />
      </head>

      <script type="text/javascript" src="javascript/common.js"></script>
      <script type="text/javascript">
        document.getElementById(click-div).title="New tooltip"
      </script>

      <div class="floatL" style="padding-top:50px;">
        <div id="click-div" title="Back to the Summary page">
          <a  href="summary.xml" class="fill-div"></a>
        </div>
      </div>

      <body bgcolor="#FFFFFF">

        <!-- LET'S JUST CALL THE TEMPLATES AND LET THE XSLT PARSER DECIDE WHICH TEMPLATE TO ACTIVATE. -->
        <xsl:apply-templates/>

      </body>

    </html>
  </xsl:template>


  <xsl:template match="Matches">

    <div>
      <table bgcolor="#E6EAE9" align="center" border="0" width="95%" cellpadding="0" cellspacing="0" summary="">

        <tr >
          <td style="background:#E6EAE9; width:100%;">
            <div class="Title">
              <div Id="Title" >Matches</div>
              <script>replaceTitle();</script>
            </div>
          </td>
        </tr>

        <tr>
          <table style="padding-top:14px;" align="center" border="0" width="100%" cellpadding="0" cellspacing="2" summary="">

            <xsl:for-each select="Match">
              <xsl:sort select="Severity/@Value" />
              <!--<xsl:sort select="File" />-->

              <!-- BEGIN:: Variables for each Category -->
              <xsl:variable name="styleDisplay">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="src">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'">images/minus.png</xsl:when>
                  <xsl:otherwise>images/plus.png</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>
              <!-- END:: Variables for each Category -->

              <tr >
                <th id="border-blue" align="left" colspan="2">
                  <b class="VariableNames">

                    <div id="leftHeaderMargin" class="SectionHeader" style="display:block">
                      <img class="floatL" id="{@Id}img" onclick="changeimage('{@Id}')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-left:0px;margin-right:5px;"/>
                      <div class="floatL">
                        <xsl:value-of select="@Name"/>
                      </div>
                      <div class="floatR" style="margin-top:2px;margin-left:0px;margin-right:10px;">
                        <xsl:value-of select="Severity" disable-output-escaping="yes" />
                      </div>
                    </div>


                    <div id="topContentMargin" style="display:block;">
                      <div class="note" id="{@Id}" style="display:{$styleDisplay};margin-top:0px">

                        <div class="noteelement">

                          <table align="center" border="0" width="98%" cellpadding="0" cellspacing="0" summary="">

                            <!-- ROW: Project name -->
                            <tr>
                              <td width="98px" valign="top" style="padding-left:5px; font-weight: bold;">
                                Project
                              </td>
                              <td width="5px" valign="top" style="font-weight: bold;">
                                :
                              </td>
                              <td>
                                <div class="float Text" style="padding-left:5px;">
                                  <xsl:value-of select="Project" disable-output-escaping="yes" />
                                </div>
                              </td>
                            </tr>

                            <!-- ROW: Category name -->
                            <tr>
                              <td valign="top" style="padding-left:5px; font-weight: bold;">
                                Category
                              </td>
                              <td width="5px" valign="top" style="font-weight: bold;">
                                :
                              </td>
                              <td>
                                <div class="float Text" style="padding-left:5px;">
                                  <xsl:value-of select="Category" disable-output-escaping="yes" />
                                </div>
                              </td>
                            </tr>

                            <!--ROW: Rule name-->
                            <tr>
                              <td valign="top" style="padding-left:5px; padding-top:7px; font-weight: bold;">
                                Rule name
                              </td>
                              <td width="5px" valign="top" style="padding-top:7px; font-weight: bold;">
                                :
                              </td>
                              <td>
                                <div class="float Text" style="padding-left:5px; padding-top:7px;">
                                  <xsl:value-of select="RuleName" disable-output-escaping="yes" />
                                </div>
                              </td>
                            </tr>

                            <!--ROW: Rule description-->
                            <tr>
                              <td valign="top" style="padding-left:5px; font-weight: bold;">
                                Rule Description
                              </td>
                              <td width="5px" valign="top" style="font-weight: bold;">
                                :
                              </td>
                              <td>
                                <div class="float Text" style="padding-left:5px;">
                                  <xsl:value-of select="RuleDescription" disable-output-escaping="yes" />
                                </div>
                              </td>
                            </tr>

                            <!--ROW: Expression used-->
                            <tr>
                              <td valign="top" style="padding-left:5px; font-weight: bold;">
                                Rule Expression
                              </td>
                              <td width="5px" valign="top" style="font-weight: bold;">
                                :
                              </td>
                              <td>
                                <div class="float Text" style="padding-left:5px;">
                                  <xsl:value-of select="RuleExpression" disable-output-escaping="yes" />
                                </div>
                              </td>
                            </tr>

                            <!--ROW: Root directory-->
                            <!--<tr>
                              <td valign="top" style="padding-left:5px; padding-top:10px; font-weight: bold;">
                                Root directory
                              </td>
                              <td width="5px" valign="top" style="padding-top:10px; font-weight: bold;">
                                :
                              </td>
                              <td>
                                <div class="float Text" style="padding-left:5px;padding-top:10px; ">
                                  <xsl:value-of select="RootDirectory" disable-output-escaping="yes" />
                                </div>
                              </td>
                            </tr>-->

                            <!--ROW: File name-->
                            <tr>
                              <td valign="top" style="padding-left:5px; padding-top:7px; font-weight: bold;">
                                Filename
                              </td>
                              <td width="5px" valign="top" style="padding-top:7px; font-weight: bold;">
                                :
                              </td>
                              <td>
                                <div class="float Text" style="padding-top:7px; padding-left:5px;">
                                  <xsl:value-of select="FileName" disable-output-escaping="yes" />
                                </div>
                              </td>
                            </tr>


                            <!--ROW: Line number-->
                            <tr>
                              <td valign="top" style="padding-left:5px; padding-top:7px; font-weight: bold;">
                                Match on line
                              </td>
                              <td width="5px" valign="top" style="padding-top:7px; font-weight: bold;">
                                :
                              </td>
                              <td>
                                <div class="float Text" style="padding-left:5px; padding-top:7px;">
                                  <xsl:value-of select="LineNumber" disable-output-escaping="yes" />
                                </div>
                              </td>
                            </tr>

                          </table>

                          <table align="center" border="0" width="98%" cellpadding="0" cellspacing="0" summary="">
                            <!--ROW: Code extract-->
                            <tr>
                              <td style="height:800px; width:100%;">
                                <div style="height:800px; width:100%;">
                                  <textarea id="border-blue" wrap="off" style="display:block; resize:none; margin-top:4px;margin-left:5px;margin-right:10px; width:99%; height:99%;" >
                                    <xsl:value-of select="CodeExtract" disable-output-escaping="yes" />
                                  </textarea>
                                </div>
                              </td>
                            </tr>
                          </table>

                        </div>

                      </div>
                    </div>

                  </b>
                </th>
              </tr>
            </xsl:for-each>

            <!-- COPYRIGHT... -->
            <div class="floatL" style="padding-top:7px;padding-left:5px; width:100%;">
              <table align="left" border="0" width="100%" cellpadding="0" cellspacing="3" summary="" >
                <tr>
                  This report was generated by <b>CLR | Code Analyzer</b>. Copyright © 2013 <b>Claes Ryom</b>, All Rights Reserved.
                </tr>
              </table>
            </div>
            
          </table>
        </tr>
      </table>
    </div>

  </xsl:template>
</xsl:stylesheet>