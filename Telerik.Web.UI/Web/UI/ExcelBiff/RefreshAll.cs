using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AD9 RID: 2777
	internal sealed class RefreshAll : BaseBiffRecord, IRecord
	{
		// Token: 0x060068AA RID: 26794 RVA: 0x00188401 File Offset: 0x00186601
		public RefreshAll() : base(439)
		{
			base.Length = 2;
			this.fRefreshAll = 0;
		}

		// Token: 0x060068AB RID: 26795 RVA: 0x0018841C File Offset: 0x0018661C
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fRefreshAll);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060068AC RID: 26796 RVA: 0x00188448 File Offset: 0x00186648
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[REFRESHALL]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fRefreshAll={0};", this.fRefreshAll);
			stringBuilder.Append("[/REFRESHALL]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BCC RID: 7116
		private const ushort type = 439;

		// Token: 0x04001BCD RID: 7117
		private const ushort length = 2;

		// Token: 0x04001BCE RID: 7118
		private ushort fRefreshAll;
	}
}
