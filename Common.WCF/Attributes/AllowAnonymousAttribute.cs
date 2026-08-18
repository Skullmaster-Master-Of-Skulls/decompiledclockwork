using System;

namespace TechnoPro.Common.WCF.Attributes
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
	public class AllowAnonymousAttribute : ClockWorkBaseServiceAttribute
	{
	}
}
