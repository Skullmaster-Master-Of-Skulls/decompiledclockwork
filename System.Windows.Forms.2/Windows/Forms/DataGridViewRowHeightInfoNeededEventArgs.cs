using System;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000211 RID: 529
	public class DataGridViewRowHeightInfoNeededEventArgs : EventArgs
	{
		// Token: 0x0600229F RID: 8863 RVA: 0x000A67BB File Offset: 0x000A49BB
		internal DataGridViewRowHeightInfoNeededEventArgs()
		{
			this.rowIndex = -1;
			this.height = -1;
			this.minimumHeight = -1;
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x000A67D8 File Offset: 0x000A49D8
		// (set) Token: 0x060022A1 RID: 8865 RVA: 0x000A67E0 File Offset: 0x000A49E0
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				if (value < this.minimumHeight)
				{
					value = this.minimumHeight;
				}
				if (value > 65536)
				{
					throw new ArgumentOutOfRangeException("Height", SR.GetString("InvalidHighBoundArgumentEx", new object[]
					{
						"Height",
						value.ToString(CultureInfo.CurrentCulture),
						65536.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.height = value;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000A6854 File Offset: 0x000A4A54
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x000A685C File Offset: 0x000A4A5C
		public int MinimumHeight
		{
			get
			{
				return this.minimumHeight;
			}
			set
			{
				if (value < 2)
				{
					throw new ArgumentOutOfRangeException("MinimumHeight", value, SR.GetString("DataGridViewBand_MinimumHeightSmallerThanOne", new object[]
					{
						2.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.height < value)
				{
					this.height = value;
				}
				this.minimumHeight = value;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x000A68B6 File Offset: 0x000A4AB6
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000A68BE File Offset: 0x000A4ABE
		internal void SetProperties(int rowIndex, int height, int minimumHeight)
		{
			this.rowIndex = rowIndex;
			this.height = height;
			this.minimumHeight = minimumHeight;
		}

		// Token: 0x04000E4B RID: 3659
		private int rowIndex;

		// Token: 0x04000E4C RID: 3660
		private int height;

		// Token: 0x04000E4D RID: 3661
		private int minimumHeight;
	}
}
