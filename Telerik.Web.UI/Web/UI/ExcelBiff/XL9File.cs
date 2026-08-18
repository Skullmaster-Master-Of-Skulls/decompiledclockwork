using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AF6 RID: 2806
	internal sealed class XL9File : BaseBiffRecord, IRecord
	{
		// Token: 0x0600697C RID: 27004 RVA: 0x0018CEC3 File Offset: 0x0018B0C3
		public XL9File() : base(448)
		{
			base.Length = 0;
		}

		// Token: 0x0600697D RID: 27005 RVA: 0x0018CED8 File Offset: 0x0018B0D8
		public byte[] GetData()
		{
			int num;
			return base.GetData(out num);
		}

		// Token: 0x0600697E RID: 27006 RVA: 0x0018CEF0 File Offset: 0x0018B0F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[XL9FILE]");
			stringBuilder.Append(base.ToString());
			stringBuilder.Append("[/XL9FILE]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C7F RID: 7295
		private const ushort type = 448;

		// Token: 0x04001C80 RID: 7296
		private const ushort length = 0;
	}
}
