using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Policy;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001DF RID: 479
	public class X509CertificateClaimSet : ClaimSet, IIdentityInfo, IDisposable
	{
		// Token: 0x06000FE6 RID: 4070 RVA: 0x00045070 File Offset: 0x00043270
		public X509CertificateClaimSet(X509Certificate2 certificate) : this(certificate, true)
		{
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0004507A File Offset: 0x0004327A
		internal X509CertificateClaimSet(X509Certificate2 certificate, bool clone)
		{
			this.expirationTime = SecurityUtils.MinUtcDateTime;
			base..ctor();
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			this.certificate = (clone ? new X509Certificate2(certificate) : certificate);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000450B2 File Offset: 0x000432B2
		private X509CertificateClaimSet(X509CertificateClaimSet from) : this(from.X509Certificate, true)
		{
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000450C1 File Offset: 0x000432C1
		private X509CertificateClaimSet(X509ChainElementCollection elements, int index)
		{
			this.expirationTime = SecurityUtils.MinUtcDateTime;
			base..ctor();
			this.elements = elements;
			this.index = index;
			this.certificate = elements[index].Certificate;
		}

		// Token: 0x17000439 RID: 1081
		public override Claim this[int index]
		{
			get
			{
				this.ThrowIfDisposed();
				this.EnsureClaims();
				return this.claims[index];
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0004510E File Offset: 0x0004330E
		public override int Count
		{
			get
			{
				this.ThrowIfDisposed();
				this.EnsureClaims();
				return this.claims.Count;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00045127 File Offset: 0x00043327
		IIdentity IIdentityInfo.Identity
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.identity == null)
				{
					this.identity = new X509Identity(this.certificate, false, false);
				}
				return this.identity;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00045150 File Offset: 0x00043350
		public DateTime ExpirationTime
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.expirationTime == SecurityUtils.MinUtcDateTime)
				{
					this.expirationTime = this.certificate.NotAfter.ToUniversalTime();
				}
				return this.expirationTime;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00045194 File Offset: 0x00043394
		public override ClaimSet Issuer
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.issuer == null)
				{
					if (this.elements == null)
					{
						X509Chain x509Chain = new X509Chain();
						x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
						x509Chain.Build(this.certificate);
						this.index = 0;
						this.elements = x509Chain.ChainElements;
					}
					if (this.index + 1 < this.elements.Count)
					{
						this.issuer = new X509CertificateClaimSet(this.elements, this.index + 1);
						this.elements = null;
					}
					else if (StringComparer.OrdinalIgnoreCase.Equals(this.certificate.SubjectName.Name, this.certificate.IssuerName.Name))
					{
						this.issuer = this;
					}
					else
					{
						this.issuer = new X509CertificateClaimSet.X500DistinguishedNameClaimSet(this.certificate.IssuerName);
					}
				}
				return this.issuer;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00045274 File Offset: 0x00043474
		public X509Certificate2 X509Certificate
		{
			get
			{
				this.ThrowIfDisposed();
				return this.certificate;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00045282 File Offset: 0x00043482
		internal X509CertificateClaimSet Clone()
		{
			this.ThrowIfDisposed();
			return new X509CertificateClaimSet(this);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00045290 File Offset: 0x00043490
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				SecurityUtils.DisposeIfNecessary(this.identity);
				if (this.issuer != null && this.issuer != this)
				{
					SecurityUtils.DisposeIfNecessary(this.issuer as IDisposable);
				}
				if (this.elements != null)
				{
					for (int i = this.index + 1; i < this.elements.Count; i++)
					{
						SecurityUtils.ResetCertificate(this.elements[i].Certificate);
					}
				}
				SecurityUtils.ResetCertificate(this.certificate);
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00045320 File Offset: 0x00043520
		private IList<Claim> InitializeClaimsCore()
		{
			List<Claim> list = new List<Claim>();
			byte[] certHash = this.certificate.GetCertHash();
			list.Add(new Claim(ClaimTypes.Thumbprint, certHash, Rights.Identity));
			list.Add(new Claim(ClaimTypes.Thumbprint, certHash, Rights.PossessProperty));
			string text = this.certificate.SubjectName.Name;
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(Claim.CreateX500DistinguishedNameClaim(this.certificate.SubjectName));
			}
			list.AddRange(X509CertificateClaimSet.GetDnsClaims(this.certificate));
			text = this.certificate.GetNameInfo(X509NameType.SimpleName, false);
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(Claim.CreateNameClaim(text));
			}
			text = this.certificate.GetNameInfo(X509NameType.EmailName, false);
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(Claim.CreateMailAddressClaim(new MailAddress(text)));
			}
			text = this.certificate.GetNameInfo(X509NameType.UpnName, false);
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(Claim.CreateUpnClaim(text));
			}
			text = this.certificate.GetNameInfo(X509NameType.UrlName, false);
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(Claim.CreateUriClaim(new Uri(text)));
			}
			RSA rsa;
			if (LocalAppContextSwitches.DisableCngCertificates)
			{
				rsa = (this.certificate.PublicKey.Key as RSA);
			}
			else
			{
				rsa = CngLightup.GetRSAPublicKey(this.certificate);
			}
			if (rsa != null)
			{
				list.Add(Claim.CreateRsaClaim(rsa));
			}
			return list;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00045478 File Offset: 0x00043678
		private void EnsureClaims()
		{
			if (this.claims != null)
			{
				return;
			}
			this.claims = this.InitializeClaimsCore();
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00045490 File Offset: 0x00043690
		private static bool SupportedClaimType(string claimType)
		{
			return claimType == null || ClaimTypes.Thumbprint.Equals(claimType) || ClaimTypes.X500DistinguishedName.Equals(claimType) || ClaimTypes.Dns.Equals(claimType) || ClaimTypes.Name.Equals(claimType) || ClaimTypes.Email.Equals(claimType) || ClaimTypes.Upn.Equals(claimType) || ClaimTypes.Uri.Equals(claimType) || ClaimTypes.Rsa.Equals(claimType);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00045508 File Offset: 0x00043708
		public override IEnumerable<Claim> FindClaims(string claimType, string right)
		{
			this.ThrowIfDisposed();
			if (!X509CertificateClaimSet.SupportedClaimType(claimType) || !ClaimSet.SupportedRight(right))
			{
				yield break;
			}
			if (this.claims == null && ClaimTypes.Thumbprint.Equals(claimType))
			{
				if (right == null || Rights.Identity.Equals(right))
				{
					yield return new Claim(ClaimTypes.Thumbprint, this.certificate.GetCertHash(), Rights.Identity);
				}
				if (right == null || Rights.PossessProperty.Equals(right))
				{
					yield return new Claim(ClaimTypes.Thumbprint, this.certificate.GetCertHash(), Rights.PossessProperty);
				}
			}
			else if (this.claims == null && ClaimTypes.Dns.Equals(claimType))
			{
				if (right == null || Rights.PossessProperty.Equals(right))
				{
					foreach (Claim claim in X509CertificateClaimSet.GetDnsClaims(this.certificate))
					{
						yield return claim;
					}
					List<Claim>.Enumerator enumerator = default(List<Claim>.Enumerator);
				}
			}
			else
			{
				this.EnsureClaims();
				bool anyClaimType = claimType == null;
				bool anyRight = right == null;
				int num;
				for (int i = 0; i < this.claims.Count; i = num)
				{
					Claim claim2 = this.claims[i];
					if (claim2 != null && (anyClaimType || claimType.Equals(claim2.ClaimType)) && (anyRight || right.Equals(claim2.Right)))
					{
						yield return claim2;
					}
					num = i + 1;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00045528 File Offset: 0x00043728
		private static List<Claim> GetDnsClaims(X509Certificate2 cert)
		{
			List<Claim> list = new List<Claim>();
			string nameInfo = cert.GetNameInfo(X509NameType.DnsName, false);
			if (!string.IsNullOrEmpty(nameInfo))
			{
				list.Add(Claim.CreateDnsClaim(nameInfo));
			}
			if (!LocalAppContextSwitches.DisableMultipleDNSEntriesInSANCertificate && X509CertificateClaimSet.X509SubjectAlternativeNameConstants.SuccessfullyInitialized)
			{
				foreach (X509Extension x509Extension in cert.Extensions)
				{
					if (x509Extension.Oid.Value == "2.5.29.7" || x509Extension.Oid.Value == "2.5.29.17")
					{
						string text = x509Extension.Format(false);
						if (string.IsNullOrWhiteSpace(text))
						{
							break;
						}
						string[] array = text.Split(X509CertificateClaimSet.X509SubjectAlternativeNameConstants.SeparatorArray, StringSplitOptions.RemoveEmptyEntries);
						for (int i = 0; i < array.Length; i++)
						{
							string[] array2 = array[i].Split(new char[]
							{
								X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Delimiter
							});
							if (string.Equals(array2[0], X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Identifier))
							{
								list.Add(Claim.CreateDnsClaim(array2[1]));
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0004562B File Offset: 0x0004382B
		public override IEnumerator<Claim> GetEnumerator()
		{
			this.ThrowIfDisposed();
			this.EnsureClaims();
			return this.claims.GetEnumerator();
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00045644 File Offset: 0x00043844
		public override string ToString()
		{
			if (!this.disposed)
			{
				return SecurityUtils.ClaimSetToString(this);
			}
			return base.ToString();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0004565B File Offset: 0x0004385B
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
		}

		// Token: 0x04000DD2 RID: 3538
		private X509Certificate2 certificate;

		// Token: 0x04000DD3 RID: 3539
		private DateTime expirationTime;

		// Token: 0x04000DD4 RID: 3540
		private ClaimSet issuer;

		// Token: 0x04000DD5 RID: 3541
		private X509Identity identity;

		// Token: 0x04000DD6 RID: 3542
		private X509ChainElementCollection elements;

		// Token: 0x04000DD7 RID: 3543
		private IList<Claim> claims;

		// Token: 0x04000DD8 RID: 3544
		private int index;

		// Token: 0x04000DD9 RID: 3545
		private bool disposed;

		// Token: 0x020002A5 RID: 677
		private class X500DistinguishedNameClaimSet : DefaultClaimSet, IIdentityInfo
		{
			// Token: 0x060013A9 RID: 5033 RVA: 0x00053718 File Offset: 0x00051918
			public X500DistinguishedNameClaimSet(X500DistinguishedName x500DistinguishedName) : base(new Claim[0])
			{
				if (x500DistinguishedName == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("x500DistinguishedName");
				}
				this.identity = new X509Identity(x500DistinguishedName);
				List<Claim> list = new List<Claim>(2);
				list.Add(new Claim(ClaimTypes.X500DistinguishedName, x500DistinguishedName, Rights.Identity));
				list.Add(Claim.CreateX500DistinguishedNameClaim(x500DistinguishedName));
				base.Initialize(ClaimSet.Anonymous, list);
			}

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x060013AA RID: 5034 RVA: 0x00053785 File Offset: 0x00051985
			public IIdentity Identity
			{
				get
				{
					return this.identity;
				}
			}

			// Token: 0x04001152 RID: 4434
			private IIdentity identity;
		}

		// Token: 0x020002A6 RID: 678
		private static class X509SubjectAlternativeNameConstants
		{
			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x060013AB RID: 5035 RVA: 0x0005378D File Offset: 0x0005198D
			// (set) Token: 0x060013AC RID: 5036 RVA: 0x00053794 File Offset: 0x00051994
			public static string Identifier { get; private set; }

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x060013AD RID: 5037 RVA: 0x0005379C File Offset: 0x0005199C
			// (set) Token: 0x060013AE RID: 5038 RVA: 0x000537A3 File Offset: 0x000519A3
			public static char Delimiter { get; private set; }

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x060013AF RID: 5039 RVA: 0x000537AB File Offset: 0x000519AB
			// (set) Token: 0x060013B0 RID: 5040 RVA: 0x000537B2 File Offset: 0x000519B2
			public static string Separator { get; private set; }

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000537BA File Offset: 0x000519BA
			// (set) Token: 0x060013B2 RID: 5042 RVA: 0x000537C1 File Offset: 0x000519C1
			public static string[] SeparatorArray { get; private set; }

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x060013B3 RID: 5043 RVA: 0x000537C9 File Offset: 0x000519C9
			// (set) Token: 0x060013B4 RID: 5044 RVA: 0x000537D0 File Offset: 0x000519D0
			public static bool SuccessfullyInitialized { get; private set; }

			// Token: 0x060013B5 RID: 5045 RVA: 0x000537D8 File Offset: 0x000519D8
			static X509SubjectAlternativeNameConstants()
			{
				byte[] rawData = new byte[]
				{
					48,
					36,
					130,
					21,
					110,
					111,
					116,
					45,
					114,
					101,
					97,
					108,
					45,
					115,
					117,
					98,
					106,
					101,
					99,
					116,
					45,
					110,
					97,
					109,
					101,
					130,
					11,
					101,
					120,
					97,
					109,
					112,
					108,
					101,
					46,
					99,
					111,
					109
				};
				string text = string.Empty;
				try
				{
					X509Extension x509Extension = new X509Extension("2.5.29.7", rawData, true);
					text = x509Extension.Format(false);
					int num = text.IndexOf("not-real-subject-name") - 1;
					X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Delimiter = text[num];
					X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Identifier = text.Substring(0, num);
					int num2 = num + "not-real-subject-name".Length + 1;
					int num3 = 1;
					int num4 = num2 + 1;
					while (num4 < text.Length && text[num4] != X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Identifier[0])
					{
						num3++;
						num4++;
					}
					X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Separator = text.Substring(num2, num3);
					X509CertificateClaimSet.X509SubjectAlternativeNameConstants.SeparatorArray = new string[]
					{
						X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Separator
					};
					X509CertificateClaimSet.X509SubjectAlternativeNameConstants.SuccessfullyInitialized = true;
				}
				catch (Exception innerException)
				{
					X509CertificateClaimSet.X509SubjectAlternativeNameConstants.SuccessfullyInitialized = false;
					DiagnosticUtility.TraceHandledException(new FormatException(string.Format(CultureInfo.InvariantCulture, "There was an error parsing the SubjectAlternativeNames: '{0}'. See inner exception for more details.{1}Detected values were: Identifier: '{2}'; Delimiter:'{3}'; Separator:'{4}'", new object[]
					{
						text,
						Environment.NewLine,
						X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Identifier,
						X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Delimiter,
						X509CertificateClaimSet.X509SubjectAlternativeNameConstants.Separator
					}), innerException), TraceEventType.Warning);
				}
			}

			// Token: 0x04001153 RID: 4435
			public const string SanOid = "2.5.29.7";

			// Token: 0x04001154 RID: 4436
			public const string San2Oid = "2.5.29.17";
		}
	}
}
