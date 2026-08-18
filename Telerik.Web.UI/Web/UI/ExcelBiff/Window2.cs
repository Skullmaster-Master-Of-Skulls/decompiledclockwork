using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AEE RID: 2798
	internal sealed class Window2 : BaseBiffRecord, IRecord
	{
		// Token: 0x060068F9 RID: 26873 RVA: 0x00189ED8 File Offset: 0x001880D8
		public Window2() : base(574)
		{
			base.Length = 18;
			this.grbit = 182;
			this.rwTop = 0;
			this.colLeft = 0;
			this.icvHdr = 64U;
			this.wScaleSLV = 0;
			this.wScaleNormal = 0;
			this.reserved = 0U;
		}

		// Token: 0x060068FA RID: 26874 RVA: 0x00189F2E File Offset: 0x0018812E
		public void DisplaySelectedSheet()
		{
			this.grbit |= 1536;
		}

		// Token: 0x060068FB RID: 26875 RVA: 0x00189F43 File Offset: 0x00188143
		public void FreezePane()
		{
			this.grbit |= 8;
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x00189F54 File Offset: 0x00188154
		public void TurnOffGridLines()
		{
			this.grbit = (ushort)((int)this.grbit & -3);
		}

		// Token: 0x060068FD RID: 26877 RVA: 0x00189F66 File Offset: 0x00188166
		public void TurnOnGridLines()
		{
			this.grbit |= 2;
		}

		// Token: 0x060068FE RID: 26878 RVA: 0x00189F78 File Offset: 0x00188178
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.rwTop);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.colLeft);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.icvHdr);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.wScaleSLV);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.wScaleNormal);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.reserved);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x060068FF RID: 26879 RVA: 0x0018A040 File Offset: 0x00188240
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[WINDOW2]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("rwTop={0};", this.rwTop);
			stringBuilder.AppendFormat("colLeft={0};", this.colLeft);
			stringBuilder.AppendFormat("icvHdr={0};", this.icvHdr);
			stringBuilder.AppendFormat("wScaleSLV={0};", this.wScaleSLV);
			stringBuilder.AppendFormat("wScaleNormal={0};", this.wScaleNormal);
			stringBuilder.AppendFormat("reserved={0};", this.reserved);
			stringBuilder.Append("[/WINDOW2]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C37 RID: 7223
		private const ushort type = 574;

		// Token: 0x04001C38 RID: 7224
		private const ushort length = 18;

		// Token: 0x04001C39 RID: 7225
		private const ushort DisplaySelectedWorkSheetBits = 1536;

		// Token: 0x04001C3A RID: 7226
		private const ushort FreezePaneBit = 8;

		// Token: 0x04001C3B RID: 7227
		private const ushort GridBitOn = 2;

		// Token: 0x04001C3C RID: 7228
		private ushort grbit;

		// Token: 0x04001C3D RID: 7229
		private ushort rwTop;

		// Token: 0x04001C3E RID: 7230
		private ushort colLeft;

		// Token: 0x04001C3F RID: 7231
		private uint icvHdr;

		// Token: 0x04001C40 RID: 7232
		private ushort wScaleSLV;

		// Token: 0x04001C41 RID: 7233
		private ushort wScaleNormal;

		// Token: 0x04001C42 RID: 7234
		private uint reserved;
	}
}
