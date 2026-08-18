using System;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000146 RID: 326
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public abstract class CustomModelBinderAttribute : Attribute
	{
		// Token: 0x0600080A RID: 2058
		public abstract IModelBinder GetBinder();

		// Token: 0x04000257 RID: 599
		internal const AttributeTargets ValidTargets = AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter;
	}
}
