using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DE1 RID: 3553
	public class PivotGridTableItemStyle : TableItemStyle
	{
		// Token: 0x1700299D RID: 10653
		// (get) Token: 0x060083CF RID: 33743 RVA: 0x001E0B0C File Offset: 0x001DED0C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsDefault
		{
			get
			{
				return this.IsEmpty;
			}
		}
	}
}
