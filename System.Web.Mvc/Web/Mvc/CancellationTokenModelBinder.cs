using System;
using System.Threading;

namespace System.Web.Mvc
{
	// Token: 0x02000074 RID: 116
	public class CancellationTokenModelBinder : IModelBinder
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			return default(CancellationToken);
		}
	}
}
