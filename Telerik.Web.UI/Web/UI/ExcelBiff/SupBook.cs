using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AE8 RID: 2792
	internal sealed class SupBook : BaseBiffRecord, IRecord
	{
		// Token: 0x060068EA RID: 26858 RVA: 0x00189948 File Offset: 0x00187B48
		public SupBook(ushort sheetCount) : base(430)
		{
			base.Length = 4;
			this.cTab = sheetCount;
			this.ownWorkBook = new byte[]
			{
				1,
				4
			};
		}

		// Token: 0x060068EB RID: 26859 RVA: 0x00189984 File Offset: 0x00187B84
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.cTab);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			this.ownWorkBook.CopyTo(data, num);
			return data;
		}

		// Token: 0x060068EC RID: 26860 RVA: 0x001899C4 File Offset: 0x00187BC4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SUPBOOK]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("cTab={0};", this.cTab);
			stringBuilder.AppendFormat("ownWorkBook[0]=0x{0:x4};", this.ownWorkBook[0]);
			stringBuilder.AppendFormat("ownWorkBook[1]=0x{0:x4};", this.ownWorkBook[1]);
			stringBuilder.Append("[/FONT]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C19 RID: 7193
		private const ushort type = 430;

		// Token: 0x04001C1A RID: 7194
		private const ushort length = 4;

		// Token: 0x04001C1B RID: 7195
		private ushort cTab;

		// Token: 0x04001C1C RID: 7196
		private byte[] ownWorkBook;
	}
}
