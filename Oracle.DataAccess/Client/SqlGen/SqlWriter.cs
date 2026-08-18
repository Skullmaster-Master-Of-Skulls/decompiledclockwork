using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x02000017 RID: 23
	internal class SqlWriter : StringWriter
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000F3FF File Offset: 0x0000E3FF
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000F407 File Offset: 0x0000E407
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

		// Token: 0x060000BF RID: 191 RVA: 0x0000F410 File Offset: 0x0000E410
		public SqlWriter(StringBuilder b) : base(b, CultureInfo.InvariantCulture)
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000F42C File Offset: 0x0000E42C
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

		// Token: 0x060000C1 RID: 193 RVA: 0x0000F486 File Offset: 0x0000E486
		public override void WriteLine()
		{
			base.WriteLine();
			this.atBeginningOfLine = true;
		}

		// Token: 0x040000A3 RID: 163
		private int indent = -1;

		// Token: 0x040000A4 RID: 164
		private bool atBeginningOfLine = true;
	}
}
