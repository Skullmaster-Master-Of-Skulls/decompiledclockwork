using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000628 RID: 1576
	public class PingReply
	{
		// Token: 0x0600307D RID: 12413 RVA: 0x000D1957 File Offset: 0x000D0957
		internal PingReply()
		{
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000D195F File Offset: 0x000D095F
		internal PingReply(IPStatus ipStatus)
		{
			this.ipStatus = ipStatus;
			this.buffer = new byte[0];
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000D197C File Offset: 0x000D097C
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

		// Token: 0x06003080 RID: 12416 RVA: 0x000D19EC File Offset: 0x000D09EC
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

		// Token: 0x06003081 RID: 12417 RVA: 0x000D1A80 File Offset: 0x000D0A80
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

		// Token: 0x06003082 RID: 12418 RVA: 0x000D1B08 File Offset: 0x000D0B08
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
				switch (type)
				{
				case IcmpV4Type.ICMP4_TIME_EXCEEDED:
					return IPStatus.TtlExpired;
				case IcmpV4Type.ICMP4_PARAM_PROB:
					return IPStatus.ParameterProblem;
				}
				break;
			}
			return IPStatus.Unknown;
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x000D1B9B File Offset: 0x000D0B9B
		public IPStatus Status
		{
			get
			{
				return this.ipStatus;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000D1BA3 File Offset: 0x000D0BA3
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x000D1BAB File Offset: 0x000D0BAB
		public long RoundtripTime
		{
			get
			{
				return this.rtt;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x000D1BB3 File Offset: 0x000D0BB3
		public PingOptions Options
		{
			get
			{
				if (!ComNetOS.IsWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
				}
				return this.options;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x000D1BD2 File Offset: 0x000D0BD2
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x04002E2B RID: 11819
		private IPAddress address;

		// Token: 0x04002E2C RID: 11820
		private PingOptions options;

		// Token: 0x04002E2D RID: 11821
		private IPStatus ipStatus;

		// Token: 0x04002E2E RID: 11822
		private long rtt;

		// Token: 0x04002E2F RID: 11823
		private byte[] buffer;
	}
}
