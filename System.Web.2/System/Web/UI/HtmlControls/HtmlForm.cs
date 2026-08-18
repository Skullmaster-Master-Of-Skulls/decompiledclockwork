using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000362 RID: 866
	public class HtmlForm : HtmlContainerControl
	{
		// Token: 0x06002818 RID: 10264 RVA: 0x00081980 File Offset: 0x0007FB80
		public HtmlForm() : base("form")
		{
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x00081990 File Offset: 0x0007FB90
		// (set) Token: 0x0600281A RID: 10266 RVA: 0x000819B8 File Offset: 0x0007FBB8
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Action
		{
			get
			{
				string text = base.Attributes["action"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["action"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x000819D0 File Offset: 0x0007FBD0
		// (set) Token: 0x0600281C RID: 10268 RVA: 0x000819E6 File Offset: 0x0007FBE6
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public string DefaultButton
		{
			get
			{
				if (this._defaultButton == null)
				{
					return string.Empty;
				}
				return this._defaultButton;
			}
			set
			{
				this._defaultButton = value;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x000819EF File Offset: 0x0007FBEF
		// (set) Token: 0x0600281E RID: 10270 RVA: 0x00081A05 File Offset: 0x0007FC05
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public string DefaultFocus
		{
			get
			{
				if (this._defaultFocus == null)
				{
					return string.Empty;
				}
				return this._defaultFocus;
			}
			set
			{
				this._defaultFocus = value;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x00081A10 File Offset: 0x0007FC10
		// (set) Token: 0x06002820 RID: 10272 RVA: 0x00081A38 File Offset: 0x0007FC38
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Enctype
		{
			get
			{
				string text = base.Attributes["enctype"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["enctype"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x00081A50 File Offset: 0x0007FC50
		// (set) Token: 0x06002822 RID: 10274 RVA: 0x00081A78 File Offset: 0x0007FC78
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Method
		{
			get
			{
				string text = base.Attributes["method"];
				if (text == null)
				{
					return "post";
				}
				return text;
			}
			set
			{
				base.Attributes["method"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x0007F357 File Offset: 0x0007D557
		// (set) Token: 0x06002824 RID: 10276 RVA: 0x00006164 File Offset: 0x00004364
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00081A90 File Offset: 0x0007FC90
		// (set) Token: 0x06002826 RID: 10278 RVA: 0x00081A98 File Offset: 0x0007FC98
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool SubmitDisabledControls
		{
			get
			{
				return this._submitDisabledControls;
			}
			set
			{
				this._submitDisabledControls = value;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x00081AA4 File Offset: 0x0007FCA4
		// (set) Token: 0x06002828 RID: 10280 RVA: 0x0007E2E4 File Offset: 0x0007C4E4
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Target
		{
			get
			{
				string text = base.Attributes["target"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["target"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x00081ACC File Offset: 0x0007FCCC
		public override string UniqueID
		{
			get
			{
				if (this.NamingContainer == this.Page)
				{
					return base.UniqueID;
				}
				if (this.EffectiveClientIDMode != ClientIDMode.AutoID)
				{
					return this.ID ?? "aspnetForm";
				}
				return "aspnetForm";
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x00081B01 File Offset: 0x0007FD01
		public override string ClientID
		{
			get
			{
				if (this.EffectiveClientIDMode != ClientIDMode.AutoID)
				{
					return this.ID;
				}
				return base.ClientID;
			}
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x00081B1C File Offset: 0x0007FD1C
		protected internal override void Render(HtmlTextWriter output)
		{
			Page page = this.Page;
			if (page == null)
			{
				throw new HttpException(SR.GetString("Form_Needs_Page"));
			}
			if (page.SmartNavigation)
			{
				((IAttributeAccessor)this).SetAttribute("__smartNavEnabled", "true");
				StringBuilder stringBuilder = new StringBuilder("<IFRAME id=\"__hifSmartNav\" name=\"__hifSmartNav\" style=\"display:none\" src=\"");
				stringBuilder.Append(HttpEncoderUtility.UrlEncodeSpaces(HttpUtility.HtmlAttributeEncode(this.Page.ClientScript.GetWebResourceUrl(typeof(HtmlForm), "SmartNav.htm"))));
				stringBuilder.Append("\"></IFRAME>");
				output.WriteLine(stringBuilder.ToString());
			}
			base.Render(output);
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x00081BB8 File Offset: 0x0007FDB8
		private string GetActionAttribute()
		{
			string action = this.Action;
			if (!string.IsNullOrEmpty(action))
			{
				return action;
			}
			VirtualPath clientFilePath = this.Context.Request.ClientFilePath;
			string text;
			if (this.Context.ServerExecuteDepth == 0)
			{
				text = clientFilePath.VirtualPathString;
				int num = text.LastIndexOf('/');
				if (num >= 0)
				{
					text = "./" + text.Substring(num + 1);
				}
			}
			else
			{
				VirtualPath virtualPath = this.Context.Request.CurrentExecutionFilePathObject;
				virtualPath = clientFilePath.MakeRelative(virtualPath);
				text = virtualPath.VirtualPathString;
			}
			bool flag = CookielessHelperClass.UseCookieless(this.Context, false, FormsAuthentication.CookieMode);
			if (flag && this.Context.Request != null && this.Context.Response != null)
			{
				text = this.Context.Response.ApplyAppPathModifier(text);
			}
			if (string.IsNullOrEmpty(text) && this.RenderingCompatibility >= VersionUtil.Framework45)
			{
				text = "./";
			}
			string clientQueryString = this.Page.ClientQueryString;
			if (!string.IsNullOrEmpty(clientQueryString))
			{
				text = text + "?" + clientQueryString;
			}
			return text;
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x00081CCB File Offset: 0x0007FECB
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.SetForm(this);
			this.Page.RegisterViewStateHandler();
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x00081CEB File Offset: 0x0007FEEB
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page.SmartNavigation)
			{
				this.Page.ClientScript.RegisterClientScriptResource(typeof(HtmlForm), "SmartNav.js");
			}
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x00081D20 File Offset: 0x0007FF20
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in base.Attributes.Keys)
			{
				string text = (string)obj;
				if (!writer.IsValidFormAttribute(text))
				{
					arrayList.Add(text);
				}
			}
			foreach (object obj2 in arrayList)
			{
				string key = (string)obj2;
				base.Attributes.Remove(key);
			}
			bool enableLegacyRendering = base.EnableLegacyRendering;
			Page page = this.Page;
			if (writer.IsValidFormAttribute("name"))
			{
				if (page != null && page.RequestInternal != null && this.RenderingCompatibility < VersionUtil.Framework40 && (page.RequestInternal.Browser.W3CDomVersion.Major == 0 || page.XhtmlConformanceMode != XhtmlConformanceMode.Strict))
				{
					writer.WriteAttribute("name", this.Name);
				}
				base.Attributes.Remove("name");
			}
			writer.WriteAttribute("method", this.Method);
			base.Attributes.Remove("method");
			writer.WriteAttribute("action", this.GetActionAttribute(), true);
			base.Attributes.Remove("action");
			if (page != null)
			{
				string clientOnSubmitEvent = page.ClientOnSubmitEvent;
				if (!string.IsNullOrEmpty(clientOnSubmitEvent))
				{
					if (base.Attributes["onsubmit"] != null)
					{
						string text2 = base.Attributes["onsubmit"];
						if (text2.Length > 0)
						{
							if (!StringUtil.StringEndsWith(text2, ';'))
							{
								text2 += ";";
							}
							if (page.ClientSupportsJavaScript || !text2.ToLower(CultureInfo.CurrentCulture).Contains("javascript"))
							{
								page.ClientScript.RegisterOnSubmitStatement(typeof(HtmlForm), "OnSubmitScript", text2);
							}
							base.Attributes.Remove("onsubmit");
						}
					}
					if (page.ClientSupportsJavaScript || !clientOnSubmitEvent.ToLower(CultureInfo.CurrentCulture).Contains("javascript"))
					{
						if (enableLegacyRendering)
						{
							writer.WriteAttribute("language", "javascript", false);
						}
						writer.WriteAttribute("onsubmit", clientOnSubmitEvent);
					}
				}
				if (page.RequestInternal != null && page.RequestInternal.Browser.EcmaScriptVersion.Major > 0 && page.RequestInternal.Browser.W3CDomVersion.Major > 0 && this.DefaultButton.Length > 0)
				{
					Control control = base.FindControlFromPageIfNecessary(this.DefaultButton);
					if (!(control is IButtonControl))
					{
						throw new InvalidOperationException(SR.GetString("HtmlForm_OnlyIButtonControlCanBeDefaultButton", new object[]
						{
							this.ID
						}));
					}
					page.ClientScript.RegisterDefaultButtonScript(control, writer, false);
				}
			}
			base.EnsureID();
			base.RenderAttributes(writer);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x00082030 File Offset: 0x00080230
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.OnFormRender();
				page.BeginFormRender(writer, this.UniqueID);
			}
			HttpWriter httpWriter = writer.InnerWriter as HttpWriter;
			if (page != null && httpWriter != null && RuntimeConfig.GetConfig(this.Context).Pages.RenderAllHiddenFieldsAtTopOfForm)
			{
				httpWriter.HasBeenClearedRecently = false;
				int responseBufferCountAfterFlush = httpWriter.GetResponseBufferCountAfterFlush();
				base.RenderChildren(writer);
				int responseBufferCountAfterFlush2 = httpWriter.GetResponseBufferCountAfterFlush();
				page.EndFormRenderHiddenFields(writer, this.UniqueID);
				if (!httpWriter.HasBeenClearedRecently)
				{
					int responseBufferCountAfterFlush3 = httpWriter.GetResponseBufferCountAfterFlush();
					httpWriter.MoveResponseBufferRangeForward(responseBufferCountAfterFlush2, responseBufferCountAfterFlush3 - responseBufferCountAfterFlush2, responseBufferCountAfterFlush);
				}
				page.EndFormRenderArrayAndExpandoAttribute(writer, this.UniqueID);
				page.EndFormRenderPostBackAndWebFormsScript(writer, this.UniqueID);
				page.OnFormPostRender(writer);
				return;
			}
			base.RenderChildren(writer);
			if (page != null)
			{
				page.EndFormRender(writer, this.UniqueID);
				page.OnFormPostRender(writer);
			}
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x0008210B File Offset: 0x0008030B
		public override void RenderControl(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				base.RenderChildren(writer);
				return;
			}
			base.RenderControl(writer);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x00082124 File Offset: 0x00080324
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this, 100, 2);
		}

		// Token: 0x04001DE3 RID: 7651
		private string _defaultFocus;

		// Token: 0x04001DE4 RID: 7652
		private string _defaultButton;

		// Token: 0x04001DE5 RID: 7653
		private bool _submitDisabledControls;

		// Token: 0x04001DE6 RID: 7654
		private const string _aspnetFormID = "aspnetForm";
	}
}
