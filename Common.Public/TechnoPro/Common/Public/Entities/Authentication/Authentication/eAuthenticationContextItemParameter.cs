using System;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x020004A0 RID: 1184
	public enum eAuthenticationContextItemParameter
	{
		// Token: 0x04001AA4 RID: 6820
		[AuthenticationContextItemParameter("AdHoc", true)]
		AdHoc,
		// Token: 0x04001AA5 RID: 6821
		[AuthenticationContextItemParameter("student_no_field", "Student number field name", "The name of the incoming field/variable that contains the student number.", null)]
		StudentNoField,
		// Token: 0x04001AA6 RID: 6822
		[AuthenticationContextItemParameter("username_field", "Username field name", "The name of the incoming field/variable that contains the username.", null)]
		UsernameField,
		// Token: 0x04001AA7 RID: 6823
		[AuthenticationContextItemParameter("email_field", "Email field name", "The name of the incoming field/variable that contains the email.", null)]
		EmailField,
		// Token: 0x04001AA8 RID: 6824
		[AuthenticationContextItemParameter("date_field", "Date field", "The name of the incoming field/variable that contains the date.", null)]
		DateField,
		// Token: 0x04001AA9 RID: 6825
		[AuthenticationContextItemParameter("token_field", "Token field", "The name of the incoming field/variable that contains the token.", null)]
		TokenField,
		// Token: 0x04001AAA RID: 6826
		[AuthenticationContextItemParameter("hash_type", "Hash type", "The type of hashing to use (must match incoming usage).  Default hash type is PBKDF2.", null)]
		HashType,
		// Token: 0x04001AAB RID: 6827
		[AuthenticationContextItemParameter("isdoublebinding", "Ldap use double binding", "Use double binding for ldap.  Note that you must fill in all of the 'Ldap pre ...' settings once double binding is enabled.", "CtrlAuthParameterBoolEdit")]
		Isdoublebinding,
		// Token: 0x04001AAC RID: 6828
		[AuthenticationContextItemParameter("ldapserver", "Ldap server", "The host name of the Ldap server.", null)]
		Ldapserver,
		// Token: 0x04001AAD RID: 6829
		[AuthenticationContextItemParameter("ReportId", "ClockWork Report Id", "The id of a ClockWork report that is used for added processing.", "CtrlAuthParameterReportIdEdit")]
		ReportId,
		// Token: 0x04001AAE RID: 6830
		[AuthenticationContextItemParameter("binPath", "Bin path", "The full Windows path to the executing folder.", null)]
		BinPath,
		// Token: 0x04001AAF RID: 6831
		[AuthenticationContextItemParameter("reportArgs", "Report args (secure only)", "A comma separated list of field/variable names that should be passed to the report.  Only incoming args from the secure environment section will be used - for example get/post variables are considered insecure args, server variables are considered secure args.", "CtrlAuthParameterStringListEdit")]
		ReportArgs,
		// Token: 0x04001AB0 RID: 6832
		[AuthenticationContextItemParameter("reportArgsInsecure", "Report args (in-secure or secure)", "A comma separated list of field/variables names that should be passed to the report.", "CtrlAuthParameterStringListEdit")]
		ReportArgsInsecure,
		// Token: 0x04001AB1 RID: 6833
		[AuthenticationContextItemParameter("ldappredomain", "Ldap pre domain", "The ldap domain used for double-binding (pre-authentication step)", null)]
		Ldappredomain,
		// Token: 0x04001AB2 RID: 6834
		[AuthenticationContextItemParameter("ldapprelookupattribute", "Ldap pre lookup attribute", "The ldap lookup attribute used for double-binding (pre-authentication step)", null)]
		Ldapprelookupattribute,
		// Token: 0x04001AB3 RID: 6835
		[AuthenticationContextItemParameter("ldapprepassword", "Ldap pre password", "The ldap password used for double-binding (pre-authentication step)", null)]
		Ldapprepassword,
		// Token: 0x04001AB4 RID: 6836
		[AuthenticationContextItemParameter("ldappreusername", "Ldap pre username", "The ldap username used for double-binding (pre-authentication step)", null)]
		Ldappreusername,
		// Token: 0x04001AB5 RID: 6837
		[AuthenticationContextItemParameter("ldapprotocolversion", "Ldap protocol version", "Leave blank or zero to use the default protocol version (recommended).", null)]
		Ldapprotocolversion,
		// Token: 0x04001AB6 RID: 6838
		[AuthenticationContextItemParameter("ldapusessl", "Ldap Use SSL", "Enable SSL", null)]
		Ldapusessl,
		// Token: 0x04001AB7 RID: 6839
		[AuthenticationContextItemParameter("ldapusetls", "Ldap use TLS", "Enable TLS", null)]
		Ldapusetls,
		// Token: 0x04001AB8 RID: 6840
		[AuthenticationContextItemParameter("ldapdontverifyservercertificate", "Ldap Don't verify server certificate", "Not recommended - this will not verify the server certificate for Ldap is valid.  eg. for self-signed certificate", null)]
		Ldapdontverifyservercertificate,
		// Token: 0x04001AB9 RID: 6841
		[AuthenticationContextItemParameter("ldapauthtype", "Ldap auth type", "The auth type for ldap.  Supports securesocketslayer, sealing, signing", null)]
		LdapAuthType,
		// Token: 0x04001ABA RID: 6842
		[AuthenticationContextItemParameter("ldapport", "Ldap port", "The port used for ldap", null)]
		LdapPort,
		// Token: 0x04001ABB RID: 6843
		[AuthenticationContextItemParameter("ldapdomain", "Ldap domain", "The domain to use for ldap (eg. ou=tpro, ou=ca)", null)]
		LdapDomain,
		// Token: 0x04001ABC RID: 6844
		[AuthenticationContextItemParameter("ldapreturnattribute", "Ldap return attribute", "The name of the ldap attribute/field that should be used after authentication.", null)]
		LdapReturnAttribute,
		// Token: 0x04001ABD RID: 6845
		[AuthenticationContextItemParameter("ldaplookupattribute", "Ldap lookup attribute", "The name of the ldap attribute/field that should be used to lookup the user during authentication", null)]
		LdapLookupAttribute,
		// Token: 0x04001ABE RID: 6846
		[AuthenticationContextItemParameter("override_token_timeout", "Override token timeout", "(in seconds).  This is the amount of time a token will be valid from based on comparing the (incoming) date/time supplied with the token and the current date/time", null)]
		OverrideTokenTimeout,
		// Token: 0x04001ABF RID: 6847
		[AuthenticationContextItemParameter("hashing_uses_hex_encoding", "Hashing uses hex encoding", "If true this would mean the plain text values used to create the hash should be assembled and then hex encoded before hashing (and comparing to the supplied hash to verify authenticity)", "CtrlAuthParameterBoolEdit")]
		HashingUsesHexEncoding,
		// Token: 0x04001AC0 RID: 6848
		[AuthenticationContextItemParameter("whole_token_is_base64_encoded", "Hashing token is base64 encoded", "If true this would mean the whole token should be decoded from base64 before passing it to the hashing algorithm", "CtrlAuthParameterBoolEdit")]
		WholeTokenIsBase64Encoded,
		// Token: 0x04001AC1 RID: 6849
		[AuthenticationContextItemParameter("extra_field", "Hashing extra field(s)", "Comma separated list of additional field names used to create the token - in the correct order", null)]
		ExtraField,
		// Token: 0x04001AC2 RID: 6850
		[AuthenticationContextItemParameter("token_issuer", "Token issuer", "The token issuer information used by ClockWork to verify that the token certificate is valid.", "CtrlAuthParameterTokenIssuerEdit")]
		TokenIssuer,
		// Token: 0x04001AC3 RID: 6851
		[AuthenticationContextItemParameter("request_issuer", "Request issuer", "The unique id assigned to the ClockWork application for use with PortalGuard (default is https://clockworks.ca)", null)]
		RequestIssuer,
		// Token: 0x04001AC4 RID: 6852
		[AuthenticationContextItemParameter("idp_url", "Idp url", "The Portal Guard login page url.", null)]
		IdpUrl
	}
}
