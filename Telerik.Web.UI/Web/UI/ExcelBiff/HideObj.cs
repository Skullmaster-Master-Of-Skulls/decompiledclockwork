using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AAD RID: 2733
	internal sealed class HideObj : BaseBiffRecord, IRecord
	{
		// Token: 0x06006803 RID: 26627 RVA: 0x001853ED File Offset: 0x001835ED
		public HideObj() : base(141)
		{
			base.Length = 2;
			this.fHideObj = 0;
		}

		// Token: 0x06006804 RID: 26628 RVA: 0x00185408 File Offset: 0x00183608
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fHideObj);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006805 RID: 26629 RVA: 0x00185434 File Offset: 0x00183634
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[HIDEOBJ]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fHideObj={0};", this.fHideObj);
			stringBuilder.Append("[/HIDEOBJ]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B0D RID: 6925
		private const ushort type = 141;

		// Token: 0x04001B0E RID: 6926
		private const ushort length = 2;

		// Token: 0x04001B0F RID: 6927
		private ushort fHideObj;
	}
}
