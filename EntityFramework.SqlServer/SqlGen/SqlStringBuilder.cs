using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000021 RID: 33
	internal class SqlStringBuilder
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00007A90 File Offset: 0x00005C90
		public SqlStringBuilder()
		{
			this._sql = new StringBuilder();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007AA3 File Offset: 0x00005CA3
		public SqlStringBuilder(int capacity)
		{
			this._sql = new StringBuilder(capacity);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007AB7 File Offset: 0x00005CB7
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00007ABF File Offset: 0x00005CBF
		public bool UpperCaseKeywords { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00007AC8 File Offset: 0x00005CC8
		internal StringBuilder InnerBuilder
		{
			get
			{
				return this._sql;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007AD0 File Offset: 0x00005CD0
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Keywords are known safe for lowercasing")]
		public SqlStringBuilder AppendKeyword(string keyword)
		{
			this._sql.Append(this.UpperCaseKeywords ? keyword.ToUpperInvariant() : keyword.ToLowerInvariant());
			return this;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007AF5 File Offset: 0x00005CF5
		public SqlStringBuilder AppendLine()
		{
			this._sql.AppendLine();
			return this;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007B04 File Offset: 0x00005D04
		public SqlStringBuilder AppendLine(string s)
		{
			this._sql.AppendLine(s);
			return this;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007B14 File Offset: 0x00005D14
		public SqlStringBuilder Append(string s)
		{
			this._sql.Append(s);
			return this;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007B24 File Offset: 0x00005D24
		public int Length
		{
			get
			{
				return this._sql.Length;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007B31 File Offset: 0x00005D31
		public override string ToString()
		{
			return this._sql.ToString();
		}

		// Token: 0x0400006E RID: 110
		private readonly StringBuilder _sql;
	}
}
