using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Internal;
using System.Web.Http.Validation;

namespace System.Web.Http.Metadata
{
	// Token: 0x02000135 RID: 309
	public class ModelMetadata
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x00019BD0 File Offset: 0x00017DD0
		public ModelMetadata(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			if (provider == null)
			{
				throw Error.ArgumentNull("provider");
			}
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			this.Provider = provider;
			this._containerType = containerType;
			this._modelAccessor = modelAccessor;
			this._modelType = modelType;
			this._propertyName = propertyName;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00019C32 File Offset: 0x00017E32
		internal ModelMetadata(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, EfficientTypePropertyKey<Type, string> cacheKey) : this(provider, containerType, modelAccessor, modelType, propertyName)
		{
			if (cacheKey == null)
			{
				throw Error.ArgumentNull("cacheKey");
			}
			this._cacheKey = cacheKey;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00019C58 File Offset: 0x00017E58
		public virtual Dictionary<string, object> AdditionalValues
		{
			get
			{
				if (this._additionalValues == null)
				{
					this._additionalValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
				}
				return this._additionalValues;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00019C78 File Offset: 0x00017E78
		public Type ContainerType
		{
			get
			{
				return this._containerType;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00019C80 File Offset: 0x00017E80
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00019C88 File Offset: 0x00017E88
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				return this._convertEmptyStringToNull;
			}
			set
			{
				this._convertEmptyStringToNull = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00019C91 File Offset: 0x00017E91
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00019C99 File Offset: 0x00017E99
		public virtual string Description { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00019CA2 File Offset: 0x00017EA2
		public virtual bool IsComplexType
		{
			get
			{
				return !TypeHelper.HasStringConverter(this.ModelType);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00019CB2 File Offset: 0x00017EB2
		public bool IsNullableValueType
		{
			get
			{
				return TypeHelper.IsNullableValueType(this.ModelType);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00019CBF File Offset: 0x00017EBF
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00019CC7 File Offset: 0x00017EC7
		public virtual bool IsReadOnly { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00019CD0 File Offset: 0x00017ED0
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00019CF8 File Offset: 0x00017EF8
		public object Model
		{
			get
			{
				if (this._modelAccessor != null)
				{
					this._model = this._modelAccessor();
					this._modelAccessor = null;
				}
				return this._model;
			}
			set
			{
				this._model = value;
				this._modelAccessor = null;
				this._properties = null;
				this._realModelType = null;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00019D16 File Offset: 0x00017F16
		public Type ModelType
		{
			get
			{
				return this._modelType;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00019D1E File Offset: 0x00017F1E
		public virtual IEnumerable<ModelMetadata> Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = this.Provider.GetMetadataForProperties(this.Model, this.RealModelType);
				}
				return this._properties;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00019D4B File Offset: 0x00017F4B
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00019D53 File Offset: 0x00017F53
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x00019D5B File Offset: 0x00017F5B
		protected ModelMetadataProvider Provider { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00019D64 File Offset: 0x00017F64
		internal Type RealModelType
		{
			get
			{
				if (this._realModelType == null)
				{
					this._realModelType = this.ModelType;
					if (this.Model != null && !TypeHelper.IsNullableValueType(this.ModelType))
					{
						this._realModelType = this.Model.GetType();
					}
				}
				return this._realModelType;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00019DB7 File Offset: 0x00017FB7
		internal EfficientTypePropertyKey<Type, string> CacheKey
		{
			get
			{
				if (this._cacheKey == null)
				{
					this._cacheKey = ModelMetadata.CreateCacheKey(this.ContainerType, this.ModelType, this.PropertyName);
				}
				return this._cacheKey;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00019DE4 File Offset: 0x00017FE4
		public virtual string GetDisplayName()
		{
			return this.PropertyName ?? this.ModelType.Name;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00019E18 File Offset: 0x00018018
		public virtual IEnumerable<ModelValidator> GetValidators(IEnumerable<ModelValidatorProvider> validatorProviders)
		{
			if (validatorProviders == null)
			{
				throw Error.ArgumentNull("validatorProviders");
			}
			return validatorProviders.SelectMany((ModelValidatorProvider provider) => provider.GetValidators(this, validatorProviders));
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00019E63 File Offset: 0x00018063
		private static EfficientTypePropertyKey<Type, string> CreateCacheKey(Type containerType, Type modelType, string propertyName)
		{
			return new EfficientTypePropertyKey<Type, string>(containerType ?? modelType, propertyName);
		}

		// Token: 0x04000232 RID: 562
		private readonly Type _containerType;

		// Token: 0x04000233 RID: 563
		private readonly Type _modelType;

		// Token: 0x04000234 RID: 564
		private readonly string _propertyName;

		// Token: 0x04000235 RID: 565
		private EfficientTypePropertyKey<Type, string> _cacheKey;

		// Token: 0x04000236 RID: 566
		private Dictionary<string, object> _additionalValues;

		// Token: 0x04000237 RID: 567
		private bool _convertEmptyStringToNull = true;

		// Token: 0x04000238 RID: 568
		private object _model;

		// Token: 0x04000239 RID: 569
		private Func<object> _modelAccessor;

		// Token: 0x0400023A RID: 570
		private IEnumerable<ModelMetadata> _properties;

		// Token: 0x0400023B RID: 571
		private Type _realModelType;
	}
}
