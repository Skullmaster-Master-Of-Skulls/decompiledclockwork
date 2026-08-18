using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ACC RID: 2764
	internal sealed class PasswordRecord : BaseBiffRecord, IRecord
	{
		// Token: 0x06006863 RID: 26723 RVA: 0x001871A6 File Offset: 0x001853A6
		public PasswordRecord() : base(19)
		{
			base.Length = 2;
			this.wPassword = 0;
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x001871C0 File Offset: 0x001853C0
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.wPassword);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x001871EC File Offset: 0x001853EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PASSWORDRECORD]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("wPassword=0x{0:x4};", this.wPassword);
			stringBuilder.Append("[/PASSWORDRECORD]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B9F RID: 7071
		private const ushort type = 19;

		// Token: 0x04001BA0 RID: 7072
		private const ushort length = 2;

		// Token: 0x04001BA1 RID: 7073
		private ushort wPassword;
	}
}
