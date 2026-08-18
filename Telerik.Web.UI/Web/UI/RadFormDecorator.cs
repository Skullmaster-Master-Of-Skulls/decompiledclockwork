using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020018F5 RID: 6389
	[LightweightRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadFormDecorator))]
	[RequiredScript(typeof(RadFromDecoratorScripts))]
	[ClientScriptResource("Telerik.Web.UI.RadFormDecorator", "Telerik.Web.UI.Common.Core.js")]
	[TelerikToolboxCategory("Miscellaneous")]
	[ToolboxBitmap(typeof(RadFormDecorator), "Telerik.Web.UI.FormDecorator.png")]
	[Designer("Telerik.Web.Design.RadFormDecoratorDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadFormDecorator Runat=server></{0}:RadFormDecorator>")]
	[NativeRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("FormDecorator", typeof(RadFormDecorator))]
	[EmbeddedSkin("FormDecorator", "Default", typeof(RadFormDecorator))]
	public class RadFormDecorator : RadWebControl
	{
		// Token: 0x0600F611 RID: 62993 RVA: 0x0037D4C6 File Offset: 0x0037B6C6
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "decorationZoneID", this.DecorationZoneID, "");
			base.DescribeProperty<bool>(descriptor, "enableRoundedCorners", this.EnableRoundedCorners, true);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600F612 RID: 62994 RVA: 0x0037D4F9 File Offset: 0x0037B6F9
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17004A0E RID: 18958
		// (get) Token: 0x0600F613 RID: 62995 RVA: 0x0037D502 File Offset: 0x0037B702
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17004A0F RID: 18959
		// (get) Token: 0x0600F614 RID: 62996 RVA: 0x0037D506 File Offset: 0x0037B706
		protected override string CssClassFormatString
		{
			get
			{
				return "RadFormDecorator";
			}
		}

		// Token: 0x17004A10 RID: 18960
		// (get) Token: 0x0600F615 RID: 62997 RVA: 0x0037D50D File Offset: 0x0037B70D
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17004A11 RID: 18961
		// (get) Token: 0x0600F616 RID: 62998 RVA: 0x0037D510 File Offset: 0x0037B710
		// (set) Token: 0x0600F617 RID: 62999 RVA: 0x0037D53C File Offset: 0x0037B73C
		[Category("Behavior")]
		[DefaultValue(FormDecoratorDecoratedControls.Default)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public FormDecoratorDecoratedControls DecoratedControls
		{
			get
			{
				if (this.ViewState["DecoratedControls"] == null)
				{
					return FormDecoratorDecoratedControls.Default;
				}
				return (FormDecoratorDecoratedControls)this.ViewState["DecoratedControls"];
			}
			set
			{
				this.ViewState["DecoratedControls"] = value;
			}
		}

		// Token: 0x17004A12 RID: 18962
		// (get) Token: 0x0600F618 RID: 63000 RVA: 0x0037D554 File Offset: 0x0037B754
		// (set) Token: 0x0600F619 RID: 63001 RVA: 0x0037D57F File Offset: 0x0037B77F
		[DefaultValue(FormDecoratorDecoratedControls.None)]
		[Category("Behavior")]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public FormDecoratorDecoratedControls ControlsToSkip
		{
			get
			{
				if (this.ViewState["ControlsToSkip"] == null)
				{
					return FormDecoratorDecoratedControls.None;
				}
				return (FormDecoratorDecoratedControls)this.ViewState["ControlsToSkip"];
			}
			set
			{
				this.ViewState["ControlsToSkip"] = value;
			}
		}

		// Token: 0x17004A13 RID: 18963
		// (get) Token: 0x0600F61A RID: 63002 RVA: 0x0037D597 File Offset: 0x0037B797
		// (set) Token: 0x0600F61B RID: 63003 RVA: 0x0037D5B8 File Offset: 0x0037B7B8
		[Bindable(true)]
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Gets or sets whether decorated textboxes, textarea and fieldset elements will have rounded corners")]
		[ClientControlProperty]
		[Browsable(true)]
		public bool EnableRoundedCorners
		{
			get
			{
				return (bool)(this.ViewState["EnableRoundedCorners"] ?? true);
			}
			set
			{
				this.ViewState["EnableRoundedCorners"] = value;
			}
		}

		// Token: 0x17004A14 RID: 18964
		// (get) Token: 0x0600F61C RID: 63004 RVA: 0x0037D5D0 File Offset: 0x0037B7D0
		// (set) Token: 0x0600F61D RID: 63005 RVA: 0x0037D5F0 File Offset: 0x0037B7F0
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Gets or sets the id (ClientID if a runat=server is used) of a html element whose children will be decorated")]
		[Bindable(true)]
		[DefaultValue("")]
		[Browsable(true)]
		public string DecorationZoneID
		{
			get
			{
				return ((string)this.ViewState["DecorationZoneID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DecorationZoneID"] = value;
			}
		}

		// Token: 0x17004A15 RID: 18965
		// (get) Token: 0x0600F61E RID: 63006 RVA: 0x0037D603 File Offset: 0x0037B803
		private FormDecoratorDecoratedControls CurrentDecoratedControls
		{
			get
			{
				return this.DecoratedControls - (int)this.ControlsToSkip;
			}
		}

		// Token: 0x0600F61F RID: 63007 RVA: 0x0037D614 File Offset: 0x0037B814
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("skin", base.RuntimeSkin);
			descriptor.AddProperty("enabled", this.Enabled);
			descriptor.AddProperty("decoratedControls", this.CurrentDecoratedControls);
			descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
		}

		// Token: 0x0600F620 RID: 63008 RVA: 0x0037D67C File Offset: 0x0037B87C
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>(scriptReferences);
			if ((this.CurrentDecoratedControls & FormDecoratorDecoratedControls.Select) > FormDecoratorDecoratedControls.None && this.EnableEmbeddedScripts)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.Popup.PopupScripts.js", Assembly.GetExecutingAssembly().FullName));
			}
			return list;
		}

		// Token: 0x0600F621 RID: 63009 RVA: 0x0037D6CC File Offset: 0x0037B8CC
		protected override void ControlPreRender()
		{
			base.EnsureID();
			base.Style[HtmlTextWriterStyle.Display] = "none";
			if (this.Enabled && (base.ScriptManager == null || !base.ScriptManager.IsInAsyncPostBack))
			{
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("script");
				htmlGenericControl.Attributes["type"] = "text/javascript";
				string text = "\r\nif (typeof(WebForm_AutoFocus) != 'undefined' && !isWebFormAutoFocusMethodCalled)\r\n{   \r\n\tvar old_WebForm_AutoFocus = WebForm_AutoFocus;\r\n\tWebForm_AutoFocus = function(arg)\r\n\t{\r\n\t\tSys.Application.add_load(function()\r\n\t\t{            \r\n\t\t\told_WebForm_AutoFocus(arg);\r\n\t\t\tWebForm_AutoFocus = old_WebForm_AutoFocus;\r\n\t\t});\r\n\t}\r\n    var isWebFormAutoFocusMethodCalled = true;\r\n}";
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				text += string.Format("\r\nif (typeof(Telerik) != 'undefined' && Type.isNamespace(Telerik.Web))\r\n{{\r\n\tif (Telerik.Web.UI.RadFormDecorator)\r\n\t{{\r\n\t\tTelerik.Web.UI.RadFormDecorator.initializePage({0}, {1}, {2}, {3});\r\n\t}}\r\n}}", new object[]
				{
					javaScriptSerializer.Serialize(this.ClientID),
					javaScriptSerializer.Serialize(this.DecorationZoneID),
					javaScriptSerializer.Serialize(base.RuntimeSkin),
					(int)this.CurrentDecoratedControls
				});
				htmlGenericControl.InnerHtml = "\n//<![CDATA[\n" + text + "\n//]]>\n";
				this.Controls.Add(htmlGenericControl);
			}
			base.ControlPreRender();
			if ((this.CurrentDecoratedControls & FormDecoratorDecoratedControls.LoginControls) > FormDecoratorDecoratedControls.None || (this.CurrentDecoratedControls & FormDecoratorDecoratedControls.ValidationSummary) > FormDecoratorDecoratedControls.None || (this.CurrentDecoratedControls & FormDecoratorDecoratedControls.GridFormDetailsViews) > FormDecoratorDecoratedControls.None)
			{
				this.DecorateAspNetControls();
			}
		}

		// Token: 0x0600F622 RID: 63010 RVA: 0x0037D7EF File Offset: 0x0037B9EF
		protected virtual void DecorateAspNetControls()
		{
			if (this.Page != null)
			{
				this.IterateNotDecoratedControls(this.Page, new Action<WebControl>(this.DecorateAspNetControl));
			}
		}

		// Token: 0x0600F623 RID: 63011 RVA: 0x0037D814 File Offset: 0x0037BA14
		private void IterateNotDecoratedControls(Control parent, Action<WebControl> decorateAction)
		{
			for (int i = 0; i < parent.Controls.Count; i++)
			{
				Control control = parent.Controls[i];
				if (control != null && control.Visible)
				{
					WebControl webControl = control as WebControl;
					if (this.CanBeDecorated(webControl))
					{
						decorateAction(webControl);
					}
					this.IterateNotDecoratedControls(control, decorateAction);
				}
			}
		}

		// Token: 0x0600F624 RID: 63012 RVA: 0x0037D86E File Offset: 0x0037BA6E
		private bool CanBeDecorated(WebControl control)
		{
			return control != null && string.IsNullOrEmpty(control.CssClass);
		}

		// Token: 0x0600F625 RID: 63013 RVA: 0x0037D880 File Offset: 0x0037BA80
		private void DecorateAspNetControl(WebControl webC)
		{
			FormView formView = webC as FormView;
			DetailsView detailsView = webC as DetailsView;
			GridView gridView = webC as GridView;
			Login login = webC as Login;
			ChangePassword changePassword = webC as ChangePassword;
			if ((formView != null || detailsView != null || gridView != null) && (this.CurrentDecoratedControls & FormDecoratorDecoratedControls.GridFormDetailsViews) > FormDecoratorDecoratedControls.None)
			{
				webC.CssClass = "rfdTable";
				if (formView != null)
				{
					formView.GridLines = GridLines.None;
					formView.RenderOuterTable = true;
				}
				else if (detailsView != null)
				{
					detailsView.GridLines = GridLines.None;
				}
				else if (gridView != null)
				{
					gridView.GridLines = GridLines.None;
				}
			}
			if (webC is ValidationSummary && (this.CurrentDecoratedControls & FormDecoratorDecoratedControls.ValidationSummary) > FormDecoratorDecoratedControls.None)
			{
				webC.CssClass = "rfdValidationSummaryControl";
			}
			if ((login != null || webC is LoginStatus || webC is LoginName || changePassword != null || webC is CreateUserWizard) && (this.CurrentDecoratedControls & FormDecoratorDecoratedControls.LoginControls) > FormDecoratorDecoratedControls.None)
			{
				webC.CssClass = "rfdLoginControl";
				if (login != null)
				{
					login.RenderOuterTable = true;
				}
				if (changePassword != null)
				{
					changePassword.RenderOuterTable = true;
				}
			}
		}
	}
}
