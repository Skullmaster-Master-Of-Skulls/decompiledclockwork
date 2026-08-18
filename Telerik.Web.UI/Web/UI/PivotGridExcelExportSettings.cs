using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000746 RID: 1862
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridExcelExportSettings : StateManager
	{
		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x000CF49A File Offset: 0x000CD69A
		// (set) Token: 0x0600420D RID: 16909 RVA: 0x000CF4C5 File Offset: 0x000CD6C5
		[Description("Set Excel export format")]
		[DefaultValue(PivotGridExcelFormat.Biff)]
		[Category("Data")]
		public PivotGridExcelFormat Format
		{
			get
			{
				if (base.ViewState["Format"] == null)
				{
					return PivotGridExcelFormat.Biff;
				}
				return (PivotGridExcelFormat)base.ViewState["Format"];
			}
			set
			{
				base.ViewState["Format"] = value;
			}
		}
	}
}
