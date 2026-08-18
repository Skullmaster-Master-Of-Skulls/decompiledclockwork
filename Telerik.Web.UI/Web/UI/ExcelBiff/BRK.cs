using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A73 RID: 2675
	internal struct BRK : IComparable<BRK>
	{
		// Token: 0x06006706 RID: 26374 RVA: 0x00181BF9 File Offset: 0x0017FDF9
		public BRK(ushort row, ushort startCol, ushort endCol)
		{
			this.row = row;
			this.startCol = startCol;
			this.endCol = endCol;
		}

		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x06006707 RID: 26375 RVA: 0x00181C10 File Offset: 0x0017FE10
		public int Row
		{
			get
			{
				return (int)this.row;
			}
		}

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x06006708 RID: 26376 RVA: 0x00181C18 File Offset: 0x0017FE18
		public int StartCol
		{
			get
			{
				return (int)this.startCol;
			}
		}

		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x06006709 RID: 26377 RVA: 0x00181C20 File Offset: 0x0017FE20
		public int EndCol
		{
			get
			{
				return (int)this.endCol;
			}
		}

		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x0600670A RID: 26378 RVA: 0x00181C28 File Offset: 0x0017FE28
		public byte[] Data
		{
			get
			{
				int num = 0;
				byte[] array = new byte[6];
				byte[] bytes = BitConverter.GetBytes(this.row);
				bytes.CopyTo(array, num);
				num += bytes.Length;
				bytes = BitConverter.GetBytes(this.startCol);
				bytes.CopyTo(array, num);
				num += bytes.Length;
				bytes = BitConverter.GetBytes(this.endCol);
				bytes.CopyTo(array, num);
				return array;
			}
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x00181C88 File Offset: 0x0017FE88
		public int CompareTo(BRK other)
		{
			if (this.Row != other.Row)
			{
				return this.Row - other.Row;
			}
			if (this.StartCol != other.StartCol)
			{
				return this.StartCol - other.StartCol;
			}
			return this.EndCol - other.EndCol;
		}

		// Token: 0x040019DE RID: 6622
		private ushort row;

		// Token: 0x040019DF RID: 6623
		private ushort startCol;

		// Token: 0x040019E0 RID: 6624
		private ushort endCol;
	}
}
