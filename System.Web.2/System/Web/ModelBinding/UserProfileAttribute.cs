using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000690 RID: 1680
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class UserProfileAttribute : Attribute, IValueProviderSource
	{
		// Token: 0x06005122 RID: 20770 RVA: 0x00117852 File Offset: 0x00115A52
		public IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			return new UserProfileValueProvider(modelBindingExecutionContext);
		}
	}
}
