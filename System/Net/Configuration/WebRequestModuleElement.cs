using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Net.Configuration
{
	// Token: 0x0200066B RID: 1643
	public sealed class WebRequestModuleElement : ConfigurationElement
	{
		// Token: 0x060032D0 RID: 13008 RVA: 0x000D76D4 File Offset: 0x000D66D4
		public WebRequestModuleElement()
		{
			this.properties.Add(this.prefix);
			this.properties.Add(this.type);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000D7752 File Offset: 0x000D6752
		public WebRequestModuleElement(string prefix, string type) : this()
		{
			this.Prefix = prefix;
			base[this.type] = new WebRequestModuleElement.TypeAndName(type);
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000D7773 File Offset: 0x000D6773
		public WebRequestModuleElement(string prefix, Type type) : this()
		{
			this.Prefix = prefix;
			this.Type = type;
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000D7789 File Offset: 0x000D6789
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x000D7791 File Offset: 0x000D6791
		// (set) Token: 0x060032D5 RID: 13013 RVA: 0x000D77A4 File Offset: 0x000D67A4
		[ConfigurationProperty("prefix", IsRequired = true, IsKey = true)]
		public string Prefix
		{
			get
			{
				return (string)base[this.prefix];
			}
			set
			{
				base[this.prefix] = value;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000D77B4 File Offset: 0x000D67B4
		// (set) Token: 0x060032D7 RID: 13015 RVA: 0x000D77DE File Offset: 0x000D67DE
		[TypeConverter(typeof(WebRequestModuleElement.TypeTypeConverter))]
		[ConfigurationProperty("type")]
		public Type Type
		{
			get
			{
				WebRequestModuleElement.TypeAndName typeAndName = (WebRequestModuleElement.TypeAndName)base[this.type];
				if (typeAndName != null)
				{
					return typeAndName.type;
				}
				return null;
			}
			set
			{
				base[this.type] = new WebRequestModuleElement.TypeAndName(value);
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000D77F2 File Offset: 0x000D67F2
		internal string Key
		{
			get
			{
				return this.Prefix;
			}
		}

		// Token: 0x04002F77 RID: 12151
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F78 RID: 12152
		private readonly ConfigurationProperty prefix = new ConfigurationProperty("prefix", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002F79 RID: 12153
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(WebRequestModuleElement.TypeAndName), null, new WebRequestModuleElement.TypeTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x0200066C RID: 1644
		private class TypeAndName
		{
			// Token: 0x060032D9 RID: 13017 RVA: 0x000D77FA File Offset: 0x000D67FA
			public TypeAndName(string name)
			{
				this.type = Type.GetType(name, true, true);
				this.name = name;
			}

			// Token: 0x060032DA RID: 13018 RVA: 0x000D7817 File Offset: 0x000D6817
			public TypeAndName(Type type)
			{
				this.type = type;
			}

			// Token: 0x060032DB RID: 13019 RVA: 0x000D7826 File Offset: 0x000D6826
			public override int GetHashCode()
			{
				return this.type.GetHashCode();
			}

			// Token: 0x060032DC RID: 13020 RVA: 0x000D7833 File Offset: 0x000D6833
			public override bool Equals(object comparand)
			{
				return this.type.Equals(((WebRequestModuleElement.TypeAndName)comparand).type);
			}

			// Token: 0x04002F7A RID: 12154
			public readonly Type type;

			// Token: 0x04002F7B RID: 12155
			public readonly string name;
		}

		// Token: 0x0200066D RID: 1645
		private class TypeTypeConverter : TypeConverter
		{
			// Token: 0x060032DD RID: 13021 RVA: 0x000D784B File Offset: 0x000D684B
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x060032DE RID: 13022 RVA: 0x000D7864 File Offset: 0x000D6864
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return new WebRequestModuleElement.TypeAndName((string)value);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x060032DF RID: 13023 RVA: 0x000D7884 File Offset: 0x000D6884
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType != typeof(string))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				WebRequestModuleElement.TypeAndName typeAndName = (WebRequestModuleElement.TypeAndName)value;
				if (typeAndName.name != null)
				{
					return typeAndName.name;
				}
				return typeAndName.type.AssemblyQualifiedName;
			}
		}
	}
}
