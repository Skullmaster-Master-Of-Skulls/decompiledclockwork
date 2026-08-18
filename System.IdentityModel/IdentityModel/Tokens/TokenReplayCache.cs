using System;
using System.IdentityModel.Configuration;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000186 RID: 390
	public abstract class TokenReplayCache : ICustomIdentityConfiguration
	{
		// Token: 0x06000CC7 RID: 3271 RVA: 0x0000443A File Offset: 0x0000263A
		public virtual void LoadCustomConfiguration(XmlNodeList nodelist)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID0023", new object[]
			{
				base.GetType().AssemblyQualifiedName
			})));
		}

		// Token: 0x06000CC8 RID: 3272
		public abstract void AddOrUpdate(string key, SecurityToken securityToken, DateTime expirationTime);

		// Token: 0x06000CC9 RID: 3273
		public abstract bool Contains(string key);

		// Token: 0x06000CCA RID: 3274
		public abstract SecurityToken Get(string key);

		// Token: 0x06000CCB RID: 3275
		public abstract void Remove(string key);
	}
}
