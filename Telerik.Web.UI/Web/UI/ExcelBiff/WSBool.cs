using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AF4 RID: 2804
	internal sealed class WSBool : BaseBiffRecord, IRecord
	{
		// Token: 0x06006959 RID: 26969 RVA: 0x0018C5B0 File Offset: 0x0018A7B0
		public WSBool(short gutsState) : base(129)
		{
			base.Length = 2;
			this.grbit = 1025;
			if ((gutsState & 2) > 0)
			{
				this.grbit |= 64;
			}
			if ((gutsState & 8) > 0)
			{
				this.grbit |= 128;
			}
		}

		// Token: 0x0600695A RID: 26970 RVA: 0x0018C60C File Offset: 0x0018A80C
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x0600695B RID: 26971 RVA: 0x0018C638 File Offset: 0x0018A838
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[WSBOOL]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.Append("[/WSBOOL]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C6F RID: 7279
		private const ushort type = 129;

		// Token: 0x04001C70 RID: 7280
		private const ushort length = 2;

		// Token: 0x04001C71 RID: 7281
		private ushort grbit;
	}
}
