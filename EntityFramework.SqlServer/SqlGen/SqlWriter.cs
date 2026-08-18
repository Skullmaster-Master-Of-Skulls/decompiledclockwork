using System;
using System.Data.Entity.Migrations.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003D RID: 61
	internal class SqlWriter : IndentedTextWriter
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x000144D8 File Offset: 0x000126D8
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Transferring ownership")]
		public SqlWriter(StringBuilder b) : base(new StringWriter(b, IndentedTextWriter.Culture))
		{
		}
	}
}
