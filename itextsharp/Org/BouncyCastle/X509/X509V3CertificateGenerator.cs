using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000179 RID: 377
	public class X509V3CertificateGenerator
	{
		// Token: 0x06000EAF RID: 3759 RVA: 0x00055C16 File Offset: 0x00054C16
		public X509V3CertificateGenerator()
		{
			this.tbsGen = new V3TbsCertificateGenerator();
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00055C34 File Offset: 0x00054C34
		public void Reset()
		{
			this.tbsGen = new V3TbsCertificateGenerator();
			this.extGenerator.Reset();
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00055C4C File Offset: 0x00054C4C
		public void SetSerialNumber(BigInteger serialNumber)
		{
			if (serialNumber.SignValue <= 0)
			{
				throw new ArgumentException("serial number must be a positive integer", "serialNumber");
			}
			this.tbsGen.SetSerialNumber(new DerInteger(serialNumber));
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00055C78 File Offset: 0x00054C78
		public void SetIssuerDN(X509Name issuer)
		{
			this.tbsGen.SetIssuer(issuer);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00055C86 File Offset: 0x00054C86
		public void SetNotBefore(DateTime date)
		{
			this.tbsGen.SetStartDate(new Time(date));
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00055C99 File Offset: 0x00054C99
		public void SetNotAfter(DateTime date)
		{
			this.tbsGen.SetEndDate(new Time(date));
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00055CAC File Offset: 0x00054CAC
		public void SetSubjectDN(X509Name subject)
		{
			this.tbsGen.SetSubject(subject);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00055CBA File Offset: 0x00054CBA
		public void SetPublicKey(AsymmetricKeyParameter publicKey)
		{
			this.tbsGen.SetSubjectPublicKeyInfo(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey));
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00055CD0 File Offset: 0x00054CD0
		public void SetSignatureAlgorithm(string signatureAlgorithm)
		{
			this.signatureAlgorithm = signatureAlgorithm;
			try
			{
				this.sigOid = X509Utilities.GetAlgorithmOid(signatureAlgorithm);
			}
			catch (Exception)
			{
				throw new ArgumentException("Unknown signature type requested: " + signatureAlgorithm);
			}
			this.sigAlgId = X509Utilities.GetSigAlgID(this.sigOid, signatureAlgorithm);
			this.tbsGen.SetSignature(this.sigAlgId);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00055D38 File Offset: 0x00054D38
		public void SetSubjectUniqueID(bool[] uniqueID)
		{
			this.tbsGen.SetSubjectUniqueID(this.booleanToBitString(uniqueID));
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00055D4C File Offset: 0x00054D4C
		public void SetIssuerUniqueID(bool[] uniqueID)
		{
			this.tbsGen.SetIssuerUniqueID(this.booleanToBitString(uniqueID));
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00055D60 File Offset: 0x00054D60
		private DerBitString booleanToBitString(bool[] id)
		{
			byte[] array = new byte[(id.Length + 7) / 8];
			for (int num = 0; num != id.Length; num++)
			{
				if (id[num])
				{
					byte[] array2 = array;
					int num2 = num / 8;
					array2[num2] |= (byte)(1 << 7 - num % 8);
				}
			}
			int num3 = id.Length % 8;
			if (num3 == 0)
			{
				return new DerBitString(array);
			}
			return new DerBitString(array, 8 - num3);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00055DC7 File Offset: 0x00054DC7
		public void AddExtension(string oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, extensionValue);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00055DDC File Offset: 0x00054DDC
		public void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, extensionValue);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00055DEC File Offset: 0x00054DEC
		public void AddExtension(string oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(new DerObjectIdentifier(oid), critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00055E06 File Offset: 0x00054E06
		public void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extensionValue)
		{
			this.extGenerator.AddExtension(oid, critical, new DerOctetString(extensionValue));
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00055E1B File Offset: 0x00054E1B
		public void CopyAndAddExtension(string oid, bool critical, X509Certificate cert)
		{
			this.CopyAndAddExtension(new DerObjectIdentifier(oid), critical, cert);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00055E2C File Offset: 0x00054E2C
		public void CopyAndAddExtension(DerObjectIdentifier oid, bool critical, X509Certificate cert)
		{
			Asn1OctetString extensionValue = cert.GetExtensionValue(oid);
			if (extensionValue == null)
			{
				throw new CertificateParsingException("extension " + oid + " not present");
			}
			try
			{
				Asn1Encodable extensionValue2 = X509ExtensionUtilities.FromExtensionValue(extensionValue);
				this.AddExtension(oid, critical, extensionValue2);
			}
			catch (Exception ex)
			{
				throw new CertificateParsingException(ex.Message, ex);
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00055E8C File Offset: 0x00054E8C
		public X509Certificate Generate(AsymmetricKeyParameter privateKey)
		{
			return this.Generate(privateKey, null);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00055E98 File Offset: 0x00054E98
		public X509Certificate Generate(AsymmetricKeyParameter privateKey, SecureRandom random)
		{
			TbsCertificateStructure tbsCertificateStructure = this.GenerateTbsCert();
			byte[] signatureForObject;
			try
			{
				signatureForObject = X509Utilities.GetSignatureForObject(this.sigOid, this.signatureAlgorithm, privateKey, random, tbsCertificateStructure);
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("exception encoding TBS cert", e);
			}
			X509Certificate result;
			try
			{
				result = this.GenerateJcaObject(tbsCertificateStructure, signatureForObject);
			}
			catch (CertificateParsingException e2)
			{
				throw new CertificateEncodingException("exception producing certificate object", e2);
			}
			return result;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00055F08 File Offset: 0x00054F08
		private TbsCertificateStructure GenerateTbsCert()
		{
			if (!this.extGenerator.IsEmpty)
			{
				this.tbsGen.SetExtensions(this.extGenerator.Generate());
			}
			return this.tbsGen.GenerateTbsCertificate();
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00055F38 File Offset: 0x00054F38
		private X509Certificate GenerateJcaObject(TbsCertificateStructure tbsCert, byte[] signature)
		{
			return new X509Certificate(new X509CertificateStructure(tbsCert, this.sigAlgId, new DerBitString(signature)));
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00055F51 File Offset: 0x00054F51
		public IEnumerable SignatureAlgNames
		{
			get
			{
				return X509Utilities.GetAlgNames();
			}
		}

		// Token: 0x04000AFF RID: 2815
		private readonly X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

		// Token: 0x04000B00 RID: 2816
		private V3TbsCertificateGenerator tbsGen;

		// Token: 0x04000B01 RID: 2817
		private DerObjectIdentifier sigOid;

		// Token: 0x04000B02 RID: 2818
		private AlgorithmIdentifier sigAlgId;

		// Token: 0x04000B03 RID: 2819
		private string signatureAlgorithm;
	}
}
