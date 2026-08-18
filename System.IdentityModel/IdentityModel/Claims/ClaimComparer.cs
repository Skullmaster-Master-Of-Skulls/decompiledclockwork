using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001D9 RID: 473
	internal class ClaimComparer : IEqualityComparer<Claim>
	{
		// Token: 0x06000F87 RID: 3975 RVA: 0x0004466F File Offset: 0x0004286F
		private ClaimComparer(IEqualityComparer resourceComparer)
		{
			this.resourceComparer = resourceComparer;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00044680 File Offset: 0x00042880
		public static IEqualityComparer<Claim> GetComparer(string claimType)
		{
			if (claimType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claimType");
			}
			if (claimType == ClaimTypes.Dns)
			{
				return ClaimComparer.Dns;
			}
			if (claimType == ClaimTypes.Hash)
			{
				return ClaimComparer.Hash;
			}
			if (claimType == ClaimTypes.Rsa)
			{
				return ClaimComparer.Rsa;
			}
			if (claimType == ClaimTypes.Thumbprint)
			{
				return ClaimComparer.Thumbprint;
			}
			if (claimType == ClaimTypes.Upn)
			{
				return ClaimComparer.Upn;
			}
			if (claimType == ClaimTypes.X500DistinguishedName)
			{
				return ClaimComparer.X500DistinguishedName;
			}
			return ClaimComparer.Default;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00044717 File Offset: 0x00042917
		public static IEqualityComparer<Claim> Default
		{
			get
			{
				if (ClaimComparer.defaultComparer == null)
				{
					ClaimComparer.defaultComparer = new ClaimComparer(new ClaimComparer.ObjectComparer());
				}
				return ClaimComparer.defaultComparer;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00044734 File Offset: 0x00042934
		public static IEqualityComparer<Claim> Dns
		{
			get
			{
				if (ClaimComparer.dnsComparer == null)
				{
					ClaimComparer.dnsComparer = new ClaimComparer(StringComparer.OrdinalIgnoreCase);
				}
				return ClaimComparer.dnsComparer;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00044751 File Offset: 0x00042951
		public static IEqualityComparer<Claim> Hash
		{
			get
			{
				if (ClaimComparer.hashComparer == null)
				{
					ClaimComparer.hashComparer = new ClaimComparer(new ClaimComparer.BinaryObjectComparer());
				}
				return ClaimComparer.hashComparer;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0004476E File Offset: 0x0004296E
		public static IEqualityComparer<Claim> Rsa
		{
			get
			{
				if (ClaimComparer.rsaComparer == null)
				{
					ClaimComparer.rsaComparer = new ClaimComparer(new ClaimComparer.RsaObjectComparer());
				}
				return ClaimComparer.rsaComparer;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0004478B File Offset: 0x0004298B
		public static IEqualityComparer<Claim> Thumbprint
		{
			get
			{
				if (ClaimComparer.thumbprintComparer == null)
				{
					ClaimComparer.thumbprintComparer = new ClaimComparer(new ClaimComparer.BinaryObjectComparer());
				}
				return ClaimComparer.thumbprintComparer;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x000447A8 File Offset: 0x000429A8
		public static IEqualityComparer<Claim> Upn
		{
			get
			{
				if (ClaimComparer.upnComparer == null)
				{
					ClaimComparer.upnComparer = new ClaimComparer(new ClaimComparer.UpnObjectComparer());
				}
				return ClaimComparer.upnComparer;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x000447C5 File Offset: 0x000429C5
		public static IEqualityComparer<Claim> X500DistinguishedName
		{
			get
			{
				if (ClaimComparer.x500DistinguishedNameComparer == null)
				{
					ClaimComparer.x500DistinguishedNameComparer = new ClaimComparer(new ClaimComparer.X500DistinguishedNameObjectComparer());
				}
				return ClaimComparer.x500DistinguishedNameComparer;
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000447E4 File Offset: 0x000429E4
		public bool Equals(Claim claim1, Claim claim2)
		{
			return claim1 == claim2 || (claim1 != null && claim2 != null && !(claim1.ClaimType != claim2.ClaimType) && !(claim1.Right != claim2.Right) && this.resourceComparer.Equals(claim1.Resource, claim2.Resource));
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00044840 File Offset: 0x00042A40
		public int GetHashCode(Claim claim)
		{
			if (claim == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claim");
			}
			return claim.ClaimType.GetHashCode() ^ claim.Right.GetHashCode() ^ ((claim.Resource == null) ? 0 : this.resourceComparer.GetHashCode(claim.Resource));
		}

		// Token: 0x04000D9A RID: 3482
		private static IEqualityComparer<Claim> defaultComparer;

		// Token: 0x04000D9B RID: 3483
		private static IEqualityComparer<Claim> hashComparer;

		// Token: 0x04000D9C RID: 3484
		private static IEqualityComparer<Claim> dnsComparer;

		// Token: 0x04000D9D RID: 3485
		private static IEqualityComparer<Claim> rsaComparer;

		// Token: 0x04000D9E RID: 3486
		private static IEqualityComparer<Claim> thumbprintComparer;

		// Token: 0x04000D9F RID: 3487
		private static IEqualityComparer<Claim> upnComparer;

		// Token: 0x04000DA0 RID: 3488
		private static IEqualityComparer<Claim> x500DistinguishedNameComparer;

		// Token: 0x04000DA1 RID: 3489
		private IEqualityComparer resourceComparer;

		// Token: 0x0200029D RID: 669
		private class ObjectComparer : IEqualityComparer
		{
			// Token: 0x06001388 RID: 5000 RVA: 0x00052E4C File Offset: 0x0005104C
			bool IEqualityComparer.Equals(object obj1, object obj2)
			{
				return (obj1 == null && obj2 == null) || (obj1 != null && obj2 != null && obj1.Equals(obj2));
			}

			// Token: 0x06001389 RID: 5001 RVA: 0x00052E65 File Offset: 0x00051065
			int IEqualityComparer.GetHashCode(object obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.GetHashCode();
			}
		}

		// Token: 0x0200029E RID: 670
		private class BinaryObjectComparer : IEqualityComparer
		{
			// Token: 0x0600138B RID: 5003 RVA: 0x00052E74 File Offset: 0x00051074
			bool IEqualityComparer.Equals(object obj1, object obj2)
			{
				if (obj1 == obj2)
				{
					return true;
				}
				byte[] array = obj1 as byte[];
				byte[] array2 = obj2 as byte[];
				if (array == null || array2 == null)
				{
					return false;
				}
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != array2[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600138C RID: 5004 RVA: 0x00052EC0 File Offset: 0x000510C0
			int IEqualityComparer.GetHashCode(object obj)
			{
				byte[] array = obj as byte[];
				if (array == null)
				{
					return 0;
				}
				int num = 0;
				int num2 = 0;
				while (num2 < array.Length && num2 < 4)
				{
					num = (num << 8 | (int)array[num2]);
					num2++;
				}
				return num ^ array.Length;
			}
		}

		// Token: 0x0200029F RID: 671
		private class RsaObjectComparer : IEqualityComparer
		{
			// Token: 0x0600138E RID: 5006 RVA: 0x00052EFC File Offset: 0x000510FC
			bool IEqualityComparer.Equals(object obj1, object obj2)
			{
				if (obj1 == obj2)
				{
					return true;
				}
				RSA rsa = obj1 as RSA;
				RSA rsa2 = obj2 as RSA;
				if (rsa == null || rsa2 == null)
				{
					return false;
				}
				RSAParameters rsaparameters = rsa.ExportParameters(false);
				RSAParameters rsaparameters2 = rsa2.ExportParameters(false);
				if (rsaparameters.Modulus.Length != rsaparameters2.Modulus.Length || rsaparameters.Exponent.Length != rsaparameters2.Exponent.Length)
				{
					return false;
				}
				for (int i = 0; i < rsaparameters.Modulus.Length; i++)
				{
					if (rsaparameters.Modulus[i] != rsaparameters2.Modulus[i])
					{
						return false;
					}
				}
				for (int j = 0; j < rsaparameters.Exponent.Length; j++)
				{
					if (rsaparameters.Exponent[j] != rsaparameters2.Exponent[j])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600138F RID: 5007 RVA: 0x00052FB8 File Offset: 0x000511B8
			int IEqualityComparer.GetHashCode(object obj)
			{
				RSA rsa = obj as RSA;
				if (rsa == null)
				{
					return 0;
				}
				RSAParameters rsaparameters = rsa.ExportParameters(false);
				return rsaparameters.Modulus.Length ^ rsaparameters.Exponent.Length;
			}
		}

		// Token: 0x020002A0 RID: 672
		private class X500DistinguishedNameObjectComparer : IEqualityComparer
		{
			// Token: 0x06001391 RID: 5009 RVA: 0x00052FEA File Offset: 0x000511EA
			public X500DistinguishedNameObjectComparer()
			{
				this.binaryComparer = new ClaimComparer.BinaryObjectComparer();
			}

			// Token: 0x06001392 RID: 5010 RVA: 0x00053000 File Offset: 0x00051200
			bool IEqualityComparer.Equals(object obj1, object obj2)
			{
				if (obj1 == obj2)
				{
					return true;
				}
				X500DistinguishedName x500DistinguishedName = obj1 as X500DistinguishedName;
				X500DistinguishedName x500DistinguishedName2 = obj2 as X500DistinguishedName;
				return x500DistinguishedName != null && x500DistinguishedName2 != null && (StringComparer.Ordinal.Equals(x500DistinguishedName.Name, x500DistinguishedName2.Name) || this.binaryComparer.Equals(x500DistinguishedName.RawData, x500DistinguishedName2.RawData));
			}

			// Token: 0x06001393 RID: 5011 RVA: 0x0005305C File Offset: 0x0005125C
			int IEqualityComparer.GetHashCode(object obj)
			{
				X500DistinguishedName x500DistinguishedName = obj as X500DistinguishedName;
				if (x500DistinguishedName == null)
				{
					return 0;
				}
				return this.binaryComparer.GetHashCode(x500DistinguishedName.RawData);
			}

			// Token: 0x0400113B RID: 4411
			private IEqualityComparer binaryComparer;
		}

		// Token: 0x020002A1 RID: 673
		private class UpnObjectComparer : IEqualityComparer
		{
			// Token: 0x06001394 RID: 5012 RVA: 0x00053088 File Offset: 0x00051288
			bool IEqualityComparer.Equals(object obj1, object obj2)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(obj1, obj2))
				{
					return true;
				}
				string text = obj1 as string;
				string text2 = obj2 as string;
				SecurityIdentifier left;
				SecurityIdentifier right;
				return text != null && text2 != null && this.TryLookupSidFromName(text, out left) && this.TryLookupSidFromName(text2, out right) && left == right;
			}

			// Token: 0x06001395 RID: 5013 RVA: 0x000530DC File Offset: 0x000512DC
			int IEqualityComparer.GetHashCode(object obj)
			{
				string text = obj as string;
				if (text == null)
				{
					return 0;
				}
				SecurityIdentifier securityIdentifier;
				if (this.TryLookupSidFromName(text, out securityIdentifier))
				{
					return securityIdentifier.GetHashCode();
				}
				return StringComparer.OrdinalIgnoreCase.GetHashCode(text);
			}

			// Token: 0x06001396 RID: 5014 RVA: 0x00053114 File Offset: 0x00051314
			private bool TryLookupSidFromName(string upn, out SecurityIdentifier sid)
			{
				sid = null;
				try
				{
					NTAccount ntaccount = new NTAccount(upn);
					sid = (ntaccount.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier);
				}
				catch (IdentityNotMappedException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				return sid != null;
			}
		}
	}
}
