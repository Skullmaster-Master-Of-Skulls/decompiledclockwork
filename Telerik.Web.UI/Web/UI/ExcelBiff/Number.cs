using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AC2 RID: 2754
	internal sealed class Number : BaseBiffRecord, IRecord
	{
		// Token: 0x06006842 RID: 26690 RVA: 0x0018698D File Offset: 0x00184B8D
		public Number(ushort row, ushort column, ushort xFIndex, double dValue) : base(515)
		{
			base.Length = 14;
			this.rw = row;
			this.col = column;
			this.ixfe = xFIndex;
			this.num = dValue;
		}

		// Token: 0x06006843 RID: 26691 RVA: 0x001869C0 File Offset: 0x00184BC0
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.rw);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.col);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.ixfe);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.num);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x04001B76 RID: 7030
		public const ushort Type = 515;

		// Token: 0x04001B77 RID: 7031
		private const ushort length = 14;

		// Token: 0x04001B78 RID: 7032
		private ushort rw;

		// Token: 0x04001B79 RID: 7033
		private ushort col;

		// Token: 0x04001B7A RID: 7034
		private ushort ixfe;

		// Token: 0x04001B7B RID: 7035
		private double num;
	}
}
