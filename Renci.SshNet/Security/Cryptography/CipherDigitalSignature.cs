using System;
using Renci.SshNet.Common;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x0200007E RID: 126
	public abstract class CipherDigitalSignature : DigitalSignature
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x000153D8 File Offset: 0x000135D8
		protected CipherDigitalSignature(ObjectIdentifier oid, AsymmetricCipher cipher)
		{
			if (cipher == null)
			{
				throw new ArgumentNullException("cipher");
			}
			this._cipher = cipher;
			this._oid = oid;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000153FC File Offset: 0x000135FC
		public override bool Verify(byte[] input, byte[] signature)
		{
			byte[] right = this._cipher.Decrypt(signature);
			byte[] hashData = this.Hash(input);
			return this.DerEncode(hashData).IsEqualTo(right);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001542C File Offset: 0x0001362C
		public override byte[] Sign(byte[] input)
		{
			byte[] hashData = this.Hash(input);
			byte[] input2 = this.DerEncode(hashData);
			return this._cipher.Encrypt(input2).TrimLeadingZeros();
		}

		// Token: 0x060006E2 RID: 1762
		protected abstract byte[] Hash(byte[] input);

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001545C File Offset: 0x0001365C
		protected byte[] DerEncode(byte[] hashData)
		{
			DerData derData = new DerData();
			derData.Write(this._oid);
			derData.WriteNull();
			DerData derData2 = new DerData();
			derData2.Write(derData);
			derData2.Write(hashData);
			return derData2.Encode();
		}

		// Token: 0x04000265 RID: 613
		private readonly AsymmetricCipher _cipher;

		// Token: 0x04000266 RID: 614
		private readonly ObjectIdentifier _oid;
	}
}
