using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000048 RID: 72
	internal interface ISecurityElement
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002CB RID: 715
		bool HasId { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002CC RID: 716
		string Id { get; }

		// Token: 0x060002CD RID: 717
		void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager);
	}
}
