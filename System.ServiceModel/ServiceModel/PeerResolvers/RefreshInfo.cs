using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001D1 RID: 465
	[MessageContract(IsWrapped = false)]
	public class RefreshInfo
	{
		// Token: 0x06000F0B RID: 3851 RVA: 0x000366FA File Offset: 0x000348FA
		public RefreshInfo(string meshId, Guid regId)
		{
			this.body = new RefreshInfo.RefreshInfoDC(meshId, regId);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003670F File Offset: 0x0003490F
		public RefreshInfo()
		{
			this.body = new RefreshInfo.RefreshInfoDC();
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00036722 File Offset: 0x00034922
		public string MeshId
		{
			get
			{
				return this.body.MeshId;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0003672F File Offset: 0x0003492F
		public Guid RegistrationId
		{
			get
			{
				return this.body.RegistrationId;
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003673C File Offset: 0x0003493C
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x040017A7 RID: 6055
		[MessageBodyMember(Name = "Refresh", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private RefreshInfo.RefreshInfoDC body;

		// Token: 0x02000B08 RID: 2824
		[DataContract(Name = "RefreshInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class RefreshInfoDC
		{
			// Token: 0x06006F5F RID: 28511 RVA: 0x0019D9E4 File Offset: 0x0019BBE4
			public RefreshInfoDC()
			{
			}

			// Token: 0x06006F60 RID: 28512 RVA: 0x0019D9EC File Offset: 0x0019BBEC
			public RefreshInfoDC(string meshId, Guid regId)
			{
				this.MeshId = meshId;
				this.RegistrationId = regId;
			}

			// Token: 0x04003F91 RID: 16273
			[DataMember(Name = "RegistrationId")]
			public Guid RegistrationId;

			// Token: 0x04003F92 RID: 16274
			[DataMember(Name = "MeshId")]
			public string MeshId;
		}
	}
}
