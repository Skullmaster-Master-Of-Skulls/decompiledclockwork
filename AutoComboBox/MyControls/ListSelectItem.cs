using System;

namespace AutoComboBox.MyControls
{
	// Token: 0x020000B6 RID: 182
	public class ListSelectItem
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x00036E0B File Offset: 0x00035E0B
		public ListSelectItem(string caption, int controlid)
		{
			this.text = caption;
			this.controlId = controlid;
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00036E24 File Offset: 0x00035E24
		public int ControlId
		{
			get
			{
				return this.controlId;
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00036E3C File Offset: 0x00035E3C
		public override string ToString()
		{
			return this.text;
		}

		// Token: 0x04000562 RID: 1378
		private string text;

		// Token: 0x04000563 RID: 1379
		private int controlId;
	}
}
