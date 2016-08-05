<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <head>
        <link rel="Stylesheet" type="text/css" href="css/common.css" media="screen" />
      </head>

      <script type="text/javascript" src="javascript/common.js"></script>

      <script type="text/javascript">
        function ReadFromWindowName()
        {
          window.location = window.localStorage.getItem("key");
        }
      </script>


      <script type="text/javascript">
        document.getElementById(click-div).title="New tooltip"
      </script>

      <div class="floatL" style="padding-top:50px;">
        <div id="click-div" title="Back to the Summary page">
          <a  href="summary.xml" class="fill-div"></a>
        </div>
      </div>


      <body bgcolor="#FFFFFF">
        
        <button type="button" onclick="ReadFromWindowName()">Try it</button>
        

        <!-- LET'S JUST CALL THE TEMPLATES AND LET THE XSLT PARSER DECIDE WHICH TEMPLATE TO ACTIVATE. -->
        <xsl:apply-templates/>
        
      </body>

    </html>
  </xsl:template>


  <xsl:template match="LanguageDeclarations">

    <div>
      <table bgcolor="#E6EAE9" align="center" border="0" width="95%" cellpadding="0" cellspacing="0" summary="">

        <tr >
          <td style="width:100%;">
            <div class="Title">
              Language Declarations
            </div>
          </td>
        </tr>

        <tr>

          <table style="padding-top:15px;" align="center" border="0" width="100%" cellpadding="0" cellspacing="3" summary="" >
            <xsl:for-each select="LanguageDeclaration">
              <xsl:sort select="Name" />

              <!--ROW: Language...-->
              <tr>
                <td width="105px" valign="top" style="padding-top:5px; padding-left:15px; background: #E6EAE9;font-weight: bold;">
                  Language name
                </td>
                <td width="5px" valign="top" style="padding-top:5px; background: #E6EAE9;font-weight: bold;">
                  :
                </td>
                <td style="padding-top:5px; background: #E6EAE9;">
                  <div style="padding-left:5px; ">
                    <xsl:value-of select="Name" disable-output-escaping="yes" /> (<xsl:value-of select="Extension" disable-output-escaping="yes" />)
                  </div>
                </td>
              </tr>

            </xsl:for-each>
          </table>
        </tr>
      </table>
    </div>
    
  </xsl:template>
</xsl:stylesheet>