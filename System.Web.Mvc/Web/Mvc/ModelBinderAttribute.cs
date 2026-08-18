using System;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001BF RID: 447
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public sealed class ModelBinderAttribute : CustomModelBinderAttribute
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x000231EC File Offset: 0x000213EC
		public ModelBinderAttribute(Type binderType)
		{
			if (binderType == null)
			{
				throw new ArgumentNullException("binderType");
			}
			if (!typeof(IModelBinder).IsAssignableFrom(binderType))
			{
				string message = string.Format(CultureInfo.CurrentCulture, MvcResources.ModelBinderAttribute_TypeNotIModelBinder, new object[]
				{
					binderType.FullName
				});
				throw new ArgumentException(message, "binderType");
			}
			this.BinderType = binderType;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x00023259 File Offset: 0x00021459
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x00023261 File Offset: 0x00021461
		public Type BinderType { get; private set; }

		// Token: 0x06000D39 RID: 3385 RVA: 0x0002326C File Offset: 0x0002146C
		public override IModelBinder GetBinder()
		{
			IModelBinder result;
			try
			{
				result = (IModelBinder)Activator.CreateInstance(this.BinderType);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ModelBinderAttribute_ErrorCreatingModelBinder, new object[]
				{
					this.BinderType.FullName
				}), innerException);
			}
			return result;
		}
	}
}
