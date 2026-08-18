using System;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000157 RID: 343
	public abstract class SamlCondition
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A6F RID: 2671
		public abstract bool IsReadOnly { get; }

		// Token: 0x06000A70 RID: 2672
		public abstract void MakeReadOnly();

		// Token: 0x06000A71 RID: 2673
		public abstract void ReadXml(XmlDictionaryReader reader, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer, SecurityTokenResolver outOfBandTokenResolver);

		// Token: 0x06000A72 RID: 2674
		public abstract void WriteXml(XmlDictionaryWriter writer, SamlSerializer samlSerializer, SecurityTokenSerializer keyInfoSerializer);
	}
}
