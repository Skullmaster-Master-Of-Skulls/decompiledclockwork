using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClockWorkAPI
{
	// Token: 0x02000091 RID: 145
	public class AccessibleProfessionalColours : ProfessionalColorTable
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00029158 File Offset: 0x00028158
		public override Color MenuItemSelected
		{
			get
			{
				return Color.White;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00029170 File Offset: 0x00028170
		public override Color ButtonSelectedHighlight
		{
			get
			{
				return Color.White;
			}
		}
	}
}
