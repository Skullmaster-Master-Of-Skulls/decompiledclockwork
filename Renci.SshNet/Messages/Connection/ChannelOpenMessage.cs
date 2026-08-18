using System;
using System.Globalization;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A4 RID: 164
	[Message("SSH_MSG_CHANNEL_OPEN", 90)]
	public class ChannelOpenMessage : Message
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001DFFB File Offset: 0x0001C1FB
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0001E003 File Offset: 0x0001C203
		public byte[] ChannelType { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0001E00C File Offset: 0x0001C20C
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0001E014 File Offset: 0x0001C214
		public uint LocalChannelNumber { get; protected set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001E01D File Offset: 0x0001C21D
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0001E025 File Offset: 0x0001C225
		public uint InitialWindowSize { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001E02E File Offset: 0x0001C22E
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001E036 File Offset: 0x0001C236
		public uint MaximumPacketSize { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001E03F File Offset: 0x0001C23F
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001E047 File Offset: 0x0001C247
		public ChannelOpenInfo Info { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001E050 File Offset: 0x0001C250
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.ChannelType.Length + 4 + 4 + 4 + this._infoBytes.Length;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001DDCE File Offset: 0x0001BFCE
		public ChannelOpenMessage()
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001E074 File Offset: 0x0001C274
		public ChannelOpenMessage(uint channelNumber, uint initialWindowSize, uint maximumPacketSize, ChannelOpenInfo info)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.ChannelType = SshData.Ascii.GetBytes(info.ChannelType);
			this.LocalChannelNumber = channelNumber;
			this.InitialWindowSize = initialWindowSize;
			this.MaximumPacketSize = maximumPacketSize;
			this.Info = info;
			this._infoBytes = info.GetBytes();
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001E0D8 File Offset: 0x0001C2D8
		protected override void LoadData()
		{
			this.ChannelType = base.ReadBinary();
			this.LocalChannelNumber = base.ReadUInt32();
			this.InitialWindowSize = base.ReadUInt32();
			this.MaximumPacketSize = base.ReadUInt32();
			this._infoBytes = base.ReadBytes();
			string @string = SshData.Ascii.GetString(this.ChannelType, 0, this.ChannelType.Length);
			if (@string == "session")
			{
				this.Info = new SessionChannelOpenInfo(this._infoBytes);
				return;
			}
			if (@string == "x11")
			{
				this.Info = new X11ChannelOpenInfo(this._infoBytes);
				return;
			}
			if (@string == "direct-tcpip")
			{
				this.Info = new DirectTcpipChannelInfo(this._infoBytes);
				return;
			}
			if (!(@string == "forwarded-tcpip"))
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Channel type '{0}' is not supported.", new object[]
				{
					@string
				}));
			}
			this.Info = new ForwardedTcpipChannelInfo(this._infoBytes);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001E1D7 File Offset: 0x0001C3D7
		protected override void SaveData()
		{
			base.WriteBinaryString(this.ChannelType);
			base.Write(this.LocalChannelNumber);
			base.Write(this.InitialWindowSize);
			base.Write(this.MaximumPacketSize);
			base.Write(this._infoBytes);
		}

		// Token: 0x04000313 RID: 787
		internal const byte MessageNumber = 90;

		// Token: 0x04000314 RID: 788
		private byte[] _infoBytes;
	}
}
