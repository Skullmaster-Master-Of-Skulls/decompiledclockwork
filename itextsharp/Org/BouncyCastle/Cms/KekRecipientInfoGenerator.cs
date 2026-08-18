using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002AF RID: 687
	internal class KekRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x060019F9 RID: 6649 RVA: 0x0009A5CB File Offset: 0x000995CB
		internal KekRecipientInfoGenerator()
		{
		}

		// Token: 0x170004AC RID: 1196
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x0009A5D3 File Offset: 0x000995D3
		internal KekIdentifier KekIdentifier
		{
			set
			{
				this.secKeyId = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x0009A5DC File Offset: 0x000995DC
		internal string WrapAlgorithm
		{
			set
			{
				this.wrapAlgorithm = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x0009A5E5 File Offset: 0x000995E5
		internal KeyParameter WrapKey
		{
			set
			{
				this.wrapKey = value;
				this.keyEncAlg = KekRecipientInfoGenerator.DetermineKeyEncAlg(this.wrapAlgorithm, this.wrapKey);
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0009A608 File Offset: 0x00099608
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			IWrapper wrapper = KekRecipientInfoGenerator.Helper.CreateWrapper(this.keyEncAlg.ObjectID.Id);
			wrapper.Init(true, new ParametersWithRandom(this.wrapKey, random));
			Asn1OctetString encryptedKey = new DerOctetString(wrapper.Wrap(key, 0, key.Length));
			return new RecipientInfo(new KekRecipientInfo(this.secKeyId, this.keyEncAlg, encryptedKey));
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0009A674 File Offset: 0x00099674
		private static AlgorithmIdentifier DetermineKeyEncAlg(string algorithm, KeyParameter key)
		{
			if (algorithm.StartsWith("DES"))
			{
				return new AlgorithmIdentifier(PkcsObjectIdentifiers.IdAlgCms3DesWrap, DerNull.Instance);
			}
			if (algorithm.StartsWith("RC2"))
			{
				return new AlgorithmIdentifier(PkcsObjectIdentifiers.IdAlgCmsRC2Wrap, new DerInteger(58));
			}
			if (algorithm.StartsWith("AES"))
			{
				int num = key.GetKey().Length * 8;
				DerObjectIdentifier objectID;
				if (num == 128)
				{
					objectID = NistObjectIdentifiers.IdAes128Wrap;
				}
				else if (num == 192)
				{
					objectID = NistObjectIdentifiers.IdAes192Wrap;
				}
				else
				{
					if (num != 256)
					{
						throw new ArgumentException("illegal keysize in AES");
					}
					objectID = NistObjectIdentifiers.IdAes256Wrap;
				}
				return new AlgorithmIdentifier(objectID);
			}
			if (algorithm.StartsWith("SEED"))
			{
				return new AlgorithmIdentifier(KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap);
			}
			if (algorithm.StartsWith("CAMELLIA"))
			{
				int num2 = key.GetKey().Length * 8;
				DerObjectIdentifier objectID2;
				if (num2 == 128)
				{
					objectID2 = NttObjectIdentifiers.IdCamellia128Wrap;
				}
				else if (num2 == 192)
				{
					objectID2 = NttObjectIdentifiers.IdCamellia192Wrap;
				}
				else
				{
					if (num2 != 256)
					{
						throw new ArgumentException("illegal keysize in Camellia");
					}
					objectID2 = NttObjectIdentifiers.IdCamellia256Wrap;
				}
				return new AlgorithmIdentifier(objectID2);
			}
			throw new ArgumentException("unknown algorithm");
		}

		// Token: 0x04001150 RID: 4432
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x04001151 RID: 4433
		private KekIdentifier secKeyId;

		// Token: 0x04001152 RID: 4434
		private string wrapAlgorithm;

		// Token: 0x04001153 RID: 4435
		private KeyParameter wrapKey;

		// Token: 0x04001154 RID: 4436
		private AlgorithmIdentifier keyEncAlg;
	}
}
