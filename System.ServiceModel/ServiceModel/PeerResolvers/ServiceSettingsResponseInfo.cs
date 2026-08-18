using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001D4 RID: 468
	[MessageContract(IsWrapped = false)]
	public class ServiceSettingsResponseInfo
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x00036816 File Offset: 0x00034A16
		public ServiceSettingsResponseInfo() : this(false)
		{
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0003681F File Offset: 0x00034A1F
		public ServiceSettingsResponseInfo(bool control)
		{
			this.body = new ServiceSettingsResponseInfo.ServiceSettingsResponseInfoDC(control);
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00036833 File Offset: 0x00034A33
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x00036840 File Offset: 0x00034A40
		public bool ControlMeshShape
		{
			get
			{
				return this.body.ControlMeshShape;
			}
			set
			{
				this.body.ControlMeshShape = value;
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003684E File Offset: 0x00034A4E
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x040017AC RID: 6060
		[MessageBodyMember(Name = "ServiceSettings", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private ServiceSettingsResponseInfo.ServiceSettingsResponseInfoDC body;

		// Token: 0x02000B0A RID: 2826
		[DataContract(Name = "ServiceSettingsResponseInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class ServiceSettingsResponseInfoDC
		{
			// Token: 0x06006F62 RID: 28514 RVA: 0x0019DA18 File Offset: 0x0019BC18
			public ServiceSettingsResponseInfoDC(bool control)
			{
				this.ControlMeshShape = control;
			}

			// Token: 0x04003F95 RID: 16277
			[DataMember(Name = "ControlMeshShape")]
			public bool ControlMeshShape;
		}
	}
}
