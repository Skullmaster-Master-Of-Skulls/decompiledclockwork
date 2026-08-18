using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001D0 RID: 464
	[MessageContract(IsWrapped = false)]
	public class UnregisterInfo
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000366AD File Offset: 0x000348AD
		public Guid RegistrationId
		{
			get
			{
				return this.body.RegistrationId;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x000366BA File Offset: 0x000348BA
		public string MeshId
		{
			get
			{
				return this.body.MeshId;
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000366C7 File Offset: 0x000348C7
		public UnregisterInfo()
		{
			this.body = new UnregisterInfo.UnregisterInfoDC();
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000366DA File Offset: 0x000348DA
		public UnregisterInfo(string meshId, Guid registrationId)
		{
			this.body = new UnregisterInfo.UnregisterInfoDC(meshId, registrationId);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x000366EF File Offset: 0x000348EF
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x040017A6 RID: 6054
		[MessageBodyMember(Name = "Unregister", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private UnregisterInfo.UnregisterInfoDC body;

		// Token: 0x02000B07 RID: 2823
		[DataContract(Name = "UnregisterInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class UnregisterInfoDC
		{
			// Token: 0x06006F5D RID: 28509 RVA: 0x0019D9C6 File Offset: 0x0019BBC6
			public UnregisterInfoDC()
			{
			}

			// Token: 0x06006F5E RID: 28510 RVA: 0x0019D9CE File Offset: 0x0019BBCE
			public UnregisterInfoDC(string meshId, Guid registrationId)
			{
				this.MeshId = meshId;
				this.RegistrationId = registrationId;
			}

			// Token: 0x04003F8F RID: 16271
			[DataMember(Name = "RegistrationId")]
			public Guid RegistrationId;

			// Token: 0x04003F90 RID: 16272
			[DataMember(Name = "MeshId")]
			public string MeshId;
		}
	}
}
