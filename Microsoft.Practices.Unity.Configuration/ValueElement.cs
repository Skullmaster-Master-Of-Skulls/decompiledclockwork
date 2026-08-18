using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000042 RID: 66
	public class ValueElement : ParameterValueElement, IAttributeOnlyElement
	{
		// Token: 0x0600021D RID: 541 RVA: 0x0000757C File Offset: 0x0000577C
		public ValueElement()
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007584 File Offset: 0x00005784
		public ValueElement(IDictionary<string, string> propertyValues)
		{
			ParameterValueElement.GuardPropertyValueIsPresent(propertyValues, "value");
			this.Value = propertyValues["value"];
			if (propertyValues.ContainsKey("typeConverter"))
			{
				this.TypeConverterTypeName = propertyValues["typeConverter"];
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000075D1 File Offset: 0x000057D1
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000075E3 File Offset: 0x000057E3
		[ConfigurationProperty("value")]
		public string Value
		{
			get
			{
				return (string)base["value"];
			}
			set
			{
				base["value"] = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000075F1 File Offset: 0x000057F1
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00007603 File Offset: 0x00005803
		[ConfigurationProperty("typeConverter", IsRequired = false)]
		public string TypeConverterTypeName
		{
			get
			{
				return (string)base["typeConverter"];
			}
			set
			{
				base["typeConverter"] = value;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007611 File Offset: 0x00005811
		void IAttributeOnlyElement.SerializeContent(XmlWriter writer)
		{
			this.SerializeContent(writer);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000761A File Offset: 0x0000581A
		public override void SerializeContent(XmlWriter writer)
		{
			writer.WriteAttributeIfNotEmpty("value", this.Value).WriteAttributeIfNotEmpty("typeConverter", this.TypeConverterTypeName);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007640 File Offset: 0x00005840
		public override InjectionParameterValue GetInjectionParameterValue(IUnityContainer container, Type parameterType)
		{
			this.CheckNonGeneric(parameterType);
			TypeConverter typeConverter = this.GetTypeConverter(parameterType);
			return new InjectionParameter(parameterType, typeConverter.ConvertFromInvariantString(this.Value));
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007670 File Offset: 0x00005870
		private void CheckNonGeneric(Type parameterType)
		{
			if (parameterType.IsGenericParameter)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.ValueNotAllowedForGenericParameterType, new object[]
				{
					parameterType.Name,
					this.Value
				}));
			}
			ReflectionHelper reflectionHelper = new ReflectionHelper(parameterType);
			if (reflectionHelper.IsOpenGeneric)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.ValueNotAllowedForOpenGenericType, new object[]
				{
					parameterType.Name,
					this.Value
				}));
			}
			if (reflectionHelper.IsGenericArray)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.ValueNotAllowedForGenericArrayType, new object[]
				{
					parameterType.Name,
					this.Value
				}));
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000772C File Offset: 0x0000592C
		private TypeConverter GetTypeConverter(Type parameterType)
		{
			if (!string.IsNullOrEmpty(this.TypeConverterTypeName))
			{
				Type type = TypeResolver.ResolveType(this.TypeConverterTypeName);
				return (TypeConverter)Activator.CreateInstance(type);
			}
			return TypeDescriptor.GetConverter(parameterType);
		}

		// Token: 0x04000086 RID: 134
		private const string ValuePropertyName = "value";

		// Token: 0x04000087 RID: 135
		private const string TypeConverterTypeNamePropertyName = "typeConverter";
	}
}
