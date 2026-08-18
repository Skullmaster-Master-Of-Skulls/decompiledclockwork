using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000966 RID: 2406
	public class TreeListTableItemStyle : TableItemStyle
	{
		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x06005B9F RID: 23455 RVA: 0x0011748D File Offset: 0x0011568D
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
