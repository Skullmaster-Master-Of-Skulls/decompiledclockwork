using System;
using System.IdentityModel;
using System.IdentityModel.Configuration;
using System.Xml;

namespace System.Security.Claims
{
	// Token: 0x0200001D RID: 29
	public class ClaimsAuthorizationManager : ICustomIdentityConfiguration
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00002434 File Offset: 0x00000634
		public virtual bool CheckAccess(AuthorizationContext context)
		{
			return true;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000443A File Offset: 0x0000263A
		public virtual void LoadCustomConfiguration(XmlNodeList nodelist)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID0023", new object[]
			{
				base.GetType().AssemblyQualifiedName
			})));
		}
	}
}
