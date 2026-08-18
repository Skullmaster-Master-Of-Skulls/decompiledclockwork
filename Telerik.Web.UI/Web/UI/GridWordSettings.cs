using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000386 RID: 902
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridWordSettings : ObjectWithState
	{
		// Token: 0x06001EED RID: 7917 RVA: 0x00061BAB File Offset: 0x0005FDAB
		public GridWordSettings(StateBag OwnerStateBag) : base("gdocs_", OwnerStateBag)
		{
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x00061BB9 File Offset: 0x0005FDB9
		// (set) Token: 0x06001EEF RID: 7919 RVA: 0x00061BE4 File Offset: 0x0005FDE4
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(GridWordExportFormat.Html)]
		public GridWordExportFormat Format
		{
			get
			{
				if (base.ViewState["_xfm"] == null)
				{
					return GridWordExportFormat.Html;
				}
				return (GridWordExportFormat)base.ViewState["_xfm"];
			}
			set
			{
				base.ViewState["_xfm"] = value;
			}
		}
	}
}
