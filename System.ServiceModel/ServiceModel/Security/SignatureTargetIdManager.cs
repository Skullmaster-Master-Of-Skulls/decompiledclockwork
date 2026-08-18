using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000286 RID: 646
	internal abstract class SignatureTargetIdManager
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060012A5 RID: 4773
		public abstract string DefaultIdNamespacePrefix { get; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060012A6 RID: 4774
		public abstract string DefaultIdNamespaceUri { get; }

		// Token: 0x060012A7 RID: 4775
		public abstract string ExtractId(XmlDictionaryReader reader);

		// Token: 0x060012A8 RID: 4776
		public abstract void WriteIdAttribute(XmlDictionaryWriter writer, string id);
	}
}
