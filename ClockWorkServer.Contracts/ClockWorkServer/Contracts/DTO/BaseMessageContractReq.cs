using System;
using System.ServiceModel;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000E9 RID: 233
	[MessageContract]
	public class BaseMessageContractReq
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000289E File Offset: 0x00000A9E
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x000028A6 File Offset: 0x00000AA6
		[MessageHeader(MustUnderstand = true)]
		public int WhoAmI { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000028AF File Offset: 0x00000AAF
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x000028B7 File Offset: 0x00000AB7
		[MessageHeader(MustUnderstand = true)]
		public string SessionId { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x000028C0 File Offset: 0x00000AC0
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x000028C8 File Offset: 0x00000AC8
		[MessageHeader(MustUnderstand = true)]
		public ApplicationContext ApplicationContext { get; set; }
	}
}
