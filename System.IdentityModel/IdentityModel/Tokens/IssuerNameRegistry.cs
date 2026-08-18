using System;
using System.IdentityModel.Configuration;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000123 RID: 291
	public abstract class IssuerNameRegistry : ICustomIdentityConfiguration
	{
		// Token: 0x06000800 RID: 2048
		public abstract string GetIssuerName(SecurityToken securityToken);

		// Token: 0x06000801 RID: 2049 RVA: 0x0002170D File Offset: 0x0001F90D
		public virtual string GetIssuerName(SecurityToken securityToken, string requestedIssuerName)
		{
			return this.GetIssuerName(securityToken);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00021716 File Offset: 0x0001F916
		public virtual string GetWindowsIssuerName()
		{
			return "LOCAL AUTHORITY";
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0000443A File Offset: 0x0000263A
		public virtual void LoadCustomConfiguration(XmlNodeList nodelist)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID0023", new object[]
			{
				base.GetType().AssemblyQualifiedName
			})));
		}
	}
}
