using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001B68 RID: 7016
	public class RadTreeNodeBinding : NavigationItemBinding
	{
		// Token: 0x170052E4 RID: 21220
		// (get) Token: 0x06010FB4 RID: 69556 RVA: 0x003C161D File Offset: 0x003BF81D
		// (set) Token: 0x06010FB5 RID: 69557 RVA: 0x003C163D File Offset: 0x003BF83D
		[DefaultValue("")]
		public string ContextMenuID
		{
			get
			{
				return (string)(base.ViewState["ContextMenuID"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ContextMenuID"] = value;
			}
		}

		// Token: 0x170052E5 RID: 21221
		// (get) Token: 0x06010FB6 RID: 69558 RVA: 0x003C1650 File Offset: 0x003BF850
		// (set) Token: 0x06010FB7 RID: 69559 RVA: 0x003C1670 File Offset: 0x003BF870
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ContextMenuIDField
		{
			get
			{
				return (string)(base.ViewState["ContextMenuIDField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ContextMenuIDField"] = value;
			}
		}

		// Token: 0x170052E6 RID: 21222
		// (get) Token: 0x06010FB8 RID: 69560 RVA: 0x003C1683 File Offset: 0x003BF883
		// (set) Token: 0x06010FB9 RID: 69561 RVA: 0x003C16A4 File Offset: 0x003BF8A4
		[DefaultValue(true)]
		public bool AllowDrag
		{
			get
			{
				return (bool)(base.ViewState["AllowDrag"] ?? true);
			}
			set
			{
				base.ViewState["AllowDrag"] = value;
			}
		}

		// Token: 0x170052E7 RID: 21223
		// (get) Token: 0x06010FBA RID: 69562 RVA: 0x003C16BC File Offset: 0x003BF8BC
		// (set) Token: 0x06010FBB RID: 69563 RVA: 0x003C16DC File Offset: 0x003BF8DC
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string AllowDragField
		{
			get
			{
				return (string)(base.ViewState["AllowDragField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["AllowDragField"] = value;
			}
		}

		// Token: 0x170052E8 RID: 21224
		// (get) Token: 0x06010FBC RID: 69564 RVA: 0x003C16EF File Offset: 0x003BF8EF
		// (set) Token: 0x06010FBD RID: 69565 RVA: 0x003C1710 File Offset: 0x003BF910
		[DefaultValue(true)]
		public bool AllowDrop
		{
			get
			{
				return (bool)(base.ViewState["AllowDrop"] ?? true);
			}
			set
			{
				base.ViewState["AllowDrop"] = value;
			}
		}

		// Token: 0x170052E9 RID: 21225
		// (get) Token: 0x06010FBE RID: 69566 RVA: 0x003C1728 File Offset: 0x003BF928
		// (set) Token: 0x06010FBF RID: 69567 RVA: 0x003C1748 File Offset: 0x003BF948
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string AllowDropField
		{
			get
			{
				return (string)(base.ViewState["AllowDropField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["AllowDropField"] = value;
			}
		}

		// Token: 0x170052EA RID: 21226
		// (get) Token: 0x06010FC0 RID: 69568 RVA: 0x003C175B File Offset: 0x003BF95B
		// (set) Token: 0x06010FC1 RID: 69569 RVA: 0x003C177C File Offset: 0x003BF97C
		[DefaultValue(true)]
		public bool AllowEdit
		{
			get
			{
				return (bool)(base.ViewState["AllowEdit"] ?? true);
			}
			set
			{
				base.ViewState["AllowEdit"] = value;
			}
		}

		// Token: 0x170052EB RID: 21227
		// (get) Token: 0x06010FC2 RID: 69570 RVA: 0x003C1794 File Offset: 0x003BF994
		// (set) Token: 0x06010FC3 RID: 69571 RVA: 0x003C17B4 File Offset: 0x003BF9B4
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string AllowEditField
		{
			get
			{
				return (string)(base.ViewState["AllowEditField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["AllowEditField"] = value;
			}
		}

		// Token: 0x170052EC RID: 21228
		// (get) Token: 0x06010FC4 RID: 69572 RVA: 0x003C17C7 File Offset: 0x003BF9C7
		// (set) Token: 0x06010FC5 RID: 69573 RVA: 0x003C17E7 File Offset: 0x003BF9E7
		[DefaultValue("")]
		public string Category
		{
			get
			{
				return (string)(base.ViewState["Category"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Category"] = value;
			}
		}

		// Token: 0x170052ED RID: 21229
		// (get) Token: 0x06010FC6 RID: 69574 RVA: 0x003C17FA File Offset: 0x003BF9FA
		// (set) Token: 0x06010FC7 RID: 69575 RVA: 0x003C181A File Offset: 0x003BFA1A
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string CategoryField
		{
			get
			{
				return (string)(base.ViewState["CategoryField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CategoryField"] = value;
			}
		}

		// Token: 0x170052EE RID: 21230
		// (get) Token: 0x06010FC8 RID: 69576 RVA: 0x003C182D File Offset: 0x003BFA2D
		// (set) Token: 0x06010FC9 RID: 69577 RVA: 0x003C184E File Offset: 0x003BFA4E
		[DefaultValue(true)]
		public bool Checkable
		{
			get
			{
				return (bool)(base.ViewState["Checkable"] ?? true);
			}
			set
			{
				base.ViewState["Checkable"] = value;
			}
		}

		// Token: 0x170052EF RID: 21231
		// (get) Token: 0x06010FCA RID: 69578 RVA: 0x003C1866 File Offset: 0x003BFA66
		// (set) Token: 0x06010FCB RID: 69579 RVA: 0x003C1886 File Offset: 0x003BFA86
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string CheckableField
		{
			get
			{
				return (string)(base.ViewState["CheckableField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CheckableField"] = value;
			}
		}

		// Token: 0x170052F0 RID: 21232
		// (get) Token: 0x06010FCC RID: 69580 RVA: 0x003C1899 File Offset: 0x003BFA99
		// (set) Token: 0x06010FCD RID: 69581 RVA: 0x003C18BA File Offset: 0x003BFABA
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return (bool)(base.ViewState["Checked"] ?? false);
			}
			set
			{
				base.ViewState["Checked"] = value;
			}
		}

		// Token: 0x170052F1 RID: 21233
		// (get) Token: 0x06010FCE RID: 69582 RVA: 0x003C18D2 File Offset: 0x003BFAD2
		// (set) Token: 0x06010FCF RID: 69583 RVA: 0x003C18F2 File Offset: 0x003BFAF2
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string CheckedField
		{
			get
			{
				return (string)(base.ViewState["CheckedField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CheckedField"] = value;
			}
		}

		// Token: 0x170052F2 RID: 21234
		// (get) Token: 0x06010FD0 RID: 69584 RVA: 0x003C1905 File Offset: 0x003BFB05
		// (set) Token: 0x06010FD1 RID: 69585 RVA: 0x003C1925 File Offset: 0x003BFB25
		[DefaultValue("")]
		public string DisabledCssClass
		{
			get
			{
				return (string)(base.ViewState["DisabledCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledCssClass"] = value;
			}
		}

		// Token: 0x170052F3 RID: 21235
		// (get) Token: 0x06010FD2 RID: 69586 RVA: 0x003C1938 File Offset: 0x003BFB38
		// (set) Token: 0x06010FD3 RID: 69587 RVA: 0x003C1958 File Offset: 0x003BFB58
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DisabledCssClassField
		{
			get
			{
				return (string)(base.ViewState["DisabledCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledCssClassField"] = value;
			}
		}

		// Token: 0x170052F4 RID: 21236
		// (get) Token: 0x06010FD4 RID: 69588 RVA: 0x003C196B File Offset: 0x003BFB6B
		// (set) Token: 0x06010FD5 RID: 69589 RVA: 0x003C198B File Offset: 0x003BFB8B
		[DefaultValue("")]
		public string DisabledImageUrl
		{
			get
			{
				return (string)(base.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x170052F5 RID: 21237
		// (get) Token: 0x06010FD6 RID: 69590 RVA: 0x003C199E File Offset: 0x003BFB9E
		// (set) Token: 0x06010FD7 RID: 69591 RVA: 0x003C19BE File Offset: 0x003BFBBE
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DisabledImageUrlField
		{
			get
			{
				return (string)(base.ViewState["DisabledImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledImageUrlField"] = value;
			}
		}

		// Token: 0x170052F6 RID: 21238
		// (get) Token: 0x06010FD8 RID: 69592 RVA: 0x003C19D1 File Offset: 0x003BFBD1
		// (set) Token: 0x06010FD9 RID: 69593 RVA: 0x003C19F2 File Offset: 0x003BFBF2
		[DefaultValue(true)]
		public bool EnableContextMenu
		{
			get
			{
				return (bool)(base.ViewState["EnableContextMenu"] ?? true);
			}
			set
			{
				base.ViewState["EnableContextMenu"] = value;
			}
		}

		// Token: 0x170052F7 RID: 21239
		// (get) Token: 0x06010FDA RID: 69594 RVA: 0x003C1A0A File Offset: 0x003BFC0A
		// (set) Token: 0x06010FDB RID: 69595 RVA: 0x003C1A2A File Offset: 0x003BFC2A
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string EnableContextMenuField
		{
			get
			{
				return (string)(base.ViewState["EnableContextMenuField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["EnableContextMenuField"] = value;
			}
		}

		// Token: 0x170052F8 RID: 21240
		// (get) Token: 0x06010FDC RID: 69596 RVA: 0x003C1A3D File Offset: 0x003BFC3D
		// (set) Token: 0x06010FDD RID: 69597 RVA: 0x003C1A5E File Offset: 0x003BFC5E
		[DefaultValue(false)]
		public bool Expanded
		{
			get
			{
				return (bool)(base.ViewState["Expanded"] ?? false);
			}
			set
			{
				base.ViewState["Expanded"] = value;
			}
		}

		// Token: 0x170052F9 RID: 21241
		// (get) Token: 0x06010FDE RID: 69598 RVA: 0x003C1A76 File Offset: 0x003BFC76
		// (set) Token: 0x06010FDF RID: 69599 RVA: 0x003C1A96 File Offset: 0x003BFC96
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ExpandedField
		{
			get
			{
				return (string)(base.ViewState["ExpandedField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandedField"] = value;
			}
		}

		// Token: 0x170052FA RID: 21242
		// (get) Token: 0x06010FE0 RID: 69600 RVA: 0x003C1AA9 File Offset: 0x003BFCA9
		// (set) Token: 0x06010FE1 RID: 69601 RVA: 0x003C1AC9 File Offset: 0x003BFCC9
		[DefaultValue("")]
		public string ExpandedImageUrl
		{
			get
			{
				return (string)(base.ViewState["ExpandedImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandedImageUrl"] = value;
			}
		}

		// Token: 0x170052FB RID: 21243
		// (get) Token: 0x06010FE2 RID: 69602 RVA: 0x003C1ADC File Offset: 0x003BFCDC
		// (set) Token: 0x06010FE3 RID: 69603 RVA: 0x003C1AFC File Offset: 0x003BFCFC
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ExpandedImageUrlField
		{
			get
			{
				return (string)(base.ViewState["ExpandedImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandedImageUrlField"] = value;
			}
		}

		// Token: 0x170052FC RID: 21244
		// (get) Token: 0x06010FE4 RID: 69604 RVA: 0x003C1B0F File Offset: 0x003BFD0F
		// (set) Token: 0x06010FE5 RID: 69605 RVA: 0x003C1B30 File Offset: 0x003BFD30
		[DefaultValue(TreeNodeExpandMode.ClientSide)]
		public TreeNodeExpandMode ExpandMode
		{
			get
			{
				return (TreeNodeExpandMode)(base.ViewState["TreeNodeExpandMode"] ?? TreeNodeExpandMode.ClientSide);
			}
			set
			{
				base.ViewState["TreeNodeExpandMode"] = value;
			}
		}

		// Token: 0x170052FD RID: 21245
		// (get) Token: 0x06010FE6 RID: 69606 RVA: 0x003C1B48 File Offset: 0x003BFD48
		// (set) Token: 0x06010FE7 RID: 69607 RVA: 0x003C1B68 File Offset: 0x003BFD68
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ExpandModeField
		{
			get
			{
				return (string)(base.ViewState["ExpandModeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandModeField"] = value;
			}
		}

		// Token: 0x170052FE RID: 21246
		// (get) Token: 0x06010FE8 RID: 69608 RVA: 0x003C1B7B File Offset: 0x003BFD7B
		// (set) Token: 0x06010FE9 RID: 69609 RVA: 0x003C1B9B File Offset: 0x003BFD9B
		[DefaultValue("")]
		public string HoveredCssClass
		{
			get
			{
				return (string)(base.ViewState["HoveredCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x170052FF RID: 21247
		// (get) Token: 0x06010FEA RID: 69610 RVA: 0x003C1BAE File Offset: 0x003BFDAE
		// (set) Token: 0x06010FEB RID: 69611 RVA: 0x003C1BCE File Offset: 0x003BFDCE
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string HoveredCssClassField
		{
			get
			{
				return (string)(base.ViewState["HoveredCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HoveredCssClassField"] = value;
			}
		}

		// Token: 0x17005300 RID: 21248
		// (get) Token: 0x06010FEC RID: 69612 RVA: 0x003C1BE1 File Offset: 0x003BFDE1
		// (set) Token: 0x06010FED RID: 69613 RVA: 0x003C1C01 File Offset: 0x003BFE01
		[DefaultValue("")]
		public string SelectedCssClass
		{
			get
			{
				return (string)(base.ViewState["SelectedCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectedCssClass"] = value;
			}
		}

		// Token: 0x17005301 RID: 21249
		// (get) Token: 0x06010FEE RID: 69614 RVA: 0x003C1C14 File Offset: 0x003BFE14
		// (set) Token: 0x06010FEF RID: 69615 RVA: 0x003C1C34 File Offset: 0x003BFE34
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string SelectedCssClassField
		{
			get
			{
				return (string)(base.ViewState["SelectedCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectedCssClassField"] = value;
			}
		}

		// Token: 0x17005302 RID: 21250
		// (get) Token: 0x06010FF0 RID: 69616 RVA: 0x003C1C47 File Offset: 0x003BFE47
		// (set) Token: 0x06010FF1 RID: 69617 RVA: 0x003C1C67 File Offset: 0x003BFE67
		[DefaultValue("")]
		public string ContentCssClass
		{
			get
			{
				return (string)(base.ViewState["ContentCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ContentCssClass"] = value;
			}
		}

		// Token: 0x17005303 RID: 21251
		// (get) Token: 0x06010FF2 RID: 69618 RVA: 0x003C1C7A File Offset: 0x003BFE7A
		// (set) Token: 0x06010FF3 RID: 69619 RVA: 0x003C1C9A File Offset: 0x003BFE9A
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ContentCssClassField
		{
			get
			{
				return (string)(base.ViewState["ContentCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ContentCssClassField"] = value;
			}
		}

		// Token: 0x17005304 RID: 21252
		// (get) Token: 0x06010FF4 RID: 69620 RVA: 0x003C1CAD File Offset: 0x003BFEAD
		// (set) Token: 0x06010FF5 RID: 69621 RVA: 0x003C1CCD File Offset: 0x003BFECD
		[DefaultValue("")]
		public string SelectedImageUrl
		{
			get
			{
				return (string)(base.ViewState["SelectedImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectedImageUrl"] = value;
			}
		}

		// Token: 0x17005305 RID: 21253
		// (get) Token: 0x06010FF6 RID: 69622 RVA: 0x003C1CE0 File Offset: 0x003BFEE0
		// (set) Token: 0x06010FF7 RID: 69623 RVA: 0x003C1D00 File Offset: 0x003BFF00
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string SelectedImageUrlField
		{
			get
			{
				return (string)(base.ViewState["SelectedImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectedImageUrlField"] = value;
			}
		}

		// Token: 0x06010FF8 RID: 69624 RVA: 0x003C1D14 File Offset: 0x003BFF14
		internal override void ApplyTo(NavigationItem navigationItem, object dataItem, PropertyDescriptorCache propertyDescriptorCache)
		{
			if (!string.IsNullOrEmpty(this.ContextMenuID) || !string.IsNullOrEmpty(this.ContextMenuIDField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "ContextMenuID");
			}
			if (!string.IsNullOrEmpty(this.DisabledCssClass) || !string.IsNullOrEmpty(this.DisabledCssClassField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "DisabledCssClass");
			}
			if (!string.IsNullOrEmpty(this.DisabledImageUrl) || !string.IsNullOrEmpty(this.DisabledImageUrlField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "DisabledImageUrl");
			}
			if (!string.IsNullOrEmpty(this.ExpandedImageUrl) || !string.IsNullOrEmpty(this.ExpandedImageUrlField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "ExpandedImageUrl");
			}
			if (!string.IsNullOrEmpty(this.HoveredCssClass) || !string.IsNullOrEmpty(this.HoveredCssClassField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "HoveredCssClass");
			}
			if (!string.IsNullOrEmpty(this.SelectedCssClass) || !string.IsNullOrEmpty(this.SelectedCssClassField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "SelectedCssClass");
			}
			if (!string.IsNullOrEmpty(this.SelectedCssClass) || !string.IsNullOrEmpty(this.ContentCssClassField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "ContentCssClass");
			}
			if (!string.IsNullOrEmpty(this.SelectedImageUrl) || !string.IsNullOrEmpty(this.SelectedImageUrlField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "SelectedImageUrl");
			}
			if (!string.IsNullOrEmpty(this.Category) || !string.IsNullOrEmpty(this.CategoryField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "Category");
			}
			if (!this.AllowDrag || !string.IsNullOrEmpty(this.AllowDragField))
			{
				base.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "AllowDrag");
			}
			if (!this.AllowDrop || !string.IsNullOrEmpty(this.AllowDropField))
			{
				base.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "AllowDrop");
			}
			if (!this.AllowEdit || !string.IsNullOrEmpty(this.AllowEditField))
			{
				base.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "AllowEdit");
			}
			if (!this.Checkable || !string.IsNullOrEmpty(this.CheckableField))
			{
				base.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "Checkable");
			}
			if (this.Checked || !string.IsNullOrEmpty(this.CheckedField))
			{
				base.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "Checked");
			}
			if (!this.EnableContextMenu || !string.IsNullOrEmpty(this.EnableContextMenuField))
			{
				base.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "EnableContextMenu");
			}
			if (this.Expanded || !string.IsNullOrEmpty(this.ExpandedField))
			{
				base.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "Expanded");
			}
			bool flag = false;
			RadTreeNode radTreeNode = navigationItem as RadTreeNode;
			if (!string.IsNullOrEmpty(this.ExpandModeField))
			{
				radTreeNode.ExpandMode = (TreeNodeExpandMode)Enum.Parse(typeof(TreeNodeExpandMode), propertyDescriptorCache.GetPropertyValue(dataItem, this.ExpandModeField).ToString());
				flag = true;
			}
			if (!flag)
			{
				radTreeNode.ExpandMode = this.ExpandMode;
			}
			base.ApplyTo(navigationItem, dataItem, propertyDescriptorCache);
		}
	}
}
