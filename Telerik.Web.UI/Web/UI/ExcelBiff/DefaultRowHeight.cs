using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A7F RID: 2687
	internal sealed class DefaultRowHeight : BaseBiffRecord, IRecord
	{
		// Token: 0x06006766 RID: 26470 RVA: 0x00182915 File Offset: 0x00180B15
		public DefaultRowHeight() : base(549)
		{
			base.Length = 4;
			this.grbit = 0;
			this.miyRw = 255;
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x0018293C File Offset: 0x00180B3C
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.miyRw);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x00182984 File Offset: 0x00180B84
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DEFAULTROWHEIGHT]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("miyRw={0};", this.miyRw);
			stringBuilder.Append("[/DEFAULTROWHEIGHT]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001A1E RID: 6686
		private const ushort type = 549;

		// Token: 0x04001A1F RID: 6687
		private const ushort length = 4;

		// Token: 0x04001A20 RID: 6688
		private ushort grbit;

		// Token: 0x04001A21 RID: 6689
		private ushort miyRw;
	}
}
