using System;
using System.Collections.Generic;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001AF RID: 431
	public class ModelBindingContext
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x000206F8 File Offset: 0x0001E8F8
		public ModelBindingContext() : this(null)
		{
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00020701 File Offset: 0x0001E901
		public ModelBindingContext(ModelBindingContext bindingContext)
		{
			if (bindingContext != null)
			{
				this.ModelState = bindingContext.ModelState;
				this.ValueProvider = bindingContext.ValueProvider;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0002072F File Offset: 0x0001E92F
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x00020737 File Offset: 0x0001E937
		public bool FallbackToEmptyPrefix { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00020740 File Offset: 0x0001E940
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0002074D File Offset: 0x0001E94D
		public object Model
		{
			get
			{
				return this.ModelMetadata.Model;
			}
			set
			{
				throw new InvalidOperationException(MvcResources.ModelMetadata_PropertyNotSettable);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00020759 File Offset: 0x0001E959
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x00020761 File Offset: 0x0001E961
		public ModelMetadata ModelMetadata { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002076A File Offset: 0x0001E96A
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x00020772 File Offset: 0x0001E972
		public string ModelName
		{
			get
			{
				return this._modelName;
			}
			set
			{
				this._modelName = (value ?? string.Empty);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00020784 File Offset: 0x0001E984
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0002079F File Offset: 0x0001E99F
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

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x000207A8 File Offset: 0x0001E9A8
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x000207B5 File Offset: 0x0001E9B5
		public Type ModelType
		{
			get
			{
				return this.ModelMetadata.ModelType;
			}
			set
			{
				throw new InvalidOperationException(MvcResources.ModelMetadata_PropertyNotSettable);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x000207C1 File Offset: 0x0001E9C1
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x000207DC File Offset: 0x0001E9DC
		public Predicate<string> PropertyFilter
		{
			get
			{
				if (this._propertyFilter == null)
				{
					this._propertyFilter = ModelBindingContext._defaultPropertyFilter;
				}
				return this._propertyFilter;
			}
			set
			{
				this._propertyFilter = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x000207F0 File Offset: 0x0001E9F0
		public IDictionary<string, ModelMetadata> PropertyMetadata
		{
			get
			{
				if (this._propertyMetadata == null)
				{
					this._propertyMetadata = this.ModelMetadata.PropertiesAsArray.ToDictionaryFast((ModelMetadata m) => m.PropertyName, StringComparer.OrdinalIgnoreCase);
				}
				return this._propertyMetadata;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00020843 File Offset: 0x0001EA43
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x0002084B File Offset: 0x0001EA4B
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00020854 File Offset: 0x0001EA54
		internal IUnvalidatedValueProvider UnvalidatedValueProvider
		{
			get
			{
				return (this.ValueProvider as IUnvalidatedValueProvider) ?? new ModelBindingContext.UnvalidatedValueProviderWrapper(this.ValueProvider);
			}
		}

		// Token: 0x0400033F RID: 831
		private static readonly Predicate<string> _defaultPropertyFilter = (string _) => true;

		// Token: 0x04000340 RID: 832
		private string _modelName = string.Empty;

		// Token: 0x04000341 RID: 833
		private ModelStateDictionary _modelState;

		// Token: 0x04000342 RID: 834
		private Predicate<string> _propertyFilter;

		// Token: 0x04000343 RID: 835
		private Dictionary<string, ModelMetadata> _propertyMetadata;

		// Token: 0x020001B0 RID: 432
		private sealed class UnvalidatedValueProviderWrapper : IUnvalidatedValueProvider, IValueProvider
		{
			// Token: 0x06000C36 RID: 3126 RVA: 0x00020897 File Offset: 0x0001EA97
			public UnvalidatedValueProviderWrapper(IValueProvider backingProvider)
			{
				this._backingProvider = backingProvider;
			}

			// Token: 0x06000C37 RID: 3127 RVA: 0x000208A6 File Offset: 0x0001EAA6
			public ValueProviderResult GetValue(string key, bool skipValidation)
			{
				return this.GetValue(key);
			}

			// Token: 0x06000C38 RID: 3128 RVA: 0x000208AF File Offset: 0x0001EAAF
			public bool ContainsPrefix(string prefix)
			{
				return this._backingProvider.ContainsPrefix(prefix);
			}

			// Token: 0x06000C39 RID: 3129 RVA: 0x000208BD File Offset: 0x0001EABD
			public ValueProviderResult GetValue(string key)
			{
				return this._backingProvider.GetValue(key);
			}

			// Token: 0x04000349 RID: 841
			private readonly IValueProvider _backingProvider;
		}
	}
}
