using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A3D RID: 2621
	[DataContract(Name = "Referral", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
	internal class Referral
	{
		// Token: 0x060067E2 RID: 26594 RVA: 0x00183F83 File Offset: 0x00182183
		public Referral(ulong nodeId, PeerNodeAddress address)
		{
			this.nodeId = nodeId;
			this.address = address;
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x060067E3 RID: 26595 RVA: 0x00183F99 File Offset: 0x00182199
		// (set) Token: 0x060067E4 RID: 26596 RVA: 0x00183FA1 File Offset: 0x001821A1
		public PeerNodeAddress Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x060067E5 RID: 26597 RVA: 0x00183FAA File Offset: 0x001821AA
		// (set) Token: 0x060067E6 RID: 26598 RVA: 0x00183FB2 File Offset: 0x001821B2
		public ulong NodeId
		{
			get
			{
				return this.nodeId;
			}
			set
			{
				this.nodeId = value;
			}
		}

		// Token: 0x04003B97 RID: 15255
		[DataMember(Name = "NodeId")]
		private ulong nodeId;

		// Token: 0x04003B98 RID: 15256
		[DataMember(Name = "Address")]
		private PeerNodeAddress address;
	}
}
