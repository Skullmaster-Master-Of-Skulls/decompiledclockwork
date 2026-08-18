using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200012D RID: 301
	public class RsaKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x000232AB File Offset: 0x000214AB
		public RsaKeyIdentifierClause(RSA rsa) : base(RsaKeyIdentifierClause.clauseType)
		{
			if (rsa == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rsa");
			}
			this.rsa = rsa;
			this.rsaParameters = rsa.ExportParameters(false);
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanCreateKey
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x000232DF File Offset: 0x000214DF
		public RSA Rsa
		{
			get
			{
				return this.rsa;
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000232E7 File Offset: 0x000214E7
		public override SecurityKey CreateKey()
		{
			if (this.rsaSecurityKey == null)
			{
				this.rsaSecurityKey = new RsaSecurityKey(this.rsa);
			}
			return this.rsaSecurityKey;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00023308 File Offset: 0x00021508
		public byte[] GetExponent()
		{
			return SecurityUtils.CloneBuffer(this.rsaParameters.Exponent);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002331A File Offset: 0x0002151A
		public byte[] GetModulus()
		{
			return SecurityUtils.CloneBuffer(this.rsaParameters.Modulus);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002332C File Offset: 0x0002152C
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			RsaKeyIdentifierClause rsaKeyIdentifierClause = keyIdentifierClause as RsaKeyIdentifierClause;
			return this == rsaKeyIdentifierClause || (rsaKeyIdentifierClause != null && rsaKeyIdentifierClause.Matches(this.rsa));
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00023358 File Offset: 0x00021558
		public bool Matches(RSA rsa)
		{
			if (rsa == null)
			{
				return false;
			}
			RSAParameters rsaparameters = rsa.ExportParameters(false);
			return SecurityUtils.MatchesBuffer(this.rsaParameters.Modulus, rsaparameters.Modulus) && SecurityUtils.MatchesBuffer(this.rsaParameters.Exponent, rsaparameters.Exponent);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000233A2 File Offset: 0x000215A2
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "RsaKeyIdentifierClause(Modulus = {0}, Exponent = {1})", new object[]
			{
				Convert.ToBase64String(this.rsaParameters.Modulus),
				Convert.ToBase64String(this.rsaParameters.Exponent)
			});
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000233DF File Offset: 0x000215DF
		public void WriteExponentAsBase64(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteBase64(this.rsaParameters.Exponent, 0, this.rsaParameters.Exponent.Length);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00023413 File Offset: 0x00021613
		public void WriteModulusAsBase64(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteBase64(this.rsaParameters.Modulus, 0, this.rsaParameters.Modulus.Length);
		}

		// Token: 0x04000B18 RID: 2840
		private static string clauseType = "http://www.w3.org/2000/09/xmldsig#RSAKeyValue";

		// Token: 0x04000B19 RID: 2841
		private readonly RSA rsa;

		// Token: 0x04000B1A RID: 2842
		private readonly RSAParameters rsaParameters;

		// Token: 0x04000B1B RID: 2843
		private RsaSecurityKey rsaSecurityKey;
	}
}
