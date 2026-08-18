using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ADF RID: 2783
	internal sealed class Setup : BaseBiffRecord, IRecord
	{
		// Token: 0x060068C1 RID: 26817 RVA: 0x00188A50 File Offset: 0x00186C50
		public Setup(int paperSizeIndex, bool isPortrait, double topMargin, double bottomMargin) : base(161)
		{
			base.Length = 34;
			this.iPaperSize = (ushort)paperSizeIndex;
			this.iScale = 100;
			this.iPageStart = 1;
			this.iFitWidth = 1;
			this.iFitHeight = 1;
			this.grbit = 0;
			if (isPortrait)
			{
				this.grbit = 2;
			}
			this.iRes = 0;
			this.iVRes = 0;
			this.numHdr = topMargin;
			this.numFtr = bottomMargin;
			this.iCopies = 1;
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x00188ACC File Offset: 0x00186CCC
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.iPaperSize);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iScale);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iPageStart);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iFitWidth);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iFitHeight);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iRes);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iVRes);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.numHdr);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.numFtr);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iCopies);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x00188BFC File Offset: 0x00186DFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SETUP]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("iPaperSize={0};", this.iPaperSize);
			stringBuilder.AppendFormat("iScale={0};", this.iScale);
			stringBuilder.AppendFormat("iPageStart={0};", this.iPageStart);
			stringBuilder.AppendFormat("iFitWidth={0};", this.iFitWidth);
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("iRes={0};", this.iRes);
			stringBuilder.AppendFormat("iVRes={0};", this.iVRes);
			stringBuilder.AppendFormat("numHdr={0};", this.numHdr);
			stringBuilder.AppendFormat("numFtr={0};", this.numFtr);
			stringBuilder.AppendFormat("iCopies={0};", this.iCopies);
			stringBuilder.Append("[/SETUP]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BEC RID: 7148
		private const ushort type = 161;

		// Token: 0x04001BED RID: 7149
		private const ushort length = 34;

		// Token: 0x04001BEE RID: 7150
		private ushort iPaperSize;

		// Token: 0x04001BEF RID: 7151
		private ushort iScale;

		// Token: 0x04001BF0 RID: 7152
		private ushort iPageStart;

		// Token: 0x04001BF1 RID: 7153
		private ushort iFitWidth;

		// Token: 0x04001BF2 RID: 7154
		private ushort iFitHeight;

		// Token: 0x04001BF3 RID: 7155
		private ushort grbit;

		// Token: 0x04001BF4 RID: 7156
		private ushort iRes;

		// Token: 0x04001BF5 RID: 7157
		private ushort iVRes;

		// Token: 0x04001BF6 RID: 7158
		private double numHdr;

		// Token: 0x04001BF7 RID: 7159
		private double numFtr;

		// Token: 0x04001BF8 RID: 7160
		private ushort iCopies;
	}
}
