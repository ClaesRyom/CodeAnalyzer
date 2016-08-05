<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
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

  
  <xsl:template match="Matches">

    <table bgcolor="#E6EAE9" align="center" border="0" width="95%" cellpadding="0" cellspacing="0" summary="">

      <tr >
        <td style="background:#E6EAE9; width:100%;">
          <div class="Title">
            <xsl:value-of select="Summary/Name" disable-output-escaping="yes" />
          </div>
        </td>
        <td style="background:#E6EAE9;">
          <div style="padding-right:3px;">
            <a  href="allmatches.xml">All</a>
          </div>
        </td>      
      </tr>

      <tr>
        
        <table align="center" border="0" width="100%" cellpadding="0" cellspacing="0" summary="" >

          <!-- ROW: Description -->
          <tr>
            <td width="112px" valign="top" style="padding-left:5px; background: #E6EAE9;font-weight: bold;">
              Description
            </td>
            <td width="5px" valign="top" style="background: #E6EAE9;font-weight: bold;">
              :
            </td>
            <td style="background: #E6EAE9;">
              <div style="padding-left:5px;">
                <xsl:value-of select="Summary/Description" disable-output-escaping="yes" />
              </div>
            </td>
          </tr>

          <!-- ROW: Expression used... -->
          <tr>
            <td width="105px" valign="top" style="padding-left:5px; background: #E6EAE9;font-weight: bold;">
              Expression used
            </td>
            <td width="2px" valign="top" style="background: #E6EAE9;font-weight: bold;">
              :
            </td>
            <td style="background: #E6EAE9;">
              <div style="padding-left:5px;">
                <xsl:value-of select="Summary/Expression" disable-output-escaping="yes" />
              </div>
            </td>
          </tr>


          <!-- ROW: Number of matches found... -->
          <tr>
            <td width="112px" valign="top" style="padding-left:5px; background: #E6EAE9;font-weight: bold;">
              Matches found
            </td>
            <td width="5px" valign="top" style="background: #E6EAE9;font-weight: bold;">
              :
            </td>
            <td style="background: #E6EAE9;">
              <div style="padding-left:5px;">
                <xsl:value-of select="Summary/Count" disable-output-escaping="yes" />
              </div>
            </td>
          </tr>

        
        </table>          
      </tr>

    </table>

    
    <br/>

    
    <table align="center" border="0" width="95%" cellpadding="0" cellspacing="2" summary="">

      <xsl:for-each select="Match">
        <!--<xsl:sort select="Severity/@Value" />-->
        <xsl:sort select="File" />

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
          <th align="left" colspan="2">
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

                    <table align="center" border="0" width="100%" cellpadding="0" cellspacing="0" summary="">

                      <!-- ROW: Line number -->
                      <tr>
                        <td width="70px" valign="top">
                          Match on line: <xsl:value-of select="LineNumber" disable-output-escaping="yes" />
                        </td>
                      </tr>

                      <!-- ROW: Expression used -->
                      <tr>
                        <td width="70px" valign="top">
                          Expression used: <xsl:value-of select="RegExpExpression" disable-output-escaping="yes" />
                        </td>
                      </tr>

                      
                      <!-- ROW: Extract -->
                      <tr>
                        <td style="height:800px; width:100%;">
                          <div style="height:800px; width:100%;">
                            <textarea wrap="off" style="display:block; resize:none; margin-top:4px;margin-left:0px;margin-right:5px; width:99%; height:99%;" >
                              <xsl:value-of select="RegExpSummary" disable-output-escaping="yes" />
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

    </table>

  </xsl:template>
</xsl:stylesheet>