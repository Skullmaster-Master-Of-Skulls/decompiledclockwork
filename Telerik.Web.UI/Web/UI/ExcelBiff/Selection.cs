using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ADE RID: 2782
	internal sealed class Selection : BaseBiffRecord, IRecord
	{
		// Token: 0x060068BE RID: 26814 RVA: 0x0018889C File Offset: 0x00186A9C
		public Selection() : base(29)
		{
			base.Length = 15;
			this.pnn = 3;
			this.rwAct = 0;
			this.colAct = 0;
			this.irefAct = 0;
			this.cref = 1;
			this.rgref = new byte[6];
		}

		// Token: 0x060068BF RID: 26815 RVA: 0x001888E8 File Offset: 0x00186AE8
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			data[num] = this.pnn;
			num++;
			byte[] bytes = BitConverter.GetBytes(this.rwAct);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.colAct);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.irefAct);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.cref);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			this.rgref.CopyTo(data, num);
			return data;
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x00188984 File Offset: 0x00186B84
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SELECTION]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("pnn={0};", this.pnn);
			stringBuilder.AppendFormat("rwAct={0};", this.rwAct);
			stringBuilder.AppendFormat("colAct={0};", this.colAct);
			stringBuilder.AppendFormat("irefAct={0};", this.irefAct);
			stringBuilder.AppendFormat("cref={0};", this.cref);
			stringBuilder.AppendFormat("rgref.Length={0};", this.rgref.Length);
			stringBuilder.Append("[/SELECTION]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BE4 RID: 7140
		private const ushort type = 29;

		// Token: 0x04001BE5 RID: 7141
		private const ushort length = 15;

		// Token: 0x04001BE6 RID: 7142
		private byte pnn;

		// Token: 0x04001BE7 RID: 7143
		private ushort rwAct;

		// Token: 0x04001BE8 RID: 7144
		private ushort colAct;

		// Token: 0x04001BE9 RID: 7145
		private ushort irefAct;

		// Token: 0x04001BEA RID: 7146
		private ushort cref;

		// Token: 0x04001BEB RID: 7147
		private byte[] rgref;
	}
}
