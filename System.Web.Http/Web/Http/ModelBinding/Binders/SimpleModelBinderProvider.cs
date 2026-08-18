using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000180 RID: 384
	public sealed class SimpleModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0002135C File Offset: 0x0001F55C
		public SimpleModelBinderProvider(Type modelType, IModelBinder modelBinder)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (modelBinder == null)
			{
				throw Error.ArgumentNull("modelBinder");
			}
			this._modelType = modelType;
			this._modelBinderFactory = (() => modelBinder);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000213C3 File Offset: 0x0001F5C3
		public SimpleModelBinderProvider(Type modelType, Func<IModelBinder> modelBinderFactory)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (modelBinderFactory == null)
			{
				throw Error.ArgumentNull("modelBinderFactory");
			}
			this._modelType = modelType;
			this._modelBinderFactory = modelBinderFactory;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000213FB File Offset: 0x0001F5FB
		public Type ModelType
		{
			get
			{
				return this._modelType;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00021403 File Offset: 0x0001F603
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0002140B File Offset: 0x0001F60B
		public bool SuppressPrefixCheck { get; set; }

		// Token: 0x06000A0B RID: 2571 RVA: 0x00021414 File Offset: 0x0001F614
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (!(modelType == this.ModelType))
			{
				return null;
			}
			if (this.SuppressPrefixCheck)
			{
				return this._modelBinderFactory();
			}
			return new SimpleModelBinderProvider.SimpleModelBinder(this);
		}

		// Token: 0x040002F9 RID: 761
		private readonly Func<IModelBinder> _modelBinderFactory;

		// Token: 0x040002FA RID: 762
		private readonly Type _modelType;

		// Token: 0x02000181 RID: 385
		private class SimpleModelBinder : IModelBinder
		{
			// Token: 0x06000A0C RID: 2572 RVA: 0x00021454 File Offset: 0x0001F654
			public SimpleModelBinder(SimpleModelBinderProvider parent)
			{
				this._parent = parent;
			}

			// Token: 0x06000A0D RID: 2573 RVA: 0x00021464 File Offset: 0x0001F664
			public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
			{
				if (bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
				{
					IModelBinder modelBinder = this._parent._modelBinderFactory();
					return modelBinder.BindModel(actionContext, bindingContext);
				}
				return false;
			}

			// Token: 0x040002FC RID: 764
			private readonly SimpleModelBinderProvider _parent;
		}
	}
}
