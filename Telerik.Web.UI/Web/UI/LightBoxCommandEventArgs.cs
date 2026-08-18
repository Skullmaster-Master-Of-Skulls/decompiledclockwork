using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000565 RID: 1381
	public class LightBoxCommandEventArgs : CommandEventArgs
	{
		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060031C9 RID: 12745 RVA: 0x000A34E5 File Offset: 0x000A16E5
		// (set) Token: 0x060031CA RID: 12746 RVA: 0x000A34ED File Offset: 0x000A16ED
		public virtual int ItemIndex { get; set; }

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x000A34F6 File Offset: 0x000A16F6
		// (set) Token: 0x060031CC RID: 12748 RVA: 0x000A34FE File Offset: 0x000A16FE
		public virtual bool Canceled { get; set; }

		// Token: 0x060031CD RID: 12749 RVA: 0x000A3507 File Offset: 0x000A1707
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal LightBoxCommandEventArgs(int itemIndex, object eventSource, string name, object argument) : base(name, argument)
		{
			this.ItemIndex = itemIndex;
		}
	}
}
