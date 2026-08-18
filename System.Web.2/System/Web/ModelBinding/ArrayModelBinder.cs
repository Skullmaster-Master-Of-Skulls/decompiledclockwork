using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200062C RID: 1580
	public class ArrayModelBinder<TElement> : CollectionModelBinder<TElement>
	{
		// Token: 0x06004EDB RID: 20187 RVA: 0x0011265D File Offset: 0x0011085D
		protected override bool CreateOrReplaceCollection(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, IList<TElement> newCollection)
		{
			bindingContext.Model = newCollection.ToArray<TElement>();
			return true;
		}
	}
}
