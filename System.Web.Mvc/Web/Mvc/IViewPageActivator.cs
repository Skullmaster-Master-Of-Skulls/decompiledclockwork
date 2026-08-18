using System;

namespace System.Web.Mvc
{
	// Token: 0x02000069 RID: 105
	public interface IViewPageActivator
	{
		// Token: 0x060002DC RID: 732
		object Create(ControllerContext controllerContext, Type type);
	}
}
