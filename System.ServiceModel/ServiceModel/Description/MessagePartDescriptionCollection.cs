using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D4 RID: 980
	[__DynamicallyInvokable]
	public class MessagePartDescriptionCollection : KeyedCollection<XmlQualifiedName, MessagePartDescription>
	{
		// Token: 0x060024E2 RID: 9442 RVA: 0x00084C68 File Offset: 0x00082E68
		internal MessagePartDescriptionCollection() : base(null, 4)
		{
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x00084C72 File Offset: 0x00082E72
		[__DynamicallyInvokable]
		protected override XmlQualifiedName GetKeyForItem(MessagePartDescription item)
		{
			return new XmlQualifiedName(item.Name, item.Namespace);
		}
	}
}
