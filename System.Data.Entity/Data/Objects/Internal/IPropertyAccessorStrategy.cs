using System;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200016A RID: 362
	internal interface IPropertyAccessorStrategy
	{
		// Token: 0x06001AD0 RID: 6864
		object GetNavigationPropertyValue(RelatedEnd relatedEnd);

		// Token: 0x06001AD1 RID: 6865
		void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value);

		// Token: 0x06001AD2 RID: 6866
		void CollectionAdd(RelatedEnd relatedEnd, object value);

		// Token: 0x06001AD3 RID: 6867
		bool CollectionRemove(RelatedEnd relatedEnd, object value);

		// Token: 0x06001AD4 RID: 6868
		object CollectionCreate(RelatedEnd relatedEnd);
	}
}
