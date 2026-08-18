using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000197 RID: 407
	public class CmsSignedGenerator
	{
		// Token: 0x06000FCE RID: 4046 RVA: 0x0005BBCC File Offset: 0x0005ABCC
		static CmsSignedGenerator()
		{
			CmsSignedGenerator.noParams.Add(CmsSignedGenerator.EncryptionDsa);
			CmsSignedGenerator.noParams.Add(CmsSignedGenerator.EncryptionECDsaWithSha1);
			CmsSignedGenerator.noParams.Add(CmsSignedGenerator.EncryptionECDsaWithSha224);
			CmsSignedGenerator.noParams.Add(CmsSignedGenerator.EncryptionECDsaWithSha256);
			CmsSignedGenerator.noParams.Add(CmsSignedGenerator.EncryptionECDsaWithSha384);
			CmsSignedGenerator.noParams.Add(CmsSignedGenerator.EncryptionECDsaWithSha512);
			CmsSignedGenerator.ecAlgorithms.Add(CmsSignedGenerator.DigestSha1, CmsSignedGenerator.EncryptionECDsaWithSha1);
			CmsSignedGenerator.ecAlgorithms.Add(CmsSignedGenerator.DigestSha224, CmsSignedGenerator.EncryptionECDsaWithSha224);
			CmsSignedGenerator.ecAlgorithms.Add(CmsSignedGenerator.DigestSha256, CmsSignedGenerator.EncryptionECDsaWithSha256);
			CmsSignedGenerator.ecAlgorithms.Add(CmsSignedGenerator.DigestSha384, CmsSignedGenerator.EncryptionECDsaWithSha384);
			CmsSignedGenerator.ecAlgorithms.Add(CmsSignedGenerator.DigestSha512, CmsSignedGenerator.EncryptionECDsaWithSha512);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0005BDF5 File Offset: 0x0005ADF5
		protected CmsSignedGenerator() : this(new SecureRandom())
		{
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0005BE02 File Offset: 0x0005AE02
		protected CmsSignedGenerator(SecureRandom rand)
		{
			this.rand = rand;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0005BE40 File Offset: 0x0005AE40
		protected string GetEncOid(AsymmetricKeyParameter key, string digestOID)
		{
			string text;
			if (key is RsaKeyParameters)
			{
				if (!((RsaKeyParameters)key).IsPrivate)
				{
					throw new ArgumentException("Expected RSA private key");
				}
				text = CmsSignedGenerator.EncryptionRsa;
			}
			else if (key is DsaPrivateKeyParameters)
			{
				if (!digestOID.Equals(CmsSignedGenerator.DigestSha1))
				{
					throw new ArgumentException("can't mix DSA with anything but SHA1");
				}
				text = CmsSignedGenerator.EncryptionDsa;
			}
			else if (key is ECPrivateKeyParameters)
			{
				ECPrivateKeyParameters ecprivateKeyParameters = (ECPrivateKeyParameters)key;
				string algorithmName = ecprivateKeyParameters.AlgorithmName;
				if (algorithmName == "ECGOST3410")
				{
					text = CmsSignedGenerator.EncryptionECGost3410;
				}
				else
				{
					text = (string)CmsSignedGenerator.ecAlgorithms[digestOID];
					if (text == null)
					{
						throw new ArgumentException("can't mix ECDSA with anything but SHA family digests");
					}
				}
			}
			else
			{
				if (!(key is Gost3410PrivateKeyParameters))
				{
					throw new ArgumentException("Unknown algorithm in CmsSignedGenerator.GetEncOid");
				}
				text = CmsSignedGenerator.EncryptionGost3410;
			}
			return text;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0005BF08 File Offset: 0x0005AF08
		internal static AlgorithmIdentifier GetEncAlgorithmIdentifier(DerObjectIdentifier encOid, Asn1Encodable sigX509Parameters)
		{
			if (CmsSignedGenerator.noParams.Contains(encOid.Id))
			{
				return new AlgorithmIdentifier(encOid);
			}
			return new AlgorithmIdentifier(encOid, sigX509Parameters);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0005BF2C File Offset: 0x0005AF2C
		protected internal virtual IDictionary GetBaseParameters(DerObjectIdentifier contentType, AlgorithmIdentifier digAlgId, byte[] hash)
		{
			IDictionary dictionary = new Hashtable();
			dictionary[CmsAttributeTableParameter.ContentType] = contentType;
			dictionary[CmsAttributeTableParameter.DigestAlgorithmIdentifier] = digAlgId;
			if (hash != null)
			{
				dictionary[CmsAttributeTableParameter.Digest] = hash.Clone();
			}
			return dictionary;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0005BF6F File Offset: 0x0005AF6F
		protected internal virtual Asn1Set GetAttributeSet(Org.BouncyCastle.Asn1.Cms.AttributeTable attr)
		{
			if (attr != null)
			{
				return new DerSet(attr.ToAsn1EncodableVector());
			}
			return null;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0005BF81 File Offset: 0x0005AF81
		public void AddCertificates(IX509Store certStore)
		{
			this._certs.AddRange(CmsUtilities.GetCertificatesFromStore(certStore));
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0005BF94 File Offset: 0x0005AF94
		public void AddCrls(IX509Store crlStore)
		{
			this._crls.AddRange(CmsUtilities.GetCrlsFromStore(crlStore));
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0005BFA8 File Offset: 0x0005AFA8
		public void AddAttributeCertificates(IX509Store store)
		{
			try
			{
				foreach (object obj in store.GetMatches(null))
				{
					IX509AttributeCertificate ix509AttributeCertificate = (IX509AttributeCertificate)obj;
					this._certs.Add(new DerTaggedObject(false, 2, AttributeCertificate.GetInstance(Asn1Object.FromByteArray(ix509AttributeCertificate.GetEncoded()))));
				}
			}
			catch (Exception e)
			{
				throw new CmsException("error processing attribute certs", e);
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0005C03C File Offset: 0x0005B03C
		public void AddSigners(SignerInformationStore signerStore)
		{
			foreach (object obj in signerStore.GetSigners())
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				this._signers.Add(signerInformation);
				this.AddSignerCallback(signerInformation);
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0005C0A4 File Offset: 0x0005B0A4
		public IDictionary GetGeneratedDigests()
		{
			return new Hashtable(this._digests);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0005C0B1 File Offset: 0x0005B0B1
		internal virtual void AddSignerCallback(SignerInformation si)
		{
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0005C0B4 File Offset: 0x0005B0B4
		internal static SignerIdentifier GetSignerIdentifier(X509Certificate cert)
		{
			TbsCertificateStructure instance;
			try
			{
				instance = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(cert.GetTbsCertificate()));
			}
			catch (Exception)
			{
				throw new ArgumentException("can't extract TBS structure from this cert");
			}
			Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber id = new Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber(instance.Issuer, instance.SerialNumber.Value);
			return new SignerIdentifier(id);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0005C110 File Offset: 0x0005B110
		internal static SignerIdentifier GetSignerIdentifier(byte[] subjectKeyIdentifier)
		{
			return new SignerIdentifier(new DerOctetString(subjectKeyIdentifier));
		}

		// Token: 0x04000B66 RID: 2918
		public static readonly string Data = CmsObjectIdentifiers.Data.Id;

		// Token: 0x04000B67 RID: 2919
		public static readonly string DigestSha1 = OiwObjectIdentifiers.IdSha1.Id;

		// Token: 0x04000B68 RID: 2920
		public static readonly string DigestSha224 = NistObjectIdentifiers.IdSha224.Id;

		// Token: 0x04000B69 RID: 2921
		public static readonly string DigestSha256 = NistObjectIdentifiers.IdSha256.Id;

		// Token: 0x04000B6A RID: 2922
		public static readonly string DigestSha384 = NistObjectIdentifiers.IdSha384.Id;

		// Token: 0x04000B6B RID: 2923
		public static readonly string DigestSha512 = NistObjectIdentifiers.IdSha512.Id;

		// Token: 0x04000B6C RID: 2924
		public static readonly string DigestMD5 = PkcsObjectIdentifiers.MD5.Id;

		// Token: 0x04000B6D RID: 2925
		public static readonly string DigestGost3411 = CryptoProObjectIdentifiers.GostR3411.Id;

		// Token: 0x04000B6E RID: 2926
		public static readonly string DigestRipeMD128 = TeleTrusTObjectIdentifiers.RipeMD128.Id;

		// Token: 0x04000B6F RID: 2927
		public static readonly string DigestRipeMD160 = TeleTrusTObjectIdentifiers.RipeMD160.Id;

		// Token: 0x04000B70 RID: 2928
		public static readonly string DigestRipeMD256 = TeleTrusTObjectIdentifiers.RipeMD256.Id;

		// Token: 0x04000B71 RID: 2929
		public static readonly string EncryptionRsa = PkcsObjectIdentifiers.RsaEncryption.Id;

		// Token: 0x04000B72 RID: 2930
		public static readonly string EncryptionDsa = X9ObjectIdentifiers.IdDsaWithSha1.Id;

		// Token: 0x04000B73 RID: 2931
		public static readonly string EncryptionECDsa = X9ObjectIdentifiers.ECDsaWithSha1.Id;

		// Token: 0x04000B74 RID: 2932
		public static readonly string EncryptionRsaPss = PkcsObjectIdentifiers.IdRsassaPss.Id;

		// Token: 0x04000B75 RID: 2933
		public static readonly string EncryptionGost3410 = CryptoProObjectIdentifiers.GostR3410x94.Id;

		// Token: 0x04000B76 RID: 2934
		public static readonly string EncryptionECGost3410 = CryptoProObjectIdentifiers.GostR3410x2001.Id;

		// Token: 0x04000B77 RID: 2935
		private static readonly string EncryptionECDsaWithSha1 = X9ObjectIdentifiers.ECDsaWithSha1.Id;

		// Token: 0x04000B78 RID: 2936
		private static readonly string EncryptionECDsaWithSha224 = X9ObjectIdentifiers.ECDsaWithSha224.Id;

		// Token: 0x04000B79 RID: 2937
		private static readonly string EncryptionECDsaWithSha256 = X9ObjectIdentifiers.ECDsaWithSha256.Id;

		// Token: 0x04000B7A RID: 2938
		private static readonly string EncryptionECDsaWithSha384 = X9ObjectIdentifiers.ECDsaWithSha384.Id;

		// Token: 0x04000B7B RID: 2939
		private static readonly string EncryptionECDsaWithSha512 = X9ObjectIdentifiers.ECDsaWithSha512.Id;

		// Token: 0x04000B7C RID: 2940
		private static readonly ISet noParams = new HashSet();

		// Token: 0x04000B7D RID: 2941
		private static readonly Hashtable ecAlgorithms = new Hashtable();

		// Token: 0x04000B7E RID: 2942
		internal ArrayList _certs = new ArrayList();

		// Token: 0x04000B7F RID: 2943
		internal ArrayList _crls = new ArrayList();

		// Token: 0x04000B80 RID: 2944
		internal ArrayList _signers = new ArrayList();

		// Token: 0x04000B81 RID: 2945
		internal IDictionary _digests = new Hashtable();

		// Token: 0x04000B82 RID: 2946
		protected readonly SecureRandom rand;

		// Token: 0x02000198 RID: 408
		internal class DigOutputStream : BaseOutputStream
		{
			// Token: 0x06000FDD RID: 4061 RVA: 0x0005C11D File Offset: 0x0005B11D
			public DigOutputStream(IDigest dig)
			{
				this.dig = dig;
			}

			// Token: 0x06000FDE RID: 4062 RVA: 0x0005C12C File Offset: 0x0005B12C
			public override void WriteByte(byte b)
			{
				this.dig.Update(b);
			}

			// Token: 0x06000FDF RID: 4063 RVA: 0x0005C13A File Offset: 0x0005B13A
			public override void Write(byte[] b, int off, int len)
			{
				this.dig.BlockUpdate(b, off, len);
			}

			// Token: 0x04000B83 RID: 2947
			private readonly IDigest dig;
		}

		// Token: 0x02000199 RID: 409
		internal class SigOutputStream : BaseOutputStream
		{
			// Token: 0x06000FE0 RID: 4064 RVA: 0x0005C14A File Offset: 0x0005B14A
			public SigOutputStream(ISigner sig)
			{
				this.sig = sig;
			}

			// Token: 0x06000FE1 RID: 4065 RVA: 0x0005C15C File Offset: 0x0005B15C
			public override void WriteByte(byte b)
			{
				try
				{
					this.sig.Update(b);
				}
				catch (SignatureException arg)
				{
					throw new CmsStreamException("signature problem: " + arg);
				}
			}

			// Token: 0x06000FE2 RID: 4066 RVA: 0x0005C19C File Offset: 0x0005B19C
			public override void Write(byte[] b, int off, int len)
			{
				try
				{
					this.sig.BlockUpdate(b, off, len);
				}
				catch (SignatureException arg)
				{
					throw new CmsStreamException("signature problem: " + arg);
				}
			}

			// Token: 0x04000B84 RID: 2948
			private readonly ISigner sig;
		}
	}
}
