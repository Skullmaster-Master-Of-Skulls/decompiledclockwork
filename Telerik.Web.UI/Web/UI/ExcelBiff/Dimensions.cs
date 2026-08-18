using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A82 RID: 2690
	internal sealed class Dimensions : BaseBiffRecord, IRecord
	{
		// Token: 0x0600676F RID: 26479 RVA: 0x00182B1C File Offset: 0x00180D1C
		public Dimensions(uint firstRow, uint lastRow, ushort firstColumn, ushort lastColumn) : base(512)
		{
			base.Length = 14;
			this.rwMic = firstRow;
			this.rwMac = lastRow;
			this.colMic = firstColumn;
			this.colMac = lastColumn;
			this.reserved = 0;
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x00182B58 File Offset: 0x00180D58
		public byte[] GetData()
		{
			int num = 0;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.rwMic);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.rwMac);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.colMic);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.colMac);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.reserved);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x04001A28 RID: 6696
		private const ushort type = 512;

		// Token: 0x04001A29 RID: 6697
		private const ushort length = 14;

		// Token: 0x04001A2A RID: 6698
		private uint rwMic;

		// Token: 0x04001A2B RID: 6699
		private uint rwMac;

		// Token: 0x04001A2C RID: 6700
		private ushort colMac;

		// Token: 0x04001A2D RID: 6701
		private ushort colMic;

		// Token: 0x04001A2E RID: 6702
		private ushort reserved;
	}
}
