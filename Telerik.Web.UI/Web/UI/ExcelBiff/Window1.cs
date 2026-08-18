using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AED RID: 2797
	internal sealed class Window1 : BaseBiffRecord, IRecord
	{
		// Token: 0x060068F6 RID: 26870 RVA: 0x00189C5C File Offset: 0x00187E5C
		public Window1() : base(61)
		{
			base.Length = 18;
			this.xWn = 240;
			this.yWn = 75;
			this.dxWn = 17115;
			this.dyWn = 12780;
			this.grbit = 56;
			this.itabCur = 0;
			this.itabFirst = 0;
			this.ctabSel = 1;
			this.wTabRatio = 600;
		}

		// Token: 0x060068F7 RID: 26871 RVA: 0x00189CCC File Offset: 0x00187ECC
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.xWn);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.yWn);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.dxWn);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.dyWn);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.itabCur);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.itabFirst);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.ctabSel);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.wTabRatio);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x00189DC8 File Offset: 0x00187FC8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[WINDOW1]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("xWn={0};", this.xWn);
			stringBuilder.AppendFormat("yWn={0};", this.yWn);
			stringBuilder.AppendFormat("dxWn={0};", this.dxWn);
			stringBuilder.AppendFormat("dyWn={0};", this.dyWn);
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("itabCur={0};", this.itabCur);
			stringBuilder.AppendFormat("itabFirst={0};", this.itabFirst);
			stringBuilder.AppendFormat("ctabSel={0};", this.ctabSel);
			stringBuilder.AppendFormat("wTabRatio={0};", this.wTabRatio);
			stringBuilder.Append("[/WINDOW1]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C2C RID: 7212
		private const ushort type = 61;

		// Token: 0x04001C2D RID: 7213
		private const ushort length = 18;

		// Token: 0x04001C2E RID: 7214
		private ushort xWn;

		// Token: 0x04001C2F RID: 7215
		private ushort yWn;

		// Token: 0x04001C30 RID: 7216
		private ushort dxWn;

		// Token: 0x04001C31 RID: 7217
		private ushort dyWn;

		// Token: 0x04001C32 RID: 7218
		private ushort grbit;

		// Token: 0x04001C33 RID: 7219
		private ushort itabCur;

		// Token: 0x04001C34 RID: 7220
		private ushort itabFirst;

		// Token: 0x04001C35 RID: 7221
		private ushort ctabSel;

		// Token: 0x04001C36 RID: 7222
		private ushort wTabRatio;
	}
}
