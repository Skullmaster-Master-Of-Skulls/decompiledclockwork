using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200116A RID: 4458
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridSelecting : ObjectWithState
	{
		// Token: 0x0600B5CC RID: 46540 RVA: 0x00280619 File Offset: 0x0027E819
		public GridSelecting(StateBag OwnerStateBag) : base("cs_select_", OwnerStateBag)
		{
		}

		// Token: 0x17003ACD RID: 15053
		// (get) Token: 0x0600B5CD RID: 46541 RVA: 0x00280627 File Offset: 0x0027E827
		// (set) Token: 0x0600B5CE RID: 46542 RVA: 0x00280652 File Offset: 0x0027E852
		[DefaultValue(GridCellSelectionMode.None)]
		public GridCellSelectionMode CellSelectionMode
		{
			get
			{
				if (base.ViewState["_csm"] != null)
				{
					return (GridCellSelectionMode)base.ViewState["_csm"];
				}
				return GridCellSelectionMode.None;
			}
			set
			{
				base.ViewState["_csm"] = value;
			}
		}

		// Token: 0x17003ACE RID: 15054
		// (get) Token: 0x0600B5CF RID: 46543 RVA: 0x0028066C File Offset: 0x0027E86C
		// (set) Token: 0x0600B5D0 RID: 46544 RVA: 0x00280695 File Offset: 0x0027E895
		[Description("RadGrid_AllowRowSelect")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DefaultValue(false)]
		public virtual bool AllowRowSelect
		{
			get
			{
				object obj = base.ViewState["AllowRowSelect"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value && this.CellSelectionMode != GridCellSelectionMode.None)
				{
					throw new GridException("You cannot use row selection and cell selection at once. Please, set ClientSettings.Selecting.CellSelectionMode = None to start using row selection.");
				}
				base.ViewState["AllowRowSelect"] = value;
			}
		}

		// Token: 0x17003ACF RID: 15055
		// (get) Token: 0x0600B5D1 RID: 46545 RVA: 0x002806C4 File Offset: 0x0027E8C4
		// (set) Token: 0x0600B5D2 RID: 46546 RVA: 0x002806ED File Offset: 0x0027E8ED
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("RadGrid_EnableDragToSelectRows")]
		public virtual bool EnableDragToSelectRows
		{
			get
			{
				object obj = base.ViewState["EnableDragToSelectRows"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["EnableDragToSelectRows"] = value;
			}
		}

		// Token: 0x17003AD0 RID: 15056
		// (get) Token: 0x0600B5D3 RID: 46547 RVA: 0x00280708 File Offset: 0x0027E908
		// (set) Token: 0x0600B5D4 RID: 46548 RVA: 0x00280731 File Offset: 0x0027E931
		[DefaultValue(false)]
		[Category("Client")]
		[Description("Gets or sets value indicating whether items can be only selected through GridClientSelectColumn")]
		[NotifyParentProperty(true)]
		public virtual bool UseClientSelectColumnOnly
		{
			get
			{
				object obj = base.ViewState["UseClientSelectColumnOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["UseClientSelectColumnOnly"] = value;
			}
		}

		// Token: 0x17003AD1 RID: 15057
		// (get) Token: 0x0600B5D5 RID: 46549 RVA: 0x00280749 File Offset: 0x0027E949
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsSet
		{
			get
			{
				return this.AllowRowSelect || this.CellSelectionMode != GridCellSelectionMode.None || this.UseClientSelectColumnOnly;
			}
		}
	}
}
