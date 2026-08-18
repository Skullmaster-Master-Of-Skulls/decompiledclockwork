using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002ED RID: 749
	public class PingReply
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x0007FEEA File Offset: 0x0007E0EA
		internal PingReply()
		{
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0007FEF2 File Offset: 0x0007E0F2
		internal PingReply(IPStatus ipStatus)
		{
			this.ipStatus = ipStatus;
			this.buffer = new byte[0];
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0007FF10 File Offset: 0x0007E110
		internal PingReply(byte[] data, int dataLength, IPAddress address, int time)
		{
			this.address = address;
			this.rtt = (long)time;
			this.ipStatus = this.GetIPStatus((IcmpV4Type)data[20], (IcmpV4Code)data[21]);
			if (this.ipStatus == IPStatus.Success)
			{
				this.buffer = new byte[dataLength - 28];
				Array.Copy(data, 28, this.buffer, 0, dataLength - 28);
				return;
			}
			this.buffer = new byte[0];
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0007FF80 File Offset: 0x0007E180
		internal PingReply(IcmpEchoReply reply)
		{
			this.address = new IPAddress((long)((ulong)reply.address));
			this.ipStatus = (IPStatus)reply.status;
			if (this.ipStatus == IPStatus.Success)
			{
				this.rtt = (long)((ulong)reply.roundTripTime);
				this.buffer = new byte[(int)reply.dataSize];
				Marshal.Copy(reply.data, this.buffer, 0, (int)reply.dataSize);
				this.options = new PingOptions(reply.options);
				return;
			}
			this.buffer = new byte[0];
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00080010 File Offset: 0x0007E210
		internal PingReply(Icmp6EchoReply reply, IntPtr dataPtr, int sendSize)
		{
			this.address = new IPAddress(reply.Address.Address, (long)((ulong)reply.Address.ScopeID));
			this.ipStatus = (IPStatus)reply.Status;
			if (this.ipStatus == IPStatus.Success)
			{
				this.rtt = (long)((ulong)reply.RoundTripTime);
				this.buffer = new byte[sendSize];
				Marshal.Copy(IntPtrHelper.Add(dataPtr, 36), this.buffer, 0, sendSize);
				return;
			}
			this.buffer = new byte[0];
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00080094 File Offset: 0x0007E294
		private IPStatus GetIPStatus(IcmpV4Type type, IcmpV4Code code)
		{
			switch (type)
			{
			case IcmpV4Type.ICMP4_ECHO_REPLY:
				return IPStatus.Success;
			case (IcmpV4Type)1:
			case (IcmpV4Type)2:
				break;
			case IcmpV4Type.ICMP4_DST_UNREACH:
				switch (code)
				{
				case IcmpV4Code.ICMP4_UNREACH_NET:
					return IPStatus.DestinationNetworkUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_HOST:
					return IPStatus.DestinationHostUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_PROTOCOL:
					return IPStatus.DestinationProtocolUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_PORT:
					return IPStatus.DestinationPortUnreachable;
				case IcmpV4Code.ICMP4_UNREACH_FRAG_NEEDED:
					return IPStatus.PacketTooBig;
				default:
					return IPStatus.DestinationUnreachable;
				}
				break;
			case IcmpV4Type.ICMP4_SOURCE_QUENCH:
				return IPStatus.SourceQuench;
			default:
				if (type == IcmpV4Type.ICMP4_TIME_EXCEEDED)
				{
					return IPStatus.TtlExpired;
				}
				if (type == IcmpV4Type.ICMP4_PARAM_PROB)
				{
					return IPStatus.ParameterProblem;
				}
				break;
			}
			return IPStatus.Unknown;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0008011C File Offset: 0x0007E31C
		public IPStatus Status
		{
			get
			{
				return this.ipStatus;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x00080124 File Offset: 0x0007E324
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0008012C File Offset: 0x0007E32C
		public long RoundtripTime
		{
			get
			{
				return this.rtt;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x00080134 File Offset: 0x0007E334
		public PingOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0008013C File Offset: 0x0007E33C
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x04001A8E RID: 6798
		private IPAddress address;

		// Token: 0x04001A8F RID: 6799
		private PingOptions options;

		// Token: 0x04001A90 RID: 6800
		private IPStatus ipStatus;

		// Token: 0x04001A91 RID: 6801
		private long rtt;

		// Token: 0x04001A92 RID: 6802
		private byte[] buffer;
	}
}
