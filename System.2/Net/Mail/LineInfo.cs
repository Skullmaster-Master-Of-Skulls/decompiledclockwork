using System;

namespace System.Net.Mail
{
	// Token: 0x02000287 RID: 647
	internal struct LineInfo
	{
		// Token: 0x0600182F RID: 6191 RVA: 0x0007B37D File Offset: 0x0007957D
		internal LineInfo(SmtpStatusCode statusCode, string line)
		{
			this.statusCode = statusCode;
			this.line = line;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0007B38D File Offset: 0x0007958D
		internal string Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0007B395 File Offset: 0x00079595
		internal SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x0400183C RID: 6204
		private string line;

		// Token: 0x0400183D RID: 6205
		private SmtpStatusCode statusCode;
	}
}
