using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AC1 RID: 2753
	internal sealed class NineteenFourRecord : BaseBiffRecord, IRecord
	{
		// Token: 0x0600683F RID: 26687 RVA: 0x001868F1 File Offset: 0x00184AF1
		public NineteenFourRecord() : base(34)
		{
			base.Length = 2;
			this.f1904 = 0;
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x0018690C File Offset: 0x00184B0C
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.f1904);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x00186938 File Offset: 0x00184B38
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[1904]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("wPassword={0};", this.f1904);
			stringBuilder.Append("[/1904]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B73 RID: 7027
		private const ushort type = 34;

		// Token: 0x04001B74 RID: 7028
		private const ushort length = 2;

		// Token: 0x04001B75 RID: 7029
		private ushort f1904;
	}
}
