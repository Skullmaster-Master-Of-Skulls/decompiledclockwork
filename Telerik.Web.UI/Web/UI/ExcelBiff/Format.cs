using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AA7 RID: 2727
	internal sealed class Format : BaseBiffRecord, IRecord
	{
		// Token: 0x060067ED RID: 26605 RVA: 0x00184E04 File Offset: 0x00183004
		public Format(string formatString) : this(146, formatString)
		{
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x00184E12 File Offset: 0x00183012
		public Format(ushort formatIndexCode, string formatString) : base(1054)
		{
			base.Length = (ushort)(5 + formatString.Length * 2);
			this.ifmt = formatIndexCode;
			this.cch = (ushort)formatString.Length;
			this.grbit = 1;
			this.rgch = formatString;
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x00184E54 File Offset: 0x00183054
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.ifmt);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.cch);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			data[num] = this.grbit;
			num++;
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			bytes = unicodeEncoding.GetBytes(this.rgch);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x1700222F RID: 8751
		// (get) Token: 0x060067F0 RID: 26608 RVA: 0x00184EC7 File Offset: 0x001830C7
		// (set) Token: 0x060067F1 RID: 26609 RVA: 0x00184ECF File Offset: 0x001830CF
		public ushort FormatIndex
		{
			get
			{
				return this.ifmt;
			}
			set
			{
				this.ifmt = value;
			}
		}

		// Token: 0x17002230 RID: 8752
		// (get) Token: 0x060067F2 RID: 26610 RVA: 0x00184ED8 File Offset: 0x001830D8
		public string FormatString
		{
			get
			{
				return this.rgch;
			}
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x00184EE0 File Offset: 0x001830E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FORMAT]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("ifmt=0x{0:x4};", this.ifmt);
			stringBuilder.AppendFormat("cch={0};", this.cch);
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("rgch={0};", this.rgch);
			stringBuilder.Append("[/FORMAT]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001AF2 RID: 6898
		private const ushort type = 1054;

		// Token: 0x04001AF3 RID: 6899
		private const ushort fixedPartLength = 5;

		// Token: 0x04001AF4 RID: 6900
		private const ushort defaultformatIndexCode = 146;

		// Token: 0x04001AF5 RID: 6901
		private ushort ifmt;

		// Token: 0x04001AF6 RID: 6902
		private ushort cch;

		// Token: 0x04001AF7 RID: 6903
		private byte grbit;

		// Token: 0x04001AF8 RID: 6904
		private string rgch;
	}
}
