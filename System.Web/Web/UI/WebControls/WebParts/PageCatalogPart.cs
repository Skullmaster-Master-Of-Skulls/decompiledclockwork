using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006D5 RID: 1749
	[Designer("System.Web.UI.Design.WebControls.WebParts.PageCatalogPartDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PageCatalogPart : CatalogPart
	{
		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x060055CE RID: 21966 RVA: 0x0015B7A4 File Offset: 0x0015A7A4
		// (set) Token: 0x060055CF RID: 21967 RVA: 0x0015B7D6 File Offset: 0x0015A7D6
		[WebSysDefaultValue("PageCatalogPart_PartTitle")]
		public override string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text == null)
				{
					return SR.GetString("PageCatalogPart_PartTitle");
				}
				return text;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0015B7EC File Offset: 0x0015A7EC
		public override WebPartDescriptionCollection GetAvailableWebPartDescriptions()
		{
			if (base.DesignMode)
			{
				return PageCatalogPart.DesignModeAvailableWebParts;
			}
			if (this._availableWebPartDescriptions == null)
			{
				WebPartCollection webPartCollection;
				if (base.WebPartManager != null)
				{
					WebPartCollection closedWebParts = this.GetClosedWebParts();
					if (closedWebParts != null)
					{
						webPartCollection = closedWebParts;
					}
					else
					{
						webPartCollection = new WebPartCollection();
					}
				}
				else
				{
					webPartCollection = new WebPartCollection();
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj in webPartCollection)
				{
					WebPart webPart = (WebPart)obj;
					if (!(webPart is UnauthorizedWebPart))
					{
						WebPartDescription value = new WebPartDescription(webPart);
						arrayList.Add(value);
					}
				}
				this._availableWebPartDescriptions = new WebPartDescriptionCollection(arrayList);
			}
			return this._availableWebPartDescriptions;
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x0015B8B0 File Offset: 0x0015A8B0
		private WebPartCollection GetClosedWebParts()
		{
			ArrayList arrayList = new ArrayList();
			WebPartCollection webParts = base.WebPartManager.WebParts;
			if (webParts != null)
			{
				foreach (object obj in webParts)
				{
					WebPart webPart = (WebPart)obj;
					if (webPart.IsClosed)
					{
						arrayList.Add(webPart);
					}
				}
			}
			return new WebPartCollection(arrayList);
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x0015B92C File Offset: 0x0015A92C
		public override WebPart GetWebPart(WebPartDescription description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			WebPartDescriptionCollection availableWebPartDescriptions = this.GetAvailableWebPartDescriptions();
			if (!availableWebPartDescriptions.Contains(description))
			{
				throw new ArgumentException(SR.GetString("CatalogPart_UnknownDescription"), "description");
			}
			return description.WebPart;
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x0015B974 File Offset: 0x0015A974
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (base.WebPartManager != null)
			{
				base.WebPartManager.WebPartAdded += this.OnWebPartsChanged;
				base.WebPartManager.WebPartClosed += this.OnWebPartsChanged;
				base.WebPartManager.WebPartDeleted += this.OnWebPartsChanged;
			}
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x0015B9D5 File Offset: 0x0015A9D5
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this._availableWebPartDescriptions = null;
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x0015B9E5 File Offset: 0x0015A9E5
		private void OnWebPartsChanged(object sender, WebPartEventArgs e)
		{
			this._availableWebPartDescriptions = null;
		}

		// Token: 0x060055D6 RID: 21974 RVA: 0x0015B9EE File Offset: 0x0015A9EE
		protected internal override void Render(HtmlTextWriter writer)
		{
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x060055D7 RID: 21975 RVA: 0x0015B9F0 File Offset: 0x0015A9F0
		// (set) Token: 0x060055D8 RID: 21976 RVA: 0x0015B9F8 File Offset: 0x0015A9F8
		[Browsable(false)]
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x060055D9 RID: 21977 RVA: 0x0015BA01 File Offset: 0x0015AA01
		// (set) Token: 0x060055DA RID: 21978 RVA: 0x0015BA09 File Offset: 0x0015AA09
		[Browsable(false)]
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x060055DB RID: 21979 RVA: 0x0015BA12 File Offset: 0x0015AA12
		// (set) Token: 0x060055DC RID: 21980 RVA: 0x0015BA1A File Offset: 0x0015AA1A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		[Browsable(false)]
		public override string BackImageUrl
		{
			get
			{
				return base.BackImageUrl;
			}
			set
			{
				base.BackImageUrl = value;
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x060055DD RID: 21981 RVA: 0x0015BA23 File Offset: 0x0015AA23
		// (set) Token: 0x060055DE RID: 21982 RVA: 0x0015BA2B File Offset: 0x0015AA2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Themeable(false)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x060055DF RID: 21983 RVA: 0x0015BA34 File Offset: 0x0015AA34
		// (set) Token: 0x060055E0 RID: 21984 RVA: 0x0015BA3C File Offset: 0x0015AA3C
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x060055E1 RID: 21985 RVA: 0x0015BA45 File Offset: 0x0015AA45
		// (set) Token: 0x060055E2 RID: 21986 RVA: 0x0015BA4D File Offset: 0x0015AA4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		[Browsable(false)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x060055E3 RID: 21987 RVA: 0x0015BA56 File Offset: 0x0015AA56
		// (set) Token: 0x060055E4 RID: 21988 RVA: 0x0015BA5E File Offset: 0x0015AA5E
		[Themeable(false)]
		[CssClassProperty]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x060055E5 RID: 21989 RVA: 0x0015BA67 File Offset: 0x0015AA67
		// (set) Token: 0x060055E6 RID: 21990 RVA: 0x0015BA6F File Offset: 0x0015AA6F
		[Browsable(false)]
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string DefaultButton
		{
			get
			{
				return base.DefaultButton;
			}
			set
			{
				base.DefaultButton = value;
			}
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x060055E7 RID: 21991 RVA: 0x0015BA78 File Offset: 0x0015AA78
		// (set) Token: 0x060055E8 RID: 21992 RVA: 0x0015BA80 File Offset: 0x0015AA80
		[Browsable(false)]
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ContentDirection Direction
		{
			get
			{
				return base.Direction;
			}
			set
			{
				base.Direction = value;
			}
		}

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x060055E9 RID: 21993 RVA: 0x0015BA89 File Offset: 0x0015AA89
		// (set) Token: 0x060055EA RID: 21994 RVA: 0x0015BA91 File Offset: 0x0015AA91
		[Browsable(false)]
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x060055EB RID: 21995 RVA: 0x0015BA9A File Offset: 0x0015AA9A
		// (set) Token: 0x060055EC RID: 21996 RVA: 0x0015BAA0 File Offset: 0x0015AAA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		[Browsable(false)]
		[Themeable(false)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x060055ED RID: 21997 RVA: 0x0015BAD2 File Offset: 0x0015AAD2
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x060055EE RID: 21998 RVA: 0x0015BADA File Offset: 0x0015AADA
		// (set) Token: 0x060055EF RID: 21999 RVA: 0x0015BAE2 File Offset: 0x0015AAE2
		[Themeable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x060055F0 RID: 22000 RVA: 0x0015BAEB File Offset: 0x0015AAEB
		// (set) Token: 0x060055F1 RID: 22001 RVA: 0x0015BAF3 File Offset: 0x0015AAF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		[Browsable(false)]
		public override string GroupingText
		{
			get
			{
				return base.GroupingText;
			}
			set
			{
				base.GroupingText = value;
			}
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x060055F2 RID: 22002 RVA: 0x0015BAFC File Offset: 0x0015AAFC
		// (set) Token: 0x060055F3 RID: 22003 RVA: 0x0015BB04 File Offset: 0x0015AB04
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Themeable(false)]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x060055F4 RID: 22004 RVA: 0x0015BB0D File Offset: 0x0015AB0D
		// (set) Token: 0x060055F5 RID: 22005 RVA: 0x0015BB15 File Offset: 0x0015AB15
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override HorizontalAlign HorizontalAlign
		{
			get
			{
				return base.HorizontalAlign;
			}
			set
			{
				base.HorizontalAlign = value;
			}
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x060055F6 RID: 22006 RVA: 0x0015BB1E File Offset: 0x0015AB1E
		// (set) Token: 0x060055F7 RID: 22007 RVA: 0x0015BB26 File Offset: 0x0015AB26
		[Browsable(false)]
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ScrollBars ScrollBars
		{
			get
			{
				return base.ScrollBars;
			}
			set
			{
				base.ScrollBars = value;
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x060055F8 RID: 22008 RVA: 0x0015BB2F File Offset: 0x0015AB2F
		// (set) Token: 0x060055F9 RID: 22009 RVA: 0x0015BB38 File Offset: 0x0015AB38
		[Browsable(false)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x060055FA RID: 22010 RVA: 0x0015BB6A File Offset: 0x0015AB6A
		// (set) Token: 0x060055FB RID: 22011 RVA: 0x0015BB72 File Offset: 0x0015AB72
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		[Browsable(false)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x060055FC RID: 22012 RVA: 0x0015BB7B File Offset: 0x0015AB7B
		// (set) Token: 0x060055FD RID: 22013 RVA: 0x0015BB83 File Offset: 0x0015AB83
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Themeable(false)]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				base.ToolTip = value;
			}
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x060055FE RID: 22014 RVA: 0x0015BB8C File Offset: 0x0015AB8C
		// (set) Token: 0x060055FF RID: 22015 RVA: 0x0015BB94 File Offset: 0x0015AB94
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Themeable(false)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06005600 RID: 22016 RVA: 0x0015BB9D File Offset: 0x0015AB9D
		// (set) Token: 0x06005601 RID: 22017 RVA: 0x0015BBA5 File Offset: 0x0015ABA5
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06005602 RID: 22018 RVA: 0x0015BBAE File Offset: 0x0015ABAE
		// (set) Token: 0x06005603 RID: 22019 RVA: 0x0015BBB6 File Offset: 0x0015ABB6
		[Browsable(false)]
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Wrap
		{
			get
			{
				return base.Wrap;
			}
			set
			{
				base.Wrap = value;
			}
		}

		// Token: 0x04002F34 RID: 12084
		private WebPartDescriptionCollection _availableWebPartDescriptions;

		// Token: 0x04002F35 RID: 12085
		private static readonly WebPartDescriptionCollection DesignModeAvailableWebParts = new WebPartDescriptionCollection(new WebPartDescription[]
		{
			new WebPartDescription("webpart1", string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogPart_SampleWebPartTitle"), new object[]
			{
				"1"
			}), null, null),
			new WebPartDescription("webpart2", string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogPart_SampleWebPartTitle"), new object[]
			{
				"2"
			}), null, null),
			new WebPartDescription("webpart3", string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogPart_SampleWebPartTitle"), new object[]
			{
				"3"
			}), null, null)
		});
	}
}
