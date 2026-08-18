using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000034 RID: 52
	internal class SqlWriter : StringWriter
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00016E8C File Offset: 0x0001508C
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00016E94 File Offset: 0x00015094
		internal int Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00016E9D File Offset: 0x0001509D
		public SqlWriter(StringBuilder b) : base(b, CultureInfo.InvariantCulture)
		{
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00016EBC File Offset: 0x000150BC
		public override void Write(string value)
		{
			if (value == "\r\n")
			{
				base.WriteLine();
				this.atBeginningOfLine = true;
				return;
			}
			if (this.atBeginningOfLine)
			{
				if (this.indent > 0)
				{
					base.Write(new string('\t', this.indent));
				}
				this.atBeginningOfLine = false;
			}
			base.Write(value);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016F16 File Offset: 0x00015116
		public override void WriteLine()
		{
			base.WriteLine();
			this.atBeginningOfLine = true;
		}

		// Token: 0x0400072E RID: 1838
		private int indent = -1;

		// Token: 0x0400072F RID: 1839
		private bool atBeginningOfLine = true;
	}
}
