using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200127D RID: 4733
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListSelecting : StateManager
	{
		// Token: 0x17003FB6 RID: 16310
		// (get) Token: 0x0600C554 RID: 50516 RVA: 0x002C11B0 File Offset: 0x002BF3B0
		// (set) Token: 0x0600C555 RID: 50517 RVA: 0x002C11D9 File Offset: 0x002BF3D9
		[Description("Gets or sets whether client-side row selection is enabled.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowItemSelection
		{
			get
			{
				object obj = base.ViewState["AllowItemSelection"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowItemSelection"] = value;
			}
		}

		// Token: 0x17003FB7 RID: 16311
		// (get) Token: 0x0600C556 RID: 50518 RVA: 0x002C11F1 File Offset: 0x002BF3F1
		internal bool ShouldSerializeAllowItemSelection
		{
			get
			{
				return this.AllowItemSelection;
			}
		}

		// Token: 0x17003FB8 RID: 16312
		// (get) Token: 0x0600C557 RID: 50519 RVA: 0x002C11FC File Offset: 0x002BF3FC
		// (set) Token: 0x0600C558 RID: 50520 RVA: 0x002C1225 File Offset: 0x002BF425
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or sets whether the TreeList item could be selected only from TreeListSelectColumn.")]
		[DefaultValue(false)]
		public virtual bool UseSelectColumnOnly
		{
			get
			{
				object obj = base.ViewState["UseSelectColumnOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["UseSelectColumnOnly"] = value;
			}
		}

		// Token: 0x17003FB9 RID: 16313
		// (get) Token: 0x0600C559 RID: 50521 RVA: 0x002C123D File Offset: 0x002BF43D
		internal bool ShouldSerializeUseSelectColumnOnly
		{
			get
			{
				return this.UseSelectColumnOnly;
			}
		}

		// Token: 0x17003FBA RID: 16314
		// (get) Token: 0x0600C55A RID: 50522 RVA: 0x002C1248 File Offset: 0x002BF448
		// (set) Token: 0x0600C55B RID: 50523 RVA: 0x002C1271 File Offset: 0x002BF471
		[Description("Gets or sets a value indicating whether clicking an item in RadTreeList will toggle the item's selected state.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowToggleSelection
		{
			get
			{
				object obj = base.ViewState["AllowToggleSelection"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowToggleSelection"] = value;
			}
		}

		// Token: 0x17003FBB RID: 16315
		// (get) Token: 0x0600C55C RID: 50524 RVA: 0x002C1289 File Offset: 0x002BF489
		internal bool ShouldSerializeAllowToggleSelection
		{
			get
			{
				return this.AllowToggleSelection;
			}
		}
	}
}
