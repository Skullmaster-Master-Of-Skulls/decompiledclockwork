using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020008F1 RID: 2289
	[DefaultProperty("DataNavigateUrlField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CommonTileBinding : StateManager
	{
		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x0600566C RID: 22124 RVA: 0x00108CFF File Offset: 0x00106EFF
		// (set) Token: 0x0600566D RID: 22125 RVA: 0x00108D20 File Offset: 0x00106F20
		[DefaultValue(TileListTileType.RadTextTile)]
		public TileListTileType TileType
		{
			get
			{
				return (TileListTileType)(base.ViewState["TileType"] ?? TileListTileType.RadTextTile);
			}
			set
			{
				base.ViewState["TileType"] = value;
			}
		}

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x0600566E RID: 22126 RVA: 0x00108D38 File Offset: 0x00106F38
		// (set) Token: 0x0600566F RID: 22127 RVA: 0x00108D58 File Offset: 0x00106F58
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataTileTypeField
		{
			get
			{
				return (string)(base.ViewState["DataTileTypeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTileTypeField"] = value;
			}
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x06005670 RID: 22128 RVA: 0x00108D6B File Offset: 0x00106F6B
		// (set) Token: 0x06005671 RID: 22129 RVA: 0x00108D8C File Offset: 0x00106F8C
		[DefaultValue(TileShape.Square)]
		public TileShape Shape
		{
			get
			{
				return (TileShape)(base.ViewState["TileShape"] ?? TileShape.Square);
			}
			set
			{
				base.ViewState["TileShape"] = value;
			}
		}

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x06005672 RID: 22130 RVA: 0x00108DA4 File Offset: 0x00106FA4
		// (set) Token: 0x06005673 RID: 22131 RVA: 0x00108DC4 File Offset: 0x00106FC4
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataShapeField
		{
			get
			{
				return (string)(base.ViewState["DataShapeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataShapeField"] = value;
			}
		}

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x06005674 RID: 22132 RVA: 0x00108DD7 File Offset: 0x00106FD7
		// (set) Token: 0x06005675 RID: 22133 RVA: 0x00108DF7 File Offset: 0x00106FF7
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataGroupNameField
		{
			get
			{
				return (string)(base.ViewState["DataGroupNameField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataGroupNameField"] = value;
			}
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06005676 RID: 22134 RVA: 0x00108E0A File Offset: 0x0010700A
		// (set) Token: 0x06005677 RID: 22135 RVA: 0x00108E2A File Offset: 0x0010702A
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataNavigateUrlField
		{
			get
			{
				return (string)(base.ViewState["DataNavigateUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataNavigateUrlField"] = value;
			}
		}

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x06005678 RID: 22136 RVA: 0x00108E3D File Offset: 0x0010703D
		// (set) Token: 0x06005679 RID: 22137 RVA: 0x00108E5D File Offset: 0x0010705D
		[DefaultValue("")]
		[TypeConverter(typeof(TargetConverter))]
		public string Target
		{
			get
			{
				return (string)(base.ViewState["Target"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Target"] = value;
			}
		}

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x0600567A RID: 22138 RVA: 0x00108E70 File Offset: 0x00107070
		// (set) Token: 0x0600567B RID: 22139 RVA: 0x00108E90 File Offset: 0x00107090
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataTargetField
		{
			get
			{
				return (string)(base.ViewState["DataTargetField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTargetField"] = value;
			}
		}

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x0600567C RID: 22140 RVA: 0x00108EA3 File Offset: 0x001070A3
		// (set) Token: 0x0600567D RID: 22141 RVA: 0x00108EC3 File Offset: 0x001070C3
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataNameField
		{
			get
			{
				return (string)(base.ViewState["DataNameField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataNameField"] = value;
			}
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x0600567E RID: 22142 RVA: 0x00108ED6 File Offset: 0x001070D6
		// (set) Token: 0x0600567F RID: 22143 RVA: 0x00108EF6 File Offset: 0x001070F6
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataTitleTextField
		{
			get
			{
				return (string)(base.ViewState["DataTitleTextField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTitleTextField"] = value;
			}
		}

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x06005680 RID: 22144 RVA: 0x00108F09 File Offset: 0x00107109
		// (set) Token: 0x06005681 RID: 22145 RVA: 0x00108F29 File Offset: 0x00107129
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataTitleImageUrlField
		{
			get
			{
				return (string)(base.ViewState["DataTitleImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTitleImageUrlField"] = value;
			}
		}

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x06005682 RID: 22146 RVA: 0x00108F3C File Offset: 0x0010713C
		// (set) Token: 0x06005683 RID: 22147 RVA: 0x00108F5C File Offset: 0x0010715C
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataBadgeValueField
		{
			get
			{
				return (string)(base.ViewState["DataBadgeValueField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataBadgeValueField"] = value;
			}
		}

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x06005684 RID: 22148 RVA: 0x00108F6F File Offset: 0x0010716F
		// (set) Token: 0x06005685 RID: 22149 RVA: 0x00108F8F File Offset: 0x0010718F
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataBadgeImageUrlField
		{
			get
			{
				return (string)(base.ViewState["DataBadgeImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataBadgeImageUrlField"] = value;
			}
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x06005686 RID: 22150 RVA: 0x00108FA2 File Offset: 0x001071A2
		// (set) Token: 0x06005687 RID: 22151 RVA: 0x00108FC2 File Offset: 0x001071C2
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataBadgePredefinedTypeField
		{
			get
			{
				return (string)(base.ViewState["DataBadgePredefinedTypeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataBadgePredefinedTypeField"] = value;
			}
		}
	}
}
