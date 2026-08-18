using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001166 RID: 4454
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridResizing : ObjectWithState
	{
		// Token: 0x0600B58E RID: 46478 RVA: 0x0027FF57 File Offset: 0x0027E157
		public GridResizing(StateBag OwnerStateBag) : base("cs_resize_", OwnerStateBag)
		{
		}

		// Token: 0x17003AB2 RID: 15026
		// (get) Token: 0x0600B58F RID: 46479 RVA: 0x0027FF68 File Offset: 0x0027E168
		// (set) Token: 0x0600B590 RID: 46480 RVA: 0x0027FF91 File Offset: 0x0027E191
		[Description("RadGrid_AllowColumnResize")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowColumnResize
		{
			get
			{
				object obj = base.ViewState["AllowColumnResize"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowColumnResize"] = value;
			}
		}

		// Token: 0x17003AB3 RID: 15027
		// (get) Token: 0x0600B591 RID: 46481 RVA: 0x0027FFAC File Offset: 0x0027E1AC
		// (set) Token: 0x0600B592 RID: 46482 RVA: 0x0027FFD5 File Offset: 0x0027E1D5
		[Category("Client")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("RadGrid_AllowRowResize")]
		public virtual bool AllowRowResize
		{
			get
			{
				object obj = base.ViewState["AllowRowResize"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowRowResize"] = value;
			}
		}

		// Token: 0x17003AB4 RID: 15028
		// (get) Token: 0x0600B593 RID: 46483 RVA: 0x0027FFF0 File Offset: 0x0027E1F0
		// (set) Token: 0x0600B594 RID: 46484 RVA: 0x00280019 File Offset: 0x0027E219
		[Description("RadGrid_ShowRowIndicatorColumn")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DefaultValue(true)]
		public virtual bool ShowRowIndicatorColumn
		{
			get
			{
				object obj = base.ViewState["ShowRowIndicatorColumn"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowRowIndicatorColumn"] = value;
			}
		}

		// Token: 0x17003AB5 RID: 15029
		// (get) Token: 0x0600B595 RID: 46485 RVA: 0x00280034 File Offset: 0x0027E234
		// (set) Token: 0x0600B596 RID: 46486 RVA: 0x0028005D File Offset: 0x0027E25D
		[Category("Client")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("RadGrid_ResizeGridOnColumnResize")]
		public virtual bool ResizeGridOnColumnResize
		{
			get
			{
				object obj = base.ViewState["ResizeGridOnColumnResize"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ResizeGridOnColumnResize"] = value;
			}
		}

		// Token: 0x17003AB6 RID: 15030
		// (get) Token: 0x0600B597 RID: 46487 RVA: 0x00280078 File Offset: 0x0027E278
		// (set) Token: 0x0600B598 RID: 46488 RVA: 0x002800A1 File Offset: 0x0027E2A1
		[Description("RadGrid_ClipCellContentOnResize")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Client")]
		public virtual bool ClipCellContentOnResize
		{
			get
			{
				object obj = base.ViewState["ClipCellContentOnResize"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ClipCellContentOnResize"] = value;
			}
		}

		// Token: 0x17003AB7 RID: 15031
		// (get) Token: 0x0600B599 RID: 46489 RVA: 0x002800BC File Offset: 0x0027E2BC
		// (set) Token: 0x0600B59A RID: 46490 RVA: 0x002800E5 File Offset: 0x0027E2E5
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("RadGrid_EnableRealTimeResize")]
		[DefaultValue(false)]
		public virtual bool EnableRealTimeResize
		{
			get
			{
				object obj = base.ViewState["EnableRealTimeResize"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableRealTimeResize"] = value;
			}
		}

		// Token: 0x17003AB8 RID: 15032
		// (get) Token: 0x0600B59B RID: 46491 RVA: 0x002800FD File Offset: 0x0027E2FD
		// (set) Token: 0x0600B59C RID: 46492 RVA: 0x0028011E File Offset: 0x0027E31E
		[Description("RadGrid_AllowResizeToFit")]
		[DefaultValue(false)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual bool AllowResizeToFit
		{
			get
			{
				return (bool)(base.ViewState["AllowResizeToFit"] ?? false);
			}
			set
			{
				base.ViewState["AllowResizeToFit"] = value;
			}
		}

		// Token: 0x17003AB9 RID: 15033
		// (get) Token: 0x0600B59D RID: 46493 RVA: 0x00280136 File Offset: 0x0027E336
		// (set) Token: 0x0600B59E RID: 46494 RVA: 0x00280157 File Offset: 0x0027E357
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("RadGrid_EnableNextColumnResize")]
		[Category("Client")]
		public virtual bool EnableNextColumnResize
		{
			get
			{
				return (bool)(base.ViewState["EnableNextColumnResize"] ?? false);
			}
			set
			{
				base.ViewState["EnableNextColumnResize"] = value;
			}
		}
	}
}
