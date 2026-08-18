using System;
using System.IdentityModel;
using System.IdentityModel.Configuration;
using System.Xml;

namespace System.Security.Claims
{
	// Token: 0x0200001C RID: 28
	public class ClaimsAuthenticationManager : ICustomIdentityConfiguration
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004437 File Offset: 0x00002637
		public virtual ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
		{
			return incomingPrincipal;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000443A File Offset: 0x0000263A
		public virtual void LoadCustomConfiguration(XmlNodeList nodelist)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID0023", new object[]
			{
				base.GetType().AssemblyQualifiedName
			})));
		}
	}
}
