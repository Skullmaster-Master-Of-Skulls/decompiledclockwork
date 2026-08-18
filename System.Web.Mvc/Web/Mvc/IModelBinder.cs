using System;

namespace System.Web.Mvc
{
	// Token: 0x02000073 RID: 115
	public interface IModelBinder
	{
		// Token: 0x060003B4 RID: 948
		object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
	}
}
