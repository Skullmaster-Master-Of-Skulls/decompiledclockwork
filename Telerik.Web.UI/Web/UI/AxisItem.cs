using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02000B9B RID: 2971
	public class AxisItem : StateManager
	{
		// Token: 0x06007033 RID: 28723 RVA: 0x001A3387 File Offset: 0x001A1587
		public AxisItem()
		{
		}

		// Token: 0x06007034 RID: 28724 RVA: 0x001A338F File Offset: 0x001A158F
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public AxisItem(string labelText)
		{
			this.LabelText = labelText;
		}

		// Token: 0x170024B4 RID: 9396
		// (get) Token: 0x06007035 RID: 28725 RVA: 0x001A339E File Offset: 0x001A159E
		// (set) Token: 0x06007036 RID: 28726 RVA: 0x001A33BE File Offset: 0x001A15BE
		public virtual string LabelText
		{
			get
			{
				return (string)(base.ViewState["LabelText"] ?? string.Empty);
			}
			set
			{
				base.ViewState["LabelText"] = value;
			}
		}
	}
}
