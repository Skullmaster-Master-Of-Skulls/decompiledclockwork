using System;

namespace System.Net.Sockets
{
	// Token: 0x020005B2 RID: 1458
	[Serializable]
	public struct SocketInformation
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000C1F67 File Offset: 0x000C0F67
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x000C1F6F File Offset: 0x000C0F6F
		public byte[] ProtocolInformation
		{
			get
			{
				return this.protocolInformation;
			}
			set
			{
				this.protocolInformation = value;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x000C1F78 File Offset: 0x000C0F78
		// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x000C1F80 File Offset: 0x000C0F80
		public SocketInformationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x000C1F89 File Offset: 0x000C0F89
		// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x000C1F99 File Offset: 0x000C0F99
		internal bool IsNonBlocking
		{
			get
			{
				return (this.options & SocketInformationOptions.NonBlocking) != (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.NonBlocking;
					return;
				}
				this.options &= ~SocketInformationOptions.NonBlocking;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000C1FBC File Offset: 0x000C0FBC
		// (set) Token: 0x06002CEB RID: 11499 RVA: 0x000C1FCC File Offset: 0x000C0FCC
		internal bool IsConnected
		{
			get
			{
				return (this.options & SocketInformationOptions.Connected) != (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Connected;
					return;
				}
				this.options &= ~SocketInformationOptions.Connected;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000C1FEF File Offset: 0x000C0FEF
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x000C1FFF File Offset: 0x000C0FFF
		internal bool IsListening
		{
			get
			{
				return (this.options & SocketInformationOptions.Listening) != (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Listening;
					return;
				}
				this.options &= ~SocketInformationOptions.Listening;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x000C2022 File Offset: 0x000C1022
		// (set) Token: 0x06002CEF RID: 11503 RVA: 0x000C2032 File Offset: 0x000C1032
		internal bool UseOnlyOverlappedIO
		{
			get
			{
				return (this.options & SocketInformationOptions.UseOnlyOverlappedIO) != (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.UseOnlyOverlappedIO;
					return;
				}
				this.options &= ~SocketInformationOptions.UseOnlyOverlappedIO;
			}
		}

		// Token: 0x04002B1F RID: 11039
		private byte[] protocolInformation;

		// Token: 0x04002B20 RID: 11040
		private SocketInformationOptions options;
	}
}
