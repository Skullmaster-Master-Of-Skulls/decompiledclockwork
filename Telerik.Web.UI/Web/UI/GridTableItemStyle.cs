using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001159 RID: 4441
	public class GridTableItemStyle : TableItemStyle
	{
		// Token: 0x17003A54 RID: 14932
		// (get) Token: 0x0600B4AE RID: 46254 RVA: 0x0027CB3E File Offset: 0x0027AD3E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual bool IsDefault
		{
			get
			{
				return this.IsEmpty;
			}
		}
	}
}
