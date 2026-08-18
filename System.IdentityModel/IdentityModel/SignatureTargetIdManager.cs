using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200007B RID: 123
	internal abstract class SignatureTargetIdManager
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600043B RID: 1083
		public abstract string DefaultIdNamespacePrefix { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600043C RID: 1084
		public abstract string DefaultIdNamespaceUri { get; }

		// Token: 0x0600043D RID: 1085
		public abstract string ExtractId(XmlDictionaryReader reader);

		// Token: 0x0600043E RID: 1086
		public abstract void WriteIdAttribute(XmlDictionaryWriter writer, string id);
	}
}
