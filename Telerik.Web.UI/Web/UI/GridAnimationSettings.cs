using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000F80 RID: 3968
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridAnimationSettings : ObjectWithState
	{
		// Token: 0x06009802 RID: 38914 RVA: 0x00220A12 File Offset: 0x0021EC12
		public GridAnimationSettings(StateBag ownerStateBag) : base("cs_anim_", ownerStateBag)
		{
		}

		// Token: 0x17003012 RID: 12306
		// (get) Token: 0x06009803 RID: 38915 RVA: 0x00220A20 File Offset: 0x0021EC20
		// (set) Token: 0x06009804 RID: 38916 RVA: 0x00220A41 File Offset: 0x0021EC41
		[Category("Client")]
		[Description("Gets or sets whether column animations are enabled for RadGrid when column reorder is enabled.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool AllowColumnReorderAnimation
		{
			get
			{
				return (bool)(base.ViewState["AllowColumnReorderAnimation"] ?? false);
			}
			set
			{
				base.ViewState["AllowColumnReorderAnimation"] = value;
			}
		}

		// Token: 0x17003013 RID: 12307
		// (get) Token: 0x06009805 RID: 38917 RVA: 0x00220A59 File Offset: 0x0021EC59
		// (set) Token: 0x06009806 RID: 38918 RVA: 0x00220A7E File Offset: 0x0021EC7E
		[Description("Gets or sets the duration of the reorder animation when column reorder is enabled in RadGrid.")]
		[DefaultValue(300)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual int ColumnReorderAnimationDuration
		{
			get
			{
				return (int)(base.ViewState["ColumnReorderAnimationDuration"] ?? 300);
			}
			set
			{
				base.ViewState["ColumnReorderAnimationDuration"] = value;
			}
		}

		// Token: 0x17003014 RID: 12308
		// (get) Token: 0x06009807 RID: 38919 RVA: 0x00220A96 File Offset: 0x0021EC96
		// (set) Token: 0x06009808 RID: 38920 RVA: 0x00220AB7 File Offset: 0x0021ECB7
		[Description("Gets or sets whether revert animations are enabled for RadGrid when column drag-to-group is enabled.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowColumnRevertAnimation
		{
			get
			{
				return (bool)(base.ViewState["AllowColumnRevertAnimation"] ?? false);
			}
			set
			{
				base.ViewState["AllowColumnRevertAnimation"] = value;
			}
		}

		// Token: 0x17003015 RID: 12309
		// (get) Token: 0x06009809 RID: 38921 RVA: 0x00220ACF File Offset: 0x0021ECCF
		// (set) Token: 0x0600980A RID: 38922 RVA: 0x00220AF4 File Offset: 0x0021ECF4
		[DefaultValue(300)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or sets the duration of the revert animation when column drag-to-group is enabled in RadGrid.")]
		public virtual int ColumnRevertAnimationDuration
		{
			get
			{
				return (int)(base.ViewState["ColumnRevertAnimationDuration"] ?? 300);
			}
			set
			{
				base.ViewState["ColumnRevertAnimationDuration"] = value;
			}
		}

		// Token: 0x17003016 RID: 12310
		// (get) Token: 0x0600980B RID: 38923 RVA: 0x00220B0C File Offset: 0x0021ED0C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsSet
		{
			get
			{
				return this.AllowColumnReorderAnimation || this.AllowColumnRevertAnimation;
			}
		}
	}
}
