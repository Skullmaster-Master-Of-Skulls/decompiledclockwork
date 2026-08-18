using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000641 RID: 1601
	public class ModelBindingContext
	{
		// Token: 0x06004F38 RID: 20280 RVA: 0x00113341 File Offset: 0x00111541
		public ModelBindingContext() : this(null)
		{
			this.ValidateRequest = true;
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x00113351 File Offset: 0x00111551
		public ModelBindingContext(ModelBindingContext bindingContext)
		{
			if (bindingContext != null)
			{
				this.ModelBinderProviders = bindingContext.ModelBinderProviders;
				this.ModelState = bindingContext.ModelState;
				this.ValueProvider = bindingContext.ValueProvider;
				this.ValidateRequest = bindingContext.ValidateRequest;
			}
		}

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x0011338C File Offset: 0x0011158C
		// (set) Token: 0x06004F3B RID: 20283 RVA: 0x0011339F File Offset: 0x0011159F
		public object Model
		{
			get
			{
				this.EnsureModelMetadata();
				return this.ModelMetadata.Model;
			}
			set
			{
				this.EnsureModelMetadata();
				this.ModelMetadata.Model = value;
			}
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06004F3C RID: 20284 RVA: 0x001133B3 File Offset: 0x001115B3
		// (set) Token: 0x06004F3D RID: 20285 RVA: 0x001133CE File Offset: 0x001115CE
		public ModelBinderProviderCollection ModelBinderProviders
		{
			get
			{
				if (this._modelBinderProviders == null)
				{
					this._modelBinderProviders = System.Web.ModelBinding.ModelBinderProviders.Providers;
				}
				return this._modelBinderProviders;
			}
			set
			{
				this._modelBinderProviders = value;
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06004F3E RID: 20286 RVA: 0x001133D7 File Offset: 0x001115D7
		// (set) Token: 0x06004F3F RID: 20287 RVA: 0x001133DF File Offset: 0x001115DF
		public ModelMetadata ModelMetadata
		{
			get
			{
				return this._modelMetadata;
			}
			set
			{
				this._modelMetadata = value;
			}
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06004F40 RID: 20288 RVA: 0x001133E8 File Offset: 0x001115E8
		// (set) Token: 0x06004F41 RID: 20289 RVA: 0x00113403 File Offset: 0x00111603
		public string ModelName
		{
			get
			{
				if (this._modelName == null)
				{
					this._modelName = string.Empty;
				}
				return this._modelName;
			}
			set
			{
				this._modelName = value;
			}
		}

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x0011340C File Offset: 0x0011160C
		// (set) Token: 0x06004F43 RID: 20291 RVA: 0x00113414 File Offset: 0x00111614
		public bool ValidateRequest { get; set; }

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x0011341D File Offset: 0x0011161D
		// (set) Token: 0x06004F45 RID: 20293 RVA: 0x00113438 File Offset: 0x00111638
		public ModelStateDictionary ModelState
		{
			get
			{
				if (this._modelState == null)
				{
					this._modelState = new ModelStateDictionary();
				}
				return this._modelState;
			}
			set
			{
				this._modelState = value;
			}
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x00113441 File Offset: 0x00111641
		public Type ModelType
		{
			get
			{
				this.EnsureModelMetadata();
				return this.ModelMetadata.ModelType;
			}
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x06004F47 RID: 20295 RVA: 0x00113454 File Offset: 0x00111654
		public IDictionary<string, ModelMetadata> PropertyMetadata
		{
			get
			{
				if (this._propertyMetadata == null)
				{
					this._propertyMetadata = this.ModelMetadata.Properties.ToDictionary((ModelMetadata m) => m.PropertyName, StringComparer.OrdinalIgnoreCase);
				}
				return this._propertyMetadata;
			}
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x001134A9 File Offset: 0x001116A9
		// (set) Token: 0x06004F49 RID: 20297 RVA: 0x001134D0 File Offset: 0x001116D0
		public ModelValidationNode ValidationNode
		{
			get
			{
				if (this._validationNode == null)
				{
					this._validationNode = new ModelValidationNode(this.ModelMetadata, this.ModelName);
				}
				return this._validationNode;
			}
			set
			{
				this._validationNode = value;
			}
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06004F4A RID: 20298 RVA: 0x001134D9 File Offset: 0x001116D9
		// (set) Token: 0x06004F4B RID: 20299 RVA: 0x001134E1 File Offset: 0x001116E1
		public IValueProvider ValueProvider
		{
			get
			{
				return this._valueProvider;
			}
			set
			{
				this._valueProvider = value;
			}
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06004F4C RID: 20300 RVA: 0x001134EA File Offset: 0x001116EA
		internal IUnvalidatedValueProvider UnvalidatedValueProvider
		{
			get
			{
				return (this.ValueProvider as IUnvalidatedValueProvider) ?? new ModelBindingContext.UnvalidatedValueProviderWrapper(this.ValueProvider);
			}
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x00113506 File Offset: 0x00111706
		private void EnsureModelMetadata()
		{
			if (this.ModelMetadata == null)
			{
				throw Error.ModelBindingContext_ModelMetadataMustBeSet();
			}
		}

		// Token: 0x04002A6A RID: 10858
		private ModelBinderProviderCollection _modelBinderProviders;

		// Token: 0x04002A6B RID: 10859
		private ModelMetadata _modelMetadata;

		// Token: 0x04002A6C RID: 10860
		private string _modelName;

		// Token: 0x04002A6D RID: 10861
		private ModelStateDictionary _modelState;

		// Token: 0x04002A6E RID: 10862
		private Dictionary<string, ModelMetadata> _propertyMetadata;

		// Token: 0x04002A6F RID: 10863
		private ModelValidationNode _validationNode;

		// Token: 0x04002A70 RID: 10864
		private IValueProvider _valueProvider;

		// Token: 0x02000A1D RID: 2589
		private sealed class UnvalidatedValueProviderWrapper : IValueProvider, IUnvalidatedValueProvider
		{
			// Token: 0x06006E07 RID: 28167 RVA: 0x00189105 File Offset: 0x00187305
			public UnvalidatedValueProviderWrapper(IValueProvider backingProvider)
			{
				this._backingProvider = backingProvider;
			}

			// Token: 0x06006E08 RID: 28168 RVA: 0x00189114 File Offset: 0x00187314
			public ValueProviderResult GetValue(string key, bool skipValidation)
			{
				return this.GetValue(key);
			}

			// Token: 0x06006E09 RID: 28169 RVA: 0x0018911D File Offset: 0x0018731D
			public bool ContainsPrefix(string prefix)
			{
				return this._backingProvider.ContainsPrefix(prefix);
			}

			// Token: 0x06006E0A RID: 28170 RVA: 0x0018912B File Offset: 0x0018732B
			public ValueProviderResult GetValue(string key)
			{
				return this._backingProvider.GetValue(key);
			}

			// Token: 0x04003AA5 RID: 15013
			private readonly IValueProvider _backingProvider;
		}
	}
}
