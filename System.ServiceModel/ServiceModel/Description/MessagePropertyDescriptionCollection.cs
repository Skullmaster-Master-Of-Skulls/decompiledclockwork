using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D6 RID: 982
	[__DynamicallyInvokable]
	public class MessagePropertyDescriptionCollection : KeyedCollection<string, MessagePropertyDescription>
	{
		// Token: 0x060024E7 RID: 9447 RVA: 0x00084CA4 File Offset: 0x00082EA4
		internal MessagePropertyDescriptionCollection() : base(null, 4)
		{
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00084CAE File Offset: 0x00082EAE
		[__DynamicallyInvokable]
		protected override string GetKeyForItem(MessagePropertyDescription item)
		{
			return item.Name;
		}
	}
}
