using System;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	// Token: 0x02000375 RID: 885
	[Serializable]
	public struct SocketInformation
	{
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000963DA File Offset: 0x000945DA
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x000963E2 File Offset: 0x000945E2
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

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x000963EB File Offset: 0x000945EB
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x000963F3 File Offset: 0x000945F3
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

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x000963FC File Offset: 0x000945FC
		// (set) Token: 0x06002024 RID: 8228 RVA: 0x00096409 File Offset: 0x00094609
		internal bool IsNonBlocking
		{
			get
			{
				return (this.options & SocketInformationOptions.NonBlocking) > (SocketInformationOptions)0;
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

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0009642C File Offset: 0x0009462C
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x00096439 File Offset: 0x00094639
		internal bool IsConnected
		{
			get
			{
				return (this.options & SocketInformationOptions.Connected) > (SocketInformationOptions)0;
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

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x0009645C File Offset: 0x0009465C
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x00096469 File Offset: 0x00094669
		internal bool IsListening
		{
			get
			{
				return (this.options & SocketInformationOptions.Listening) > (SocketInformationOptions)0;
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

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x0009648C File Offset: 0x0009468C
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x00096499 File Offset: 0x00094699
		internal bool UseOnlyOverlappedIO
		{
			get
			{
				return (this.options & SocketInformationOptions.UseOnlyOverlappedIO) > (SocketInformationOptions)0;
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

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x000964BC File Offset: 0x000946BC
		// (set) Token: 0x0600202C RID: 8236 RVA: 0x000964C4 File Offset: 0x000946C4
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
			set
			{
				this.remoteEndPoint = value;
			}
		}

		// Token: 0x04001E38 RID: 7736
		private byte[] protocolInformation;

		// Token: 0x04001E39 RID: 7737
		private SocketInformationOptions options;

		// Token: 0x04001E3A RID: 7738
		[OptionalField]
		private EndPoint remoteEndPoint;
	}
}
