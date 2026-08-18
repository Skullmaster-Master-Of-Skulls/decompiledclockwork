using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace System.Security.Claims
{
	// Token: 0x0200001E RID: 30
	internal static class ClaimsConversionHelper
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004474 File Offset: 0x00002674
		public static ClaimsIdentity CreateClaimsIdentityFromClaimSet(ClaimSet claimset, string authenticationType)
		{
			if (claimset == null)
			{
				throw new ArgumentNullException("claimSet");
			}
			string issuer = null;
			if (claimset.Issuer == null)
			{
				issuer = "LOCAL AUTHORITY";
			}
			else
			{
				foreach (Claim claim in claimset.Issuer.FindClaims(ClaimTypes.Name, Rights.Identity))
				{
					if (claim != null && claim.Resource is string)
					{
						issuer = (claim.Resource as string);
						break;
					}
				}
			}
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(authenticationType);
			for (int i = 0; i < claimset.Count; i++)
			{
				if (string.Equals(claimset[i].Right, Rights.PossessProperty, StringComparison.Ordinal))
				{
					claimsIdentity.AddClaim(ClaimsConversionHelper.CreateClaimFromWcfClaim(claimset[i], issuer));
				}
			}
			return claimsIdentity;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004554 File Offset: 0x00002754
		public static ClaimsIdentity CreateClaimsIdentityFromClaimSet(ClaimSet claimset)
		{
			return ClaimsConversionHelper.CreateClaimsIdentityFromClaimSet(claimset, null);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000455D File Offset: 0x0000275D
		public static Claim CreateClaimFromWcfClaim(Claim wcfClaim)
		{
			return ClaimsConversionHelper.CreateClaimFromWcfClaim(wcfClaim, null);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004568 File Offset: 0x00002768
		public static Claim CreateClaimFromWcfClaim(Claim wcfClaim, string issuer)
		{
			string type = null;
			string text = null;
			string valueType = "http://www.w3.org/2001/XMLSchema#string";
			string originalIssuer = issuer;
			string value = null;
			string value2 = null;
			if (wcfClaim == null)
			{
				throw new ArgumentNullException("claim");
			}
			if (wcfClaim.Resource == null)
			{
				throw new InvalidOperationException();
			}
			if (string.IsNullOrEmpty(issuer))
			{
				issuer = "LOCAL AUTHORITY";
			}
			if (wcfClaim.Resource is string)
			{
				ClaimsConversionHelper.AssignClaimFromStringResourceSysClaim(wcfClaim, out type, out text);
			}
			else
			{
				ClaimsConversionHelper.AssignClaimFromSysClaim(wcfClaim, out type, out text, out valueType, out value, out value2);
			}
			if (text == null)
			{
				throw new InvalidOperationException();
			}
			Claim claim = new Claim(type, text, valueType, issuer, originalIssuer);
			claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/format"] = value;
			claim.Properties["http://schemas.xmlsoap.org/ws/2005/05/identity/claimproperties/namequalifier"] = value2;
			return claim;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004618 File Offset: 0x00002818
		private static void AssignClaimFromStringResourceSysClaim(Claim claim, out string claimType, out string claimValue)
		{
			claimType = claim.ClaimType;
			claimValue = (string)claim.Resource;
			if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid"))
			{
				if (claim.Right == Rights.Identity)
				{
					claimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";
					return;
				}
				claimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004674 File Offset: 0x00002874
		private static void AssignClaimFromSysClaim(Claim claim, out string _type, out string _value, out string _valueType, out string samlNameIdentifierFormat, out string samlNameIdentifierNameQualifier)
		{
			samlNameIdentifierFormat = null;
			samlNameIdentifierNameQualifier = null;
			_type = null;
			_value = null;
			_valueType = null;
			if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid") && claim.Resource is SecurityIdentifier)
			{
				if (claim.Right == Rights.Identity)
				{
					_type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";
				}
				else
				{
					_type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";
				}
				_value = ((SecurityIdentifier)claim.Resource).Value;
				return;
			}
			if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress") && claim.Resource is MailAddress)
			{
				_type = claim.ClaimType;
				_value = ((MailAddress)claim.Resource).Address;
				return;
			}
			if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint") && claim.Resource is byte[])
			{
				_type = claim.ClaimType;
				_value = Convert.ToBase64String((byte[])claim.Resource);
				_valueType = "http://www.w3.org/2001/XMLSchema#base64Binary";
				return;
			}
			if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash") && claim.Resource is byte[])
			{
				_type = claim.ClaimType;
				_value = Convert.ToBase64String((byte[])claim.Resource);
				_valueType = "http://www.w3.org/2001/XMLSchema#base64Binary";
				return;
			}
			if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") && claim.Resource is SamlNameIdentifierClaimResource)
			{
				_type = claim.ClaimType;
				_value = ((SamlNameIdentifierClaimResource)claim.Resource).Name;
				if (((SamlNameIdentifierClaimResource)claim.Resource).Format != null)
				{
					samlNameIdentifierFormat = ((SamlNameIdentifierClaimResource)claim.Resource).Format;
				}
				if (((SamlNameIdentifierClaimResource)claim.Resource).NameQualifier != null)
				{
					samlNameIdentifierNameQualifier = ((SamlNameIdentifierClaimResource)claim.Resource).NameQualifier;
					return;
				}
			}
			else
			{
				if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname") && claim.Resource is X500DistinguishedName)
				{
					_type = claim.ClaimType;
					_value = ((X500DistinguishedName)claim.Resource).Name;
					_valueType = "urn:oasis:names:tc:xacml:1.0:data-type:x500Name";
					return;
				}
				if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri") && claim.Resource is Uri)
				{
					_type = claim.ClaimType;
					_value = ((Uri)claim.Resource).ToString();
					return;
				}
				if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa") && claim.Resource is RSA)
				{
					_type = claim.ClaimType;
					_value = ((RSA)claim.Resource).ToXmlString(false);
					_valueType = "http://www.w3.org/2000/09/xmldsig#RSAKeyValue";
					return;
				}
				if (StringComparer.Ordinal.Equals(claim.ClaimType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid") && claim.Resource is SecurityIdentifier)
				{
					_type = claim.ClaimType;
					_value = ((SecurityIdentifier)claim.Resource).Value;
				}
			}
		}
	}
}
