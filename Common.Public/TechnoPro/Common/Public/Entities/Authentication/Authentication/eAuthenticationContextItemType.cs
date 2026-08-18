using System;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x0200049D RID: 1181
	[Serializable]
	public enum eAuthenticationContextItemType
	{
		// Token: 0x04001A93 RID: 6803
		[AuthenticationContextItemType("Unknown", "Not to be used")]
		Unknown,
		// Token: 0x04001A94 RID: 6804
		[AuthenticationContextItemType("ClockWork", "User will be authenticated using ClockWork login, or a report. Report must return 'authenticated' boolean column.", null, new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.ReportId,
			eAuthenticationContextItemParameter.BinPath,
			eAuthenticationContextItemParameter.ReportArgs,
			eAuthenticationContextItemParameter.ReportArgsInsecure,
			eAuthenticationContextItemParameter.StudentNoField,
			eAuthenticationContextItemParameter.EmailField,
			eAuthenticationContextItemParameter.UsernameField
		})]
		ClockWork,
		// Token: 0x04001A95 RID: 6805
		[AuthenticationContextItemType("Ldap", "User will be authenticated against an LDAP server", new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.Ldapserver,
			eAuthenticationContextItemParameter.LdapPort,
			eAuthenticationContextItemParameter.LdapDomain,
			eAuthenticationContextItemParameter.LdapAuthType,
			eAuthenticationContextItemParameter.LdapReturnAttribute,
			eAuthenticationContextItemParameter.LdapLookupAttribute
		}, new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.StudentNoField,
			eAuthenticationContextItemParameter.EmailField,
			eAuthenticationContextItemParameter.Isdoublebinding,
			eAuthenticationContextItemParameter.Ldappredomain,
			eAuthenticationContextItemParameter.Ldapprelookupattribute,
			eAuthenticationContextItemParameter.Ldappreusername,
			eAuthenticationContextItemParameter.Ldapprepassword,
			eAuthenticationContextItemParameter.Ldapprotocolversion,
			eAuthenticationContextItemParameter.Ldapusessl,
			eAuthenticationContextItemParameter.Ldapusetls,
			eAuthenticationContextItemParameter.Ldapdontverifyservercertificate
		})]
		Ldap,
		// Token: 0x04001A96 RID: 6806
		[AuthenticationContextItemType("Active Directory", "User will be authenticated against a Windows Active Directory server", null, new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.Ldapserver,
			eAuthenticationContextItemParameter.EmailField,
			eAuthenticationContextItemParameter.LdapLookupAttribute,
			eAuthenticationContextItemParameter.LdapReturnAttribute,
			eAuthenticationContextItemParameter.StudentNoField
		})]
		ActiveDirectory,
		// Token: 0x04001A97 RID: 6807
		[AuthenticationContextItemType("CAS", "NOTE: Make sure to enter the CAS settings located in the 'Login' section of the web settings.  The user will first be directed to an external (CAS) login page with a return url.  Once authentication has been passed, the CAS system will redirect the user back to ClockWork (ie. back to the return url).  ClockWork will receive a ticket string in the page parameters, it will then consume a CAS web service that will provide authentication information in exchange for the ticket and original redirect url.")]
		CAS,
		// Token: 0x04001A98 RID: 6808
		[AuthenticationContextItemType("Shibboleth", "The Shibboleth IIS provider must be installed on the ClockWork IIS server first - this will redirect the user (before they get to any ClockWork page) to the Shibboleth login page.  Once authentication has been passed Shibboleth will redirect the user back to ClockWork (ie. back to the originally requested ClockWork page).  ClockWork will receive authentication information directly in the server variables provided through IIS.", new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.UsernameField
		}, new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.EmailField,
			eAuthenticationContextItemParameter.StudentNoField
		})]
		Shibboleth,
		// Token: 0x04001A99 RID: 6809
		[AuthenticationContextItemType("Portal with hashing", "Portal provides username, datetime string, token.  Default hash type is PBKDF2. Remember to set the custom password setting to the secret key, 'Try to login first without credentials' setting should be true, and 'login url' setting should be set to portal url for failed logins.", new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.UsernameField,
			eAuthenticationContextItemParameter.DateField,
			eAuthenticationContextItemParameter.TokenField,
			eAuthenticationContextItemParameter.HashType
		}, new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.ExtraField,
			eAuthenticationContextItemParameter.StudentNoField,
			eAuthenticationContextItemParameter.EmailField,
			eAuthenticationContextItemParameter.OverrideTokenTimeout,
			eAuthenticationContextItemParameter.HashingUsesHexEncoding,
			eAuthenticationContextItemParameter.WholeTokenIsBase64Encoded
		})]
		Portal,
		// Token: 0x04001A9A RID: 6810
		[Obsolete("Use AuthenticationContextItemTypeAttribute.ClockWork with ReportId instead")]
		[AuthenticationContextItemType("Custom (deprecated)", "Deprecated - User will be authenticated using the /custom/login/customlogin.ascx.cs custom page code.  This authentication option has been deprecated; please use ClockWork with report as a repalcement", IsHidden = true)]
		Custom,
		// Token: 0x04001A9B RID: 6811
		[AuthenticationContextItemType("PortalGuard", "The user will be automatically redirected from the ClockWork login page to /user/misc/LoginPG.aspx, which will create a SAML request and pass that along with the relaystate (the page the user was trying to get to) to the external PortalGuard school login page.  Note that the user never lands on LoginPG.aspx - it contains a javascript function to automatically redirect the user (using a post with samlrequest and relaystate) to the external login page.  Once the user successfully authenticates via the external login page, they will be redirected back to /user/misc/pg.aspx.  This page will parse the provided samlresponse and relaystate, and forward the user to the relaystate url upon successful saml request parsing.  If the parsing fails the user will be redirected back to login.aspx, then to LoginPG.aspx (both will auto redirect so the user doesn't land on either page), then to the external login page.", new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.UsernameField,
			eAuthenticationContextItemParameter.TokenIssuer,
			eAuthenticationContextItemParameter.RequestIssuer,
			eAuthenticationContextItemParameter.IdpUrl
		}, new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.StudentNoField,
			eAuthenticationContextItemParameter.EmailField
		})]
		PortalGuard,
		// Token: 0x04001A9C RID: 6812
		[AuthenticationContextItemType("ADFS", "The user will be automatically redirected to the ADFS login page where they will enter their username and password.  After successful authentication they will be redirected back to ClockWork (custom/login/logins.aspx) with a saml response token.", new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.UsernameField,
			eAuthenticationContextItemParameter.TokenIssuer,
			eAuthenticationContextItemParameter.RequestIssuer,
			eAuthenticationContextItemParameter.IdpUrl
		}, new eAuthenticationContextItemParameter[]
		{
			eAuthenticationContextItemParameter.StudentNoField,
			eAuthenticationContextItemParameter.EmailField
		})]
		Adfs
	}
}
