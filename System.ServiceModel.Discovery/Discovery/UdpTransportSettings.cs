using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000059 RID: 89
	public class UdpTransportSettings
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0000CD90 File Offset: 0x0000AF90
		internal UdpTransportSettings(UdpTransportBindingElement udpTransportBindingElement)
		{
			this.maxPendingMessageCount = 32;
			this.UdpTransportBindingElement = udpTransportBindingElement;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000CDA7 File Offset: 0x0000AFA7
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		public int DuplicateMessageHistoryLength
		{
			get
			{
				return this.UdpTransportBindingElement.DuplicateMessageHistoryLength;
			}
			set
			{
				this.UdpTransportBindingElement.DuplicateMessageHistoryLength = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000CDC2 File Offset: 0x0000AFC2
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000CDCA File Offset: 0x0000AFCA
		public int MaxPendingMessageCount
		{
			get
			{
				return this.maxPendingMessageCount;
			}
			set
			{
				this.maxPendingMessageCount = value;
				this.UdpTransportBindingElement.MaxPendingMessagesTotalSize = this.MaxReceivedMessageSize * (long)this.MaxPendingMessageCount;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000CDFE File Offset: 0x0000AFFE
		public int MaxMulticastRetransmitCount
		{
			get
			{
				return this.UdpTransportBindingElement.RetransmissionSettings.MaxMulticastRetransmitCount;
			}
			set
			{
				this.UdpTransportBindingElement.RetransmissionSettings.MaxMulticastRetransmitCount = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000CE11 File Offset: 0x0000B011
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000CE23 File Offset: 0x0000B023
		public int MaxUnicastRetransmitCount
		{
			get
			{
				return this.UdpTransportBindingElement.RetransmissionSettings.MaxUnicastRetransmitCount;
			}
			set
			{
				this.UdpTransportBindingElement.RetransmissionSettings.MaxUnicastRetransmitCount = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000CE36 File Offset: 0x0000B036
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000CE43 File Offset: 0x0000B043
		public string MulticastInterfaceId
		{
			get
			{
				return this.UdpTransportBindingElement.MulticastInterfaceId;
			}
			set
			{
				this.UdpTransportBindingElement.MulticastInterfaceId = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000CE51 File Offset: 0x0000B051
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000CE5E File Offset: 0x0000B05E
		public int SocketReceiveBufferSize
		{
			get
			{
				return this.UdpTransportBindingElement.SocketReceiveBufferSize;
			}
			set
			{
				this.UdpTransportBindingElement.SocketReceiveBufferSize = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000CE6C File Offset: 0x0000B06C
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000CE79 File Offset: 0x0000B079
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.UdpTransportBindingElement.MaxReceivedMessageSize;
			}
			set
			{
				this.UdpTransportBindingElement.MaxReceivedMessageSize = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000CE87 File Offset: 0x0000B087
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000CE94 File Offset: 0x0000B094
		public long MaxBufferPoolSize
		{
			get
			{
				return this.UdpTransportBindingElement.MaxBufferPoolSize;
			}
			set
			{
				this.UdpTransportBindingElement.MaxBufferPoolSize = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000CEA2 File Offset: 0x0000B0A2
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000CEAF File Offset: 0x0000B0AF
		public int TimeToLive
		{
			get
			{
				return this.UdpTransportBindingElement.TimeToLive;
			}
			set
			{
				this.UdpTransportBindingElement.TimeToLive = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000CEBD File Offset: 0x0000B0BD
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000CEC5 File Offset: 0x0000B0C5
		internal UdpTransportBindingElement UdpTransportBindingElement { get; private set; }

		// Token: 0x04000112 RID: 274
		private int maxPendingMessageCount;
	}
}
