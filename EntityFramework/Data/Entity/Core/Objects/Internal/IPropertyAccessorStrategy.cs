using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200058A RID: 1418
	internal interface IPropertyAccessorStrategy
	{
		// Token: 0x06003765 RID: 14181
		object GetNavigationPropertyValue(RelatedEnd relatedEnd);

		// Token: 0x06003766 RID: 14182
		void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06003767 RID: 14183
		void CollectionAdd(RelatedEnd relatedEnd, object value);

		// Token: 0x06003768 RID: 14184
		bool CollectionRemove(RelatedEnd relatedEnd, object value);

		// Token: 0x06003769 RID: 14185
		object CollectionCreate(RelatedEnd relatedEnd);
	}
}
