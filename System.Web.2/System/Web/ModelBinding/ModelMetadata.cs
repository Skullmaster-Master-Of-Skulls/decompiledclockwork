using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200065A RID: 1626
	public class ModelMetadata
	{
		// Token: 0x06004FCB RID: 20427 RVA: 0x00114AC8 File Offset: 0x00112CC8
		public ModelMetadata(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			this.Provider = provider;
			this._containerType = containerType;
			this._isRequired = !TypeHelpers.TypeAllowsNullValue(modelType);
			this._modelAccessor = modelAccessor;
			this._modelType = modelType;
			this._propertyName = propertyName;
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06004FCC RID: 20428 RVA: 0x00114B6A File Offset: 0x00112D6A
		public virtual Dictionary<string, object> AdditionalValues
		{
			get
			{
				return this._additionalValues;
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06004FCD RID: 20429 RVA: 0x00114B72 File Offset: 0x00112D72
		public Type ContainerType
		{
			get
			{
				return this._containerType;
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x00114B7A File Offset: 0x00112D7A
		// (set) Token: 0x06004FCF RID: 20431 RVA: 0x00114B82 File Offset: 0x00112D82
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

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x00114B8B File Offset: 0x00112D8B
		// (set) Token: 0x06004FD1 RID: 20433 RVA: 0x00114B93 File Offset: 0x00112D93
		public virtual string DataTypeName { get; set; }

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x00114B9C File Offset: 0x00112D9C
		// (set) Token: 0x06004FD3 RID: 20435 RVA: 0x00114BA4 File Offset: 0x00112DA4
		public virtual string Description { get; set; }

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x00114BAD File Offset: 0x00112DAD
		// (set) Token: 0x06004FD5 RID: 20437 RVA: 0x00114BB5 File Offset: 0x00112DB5
		public virtual string DisplayFormatString { get; set; }

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06004FD6 RID: 20438 RVA: 0x00114BBE File Offset: 0x00112DBE
		// (set) Token: 0x06004FD7 RID: 20439 RVA: 0x00114BC6 File Offset: 0x00112DC6
		public virtual string DisplayName { get; set; }

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06004FD8 RID: 20440 RVA: 0x00114BCF File Offset: 0x00112DCF
		// (set) Token: 0x06004FD9 RID: 20441 RVA: 0x00114BD7 File Offset: 0x00112DD7
		public virtual string EditFormatString { get; set; }

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06004FDA RID: 20442 RVA: 0x00114BE0 File Offset: 0x00112DE0
		// (set) Token: 0x06004FDB RID: 20443 RVA: 0x00114BE8 File Offset: 0x00112DE8
		public virtual bool HideSurroundingHtml { get; set; }

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06004FDC RID: 20444 RVA: 0x00114BF1 File Offset: 0x00112DF1
		public virtual bool IsComplexType
		{
			get
			{
				return !TypeDescriptor.GetConverter(this.ModelType).CanConvertFrom(typeof(string));
			}
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06004FDD RID: 20445 RVA: 0x00114C10 File Offset: 0x00112E10
		public bool IsNullableValueType
		{
			get
			{
				return TypeHelpers.IsNullableValueType(this.ModelType);
			}
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06004FDE RID: 20446 RVA: 0x00114C1D File Offset: 0x00112E1D
		// (set) Token: 0x06004FDF RID: 20447 RVA: 0x00114C25 File Offset: 0x00112E25
		public virtual bool IsReadOnly { get; set; }

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06004FE0 RID: 20448 RVA: 0x00114C2E File Offset: 0x00112E2E
		// (set) Token: 0x06004FE1 RID: 20449 RVA: 0x00114C36 File Offset: 0x00112E36
		public virtual bool IsRequired
		{
			get
			{
				return this._isRequired;
			}
			set
			{
				this._isRequired = value;
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06004FE2 RID: 20450 RVA: 0x00114C3F File Offset: 0x00112E3F
		// (set) Token: 0x06004FE3 RID: 20451 RVA: 0x00114C67 File Offset: 0x00112E67
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

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06004FE4 RID: 20452 RVA: 0x00114C85 File Offset: 0x00112E85
		public Type ModelType
		{
			get
			{
				return this._modelType;
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06004FE5 RID: 20453 RVA: 0x00114C8D File Offset: 0x00112E8D
		// (set) Token: 0x06004FE6 RID: 20454 RVA: 0x00114C95 File Offset: 0x00112E95
		public virtual string NullDisplayText { get; set; }

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06004FE7 RID: 20455 RVA: 0x00114C9E File Offset: 0x00112E9E
		// (set) Token: 0x06004FE8 RID: 20456 RVA: 0x00114CA6 File Offset: 0x00112EA6
		public virtual int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06004FE9 RID: 20457 RVA: 0x00114CB0 File Offset: 0x00112EB0
		public virtual IEnumerable<ModelMetadata> Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = from m in this.Provider.GetMetadataForProperties(this.Model, this.RealModelType)
					orderby m.Order
					select m;
				}
				return this._properties;
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06004FEA RID: 20458 RVA: 0x00114D0C File Offset: 0x00112F0C
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06004FEB RID: 20459 RVA: 0x00114D14 File Offset: 0x00112F14
		// (set) Token: 0x06004FEC RID: 20460 RVA: 0x00114D1C File Offset: 0x00112F1C
		protected ModelMetadataProvider Provider { get; set; }

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06004FED RID: 20461 RVA: 0x00114D28 File Offset: 0x00112F28
		internal Type RealModelType
		{
			get
			{
				if (this._realModelType == null)
				{
					this._realModelType = this.ModelType;
					if (this.Model != null && !TypeHelpers.IsNullableValueType(this.ModelType))
					{
						this._realModelType = this.Model.GetType();
					}
				}
				return this._realModelType;
			}
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x00114D7B File Offset: 0x00112F7B
		// (set) Token: 0x06004FEF RID: 20463 RVA: 0x00114D83 File Offset: 0x00112F83
		public virtual bool RequestValidationEnabled
		{
			get
			{
				return this._requestValidationEnabled;
			}
			set
			{
				this._requestValidationEnabled = value;
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06004FF0 RID: 20464 RVA: 0x00114D8C File Offset: 0x00112F8C
		// (set) Token: 0x06004FF1 RID: 20465 RVA: 0x00114D94 File Offset: 0x00112F94
		public virtual string ShortDisplayName { get; set; }

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06004FF2 RID: 20466 RVA: 0x00114D9D File Offset: 0x00112F9D
		// (set) Token: 0x06004FF3 RID: 20467 RVA: 0x00114DA5 File Offset: 0x00112FA5
		public virtual bool ShowForDisplay
		{
			get
			{
				return this._showForDisplay;
			}
			set
			{
				this._showForDisplay = value;
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06004FF4 RID: 20468 RVA: 0x00114DAE File Offset: 0x00112FAE
		// (set) Token: 0x06004FF5 RID: 20469 RVA: 0x00114DB6 File Offset: 0x00112FB6
		public virtual bool ShowForEdit
		{
			get
			{
				return this._showForEdit;
			}
			set
			{
				this._showForEdit = value;
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06004FF6 RID: 20470 RVA: 0x00114DBF File Offset: 0x00112FBF
		// (set) Token: 0x06004FF7 RID: 20471 RVA: 0x00114DDB File Offset: 0x00112FDB
		public virtual string SimpleDisplayText
		{
			get
			{
				if (this._simpleDisplayText == null)
				{
					this._simpleDisplayText = this.GetSimpleDisplayText();
				}
				return this._simpleDisplayText;
			}
			set
			{
				this._simpleDisplayText = value;
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06004FF8 RID: 20472 RVA: 0x00114DE4 File Offset: 0x00112FE4
		// (set) Token: 0x06004FF9 RID: 20473 RVA: 0x00114DEC File Offset: 0x00112FEC
		public virtual string TemplateHint { get; set; }

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06004FFA RID: 20474 RVA: 0x00114DF5 File Offset: 0x00112FF5
		// (set) Token: 0x06004FFB RID: 20475 RVA: 0x00114DFD File Offset: 0x00112FFD
		public virtual string Watermark { get; set; }

		// Token: 0x06004FFC RID: 20476 RVA: 0x00114E06 File Offset: 0x00113006
		public string GetDisplayName()
		{
			string result;
			if ((result = this.DisplayName) == null)
			{
				result = (this.PropertyName ?? this.ModelType.Name);
			}
			return result;
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x00114E27 File Offset: 0x00113027
		private static ModelMetadata GetMetadataFromProvider(Func<object> modelAccessor, Type modelType, string propertyName, Type containerType, ModelMetadataProvider metadataProvider)
		{
			metadataProvider = (metadataProvider ?? ModelMetadataProviders.Current);
			if (containerType != null && !string.IsNullOrEmpty(propertyName))
			{
				return metadataProvider.GetMetadataForProperty(modelAccessor, containerType, propertyName);
			}
			return metadataProvider.GetMetadataForType(modelAccessor, modelType);
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x00114E5C File Offset: 0x0011305C
		protected virtual string GetSimpleDisplayText()
		{
			if (this.Model == null)
			{
				return this.NullDisplayText;
			}
			string text = Convert.ToString(this.Model, CultureInfo.CurrentCulture);
			if (text == null)
			{
				return string.Empty;
			}
			if (!text.Equals(this.Model.GetType().FullName, StringComparison.Ordinal))
			{
				return text;
			}
			ModelMetadata modelMetadata = this.Properties.FirstOrDefault<ModelMetadata>();
			if (modelMetadata == null)
			{
				return string.Empty;
			}
			if (modelMetadata.Model == null)
			{
				return modelMetadata.NullDisplayText;
			}
			return Convert.ToString(modelMetadata.Model, CultureInfo.CurrentCulture);
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x00114EE1 File Offset: 0x001130E1
		public virtual IEnumerable<ModelValidator> GetValidators(ModelBindingExecutionContext context)
		{
			return ModelValidatorProviders.Providers.GetValidators(this, context);
		}

		// Token: 0x04002A94 RID: 10900
		public const int DefaultOrder = 10000;

		// Token: 0x04002A95 RID: 10901
		private Dictionary<string, object> _additionalValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002A96 RID: 10902
		private readonly Type _containerType;

		// Token: 0x04002A97 RID: 10903
		private bool _convertEmptyStringToNull = true;

		// Token: 0x04002A98 RID: 10904
		private bool _isRequired;

		// Token: 0x04002A99 RID: 10905
		private object _model;

		// Token: 0x04002A9A RID: 10906
		private Func<object> _modelAccessor;

		// Token: 0x04002A9B RID: 10907
		private readonly Type _modelType;

		// Token: 0x04002A9C RID: 10908
		private int _order = 10000;

		// Token: 0x04002A9D RID: 10909
		private IEnumerable<ModelMetadata> _properties;

		// Token: 0x04002A9E RID: 10910
		private readonly string _propertyName;

		// Token: 0x04002A9F RID: 10911
		private Type _realModelType;

		// Token: 0x04002AA0 RID: 10912
		private bool _requestValidationEnabled = true;

		// Token: 0x04002AA1 RID: 10913
		private bool _showForDisplay = true;

		// Token: 0x04002AA2 RID: 10914
		private bool _showForEdit = true;

		// Token: 0x04002AA3 RID: 10915
		private string _simpleDisplayText;
	}
}
