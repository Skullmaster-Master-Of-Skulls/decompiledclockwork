using System;
using System.Text;

namespace MailBee
{
	// Token: 0x02000061 RID: 97
	public interface IMailBeeNegativeTextResponseException : IMailBeeNegativeResponseException
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000390 RID: 912
		Encoding ResponseEncoding { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000391 RID: 913
		string ResponseString { get; }
	}
}
