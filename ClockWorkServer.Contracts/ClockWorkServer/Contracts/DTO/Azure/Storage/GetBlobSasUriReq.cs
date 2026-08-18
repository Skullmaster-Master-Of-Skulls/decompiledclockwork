using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Azure.Storage;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage
{
	// Token: 0x020008B3 RID: 2227
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetBlobSasUriReq : BaseMessageReq
	{
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x0001550A File Offset: 0x0001370A
		// (set) Token: 0x06002D0E RID: 11534 RVA: 0x00015512 File Offset: 0x00013712
		[DataMember]
		public TokenBasedClientCredentialsDTO ClientCredentials { get; set; }

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x0001551B File Offset: 0x0001371B
		// (set) Token: 0x06002D10 RID: 11536 RVA: 0x00015523 File Offset: 0x00013723
		[DataMember]
		public Uri BlobUri { get; set; }

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06002D11 RID: 11537 RVA: 0x0001552C File Offset: 0x0001372C
		// (set) Token: 0x06002D12 RID: 11538 RVA: 0x00015534 File Offset: 0x00013734
		[DataMember]
		public AzureSharedAccessBlobPermissions Permissions { get; set; }

		// Token: 0x06002D13 RID: 11539 RVA: 0x0001553D File Offset: 0x0001373D
		public GetBlobSasUriReq()
		{
			this.SetDefaults();
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x0001554E File Offset: 0x0001374E
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetDefaults();
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x00015558 File Offset: 0x00013758
		private void SetDefaults()
		{
			this.Permissions = (AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
		}
	}
}
