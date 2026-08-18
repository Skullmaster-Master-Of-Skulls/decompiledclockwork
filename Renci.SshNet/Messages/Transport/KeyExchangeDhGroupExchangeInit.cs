using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
	// Token: 0x020000D4 RID: 212
	[Message("SSH_MSG_KEX_DH_GEX_INIT", 32)]
	internal class KeyExchangeDhGroupExchangeInit : Message, IKeyExchangedAllowed
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001FC64 File Offset: 0x0001DE64
		public BigInteger E
		{
			get
			{
				return this._eBytes.ToBigInteger();
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001FC71 File Offset: 0x0001DE71
		protected override int BufferCapacity
		{
			get
			{
				return base.BufferCapacity + 4 + this._eBytes.Length;
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001FC84 File Offset: 0x0001DE84
		public KeyExchangeDhGroupExchangeInit(BigInteger clientExchangeValue)
		{
			this._eBytes = clientExchangeValue.ToByteArray().Reverse<byte>();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001FC9E File Offset: 0x0001DE9E
		protected override void LoadData()
		{
			this._eBytes = base.ReadBinary();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		protected override void SaveData()
		{
			base.WriteBinaryString(this._eBytes);
		}

		// Token: 0x0400039D RID: 925
		private byte[] _eBytes;
	}
}
