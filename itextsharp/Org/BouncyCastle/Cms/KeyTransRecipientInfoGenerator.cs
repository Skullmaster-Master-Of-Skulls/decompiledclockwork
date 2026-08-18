using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000135 RID: 309
	internal class KeyTransRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0003F964 File Offset: 0x0003E964
		internal KeyTransRecipientInfoGenerator()
		{
		}

		// Token: 0x17000248 RID: 584
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0003F96C File Offset: 0x0003E96C
		internal X509Certificate RecipientCert
		{
			set
			{
				try
				{
					this.recipientTbsCert = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(value.GetTbsCertificate()));
				}
				catch (Exception)
				{
					throw new ArgumentException("can't extract TBS structure from this cert");
				}
				this.recipientPublicKey = value.GetPublicKey();
				this.info = this.recipientTbsCert.SubjectPublicKeyInfo;
			}
		}

		// Token: 0x17000249 RID: 585
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x0003F9CC File Offset: 0x0003E9CC
		internal AsymmetricKeyParameter RecipientPublicKey
		{
			set
			{
				this.recipientPublicKey = value;
				try
				{
					this.info = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(this.recipientPublicKey);
				}
				catch (IOException)
				{
					throw new ArgumentException("can't extract key algorithm from this key");
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0003FA10 File Offset: 0x0003EA10
		internal Asn1OctetString SubjectKeyIdentifier
		{
			set
			{
				this.subjectKeyIdentifier = value;
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0003FA1C File Offset: 0x0003EA1C
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			AlgorithmIdentifier algorithmID = this.info.AlgorithmID;
			IWrapper wrapper = KeyTransRecipientInfoGenerator.Helper.CreateWrapper(algorithmID.ObjectID.Id);
			wrapper.Init(true, new ParametersWithRandom(this.recipientPublicKey, random));
			byte[] str = wrapper.Wrap(key, 0, key.Length);
			RecipientIdentifier rid;
			if (this.recipientTbsCert != null)
			{
				IssuerAndSerialNumber id = new IssuerAndSerialNumber(this.recipientTbsCert.Issuer, this.recipientTbsCert.SerialNumber.Value);
				rid = new RecipientIdentifier(id);
			}
			else
			{
				rid = new RecipientIdentifier(this.subjectKeyIdentifier);
			}
			return new RecipientInfo(new KeyTransRecipientInfo(rid, algorithmID, new DerOctetString(str)));
		}

		// Token: 0x040008DD RID: 2269
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x040008DE RID: 2270
		private TbsCertificateStructure recipientTbsCert;

		// Token: 0x040008DF RID: 2271
		private AsymmetricKeyParameter recipientPublicKey;

		// Token: 0x040008E0 RID: 2272
		private Asn1OctetString subjectKeyIdentifier;

		// Token: 0x040008E1 RID: 2273
		private SubjectPublicKeyInfo info;
	}
}
