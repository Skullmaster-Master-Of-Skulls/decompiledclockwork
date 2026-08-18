using System;

namespace MailBee.DnsMX
{
	// Token: 0x0200056F RID: 1391
	public interface IMailBeeNegativeDnsResponseException : IMailBeeNegativeResponseException
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002E31 RID: 11825
		short ID { get; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002E32 RID: 11826
		byte[] ResponseData { get; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06002E33 RID: 11827
		DnsReplyCode ResponseCode { get; }
	}
}
