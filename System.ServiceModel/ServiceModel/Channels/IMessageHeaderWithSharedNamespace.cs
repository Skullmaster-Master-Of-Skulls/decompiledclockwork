using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009D0 RID: 2512
	internal interface IMessageHeaderWithSharedNamespace
	{
		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x060062C2 RID: 25282
		XmlDictionaryString SharedNamespace { get; }

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x060062C3 RID: 25283
		XmlDictionaryString SharedPrefix { get; }
	}
}
