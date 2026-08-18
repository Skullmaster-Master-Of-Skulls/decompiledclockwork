using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ABE RID: 2750
	internal sealed class MulBlank : BaseBiffRecord, IRecord
	{
		// Token: 0x06006836 RID: 26678 RVA: 0x00186340 File Offset: 0x00184540
		public MulBlank(ushort row, ushort colFirst, ushort colLast, List<BlankCell> blankCells) : base(190)
		{
			this.row = row;
			this.colFirst = colFirst;
			this.colLast = colLast;
			this.blankCells = blankCells;
			base.Length = (ushort)(6 + blankCells.Count * 2);
		}

		// Token: 0x06006837 RID: 26679 RVA: 0x0018637C File Offset: 0x0018457C
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.row);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.colFirst);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			for (int i = 0; i < this.blankCells.Count; i++)
			{
				bytes = BitConverter.GetBytes((ushort)this.blankCells[i].XFIndex);
				bytes.CopyTo(data, num);
				num += bytes.Length;
			}
			bytes = BitConverter.GetBytes(this.colLast);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x04001B57 RID: 6999
		private const ushort type = 190;

		// Token: 0x04001B58 RID: 7000
		private const ushort fixedPartLength = 6;

		// Token: 0x04001B59 RID: 7001
		private List<BlankCell> blankCells;

		// Token: 0x04001B5A RID: 7002
		private ushort colFirst;

		// Token: 0x04001B5B RID: 7003
		private ushort colLast;

		// Token: 0x04001B5C RID: 7004
		private ushort row;
	}
}
