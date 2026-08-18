using System;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.Owin.Security.DataHandler
{
	// Token: 0x02000009 RID: 9
	public class TicketDataFormat : SecureDataFormat<AuthenticationTicket>
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000229B File Offset: 0x0000049B
		public TicketDataFormat(IDataProtector protector) : base(DataSerializers.Ticket, protector, TextEncodings.Base64Url)
		{
		}
	}
}
