using System;
using System.ComponentModel;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200054A RID: 1354
	internal interface IObjectView
	{
		// Token: 0x06003477 RID: 13431
		void EntityPropertyChanged(object sender, PropertyChangedEventArgs e);

		// Token: 0x06003478 RID: 13432
		void CollectionChanged(object sender, CollectionChangeEventArgs e);
	}
}
