using System;
using System.ComponentModel;
using System.Configuration;
using System.Xml;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000028 RID: 40
	public class InstanceElement : ContainerConfiguringElement
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005453 File Offset: 0x00003653
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005465 File Offset: 0x00003665
		[ConfigurationProperty("name", IsRequired = false, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005473 File Offset: 0x00003673
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00005485 File Offset: 0x00003685
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005493 File Offset: 0x00003693
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000054A5 File Offset: 0x000036A5
		[ConfigurationProperty("type", IsRequired = false, DefaultValue = "")]
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000054B3 File Offset: 0x000036B3
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000054C5 File Offset: 0x000036C5
		[ConfigurationProperty("typeConverter", IsRequired = false, DefaultValue = "")]
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000054D3 File Offset: 0x000036D3
		public override string Key
		{
			get
			{
				return "instance:" + this.Name + ":" + this.Value;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000054F0 File Offset: 0x000036F0
		public override void SerializeContent(XmlWriter writer)
		{
			writer.WriteAttributeIfNotEmpty("name", this.Name).WriteAttributeIfNotEmpty("value", this.Value).WriteAttributeIfNotEmpty("type", this.TypeName).WriteAttributeIfNotEmpty("typeConverter", this.TypeConverterTypeName);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005540 File Offset: 0x00003740
		protected override void ConfigureContainer(IUnityContainer container)
		{
			Type instanceType = this.GetInstanceType();
			object instanceValue = this.GetInstanceValue();
			container.RegisterInstance(instanceType, this.Name, instanceValue);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000556A File Offset: 0x0000376A
		private Type GetInstanceType()
		{
			return TypeResolver.ResolveTypeWithDefault(this.TypeName, typeof(string));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005584 File Offset: 0x00003784
		private object GetInstanceValue()
		{
			if (string.IsNullOrEmpty(this.Value) && string.IsNullOrEmpty(this.TypeConverterTypeName))
			{
				return null;
			}
			TypeConverter typeConverter = this.GetTypeConverter();
			return typeConverter.ConvertFromInvariantString(this.Value);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000055C0 File Offset: 0x000037C0
		private TypeConverter GetTypeConverter()
		{
			if (!string.IsNullOrEmpty(this.TypeConverterTypeName))
			{
				Type type = TypeResolver.ResolveType(this.TypeConverterTypeName);
				return (TypeConverter)Activator.CreateInstance(type);
			}
			return TypeDescriptor.GetConverter(this.GetInstanceType());
		}

		// Token: 0x04000047 RID: 71
		private const string NamePropertyName = "name";

		// Token: 0x04000048 RID: 72
		private const string TypeConverterTypeNamePropertyName = "typeConverter";

		// Token: 0x04000049 RID: 73
		private const string TypeNamePropertyName = "type";

		// Token: 0x0400004A RID: 74
		private const string ValuePropertyName = "value";
	}
}
