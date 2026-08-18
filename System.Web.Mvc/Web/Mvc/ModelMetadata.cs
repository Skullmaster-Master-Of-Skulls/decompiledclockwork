using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc.ExpressionUtil;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200006F RID: 111
	public class ModelMetadata
	{
		// Token: 0x0600031A RID: 794 RVA: 0x00009DA8 File Offset: 0x00007FA8
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00009E51 File Offset: 0x00008051
		public virtual Dictionary<string, object> AdditionalValues
		{
			get
			{
				return this._additionalValues;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00009E59 File Offset: 0x00008059
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00009E61 File Offset: 0x00008061
		public object Container { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00009E6A File Offset: 0x0000806A
		public Type ContainerType
		{
			get
			{
				return this._containerType;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00009E72 File Offset: 0x00008072
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00009E7A File Offset: 0x0000807A
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00009E83 File Offset: 0x00008083
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00009E8B File Offset: 0x0000808B
		public virtual string DataTypeName { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00009E94 File Offset: 0x00008094
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00009E9C File Offset: 0x0000809C
		public virtual string Description { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00009EA5 File Offset: 0x000080A5
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00009EAD File Offset: 0x000080AD
		public virtual string DisplayFormatString { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00009EB6 File Offset: 0x000080B6
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00009EBE File Offset: 0x000080BE
		public virtual string DisplayName { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00009EC7 File Offset: 0x000080C7
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00009ECF File Offset: 0x000080CF
		public virtual string EditFormatString { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00009ED8 File Offset: 0x000080D8
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00009EE0 File Offset: 0x000080E0
		internal virtual bool HasNonDefaultEditFormat { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00009EE9 File Offset: 0x000080E9
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00009EF1 File Offset: 0x000080F1
		public virtual bool HideSurroundingHtml { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00009EFA File Offset: 0x000080FA
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00009F02 File Offset: 0x00008102
		public virtual bool HtmlEncode
		{
			get
			{
				return this._htmlEncode;
			}
			set
			{
				this._htmlEncode = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00009F0B File Offset: 0x0000810B
		public virtual bool IsComplexType
		{
			get
			{
				return !TypeDescriptor.GetConverter(this.ModelType).CanConvertFrom(typeof(string));
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00009F2A File Offset: 0x0000812A
		public bool IsNullableValueType
		{
			get
			{
				return TypeHelpers.IsNullableValueType(this.ModelType);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00009F37 File Offset: 0x00008137
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00009F3F File Offset: 0x0000813F
		public virtual bool IsReadOnly { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00009F48 File Offset: 0x00008148
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00009F50 File Offset: 0x00008150
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

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00009F59 File Offset: 0x00008159
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00009F81 File Offset: 0x00008181
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

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00009F9F File Offset: 0x0000819F
		public Type ModelType
		{
			get
			{
				return this._modelType;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00009FA7 File Offset: 0x000081A7
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00009FAF File Offset: 0x000081AF
		public virtual string NullDisplayText { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00009FB8 File Offset: 0x000081B8
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00009FC0 File Offset: 0x000081C0
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00009FCC File Offset: 0x000081CC
		public virtual IEnumerable<ModelMetadata> Properties
		{
			get
			{
				if (this._properties == null)
				{
					IEnumerable<ModelMetadata> metadataForProperties = this.Provider.GetMetadataForProperties(this.Model, this.RealModelType);
					this._propertiesInternal = ModelMetadata.SortProperties(metadataForProperties.AsArray<ModelMetadata>());
					this._properties = new ReadOnlyCollection<ModelMetadata>(this._propertiesInternal);
				}
				return this._properties;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A024 File Offset: 0x00008224
		internal ModelMetadata[] PropertiesAsArray
		{
			get
			{
				IEnumerable<ModelMetadata> properties = this.Properties;
				if (object.ReferenceEquals(properties, this._properties))
				{
					return this._propertiesInternal;
				}
				return properties.AsArray<ModelMetadata>();
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000A053 File Offset: 0x00008253
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A05B File Offset: 0x0000825B
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000A063 File Offset: 0x00008263
		protected ModelMetadataProvider Provider { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000A06C File Offset: 0x0000826C
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

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000A0BF File Offset: 0x000082BF
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000A0C7 File Offset: 0x000082C7
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

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000A0D0 File Offset: 0x000082D0
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000A0D8 File Offset: 0x000082D8
		public virtual string ShortDisplayName { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000A0E1 File Offset: 0x000082E1
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000A0E9 File Offset: 0x000082E9
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000A0F2 File Offset: 0x000082F2
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000A0FA File Offset: 0x000082FA
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

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000A103 File Offset: 0x00008303
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000A11F File Offset: 0x0000831F
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

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000A128 File Offset: 0x00008328
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000A130 File Offset: 0x00008330
		public virtual string TemplateHint { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000A139 File Offset: 0x00008339
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000A141 File Offset: 0x00008341
		public virtual string Watermark { get; set; }

		// Token: 0x06000352 RID: 850 RVA: 0x0000A14A File Offset: 0x0000834A
		public static ModelMetadata FromLambdaExpression<TParameter, TValue>(Expression<Func<TParameter, TValue>> expression, ViewDataDictionary<TParameter> viewData)
		{
			return ModelMetadata.FromLambdaExpression<TParameter, TValue>(expression, viewData, null);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000A1A0 File Offset: 0x000083A0
		internal static ModelMetadata FromLambdaExpression<TParameter, TValue>(Expression<Func<TParameter, TValue>> expression, ViewDataDictionary<TParameter> viewData, ModelMetadataProvider metadataProvider)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (viewData == null)
			{
				throw new ArgumentNullException("viewData");
			}
			string propertyName = null;
			Type containerType = null;
			bool flag = false;
			ExpressionType nodeType = expression.Body.NodeType;
			switch (nodeType)
			{
			case ExpressionType.ArrayIndex:
				flag = true;
				break;
			case ExpressionType.Call:
				flag = ExpressionHelper.IsSingleArgumentIndexer(expression.Body);
				break;
			default:
				if (nodeType != ExpressionType.MemberAccess)
				{
					if (nodeType == ExpressionType.Parameter)
					{
						return ModelMetadata.FromModel(viewData, metadataProvider);
					}
				}
				else
				{
					MemberExpression memberExpression = (MemberExpression)expression.Body;
					propertyName = ((memberExpression.Member is PropertyInfo) ? memberExpression.Member.Name : null);
					containerType = memberExpression.Expression.Type;
					flag = true;
				}
				break;
			}
			if (!flag)
			{
				throw new InvalidOperationException(MvcResources.TemplateHelpers_TemplateLimitations);
			}
			TParameter container = viewData.Model;
			Func<object> modelAccessor = delegate()
			{
				object result;
				try
				{
					result = CachedExpressionCompiler.Process<TParameter, TValue>(expression)(container);
				}
				catch (NullReferenceException)
				{
					result = null;
				}
				return result;
			};
			return ModelMetadata.GetMetadataFromProvider(modelAccessor, typeof(TValue), propertyName, container, containerType, metadataProvider);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000A2BE File Offset: 0x000084BE
		private static ModelMetadata FromModel(ViewDataDictionary viewData, ModelMetadataProvider metadataProvider)
		{
			return viewData.ModelMetadata ?? ModelMetadata.GetMetadataFromProvider(null, typeof(string), null, null, null, metadataProvider);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000A2DE File Offset: 0x000084DE
		public static ModelMetadata FromStringExpression(string expression, ViewDataDictionary viewData)
		{
			return ModelMetadata.FromStringExpression(expression, viewData, null);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000A310 File Offset: 0x00008510
		internal static ModelMetadata FromStringExpression(string expression, ViewDataDictionary viewData, ModelMetadataProvider metadataProvider)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (viewData == null)
			{
				throw new ArgumentNullException("viewData");
			}
			if (expression.Length == 0)
			{
				return ModelMetadata.FromModel(viewData, metadataProvider);
			}
			ViewDataInfo vdi = viewData.GetViewDataInfo(expression);
			object container = null;
			Type containerType = null;
			Type type = null;
			Func<object> modelAccessor = null;
			string propertyName = null;
			if (vdi != null)
			{
				if (vdi.Container != null)
				{
					container = vdi.Container;
					containerType = vdi.Container.GetType();
				}
				modelAccessor = (() => vdi.Value);
				if (vdi.PropertyDescriptor != null)
				{
					propertyName = vdi.PropertyDescriptor.Name;
					type = vdi.PropertyDescriptor.PropertyType;
				}
				else if (vdi.Value != null)
				{
					type = vdi.Value.GetType();
				}
			}
			else if (viewData.ModelMetadata != null)
			{
				ModelMetadata modelMetadata = (from p in viewData.ModelMetadata.Properties
				where p.PropertyName == expression
				select p).FirstOrDefault<ModelMetadata>();
				if (modelMetadata != null)
				{
					return modelMetadata;
				}
			}
			return ModelMetadata.GetMetadataFromProvider(modelAccessor, type ?? typeof(string), propertyName, container, containerType, metadataProvider);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000A484 File Offset: 0x00008684
		public string GetDisplayName()
		{
			string result;
			if ((result = this.DisplayName) == null)
			{
				result = (this.PropertyName ?? this.ModelType.Name);
			}
			return result;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000A4A8 File Offset: 0x000086A8
		private static ModelMetadata GetMetadataFromProvider(Func<object> modelAccessor, Type modelType, string propertyName, object container, Type containerType, ModelMetadataProvider metadataProvider)
		{
			metadataProvider = (metadataProvider ?? ModelMetadataProviders.Current);
			if (containerType != null && !string.IsNullOrEmpty(propertyName))
			{
				ModelMetadata metadataForProperty = metadataProvider.GetMetadataForProperty(modelAccessor, containerType, propertyName);
				if (metadataForProperty != null)
				{
					metadataForProperty.Container = container;
				}
				return metadataForProperty;
			}
			return metadataProvider.GetMetadataForType(modelAccessor, modelType);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000A4F8 File Offset: 0x000086F8
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

		// Token: 0x0600035A RID: 858 RVA: 0x0000A57D File Offset: 0x0000877D
		public virtual IEnumerable<ModelValidator> GetValidators(ControllerContext context)
		{
			return ModelValidatorProviders.Providers.GetValidators(this, context);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000A594 File Offset: 0x00008794
		private static ModelMetadata[] SortProperties(ModelMetadata[] properties)
		{
			int? num = null;
			bool flag = false;
			foreach (ModelMetadata modelMetadata in properties)
			{
				if (num != null && num > modelMetadata.Order)
				{
					flag = true;
					break;
				}
				num = new int?(modelMetadata.Order);
			}
			if (!flag)
			{
				return properties;
			}
			return (from m in properties
			orderby m.Order
			select m).ToArray<ModelMetadata>();
		}

		// Token: 0x040000BD RID: 189
		public const int DefaultOrder = 10000;

		// Token: 0x040000BE RID: 190
		private readonly Type _containerType;

		// Token: 0x040000BF RID: 191
		private readonly Type _modelType;

		// Token: 0x040000C0 RID: 192
		private readonly string _propertyName;

		// Token: 0x040000C1 RID: 193
		private Dictionary<string, object> _additionalValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000C2 RID: 194
		private bool _convertEmptyStringToNull = true;

		// Token: 0x040000C3 RID: 195
		private bool _htmlEncode = true;

		// Token: 0x040000C4 RID: 196
		private bool _isRequired;

		// Token: 0x040000C5 RID: 197
		private object _model;

		// Token: 0x040000C6 RID: 198
		private Func<object> _modelAccessor;

		// Token: 0x040000C7 RID: 199
		private int _order = 10000;

		// Token: 0x040000C8 RID: 200
		private IEnumerable<ModelMetadata> _properties;

		// Token: 0x040000C9 RID: 201
		private ModelMetadata[] _propertiesInternal;

		// Token: 0x040000CA RID: 202
		private Type _realModelType;

		// Token: 0x040000CB RID: 203
		private bool _requestValidationEnabled = true;

		// Token: 0x040000CC RID: 204
		private bool _showForDisplay = true;

		// Token: 0x040000CD RID: 205
		private bool _showForEdit = true;

		// Token: 0x040000CE RID: 206
		private string _simpleDisplayText;
	}
}
