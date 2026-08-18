using System;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000083 RID: 131
	public class TlsMac
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x00016314 File Offset: 0x00015314
		internal TlsMac(IDigest digest, byte[] key_block, int offset, int len)
		{
			this.mac = new HMac(digest);
			KeyParameter parameters = new KeyParameter(key_block, offset, len);
			this.mac.Init(parameters);
			this.seqNo = 0L;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00016351 File Offset: 0x00015351
		internal int Size
		{
			get
			{
				return this.mac.GetMacSize();
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00016360 File Offset: 0x00015360
		internal byte[] CalculateMac(short type, byte[] message, int offset, int len)
		{
			byte[] array = new byte[13];
			long i;
			this.seqNo = (i = this.seqNo) + 1L;
			TlsUtilities.WriteUint64(i, array, 0);
			TlsUtilities.WriteUint8(type, array, 8);
			TlsUtilities.WriteVersion(array, 9);
			TlsUtilities.WriteUint16(len, array, 11);
			this.mac.BlockUpdate(array, 0, array.Length);
			this.mac.BlockUpdate(message, offset, len);
			return MacUtilities.DoFinal(this.mac);
		}

		// Token: 0x04000219 RID: 537
		private long seqNo;

		// Token: 0x0400021A RID: 538
		private HMac mac;
	}
}
