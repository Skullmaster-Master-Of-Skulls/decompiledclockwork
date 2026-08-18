using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x020001CE RID: 462
	public sealed class SchemaImporterExtensionElement : ConfigurationElement
	{
		// Token: 0x06001F53 RID: 8019 RVA: 0x000AA2C4 File Offset: 0x000A84C4
		public SchemaImporterExtensionElement()
		{
			this.properties.Add(this.name);
			this.properties.Add(this.type);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000AA342 File Offset: 0x000A8542
		public SchemaImporterExtensionElement(string name, string type) : this()
		{
			this.Name = name;
			base[this.type] = new SchemaImporterExtensionElement.TypeAndName(type);
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x000AA363 File Offset: 0x000A8563
		public SchemaImporterExtensionElement(string name, Type type) : this()
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x000AA379 File Offset: 0x000A8579
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x000AA38C File Offset: 0x000A858C
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[this.name];
			}
			set
			{
				base[this.name] = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x000AA39B File Offset: 0x000A859B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x000AA3A3 File Offset: 0x000A85A3
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x000AA3BB File Offset: 0x000A85BB
		[ConfigurationProperty("type", IsRequired = true, IsKey = false)]
		[TypeConverter(typeof(SchemaImporterExtensionElement.TypeTypeConverter))]
		public Type Type
		{
			get
			{
				return ((SchemaImporterExtensionElement.TypeAndName)base[this.type]).type;
			}
			set
			{
				base[this.type] = new SchemaImporterExtensionElement.TypeAndName(value);
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000AA3CF File Offset: 0x000A85CF
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x04000D3C RID: 3388
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000D3D RID: 3389
		private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000D3E RID: 3390
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(Type), null, new SchemaImporterExtensionElement.TypeTypeConverter(), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x02000489 RID: 1161
		private class TypeAndName
		{
			// Token: 0x06003114 RID: 12564 RVA: 0x0011DF70 File Offset: 0x0011C170
			public TypeAndName(string name)
			{
				this.type = Type.GetType(name, true, true);
				this.name = name;
			}

			// Token: 0x06003115 RID: 12565 RVA: 0x0011DF8D File Offset: 0x0011C18D
			public TypeAndName(Type type)
			{
				this.type = type;
			}

			// Token: 0x06003116 RID: 12566 RVA: 0x0011DF9C File Offset: 0x0011C19C
			public override int GetHashCode()
			{
				return this.type.GetHashCode();
			}

			// Token: 0x06003117 RID: 12567 RVA: 0x0011DFA9 File Offset: 0x0011C1A9
			public override bool Equals(object comparand)
			{
				return this.type.Equals(((SchemaImporterExtensionElement.TypeAndName)comparand).type);
			}

			// Token: 0x04001E0B RID: 7691
			public readonly Type type;

			// Token: 0x04001E0C RID: 7692
			public readonly string name;
		}

		// Token: 0x0200048A RID: 1162
		private class TypeTypeConverter : TypeConverter
		{
			// Token: 0x06003118 RID: 12568 RVA: 0x0011DFC1 File Offset: 0x0011C1C1
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06003119 RID: 12569 RVA: 0x0011DFDF File Offset: 0x0011C1DF
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return new SchemaImporterExtensionElement.TypeAndName((string)value);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x0600311A RID: 12570 RVA: 0x0011E000 File Offset: 0x0011C200
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (!(destinationType == typeof(string)))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				SchemaImporterExtensionElement.TypeAndName typeAndName = (SchemaImporterExtensionElement.TypeAndName)value;
				if (typeAndName.name != null)
				{
					return typeAndName.name;
				}
				return typeAndName.type.AssemblyQualifiedName;
			}
		}
	}
}
