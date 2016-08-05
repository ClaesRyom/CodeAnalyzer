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


  <xsl:template match="Projects">

    <div>
      <table bgcolor="#E6EAE9" align="center" border="0" width="95%" cellpadding="0" cellspacing="0" summary="">

        <tr >
          <td style="width:100%;">
            <div class="Title">
              Files found during search
            </div>
          </td>
          <!--<td style="background:#E6EAE9;">
            <div style="padding-right:3px;">
              <a target="rulesFrame" href="allrules.xml">All</a>
            </div>
          </td>-->
        </tr>

        <tr>
          <table style="padding-top:14px;" align="left" border="0" width="100%" cellpadding="0" cellspacing="2" summary="">

            <xsl:for-each select="Project">
              <xsl:sort select="@Name" />

              <!-- BEGIN:: Variables for each Project -->
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
              <!-- END:: Variables for each Project -->

              <tr>
                <th id="border-blue" align="left" colspan="2">
                  <b class="VariableNames">

                    <div id="leftHeaderMargin" class="SectionHeader" style="display:block">
                      <img class="floatL" id="{@Name}img" onclick="changeimage('{@Name}')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-left:0px;margin-right:5px;"/>
                      <div class="floatL">
                        <xsl:value-of select="@Name"/> (<xsl:value-of select="@Id" disable-output-escaping="yes" />)
                      </div>
                    </div>


                    <div id="topContentMargin" style="display:block;">
                      <div class="note" id="{@Name}" style="display:{$styleDisplay};margin-top:0px">

                        <div class="noteelement">

                          <table class="" align="center" border="0" width="100%" cellpadding="0" cellspacing="0" summary="">

                            <!-- ROW: Description -->
                            <tr>
                              <td width="120px" valign="top">
                                Root dir:
                              </td>
                              <td>
                                <div class="float Text">
                                  <xsl:value-of select="RootDirectory/@Path" disable-output-escaping="yes" />
                                </div>
                              </td>
                            </tr>

                            <br/>

                            <!-- ROW: Expression -->
                            <tr>
                              <td width="70px" valign="top">
                                Files found during search in root dir:
                              </td>
                              <td>
                                <div class="float Text">
                                  <xsl:for-each select="RootDirectory/File">
                                    <xsl:sort select="@Path" />
                                    <xsl:value-of select="@Path" disable-output-escaping="yes" />
                                    <br/>
                                  </xsl:for-each>
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
