using System;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000543 RID: 1347
	public class TrustAnchor
	{
		// Token: 0x06002E47 RID: 11847 RVA: 0x0011E5A3 File Offset: 0x0011D5A3
		public TrustAnchor(X509Certificate trustedCert, byte[] nameConstraints)
		{
			if (trustedCert == null)
			{
				throw new ArgumentNullException("trustedCert");
			}
			this.trustedCert = trustedCert;
			this.pubKey = null;
			this.caName = null;
			this.caPrincipal = null;
			this.setNameConstraints(nameConstraints);
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x0011E5DC File Offset: 0x0011D5DC
		public TrustAnchor(X509Name caPrincipal, AsymmetricKeyParameter pubKey, byte[] nameConstraints)
		{
			if (caPrincipal == null)
			{
				throw new ArgumentNullException("caPrincipal");
			}
			if (pubKey == null)
			{
				throw new ArgumentNullException("pubKey");
			}
			this.trustedCert = null;
			this.caPrincipal = caPrincipal;
			this.caName = caPrincipal.ToString();
			this.pubKey = pubKey;
			this.setNameConstraints(nameConstraints);
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x0011E634 File Offset: 0x0011D634
		public TrustAnchor(string caName, AsymmetricKeyParameter pubKey, byte[] nameConstraints)
		{
			if (caName == null)
			{
				throw new ArgumentNullException("caName");
			}
			if (pubKey == null)
			{
				throw new ArgumentNullException("pubKey");
			}
			if (caName.Length == 0)
			{
				throw new ArgumentException("caName can not be an empty string");
			}
			this.caPrincipal = new X509Name(caName);
			this.pubKey = pubKey;
			this.caName = caName;
			this.trustedCert = null;
			this.setNameConstraints(nameConstraints);
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x0011E69E File Offset: 0x0011D69E
		public X509Certificate TrustedCert
		{
			get
			{
				return this.trustedCert;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x0011E6A6 File Offset: 0x0011D6A6
		public X509Name CA
		{
			get
			{
				return this.caPrincipal;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x0011E6AE File Offset: 0x0011D6AE
		public string CAName
		{
			get
			{
				return this.caName;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x0011E6B6 File Offset: 0x0011D6B6
		public AsymmetricKeyParameter CAPublicKey
		{
			get
			{
				return this.pubKey;
			}
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x0011E6BE File Offset: 0x0011D6BE
		private void setNameConstraints(byte[] bytes)
		{
			if (bytes == null)
			{
				this.ncBytes = null;
				this.nc = null;
				return;
			}
			this.ncBytes = (byte[])bytes.Clone();
			this.nc = NameConstraints.GetInstance(Asn1Object.FromByteArray(bytes));
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x0011E6F4 File Offset: 0x0011D6F4
		public byte[] GetNameConstraints
		{
			get
			{
				return Arrays.Clone(this.ncBytes);
			}
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x0011E704 File Offset: 0x0011D704
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.Append(newLine);
			if (this.pubKey != null)
			{
				stringBuilder.Append("  Trusted CA Public Key: ").Append(this.pubKey).Append(newLine);
				stringBuilder.Append("  Trusted CA Issuer Name: ").Append(this.caName).Append(newLine);
			}
			else
			{
				stringBuilder.Append("  Trusted CA cert: ").Append(this.TrustedCert).Append(newLine);
			}
			if (this.nc != null)
			{
				stringBuilder.Append("  Name Constraints: ").Append(this.nc).Append(newLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001FFD RID: 8189
		private readonly AsymmetricKeyParameter pubKey;

		// Token: 0x04001FFE RID: 8190
		private readonly string caName;

		// Token: 0x04001FFF RID: 8191
		private readonly X509Name caPrincipal;

		// Token: 0x04002000 RID: 8192
		private readonly X509Certificate trustedCert;

		// Token: 0x04002001 RID: 8193
		private byte[] ncBytes;

		// Token: 0x04002002 RID: 8194
		private NameConstraints nc;
	}
}
