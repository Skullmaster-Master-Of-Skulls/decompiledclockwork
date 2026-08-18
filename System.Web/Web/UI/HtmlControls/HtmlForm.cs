using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000400 RID: 1024
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlForm : HtmlContainerControl
	{
		// Token: 0x0600327A RID: 12922 RVA: 0x000DCBC5 File Offset: 0x000DBBC5
		public HtmlForm() : base("form")
		{
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000DCBD4 File Offset: 0x000DBBD4
		// (set) Token: 0x0600327C RID: 12924 RVA: 0x000DCBFC File Offset: 0x000DBBFC
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Behavior")]
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

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000DCC14 File Offset: 0x000DBC14
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x000DCC2A File Offset: 0x000DBC2A
		[DefaultValue("")]
		[WebCategory("Behavior")]
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

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000DCC33 File Offset: 0x000DBC33
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000DCC49 File Offset: 0x000DBC49
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

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000DCC54 File Offset: 0x000DBC54
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x000DCC7C File Offset: 0x000DBC7C
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Behavior")]
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

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000DCC94 File Offset: 0x000DBC94
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x000DCCBC File Offset: 0x000DBCBC
		[DefaultValue("")]
		[WebCategory("Behavior")]
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

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000DCCD4 File Offset: 0x000DBCD4
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000DCCDC File Offset: 0x000DBCDC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
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

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000DCCDE File Offset: 0x000DBCDE
		// (set) Token: 0x06003288 RID: 12936 RVA: 0x000DCCE6 File Offset: 0x000DBCE6
		[DefaultValue(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000DCCF0 File Offset: 0x000DBCF0
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x000DCD18 File Offset: 0x000DBD18
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Behavior")]
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

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000DCD30 File Offset: 0x000DBD30
		public override string UniqueID
		{
			get
			{
				if (this.NamingContainer == this.Page)
				{
					return base.UniqueID;
				}
				return "aspnetForm";
			}
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000DCD4C File Offset: 0x000DBD4C
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
				stringBuilder.Append(HttpUtility.UrlEncodeSpaces(HttpUtility.HtmlAttributeEncode(this.Page.ClientScript.GetWebResourceUrl(typeof(HtmlForm), "SmartNav.htm"))));
				stringBuilder.Append("\"></IFRAME>");
				output.WriteLine(stringBuilder.ToString());
			}
			base.Render(output);
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000DCDE8 File Offset: 0x000DBDE8
		private string GetActionAttribute()
		{
			string action = this.Action;
			if (!string.IsNullOrEmpty(action) && !AppSettings.IgnoreFormActionAttribute)
			{
				return action;
			}
			VirtualPath clientFilePath = this.Context.Request.ClientFilePath;
			VirtualPath virtualPath = this.Context.Request.CurrentExecutionFilePathObject;
			string text;
			if ((this.Context.WorkerRequest != null && this.Context.WorkerRequest.IsRewriteModuleEnabled && this.Context.ServerExecuteDepth == 0) || object.ReferenceEquals(virtualPath, clientFilePath))
			{
				text = clientFilePath.VirtualPathString;
				int num = text.LastIndexOf('/');
				if (num >= 0)
				{
					text = text.Substring(num + 1);
				}
			}
			else
			{
				virtualPath = clientFilePath.MakeRelative(virtualPath);
				text = virtualPath.VirtualPathString;
			}
			bool flag = CookielessHelperClass.UseCookieless(this.Context, false, FormsAuthentication.CookieMode);
			if (flag && this.Context.Request != null && this.Context.Response != null)
			{
				text = this.Context.Response.ApplyAppPathModifier(text);
			}
			string clientQueryString = this.Page.ClientQueryString;
			if (!string.IsNullOrEmpty(clientQueryString))
			{
				text = text + "?" + clientQueryString;
			}
			return text;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000DCEFE File Offset: 0x000DBEFE
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.SetForm(this);
			this.Page.RegisterViewStateHandler();
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000DCF1E File Offset: 0x000DBF1E
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page.SmartNavigation)
			{
				this.Page.ClientScript.RegisterClientScriptResource(typeof(HtmlForm), "SmartNav.js");
			}
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000DCF54 File Offset: 0x000DBF54
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
				if (page != null && page.RequestInternal != null && (page.RequestInternal.Browser.W3CDomVersion.Major == 0 || page.XhtmlConformanceMode != XhtmlConformanceMode.Strict))
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
					Control control = this.FindControl(this.DefaultButton);
					if (control == null && this.Page != null)
					{
						char[] anyOf = new char[]
						{
							'$',
							':'
						};
						if (this.DefaultButton.IndexOfAny(anyOf) != -1)
						{
							control = this.Page.FindControl(this.DefaultButton);
						}
					}
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

		// Token: 0x06003291 RID: 12945 RVA: 0x000DD2B4 File Offset: 0x000DC2B4
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
				page.OnFormPostRender();
				return;
			}
			base.RenderChildren(writer);
			if (page != null)
			{
				page.EndFormRender(writer, this.UniqueID);
				page.OnFormPostRender();
			}
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000DD38D File Offset: 0x000DC38D
		public override void RenderControl(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				base.RenderChildren(writer);
				return;
			}
			base.RenderControl(writer);
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000DD3A6 File Offset: 0x000DC3A6
		protected override ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this, 100, 2);
		}

		// Token: 0x04002303 RID: 8963
		private const string _aspnetFormID = "aspnetForm";

		// Token: 0x04002304 RID: 8964
		private string _defaultFocus;

		// Token: 0x04002305 RID: 8965
		private string _defaultButton;

		// Token: 0x04002306 RID: 8966
		private bool _submitDisabledControls;
	}
}
