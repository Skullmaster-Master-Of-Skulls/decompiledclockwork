using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001D8 RID: 472
	[DataContract(Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity")]
	public class Claim
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x00044304 File Offset: 0x00042504
		private Claim(string claimType, object resource, string right, IEqualityComparer<Claim> comparer)
		{
			if (claimType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claimType");
			}
			if (claimType.Length <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("claimType", SR.GetString("ArgumentCannotBeEmptyString"));
			}
			if (right == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("right");
			}
			if (right.Length <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("right", SR.GetString("ArgumentCannotBeEmptyString"));
			}
			this.claimType = StringUtil.OptimizeString(claimType);
			this.resource = resource;
			this.right = StringUtil.OptimizeString(right);
			this.comparer = comparer;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000443AA File Offset: 0x000425AA
		public Claim(string claimType, object resource, string right) : this(claimType, resource, right, null)
		{
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000443B6 File Offset: 0x000425B6
		public static IEqualityComparer<Claim> DefaultComparer
		{
			get
			{
				return EqualityComparer<Claim>.Default;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000443BD File Offset: 0x000425BD
		public static Claim System
		{
			get
			{
				if (Claim.system == null)
				{
					Claim.system = new Claim(ClaimTypes.System, "System", Rights.Identity);
				}
				return Claim.system;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x000443E4 File Offset: 0x000425E4
		public object Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x000443EC File Offset: 0x000425EC
		public string ClaimType
		{
			get
			{
				return this.claimType;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x000443F4 File Offset: 0x000425F4
		public string Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x000443FC File Offset: 0x000425FC
		public static Claim CreateDnsClaim(string dns)
		{
			if (dns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dns");
			}
			return new Claim(ClaimTypes.Dns, dns, Rights.PossessProperty, ClaimComparer.Dns);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00044426 File Offset: 0x00042626
		public static Claim CreateDenyOnlyWindowsSidClaim(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sid");
			}
			return new Claim(ClaimTypes.DenyOnlySid, sid, Rights.PossessProperty);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00044451 File Offset: 0x00042651
		public static Claim CreateHashClaim(byte[] hash)
		{
			if (hash == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("hash");
			}
			return new Claim(ClaimTypes.Hash, SecurityUtils.CloneBuffer(hash), Rights.PossessProperty, ClaimComparer.Hash);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00044480 File Offset: 0x00042680
		public static Claim CreateMailAddressClaim(MailAddress mailAddress)
		{
			if (mailAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("mailAddress");
			}
			return new Claim(ClaimTypes.Email, mailAddress, Rights.PossessProperty);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x000444A5 File Offset: 0x000426A5
		public static Claim CreateNameClaim(string name)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			return new Claim(ClaimTypes.Name, name, Rights.PossessProperty);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x000444CA File Offset: 0x000426CA
		public static Claim CreateRsaClaim(RSA rsa)
		{
			if (rsa == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rsa");
			}
			return new Claim(ClaimTypes.Rsa, rsa, Rights.PossessProperty, ClaimComparer.Rsa);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x000444F4 File Offset: 0x000426F4
		public static Claim CreateSpnClaim(string spn)
		{
			if (spn == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("spn");
			}
			return new Claim(ClaimTypes.Spn, spn, Rights.PossessProperty);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00044519 File Offset: 0x00042719
		public static Claim CreateThumbprintClaim(byte[] thumbprint)
		{
			if (thumbprint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("thumbprint");
			}
			return new Claim(ClaimTypes.Thumbprint, SecurityUtils.CloneBuffer(thumbprint), Rights.PossessProperty, ClaimComparer.Thumbprint);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00044548 File Offset: 0x00042748
		public static Claim CreateUpnClaim(string upn)
		{
			if (upn == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("upn");
			}
			return new Claim(ClaimTypes.Upn, upn, Rights.PossessProperty, ClaimComparer.Upn);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00044572 File Offset: 0x00042772
		public static Claim CreateUriClaim(Uri uri)
		{
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
			}
			return new Claim(ClaimTypes.Uri, uri, Rights.PossessProperty);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0004459D File Offset: 0x0004279D
		public static Claim CreateWindowsSidClaim(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sid");
			}
			return new Claim(ClaimTypes.Sid, sid, Rights.PossessProperty);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000445C8 File Offset: 0x000427C8
		public static Claim CreateX500DistinguishedNameClaim(X500DistinguishedName x500DistinguishedName)
		{
			if (x500DistinguishedName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("x500DistinguishedName");
			}
			return new Claim(ClaimTypes.X500DistinguishedName, x500DistinguishedName, Rights.PossessProperty, ClaimComparer.X500DistinguishedName);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000445F2 File Offset: 0x000427F2
		public override bool Equals(object obj)
		{
			if (this.comparer == null)
			{
				this.comparer = ClaimComparer.GetComparer(this.claimType);
			}
			return this.comparer.Equals(this, obj as Claim);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0004461F File Offset: 0x0004281F
		public override int GetHashCode()
		{
			if (this.comparer == null)
			{
				this.comparer = ClaimComparer.GetComparer(this.claimType);
			}
			return this.comparer.GetHashCode(this);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00044646 File Offset: 0x00042846
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}: {1}", new object[]
			{
				this.right,
				this.claimType
			});
		}

		// Token: 0x04000D95 RID: 3477
		private static Claim system;

		// Token: 0x04000D96 RID: 3478
		[DataMember(Name = "ClaimType")]
		private string claimType;

		// Token: 0x04000D97 RID: 3479
		[DataMember(Name = "Resource")]
		private object resource;

		// Token: 0x04000D98 RID: 3480
		[DataMember(Name = "Right")]
		private string right;

		// Token: 0x04000D99 RID: 3481
		private IEqualityComparer<Claim> comparer;
	}
}
