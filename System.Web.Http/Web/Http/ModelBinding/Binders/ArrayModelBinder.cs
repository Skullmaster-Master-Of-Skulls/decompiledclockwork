using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200013C RID: 316
	public class ArrayModelBinder<TElement> : CollectionModelBinder<TElement>
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x0001A431 File Offset: 0x00018631
		public override bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			return !bindingContext.ModelMetadata.IsReadOnly && base.BindModel(actionContext, bindingContext);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001A44A File Offset: 0x0001864A
		protected override bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<TElement> newCollection)
		{
			bindingContext.Model = newCollection.ToArray<TElement>();
			return true;
		}
	}
}
