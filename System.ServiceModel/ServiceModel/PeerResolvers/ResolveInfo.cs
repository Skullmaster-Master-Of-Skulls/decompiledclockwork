using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001CB RID: 459
	[MessageContract(IsWrapped = false)]
	public class ResolveInfo
	{
		// Token: 0x06000EE7 RID: 3815 RVA: 0x00036477 File Offset: 0x00034677
		public ResolveInfo(Guid clientId, string meshId, int maxAddresses)
		{
			this.body = new ResolveInfo.ResolveInfoDC(clientId, meshId, maxAddresses);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003648D File Offset: 0x0003468D
		public ResolveInfo()
		{
			this.body = new ResolveInfo.ResolveInfoDC();
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x000364A0 File Offset: 0x000346A0
		public Guid ClientId
		{
			get
			{
				return this.body.ClientId;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x000364AD File Offset: 0x000346AD
		public string MeshId
		{
			get
			{
				return this.body.MeshId;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x000364BA File Offset: 0x000346BA
		public int MaxAddresses
		{
			get
			{
				return this.body.MaxAddresses;
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000364C7 File Offset: 0x000346C7
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x040017A1 RID: 6049
		[MessageBodyMember(Name = "Resolve", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private ResolveInfo.ResolveInfoDC body;

		// Token: 0x02000B02 RID: 2818
		[DataContract(Name = "ResolveInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class ResolveInfoDC
		{
			// Token: 0x06006F54 RID: 28500 RVA: 0x0019D922 File Offset: 0x0019BB22
			public ResolveInfoDC(Guid clientId, string meshId, int maxAddresses)
			{
				this.ClientId = clientId;
				this.MeshId = meshId;
				this.MaxAddresses = maxAddresses;
			}

			// Token: 0x06006F55 RID: 28501 RVA: 0x0019D93F File Offset: 0x0019BB3F
			public ResolveInfoDC()
			{
			}

			// Token: 0x04003F82 RID: 16258
			[DataMember(Name = "ClientId")]
			public Guid ClientId;

			// Token: 0x04003F83 RID: 16259
			[DataMember(Name = "MeshId")]
			public string MeshId;

			// Token: 0x04003F84 RID: 16260
			[DataMember(Name = "MaxAddresses")]
			public int MaxAddresses;
		}
	}
}
