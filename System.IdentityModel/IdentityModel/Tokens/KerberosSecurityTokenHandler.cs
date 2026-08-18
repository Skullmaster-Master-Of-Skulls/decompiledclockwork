using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime;
using System.Security.Claims;
using System.Security.Principal;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000127 RID: 295
	public class KerberosSecurityTokenHandler : SecurityTokenHandler
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0002214C File Offset: 0x0002034C
		public override Type TokenType
		{
			get
			{
				return typeof(KerberosReceiverSecurityToken);
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00022158 File Offset: 0x00020358
		public override string[] GetTokenTypeIdentifiers()
		{
			return KerberosSecurityTokenHandler._tokenTypeIdentifiers;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00022160 File Offset: 0x00020360
		public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			KerberosReceiverSecurityToken kerberosReceiverSecurityToken = token as KerberosReceiverSecurityToken;
			if (kerberosReceiverSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID0018", new object[]
				{
					typeof(KerberosReceiverSecurityToken)
				}));
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			ReadOnlyCollection<ClaimsIdentity> result;
			try
			{
				if (kerberosReceiverSecurityToken.WindowsIdentity == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4026"));
				}
				WindowsIdentity windowsIdentity = new WindowsIdentity(kerberosReceiverSecurityToken.WindowsIdentity.Token, kerberosReceiverSecurityToken.WindowsIdentity.AuthenticationType);
				windowsIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant", XmlConvert.ToString(DateTime.UtcNow, DateTimeFormats.Generated), "http://www.w3.org/2001/XMLSchema#dateTime"));
				windowsIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/windows", "http://www.w3.org/2001/XMLSchema#string"));
				if (base.Configuration.SaveBootstrapContext)
				{
					windowsIdentity.BootstrapContext = new BootstrapContext(token, this);
				}
				base.TraceTokenValidationSuccess(token);
				result = new List<ClaimsIdentity>(1)
				{
					windowsIdentity
				}.AsReadOnly();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.TraceTokenValidationFailure(token, ex.Message);
				throw ex;
			}
			return result;
		}

		// Token: 0x04000B02 RID: 2818
		private static string[] _tokenTypeIdentifiers = new string[]
		{
			SecurityTokenTypes.Kerberos
		};
	}
}
