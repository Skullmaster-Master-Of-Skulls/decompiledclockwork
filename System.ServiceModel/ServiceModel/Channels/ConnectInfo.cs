using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A36 RID: 2614
	[MessageContract(IsWrapped = false)]
	internal class ConnectInfo
	{
		// Token: 0x060067CC RID: 26572 RVA: 0x00183EA8 File Offset: 0x001820A8
		public ConnectInfo()
		{
			this.body = new ConnectInfo.ConnectInfoDC();
		}

		// Token: 0x060067CD RID: 26573 RVA: 0x00183EBB File Offset: 0x001820BB
		public ConnectInfo(ulong nodeId, PeerNodeAddress address)
		{
			this.body = new ConnectInfo.ConnectInfoDC(nodeId, address);
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x060067CE RID: 26574 RVA: 0x00183ED0 File Offset: 0x001820D0
		public PeerNodeAddress Address
		{
			get
			{
				return this.body.address;
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x060067CF RID: 26575 RVA: 0x00183EDD File Offset: 0x001820DD
		public ulong NodeId
		{
			get
			{
				return this.body.nodeId;
			}
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x00183EEA File Offset: 0x001820EA
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x04003B8E RID: 15246
		[MessageBodyMember(Name = "Connect", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private ConnectInfo.ConnectInfoDC body;

		// Token: 0x02000E74 RID: 3700
		[DataContract(Name = "ConnectInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class ConnectInfoDC
		{
			// Token: 0x060083EA RID: 33770 RVA: 0x001E801F File Offset: 0x001E621F
			public ConnectInfoDC()
			{
			}

			// Token: 0x060083EB RID: 33771 RVA: 0x001E8027 File Offset: 0x001E6227
			public ConnectInfoDC(ulong nodeId, PeerNodeAddress address)
			{
				this.nodeId = nodeId;
				this.address = address;
			}

			// Token: 0x04004B1B RID: 19227
			[DataMember(Name = "NodeId")]
			public ulong nodeId;

			// Token: 0x04004B1C RID: 19228
			[DataMember(Name = "Address")]
			public PeerNodeAddress address;
		}
	}
}
