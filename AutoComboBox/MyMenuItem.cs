using System;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000043 RID: 67
	public class MyMenuItem : MenuItem
	{
		// Token: 0x06000272 RID: 626 RVA: 0x00014895 File Offset: 0x00013895
		public MyMenuItem()
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000148A0 File Offset: 0x000138A0
		public MyMenuItem(string text, EventHandler onClick) : base(text, onClick)
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000148AD File Offset: 0x000138AD
		public MyMenuItem(string text, EventHandler onClick, object _Tag) : base(text, onClick)
		{
			this.tag = _Tag;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000148C4 File Offset: 0x000138C4
		// (set) Token: 0x06000276 RID: 630 RVA: 0x000148DC File Offset: 0x000138DC
		public new object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x040001FD RID: 509
		private object tag;
	}
}
