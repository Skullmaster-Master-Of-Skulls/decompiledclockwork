using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D2 RID: 978
	[__DynamicallyInvokable]
	public class MessageHeaderDescriptionCollection : KeyedCollection<XmlQualifiedName, MessageHeaderDescription>
	{
		// Token: 0x060024C5 RID: 9413 RVA: 0x00084A3C File Offset: 0x00082C3C
		internal MessageHeaderDescriptionCollection() : base(null, 4)
		{
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x00084A46 File Offset: 0x00082C46
		[__DynamicallyInvokable]
		protected override XmlQualifiedName GetKeyForItem(MessageHeaderDescription item)
		{
			return new XmlQualifiedName(item.Name, item.Namespace);
		}
	}
}
