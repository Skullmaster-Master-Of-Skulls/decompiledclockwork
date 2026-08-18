using System;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x02000152 RID: 338
	internal class ConnectionOption
	{
		// Token: 0x06000D5B RID: 3419 RVA: 0x000914D8 File Offset: 0x0008F6D8
		internal ConnectionOption()
		{
			this.m_sessionDataUnitSize = ConnectionOption.NSPDFSDULN;
			this.m_transportDataUnitSize = ConnectionOption.NSPDFTDULN;
			this.m_SBS = SqlNetOraConfig.SendBufSize;
			this.m_RBS = SqlNetOraConfig.RecvBufSize;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00091548 File Offset: 0x0008F748
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00091550 File Offset: 0x0008F750
		internal bool inAddr_Any
		{
			get
			{
				return this.m_inAddr_Any;
			}
			set
			{
				this.m_inAddr_Any = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0009155C File Offset: 0x0008F75C
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x00091564 File Offset: 0x0008F764
		internal string SSL_Version
		{
			get
			{
				return this.m_ssl_version;
			}
			set
			{
				this.m_ssl_version = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00091570 File Offset: 0x0008F770
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x00091578 File Offset: 0x0008F778
		internal string SSL_WALLET_DIRECTORY
		{
			get
			{
				return this.m_wallet_directory;
			}
			set
			{
				this.m_wallet_directory = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00091584 File Offset: 0x0008F784
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0009158C File Offset: 0x0008F78C
		internal string SSLServerDN
		{
			get
			{
				return this.m_sslServerDN;
			}
			set
			{
				this.m_sslServerDN = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00091598 File Offset: 0x0008F798
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x000915A0 File Offset: 0x0008F7A0
		internal string IP
		{
			get
			{
				return this.m_IP;
			}
			set
			{
				this.m_IP = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000915AC File Offset: 0x0008F7AC
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x000915B4 File Offset: 0x0008F7B4
		internal int SBS
		{
			get
			{
				return this.m_SBS;
			}
			set
			{
				this.m_SBS = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000915C0 File Offset: 0x0008F7C0
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x000915C8 File Offset: 0x0008F7C8
		internal int RBS
		{
			get
			{
				return this.m_RBS;
			}
			set
			{
				this.m_RBS = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x000915D4 File Offset: 0x0008F7D4
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x000915DC File Offset: 0x0008F7DC
		internal int Port
		{
			get
			{
				return this.m_portNumber;
			}
			set
			{
				this.m_portNumber = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x000915E8 File Offset: 0x0008F7E8
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x000915F0 File Offset: 0x0008F7F0
		internal int TransportDataUnitSize
		{
			get
			{
				return this.m_transportDataUnitSize;
			}
			set
			{
				this.m_transportDataUnitSize = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x000915FC File Offset: 0x0008F7FC
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x00091604 File Offset: 0x0008F804
		internal int TransportConnectTO
		{
			get
			{
				return this.m_transportConnectTO;
			}
			set
			{
				this.m_transportConnectTO = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00091610 File Offset: 0x0008F810
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x00091618 File Offset: 0x0008F818
		internal int SessionDataUnitSize
		{
			get
			{
				return this.m_sessionDataUnitSize;
			}
			set
			{
				this.m_sessionDataUnitSize = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00091624 File Offset: 0x0008F824
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x0009162C File Offset: 0x0008F82C
		internal string Protocol
		{
			get
			{
				return this.m_protocol;
			}
			set
			{
				this.m_protocol = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00091638 File Offset: 0x0008F838
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00091640 File Offset: 0x0008F840
		internal string Host
		{
			get
			{
				return this.m_host;
			}
			set
			{
				this.m_host = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0009164C File Offset: 0x0008F84C
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00091654 File Offset: 0x0008F854
		internal string SID
		{
			get
			{
				return this.m_sid;
			}
			set
			{
				this.m_sid = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00091660 File Offset: 0x0008F860
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x00091668 File Offset: 0x0008F868
		internal string Server
		{
			get
			{
				return this.m_server;
			}
			set
			{
				this.m_server = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00091674 File Offset: 0x0008F874
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x0009167C File Offset: 0x0008F87C
		internal string ServiceName
		{
			get
			{
				return this.m_service_name;
			}
			set
			{
				this.m_service_name = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00091688 File Offset: 0x0008F888
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x00091690 File Offset: 0x0008F890
		internal string InstanceName
		{
			get
			{
				return this.m_instance_name;
			}
			set
			{
				this.m_instance_name = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0009169C File Offset: 0x0008F89C
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x000916A4 File Offset: 0x0008F8A4
		internal string Address
		{
			get
			{
				return this.m_addr;
			}
			set
			{
				this.m_addr = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000916B0 File Offset: 0x0008F8B0
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x000916B8 File Offset: 0x0008F8B8
		internal string ConnectData
		{
			get
			{
				return this.m_conn_data;
			}
			set
			{
				this.m_conn_data = value;
			}
		}

		// Token: 0x04000EE1 RID: 3809
		internal static readonly int NSPDFSDULN = 65535;

		// Token: 0x04000EE2 RID: 3810
		internal static readonly int NSPDFTDULN = 65535;

		// Token: 0x04000EE3 RID: 3811
		internal static readonly int NSPMNSDULN = 512;

		// Token: 0x04000EE4 RID: 3812
		internal static readonly int NSPMNTDULN = 255;

		// Token: 0x04000EE5 RID: 3813
		internal static readonly int NSPMXSDULN = 65535;

		// Token: 0x04000EE6 RID: 3814
		internal static readonly int NSPMXTDULN = 65535;

		// Token: 0x04000EE7 RID: 3815
		internal static readonly int NSPINSDULN = 255;

		// Token: 0x04000EE8 RID: 3816
		internal int m_portNumber = -1;

		// Token: 0x04000EE9 RID: 3817
		private int m_transportDataUnitSize;

		// Token: 0x04000EEA RID: 3818
		private int m_sessionDataUnitSize;

		// Token: 0x04000EEB RID: 3819
		private int m_transportConnectTO = -1;

		// Token: 0x04000EEC RID: 3820
		private string m_protocol;

		// Token: 0x04000EED RID: 3821
		private string m_host;

		// Token: 0x04000EEE RID: 3822
		private string m_IP;

		// Token: 0x04000EEF RID: 3823
		private int m_SBS;

		// Token: 0x04000EF0 RID: 3824
		private int m_RBS;

		// Token: 0x04000EF1 RID: 3825
		private string m_sid;

		// Token: 0x04000EF2 RID: 3826
		private string m_addr;

		// Token: 0x04000EF3 RID: 3827
		private string m_server;

		// Token: 0x04000EF4 RID: 3828
		private string m_service_name;

		// Token: 0x04000EF5 RID: 3829
		private string m_instance_name;

		// Token: 0x04000EF6 RID: 3830
		private string m_conn_data;

		// Token: 0x04000EF7 RID: 3831
		private bool m_inAddr_Any;

		// Token: 0x04000EF8 RID: 3832
		private string m_ssl_version = "";

		// Token: 0x04000EF9 RID: 3833
		private string m_wallet_directory = "";

		// Token: 0x04000EFA RID: 3834
		private string m_sslServerDN = "";

		// Token: 0x04000EFB RID: 3835
		internal object AsyncBufferInitArg;

		// Token: 0x04000EFC RID: 3836
		internal ConOraBufPool AsyncBufferPool;

		// Token: 0x02000153 RID: 339
		// (Invoke) Token: 0x06000D84 RID: 3460
		internal delegate void AsyncReceiveCallback(byte[] buf, int length);
	}
}
