using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000B7 RID: 183
	internal class X11ForwardingRequestInfo : RequestInfo
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0001ED88 File Offset: 0x0001CF88
		public override string RequestName
		{
			get
			{
				return "x11-req";
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0001ED8F File Offset: 0x0001CF8F
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x0001ED97 File Offset: 0x0001CF97
		public bool IsSingleConnection { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001EDA0 File Offset: 0x0001CFA0
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0001EDBB File Offset: 0x0001CFBB
		public string AuthenticationProtocol
		{
			get
			{
				return SshData.Ascii.GetString(this._authenticationProtocol, 0, this._authenticationProtocol.Length);
			}
			private set
			{
				this._authenticationProtocol = SshData.Ascii.GetBytes(value);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0001EDCE File Offset: 0x0001CFCE
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0001EDD6 File Offset: 0x0001CFD6
		public byte[] AuthenticationCookie { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0001EDDF File Offset: 0x0001CFDF
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0001EDE7 File Offset: 0x0001CFE7
		public uint ScreenNumber { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 1 + 4 + this._authenticationProtocol.Length + 4 + this.AuthenticationCookie.Length + 4;
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001E57E File Offset: 0x0001C77E
		public X11ForwardingRequestInfo()
		{
			base.WantReply = true;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001EE12 File Offset: 0x0001D012
		public X11ForwardingRequestInfo(bool isSingleConnection, string protocol, byte[] cookie, uint screenNumber) : this()
		{
			this.IsSingleConnection = isSingleConnection;
			this.AuthenticationProtocol = protocol;
			this.AuthenticationCookie = cookie;
			this.ScreenNumber = screenNumber;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001EE37 File Offset: 0x0001D037
		protected override void LoadData()
		{
			base.LoadData();
			this.IsSingleConnection = base.ReadBoolean();
			this._authenticationProtocol = base.ReadBinary();
			this.AuthenticationCookie = base.ReadBinary();
			this.ScreenNumber = base.ReadUInt32();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001EE6F File Offset: 0x0001D06F
		protected override void SaveData()
		{
			base.SaveData();
			base.Write(this.IsSingleConnection);
			base.WriteBinaryString(this._authenticationProtocol);
			base.WriteBinaryString(this.AuthenticationCookie);
			base.Write(this.ScreenNumber);
		}

		// Token: 0x0400034E RID: 846
		private byte[] _authenticationProtocol;

		// Token: 0x0400034F RID: 847
		public const string Name = "x11-req";
	}
}
