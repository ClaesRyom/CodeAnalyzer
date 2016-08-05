<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <meta charset="UTF-8"/>
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


  <xsl:template match="RuleDefinitions">

    <div>
      <table bgcolor="#E6EAE9" align="center" border="0" width="95%" cellpadding="0" cellspacing="0" summary="">

        <tr >
          <td style="width:100%;">
            <div class="Title">
              Rule Definitions
            </div>
          </td>
        </tr>

        <tr>
          <table style="padding-top:14px;" align="left" border="0" width="100%" cellpadding="0" cellspacing="2" summary="">

            <xsl:for-each select="RuleDefinition">
              <xsl:sort select="@RefName" />

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


              <tr>
                <th id="border-blue" align="left" colspan="2">
                  <b class="VariableNames">

                    <div id="leftHeaderMargin" class="SectionHeader" style="display:block">

                      <img class="floatL" id="{@Id}img" onclick="changeimage('{@Id}')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>

                      <div class="floatL">
                        <xsl:if test="@Enabled = 'True'">
                          <div class="floatL" >
                            <xsl:value-of select="@RefName" />
                          </div>
                        </xsl:if>
                        <xsl:if test="@Enabled = 'False'">
                          <div class="floatL" style="color:#CACACA;">
                            <xsl:value-of select="@RefName" />
                          </div>
                        </xsl:if>
                      </div>


                      <div id="topContentMargin" style="display:block;">
                        <div class="note" id="{@Id}" style="display:{$styleDisplay};margin-top:0px">

                          <div class="noteelement">

                            <table align="center" border="0" width="98%" cellpadding="0" cellspacing="0" summary="">

                              <!-- ROW: Rule description -->
                              <tr>
                                <td valign="top" width="190px" style="padding-left:5px; font-weight: bold;">
                                  Rule description
                                </td>
                                <td width="5px" valign="top" style="font-weight: bold;">
                                  :
                                </td>
                                <td>
                                  <div class="float Text" style="padding-left:5px;">
                                    <xsl:value-of select="Description" disable-output-escaping="yes" />
                                  </div>
                                </td>
                              </tr>

                              <!-- ROW: Project Definition Relation -->
                              <tr>
                                <td valign="top" style="padding-left:5px; font-weight: bold;">
                                  Part of Project definition
                                </td>
                                <td width="5px" valign="top" style="font-weight: bold;">
                                  :
                                </td>
                                <td>
                                  <div class="float Text" style="padding-left:5px;">
                                    <xsl:value-of select="ProjectDefinitionRelation" disable-output-escaping="yes" />
                                  </div>
                                </td>
                              </tr>

                              <!-- ROW: Category Declaration Relation -->
                              <tr>
                                <td valign="top" style="padding-left:5px; font-weight: bold;">
                                  Part of Category declaration
                                </td>
                                <td width="5px" valign="top" style="font-weight: bold;">
                                  :
                                </td>
                                <td>
                                  <div class="float Text" style="padding-left:5px;">
                                    <xsl:value-of select="CategoryDeclarationRelation" disable-output-escaping="yes" />
                                  </div>
                                </td>
                              </tr>

                            </table>
                          </div>

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
