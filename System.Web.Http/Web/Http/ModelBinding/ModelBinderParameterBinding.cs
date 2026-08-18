using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000D7 RID: 215
	public class ModelBinderParameterBinding : HttpParameterBinding, IValueProviderParameterBinding
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public ModelBinderParameterBinding(HttpParameterDescriptor descriptor, IModelBinder modelBinder, IEnumerable<ValueProviderFactory> valueProviderFactories) : base(descriptor)
		{
			if (modelBinder == null)
			{
				throw Error.ArgumentNull("modelBinder");
			}
			if (valueProviderFactories == null)
			{
				throw Error.ArgumentNull("valueProviderFactories");
			}
			this._binder = modelBinder;
			this._valueProviderFactories = valueProviderFactories.ToArray<ValueProviderFactory>();
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x000110FC File Offset: 0x0000F2FC
		public IEnumerable<ValueProviderFactory> ValueProviderFactories
		{
			get
			{
				return this._valueProviderFactories;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00011104 File Offset: 0x0000F304
		public IModelBinder Binder
		{
			get
			{
				return this._binder;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001110C File Offset: 0x0000F30C
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			ModelBindingContext modelBindingContext = this.GetModelBindingContext(metadataProvider, actionContext);
			object value = this._binder.BindModel(actionContext, modelBindingContext) ? modelBindingContext.Model : base.Descriptor.DefaultValue;
			base.SetValue(actionContext, value);
			return TaskHelpers.Completed();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00011154 File Offset: 0x0000F354
		private ModelBindingContext GetModelBindingContext(ModelMetadataProvider metadataProvider, HttpActionContext actionContext)
		{
			string parameterName = base.Descriptor.ParameterName;
			Type parameterType = base.Descriptor.ParameterType;
			string prefix = base.Descriptor.Prefix;
			IValueProvider valueProvider = CompositeValueProviderFactory.GetValueProvider(actionContext, this._valueProviderFactories);
			return new ModelBindingContext
			{
				ModelName = (prefix ?? parameterName),
				FallbackToEmptyPrefix = (prefix == null),
				ModelMetadata = metadataProvider.GetMetadataForType(null, parameterType),
				ModelState = actionContext.ModelState,
				ValueProvider = valueProvider
			};
		}

		// Token: 0x04000184 RID: 388
		private readonly ValueProviderFactory[] _valueProviderFactories;

		// Token: 0x04000185 RID: 389
		private readonly IModelBinder _binder;
	}
}
