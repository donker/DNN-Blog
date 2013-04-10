﻿Imports System.Xml
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Services.Tokens
Imports DotNetNuke.Modules.Blog.Common.Globals

Namespace Common
 Public Class ModuleSettings
  Implements IPropertyAccess

#Region " Private Members "
  Private _allSettings As Hashtable = Nothing
  Private _moduleId As Integer = -1
#End Region

#Region " Properties "
  Public Property AllowWLW As Boolean = False
  Public Property AllowMultipleCategories As Boolean = True
  Public Property VocabularyId As Integer = -1
  Public Property AllowAttachments As Boolean = True
  Public Property SummaryModel As SummaryType = SummaryType.HtmlIndependent
  Public Property LocalizationModel As LocalizationType = LocalizationType.None
  Public Property StyleDetectionUrl As String = ""
  Public Property WLWRecentPostsMax As Integer = 10
  Public Property AutoGeneratedSummaryLength As Integer = 1000
  Public Property BloggersCanEditCategories As Boolean = False
  Public Property AllowAllLocales As Boolean = False
  Public Property ShowAllLocales As Boolean = True

  Public Property RssEmail As String = ""
  Public Property RssDefaultNrItems As Integer = 20
  Public Property RssMaxNrItems As Integer = 50
  Public Property RssTtl As Integer = 30
  Public Property RssImageWidth As Integer = 144
  Public Property RssImageHeight As Integer = 96
  Public Property RssImageSizeAllowOverride As Boolean = True
  Public Property RssAllowContentInFeed As Boolean = True
  Public Property RssDefaultCopyright As String = ""

  Public Property PortalTemplatesPath As String = ""
  Private Property PortalModulePath As String = ""
  Private Property PortalModuleMapPath As String = ""
  Private _portalTemplatesMapPath As String = ""
  Public ReadOnly Property PortalTemplatesMapPath As String
   Get
    Return _portalTemplatesMapPath
   End Get
  End Property
  Public ReadOnly Property ModuleId As Integer
   Get
    Return _moduleId
   End Get
  End Property
#End Region

#Region " Constructors "
  Public Sub New(moduleId As Integer)

   _moduleId = moduleId
   _allSettings = (New DotNetNuke.Entities.Modules.ModuleController).GetModuleSettings(moduleId)
   _allSettings.ReadValue("AllowWLW", AllowWLW)
   _allSettings.ReadValue("AllowMultipleCategories", AllowMultipleCategories)
   _allSettings.ReadValue("VocabularyId", VocabularyId)
   _allSettings.ReadValue("AllowAttachments", AllowAttachments)
   _allSettings.ReadValue("SummaryModel", SummaryModel)
   _allSettings.ReadValue("LocalizationModel", LocalizationModel)
   _allSettings.ReadValue("StyleDetectionUrl", StyleDetectionUrl)
   _allSettings.ReadValue("WLWRecentPostsMax", WLWRecentPostsMax)
   _allSettings.ReadValue("BloggersCanEditCategories", BloggersCanEditCategories)
   _allSettings.ReadValue("AllowAllLocales", AllowAllLocales)
   _allSettings.ReadValue("RssEmail", RssEmail)
   _allSettings.ReadValue("RssDefaultNrItems", RssDefaultNrItems)
   _allSettings.ReadValue("RssMaxNrItems", RssMaxNrItems)
   _allSettings.ReadValue("RssTtl", RssTtl)
   _allSettings.ReadValue("RssImageWidth", RssImageWidth)
   _allSettings.ReadValue("RssImageHeight", RssImageHeight)
   _allSettings.ReadValue("RssImageSizeAllowOverride", RssImageSizeAllowOverride)
   _allSettings.ReadValue("RssAllowContentInFeed", RssAllowContentInFeed)
   _allSettings.ReadValue("RssDefaultCopyright", RssDefaultCopyright)

   _PortalModulePath = DotNetNuke.Entities.Portals.PortalSettings.Current.HomeDirectory
   If Not _PortalModulePath.EndsWith("/") Then
    _PortalModulePath &= "/"
   End If
   _PortalModulePath &= String.Format("Blog/", moduleId)

   _PortalModuleMapPath = DotNetNuke.Entities.Portals.PortalSettings.Current.HomeDirectoryMapPath
   If Not _PortalModuleMapPath.EndsWith("\") Then
    _PortalModuleMapPath &= "\"
   End If
   _PortalModuleMapPath &= String.Format("Blog\", moduleId)

   _portalTemplatesMapPath = String.Format("{0}Templates\", _PortalModuleMapPath)
   If Not IO.Directory.Exists(_portalTemplatesMapPath) Then
    IO.Directory.CreateDirectory(_portalTemplatesMapPath)
   End If
   _PortalTemplatesPath = String.Format("{0}Templates/", _PortalModulePath)

  End Sub

  Public Shared Function GetModuleSettings(moduleId As Integer) As ModuleSettings
   Dim CacheKey As String = "ModuleSettings" & moduleId.ToString
   Dim settings As ModuleSettings = CType(DotNetNuke.Common.Utilities.DataCache.GetCache(CacheKey), ModuleSettings)
   If settings Is Nothing Then
    settings = New ModuleSettings(moduleId)
    DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey, settings)
   End If
   Return settings
  End Function
#End Region

#Region " Public Members "
  Public Overridable Sub UpdateSettings()

   Dim objModules As New DotNetNuke.Entities.Modules.ModuleController
   objModules.UpdateModuleSetting(_moduleId, "AllowWLW", AllowWLW.ToString)
   objModules.UpdateModuleSetting(_moduleId, "AllowMultipleCategories", AllowMultipleCategories.ToString)
   objModules.UpdateModuleSetting(_moduleId, "VocabularyId", VocabularyId.ToString)
   objModules.UpdateModuleSetting(_moduleId, "AllowAttachments", AllowAttachments.ToString)
   objModules.UpdateModuleSetting(_moduleId, "SummaryModel", CInt(SummaryModel).ToString)
   objModules.UpdateModuleSetting(_moduleId, "LocalizationModel", CInt(LocalizationModel).ToString)
   objModules.UpdateModuleSetting(_moduleId, "StyleDetectionUrl", StyleDetectionUrl)
   objModules.UpdateModuleSetting(_moduleId, "WLWRecentPostsMax", WLWRecentPostsMax.ToString)
   objModules.UpdateModuleSetting(_moduleId, "BloggersCanEditCategories", BloggersCanEditCategories.ToString)
   objModules.UpdateModuleSetting(_moduleId, "AllowAllLocales", AllowAllLocales.ToString)
   objModules.UpdateModuleSetting(_moduleId, "RssEmail", RssEmail)
   objModules.UpdateModuleSetting(_moduleId, "RssDefaultNrItems", RssDefaultNrItems.ToString)
   objModules.UpdateModuleSetting(_moduleId, "RssMaxNrItems", RssMaxNrItems.ToString)
   objModules.UpdateModuleSetting(_moduleId, "RssTtl", RssTtl.ToString)
   objModules.UpdateModuleSetting(_moduleId, "RssImageWidth", RssImageWidth.ToString)
   objModules.UpdateModuleSetting(_moduleId, "RssImageHeight", RssImageHeight.ToString)
   objModules.UpdateModuleSetting(_moduleId, "RssImageSizeAllowOverride", RssImageSizeAllowOverride.ToString)
   objModules.UpdateModuleSetting(_moduleId, "RssAllowContentInFeed", RssAllowContentInFeed.ToString)
   objModules.UpdateModuleSetting(_moduleId, "RssDefaultCopyright", RssDefaultCopyright)

   Dim CacheKey As String = "ModuleSettings" & _moduleId.ToString
   DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey, Me)
  End Sub
#End Region

#Region " IPropertyAccess Implementation "
  Public Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As System.Globalization.CultureInfo, AccessingUser As DotNetNuke.Entities.Users.UserInfo, AccessLevel As DotNetNuke.Services.Tokens.Scope, ByRef PropertyNotFound As Boolean) As String Implements DotNetNuke.Services.Tokens.IPropertyAccess.GetProperty
   Dim OutputFormat As String = String.Empty
   Dim portalSettings As DotNetNuke.Entities.Portals.PortalSettings = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings()
   If strFormat = String.Empty Then
    OutputFormat = "D"
   Else
    OutputFormat = strFormat
   End If
   Select Case strPropertyName.ToLower
    Case "email"
     Return PropertyAccess.FormatString(Me.RssEmail, strFormat)
    Case "allowmultiplecategories"
     Return Me.AllowMultipleCategories.ToString(formatProvider)
    Case "allowattachments"
     Return Me.AllowAttachments.ToString(formatProvider)
    Case "allowalllocales"
     Return Me.AllowAllLocales.ToString(formatProvider)
    Case "summarymodel"
     Return CInt(SummaryModel).ToString
    Case "localizationmodel"
     Return CInt(LocalizationModel).ToString

    Case "portaltemplatespath"
     Return PropertyAccess.FormatString(Me.PortalTemplatesPath, strFormat)
    Case "portalmodulepath"
     Return PropertyAccess.FormatString(_PortalModulePath, strFormat)
    Case "apppath"
     Return DotNetNuke.Common.ApplicationPath
    Case "imagehandlerpath"
     Return DotNetNuke.Common.ResolveUrl(glbImageHandlerPath)
    Case Else
     PropertyNotFound = True
   End Select

   Return Null.NullString
  End Function

  Public ReadOnly Property Cacheability() As DotNetNuke.Services.Tokens.CacheLevel Implements DotNetNuke.Services.Tokens.IPropertyAccess.Cacheability
   Get
    Return CacheLevel.fullyCacheable
   End Get
  End Property
#End Region

 End Class
End Namespace
