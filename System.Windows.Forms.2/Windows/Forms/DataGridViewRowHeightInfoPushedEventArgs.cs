using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000212 RID: 530
	public class DataGridViewRowHeightInfoPushedEventArgs : HandledEventArgs
	{
		// Token: 0x060022A6 RID: 8870 RVA: 0x000A68D5 File Offset: 0x000A4AD5
		internal DataGridViewRowHeightInfoPushedEventArgs(int rowIndex, int height, int minimumHeight) : base(false)
		{
			this.rowIndex = rowIndex;
			this.height = height;
			this.minimumHeight = minimumHeight;
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x000A68F3 File Offset: 0x000A4AF3
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000A68FB File Offset: 0x000A4AFB
		public int MinimumHeight
		{
			get
			{
				return this.minimumHeight;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x000A6903 File Offset: 0x000A4B03
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000E4E RID: 3662
		private int rowIndex;

		// Token: 0x04000E4F RID: 3663
		private int height;

		// Token: 0x04000E50 RID: 3664
		private int minimumHeight;
	}
}
