using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D5 RID: 213
	[Message("SSH_MSG_KEX_DH_GEX_REPLY", 33)]
	internal class KeyExchangeDhGroupExchangeReply : Message
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001FCBA File Offset: 0x0001DEBA
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0001FCC2 File Offset: 0x0001DEC2
		public byte[] HostKey { get; private set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001FCCB File Offset: 0x0001DECB
		public BigInteger F
		{
			get
			{
				return this._fBytes.ToBigInteger();
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0001FCE0 File Offset: 0x0001DEE0
		public byte[] Signature { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0001FCE9 File Offset: 0x0001DEE9
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.HostKey.Length + 4 + this._fBytes.Length + 4 + this.Signature.Length;
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001FD12 File Offset: 0x0001DF12
		protected override void LoadData()
		{
			this.HostKey = base.ReadBinary();
			this._fBytes = base.ReadBinary();
			this.Signature = base.ReadBinary();
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001FD38 File Offset: 0x0001DF38
		protected override void SaveData()
		{
			base.WriteBinaryString(this.HostKey);
			base.WriteBinaryString(this._fBytes);
			base.WriteBinaryString(this.Signature);
		}

		// Token: 0x0400039E RID: 926
		internal const byte MessageNumber = 33;

		// Token: 0x0400039F RID: 927
		private byte[] _fBytes;
	}
}
