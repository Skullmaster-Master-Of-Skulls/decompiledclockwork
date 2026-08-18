using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x0200019E RID: 414
	public class CompositeValueProviderFactory : ValueProviderFactory
	{
		// Token: 0x06000A7A RID: 2682 RVA: 0x00023294 File Offset: 0x00021494
		public CompositeValueProviderFactory(IEnumerable<ValueProviderFactory> factories)
		{
			this._factories = factories.ToArray<ValueProviderFactory>();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000232A8 File Offset: 0x000214A8
		public override IValueProvider GetValueProvider(HttpActionContext actionContext)
		{
			return CompositeValueProviderFactory.GetValueProvider(actionContext, this._factories);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000232B8 File Offset: 0x000214B8
		internal static IValueProvider GetValueProvider(HttpActionContext actionContext, ValueProviderFactory[] factories)
		{
			if (factories.Length == 1)
			{
				IValueProvider valueProvider = factories[0].GetValueProvider(actionContext);
				if (valueProvider != null)
				{
					return valueProvider;
				}
			}
			List<IValueProvider> list = new List<IValueProvider>();
			foreach (ValueProviderFactory valueProviderFactory in factories)
			{
				IValueProvider valueProvider2 = valueProviderFactory.GetValueProvider(actionContext);
				if (valueProvider2 != null)
				{
					list.Add(valueProvider2);
				}
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new CompositeValueProvider(list);
		}

		// Token: 0x04000310 RID: 784
		private ValueProviderFactory[] _factories;
	}
}
