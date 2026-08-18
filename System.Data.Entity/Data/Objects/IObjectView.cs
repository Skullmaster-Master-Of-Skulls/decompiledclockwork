using System;
using System.ComponentModel;

namespace System.Data.Objects
{
	// Token: 0x02000137 RID: 311
	internal interface IObjectView
	{
		// Token: 0x060016A9 RID: 5801
		void EntityPropertyChanged(object sender, PropertyChangedEventArgs e);

		// Token: 0x060016AA RID: 5802
		void CollectionChanged(object sender, CollectionChangeEventArgs e);
	}
}
