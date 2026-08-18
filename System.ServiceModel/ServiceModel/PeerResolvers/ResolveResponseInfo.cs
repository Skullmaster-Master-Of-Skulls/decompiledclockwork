using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001CC RID: 460
	[MessageContract(IsWrapped = false)]
	public class ResolveResponseInfo
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x000364D2 File Offset: 0x000346D2
		public ResolveResponseInfo() : this(null)
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000364DB File Offset: 0x000346DB
		public ResolveResponseInfo(PeerNodeAddress[] addresses)
		{
			this.body = new ResolveResponseInfo.ResolveResponseInfoDC(addresses);
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x000364EF File Offset: 0x000346EF
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x000364FC File Offset: 0x000346FC
		public IList<PeerNodeAddress> Addresses
		{
			get
			{
				return this.body.Addresses;
			}
			set
			{
				this.body.Addresses = value;
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003650A File Offset: 0x0003470A
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x040017A2 RID: 6050
		[MessageBodyMember(Name = "ResolveResponse", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private ResolveResponseInfo.ResolveResponseInfoDC body;

		// Token: 0x02000B03 RID: 2819
		[DataContract(Name = "ResolveResponseInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class ResolveResponseInfoDC
		{
			// Token: 0x06006F56 RID: 28502 RVA: 0x0019D947 File Offset: 0x0019BB47
			public ResolveResponseInfoDC(PeerNodeAddress[] addresses)
			{
				this.Addresses = addresses;
			}

			// Token: 0x04003F85 RID: 16261
			[DataMember(Name = "Addresses")]
			public IList<PeerNodeAddress> Addresses;
		}
	}
}
