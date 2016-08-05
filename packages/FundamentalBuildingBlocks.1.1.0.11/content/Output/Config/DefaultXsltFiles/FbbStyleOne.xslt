<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2004/07/xpath-functions" xmlns:xdt="http://www.w3.org/2004/07/xpath-datatypes">
  <xsl:output method="xml" version="1.0" encoding="ISO-8859-1" indent="yes"/>

  <xsl:template match="/">
    <html>
      <style type="text/css">

        .TimeStampStyle {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-weight: bold;
        font-size: 7pt;
        color: #505050;
        }

        .InfoStyle {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-weight: bold;
        font-size: 7pt;
        color: #4B39EE;
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

        body {
        font: normal 11px auto "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
        color: #4f6b72;
        background: #E6EAE9;
        }

        a {
        color: #c75f3e;
        }

        #mytable {
        width: 700px;
        padding: 0;
        margin: 0;
        border-collapse: collapse;
        }

        caption {
        padding: 0 0 5px 0;
        width: 700px;
        font: italic 11px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
        text-align: right;
        font-style: italic;
        }

        <!--        
        th { 
          font: bold 11px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif; 
          color: #4f6b72; 
          border-right: 1px solid #C1DAD7; 
          border-bottom: 1px solid #C1DAD7; 
          border-top: 1px solid #C1DAD7; 
          letter-spacing: 2px; 
          text-transform: uppercase; 
          text-align: left; 
          padding: 6px 6px 6px 12px; 
          background: #CAE8EA url(images/bg_header.jpg) no-repeat; 
        }
-->
        th {
        border-right: 1px solid #C1DAD7;
        border-bottom: 1px solid #C1DAD7;
        border-top: 1px solid #C1DAD7;
        padding: 6px 6px 6px 12px;
        }

        thead th:first-child {
        border-top: 0;
        border-left: 0;
        border-right: 1px solid #C1DAD7;
        background: none;
        }

        tbody td {
        border-right: 1px solid #C1DAD7;
        border-bottom: 1px solid #C1DAD7;
        background: #fff;
        padding: 6px 6px 6px 12px;
        color: #4f6b72;
        }

        <!--
        tbody tr:nth-child(odd) td { 
          background: #F5FAFA; 
          color: #797268; 
        }
-->

        <!--        
        tbody th{ 
          font: bold 10px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif; 
          border-left: 1px solid #C1DAD7; 
          border-top: 0; 
          font: bold; 
          background: #fff url(images/bullet1.gif) no-repeat; 
        } 
-->
        tbody th{
        border-left: 1px solid #C1DAD7;
        border-top: 0;
        font: bold;
        background: #fff url(images/bullet1.gif) no-repeat;
        }

        <!--     
        tbody tr:nth-child(odd) th { 
          background: #f5fafa url(images/bullet2.gif) no-repeat; 
          color: #797268; 
        } 
-->
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
      <tr width="100%" bgcolor="#eaeaea">
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


  <!-- WARNING TEMPLATE -->
  <xsl:template match="Log/Warning">
    <table border="0" width="100%" cellpadding="5" cellspacing="0" summary="">
      <tr width="100%" bgcolor="#eaeaea">
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
      <tr width="100%" bgcolor="#eaeaea">
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
