using System;

namespace System.Web.Mvc
{
	// Token: 0x02000198 RID: 408
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public abstract class CustomModelBinderAttribute : Attribute
	{
		// Token: 0x06000B8E RID: 2958
		public abstract IModelBinder GetBinder();

		// Token: 0x04000310 RID: 784
		internal const AttributeTargets ValidTargets = AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter;
	}
}
