using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D8 RID: 216
	[Message("SSH_MSG_KEXDH_REPLY", 31)]
	public class KeyExchangeDhReplyMessage : Message
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001FE64 File Offset: 0x0001E064
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0001FE6C File Offset: 0x0001E06C
		public byte[] HostKey { get; private set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0001FE75 File Offset: 0x0001E075
		public BigInteger F
		{
			get
			{
				return this._fBytes.ToBigInteger();
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001FE82 File Offset: 0x0001E082
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x0001FE8A File Offset: 0x0001E08A
		public byte[] Signature { get; private set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001FE93 File Offset: 0x0001E093
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this.HostKey.Length + 4 + this._fBytes.Length + 4 + this.Signature.Length;
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001FEBC File Offset: 0x0001E0BC
		protected override void LoadData()
		{
			base.ResetReader();
			this.HostKey = base.ReadBinary();
			this._fBytes = base.ReadBinary();
			this.Signature = base.ReadBinary();
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		protected override void SaveData()
		{
			base.WriteBinaryString(this.HostKey);
			base.WriteBinaryString(this._fBytes);
			base.WriteBinaryString(this.Signature);
		}

		// Token: 0x040003A7 RID: 935
		private byte[] _fBytes;
	}
}
