using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000165 RID: 357
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRequiredRequest : BaseMessageReq
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00003EB0 File Offset: 0x000020B0
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00003EB8 File Offset: 0x000020B8
		[DataMember]
		public string FileType { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00003EC1 File Offset: 0x000020C1
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00003EC9 File Offset: 0x000020C9
		[DataMember]
		public string ClientVersion { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00003ED2 File Offset: 0x000020D2
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00003EDA File Offset: 0x000020DA
		[DataMember]
		public eAddressSize AddressSize { get; set; } = eAddressSize.x64;

		// Token: 0x060008C3 RID: 2243 RVA: 0x00003EE3 File Offset: 0x000020E3
		public UpdateRequiredRequest()
		{
			this.SetDefaults();
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00003EFC File Offset: 0x000020FC
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetDefaults();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00003F06 File Offset: 0x00002106
		private void SetDefaults()
		{
			this.AddressSize = eAddressSize.x64;
		}
	}
}
