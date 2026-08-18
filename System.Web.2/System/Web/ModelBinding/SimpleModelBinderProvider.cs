using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000687 RID: 1671
	public sealed class SimpleModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x060050FC RID: 20732 RVA: 0x001172B8 File Offset: 0x001154B8
		public SimpleModelBinderProvider(Type modelType, IModelBinder modelBinder)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			if (modelBinder == null)
			{
				throw new ArgumentNullException("modelBinder");
			}
			this._modelType = modelType;
			this._modelBinderFactory = (() => modelBinder);
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x00117318 File Offset: 0x00115518
		public SimpleModelBinderProvider(Type modelType, Func<IModelBinder> modelBinderFactory)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			if (modelBinderFactory == null)
			{
				throw new ArgumentNullException("modelBinderFactory");
			}
			this._modelType = modelType;
			this._modelBinderFactory = modelBinderFactory;
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x00117350 File Offset: 0x00115550
		public Type ModelType
		{
			get
			{
				return this._modelType;
			}
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x00117358 File Offset: 0x00115558
		// (set) Token: 0x06005100 RID: 20736 RVA: 0x00117360 File Offset: 0x00115560
		public bool SuppressPrefixCheck { get; set; }

		// Token: 0x06005101 RID: 20737 RVA: 0x0011736C File Offset: 0x0011556C
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			ModelBinderUtil.ValidateBindingContext(bindingContext);
			if (bindingContext.ModelType == this.ModelType && (this.SuppressPrefixCheck || bindingContext.UnvalidatedValueProvider.ContainsPrefix(bindingContext.ModelName)))
			{
				return this._modelBinderFactory();
			}
			return null;
		}

		// Token: 0x04002ADB RID: 10971
		private readonly Func<IModelBinder> _modelBinderFactory;

		// Token: 0x04002ADC RID: 10972
		private readonly Type _modelType;
	}
}
