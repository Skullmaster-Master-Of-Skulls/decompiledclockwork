using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AAC RID: 2732
	internal sealed class HeaderFooter : BaseBiffRecord, IRecord
	{
		// Token: 0x06006800 RID: 26624 RVA: 0x0018529C File Offset: 0x0018349C
		public HeaderFooter(string biffString, ushort recordType) : base(recordType)
		{
			this.unicodeByte = 1;
			if (biffString != null)
			{
				this.rgch = biffString;
				this.cch = (ushort)this.rgch.Length;
				base.Length = this.cch * 2 + 3;
				return;
			}
			base.Length = 0;
			this.cch = 0;
		}

		// Token: 0x06006801 RID: 26625 RVA: 0x001852F4 File Offset: 0x001834F4
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			if (this.rgch != null)
			{
				byte[] bytes = BitConverter.GetBytes(this.cch);
				bytes.CopyTo(data, num);
				num += bytes.Length;
				data[num] = this.unicodeByte;
				num++;
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				bytes = unicodeEncoding.GetBytes(this.rgch);
				bytes.CopyTo(data, num);
			}
			return data;
		}

		// Token: 0x06006802 RID: 26626 RVA: 0x00185358 File Offset: 0x00183558
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[HEADER-FOOTER]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("RecordType=0x{0:x4};", base.RecordType);
			stringBuilder.AppendFormat("cch={0};", this.cch);
			stringBuilder.AppendFormat("unicodeByte={0};", this.unicodeByte);
			stringBuilder.AppendFormat("rgch={0};", this.rgch);
			stringBuilder.Append("[/HEADER-FOOTER]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B08 RID: 6920
		public const ushort HeaderType = 20;

		// Token: 0x04001B09 RID: 6921
		public const ushort FooterType = 21;

		// Token: 0x04001B0A RID: 6922
		private ushort cch;

		// Token: 0x04001B0B RID: 6923
		private string rgch;

		// Token: 0x04001B0C RID: 6924
		private byte unicodeByte;
	}
}
