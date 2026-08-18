using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.PageLayout;
using Telerik.Web.UI.PageLayout.Enums;
using Telerik.Web.UI.PageLayout.Utils;

namespace Telerik.Web.UI
{
	// Token: 0x02000644 RID: 1604
	[ToolboxData("<{0}:RadPageLayout runat=\"server\"></{0}:RadPageLayout>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Container")]
	[ToolboxBitmap(typeof(RadPageLayout), "Telerik.Web.UI.PageLayout.png")]
	[ParseChildren(typeof(LayoutRow), ChildrenAsProperties = true, DefaultProperty = "Rows")]
	[EmbeddedSkin("PageLayout", typeof(RadPageLayout))]
	[Designer("Telerik.Web.Design.RadPageLayoutDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadPageLayout : WebControl, IMutableRendering, ISkinnableControl, IControl
	{
		// Token: 0x06003A85 RID: 14981 RVA: 0x000BF01E File Offset: 0x000BD21E
		public RadPageLayout()
		{
			this.EnableEmbeddedBaseStylesheet = true;
			this.RegisterWithScriptManager = true;
			this.RenderMode = RenderMode.Classic;
			this.EnsureLicensing();
			this.GridType = GridType.Static;
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x000BF05E File Offset: 0x000BD25E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		internal ControlCollectionBase Children
		{
			get
			{
				if (this._children == null)
				{
					this._children = new LayoutRowCollection(this);
					this._children.SetOwner(this);
				}
				return this._children;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06003A87 RID: 14983 RVA: 0x000BF086 File Offset: 0x000BD286
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public LayoutRowCollection Rows
		{
			[DebuggerStepThrough]
			get
			{
				return (LayoutRowCollection)this.Children;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x000BF093 File Offset: 0x000BD293
		// (set) Token: 0x06003A89 RID: 14985 RVA: 0x000BF09B File Offset: 0x000BD29B
		public GridType GridType { get; set; }

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06003A8A RID: 14986 RVA: 0x000BF0A4 File Offset: 0x000BD2A4
		// (set) Token: 0x06003A8B RID: 14987 RVA: 0x000BF0AC File Offset: 0x000BD2AC
		public bool ShowGrid { get; set; }

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000BF0B5 File Offset: 0x000BD2B5
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000BF0BD File Offset: 0x000BD2BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public string Skin { get; set; }

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000BF0C6 File Offset: 0x000BD2C6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsSkinSet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x000BF0C9 File Offset: 0x000BD2C9
		// (set) Token: 0x06003A90 RID: 14992 RVA: 0x000BF0D1 File Offset: 0x000BD2D1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool EnableEmbeddedSkins { get; set; }

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06003A91 RID: 14993 RVA: 0x000BF0DA File Offset: 0x000BD2DA
		// (set) Token: 0x06003A92 RID: 14994 RVA: 0x000BF0E2 File Offset: 0x000BD2E2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool EnableEmbeddedScripts { get; set; }

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06003A93 RID: 14995 RVA: 0x000BF0EB File Offset: 0x000BD2EB
		// (set) Token: 0x06003A94 RID: 14996 RVA: 0x000BF0F3 File Offset: 0x000BD2F3
		public bool EnableEmbeddedBaseStylesheet { get; set; }

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x000BF0FC File Offset: 0x000BD2FC
		// (set) Token: 0x06003A96 RID: 14998 RVA: 0x000BF104 File Offset: 0x000BD304
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public string AjaxCssRegistrations { get; set; }

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06003A97 RID: 14999 RVA: 0x000BF10D File Offset: 0x000BD30D
		// (set) Token: 0x06003A98 RID: 15000 RVA: 0x000BF115 File Offset: 0x000BD315
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool EnableAjaxSkinRendering { get; set; }

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x000BF11E File Offset: 0x000BD31E
		// (set) Token: 0x06003A9A RID: 15002 RVA: 0x000BF126 File Offset: 0x000BD326
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public RenderMode RenderMode { get; set; }

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06003A9B RID: 15003 RVA: 0x000BF130 File Offset: 0x000BD330
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public RenderMode ResolvedRenderMode
		{
			get
			{
				RenderMode renderMode = this.RenderMode;
				if (renderMode == RenderMode.Auto)
				{
					renderMode = this.PreferredRenderMode(RenderModeBrowserAdaptor.Instance);
				}
				return renderMode;
			}
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000BF154 File Offset: 0x000BD354
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public virtual List<string> GetEmbeddedSkinNames()
		{
			return SkinRegistrar.GetEmbeddedSkinNames(base.GetType());
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000BF161 File Offset: 0x000BD361
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string GetSkinSuffix()
		{
			return "";
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000BF168 File Offset: 0x000BD368
		public virtual RenderMode PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			return RenderMode.Classic;
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000BF16B File Offset: 0x000BD36B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public void DescribeComponent(IScriptDescriptor descriptor)
		{
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06003AA0 RID: 15008 RVA: 0x000BF16D File Offset: 0x000BD36D
		// (set) Token: 0x06003AA1 RID: 15009 RVA: 0x000BF175 File Offset: 0x000BD375
		public bool RegisterWithScriptManager { get; set; }

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000BF17E File Offset: 0x000BD37E
		public void EnsureChildControlsCreated()
		{
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000BF180 File Offset: 0x000BD380
		private void EnsureLicensing()
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
			{
				try
				{
					LicenseManager.Validate(base.GetType());
				}
				catch
				{
				}
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000BF1B8 File Offset: 0x000BD3B8
		// (set) Token: 0x06003AA5 RID: 15013 RVA: 0x000BF1C0 File Offset: 0x000BD3C0
		public TagName HtmlTag
		{
			get
			{
				return this._htmlTag;
			}
			set
			{
				this._htmlTag = value;
				this._tagName = this._htmlTag.ToString().ToLower();
			}
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x000BF1E4 File Offset: 0x000BD3E4
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Unknown;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x000BF1E7 File Offset: 0x000BD3E7
		protected override string TagName
		{
			get
			{
				return this._tagName;
			}
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000BF1EF File Offset: 0x000BD3EF
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.RegisterCssReferences();
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000BF200 File Offset: 0x000BD400
		protected virtual void RegisterCssReferences()
		{
			RadStyleSheetManager current = RadStyleSheetManager.GetCurrent(this.Page);
			if (current == null)
			{
				SkinRegistrar.RegisterCssReferences(this);
				return;
			}
			current.RegisterSkinnableControl(this);
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000BF22C File Offset: 0x000BD42C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = this.GetAllCssClasses(cssClass);
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000BF25C File Offset: 0x000BD45C
		internal string GetAllCssClasses(string originalCssClass)
		{
			List<string> list = new List<string>
			{
				"t-container",
				string.Format("{0}-{1}", "t-container", this.GridType.ToString().ToLower())
			};
			list.AddRange(originalCssClass.Split(null, StringSplitOptions.RemoveEmptyEntries));
			return CssUtils.NormalizeClassNames(list);
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000BF2BC File Offset: 0x000BD4BC
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (this.HtmlTag == Telerik.Web.UI.PageLayout.TagName.None)
			{
				return;
			}
			base.RenderBeginTag(writer);
			if (!base.DesignMode && this.Page.Request != null && this.Page.Request.Browser.Browser == "IE" && this.Page.Request.Browser.MajorVersion <= 7)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-container-inner");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
		}

		// Token: 0x06003AAD RID: 15021 RVA: 0x000BF340 File Offset: 0x000BD540
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			foreach (object obj in this.Children)
			{
				LayoutRow layoutRow = (LayoutRow)obj;
				layoutRow.RenderControl(writer);
			}
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000BF39C File Offset: 0x000BD59C
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this.HtmlTag == Telerik.Web.UI.PageLayout.TagName.None)
			{
				return;
			}
			if (!base.DesignMode && this.Page.Request != null && this.Page.Request.Browser.Browser == "IE" && this.Page.Request.Browser.MajorVersion <= 7)
			{
				writer.RenderEndTag();
			}
			base.RenderEndTag(writer);
			if (this.ShowGrid)
			{
				string value = string.Format("<div class='{0} {1}1'><span></span></div>", "t-col", "t-col-");
				writer.Write("<div class='t-show-grid'>");
				writer.Write(string.Format("<div class='t-container t-container-{0}'>", this.GridType.ToString().ToLower()));
				writer.Write("<div class='t-row'>");
				for (int i = 0; i < 12; i++)
				{
					writer.Write(value);
				}
				writer.Write("</div>");
				writer.Write("</div>");
				writer.Write("</div>");
			}
		}

		// Token: 0x04000FC7 RID: 4039
		private const int GridColumnsNumber = 12;

		// Token: 0x04000FC8 RID: 4040
		private LayoutRowCollection _children;

		// Token: 0x04000FC9 RID: 4041
		private TagName _htmlTag;

		// Token: 0x04000FCA RID: 4042
		private string _tagName = Telerik.Web.UI.PageLayout.TagName.Div.ToString().ToLower();
	}
}
