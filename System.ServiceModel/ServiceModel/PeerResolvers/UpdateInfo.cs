using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001CF RID: 463
	[MessageContract(IsWrapped = false)]
	public class UpdateInfo
	{
		// Token: 0x06000EFF RID: 3839 RVA: 0x00036643 File Offset: 0x00034843
		public UpdateInfo(Guid registrationId, Guid client, string meshId, PeerNodeAddress address)
		{
			this.body = new UpdateInfo.UpdateInfoDC(registrationId, client, meshId, address);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003665B File Offset: 0x0003485B
		public UpdateInfo()
		{
			this.body = new UpdateInfo.UpdateInfoDC();
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0003666E File Offset: 0x0003486E
		public Guid ClientId
		{
			get
			{
				return this.body.ClientId;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0003667B File Offset: 0x0003487B
		public Guid RegistrationId
		{
			get
			{
				return this.body.RegistrationId;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00036688 File Offset: 0x00034888
		public string MeshId
		{
			get
			{
				return this.body.MeshId;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00036695 File Offset: 0x00034895
		public PeerNodeAddress NodeAddress
		{
			get
			{
				return this.body.NodeAddress;
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x000366A2 File Offset: 0x000348A2
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x040017A5 RID: 6053
		[MessageBodyMember(Name = "Update", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private UpdateInfo.UpdateInfoDC body;

		// Token: 0x02000B06 RID: 2822
		[DataContract(Name = "Update", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class UpdateInfoDC
		{
			// Token: 0x06006F5B RID: 28507 RVA: 0x0019D999 File Offset: 0x0019BB99
			public UpdateInfoDC()
			{
			}

			// Token: 0x06006F5C RID: 28508 RVA: 0x0019D9A1 File Offset: 0x0019BBA1
			public UpdateInfoDC(Guid registrationId, Guid client, string meshId, PeerNodeAddress address)
			{
				this.ClientId = client;
				this.MeshId = meshId;
				this.NodeAddress = address;
				this.RegistrationId = registrationId;
			}

			// Token: 0x04003F8B RID: 16267
			[DataMember(Name = "ClientId")]
			public Guid ClientId;

			// Token: 0x04003F8C RID: 16268
			[DataMember(Name = "MeshId")]
			public string MeshId;

			// Token: 0x04003F8D RID: 16269
			[DataMember(Name = "NodeAddress")]
			public PeerNodeAddress NodeAddress;

			// Token: 0x04003F8E RID: 16270
			[DataMember(Name = "RegistrationId")]
			public Guid RegistrationId;
		}
	}
}
