using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x020000FC RID: 252
	public sealed class CompositeModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x0001441C File Offset: 0x0001261C
		public CompositeModelBinderProvider()
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00014424 File Offset: 0x00012624
		public CompositeModelBinderProvider(IEnumerable<ModelBinderProvider> providers)
		{
			if (providers == null)
			{
				throw Error.ArgumentNull("providers");
			}
			this._providers = providers.ToArray<ModelBinderProvider>();
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00014446 File Offset: 0x00012646
		public IEnumerable<ModelBinderProvider> Providers
		{
			get
			{
				return this._providers;
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000145A0 File Offset: 0x000127A0
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			ModelBinderProvider[] providers = this._providers;
			IEnumerable<ModelBinderProvider> source = (providers != null) ? ((IEnumerable<ModelBinderProvider>)providers) : configuration.Services.GetModelBinderProviders();
			IEnumerable<IModelBinder> binders = from provider in source
			let binder = provider.GetBinder(configuration, modelType)
			where binder != null
			select binder;
			return new CompositeModelBinder(binders);
		}

		// Token: 0x040001B8 RID: 440
		private ModelBinderProvider[] _providers;
	}
}
