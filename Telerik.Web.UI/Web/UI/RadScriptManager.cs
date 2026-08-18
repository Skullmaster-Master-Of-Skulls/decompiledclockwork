using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000E80 RID: 3712
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ToolboxData("<{0}:RadScriptManager Runat=server></{0}:RadScriptManager>")]
	[Designer("Telerik.Web.Design.RadScriptManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadStyleSheetManager), "Telerik.Web.UI.ScriptManager.png")]
	[TelerikToolboxCategory("Performance Optimization")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class RadScriptManager : ScriptManager
	{
		// Token: 0x17002C6F RID: 11375
		// (get) Token: 0x06008CA7 RID: 36007 RVA: 0x001FEA7C File Offset: 0x001FCC7C
		private RadScriptManager.ScriptRefProcessor ScriptReferenceProcessor
		{
			get
			{
				if (this._scriptReferenceProcessor == null)
				{
					this._scriptReferenceProcessor = new RadScriptManager.ScriptRefProcessor(this, this._scriptEntryUrlBuilder);
				}
				return this._scriptReferenceProcessor;
			}
		}

		// Token: 0x17002C70 RID: 11376
		// (get) Token: 0x06008CA8 RID: 36008 RVA: 0x001FEA9E File Offset: 0x001FCC9E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<ScriptReferenceGroup> Groups
		{
			get
			{
				return this._groups;
			}
		}

		// Token: 0x06008CA9 RID: 36009 RVA: 0x001FEAA8 File Offset: 0x001FCCA8
		private void AddGroupedSriptReferencesToScriptCollection()
		{
			foreach (ScriptReferenceGroup scriptReferenceGroup in this.Groups)
			{
				foreach (ScriptReference item in scriptReferenceGroup.Scripts)
				{
					base.Scripts.Add(item);
				}
			}
		}

		// Token: 0x17002C71 RID: 11377
		// (get) Token: 0x06008CAA RID: 36010 RVA: 0x001FEB38 File Offset: 0x001FCD38
		protected string HiddenFieldName
		{
			get
			{
				return this.ClientID + "_TSM";
			}
		}

		// Token: 0x17002C72 RID: 11378
		// (get) Token: 0x06008CAB RID: 36011 RVA: 0x001FEB4A File Offset: 0x001FCD4A
		private string FrameworkAssemblyName
		{
			get
			{
				return this.AjaxFrameworkAssembly.FullName;
			}
		}

		// Token: 0x06008CAC RID: 36012 RVA: 0x001FEB57 File Offset: 0x001FCD57
		public RadScriptManager()
		{
			this._cdnSettings = this.CreateCdnSettings();
			this._cacheSettings = this.CreateCacheSettings();
			if (this.IsInCodeEditor)
			{
				this.EnableJavaScriptIntelliSense();
			}
		}

		// Token: 0x06008CAD RID: 36013 RVA: 0x001FEB90 File Offset: 0x001FCD90
		public static bool IsCombinedScriptEnabled(Page page)
		{
			RadScriptManager radScriptManager = ScriptManager.GetCurrent(page) as RadScriptManager;
			return radScriptManager != null && radScriptManager.CdnSettings.CombinedResourceResloved == CombinedResourceMode.Enabled;
		}

		// Token: 0x06008CAE RID: 36014 RVA: 0x001FEBBC File Offset: 0x001FCDBC
		protected virtual CdnSettings CreateCdnSettings()
		{
			return new CdnSettings("CdnSettings", this.ViewState);
		}

		// Token: 0x06008CAF RID: 36015 RVA: 0x001FEBCE File Offset: 0x001FCDCE
		protected virtual CacheSettings CreateCacheSettings()
		{
			return new CacheSettings("CacheSettings", this.ViewState);
		}

		// Token: 0x17002C73 RID: 11379
		// (get) Token: 0x06008CB0 RID: 36016 RVA: 0x001FEBE0 File Offset: 0x001FCDE0
		private bool IsInCodeEditor
		{
			get
			{
				return !base.DesignMode && HttpContext.Current == null;
			}
		}

		// Token: 0x06008CB1 RID: 36017 RVA: 0x001FEC02 File Offset: 0x001FCE02
		private bool AreScriptReferencesSame(ScriptReference ref1, ScriptReference ref2)
		{
			return string.Equals(ref1.Assembly, ref2.Assembly) && string.Equals(ref1.Name, ref2.Name);
		}

		// Token: 0x06008CB2 RID: 36018 RVA: 0x001FEC2C File Offset: 0x001FCE2C
		private bool IsScriptReferenceRegistered(ScriptReference reference)
		{
			foreach (ScriptReference @ref in base.Scripts)
			{
				if (this.AreScriptReferencesSame(@ref, reference))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008CB3 RID: 36019 RVA: 0x001FEC84 File Offset: 0x001FCE84
		private void EnableJavaScriptIntelliSense()
		{
			string assembly = "Telerik.Web.UI";
			string[] array = new string[]
			{
				"Telerik.Web.UI.Common.Core.js",
				"Telerik.Web.UI.Common.jQuery.js",
				"Telerik.Web.UI.Common.jQueryInclude.js"
			};
			foreach (string name in array)
			{
				ScriptReference scriptReference = new ScriptReference
				{
					Assembly = assembly,
					Name = name
				};
				if (!this.IsScriptReferenceRegistered(scriptReference))
				{
					base.Scripts.Add(scriptReference);
				}
			}
		}

		// Token: 0x06008CB4 RID: 36020 RVA: 0x001FED08 File Offset: 0x001FCF08
		private string GetBaseUrl()
		{
			string arg = string.Format(CultureInfo.InvariantCulture, "{0}?{1}={2}&{3}={4}", new object[]
			{
				VirtualPathUtility.ToAbsolute(this.HttpHandlerUrl),
				"_TSM_HiddenField_",
				this.HiddenFieldName,
				"compress",
				this.OutputCompression.ToString("d")
			});
			string text = this.IsScriptCacheEnabled() ? string.Format("{0}={1}", "pk", this.GetPageKey()) : string.Empty;
			string text2 = string.Format("{0}={1}", "_TSM_CombinedScripts_", HttpUtility.UrlEncode(";"));
			if (string.IsNullOrEmpty(text))
			{
				return string.Format("{0}&{1}", arg, text2);
			}
			return string.Format("{0}&{1}&{2}", arg, text, text2);
		}

		// Token: 0x06008CB5 RID: 36021 RVA: 0x001FEDD0 File Offset: 0x001FCFD0
		private bool IsScriptCacheEnabled()
		{
			string value = WebConfigurationManager.AppSettings["telerikEnableScriptCache"];
			if (string.IsNullOrEmpty(value))
			{
				return this._cacheSettings.Enabled;
			}
			return bool.Parse(value);
		}

		// Token: 0x06008CB6 RID: 36022 RVA: 0x001FEE08 File Offset: 0x001FD008
		private string GetPageKey()
		{
			string pageKey = this._cacheSettings.PageKey;
			if (!string.IsNullOrEmpty(pageKey))
			{
				return pageKey;
			}
			if (this.Page == null)
			{
				return string.Empty;
			}
			return this.Page.AppRelativeVirtualPath;
		}

		// Token: 0x06008CB7 RID: 36023 RVA: 0x001FEE44 File Offset: 0x001FD044
		protected override void OnLoad(EventArgs e)
		{
			string hiddenFieldName = this.HiddenFieldName;
			string text = "";
			if (!base.IsInAsyncPostBack || this.Page.Request.Form[hiddenFieldName] == null)
			{
				if (this.Page != null && this.Page.Form != null)
				{
					ScriptManager.RegisterHiddenField(this.Page, hiddenFieldName, text);
				}
			}
			else
			{
				text = this.Page.Request.Form[hiddenFieldName];
			}
			this._scriptEntryUrlBuilder = new ScriptEntryUrlBuilder(this.GetBaseUrl(), base.GetType().ToString());
			foreach (ScriptEntry scriptEntry in ScriptEntry.Deserialize(text))
			{
				this._scriptEntryUrlBuilder.RegisterDisabledScriptEntry(scriptEntry);
			}
			base.OnLoad(e);
			string script = string.Format("window.__TsmHiddenField = $get('{0}');", this.HiddenFieldName);
			ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "hfKeyRegistration", script, true);
		}

		// Token: 0x17002C74 RID: 11380
		// (get) Token: 0x06008CB8 RID: 36024 RVA: 0x001FEF58 File Offset: 0x001FD158
		private IList<ScriptReference> ReferencesFromPartialViews
		{
			get
			{
				if (this.Context.Items["ReferencesFromPartialViews"] == null)
				{
					this.Context.Items["ReferencesFromPartialViews"] = new List<ScriptReference>();
				}
				return (IList<ScriptReference>)this.Context.Items["ReferencesFromPartialViews"];
			}
		}

		// Token: 0x17002C75 RID: 11381
		// (get) Token: 0x06008CB9 RID: 36025 RVA: 0x001FEFB8 File Offset: 0x001FD1B8
		private IScriptReferenceResolver TelerikCdn
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

		// Token: 0x06008CBA RID: 36026 RVA: 0x001FF018 File Offset: 0x001FD218
		protected override void OnResolveScriptReference(ScriptReferenceEventArgs e)
		{
			base.OnResolveScriptReference(e);
			if (this.IsInPartialView)
			{
				this.ReferencesFromPartialViews.Add(e.Script);
				return;
			}
			if (this.CdnSettings.TelerikCdnResolved == TelerikCdnMode.Enabled)
			{
				this.TelerikCdn.ResolveScriptReference(e.Script);
			}
			if (!this.EnableScriptCombine)
			{
				return;
			}
			ScriptReference script = e.Script;
			if (base.IsInAsyncPostBack)
			{
				if (script.Name == "MicrosoftAjax.js" || script.Name == "MicrosoftAjaxWebForms.js")
				{
					return;
				}
				if (base.Scripts.Contains(e.Script))
				{
					return;
				}
			}
			if (script.Name == "jquery")
			{
				return;
			}
			try
			{
				string empty = string.Empty;
				if (string.IsNullOrEmpty(script.Assembly))
				{
					ExternalScriptHelper.ResolveSecurePath(script.Path);
				}
			}
			catch
			{
				return;
			}
			if (!this.ShouldCombine(script))
			{
				this._scriptEntryUrlBuilder.StartNewSlot();
			}
			else
			{
				this.ScriptReferenceProcessor.ProcessGroupedScriptReference(script);
				string name = script.Name;
				ScriptEntry scriptEntryFromScriptReference = this.GetScriptEntryFromScriptReference(script);
				if (this._scriptEntryUrlBuilder.IsScriptEntryRegistered(scriptEntryFromScriptReference))
				{
					if (!scriptEntryFromScriptReference.HasInitialPath)
					{
						this._scriptEntryUrlBuilder.RegisterDisabledScriptEntry(scriptEntryFromScriptReference);
					}
				}
				else
				{
					this._scriptEntryUrlBuilder.RegisterScriptEntry(scriptEntryFromScriptReference);
				}
				if (base.AjaxFrameworkMode == AjaxFrameworkMode.Disabled)
				{
					return;
				}
				if (!this.OutputCompositeScriptLast && base.CompositeScript.Scripts.Count != 0 && name == "MicrosoftAjaxWebForms.js")
				{
					this._scriptEntryUrlBuilder.StartNewSlot();
				}
				return;
			}
		}

		// Token: 0x06008CBB RID: 36027 RVA: 0x001FF19C File Offset: 0x001FD39C
		private ScriptEntry GetScriptEntryFromScriptReference(ScriptReference scriptReference)
		{
			if (string.IsNullOrEmpty(scriptReference.Assembly))
			{
				return new ExternalScriptEntry(scriptReference);
			}
			return new ScriptEntry(scriptReference);
		}

		// Token: 0x06008CBC RID: 36028 RVA: 0x001FF1B8 File Offset: 0x001FD3B8
		private bool ShouldCombine(ScriptReference scriptReference)
		{
			RadScriptReference radScriptReference = scriptReference as RadScriptReference;
			return radScriptReference == null || radScriptReference.Combine;
		}

		// Token: 0x17002C76 RID: 11382
		// (get) Token: 0x06008CBD RID: 36029 RVA: 0x001FF1D7 File Offset: 0x001FD3D7
		private bool IsInPartialView
		{
			get
			{
				return this.Page.ToString().EndsWith("ViewUserControlContainerPage") && this.Context.Items["RadScriptManagerRendered"] == null;
			}
		}

		// Token: 0x06008CBE RID: 36030 RVA: 0x001FF20A File Offset: 0x001FD40A
		protected override void OnInit(EventArgs e)
		{
			this.Page.PreRenderComplete += this.Page_PreRenderComplete;
			this.EnsureResourceMappings();
			base.OnInit(e);
		}

		// Token: 0x06008CBF RID: 36031 RVA: 0x001FF230 File Offset: 0x001FD430
		protected void EnsureResourceMappings()
		{
			if (ScriptManager.ScriptResourceMapping.GetDefinition("jquery") == null && this.EnableEmbeddedjQuery)
			{
				ScriptResourceDefinition definition = new ScriptResourceDefinition
				{
					ResourceName = "Telerik.Web.UI.Common.jQuery.js",
					ResourceAssembly = Assembly.GetAssembly(typeof(RadWebControl))
				};
				ScriptManager.ScriptResourceMapping.AddDefinition("jquery", definition);
			}
		}

		// Token: 0x06008CC0 RID: 36032 RVA: 0x001FF28E File Offset: 0x001FD48E
		private void Page_PreRenderComplete(object sender, EventArgs e)
		{
			this.EnsureAjaxFrameworkScripts();
			if (this.EnableEmbeddedjQuery)
			{
				this.EnsureSinglejQuery();
			}
		}

		// Token: 0x06008CC1 RID: 36033 RVA: 0x001FF2A4 File Offset: 0x001FD4A4
		private void EnsureSinglejQuery()
		{
			ScriptReference item = null;
			foreach (ScriptReference scriptReference in base.Scripts)
			{
				if (scriptReference.Name == "jquery")
				{
					item = scriptReference;
					break;
				}
			}
			base.Scripts.Remove(item);
		}

		// Token: 0x06008CC2 RID: 36034 RVA: 0x001FF310 File Offset: 0x001FD510
		private void EnsureAjaxFrameworkScripts()
		{
			if (base.AjaxFrameworkMode == AjaxFrameworkMode.Disabled || base.AjaxFrameworkMode == AjaxFrameworkMode.Explicit || base.EnableCdn)
			{
				return;
			}
			if (!base.IsInAsyncPostBack && base.LoadScriptsBeforeUI)
			{
				bool flag = false;
				bool flag2 = false;
				foreach (ScriptReference scriptReference in base.Scripts)
				{
					string name = scriptReference.Name;
					if (!flag && (name == "MicrosoftAjax.js" || name == "MicrosoftAjax.debug.js"))
					{
						flag = true;
					}
					if (!flag2 && (name == "MicrosoftAjaxWebForms.js" || name == "MicrosoftAjaxWebForms.debug.js"))
					{
						flag2 = true;
					}
					if (flag && flag2)
					{
						break;
					}
				}
				if (!flag)
				{
					base.Scripts.Add(new ScriptReference("MicrosoftAjax.js", this.FrameworkAssemblyName));
				}
				if (base.SupportsPartialRendering && !flag2)
				{
					base.Scripts.Add(new ScriptReference("MicrosoftAjaxWebForms.js", this.FrameworkAssemblyName));
				}
			}
		}

		// Token: 0x06008CC3 RID: 36035 RVA: 0x001FF420 File Offset: 0x001FD620
		protected virtual void EnsureCdnCombinedResources()
		{
			if (this.CdnSettings.CombinedResourceResloved == CombinedResourceMode.Enabled)
			{
				ScriptReference item = new ScriptReference(this.TelerikCdn.ResoveScriptUri("CombinedScript.js").AbsoluteUri);
				base.Scripts.Add(item);
			}
		}

		// Token: 0x06008CC4 RID: 36036 RVA: 0x001FF464 File Offset: 0x001FD664
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.EnableHandlerDetection && !WebResource.Exists(this.Context, this.HttpHandlerUrl, this.Page.Request.ApplicationPath))
			{
				throw new InvalidOperationException(string.Format("'{0}' is missing in web.config. RadScriptManager requires a HttpHandler registration in web.config. Please, use the control Smart Tag to add the handler automatically, or see the help for more information: Controls > RadScriptManager", this.HttpHandlerUrl));
			}
			this.EnsureCdnCombinedResources();
			this.AddGroupedSriptReferencesToScriptCollection();
			this.SortScriptReferenceByOutputPosition();
		}

		// Token: 0x06008CC5 RID: 36037 RVA: 0x001FF4CC File Offset: 0x001FD6CC
		private void SortScriptReferenceByOutputPosition()
		{
			Stack<ScriptReference> stack = new Stack<ScriptReference>();
			List<ScriptReference> list = new List<ScriptReference>();
			using (IEnumerator<ScriptReference> enumerator = base.Scripts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ScriptReference scriptReference = enumerator.Current;
					RadScriptReference radScriptReference = scriptReference as RadScriptReference;
					if (radScriptReference != null)
					{
						if (radScriptReference.OutputPosition == ScriptReferenceOutputPosition.Beginning)
						{
							stack.Push(radScriptReference);
						}
						else if (radScriptReference.OutputPosition == ScriptReferenceOutputPosition.End)
						{
							list.Add(scriptReference);
						}
					}
				}
				goto IL_89;
			}
			IL_65:
			ScriptReference item = stack.Pop();
			base.Scripts.Remove(item);
			base.Scripts.Insert(0, item);
			IL_89:
			if (stack.Count <= 0)
			{
				foreach (ScriptReference item2 in list)
				{
					base.Scripts.Remove(item2);
					base.Scripts.Add(item2);
				}
				return;
			}
			goto IL_65;
		}

		// Token: 0x06008CC6 RID: 36038 RVA: 0x001FF5DC File Offset: 0x001FD7DC
		internal string SerializeScriptManagerProperties()
		{
			object[] array = new object[10];
			int num = 0;
			array[num++] = this.ViewState["EnableHandlerDetection"];
			array[num++] = this.ViewState["EnableScriptCombine"];
			array[num++] = this.ViewState["HandlerUrl"];
			array[num++] = this.ViewState["OutputCompression"];
			array[num++] = base.EnableCdn;
			array[num++] = (int)base.ScriptMode;
			if (this.CdnSettings.ViewState["TelerikCdnMode"] != null)
			{
				array[num++] = (int)this.CdnSettings.ViewState["TelerikCdnMode"];
			}
			else
			{
				array[num++] = null;
			}
			array[num++] = this.CdnSettings.ViewState["BaseUrl"];
			array[num++] = this.CdnSettings.ViewState["BaseSecureUrl"];
			array[num++] = this.CdnSettings.ViewState["CombinedResource"];
			bool flag = Array.Find<object>(array, (object p) => p != null) != null;
			if (flag)
			{
				return BaseClass.SerializeToString(array);
			}
			return "";
		}

		// Token: 0x06008CC7 RID: 36039 RVA: 0x001FF748 File Offset: 0x001FD948
		internal void DeserializeScriptManagerProperties(string serializedProperties)
		{
			int num = -1;
			string[] array = BaseClass.DeserializeFromString(serializedProperties);
			if (array != null && array.Length == 10)
			{
				if (!string.IsNullOrEmpty(array[++num]))
				{
					this.EnableHandlerDetection = bool.Parse(array[num]);
				}
				if (!string.IsNullOrEmpty(array[++num]))
				{
					this.EnableScriptCombine = bool.Parse(array[num]);
				}
				if (!string.IsNullOrEmpty(array[++num]))
				{
					this.HttpHandlerUrl = array[num];
				}
				if (!string.IsNullOrEmpty(array[++num]))
				{
					this.OutputCompression = (OutputCompression)Enum.Parse(typeof(OutputCompression), array[num]);
				}
				if (!string.IsNullOrEmpty(array[++num]))
				{
					base.EnableCdn = bool.Parse(array[num]);
				}
				if (!string.IsNullOrEmpty(array[++num]))
				{
					base.ScriptMode = (ScriptMode)Enum.Parse(typeof(ScriptMode), array[num]);
				}
				if (!string.IsNullOrEmpty(array[++num]))
				{
					this.CdnSettings.TelerikCdn = (TelerikCdnMode)Enum.Parse(typeof(TelerikCdnMode), array[num]);
				}
				if (!string.IsNullOrEmpty(array[++num]))
				{
					this.CdnSettings.BaseUrl = array[num];
				}
				if (!string.IsNullOrEmpty(array[++num]))
				{
					this.CdnSettings.BaseSecureUrl = array[num];
				}
				if (!string.IsNullOrEmpty(array[++num]))
				{
					this.CdnSettings.CombinedResource = (CombinedResourceMode)Enum.Parse(typeof(CombinedResourceMode), array[num]);
				}
			}
		}

		// Token: 0x06008CC8 RID: 36040 RVA: 0x001FF8C8 File Offset: 0x001FDAC8
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.Page.Form != null)
			{
				base.Render(writer);
				return;
			}
			if (this.IsInPartialView)
			{
				this.SerializeScriptsForScriptControls(this.Page, this.InitStatements);
				return;
			}
			foreach (ScriptReference script in this.ReferencesFromPartialViews)
			{
				this.OnResolveScriptReference(new ScriptReferenceEventArgs(script));
			}
			this.RenderScriptReferences(writer);
			this.RenderApplicationInitStatement(writer);
			this.RenderScriptObjects(writer);
			if (!this.IsInPartialView)
			{
				this.Context.Items["RadScriptManagerRendered"] = true;
			}
		}

		// Token: 0x17002C77 RID: 11383
		// (get) Token: 0x06008CC9 RID: 36041 RVA: 0x001FF984 File Offset: 0x001FDB84
		private StringBuilder InitStatements
		{
			get
			{
				if (this.Context.Items["RadScriptManagerInitStatements"] == null)
				{
					this.Context.Items["RadScriptManagerInitStatements"] = new StringBuilder();
				}
				return (StringBuilder)this.Context.Items["RadScriptManagerInitStatements"];
			}
		}

		// Token: 0x06008CCA RID: 36042 RVA: 0x001FF9DC File Offset: 0x001FDBDC
		private void RenderScriptObjects(HtmlTextWriter writer)
		{
			this.SerializeScriptsForScriptControls(this.Page, this.InitStatements);
			string text = this.InitStatements.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				writer.WriteLine(string.Format("<script type=\"text/javascript\">\r\n//<![CDATA[ \r\n    {0}\r\n//]]>\r\n</script>", string.Format("Sys.Application.add_init(function(){{{0}}});", text)));
			}
		}

		// Token: 0x06008CCB RID: 36043 RVA: 0x001FFA2C File Offset: 0x001FDC2C
		private void RenderApplicationInitStatement(HtmlTextWriter writer)
		{
			if (this.Context.Items["AppInitialize"] == null)
			{
				this.Context.Items["AppInitialize"] = true;
				writer.WriteLine(string.Format("<script type=\"text/javascript\">\r\n//<![CDATA[ \r\n    {0}\r\n//]]>\r\n</script>", "if(typeof(Sys) != \"undefined\"){$addHandler(window, \"load\", function(){Sys.Application.initialize();}); } else { throw new Error(\"Microsoft ASP.NET AJAX cannot be initialized!\")}"));
			}
		}

		// Token: 0x06008CCC RID: 36044 RVA: 0x001FFA80 File Offset: 0x001FDC80
		public static bool IsEncodingInAcceptList(string acceptEncodingHeader, string expectedEncoding)
		{
			if (!string.IsNullOrEmpty(acceptEncodingHeader))
			{
				foreach (string text in acceptEncodingHeader.Split(new char[]
				{
					','
				}))
				{
					if (string.Equals(text.Trim(), expectedEncoding, StringComparison.Ordinal))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06008CCD RID: 36045 RVA: 0x001FFAD6 File Offset: 0x001FDCD6
		private void RenderScriptReferences(HtmlTextWriter writer)
		{
			if (this.EnableScriptCombine)
			{
				this.RegisterCombinedScripts(writer);
				return;
			}
			this.RegisterUnCombinedScripts(writer);
		}

		// Token: 0x06008CCE RID: 36046 RVA: 0x001FFAF0 File Offset: 0x001FDCF0
		private void RegisterUnCombinedScripts(HtmlTextWriter writer)
		{
			foreach (RegisteredScript registeredScript in base.GetRegisteredClientScriptBlocks())
			{
				string url = registeredScript.Url;
				if (this.Context.Items[url] == null)
				{
					this.Context.Items[url] = true;
					if (registeredScript.ScriptType == RegisteredScriptType.ClientScriptInclude)
					{
						writer.WriteLine(string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", HttpUtility.HtmlEncode(url)));
					}
				}
			}
		}

		// Token: 0x06008CCF RID: 36047 RVA: 0x001FFB88 File Offset: 0x001FDD88
		private void RegisterCombinedScripts(HtmlTextWriter writer)
		{
			foreach (ScriptReference scriptReference in base.Scripts)
			{
				string text = base.ResolveUrl(scriptReference.Path);
				if (this.Context.Items[text] == null)
				{
					this.Context.Items[text] = true;
					writer.WriteLine(string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", HttpUtility.HtmlEncode(text)));
				}
			}
		}

		// Token: 0x06008CD0 RID: 36048 RVA: 0x001FFC1C File Offset: 0x001FDE1C
		internal void SerializeScriptsForScriptControls(Control control, StringBuilder builder)
		{
			IScriptControl scriptControl = control as IScriptControl;
			if (scriptControl != null && control.Visible)
			{
				IEnumerable<ScriptDescriptor> scriptDescriptors = scriptControl.GetScriptDescriptors();
				if (scriptDescriptors != null)
				{
					foreach (ScriptDescriptor scriptDescriptor in scriptDescriptors)
					{
						RadControlScriptDescriptor radControlScriptDescriptor = scriptDescriptor as RadControlScriptDescriptor;
						if (radControlScriptDescriptor != null)
						{
							builder.Append(radControlScriptDescriptor.Script);
						}
					}
				}
			}
			if (control.HasControls())
			{
				foreach (object obj in control.Controls)
				{
					Control control2 = (Control)obj;
					this.SerializeScriptsForScriptControls(control2, builder);
				}
			}
		}

		// Token: 0x17002C78 RID: 11384
		// (get) Token: 0x06008CD1 RID: 36049 RVA: 0x001FFCF0 File Offset: 0x001FDEF0
		// (set) Token: 0x06008CD2 RID: 36050 RVA: 0x001FFD1B File Offset: 0x001FDF1B
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating if RadScriptManager should check the Telerik.Web.UI.WebResource handler existence in the application configuration file.")]
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

		// Token: 0x17002C79 RID: 11385
		// (get) Token: 0x06008CD3 RID: 36051 RVA: 0x001FFD33 File Offset: 0x001FDF33
		// (set) Token: 0x06008CD4 RID: 36052 RVA: 0x001FFD5E File Offset: 0x001FDF5E
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Specifies whether or not multiple script references should be combined into a single file")]
		public bool EnableScriptCombine
		{
			get
			{
				return this.ViewState["EnableScriptCombine"] == null || (bool)this.ViewState["EnableScriptCombine"];
			}
			set
			{
				this.ViewState["EnableScriptCombine"] = value;
			}
		}

		// Token: 0x17002C7A RID: 11386
		// (get) Token: 0x06008CD5 RID: 36053 RVA: 0x001FFD76 File Offset: 0x001FDF76
		// (set) Token: 0x06008CD6 RID: 36054 RVA: 0x001FFDA1 File Offset: 0x001FDFA1
		[Description("Specifies whether or not the combined output will be compressed.")]
		[DefaultValue(OutputCompression.AutoDetect)]
		[Category("Behavior")]
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

		// Token: 0x17002C7B RID: 11387
		// (get) Token: 0x06008CD7 RID: 36055 RVA: 0x001FFDD2 File Offset: 0x001FDFD2
		// (set) Token: 0x06008CD8 RID: 36056 RVA: 0x001FFDF2 File Offset: 0x001FDFF2
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

		// Token: 0x17002C7C RID: 11388
		// (get) Token: 0x06008CD9 RID: 36057 RVA: 0x001FFE13 File Offset: 0x001FE013
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Description("CDN settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		public CdnSettings CdnSettings
		{
			get
			{
				return this._cdnSettings;
			}
		}

		// Token: 0x17002C7D RID: 11389
		// (get) Token: 0x06008CDA RID: 36058 RVA: 0x001FFE1B File Offset: 0x001FE01B
		[Description("Gets the assembly white list collection as returned from the provider")]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AssemblyWhiteListCollection AssemblyWhiteList
		{
			get
			{
				if (this._whiteList == null)
				{
					this._whiteList = new AssemblyWhiteListCollection(null);
				}
				return this._whiteList;
			}
		}

		// Token: 0x17002C7E RID: 11390
		// (get) Token: 0x06008CDB RID: 36059 RVA: 0x001FFE37 File Offset: 0x001FE037
		[Description("Cache settings")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CacheSettings CacheSettings
		{
			get
			{
				return this._cacheSettings;
			}
		}

		// Token: 0x17002C7F RID: 11391
		// (get) Token: 0x06008CDC RID: 36060 RVA: 0x001FFE3F File Offset: 0x001FE03F
		// (set) Token: 0x06008CDD RID: 36061 RVA: 0x001FFE6A File Offset: 0x001FE06A
		[Category("Behavior")]
		[Description("Specifies whether the CompositeScript (if defined) should render last or on its default place")]
		[DefaultValue(false)]
		public bool OutputCompositeScriptLast
		{
			get
			{
				return this.ViewState["OutputCompositeScriptLast"] != null && (bool)this.ViewState["OutputCompositeScriptLast"];
			}
			set
			{
				this.ViewState["OutputCompositeScriptLast"] = value;
			}
		}

		// Token: 0x17002C80 RID: 11392
		// (get) Token: 0x06008CDE RID: 36062 RVA: 0x001FFE82 File Offset: 0x001FE082
		// (set) Token: 0x06008CDF RID: 36063 RVA: 0x001FFEB6 File Offset: 0x001FE0B6
		[DefaultValue(true)]
		[Description("Specifies whether the embedded jQuery library is output with RadControls' scripts.")]
		[Category("Behavior")]
		public bool EnableEmbeddedjQuery
		{
			get
			{
				if (this.ViewState["EnableEmbeddedjQuery"] == null)
				{
					return ScriptManagerConfigurationSettings.GetConfiguration().EnableEmbeddedjQuery;
				}
				return (bool)this.ViewState["EnableEmbeddedjQuery"];
			}
			set
			{
				this.ViewState["EnableEmbeddedjQuery"] = value;
			}
		}

		// Token: 0x04002780 RID: 10112
		private const string JavaScriptBlockFormat = "<script type=\"text/javascript\">\r\n//<![CDATA[ \r\n    {0}\r\n//]]>\r\n</script>";

		// Token: 0x04002781 RID: 10113
		private const string JavaScriptFileIncludeFormat = "<script type=\"text/javascript\" src=\"{0}\"></script>";

		// Token: 0x04002782 RID: 10114
		private const string ApplicationInitStatement = "if(typeof(Sys) != \"undefined\"){$addHandler(window, \"load\", function(){Sys.Application.initialize();}); } else { throw new Error(\"Microsoft ASP.NET AJAX cannot be initialized!\")}";

		// Token: 0x04002783 RID: 10115
		private const string AddInitStatementFormat = "Sys.Application.add_init(function(){{{0}}});";

		// Token: 0x04002784 RID: 10116
		private RadScriptManager.ScriptRefProcessor _scriptReferenceProcessor;

		// Token: 0x04002785 RID: 10117
		private List<ScriptReferenceGroup> _groups = new List<ScriptReferenceGroup>();

		// Token: 0x04002786 RID: 10118
		private ScriptEntryUrlBuilder _scriptEntryUrlBuilder;

		// Token: 0x04002787 RID: 10119
		private CdnSettings _cdnSettings;

		// Token: 0x04002788 RID: 10120
		private CacheSettings _cacheSettings;

		// Token: 0x04002789 RID: 10121
		private AssemblyWhiteListCollection _whiteList;

		// Token: 0x0400278A RID: 10122
		private IScriptReferenceResolver _telerikCdn;

		// Token: 0x02000E81 RID: 3713
		private class ScriptRefProcessor
		{
			// Token: 0x06008CE2 RID: 36066 RVA: 0x001FFECE File Offset: 0x001FE0CE
			public ScriptRefProcessor(RadScriptManager scriptManager, ScriptEntryUrlBuilder urlBuilder)
			{
				this._scriptManager = scriptManager;
				this._urlBuilder = urlBuilder;
			}

			// Token: 0x06008CE3 RID: 36067 RVA: 0x001FFEE4 File Offset: 0x001FE0E4
			private ScriptReferenceGroup FindContainingGroup(ScriptReference scriptReference)
			{
				foreach (ScriptReferenceGroup scriptReferenceGroup in this._scriptManager.Groups)
				{
					foreach (ScriptReference scriptReference2 in scriptReferenceGroup.Scripts)
					{
						if (scriptReference2.Equals(scriptReference))
						{
							return scriptReferenceGroup;
						}
					}
				}
				return null;
			}

			// Token: 0x06008CE4 RID: 36068 RVA: 0x001FFF80 File Offset: 0x001FE180
			public void ProcessGroupedScriptReference(ScriptReference scriptReference)
			{
				ScriptReferenceGroup scriptReferenceGroup = this.FindContainingGroup(scriptReference);
				if (scriptReferenceGroup != null)
				{
					if (this._notGroupedScriptReferencesAdded && scriptReferenceGroup != this._lastScriptGroup)
					{
						this._urlBuilder.StartNewSlot();
						this._lastScriptGroup = scriptReferenceGroup;
						return;
					}
				}
				else
				{
					if (this._lastScriptGroup != null)
					{
						this._urlBuilder.StartNewSlot();
						this._lastScriptGroup = null;
						return;
					}
					this._notGroupedScriptReferencesAdded = true;
				}
			}

			// Token: 0x0400278C RID: 10124
			private bool _notGroupedScriptReferencesAdded;

			// Token: 0x0400278D RID: 10125
			private ScriptReferenceGroup _lastScriptGroup;

			// Token: 0x0400278E RID: 10126
			private RadScriptManager _scriptManager;

			// Token: 0x0400278F RID: 10127
			private ScriptEntryUrlBuilder _urlBuilder;
		}
	}
}
