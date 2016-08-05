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


  <xsl:template match="ProjectDefinitions">

    <div>
      <table bgcolor="#E6EAE9" align="center" border="0" width="95%" cellpadding="0" cellspacing="0" summary="">

        <tr >
          <td style="width:100%;">
            <div class="Title">
              Project Definitions
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

            <xsl:for-each select="ProjectDefinition">
              <xsl:sort select="@Name" />

              <xsl:variable name="styleDisplay">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>



              <xsl:variable name="styleDirsIncludedEnabled">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="styleDirsIncludedDisabled">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="styleDirsExcludedEnabled">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="styleDirsExcludedDisabled">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>



              <xsl:variable name="styleFilesIncludedEnabled">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="styleFilesIncludedDisabled">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="styleFilesExcludedEnabled">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="styleFilesExcludedDisabled">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="styleIO">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="styleCategories">
                <xsl:choose>
                  <xsl:when test="@Open = 'true'"></xsl:when>
                  <xsl:otherwise>none</xsl:otherwise>
                </xsl:choose>
              </xsl:variable>

              <xsl:variable name="styleCategory">
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

                    <xsl:variable name="isProjectEnabled" select="@Enabled" />

                    <div id="leftHeaderMargin" class="SectionHeader" style="display:block">
                      <xsl:if test="$isProjectEnabled = 'False'">
                        <img class="floatL" id="{@Name}img" onclick="changeimage('{@Name}')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-left:0px;margin-right:5px;"/>
                        <div class="floatL" style="color:#CACACA;">
                          <xsl:value-of select="@Name"/>
                          <!--(<xsl:value-of select="@Id"  />)-->
                        </div>
                      </xsl:if>
                      <xsl:if test="$isProjectEnabled = 'True'">
                        <img class="floatL" id="{@Name}img" onclick="changeimage('{@Name}')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-left:0px;margin-right:5px;"/>
                        <div class="floatL">
                          <xsl:value-of select="@Name"/>
                          <!--(<xsl:value-of select="@Id"  />)-->
                        </div>
                      </xsl:if>
                    </div>


                    <div id="topContentMargin" style="display:block;">

                      <xsl:if test="$isProjectEnabled = 'True'">

                        <div class="note" id="{@Name}" style="display:{$styleDisplay};margin-top:0px">

                          <div class="noteelement">

                            <table class="" align="center" border="0" width="100%" cellpadding="0" cellspacing="0" summary="">

                              <!-- ROW: Directories and Files -->
                              <tr>
                                <div class="SectionHeader">
                                  <img class="floatL" id="{@Name}IOimg" onclick="changeimage('{@Name}IO')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                  <div class="floatL" >
                                    Directories and Files
                                  </div>
                                  <br/>
                                  <div style="display:block;">
                                    <div class="float Text" id="{@Name}IO" style="display:{$styleIO};margin-top:5px;margin-left:15px;" >


                                      <table class="" align="center" border="0" width="100%" cellpadding="0" cellspacing="0" >

                                        <!-- ROW: Directories included (Enabled)... -->
                                        <tr>
                                          <div class="SectionHeader" style="margin-top:5px;">
                                            <img class="floatL" id="{@Name}DirsIncludedEnabledimg" onclick="changeimage('{@Name}DirsIncludedEnabled')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                            <div class="floatL" >
                                              Directories included
                                            </div>
                                            <br/>
                                            <div style="display:block;">
                                              <div class="float Text" id="{@Name}DirsIncludedEnabled" style="display:{$styleDirsIncludedEnabled};margin-top:5px;margin-left:15px;" >
                                                <xsl:for-each select="Directories/Include/Directory">
                                                  <xsl:sort select="@Path" />
                                                  <xsl:if test="@Enabled = 'True'">
                                                    <xsl:value-of select="@Path"  />
                                                    <br/>
                                                  </xsl:if>
                                                </xsl:for-each>
                                              </div>
                                            </div>
                                          </div>
                                        </tr>

                                        <!-- ROW: Directories included (Disabled)... -->
                                        <tr>
                                          <div class="SectionHeader" style="margin-top:5px;">
                                            <img class="floatL" id="{@Name}DirsIncludedDisabledimg" onclick="changeimage('{@Name}DirsIncludedDisabled')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                            <div class="floatL" >
                                              Directories included but disabled
                                            </div>
                                            <br/>
                                            <div style="display:block;">
                                              <div class="float Text" id="{@Name}DirsIncludedDisabled" style="display:{$styleDirsIncludedDisabled};margin-top:5px;margin-left:15px;" >
                                                <xsl:for-each select="Directories/Include/Directory">
                                                  <xsl:sort select="@Path" />
                                                  <xsl:if test="@Enabled = 'False'">
                                                    <xsl:value-of select="@Path"  />
                                                    <br/>
                                                  </xsl:if>
                                                </xsl:for-each>
                                              </div>
                                            </div>
                                          </div>
                                        </tr>

                                        <!-- ROW: Directories excluded (Enabled)... -->
                                        <tr>
                                          <div class="SectionHeader" style="margin-top:5px;">
                                            <img class="floatL" id="{@Name}DirsExcludedEnabledimg" onclick="changeimage('{@Name}DirsExcludedEnabled')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                            <div class="floatL" >
                                              Directories excluded
                                            </div>
                                            <br/>
                                            <div style="display:block;">
                                              <div class="float Text" id="{@Name}DirsExcludedEnabled" style="display:{$styleDirsExcludedEnabled};margin-top:5px;margin-left:15px;" >
                                                <xsl:for-each select="Directories/Exclude/Directory">
                                                  <xsl:sort select="@Path" />
                                                  <xsl:if test="@Enabled = 'True'">
                                                    <xsl:value-of select="@Path"  />
                                                    <br/>
                                                  </xsl:if>
                                                </xsl:for-each>
                                              </div>
                                            </div>
                                          </div>
                                        </tr>

                                        <!-- ROW: Directories excluded (Disabled)... -->
                                        <tr>
                                          <div class="SectionHeader" style="margin-top:5px;">
                                            <img class="floatL" id="{@Name}DirsExcludedDisabledimg" onclick="changeimage('{@Name}DirsExcludedDisabled')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                            <div class="floatL" >
                                              Directories excluded but disabled
                                            </div>
                                            <br/>
                                            <div style="display:block;">
                                              <div class="float Text" id="{@Name}DirsExcludedDisabled" style="display:{$styleDirsExcludedDisabled};margin-top:5px;margin-left:15px;" >
                                                <xsl:for-each select="Directories/Exclude/Directory">
                                                  <xsl:sort select="@Path" />
                                                  <xsl:if test="@Enabled = 'False'">
                                                    <xsl:value-of select="@Path"  />
                                                    <br/>
                                                  </xsl:if>
                                                </xsl:for-each>
                                              </div>
                                            </div>
                                          </div>
                                        </tr>
                                        <br/>



                                        <!-- ROW: Files included (Enabled)... -->
                                        <tr>
                                          <div class="SectionHeader" style="margin-top:5px;">
                                            <img class="floatL" id="{@Name}FilesIncludedEnabledimg" onclick="changeimage('{@Name}FilesIncludedEnabled')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                            <div class="floatL" >
                                              Files included
                                            </div>
                                            <br/>
                                            <div style="display:block;">
                                              <div class="float Text" id="{@Name}FilesIncludedEnabled" style="display:{$styleFilesIncludedEnabled};margin-top:5px;margin-left:15px;" >
                                                <xsl:for-each select="Files/Include/File">
                                                  <xsl:sort select="@Path" />
                                                  <xsl:if test="@Enabled = 'True'">
                                                    <xsl:value-of select="@Path"  />
                                                    <br/>
                                                  </xsl:if>
                                                </xsl:for-each>
                                              </div>
                                            </div>
                                          </div>
                                        </tr>

                                        <!-- ROW: Files included (Disabled)... -->
                                        <tr>
                                          <div class="SectionHeader" style="margin-top:5px;">
                                            <img class="floatL" id="{@Name}FilesIncludedDisabledimg" onclick="changeimage('{@Name}FilesIncludedDisabled')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                            <div class="floatL" >
                                              Files included but disabled
                                            </div>
                                            <br/>
                                            <div style="display:block;">
                                              <div class="float Text" id="{@Name}FilesIncludedDisabled" style="display:{$styleFilesIncludedDisabled};margin-top:5px;margin-left:15px;" >
                                                <xsl:for-each select="Files/Include/File">
                                                  <xsl:sort select="@Path" />
                                                  <xsl:if test="@Enabled = 'False'">
                                                    <xsl:value-of select="@Path"  />
                                                    <br/>
                                                  </xsl:if>
                                                </xsl:for-each>
                                              </div>
                                            </div>
                                          </div>
                                        </tr>

                                        <!-- ROW: Files excluded (Enabled)... -->
                                        <tr>
                                          <div class="SectionHeader" style="margin-top:5px;">
                                            <img class="floatL" id="{@Name}FilesExcludedEnabledimg" onclick="changeimage('{@Name}FilesExcludedEnabled')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                            <div class="floatL" >
                                              Files excluded
                                            </div>
                                            <br/>
                                            <div style="display:block;">
                                              <div class="float Text" id="{@Name}FilesExcludedEnabled" style="display:{$styleFilesExcludedEnabled};margin-top:5px;margin-left:15px;" >
                                                <xsl:for-each select="Files/Exclude/File">
                                                  <xsl:sort select="@Path" />
                                                  <xsl:if test="@Enabled = 'True'">
                                                    <xsl:value-of select="@Path"  />
                                                    <br/>
                                                  </xsl:if>
                                                </xsl:for-each>
                                              </div>
                                            </div>
                                          </div>
                                        </tr>

                                        <!-- ROW: Files excluded (Disabled)... -->
                                        <tr>
                                          <div class="SectionHeader" style="margin-top:5px;">
                                            <img class="floatL" id="{@Name}FilesExcludedDisabledimg" onclick="changeimage('{@Name}FilesExcludedDisabled')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                            <div class="floatL" >
                                              Files excluded but disabled
                                            </div>
                                            <br/>
                                            <div style="display:block;">
                                              <div class="float Text" id="{@Name}FilesExcludedDisabled" style="display:{$styleFilesExcludedDisabled};margin-top:5px;margin-left:15px;" >
                                                <xsl:for-each select="Files/Exclude/File">
                                                  <xsl:sort select="@Path" />
                                                  <xsl:if test="@Enabled = 'False'">
                                                    <xsl:value-of select="@Path"  />
                                                    <br/>
                                                  </xsl:if>
                                                </xsl:for-each>
                                              </div>
                                            </div>
                                          </div>
                                        </tr>
                                        <br/>
                                      </table>


                                    </div>
                                  </div>
                                </div>
                              </tr>
                              <br/>



                              <!-- ROW: Categories and Rules -->
                              <tr>
                                <div class="SectionHeader">
                                  <img class="floatL" id="{@Name}Categoriesimg" onclick="changeimage('{@Name}Categories')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>
                                  <div class="floatL" >
                                    Categories
                                  </div>
                                  <br/>
                                  <div style="display:block;">
                                    <div class="float Text" id="{@Name}Categories" style="display:{$styleCategories};margin-top:5px;margin-left:15px;" >
                                      
                                      <xsl:variable name="projectName" select="@Name" />

                                      <table class="" align="center" border="0" width="100%" cellpadding="0" cellspacing="0" >

                                        <xsl:for-each select="CategoryDefinitions/CategoryDefinition">

                                          <xsl:variable name="isCategoryEnabled" select="@Enabled" />

                                          <!-- ROW: Each category... -->
                                          <tr>
                                            <div class="SectionHeader" style="margin-top:5px;">
                                              <img class="floatL" id="{$projectName}{@RefName}Categoryimg" onclick="changeimage('{$projectName}{@RefName}Category')" src="{$src}" width="9" height="9" style="margin-top:2px;margin-right:5px;"/>

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
                                              <br/>
                                              <div style="display:block;">
                                                <xsl:if test="$isCategoryEnabled = 'True'">
                                                  <div class="float Text" id="{$projectName}{@RefName}Category" style="display:{$styleCategory};margin-top:5px;margin-left:15px;" >
                                                    <xsl:for-each select="RuleDefinition">
                                                      <xsl:if test="@Enabled = 'True'">
                                                        <xsl:value-of select="@RefName"  />
                                                        <br/>
                                                      </xsl:if>
                                                      <xsl:if test="@Enabled = 'False'">
                                                        <xsl:value-of select="@RefName"  />
                                                        <br/>
                                                      </xsl:if>
                                                    </xsl:for-each>                                         
                                                  </div>
                                                </xsl:if>
                                              </div>
                                            </div>
                                          </tr>
                                        
                                        </xsl:for-each>
                                      </table>


                                    </div>
                                  </div>
                                </div>
                              </tr>
                              <br/>
                            </table>

                          </div>

                        </div>
                      </xsl:if>
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
