using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AF3 RID: 2803
	internal sealed class WriteAccess : BaseBiffRecord, IRecord
	{
		// Token: 0x06006956 RID: 26966 RVA: 0x0018C46D File Offset: 0x0018A66D
		public WriteAccess() : base(92)
		{
			base.Length = 112;
			this.name = " ";
			this.cch = (ushort)this.name.Length;
			this.grbit = 1;
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x0018C4A4 File Offset: 0x0018A6A4
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.cch);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			data[num] = this.grbit;
			num++;
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			bytes = unicodeEncoding.GetBytes(this.name);
			bytes.CopyTo(data, num);
			num += this.name.Length * 2;
			for (int i = num; i < (int)(4 + base.Length); i++)
			{
				data[i] = Convert.ToByte(' ');
			}
			return data;
		}

		// Token: 0x06006958 RID: 26968 RVA: 0x0018C530 File Offset: 0x0018A730
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[WRITEACCESS]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("cch={0};", this.cch);
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("name={0};", this.name);
			stringBuilder.Append("[/WRITEACCESS]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C6A RID: 7274
		private const ushort type = 92;

		// Token: 0x04001C6B RID: 7275
		private const ushort length = 112;

		// Token: 0x04001C6C RID: 7276
		private ushort cch;

		// Token: 0x04001C6D RID: 7277
		private byte grbit;

		// Token: 0x04001C6E RID: 7278
		private string name;
	}
}
