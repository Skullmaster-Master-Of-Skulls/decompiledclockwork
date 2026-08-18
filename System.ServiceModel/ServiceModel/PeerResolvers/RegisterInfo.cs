using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001CD RID: 461
	[MessageContract(IsWrapped = false)]
	public class RegisterInfo
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x00036515 File Offset: 0x00034715
		public RegisterInfo(Guid client, string meshId, PeerNodeAddress address)
		{
			this.body = new RegisterInfo.RegisterInfoDC(client, meshId, address);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003652B File Offset: 0x0003472B
		public RegisterInfo()
		{
			this.body = new RegisterInfo.RegisterInfoDC();
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0003653E File Offset: 0x0003473E
		public Guid ClientId
		{
			get
			{
				return this.body.ClientId;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003654B File Offset: 0x0003474B
		public string MeshId
		{
			get
			{
				return this.body.MeshId;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00036558 File Offset: 0x00034758
		public PeerNodeAddress NodeAddress
		{
			get
			{
				return this.body.NodeAddress;
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00036565 File Offset: 0x00034765
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x040017A3 RID: 6051
		[MessageBodyMember(Name = "Register", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private RegisterInfo.RegisterInfoDC body;

		// Token: 0x02000B04 RID: 2820
		[DataContract(Name = "Register", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class RegisterInfoDC
		{
			// Token: 0x06006F57 RID: 28503 RVA: 0x0019D956 File Offset: 0x0019BB56
			public RegisterInfoDC()
			{
			}

			// Token: 0x06006F58 RID: 28504 RVA: 0x0019D95E File Offset: 0x0019BB5E
			public RegisterInfoDC(Guid client, string meshId, PeerNodeAddress address)
			{
				this.ClientId = client;
				this.MeshId = meshId;
				this.NodeAddress = address;
			}

			// Token: 0x04003F86 RID: 16262
			[DataMember(Name = "ClientId")]
			public Guid ClientId;

			// Token: 0x04003F87 RID: 16263
			[DataMember(Name = "MeshId")]
			public string MeshId;

			// Token: 0x04003F88 RID: 16264
			[DataMember(Name = "NodeAddress")]
			public PeerNodeAddress NodeAddress;
		}
	}
}
