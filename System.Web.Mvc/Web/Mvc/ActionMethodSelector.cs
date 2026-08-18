using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000173 RID: 371
	internal sealed class ActionMethodSelector : ActionMethodSelectorBase
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0001AD46 File Offset: 0x00018F46
		public ActionMethodSelector(Type controllerType)
		{
			base.Initialize(controllerType);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001AD55 File Offset: 0x00018F55
		protected override bool IsValidActionMethod(MethodInfo methodInfo)
		{
			return !methodInfo.IsSpecialName && !methodInfo.GetBaseDefinition().DeclaringType.IsAssignableFrom(typeof(Controller));
		}
	}
}
