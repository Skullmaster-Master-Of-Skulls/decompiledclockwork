using System;
using System.ServiceModel;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005F9 RID: 1529
	[MessageContract]
	public class FileIdentifierMessageDTO
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0000E36B File Offset: 0x0000C56B
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x0000E373 File Offset: 0x0000C573
		[MessageHeader(MustUnderstand = true)]
		public Guid? FileUniqueId { get; set; }

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x0000E37C File Offset: 0x0000C57C
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x0000E384 File Offset: 0x0000C584
		[MessageHeader(MustUnderstand = true)]
		public eFileSource Source { get; set; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x0000E38D File Offset: 0x0000C58D
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x0000E395 File Offset: 0x0000C595
		[MessageHeader(MustUnderstand = true)]
		public int LegacyId { get; set; }
	}
}
