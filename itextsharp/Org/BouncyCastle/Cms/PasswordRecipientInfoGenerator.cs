using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200061C RID: 1564
	internal class PasswordRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x0600352E RID: 13614 RVA: 0x0014A42F File Offset: 0x0014942F
		internal PasswordRecipientInfoGenerator()
		{
		}

		// Token: 0x1700092F RID: 2351
		// (set) Token: 0x0600352F RID: 13615 RVA: 0x0014A437 File Offset: 0x00149437
		internal AlgorithmIdentifier DerivationAlg
		{
			set
			{
				this.derivationAlg = value;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (set) Token: 0x06003530 RID: 13616 RVA: 0x0014A440 File Offset: 0x00149440
		internal string WrapAlgorithm
		{
			set
			{
				this.wrapAlgorithm = value;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (set) Token: 0x06003531 RID: 13617 RVA: 0x0014A449 File Offset: 0x00149449
		internal KeyParameter WrapKey
		{
			set
			{
				this.wrapKey = value;
			}
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x0014A454 File Offset: 0x00149454
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			string rfc3211WrapperName = PasswordRecipientInfoGenerator.Helper.GetRfc3211WrapperName(this.wrapAlgorithm);
			IWrapper wrapper = PasswordRecipientInfoGenerator.Helper.CreateWrapper(rfc3211WrapperName);
			int num = rfc3211WrapperName.StartsWith("DESEDE") ? 8 : 16;
			byte[] array = new byte[num];
			random.NextBytes(array);
			ICipherParameters parameters = new ParametersWithIV(this.wrapKey, array);
			wrapper.Init(true, new ParametersWithRandom(parameters, random));
			Asn1OctetString encryptedKey = new DerOctetString(wrapper.Wrap(key, 0, key.Length));
			DerSequence parameters2 = new DerSequence(new Asn1Encodable[]
			{
				new DerObjectIdentifier(this.wrapAlgorithm),
				new DerOctetString(array)
			});
			AlgorithmIdentifier keyEncryptionAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdAlgPwriKek, parameters2);
			return new RecipientInfo(new PasswordRecipientInfo(this.derivationAlg, keyEncryptionAlgorithm, encryptedKey));
		}

		// Token: 0x0400238F RID: 9103
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x04002390 RID: 9104
		private AlgorithmIdentifier derivationAlg;

		// Token: 0x04002391 RID: 9105
		private string wrapAlgorithm;

		// Token: 0x04002392 RID: 9106
		private KeyParameter wrapKey;
	}
}
