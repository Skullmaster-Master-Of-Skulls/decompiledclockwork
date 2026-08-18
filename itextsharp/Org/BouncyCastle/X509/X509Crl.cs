using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000425 RID: 1061
	public class X509Crl : X509ExtensionBase
	{
		// Token: 0x06002414 RID: 9236 RVA: 0x000DBE98 File Offset: 0x000DAE98
		public X509Crl(CertificateList c)
		{
			this.c = c;
			try
			{
				this.sigAlgName = X509SignatureUtilities.GetSignatureName(c.SignatureAlgorithm);
				if (c.SignatureAlgorithm.Parameters != null)
				{
					this.sigAlgParams = c.SignatureAlgorithm.Parameters.GetDerEncoded();
				}
				else
				{
					this.sigAlgParams = null;
				}
				this.isIndirect = this.IsIndirectCrl;
			}
			catch (Exception arg)
			{
				throw new CrlException("CRL contents invalid: " + arg);
			}
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000DBF20 File Offset: 0x000DAF20
		protected override X509Extensions GetX509Extensions()
		{
			if (this.Version != 2)
			{
				return null;
			}
			return this.c.TbsCertList.Extensions;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000DBF40 File Offset: 0x000DAF40
		public virtual byte[] GetEncoded()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.c.GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CrlException(ex.ToString());
			}
			return derEncoded;
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000DBF7C File Offset: 0x000DAF7C
		public virtual void Verify(AsymmetricKeyParameter publicKey)
		{
			if (!this.c.SignatureAlgorithm.Equals(this.c.TbsCertList.Signature))
			{
				throw new CrlException("Signature algorithm on CertificateList does not match TbsCertList.");
			}
			ISigner signer = SignerUtilities.GetSigner(this.SigAlgName);
			signer.Init(false, publicKey);
			byte[] tbsCertList = this.GetTbsCertList();
			signer.BlockUpdate(tbsCertList, 0, tbsCertList.Length);
			if (!signer.VerifySignature(this.GetSignature()))
			{
				throw new SignatureException("CRL does not verify with supplied public key.");
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x000DBFF5 File Offset: 0x000DAFF5
		public virtual int Version
		{
			get
			{
				return this.c.Version;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000DC002 File Offset: 0x000DB002
		public virtual X509Name IssuerDN
		{
			get
			{
				return this.c.Issuer;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000DC00F File Offset: 0x000DB00F
		public virtual DateTime ThisUpdate
		{
			get
			{
				return this.c.ThisUpdate.ToDateTime();
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x000DC021 File Offset: 0x000DB021
		public virtual DateTimeObject NextUpdate
		{
			get
			{
				if (this.c.NextUpdate != null)
				{
					return new DateTimeObject(this.c.NextUpdate.ToDateTime());
				}
				return null;
			}
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000DC048 File Offset: 0x000DB048
		private ISet LoadCrlEntries()
		{
			ISet set = new HashSet();
			IEnumerable revokedCertificateEnumeration = this.c.GetRevokedCertificateEnumeration();
			X509Name previousCertificateIssuer = this.IssuerDN;
			foreach (object obj in revokedCertificateEnumeration)
			{
				CrlEntry crlEntry = (CrlEntry)obj;
				X509CrlEntry x509CrlEntry = new X509CrlEntry(crlEntry, this.isIndirect, previousCertificateIssuer);
				set.Add(x509CrlEntry);
				previousCertificateIssuer = x509CrlEntry.GetCertificateIssuer();
			}
			return set;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000DC0D8 File Offset: 0x000DB0D8
		public virtual X509CrlEntry GetRevokedCertificate(BigInteger serialNumber)
		{
			IEnumerable revokedCertificateEnumeration = this.c.GetRevokedCertificateEnumeration();
			X509Name previousCertificateIssuer = this.IssuerDN;
			foreach (object obj in revokedCertificateEnumeration)
			{
				CrlEntry crlEntry = (CrlEntry)obj;
				X509CrlEntry x509CrlEntry = new X509CrlEntry(crlEntry, this.isIndirect, previousCertificateIssuer);
				if (serialNumber.Equals(crlEntry.UserCertificate.Value))
				{
					return x509CrlEntry;
				}
				previousCertificateIssuer = x509CrlEntry.GetCertificateIssuer();
			}
			return null;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000DC170 File Offset: 0x000DB170
		public virtual ISet GetRevokedCertificates()
		{
			ISet set = this.LoadCrlEntries();
			if (set.Count > 0)
			{
				return set;
			}
			return null;
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000DC190 File Offset: 0x000DB190
		public virtual byte[] GetTbsCertList()
		{
			byte[] derEncoded;
			try
			{
				derEncoded = this.c.TbsCertList.GetDerEncoded();
			}
			catch (Exception ex)
			{
				throw new CrlException(ex.ToString());
			}
			return derEncoded;
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000DC1D0 File Offset: 0x000DB1D0
		public virtual byte[] GetSignature()
		{
			return this.c.Signature.GetBytes();
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x000DC1E2 File Offset: 0x000DB1E2
		public virtual string SigAlgName
		{
			get
			{
				return this.sigAlgName;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x000DC1EA File Offset: 0x000DB1EA
		public virtual string SigAlgOid
		{
			get
			{
				return this.c.SignatureAlgorithm.ObjectID.Id;
			}
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000DC201 File Offset: 0x000DB201
		public virtual byte[] GetSigAlgParams()
		{
			return Arrays.Clone(this.sigAlgParams);
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000DC210 File Offset: 0x000DB210
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509Crl x509Crl = obj as X509Crl;
			return x509Crl != null && this.c.Equals(x509Crl.c);
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000DC240 File Offset: 0x000DB240
		public override int GetHashCode()
		{
			return this.c.GetHashCode();
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000DC250 File Offset: 0x000DB250
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("              Version: ").Append(this.Version).Append(newLine);
			stringBuilder.Append("             IssuerDN: ").Append(this.IssuerDN).Append(newLine);
			stringBuilder.Append("          This update: ").Append(this.ThisUpdate).Append(newLine);
			stringBuilder.Append("          Next update: ").Append(this.NextUpdate).Append(newLine);
			stringBuilder.Append("  Signature Algorithm: ").Append(this.SigAlgName).Append(newLine);
			byte[] signature = this.GetSignature();
			stringBuilder.Append("            Signature: ");
			stringBuilder.Append(Hex.ToHexString(signature, 0, 20)).Append(newLine);
			for (int i = 20; i < signature.Length; i += 20)
			{
				int length = Math.Min(20, signature.Length - i);
				stringBuilder.Append("                       ");
				stringBuilder.Append(Hex.ToHexString(signature, i, length)).Append(newLine);
			}
			X509Extensions extensions = this.c.TbsCertList.Extensions;
			if (extensions != null)
			{
				IEnumerator enumerator = extensions.ExtensionOids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("           Extensions: ").Append(newLine);
				}
				for (;;)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)enumerator.Current;
					X509Extension extension = extensions.GetExtension(derObjectIdentifier);
					if (extension.Value != null)
					{
						Asn1Object asn1Object = X509ExtensionUtilities.FromExtensionValue(extension.Value);
						stringBuilder.Append("                       critical(").Append(extension.IsCritical).Append(") ");
						try
						{
							if (derObjectIdentifier.Equals(X509Extensions.CrlNumber))
							{
								stringBuilder.Append(new CrlNumber(DerInteger.GetInstance(asn1Object).PositiveValue)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.DeltaCrlIndicator))
							{
								stringBuilder.Append("Base CRL: " + new CrlNumber(DerInteger.GetInstance(asn1Object).PositiveValue)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.IssuingDistributionPoint))
							{
								stringBuilder.Append(IssuingDistributionPoint.GetInstance((Asn1Sequence)asn1Object)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.CrlDistributionPoints))
							{
								stringBuilder.Append(CrlDistPoint.GetInstance((Asn1Sequence)asn1Object)).Append(newLine);
							}
							else if (derObjectIdentifier.Equals(X509Extensions.FreshestCrl))
							{
								stringBuilder.Append(CrlDistPoint.GetInstance((Asn1Sequence)asn1Object)).Append(newLine);
							}
							else
							{
								stringBuilder.Append(derObjectIdentifier.Id);
								stringBuilder.Append(" value = ").Append(Asn1Dump.DumpAsString(asn1Object)).Append(newLine);
							}
							goto IL_2EC;
						}
						catch (Exception)
						{
							stringBuilder.Append(derObjectIdentifier.Id);
							stringBuilder.Append(" value = ").Append("*****").Append(newLine);
							goto IL_2EC;
						}
						goto IL_2E4;
					}
					goto IL_2E4;
					IL_2EC:
					if (!enumerator.MoveNext())
					{
						break;
					}
					continue;
					IL_2E4:
					stringBuilder.Append(newLine);
					goto IL_2EC;
				}
			}
			ISet revokedCertificates = this.GetRevokedCertificates();
			if (revokedCertificates != null)
			{
				foreach (object obj in revokedCertificates)
				{
					X509CrlEntry value = (X509CrlEntry)obj;
					stringBuilder.Append(value);
					stringBuilder.Append(newLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000DC5E8 File Offset: 0x000DB5E8
		public virtual bool IsRevoked(X509Certificate cert)
		{
			CrlEntry[] revokedCertificates = this.c.GetRevokedCertificates();
			if (revokedCertificates != null)
			{
				BigInteger serialNumber = cert.SerialNumber;
				for (int i = 0; i < revokedCertificates.Length; i++)
				{
					if (revokedCertificates[i].UserCertificate.Value.Equals(serialNumber))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x000DC634 File Offset: 0x000DB634
		protected virtual bool IsIndirectCrl
		{
			get
			{
				Asn1OctetString extensionValue = this.GetExtensionValue(X509Extensions.IssuingDistributionPoint);
				bool result = false;
				try
				{
					if (extensionValue != null)
					{
						result = IssuingDistributionPoint.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue)).IsIndirectCrl;
					}
				}
				catch (Exception arg)
				{
					throw new CrlException("Exception reading IssuingDistributionPoint" + arg);
				}
				return result;
			}
		}

		// Token: 0x04001915 RID: 6421
		private readonly CertificateList c;

		// Token: 0x04001916 RID: 6422
		private readonly string sigAlgName;

		// Token: 0x04001917 RID: 6423
		private readonly byte[] sigAlgParams;

		// Token: 0x04001918 RID: 6424
		private readonly bool isIndirect;
	}
}
