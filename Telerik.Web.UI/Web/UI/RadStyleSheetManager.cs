using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02001ACA RID: 6858
	[ToolboxBitmap(typeof(RadStyleSheetManager), "Telerik.Web.UI.StyleSheetManager.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Performance Optimization")]
	[ToolboxData("<{0}:RadStyleSheetManager Runat=server></{0}:RadStyleSheetManager>")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[Designer("Telerik.Web.Design.RadStyleSheetManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadStyleSheetManager : Control
	{
		// Token: 0x170050AF RID: 20655
		// (get) Token: 0x0601098D RID: 67981 RVA: 0x003B38CD File Offset: 0x003B1ACD
		private ScriptManager ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					this._scriptManager = ScriptRegistrar.GetScriptManager(this);
				}
				return this._scriptManager;
			}
		}

		// Token: 0x170050B0 RID: 20656
		// (get) Token: 0x0601098E RID: 67982 RVA: 0x003B38E9 File Offset: 0x003B1AE9
		private string HiddenFieldID
		{
			get
			{
				return this.ClientID + "_TSSM";
			}
		}

		// Token: 0x170050B1 RID: 20657
		// (get) Token: 0x0601098F RID: 67983 RVA: 0x003B3904 File Offset: 0x003B1B04
		internal IStyleSheetReferenceResolver TelerikCdn
		{
			get
			{
				if (this._telerikCdn == null)
				{
					HttpRequestInfo request = new HttpRequestInfo(HttpContext.Current.Request);
					this._telerikCdn = new TelerikCdnService(this.CdnSettings, request);
					((TelerikCdnService)this._telerikCdn).GetOutputCompression = (() => this.OutputCompression);
				}
				return this._telerikCdn;
			}
		}

		// Token: 0x170050B2 RID: 20658
		// (get) Token: 0x06010990 RID: 67984 RVA: 0x003B3964 File Offset: 0x003B1B64
		private StyleSheetReferenceCollection WebUIStyleSheetBuffer
		{
			get
			{
				if (this._webUIStyleSheetBuffer == null)
				{
					this._webUIStyleSheetBuffer = new StyleSheetReferenceCollection();
				}
				return this._webUIStyleSheetBuffer;
			}
		}

		// Token: 0x170050B3 RID: 20659
		// (get) Token: 0x06010991 RID: 67985 RVA: 0x003B397F File Offset: 0x003B1B7F
		private StyleSheetReferenceCollection WebUISkinsStyleSheetBuffer
		{
			get
			{
				if (this._webUISkinsStyleSheetBuffer == null)
				{
					this._webUISkinsStyleSheetBuffer = new StyleSheetReferenceCollection();
				}
				return this._webUISkinsStyleSheetBuffer;
			}
		}

		// Token: 0x170050B4 RID: 20660
		// (get) Token: 0x06010992 RID: 67986 RVA: 0x003B399A File Offset: 0x003B1B9A
		private string AppDataPath
		{
			get
			{
				return Path.Combine(this.Context.Request.PhysicalApplicationPath, "App_Data");
			}
		}

		// Token: 0x170050B5 RID: 20661
		// (get) Token: 0x06010993 RID: 67987 RVA: 0x003B39B6 File Offset: 0x003B1BB6
		private string SplitStyleSheetsFolderPath
		{
			get
			{
				return Path.Combine(this.AppDataPath, this.SplitStyleSheetsFolder);
			}
		}

		// Token: 0x170050B6 RID: 20662
		// (get) Token: 0x06010994 RID: 67988 RVA: 0x003B39C9 File Offset: 0x003B1BC9
		internal IHmacEnabledService EncryptionService
		{
			get
			{
				return this._cryptoService;
			}
		}

		// Token: 0x170050B7 RID: 20663
		// (get) Token: 0x06010995 RID: 67989 RVA: 0x003B39D4 File Offset: 0x003B1BD4
		// (set) Token: 0x06010996 RID: 67990 RVA: 0x003B3A49 File Offset: 0x003B1C49
		internal bool IsIE
		{
			get
			{
				if (this._isIE != null)
				{
					return this._isIE.Value;
				}
				bool result = false;
				if (this.Context.Request.UserAgent != null)
				{
					result = (this.Context.Request.UserAgent.Contains("Trident/5.0") || this.Context.Request.UserAgent.Contains("Trident/4.0"));
				}
				return result;
			}
			set
			{
				this._isIE = new bool?(value);
			}
		}

		// Token: 0x06010997 RID: 67991 RVA: 0x003B3A57 File Offset: 0x003B1C57
		public RadStyleSheetManager()
		{
			this._cdnSettings = this.CreateCdnSettings();
		}

		// Token: 0x06010998 RID: 67992 RVA: 0x003B3A92 File Offset: 0x003B1C92
		protected virtual StyleSheetCdnSettings CreateCdnSettings()
		{
			return new StyleSheetCdnSettings("CdnSettings", this.ViewState);
		}

		// Token: 0x06010999 RID: 67993 RVA: 0x003B3AA4 File Offset: 0x003B1CA4
		private string GetBaseUrl()
		{
			ClientScriptManager clientScript = this.Page.ClientScript;
			string webResourceUrl = clientScript.GetWebResourceUrl(typeof(string), "custom");
			string[] array = webResourceUrl.Split(new char[]
			{
				'?'
			});
			return string.Format(CultureInfo.InvariantCulture, "{0}?{1}&{2}={3}&{4}={5}", new object[]
			{
				VirtualPathUtility.ToAbsolute(this.HttpHandlerUrl),
				array[1],
				"compress",
				this.OutputCompression.ToString("d"),
				"_TSM_CombinedScripts_",
				HttpUtility.UrlEncode(";")
			});
		}

		// Token: 0x0601099A RID: 67994 RVA: 0x003B3B54 File Offset: 0x003B1D54
		public void RegisterSkinnableControl(ISkinnableControl control)
		{
			bool flag = this.CdnSettings.TelerikCdnResolved == TelerikCdnMode.Enabled;
			Type type = control.GetType();
			if (!this.EnableStyleSheetCombine || flag)
			{
				SkinRegistrar.RegisterCssReferences(control);
				return;
			}
			if (control.ResolvedRenderMode == RenderMode.Native)
			{
				return;
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			foreach (RequiredCssAttribute requiredCssAttribute in SkinRegistrar.GetRequiredCssAttributes(control, type))
			{
				this.RegisterStyleSheetReference(new StyleSheetReference(requiredCssAttribute.CssResourceName, requiredCssAttribute.Type.Assembly.FullName));
			}
			foreach (EmbeddedSkinAttribute embeddedSkinAttribute in SkinRegistrar.GetEmbeddedSkinAttributes(control, type))
			{
				embeddedSkinAttribute.Suffix = control.GetSkinSuffix();
				this.RegisterStyleSheetReference(new StyleSheetReference(embeddedSkinAttribute.CssResourceName, embeddedSkinAttribute.Type.Assembly.FullName));
			}
			RadSkinManager current = RadSkinManager.GetCurrent(this.Page);
			if (current != null)
			{
				foreach (object obj in current.CustomNonEmbeddedSkins)
				{
					CustomNonEmbeddedSkin customNonEmbeddedSkin = (CustomNonEmbeddedSkin)obj;
					this.StyleSheets.Add(new StyleSheetReference
					{
						Path = customNonEmbeddedSkin.Url
					});
				}
			}
		}

		// Token: 0x0601099B RID: 67995 RVA: 0x003B3CEC File Offset: 0x003B1EEC
		protected void RegisterStyleSheetReference(StyleSheetReference styleSheet)
		{
			if (styleSheet.Assembly.StartsWith("Telerik.Web.UI.Skins"))
			{
				this.WebUISkinsStyleSheetBuffer.Add(styleSheet);
				return;
			}
			if (styleSheet.Assembly.StartsWith("Telerik.Web.UI"))
			{
				this.WebUIStyleSheetBuffer.Add(styleSheet);
				return;
			}
			this.StyleSheets.Add(styleSheet);
		}

		// Token: 0x0601099C RID: 67996 RVA: 0x003B3D43 File Offset: 0x003B1F43
		public static RadStyleSheetManager GetCurrent(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			return page.Items[typeof(RadStyleSheetManager)] as RadStyleSheetManager;
		}

		// Token: 0x0601099D RID: 67997 RVA: 0x003B3D70 File Offset: 0x003B1F70
		public static bool IsCombinedBaseSkinEnabled(Page page)
		{
			RadStyleSheetManager current = RadStyleSheetManager.GetCurrent(page);
			return current != null && current.CdnSettings.CombinedResourceResloved == CombinedResourceMode.Enabled;
		}

		// Token: 0x0601099E RID: 67998 RVA: 0x003B3D98 File Offset: 0x003B1F98
		protected override void OnInit(EventArgs e)
		{
			if (this.SupportsRenderingMode)
			{
				this.InitializeRenderMode();
			}
			this.Page.PreRenderComplete += this.Page_PreRenderComplete;
			if (!base.DesignMode)
			{
				if (RadStyleSheetManager.GetCurrent(this.Page) != null)
				{
					throw new InvalidOperationException("There must be only one instance of RadStyleSheetManager per page.");
				}
				this.Page.Items[typeof(RadStyleSheetManager)] = this;
			}
		}

		// Token: 0x0601099F RID: 67999 RVA: 0x003B3E08 File Offset: 0x003B2008
		protected override void OnLoad(EventArgs e)
		{
			string text = string.Empty;
			ScriptManager current = ScriptManager.GetCurrent(this.Page);
			if (current != null)
			{
				if (!current.IsInAsyncPostBack || this.Page.Request.Form[this.HiddenFieldID] == null)
				{
					ScriptManager.RegisterHiddenField(this.Page, this.HiddenFieldID, text);
				}
				else
				{
					text = this.Page.Request.Form[this.HiddenFieldID];
					if (ScriptManagerConfigurationSettings.GetConfiguration().EnableHandlerEncryption)
					{
						text = this.EncryptionService.Decrypt(text);
					}
				}
			}
			this._scriptEntryUrlBuilder = new ScriptEntryUrlBuilder(this.GetBaseUrl(), base.GetType().ToString(), this.IsIE);
			foreach (ScriptEntry scriptEntry in ScriptEntry.Deserialize(text))
			{
				this._scriptEntryUrlBuilder.RegisterDisabledScriptEntry(scriptEntry);
			}
			base.OnLoad(e);
		}

		// Token: 0x060109A0 RID: 68000 RVA: 0x003B3F0C File Offset: 0x003B210C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.EnableHandlerDetection && !WebResource.Exists(this.Context, this.HttpHandlerUrl, this.Page.Request.ApplicationPath))
			{
				throw new InvalidOperationException(string.Format("'{0}' is missing in web.config. RadStyleSheetManager requires a HttpHandler registration in web.config. Please, use the control Smart Tag to add the handler automatically, or see the help for more information: Controls > RadStyleSheetManager", this.HttpHandlerUrl));
			}
		}

		// Token: 0x060109A1 RID: 68001 RVA: 0x003B3F64 File Offset: 0x003B2164
		private void RegisterStyleSheet(StyleSheetReference styleSheet)
		{
			ScriptEntry scriptEntry = styleSheet.GetScriptEntry();
			if (this._scriptEntryUrlBuilder.IsScriptEntryRegistered(scriptEntry))
			{
				this._scriptEntryUrlBuilder.RegisterDisabledScriptEntry(scriptEntry);
				return;
			}
			if (this.EnableStyleSheetCombine)
			{
				scriptEntry.EnableSelectorLimitCheck = this.EnableSelectorLimitCheck;
				this._scriptEntryUrlBuilder.RegisterScriptEntry(scriptEntry);
				return;
			}
			this._scriptEntryUrlBuilder.RegisterScriptEntryToSeparateSlot(scriptEntry);
		}

		// Token: 0x060109A2 RID: 68002 RVA: 0x003B3FC0 File Offset: 0x003B21C0
		private bool GetIsValidScriptEntry(ScriptEntry scriptEntry)
		{
			Assembly assembly = scriptEntry.LoadAssembly();
			string[] manifestResourceNames = assembly.GetManifestResourceNames();
			foreach (string value in manifestResourceNames)
			{
				if (scriptEntry.Name.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060109A3 RID: 68003 RVA: 0x003B400C File Offset: 0x003B220C
		protected virtual void EnsureCdnCombinedResources()
		{
			if (this.CdnSettings.CombinedResourceResloved == CombinedResourceMode.Enabled)
			{
				string resourceUri = string.Format("CombinedBaseSkin{0}.css", this.GetSkinSuffix());
				int index = -1;
				if (this.Page.Header != null)
				{
					for (int i = 0; i < this.Page.Header.Controls.Count; i++)
					{
						if (this.Page.Header.Controls[i] is HtmlLink)
						{
							HtmlLink htmlLink = (HtmlLink)this.Page.Header.Controls[i];
							string text = htmlLink.Attributes["class"];
							if (text != null && text.Contains("Telerik_stylesheet"))
							{
								index = i;
								break;
							}
						}
					}
				}
				SkinRegistrar.RegisterCssReference(this.Page, this, this.TelerikCdn.ResoveSkinUri(resourceUri).AbsoluteUri, index);
			}
		}

		// Token: 0x060109A4 RID: 68004 RVA: 0x003B40EB File Offset: 0x003B22EB
		private string GetSkinSuffix()
		{
			return RenderModeHelper.GetRenderingModeString(this.ResolvedRenderMode);
		}

		// Token: 0x060109A5 RID: 68005 RVA: 0x003B4100 File Offset: 0x003B2300
		private void RegisterValidStyleSheets(StyleSheetReferenceCollection styleSheets)
		{
			if (this.EnableStyleSheetCombine && !string.IsNullOrEmpty(this.SplitStyleSheetsFolder) && this.IsIE && this.StyleSheetsForSplitting.Count > 0)
			{
				this.CheckAppDataFolderForSplitStyles(this.StyleSheetsForSplitting);
				this.CreateNewStyleSheetReferences(this.StyleSheetsForSplitting);
			}
			else if (this.StyleSheetsForSplitting.Count > 0)
			{
				foreach (StyleSheetReference item in this.StyleSheetsForSplitting)
				{
					styleSheets.Add(item);
				}
			}
			foreach (StyleSheetReference styleSheetReference in from styleSheet in styleSheets
			orderby styleSheet.OrderIndex
			select styleSheet)
			{
				ScriptEntry scriptEntry = styleSheetReference.GetScriptEntry();
				if (scriptEntry is ExternalStyleSheetEntry || this.GetIsValidScriptEntry(scriptEntry))
				{
					this.RegisterStyleSheet(styleSheetReference);
				}
			}
		}

		// Token: 0x060109A6 RID: 68006 RVA: 0x003B4220 File Offset: 0x003B2420
		private void CheckAppDataFolderForSplitStyles(StyleSheetReferenceCollection styleSheets)
		{
			this.EnsureSplitStyleSheetsFolderExists();
			this.SplitCurrentStyleSheets(styleSheets);
		}

		// Token: 0x060109A7 RID: 68007 RVA: 0x003B4240 File Offset: 0x003B2440
		private void CreateNewStyleSheetReferences(StyleSheetReferenceCollection styleSheets)
		{
			StyleSheetReferenceCollection styleSheetReferenceCollection = new StyleSheetReferenceCollection();
			DirectoryInfo directoryInfo = new DirectoryInfo(this.SplitStyleSheetsFolderPath);
			FileInfo[] array = (from p in directoryInfo.GetFiles()
			orderby p.CreationTime
			select p).ToArray<FileInfo>();
			foreach (FileInfo fileInfo in array)
			{
				styleSheetReferenceCollection.Add(new StyleSheetReference
				{
					Path = "~/App_Data/" + this.SplitStyleSheetsFolder + "/" + fileInfo.Name
				});
			}
			styleSheets = styleSheetReferenceCollection;
			foreach (StyleSheetReference styleSheetReference in from styleSheet in styleSheets
			orderby styleSheet.OrderIndex
			select styleSheet)
			{
				ScriptEntry scriptEntry = styleSheetReference.GetScriptEntry();
				scriptEntry.EnableSelectorLimitCheck = false;
				scriptEntry.LoadSeparately = true;
				if (scriptEntry is ExternalStyleSheetEntry || this.GetIsValidScriptEntry(scriptEntry))
				{
					this.RegisterStyleSheet(styleSheetReference);
				}
			}
		}

		// Token: 0x060109A8 RID: 68008 RVA: 0x003B437C File Offset: 0x003B257C
		private void SplitCurrentStyleSheets(StyleSheetReferenceCollection styleSheets)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(this.SplitStyleSheetsFolderPath);
			FileInfo[] files = directoryInfo.GetFiles();
			if (HttpContext.Current.Application["SplitFolderApplicationKey" + this.SplitStyleSheetsFolder] == null)
			{
				foreach (FileInfo fileInfo in files)
				{
					fileInfo.Delete();
				}
			}
			if (files.Length == 0)
			{
				string str = Guid.NewGuid().ToString() + ".css";
				string text = this.SplitStyleSheetsFolderPath + "\\" + str;
				foreach (StyleSheetReference styleSheetReference in from styleSheet in styleSheets
				orderby styleSheet.OrderIndex
				select styleSheet)
				{
					ScriptEntry scriptEntry = styleSheetReference.GetScriptEntry();
					string value = this.GetStyleSheetCommentRegex().Replace(scriptEntry.GetScript(), string.Empty);
					if (!File.Exists(text))
					{
						FileStream fileStream = File.Create(text);
						fileStream.Close();
					}
					using (StreamWriter streamWriter = File.AppendText(text))
					{
						streamWriter.WriteLine(value);
					}
				}
				this.SplitStylesIntoSeparateFiles(text);
			}
			HttpContext.Current.Application["SplitFolderApplicationKey" + this.SplitStyleSheetsFolder] = "SplitFolderApplicationKey";
		}

		// Token: 0x060109A9 RID: 68009 RVA: 0x003B4508 File Offset: 0x003B2708
		private void SplitStylesIntoSeparateFiles(string combinedFilePath)
		{
			string text = File.ReadAllText(combinedFilePath);
			int num = 0;
			int num2 = 0;
			Match match = this.GetStyleSheetSelectorRegex().Match(text);
			while (match.Success)
			{
				num++;
				if (num == this.SplitThreshold)
				{
					string text2 = text.Substring(num2, match.Index - num2);
					int num3 = text2.LastIndexOf("}");
					string content = text2.Substring(0, num3 + 1);
					num2 += num3 + 1;
					this.CreateShtyleSheetFile(content);
					num = 0;
					match = this.GetStyleSheetSelectorRegex().Match(text, num2);
				}
				else
				{
					match = match.NextMatch();
				}
				if (!match.Success && num != 0)
				{
					string content2 = text.Substring(num2 + 1, text.Length - num2 - 1);
					this.CreateShtyleSheetFile(content2);
					break;
				}
			}
			File.Delete(combinedFilePath);
		}

		// Token: 0x060109AA RID: 68010 RVA: 0x003B45D8 File Offset: 0x003B27D8
		private void CreateShtyleSheetFile(string content)
		{
			string str = Guid.NewGuid().ToString() + ".css";
			string path = this.SplitStyleSheetsFolderPath + "\\" + str;
			FileStream fileStream = File.Create(path);
			fileStream.Close();
			using (StreamWriter streamWriter = File.AppendText(path))
			{
				streamWriter.WriteLine(content);
			}
		}

		// Token: 0x060109AB RID: 68011 RVA: 0x003B4650 File Offset: 0x003B2850
		protected internal virtual void EnsureSplitStyleSheetsFolderExists()
		{
			if (!Directory.Exists(this.AppDataPath))
			{
				this.CreateAppDataFolder();
			}
			if (!Directory.Exists(this.SplitStyleSheetsFolderPath))
			{
				this.CreateSplitFolder();
			}
		}

		// Token: 0x060109AC RID: 68012 RVA: 0x003B4678 File Offset: 0x003B2878
		private void CreateSplitFolder()
		{
			try
			{
				Directory.CreateDirectory(this.SplitStyleSheetsFolderPath);
			}
			catch (UnauthorizedAccessException)
			{
				throw new UnauthorizedAccessException(string.Format("RadStyleSheetManager could not create App_Data\\{0} folder. Ensure the App_Data folder is writable or set the {0} property to a writable location.", this.SplitStyleSheetsFolder));
			}
		}

		// Token: 0x060109AD RID: 68013 RVA: 0x003B46BC File Offset: 0x003B28BC
		private void CreateAppDataFolder()
		{
			try
			{
				Directory.CreateDirectory(this.AppDataPath);
			}
			catch (UnauthorizedAccessException)
			{
				throw new UnauthorizedAccessException("RadStyleSheetManager could not create App_Data folder. Ensure the App_Data's location is writable.");
			}
		}

		// Token: 0x060109AE RID: 68014 RVA: 0x003B46F4 File Offset: 0x003B28F4
		private Regex GetStyleSheetCommentRegex()
		{
			if (this._styleSheetCommentRegex == null)
			{
				this._styleSheetCommentRegex = new Regex("\\/\\*.*?(\\*\\/|$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}
			return this._styleSheetCommentRegex;
		}

		// Token: 0x060109AF RID: 68015 RVA: 0x003B4716 File Offset: 0x003B2916
		private Regex GetStyleSheetSelectorRegex()
		{
			if (this._styleSheetSelectorRegex == null)
			{
				this._styleSheetSelectorRegex = new Regex("[\\{\\,]", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			}
			return this._styleSheetSelectorRegex;
		}

		// Token: 0x060109B0 RID: 68016 RVA: 0x003B4738 File Offset: 0x003B2938
		internal int GetSelectorCount(ScriptEntry styleSheetEntry)
		{
			string script = styleSheetEntry.GetScript();
			string input = this.GetStyleSheetCommentRegex().Replace(script, string.Empty);
			int num = 0;
			Match match = this.GetStyleSheetSelectorRegex().Match(input);
			while (match.Success)
			{
				num++;
				match = match.NextMatch();
			}
			return num;
		}

		// Token: 0x060109B1 RID: 68017 RVA: 0x003B4784 File Offset: 0x003B2984
		private void AddCompactedRadControlStyleSheets()
		{
			foreach (StyleSheetReference item in this.WebUIStyleSheetBuffer)
			{
				this.StyleSheets.Add(item);
			}
			foreach (StyleSheetReference item2 in this.WebUISkinsStyleSheetBuffer)
			{
				this.StyleSheets.Add(item2);
			}
		}

		// Token: 0x060109B2 RID: 68018 RVA: 0x003B4818 File Offset: 0x003B2A18
		private void Page_PreRenderComplete(object sender, EventArgs e)
		{
			RadStyleSheetManager.AllowFolderLookup = true;
			this.AddCompactedRadControlStyleSheets();
			this.EnsureCdnCombinedResources();
			this.RegisterValidStyleSheets(this.StyleSheets);
			List<string> urls = this._scriptEntryUrlBuilder.GetUrls();
			ScriptManager current = ScriptManager.GetCurrent(this.Page);
			if (current != null && current.IsInAsyncPostBack && urls.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder("[");
				foreach (string arg in urls)
				{
					stringBuilder.AppendFormat("'{0}', ", arg);
				}
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
				stringBuilder.Append("]");
				string script = string.Format(CultureInfo.InvariantCulture, "(function() {{\r\n                        function loadHandler() {{\r\n                            var hrefs = {0};\r\n                            var head = document.getElementsByTagName('head')[0];\r\n                            for (var i = 0; i < hrefs.length; i++)\r\n                                if ('createStyleSheet' in document) {{ \r\n                                    try {{ \r\n                                        document.createStyleSheet(hrefs[i]); \r\n                                    }} catch(e) {{\r\n                                        if (e.number == -2147024882 && e.message.indexOf('Not enough storage is available to complete this operation.') == 0)\r\n                                            alert('Error: The maximum number of style sheets on the page (31) has been reached. The browser will not load these excessive style sheets. Please consider combining external style sheets to reduce the total style sheet count.');\r\n                                    }};\r\n                                }}\r\n                                else {{\r\n                                    var link = document.createElement('link');\r\n                                    link.setAttribute('type', 'text/css');\r\n                                    link.setAttribute('rel', 'stylesheet');\r\n                                    link.setAttribute('href', hrefs[i]);\r\n                                    head.appendChild(link);\r\n                                }}\r\n                            Sys.Application.remove_load(loadHandler);\r\n                        }}\r\n                        Sys.Application.add_load(loadHandler);\r\n                    }})();", new object[]
				{
					stringBuilder.ToString()
				});
				ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "RegisterStyleSheets", script, true);
			}
			else if (this.Page.Header != null)
			{
				foreach (string href in urls)
				{
					HtmlLink htmlLink = new HtmlLink();
					htmlLink.Href = href;
					htmlLink.Attributes.Add("type", "text/css");
					htmlLink.Attributes.Add("rel", "stylesheet");
					this.Page.Header.Controls.Add(htmlLink);
				}
			}
			this.RegisterUpdateHiddenFieldWithLoadedStyleSheetsScript();
		}

		// Token: 0x060109B3 RID: 68019 RVA: 0x003B49D0 File Offset: 0x003B2BD0
		private void RegisterUpdateHiddenFieldWithLoadedStyleSheetsScript()
		{
			this._scriptEntryUrlBuilder.UpdateBaseUrl(string.Empty);
			List<string> urls = this._scriptEntryUrlBuilder.GetUrls();
			if (urls.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string s in urls)
				{
					stringBuilder.Append(HttpContext.Current.Server.UrlDecode(s));
				}
				string text = stringBuilder.ToString();
				if (ScriptManagerConfigurationSettings.GetConfiguration().EnableHandlerEncryption)
				{
					text = this.EncryptionService.Encrypt(text);
				}
				string script = string.Format(CultureInfo.InvariantCulture, ";(function() {{\r\n                        function loadHandler() {{\r\n                            var hf = $get('{0}');\r\n                            if (!hf._RSSM_init) {{ hf._RSSM_init = true; hf.value = ''; }}\r\n                            hf.value += '{1}';\r\n                            Sys.Application.remove_load(loadHandler);\r\n                        }};\r\n                        Sys.Application.add_load(loadHandler);\r\n                    }})();", new object[]
				{
					this.ClientID + "_TSSM",
					text
				});
				ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "RegisterLoadedStyleSheets", script, true);
			}
			this._scriptEntryUrlBuilder.UpdateBaseUrl(this.GetBaseUrl());
		}

		// Token: 0x060109B4 RID: 68020 RVA: 0x003B4AE0 File Offset: 0x003B2CE0
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				return;
			}
			if (this.Page.Header == null && this._scriptEntryUrlBuilder != null)
			{
				foreach (string value in this._scriptEntryUrlBuilder.GetUrls())
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
					writer.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
					writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
					writer.RenderBeginTag(HtmlTextWriterTag.Link);
					writer.RenderEndTag();
				}
			}
		}

		// Token: 0x060109B5 RID: 68021 RVA: 0x003B4B8C File Offset: 0x003B2D8C
		internal string SerializeStyleSheetManagerProperties()
		{
			object[] array = new object[8];
			array[0] = this.ViewState["EnableHandlerDetection"];
			array[1] = this.ViewState["EnableStyleSheetCombine"];
			array[2] = this.ViewState["HandlerUrl"];
			array[3] = this.ViewState["OutputCompression"];
			if (this.CdnSettings.ViewState["TelerikCdnMode"] != null)
			{
				array[4] = (int)this.CdnSettings.ViewState["TelerikCdnMode"];
			}
			else
			{
				array[4] = null;
			}
			array[5] = this.CdnSettings.ViewState["BaseUrl"];
			array[6] = this.CdnSettings.ViewState["BaseSecureUrl"];
			array[7] = this.CdnSettings.ViewState["CombinedResource"];
			bool flag = Array.Find<object>(array, (object p) => p != null) != null;
			if (flag)
			{
				return BaseClass.SerializeToString(array);
			}
			return "";
		}

		// Token: 0x060109B6 RID: 68022 RVA: 0x003B4CAC File Offset: 0x003B2EAC
		internal void DeserializeStyleSheetManagerProperties(string serializedProperties)
		{
			string[] array = BaseClass.DeserializeFromString(serializedProperties);
			if (array != null && array.Length == 8)
			{
				if (!string.IsNullOrEmpty(array[0]))
				{
					this.EnableHandlerDetection = bool.Parse(array[0]);
				}
				if (!string.IsNullOrEmpty(array[1]))
				{
					this.EnableStyleSheetCombine = bool.Parse(array[1]);
				}
				if (!string.IsNullOrEmpty(array[2]))
				{
					this.HttpHandlerUrl = array[2];
				}
				if (!string.IsNullOrEmpty(array[3]))
				{
					this.OutputCompression = (OutputCompression)Enum.Parse(typeof(OutputCompression), array[3]);
				}
				if (!string.IsNullOrEmpty(array[4]))
				{
					this.CdnSettings.TelerikCdn = (TelerikCdnMode)Enum.Parse(typeof(TelerikCdnMode), array[4]);
				}
				if (!string.IsNullOrEmpty(array[5]))
				{
					this.CdnSettings.BaseUrl = array[5];
				}
				if (!string.IsNullOrEmpty(array[6]))
				{
					this.CdnSettings.BaseSecureUrl = array[6];
				}
				if (!string.IsNullOrEmpty(array[7]))
				{
					this.CdnSettings.CombinedResource = (CombinedResourceMode)Enum.Parse(typeof(CombinedResourceMode), array[7]);
				}
			}
		}

		// Token: 0x170050B8 RID: 20664
		// (get) Token: 0x060109B7 RID: 68023 RVA: 0x003B4DC1 File Offset: 0x003B2FC1
		// (set) Token: 0x060109B8 RID: 68024 RVA: 0x003B4DE2 File Offset: 0x003B2FE2
		[Category("Appearance")]
		[DefaultValue(RenderMode.Classic)]
		[NotifyParentProperty(true)]
		[Description("Specifies the rendering mode of the control")]
		public RenderMode RenderMode
		{
			get
			{
				return (RenderMode)(this.ViewState["RenderMode"] ?? RenderMode.Classic);
			}
			set
			{
				this.ViewState["RenderMode"] = value;
				this._renderModeSet = true;
			}
		}

		// Token: 0x170050B9 RID: 20665
		// (get) Token: 0x060109B9 RID: 68025 RVA: 0x003B4E04 File Offset: 0x003B3004
		// (set) Token: 0x060109BA RID: 68026 RVA: 0x003B4E60 File Offset: 0x003B3060
		[Description("Returns resolved RenderMode should the original value was Auto")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		public RenderMode ResolvedRenderMode
		{
			get
			{
				if (!base.DesignMode)
				{
					if (this.ViewState["ResolvedRenderMode"] == null || this.ViewState.IsItemDirty("RenderMode"))
					{
						this.ResolvedRenderMode = this.ResolveRenderMode();
					}
					return (RenderMode)this.ViewState["ResolvedRenderMode"];
				}
				return RenderMode.Classic;
			}
			private set
			{
				this.ViewState["ResolvedRenderMode"] = value;
			}
		}

		// Token: 0x060109BB RID: 68027 RVA: 0x003B4E78 File Offset: 0x003B3078
		protected RenderMode ResolveRenderMode()
		{
			RenderMode renderMode = this.SupportsRenderingMode ? this.RenderMode : RenderMode.Classic;
			if (renderMode == RenderMode.Classic)
			{
				return renderMode;
			}
			RenderModeBrowserAdaptor instance = RenderModeBrowserAdaptor.Instance;
			if (this.CanRenderInMode(instance, renderMode))
			{
				return renderMode;
			}
			return this.PreferredRenderMode(instance);
		}

		// Token: 0x060109BC RID: 68028 RVA: 0x003B4EB6 File Offset: 0x003B30B6
		protected internal bool CanRenderInMode(RenderModeBrowserAdaptor browser, RenderMode mode)
		{
			if (mode == RenderMode.Native)
			{
				return this.SupportsNativeRendering;
			}
			if (mode == RenderMode.Mobile)
			{
				return this.SupportsAdaptiveRendering;
			}
			return mode == RenderMode.Lightweight && browser.IsModernBrowser && this.SupportsLightweightRendering;
		}

		// Token: 0x060109BD RID: 68029 RVA: 0x003B4EE2 File Offset: 0x003B30E2
		protected internal virtual RenderMode PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			if (this.RenderMode != RenderMode.Auto && !this.CanRenderInMode(browser, RenderMode.Lightweight))
			{
				return RenderMode.Classic;
			}
			if (this.SupportsAdaptiveRendering && browser.IsMobileDevice)
			{
				return RenderMode.Mobile;
			}
			if (this.CanRenderInMode(browser, RenderMode.Lightweight))
			{
				return RenderMode.Lightweight;
			}
			return RenderMode.Classic;
		}

		// Token: 0x170050BA RID: 20666
		// (get) Token: 0x060109BE RID: 68030 RVA: 0x003B4F17 File Offset: 0x003B3117
		protected internal bool SupportsAdaptiveRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170050BB RID: 20667
		// (get) Token: 0x060109BF RID: 68031 RVA: 0x003B4F1A File Offset: 0x003B311A
		protected internal bool SupportsNativeRendering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170050BC RID: 20668
		// (get) Token: 0x060109C0 RID: 68032 RVA: 0x003B4F1D File Offset: 0x003B311D
		protected internal bool SupportsLightweightRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170050BD RID: 20669
		// (get) Token: 0x060109C1 RID: 68033 RVA: 0x003B4F20 File Offset: 0x003B3120
		protected internal bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170050BE RID: 20670
		// (get) Token: 0x060109C2 RID: 68034 RVA: 0x003B4F23 File Offset: 0x003B3123
		protected bool IsRenderModeSet
		{
			get
			{
				return this._renderModeSet;
			}
		}

		// Token: 0x060109C3 RID: 68035 RVA: 0x003B4F2C File Offset: 0x003B312C
		protected internal void InitializeRenderMode()
		{
			if (!this.IsRenderModeSet)
			{
				if (RenderModeConfigurationReader.Instance.HasGlobalKey())
				{
					this.RenderMode = RenderModeConfigurationReader.Instance.GetRenderMode(null);
				}
				if (RenderModeConfigurationReader.Instance.HasKey(base.GetType()))
				{
					this.RenderMode = RenderModeConfigurationReader.Instance.GetRenderMode(base.GetType(), null);
				}
			}
		}

		// Token: 0x170050BF RID: 20671
		// (get) Token: 0x060109C4 RID: 68036 RVA: 0x003B4F87 File Offset: 0x003B3187
		// (set) Token: 0x060109C5 RID: 68037 RVA: 0x003B4FB2 File Offset: 0x003B31B2
		[Description("Gets or sets a value indicating if RadStyleSheetManager should check the Telerik.Web.UI.WebResource handler existence in the application configuration file.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool EnableHandlerDetection
		{
			get
			{
				return this.ViewState["EnableHandlerDetection"] == null || (bool)this.ViewState["EnableHandlerDetection"];
			}
			set
			{
				this.ViewState["EnableHandlerDetection"] = value;
			}
		}

		// Token: 0x170050C0 RID: 20672
		// (get) Token: 0x060109C6 RID: 68038 RVA: 0x003B4FCA File Offset: 0x003B31CA
		// (set) Token: 0x060109C7 RID: 68039 RVA: 0x003B4FF5 File Offset: 0x003B31F5
		[Description("Specifies whether or not multiple stylesheet references should be combined into a single file")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool EnableStyleSheetCombine
		{
			get
			{
				return this.ViewState["EnableStyleSheetCombine"] == null || (bool)this.ViewState["EnableStyleSheetCombine"];
			}
			set
			{
				this.ViewState["EnableStyleSheetCombine"] = value;
			}
		}

		// Token: 0x170050C1 RID: 20673
		// (get) Token: 0x060109C8 RID: 68040 RVA: 0x003B500D File Offset: 0x003B320D
		// (set) Token: 0x060109C9 RID: 68041 RVA: 0x003B5038 File Offset: 0x003B3238
		[Category("Behavior")]
		[Description("Specifies whether or not the combined output will be compressed.")]
		[DefaultValue(OutputCompression.AutoDetect)]
		public OutputCompression OutputCompression
		{
			get
			{
				if (this.ViewState["OutputCompression"] != null)
				{
					return (OutputCompression)this.ViewState["OutputCompression"];
				}
				return OutputCompression.AutoDetect;
			}
			set
			{
				this.ViewState["OutputCompression"] = value;
				if (this._scriptEntryUrlBuilder != null)
				{
					this._scriptEntryUrlBuilder.UpdateBaseUrl(this.GetBaseUrl());
				}
			}
		}

		// Token: 0x170050C2 RID: 20674
		// (get) Token: 0x060109CA RID: 68042 RVA: 0x003B5069 File Offset: 0x003B3269
		// (set) Token: 0x060109CB RID: 68043 RVA: 0x003B5089 File Offset: 0x003B3289
		[Description("Specifies the URL of the HTTPHandler that combines and serves the scripts.")]
		[Category("Advanced")]
		[DefaultValue("~/Telerik.Web.UI.WebResource.axd")]
		public string HttpHandlerUrl
		{
			get
			{
				return ((string)this.ViewState["HandlerUrl"]) ?? "~/Telerik.Web.UI.WebResource.axd";
			}
			set
			{
				if (!VirtualPathUtility.IsAppRelative(value))
				{
					throw WebResource.GetHttpHandlerUrlNotAppRelative();
				}
				this.ViewState["HandlerUrl"] = value;
			}
		}

		// Token: 0x170050C3 RID: 20675
		// (get) Token: 0x060109CC RID: 68044 RVA: 0x003B50AA File Offset: 0x003B32AA
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[Category("Behavior")]
		public StyleSheetReferenceCollection StyleSheets
		{
			get
			{
				if (this._styleSheets == null)
				{
					this._styleSheets = new StyleSheetReferenceCollection();
				}
				return this._styleSheets;
			}
		}

		// Token: 0x170050C4 RID: 20676
		// (get) Token: 0x060109CD RID: 68045 RVA: 0x003B50C5 File Offset: 0x003B32C5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[DefaultValue(null)]
		[Category("Behavior")]
		internal StyleSheetReferenceCollection StyleSheetsForSplitting
		{
			get
			{
				if (this._styleSheetsForSplitting == null)
				{
					this._styleSheetsForSplitting = new StyleSheetReferenceCollection();
				}
				return this._styleSheetsForSplitting;
			}
		}

		// Token: 0x170050C5 RID: 20677
		// (get) Token: 0x060109CE RID: 68046 RVA: 0x003B50E0 File Offset: 0x003B32E0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("CDN settings")]
		public StyleSheetCdnSettings CdnSettings
		{
			get
			{
				return this._cdnSettings;
			}
		}

		// Token: 0x170050C6 RID: 20678
		// (get) Token: 0x060109CF RID: 68047 RVA: 0x003B50E8 File Offset: 0x003B32E8
		// (set) Token: 0x060109D0 RID: 68048 RVA: 0x003B5113 File Offset: 0x003B3313
		[DefaultValue(true)]
		[Description("Specifies whether or not multiple stylesheet references should be combined into a single file")]
		[Category("Behavior")]
		public bool EnableSelectorLimitCheck
		{
			get
			{
				return this.ViewState["EnableSelectorLimitCheck"] == null || (bool)this.ViewState["EnableSelectorLimitCheck"];
			}
			set
			{
				this.ViewState["EnableSelectorLimitCheck"] = value;
			}
		}

		// Token: 0x170050C7 RID: 20679
		// (get) Token: 0x060109D1 RID: 68049 RVA: 0x003B512B File Offset: 0x003B332B
		// (set) Token: 0x060109D2 RID: 68050 RVA: 0x003B514B File Offset: 0x003B334B
		[Description("Specifies where the split CSS should be placed in case the request is under IE9 or IE8")]
		[DefaultValue("")]
		[Category("Behavior")]
		internal string SplitStyleSheetsFolder
		{
			get
			{
				return ((string)this.ViewState["SplitStyleSheetsFolder"]) ?? "";
			}
			set
			{
				this.ViewState["SplitStyleSheetsFolder"] = value;
			}
		}

		// Token: 0x04004A26 RID: 18982
		private const string Custom_Text_For_Encryption = "custom";

		// Token: 0x04004A27 RID: 18983
		private const string AppData = "App_Data";

		// Token: 0x04004A28 RID: 18984
		private const string SplitFolderApplicationKey = "SplitFolderApplicationKey";

		// Token: 0x04004A29 RID: 18985
		private ScriptEntryUrlBuilder _scriptEntryUrlBuilder;

		// Token: 0x04004A2A RID: 18986
		private StyleSheetReferenceCollection _styleSheets;

		// Token: 0x04004A2B RID: 18987
		private StyleSheetReferenceCollection _styleSheetsForSplitting;

		// Token: 0x04004A2C RID: 18988
		private StyleSheetCdnSettings _cdnSettings;

		// Token: 0x04004A2D RID: 18989
		private ScriptManager _scriptManager;

		// Token: 0x04004A2E RID: 18990
		private IStyleSheetReferenceResolver _telerikCdn;

		// Token: 0x04004A2F RID: 18991
		private StyleSheetReferenceCollection _webUIStyleSheetBuffer;

		// Token: 0x04004A30 RID: 18992
		private StyleSheetReferenceCollection _webUISkinsStyleSheetBuffer;

		// Token: 0x04004A31 RID: 18993
		private bool _renderModeSet;

		// Token: 0x04004A32 RID: 18994
		private bool? _isIE = null;

		// Token: 0x04004A33 RID: 18995
		private readonly IHmacEnabledService _cryptoService = HmacEnabledCryptoService.GetService("");

		// Token: 0x04004A34 RID: 18996
		private Regex _styleSheetSelectorRegex;

		// Token: 0x04004A35 RID: 18997
		private Regex _styleSheetCommentRegex;

		// Token: 0x04004A36 RID: 18998
		private int SplitThreshold = 4095;

		// Token: 0x04004A37 RID: 18999
		internal static bool AllowFolderLookup;
	}
}
