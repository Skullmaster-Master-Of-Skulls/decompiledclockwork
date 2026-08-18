using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200002B RID: 43
	public class LifetimeElement : DeserializableConfigurationElement
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000561A File Offset: 0x0000381A
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000562C File Offset: 0x0000382C
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public string TypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000563A File Offset: 0x0000383A
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000564C File Offset: 0x0000384C
		[ConfigurationProperty("value", IsRequired = false)]
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000565A File Offset: 0x0000385A
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000566C File Offset: 0x0000386C
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

		// Token: 0x0600014E RID: 334 RVA: 0x0000567C File Offset: 0x0000387C
		public LifetimeManager CreateLifetimeManager()
		{
			TypeConverter typeConverter = this.GetTypeConverter();
			if (typeConverter == null)
			{
				return (LifetimeManager)Activator.CreateInstance(this.GetLifetimeType());
			}
			return (LifetimeManager)typeConverter.ConvertFrom(this.Value);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000056B8 File Offset: 0x000038B8
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void SerializeContent(XmlWriter writer)
		{
			Guard.ArgumentNotNull(writer, "writer");
			writer.WriteAttributeString("type", this.TypeName);
			if (!string.IsNullOrEmpty(this.Value))
			{
				writer.WriteAttributeString("value", this.Value);
			}
			if (!string.IsNullOrEmpty(this.TypeConverterTypeName))
			{
				writer.WriteAttributeString("typeConverter", this.TypeConverterTypeName);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000571D File Offset: 0x0000391D
		private Type GetLifetimeType()
		{
			return TypeResolver.ResolveTypeWithDefault(this.TypeName, typeof(TransientLifetimeManager));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005734 File Offset: 0x00003934
		private TypeConverter GetTypeConverter()
		{
			if (string.IsNullOrEmpty(this.Value) && string.IsNullOrEmpty(this.TypeConverterTypeName))
			{
				return null;
			}
			if (!string.IsNullOrEmpty(this.TypeConverterTypeName))
			{
				Type type = TypeResolver.ResolveType(this.TypeConverterTypeName);
				return (TypeConverter)Activator.CreateInstance(type);
			}
			return TypeDescriptor.GetConverter(this.GetLifetimeType());
		}

		// Token: 0x0400004B RID: 75
		private const string TypeConverterTypeNamePropertyName = "typeConverter";

		// Token: 0x0400004C RID: 76
		private const string TypeNamePropertyName = "type";

		// Token: 0x0400004D RID: 77
		private const string ValuePropertyName = "value";
	}
}
