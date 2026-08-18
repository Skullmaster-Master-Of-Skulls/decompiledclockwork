using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200066F RID: 1647
	public abstract class ValueProviderSourceAttribute : Attribute, IValueProviderSource, IModelNameProvider
	{
		// Token: 0x06005061 RID: 20577
		public abstract IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext);

		// Token: 0x06005062 RID: 20578 RVA: 0x0000298D File Offset: 0x00000B8D
		public virtual string GetModelName()
		{
			return null;
		}
	}
}
