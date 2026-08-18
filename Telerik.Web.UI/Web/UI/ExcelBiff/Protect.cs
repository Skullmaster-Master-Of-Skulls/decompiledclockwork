using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AD2 RID: 2770
	internal sealed class Protect : BaseBiffRecord, IRecord
	{
		// Token: 0x06006875 RID: 26741 RVA: 0x0018754D File Offset: 0x0018574D
		public Protect() : base(18)
		{
			base.Length = 2;
			this.fLock = 0;
		}

		// Token: 0x06006876 RID: 26742 RVA: 0x00187568 File Offset: 0x00185768
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fLock);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006877 RID: 26743 RVA: 0x00187594 File Offset: 0x00185794
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PROTECT]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fLock={0};", this.fLock);
			stringBuilder.Append("[/PROTECT]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BB1 RID: 7089
		private const ushort type = 18;

		// Token: 0x04001BB2 RID: 7090
		private const ushort length = 2;

		// Token: 0x04001BB3 RID: 7091
		private short fLock;
	}
}
