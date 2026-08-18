using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000F5 RID: 245
	internal class SqlWriter : StringWriter
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0007534C File Offset: 0x0007354C
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00075354 File Offset: 0x00073554
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

		// Token: 0x06000A56 RID: 2646 RVA: 0x00075360 File Offset: 0x00073560
		public SqlWriter(StringBuilder b) : base(b, CultureInfo.InvariantCulture)
		{
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0007537C File Offset: 0x0007357C
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

		// Token: 0x06000A58 RID: 2648 RVA: 0x000753D8 File Offset: 0x000735D8
		public override void WriteLine()
		{
			base.WriteLine();
			this.atBeginningOfLine = true;
		}

		// Token: 0x04000C7F RID: 3199
		private int indent = -1;

		// Token: 0x04000C80 RID: 3200
		private bool atBeginningOfLine = true;
	}
}
