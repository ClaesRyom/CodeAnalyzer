<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <meta charset="UTF-8"/>
      <head>
        <link rel="Stylesheet" type="text/css" href="css/common.css" media="screen" />
      </head>

      <script type="text/javascript" src="javascript/common.js"></script>

      <body bgcolor="#FFFFFF">

        <!-- LET'S JUST CALL THE TEMPLATES AND LET THE XSLT PARSER DECIDE WHICH TEMPLATE TO ACTIVATE. -->
        <xsl:apply-templates/>

      </body>

    </html>
  </xsl:template>


  <xsl:template match="Rules">
    <table bgcolor="#E6EAE9" align="center" border="0" width="100%" cellpadding="0" cellspacing="0" summary="">
      <tr >
        <td style="width:100%;">
          <div class="Title">
            Rules
          </div>
        </td>
        <td style="background:#E6EAE9;">
          <div style="padding-right:3px;">
            <a target="rulesFrame" href="allrules.xml">All</a>
          </div>
        </td>      
      </tr>
    </table>

    <br/>    
    
    <table align="left" border="0" width="100%" cellpadding="0" cellspacing="2" summary="">

      <xsl:for-each select="Rule">
        <xsl:sort select="Severity" />

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

        <tr>
          <th align="left" colspan="2">
            <b class="VariableNames">

              <div id="leftHeaderMargin" class="SectionHeader" style="display:block">
                <img class="floatL" id="{@Id}img" onclick="changeimage('{@Id}')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-left:0px;margin-right:5px;"/>
                <div class="floatL">
                  <xsl:value-of select="Name"/> (<xsl:value-of select="Count" disable-output-escaping="yes" />)
                </div>
                <div class="floatR" style="margin-top:2px;margin-left:0px;margin-right:10px;">
                  <xsl:value-of select="Severity" disable-output-escaping="yes" />
                </div>
              </div>


              <div id="topContentMargin" style="display:block;">
                <div class="note" id="{@Id}" style="display:{$styleDisplay};margin-top:0px">

                  <div class="noteelement">

                    <table class="" align="center" border="0" width="100%" cellpadding="0" cellspacing="0" summary="">

                      <!-- ROW: Count -->
                      <!--
                      <tr>
                        <td width="70px" valign="top">
                          Count:
                        </td>
                        <td>
                          <div class="float Text">
                            <xsl:value-of select="Count" disable-output-escaping="yes" />
                          </div>
                        </td>
                      </tr>
                      -->

                      <!-- ROW: Description -->
                      <tr>
                        <td width="70px" valign="top">
                          Description:
                        </td>
                        <td>
                          <div class="float Text">
                            <xsl:value-of select="Description" disable-output-escaping="yes" />
                          </div>
                        </td>
                      </tr>

                      <!-- ROW: Expression -->
                      <tr>
                        <td width="70px" valign="top">
                          Expression:
                        </td>
                        <td>
                          <div class="float Text">
                            <xsl:value-of select="Expression" disable-output-escaping="yes" />
                          </div>
                        </td>
                      </tr>

                      <!-- ROW: File link -->
                      <tr>
                        <td width="70px" valign="top">
                          Link:
                        </td>
                        <td>
                          <div class="float Text">
                            <a ><xsl:attribute name="href"><xsl:value-of select="Link" disable-output-escaping="yes" /></xsl:attribute>Matches...</a>
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

    </table>

  </xsl:template>
</xsl:stylesheet>
