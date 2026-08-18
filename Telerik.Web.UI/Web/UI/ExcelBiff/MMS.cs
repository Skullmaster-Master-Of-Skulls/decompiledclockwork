using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ABB RID: 2747
	internal sealed class MMS : BaseBiffRecord, IRecord
	{
		// Token: 0x0600682B RID: 26667 RVA: 0x00185D83 File Offset: 0x00183F83
		public MMS() : base(193)
		{
			base.Length = 2;
			this.caitm = 0;
			this.cditm = 0;
		}

		// Token: 0x0600682C RID: 26668 RVA: 0x00185DA8 File Offset: 0x00183FA8
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			data[num] = this.caitm;
			num++;
			data[num] = this.cditm;
			return data;
		}

		// Token: 0x0600682D RID: 26669 RVA: 0x00185DD8 File Offset: 0x00183FD8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[MMS]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("caitm={0};", this.caitm);
			stringBuilder.AppendFormat("cditm={0};", this.cditm);
			stringBuilder.Append("[/MMS]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B4D RID: 6989
		private const ushort type = 193;

		// Token: 0x04001B4E RID: 6990
		private const ushort length = 2;

		// Token: 0x04001B4F RID: 6991
		private byte caitm;

		// Token: 0x04001B50 RID: 6992
		private byte cditm;
	}
}
