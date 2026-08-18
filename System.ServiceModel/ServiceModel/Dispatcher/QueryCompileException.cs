using System;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000498 RID: 1176
	internal class QueryCompileException : XPathException
	{
		// Token: 0x06002D2C RID: 11564 RVA: 0x000AFFBA File Offset: 0x000AE1BA
		internal QueryCompileException(QueryCompileError error, string message) : base(message, null)
		{
			this.error = error;
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000AFFCB File Offset: 0x000AE1CB
		internal QueryCompileException(QueryCompileError error) : this(error, null)
		{
			this.error = error;
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000AFFDC File Offset: 0x000AE1DC
		public override string ToString()
		{
			return this.error.ToString();
		}

		// Token: 0x04002494 RID: 9364
		private QueryCompileError error;
	}
}
