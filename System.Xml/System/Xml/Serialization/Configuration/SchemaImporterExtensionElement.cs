using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Xml.Serialization.Configuration
{
	// Token: 0x02000350 RID: 848
	public sealed class SchemaImporterExtensionElement : ConfigurationElement
	{
		// Token: 0x0600291E RID: 10526 RVA: 0x000D332C File Offset: 0x000D232C
		public SchemaImporterExtensionElement()
		{
			this.properties.Add(this.name);
			this.properties.Add(this.type);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000D33AA File Offset: 0x000D23AA
		public SchemaImporterExtensionElement(string name, string type) : this()
		{
			this.Name = name;
			base[this.type] = new SchemaImporterExtensionElement.TypeAndName(type);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000D33CB File Offset: 0x000D23CB
		public SchemaImporterExtensionElement(string name, Type type) : this()
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x000D33E1 File Offset: 0x000D23E1
		// (set) Token: 0x06002922 RID: 10530 RVA: 0x000D33F4 File Offset: 0x000D23F4
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

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x000D3403 File Offset: 0x000D2403
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x000D340B File Offset: 0x000D240B
		// (set) Token: 0x06002925 RID: 10533 RVA: 0x000D3423 File Offset: 0x000D2423
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

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x000D3437 File Offset: 0x000D2437
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x040016E0 RID: 5856
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040016E1 RID: 5857
		private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x040016E2 RID: 5858
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(Type), null, new SchemaImporterExtensionElement.TypeTypeConverter(), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x02000351 RID: 849
		private class TypeAndName
		{
			// Token: 0x06002927 RID: 10535 RVA: 0x000D343F File Offset: 0x000D243F
			public TypeAndName(string name)
			{
				this.type = Type.GetType(name, true, true);
				this.name = name;
			}

			// Token: 0x06002928 RID: 10536 RVA: 0x000D345C File Offset: 0x000D245C
			public TypeAndName(Type type)
			{
				this.type = type;
			}

			// Token: 0x06002929 RID: 10537 RVA: 0x000D346B File Offset: 0x000D246B
			public override int GetHashCode()
			{
				return this.type.GetHashCode();
			}

			// Token: 0x0600292A RID: 10538 RVA: 0x000D3478 File Offset: 0x000D2478
			public override bool Equals(object comparand)
			{
				return this.type.Equals(((SchemaImporterExtensionElement.TypeAndName)comparand).type);
			}

			// Token: 0x040016E3 RID: 5859
			public readonly Type type;

			// Token: 0x040016E4 RID: 5860
			public readonly string name;
		}

		// Token: 0x02000352 RID: 850
		private class TypeTypeConverter : TypeConverter
		{
			// Token: 0x0600292B RID: 10539 RVA: 0x000D3490 File Offset: 0x000D2490
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x0600292C RID: 10540 RVA: 0x000D34A9 File Offset: 0x000D24A9
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return new SchemaImporterExtensionElement.TypeAndName((string)value);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x0600292D RID: 10541 RVA: 0x000D34C8 File Offset: 0x000D24C8
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType != typeof(string))
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
