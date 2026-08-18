using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D7 RID: 215
	[Message("SSH_MSG_KEXDH_INIT", 30)]
	internal class KeyExchangeDhInitMessage : Message, IKeyExchangedAllowed
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0001FE08 File Offset: 0x0001E008
		public BigInteger E
		{
			get
			{
				return this._eBytes.ToBigInteger();
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0001FE15 File Offset: 0x0001E015
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._eBytes.Length;
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001FE28 File Offset: 0x0001E028
		public KeyExchangeDhInitMessage(BigInteger clientExchangeValue)
		{
			this._eBytes = clientExchangeValue.ToByteArray().Reverse<byte>();
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001FE42 File Offset: 0x0001E042
		protected override void LoadData()
		{
			base.ResetReader();
			this._eBytes = base.ReadBinary();
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001FE56 File Offset: 0x0001E056
		protected override void SaveData()
		{
			base.WriteBinaryString(this._eBytes);
		}

		// Token: 0x040003A6 RID: 934
		private byte[] _eBytes;
	}
}
