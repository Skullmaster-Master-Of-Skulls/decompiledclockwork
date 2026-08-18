using System;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
	// Token: 0x02000010 RID: 16
	public static class DataSerializers
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000024EC File Offset: 0x000006EC
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000024F3 File Offset: 0x000006F3
		public static IDataSerializer<AuthenticationProperties> Properties { get; set; } = new PropertiesSerializer();

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000024FB File Offset: 0x000006FB
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002502 File Offset: 0x00000702
		public static IDataSerializer<AuthenticationTicket> Ticket { get; set; } = new TicketSerializer();
	}
}
