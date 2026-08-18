using System;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000496 RID: 1174
	internal class QueryProcessingException : XPathException
	{
		// Token: 0x06002D29 RID: 11561 RVA: 0x000AFF85 File Offset: 0x000AE185
		internal QueryProcessingException(QueryProcessingError error, string message) : base(message, null)
		{
			this.error = error;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000AFF96 File Offset: 0x000AE196
		internal QueryProcessingException(QueryProcessingError error) : this(error, null)
		{
			this.error = error;
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000AFFA7 File Offset: 0x000AE1A7
		public override string ToString()
		{
			return this.error.ToString();
		}

		// Token: 0x04002471 RID: 9329
		private QueryProcessingError error;
	}
}
