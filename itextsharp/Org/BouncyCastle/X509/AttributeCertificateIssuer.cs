using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.X509
{
	// Token: 0x0200017A RID: 378
	public class AttributeCertificateIssuer : IX509Selector, ICloneable
	{
		// Token: 0x06000EC6 RID: 3782 RVA: 0x00055F58 File Offset: 0x00054F58
		public AttributeCertificateIssuer(AttCertIssuer issuer)
		{
			this.form = issuer.Issuer;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00055F6C File Offset: 0x00054F6C
		public AttributeCertificateIssuer(X509Name principal)
		{
			this.form = new V2Form(new GeneralNames(new GeneralName(principal)));
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00055F8C File Offset: 0x00054F8C
		private object[] GetNames()
		{
			GeneralNames generalNames;
			if (this.form is V2Form)
			{
				generalNames = ((V2Form)this.form).IssuerName;
			}
			else
			{
				generalNames = (GeneralNames)this.form;
			}
			GeneralName[] names = generalNames.GetNames();
			ArrayList arrayList = new ArrayList(names.Length);
			for (int num = 0; num != names.Length; num++)
			{
				if (names[num].TagNo == 4)
				{
					arrayList.Add(X509Name.GetInstance(names[num].Name));
				}
			}
			return arrayList.ToArray();
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00056008 File Offset: 0x00055008
		public X509Name[] GetPrincipals()
		{
			object[] names = this.GetNames();
			ArrayList arrayList = new ArrayList(names.Length);
			for (int num = 0; num != names.Length; num++)
			{
				if (names[num] is X509Name)
				{
					arrayList.Add(names[num]);
				}
			}
			return (X509Name[])arrayList.ToArray(typeof(X509Name));
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0005605C File Offset: 0x0005505C
		private bool MatchesDN(X509Name subject, GeneralNames targets)
		{
			GeneralName[] names = targets.GetNames();
			for (int num = 0; num != names.Length; num++)
			{
				GeneralName generalName = names[num];
				if (generalName.TagNo == 4)
				{
					try
					{
						if (X509Name.GetInstance(generalName.Name).Equivalent(subject))
						{
							return true;
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return false;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000560BC File Offset: 0x000550BC
		public object Clone()
		{
			return new AttributeCertificateIssuer(AttCertIssuer.GetInstance(this.form));
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000560D0 File Offset: 0x000550D0
		public bool Match(X509Certificate x509Cert)
		{
			if (!(this.form is V2Form))
			{
				return this.MatchesDN(x509Cert.SubjectDN, (GeneralNames)this.form);
			}
			V2Form v2Form = (V2Form)this.form;
			if (v2Form.BaseCertificateID != null)
			{
				return v2Form.BaseCertificateID.Serial.Value.Equals(x509Cert.SerialNumber) && this.MatchesDN(x509Cert.IssuerDN, v2Form.BaseCertificateID.Issuer);
			}
			return this.MatchesDN(x509Cert.SubjectDN, v2Form.IssuerName);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00056160 File Offset: 0x00055160
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (!(obj is AttributeCertificateIssuer))
			{
				return false;
			}
			AttributeCertificateIssuer attributeCertificateIssuer = (AttributeCertificateIssuer)obj;
			return this.form.Equals(attributeCertificateIssuer.form);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00056195 File Offset: 0x00055195
		public override int GetHashCode()
		{
			return this.form.GetHashCode();
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000561A2 File Offset: 0x000551A2
		public bool Match(object obj)
		{
			return obj is X509Certificate && this.Match((X509Certificate)obj);
		}

		// Token: 0x04000B04 RID: 2820
		internal readonly Asn1Encodable form;
	}
}
