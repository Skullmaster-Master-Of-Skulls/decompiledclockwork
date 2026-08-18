using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AB7 RID: 2743
	internal sealed class LabelSST : BaseBiffRecord, IRecord
	{
		// Token: 0x0600681B RID: 26651 RVA: 0x00185A97 File Offset: 0x00183C97
		public LabelSST(ushort row, ushort column, ushort xFIndex, uint sSTIndex) : base(253)
		{
			base.Length = 10;
			this.rw = row;
			this.col = column;
			this.ixfe = xFIndex;
			this.isst = sSTIndex;
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x00185ACC File Offset: 0x00183CCC
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
			bytes = BitConverter.GetBytes(this.isst);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x04001B38 RID: 6968
		public const ushort Type = 253;

		// Token: 0x04001B39 RID: 6969
		private const ushort length = 10;

		// Token: 0x04001B3A RID: 6970
		private ushort rw;

		// Token: 0x04001B3B RID: 6971
		private ushort col;

		// Token: 0x04001B3C RID: 6972
		private uint isst;

		// Token: 0x04001B3D RID: 6973
		private ushort ixfe;
	}
}
