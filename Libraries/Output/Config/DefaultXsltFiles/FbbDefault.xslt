<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2004/07/xpath-functions" xmlns:xdt="http://www.w3.org/2004/07/xpath-datatypes">
  <xsl:output method="xml" version="1.0" encoding="ISO-8859-1" indent="yes"/>
  <xsl:template match="/">
    <html>

      <!-- STYLE SHEET STUFF -->
      <style type="text/css">

        .TimeStampStyle {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-weight: bold;
        font-size: 7pt;
        color: #505050;
        }

        .ZoneThreadStyle {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-weight: bold;
        font-size: 7pt;
        color: #505050;
        }

        .MsgStyle {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-weight: bold;
        font-size: 7pt;
        color: #505050;
        }

      </style>

      <body bgcolor="#FFFFFF">

        <!-- LET'S JUST CALL THE TEMPLATES AND LET THE XSLT PARSER 
             DECIDE WHICH TEMPLATE TO ACTIVATE. -->
        <xsl:apply-templates/>
        
      </body>
      
    </html>
  </xsl:template>


  <!-- DEBUG TEMPLATE -->
  <xsl:template match="Log/Debug">
    <table border="0" width="100%" cellpadding="5" cellspacing="0" summary="">
      <tr width="100%" bgcolor="#eaeaea">
        <th align="left" colspan="1">
          <b class="TimeStampStyle">
            <xsl:value-of select="TimeStamp"/>
          </b>
        </th>
        <th align="right" colspan="1">
          <b class="TimeStampStyle">Debug</b>
        </th>
      </tr>
      <tr>
        <th width="150" align="left" valign="top" colspan="1" bgcolor="#FFFFFF">
          <font class="ZoneThreadStyle">
            Z: <xsl:value-of select="Zone"/><br/>
            T: <xsl:value-of select="Thread"/>
          </font>
        </th>
        <th align="left" valign="top" colspan="1" bgcolor="#FFFFFF">
          <xsl:for-each select="Msg/Line">
            <font class="MsgStyle">
              <xsl:value-of select="."/>
              <br/>
            </font>
          </xsl:for-each>
        </th>
      </tr>
    </table>
  </xsl:template>


  <!-- INFO TEMPLATE -->
  <xsl:template match="Log/Info">
    <table border="0" width="100%" cellpadding="5" cellspacing="0" summary="">
      <tr bgcolor="#99CCFF">
        <th align="left" colspan="1">
          <b class="TimeStampStyle">
            <xsl:value-of select="TimeStamp"/>
          </b>
        </th>
        <th align="right" colspan="1">
          <b class="TimeStampStyle">Info</b>
        </th>
      </tr>
      <tr>
        <th width="150" align="left" valign="top" colspan="1" bgcolor="#FFFFFF">
          <font  class="ZoneThreadStyle">
            Z: <xsl:value-of select="Zone"/><br/>
            T: <xsl:value-of select="Thread"/>
          </font>
        </th>
        <th align="left" valign="top" colspan="1" bgcolor="#FFFFFF">
          <xsl:for-each select="Msg/Line">
            <font class="MsgStyle">
              <xsl:value-of select="."/>
              <br/>
            </font>
          </xsl:for-each>
        </th>
      </tr>
    </table>
  </xsl:template>


  <!-- WARNING TEMPLATE -->
  <xsl:template match="Log/Warning">
    <table border="0" width="100%" cellpadding="5" cellspacing="0" summary="">
      <tr bgcolor="#F2FF4F">
        <th align="left" colspan="1">
          <b class="TimeStampStyle">
            <xsl:value-of select="TimeStamp"/>
          </b>
        </th>
        <th align="right" colspan="1">
          <b class="TimeStampStyle">Warning</b>
        </th>
      </tr>
      <tr>
        <th width="150" align="left" valign="top" colspan="1" bgcolor="#FFFFFF">
          <font class="ZoneThreadStyle">
            Z: <xsl:value-of select="Zone"/><br/>
            T: <xsl:value-of select="Thread"/>
          </font>
        </th>
        <th align="left" valign="top" colspan="1" bgcolor="#FFFFFF">
          <xsl:for-each select="Msg/Line">
            <font class="MsgStyle">
              <xsl:value-of select="."/>
              <br/>
            </font>
          </xsl:for-each>
        </th>
      </tr>
    </table>
  </xsl:template>


  <!-- ERROR TEMPLATE -->
  <xsl:template match="Log/Error">
    <table border="0" width="100%" cellpadding="5" cellspacing="0" summary="">
      <tr bgcolor="#FF5858">
        <th align="left" colspan="1">
          <b class="TimeStampStyle">
            <xsl:value-of select="TimeStamp"/>
          </b>
        </th>
        <th align="right" colspan="1">
          <b class="TimeStampStyle">Error</b>
        </th>
      </tr>
      <tr>
        <th width="150" align="left" valign="top" colspan="1" bgcolor="#FFFFFF">
          <font class="ZoneThreadStyle">
            Z: <xsl:value-of select="Zone"/><br/>
            T: <xsl:value-of select="Thread"/>
          </font>
        </th>
        <th align="left" valign="top" colspan="1" bgcolor="#FFFFFF">
          <xsl:for-each select="Msg/Line">
            <font class="MsgStyle">
              <xsl:value-of select="."/>
              <br/>
            </font>
          </xsl:for-each>
        </th>
      </tr>
    </table>
  </xsl:template>

</xsl:stylesheet>
