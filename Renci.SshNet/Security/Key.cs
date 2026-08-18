using System;
using System.Collections.Generic;
using Renci.SshNet.Common;
using Renci.SshNet.Security.Cryptography;

namespace Renci.SshNet.Security
{
	// Token: 0x0200006C RID: 108
	public abstract class Key
	{
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600065D RID: 1629
		protected abstract DigitalSignature DigitalSignature { get; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600065E RID: 1630
		// (set) Token: 0x0600065F RID: 1631
		public abstract BigInteger[] Public { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000660 RID: 1632
		public abstract int KeyLength { get; }

		// Token: 0x06000661 RID: 1633 RVA: 0x00013F88 File Offset: 0x00012188
		protected Key(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			DerData derData = new DerData(data);
			derData.ReadBigInteger();
			List<BigInteger> list = new List<BigInteger>();
			while (!derData.IsEndOfData)
			{
				list.Add(derData.ReadBigInteger());
			}
			this._privateKey = list.ToArray();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000027FD File Offset: 0x000009FD
		protected Key()
		{
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00013FDF File Offset: 0x000121DF
		public byte[] Sign(byte[] data)
		{
			return this.DigitalSignature.Sign(data);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00013FED File Offset: 0x000121ED
		public bool VerifySignature(byte[] data, byte[] signature)
		{
			return this.DigitalSignature.Verify(data, signature);
		}

		// Token: 0x04000243 RID: 579
		protected BigInteger[] _privateKey;
	}
}
