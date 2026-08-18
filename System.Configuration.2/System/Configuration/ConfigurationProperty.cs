using System;
using System.ComponentModel;
using System.Reflection;

namespace System.Configuration
{
	// Token: 0x02000032 RID: 50
	public sealed class ConfigurationProperty
	{
		// Token: 0x0600024F RID: 591 RVA: 0x00010B9C File Offset: 0x0000ED9C
		public ConfigurationProperty(string name, Type type)
		{
			object defaultValue = null;
			this.ConstructorInit(name, type, ConfigurationPropertyOptions.None, null, null);
			if (type == typeof(string))
			{
				defaultValue = string.Empty;
			}
			else if (type.IsValueType)
			{
				defaultValue = TypeUtil.CreateInstanceWithReflectionPermission(type);
			}
			this.SetDefaultValue(defaultValue);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00010BEC File Offset: 0x0000EDEC
		public ConfigurationProperty(string name, Type type, object defaultValue) : this(name, type, defaultValue, ConfigurationPropertyOptions.None)
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00010BF8 File Offset: 0x0000EDF8
		public ConfigurationProperty(string name, Type type, object defaultValue, ConfigurationPropertyOptions options) : this(name, type, defaultValue, null, null, options)
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00010C07 File Offset: 0x0000EE07
		public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options) : this(name, type, defaultValue, typeConverter, validator, options, null)
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00010C19 File Offset: 0x0000EE19
		public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options, string description)
		{
			this.ConstructorInit(name, type, options, validator, typeConverter);
			this.SetDefaultValue(defaultValue);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00010C38 File Offset: 0x0000EE38
		internal ConfigurationProperty(PropertyInfo info)
		{
			ConfigurationPropertyAttribute configurationPropertyAttribute = null;
			DescriptionAttribute descriptionAttribute = null;
			DefaultValueAttribute attribStdDefault = null;
			TypeConverter converter = null;
			ConfigurationValidatorBase configurationValidatorBase = null;
			foreach (Attribute attribute in Attribute.GetCustomAttributes(info))
			{
				if (attribute is TypeConverterAttribute)
				{
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)attribute;
					converter = TypeUtil.CreateInstanceRestricted<TypeConverter>(info.DeclaringType, typeConverterAttribute.ConverterTypeName);
				}
				else if (attribute is ConfigurationPropertyAttribute)
				{
					configurationPropertyAttribute = (ConfigurationPropertyAttribute)attribute;
				}
				else if (attribute is ConfigurationValidatorAttribute)
				{
					if (configurationValidatorBase != null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Validator_multiple_validator_attributes", new object[]
						{
							info.Name
						}));
					}
					ConfigurationValidatorAttribute configurationValidatorAttribute = (ConfigurationValidatorAttribute)attribute;
					configurationValidatorAttribute.SetDeclaringType(info.DeclaringType);
					configurationValidatorBase = configurationValidatorAttribute.ValidatorInstance;
				}
				else if (attribute is DescriptionAttribute)
				{
					descriptionAttribute = (DescriptionAttribute)attribute;
				}
				else if (attribute is DefaultValueAttribute)
				{
					attribStdDefault = (DefaultValueAttribute)attribute;
				}
			}
			Type propertyType = info.PropertyType;
			if (typeof(ConfigurationElementCollection).IsAssignableFrom(propertyType))
			{
				ConfigurationCollectionAttribute configurationCollectionAttribute = Attribute.GetCustomAttribute(info, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute;
				if (configurationCollectionAttribute == null)
				{
					configurationCollectionAttribute = (Attribute.GetCustomAttribute(propertyType, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute);
				}
				if (configurationCollectionAttribute != null)
				{
					if (configurationCollectionAttribute.AddItemName.IndexOf(',') == -1)
					{
						this._addElementName = configurationCollectionAttribute.AddItemName;
					}
					this._removeElementName = configurationCollectionAttribute.RemoveItemName;
					this._clearElementName = configurationCollectionAttribute.ClearItemsName;
				}
			}
			this.ConstructorInit(configurationPropertyAttribute.Name, info.PropertyType, configurationPropertyAttribute.Options, configurationValidatorBase, converter);
			this.InitDefaultValueFromTypeInfo(configurationPropertyAttribute, attribStdDefault);
			if (descriptionAttribute != null && !string.IsNullOrEmpty(descriptionAttribute.Description))
			{
				this._description = descriptionAttribute.Description;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00010DFC File Offset: 0x0000EFFC
		private void ConstructorInit(string name, Type type, ConfigurationPropertyOptions options, ConfigurationValidatorBase validator, TypeConverter converter)
		{
			if (typeof(ConfigurationSection).IsAssignableFrom(type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_properties_may_not_be_derived_from_configuration_section", new object[]
				{
					name
				}));
			}
			this._providedName = name;
			if ((options & ConfigurationPropertyOptions.IsDefaultCollection) != ConfigurationPropertyOptions.None && string.IsNullOrEmpty(name))
			{
				name = ConfigurationProperty.DefaultCollectionPropertyName;
			}
			else
			{
				this.ValidatePropertyName(name);
			}
			this._name = name;
			this._type = type;
			this._options = options;
			this._validator = validator;
			this._converter = converter;
			if (this._validator == null)
			{
				this._validator = ConfigurationProperty.DefaultValidatorInstance;
				return;
			}
			if (!this._validator.CanValidate(this._type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Validator_does_not_support_prop_type", new object[]
				{
					this._name
				}));
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00010EC4 File Offset: 0x0000F0C4
		private void ValidatePropertyName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(SR.GetString("String_null_or_empty"), "name");
			}
			if (BaseConfigurationRecord.IsReservedAttributeName(name))
			{
				throw new ArgumentException(SR.GetString("Property_name_reserved", new object[]
				{
					name
				}));
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00010F10 File Offset: 0x0000F110
		private void SetDefaultValue(object value)
		{
			if (value != null && value != ConfigurationElement.s_nullPropertyValue)
			{
				bool flag = this._type.IsAssignableFrom(value.GetType());
				if (!flag && this.Converter.CanConvertFrom(value.GetType()))
				{
					value = this.Converter.ConvertFrom(value);
				}
				else if (!flag)
				{
					throw new ConfigurationErrorsException(SR.GetString("Default_value_wrong_type", new object[]
					{
						this._name
					}));
				}
				this.Validate(value);
				this._defaultValue = value;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00010F90 File Offset: 0x0000F190
		private void InitDefaultValueFromTypeInfo(ConfigurationPropertyAttribute attribProperty, DefaultValueAttribute attribStdDefault)
		{
			object obj = attribProperty.DefaultValue;
			if ((obj == null || obj == ConfigurationElement.s_nullPropertyValue) && attribStdDefault != null)
			{
				obj = attribStdDefault.Value;
			}
			if (obj != null && obj is string && this._type != typeof(string))
			{
				try
				{
					obj = this.Converter.ConvertFromInvariantString((string)obj);
				}
				catch (Exception ex)
				{
					throw new ConfigurationErrorsException(SR.GetString("Default_value_conversion_error_from_string", new object[]
					{
						this._name,
						ex.Message
					}));
				}
			}
			if (obj == null || obj == ConfigurationElement.s_nullPropertyValue)
			{
				if (this._type == typeof(string))
				{
					obj = string.Empty;
				}
				else if (this._type.IsValueType)
				{
					obj = TypeUtil.CreateInstanceWithReflectionPermission(this._type);
				}
			}
			this.SetDefaultValue(obj);
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00011074 File Offset: 0x0000F274
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0001107C File Offset: 0x0000F27C
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00011084 File Offset: 0x0000F284
		internal string ProvidedName
		{
			get
			{
				return this._providedName;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0001108C File Offset: 0x0000F28C
		internal bool IsConfigurationElementType
		{
			get
			{
				if (!this._isTypeInited)
				{
					this._isConfigurationElementType = typeof(ConfigurationElement).IsAssignableFrom(this._type);
					this._isTypeInited = true;
				}
				return this._isConfigurationElementType;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000110C6 File Offset: 0x0000F2C6
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000110CE File Offset: 0x0000F2CE
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000110D6 File Offset: 0x0000F2D6
		public bool IsRequired
		{
			get
			{
				return (this._options & ConfigurationPropertyOptions.IsRequired) > ConfigurationPropertyOptions.None;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000110E3 File Offset: 0x0000F2E3
		public bool IsKey
		{
			get
			{
				return (this._options & ConfigurationPropertyOptions.IsKey) > ConfigurationPropertyOptions.None;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000110F0 File Offset: 0x0000F2F0
		public bool IsDefaultCollection
		{
			get
			{
				return (this._options & ConfigurationPropertyOptions.IsDefaultCollection) > ConfigurationPropertyOptions.None;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000110FD File Offset: 0x0000F2FD
		public bool IsTypeStringTransformationRequired
		{
			get
			{
				return (this._options & ConfigurationPropertyOptions.IsTypeStringTransformationRequired) > ConfigurationPropertyOptions.None;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0001110A File Offset: 0x0000F30A
		public bool IsAssemblyStringTransformationRequired
		{
			get
			{
				return (this._options & ConfigurationPropertyOptions.IsAssemblyStringTransformationRequired) > ConfigurationPropertyOptions.None;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00011118 File Offset: 0x0000F318
		public bool IsVersionCheckRequired
		{
			get
			{
				return (this._options & ConfigurationPropertyOptions.IsVersionCheckRequired) > ConfigurationPropertyOptions.None;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00011126 File Offset: 0x0000F326
		public TypeConverter Converter
		{
			get
			{
				this.CreateConverter();
				return this._converter;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00011134 File Offset: 0x0000F334
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this._validator;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0001113C File Offset: 0x0000F33C
		internal string AddElementName
		{
			get
			{
				return this._addElementName;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00011144 File Offset: 0x0000F344
		internal string RemoveElementName
		{
			get
			{
				return this._removeElementName;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0001114C File Offset: 0x0000F34C
		internal string ClearElementName
		{
			get
			{
				return this._clearElementName;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00011154 File Offset: 0x0000F354
		internal object ConvertFromString(string value)
		{
			object result = null;
			try
			{
				result = this.Converter.ConvertFromInvariantString(value);
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Top_level_conversion_error_from_string", new object[]
				{
					this._name,
					ex.Message
				}));
			}
			return result;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000111AC File Offset: 0x0000F3AC
		internal string ConvertToString(object value)
		{
			string result = null;
			try
			{
				if (this._type == typeof(bool))
				{
					result = (((bool)value) ? "true" : "false");
				}
				else
				{
					result = this.Converter.ConvertToInvariantString(value);
				}
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Top_level_conversion_error_to_string", new object[]
				{
					this._name,
					ex.Message
				}));
			}
			return result;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00011234 File Offset: 0x0000F434
		internal void Validate(object value)
		{
			try
			{
				this._validator.Validate(value);
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("Top_level_validation_error", new object[]
				{
					this._name,
					ex.Message
				}), ex);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001128C File Offset: 0x0000F48C
		private void CreateConverter()
		{
			if (this._converter == null)
			{
				if (this._type.IsEnum)
				{
					this._converter = new GenericEnumConverter(this._type);
					return;
				}
				if (!this._type.IsSubclassOf(typeof(ConfigurationElement)))
				{
					this._converter = TypeDescriptor.GetConverter(this._type);
					if (this._converter == null || !this._converter.CanConvertFrom(typeof(string)) || !this._converter.CanConvertTo(typeof(string)))
					{
						throw new ConfigurationErrorsException(SR.GetString("No_converter", new object[]
						{
							this._name,
							this._type.Name
						}));
					}
				}
			}
		}

		// Token: 0x040001E4 RID: 484
		internal static readonly ConfigurationValidatorBase NonEmptyStringValidator = new StringValidator(1);

		// Token: 0x040001E5 RID: 485
		private static readonly ConfigurationValidatorBase DefaultValidatorInstance = new DefaultValidator();

		// Token: 0x040001E6 RID: 486
		internal static readonly string DefaultCollectionPropertyName = "";

		// Token: 0x040001E7 RID: 487
		private string _name;

		// Token: 0x040001E8 RID: 488
		private string _providedName;

		// Token: 0x040001E9 RID: 489
		private string _description;

		// Token: 0x040001EA RID: 490
		private Type _type;

		// Token: 0x040001EB RID: 491
		private object _defaultValue;

		// Token: 0x040001EC RID: 492
		private TypeConverter _converter;

		// Token: 0x040001ED RID: 493
		private ConfigurationPropertyOptions _options;

		// Token: 0x040001EE RID: 494
		private ConfigurationValidatorBase _validator;

		// Token: 0x040001EF RID: 495
		private string _addElementName;

		// Token: 0x040001F0 RID: 496
		private string _removeElementName;

		// Token: 0x040001F1 RID: 497
		private string _clearElementName;

		// Token: 0x040001F2 RID: 498
		private volatile bool _isTypeInited;

		// Token: 0x040001F3 RID: 499
		private volatile bool _isConfigurationElementType;
	}
}
