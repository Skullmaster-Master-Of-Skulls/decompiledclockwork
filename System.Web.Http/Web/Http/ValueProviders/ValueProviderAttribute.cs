using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x020001A5 RID: 421
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class ValueProviderAttribute : ModelBinderAttribute
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x00023930 File Offset: 0x00021B30
		public ValueProviderAttribute(Type valueProviderFactory) : this(new Type[]
		{
			valueProviderFactory
		})
		{
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002394F File Offset: 0x00021B4F
		public ValueProviderAttribute(params Type[] valueProviderFactories)
		{
			this._valueProviderFactoryTypes = valueProviderFactories;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002395E File Offset: 0x00021B5E
		public IEnumerable<Type> ValueProviderFactoryTypes
		{
			get
			{
				return this._valueProviderFactoryTypes;
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00023966 File Offset: 0x00021B66
		public override IEnumerable<ValueProviderFactory> GetValueProviderFactories(HttpConfiguration configuration)
		{
			return Array.ConvertAll<Type, ValueProviderFactory>(this._valueProviderFactoryTypes, new Converter<Type, ValueProviderFactory>(ValueProviderAttribute.Instantiate));
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00023980 File Offset: 0x00021B80
		private static ValueProviderFactory Instantiate(Type factoryType)
		{
			if (factoryType == null)
			{
				throw new ArgumentNullException("factoryType");
			}
			if (!typeof(ValueProviderFactory).IsAssignableFrom(factoryType))
			{
				throw Error.InvalidOperation(SRResources.ValueProviderFactory_Cannot_Create, new object[]
				{
					typeof(ValueProviderFactory),
					factoryType
				});
			}
			return (ValueProviderFactory)Activator.CreateInstance(factoryType);
		}

		// Token: 0x0400031A RID: 794
		private readonly Type[] _valueProviderFactoryTypes;
	}
}
