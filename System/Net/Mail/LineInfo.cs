using System;

namespace System.Net.Mail
{
	// Token: 0x020006C7 RID: 1735
	internal struct LineInfo
	{
		// Token: 0x0600358F RID: 13711 RVA: 0x000E4081 File Offset: 0x000E3081
		internal LineInfo(SmtpStatusCode statusCode, string line)
		{
			this.statusCode = statusCode;
			this.line = line;
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x000E4091 File Offset: 0x000E3091
		internal string Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x000E4099 File Offset: 0x000E3099
		internal SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x040030EE RID: 12526
		private string line;

		// Token: 0x040030EF RID: 12527
		private SmtpStatusCode statusCode;
	}
}
