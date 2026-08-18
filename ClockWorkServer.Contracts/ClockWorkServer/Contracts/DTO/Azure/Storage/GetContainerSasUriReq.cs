using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Azure.Storage;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage
{
	// Token: 0x020008B1 RID: 2225
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetContainerSasUriReq : BaseMessageReq
	{
		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x0001548E File Offset: 0x0001368E
		// (set) Token: 0x06002D00 RID: 11520 RVA: 0x00015496 File Offset: 0x00013696
		[DataMember]
		public TokenBasedClientCredentialsDTO ClientCredentials { get; set; }

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x0001549F File Offset: 0x0001369F
		// (set) Token: 0x06002D02 RID: 11522 RVA: 0x000154A7 File Offset: 0x000136A7
		[DataMember]
		public string ContainerName { get; set; }

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x000154B0 File Offset: 0x000136B0
		// (set) Token: 0x06002D04 RID: 11524 RVA: 0x000154B8 File Offset: 0x000136B8
		[DataMember]
		public bool UseClientIdPrefix { get; set; }

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x000154C1 File Offset: 0x000136C1
		// (set) Token: 0x06002D06 RID: 11526 RVA: 0x000154C9 File Offset: 0x000136C9
		[DataMember]
		public AzureSharedAccessBlobPermissions Permissions { get; set; }

		// Token: 0x06002D07 RID: 11527 RVA: 0x000154D2 File Offset: 0x000136D2
		public GetContainerSasUriReq()
		{
			this.SetDefaults();
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000154E3 File Offset: 0x000136E3
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetDefaults();
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000154ED File Offset: 0x000136ED
		private void SetDefaults()
		{
			this.Permissions = (AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
		}
	}
}
