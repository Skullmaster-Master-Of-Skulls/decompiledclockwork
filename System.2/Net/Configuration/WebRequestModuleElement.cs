using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Net.Configuration
{
	// Token: 0x0200034A RID: 842
	public sealed class WebRequestModuleElement : ConfigurationElement
	{
		// Token: 0x06001E3A RID: 7738 RVA: 0x0008DC5C File Offset: 0x0008BE5C
		public WebRequestModuleElement()
		{
			this.properties.Add(this.prefix);
			this.properties.Add(this.type);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0008DCDA File Offset: 0x0008BEDA
		public WebRequestModuleElement(string prefix, string type) : this()
		{
			this.Prefix = prefix;
			base[this.type] = new WebRequestModuleElement.TypeAndName(type);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0008DCFB File Offset: 0x0008BEFB
		public WebRequestModuleElement(string prefix, Type type) : this()
		{
			this.Prefix = prefix;
			this.Type = type;
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x0008DD11 File Offset: 0x0008BF11
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x0008DD19 File Offset: 0x0008BF19
		// (set) Token: 0x06001E3F RID: 7743 RVA: 0x0008DD2C File Offset: 0x0008BF2C
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

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0008DD3C File Offset: 0x0008BF3C
		// (set) Token: 0x06001E41 RID: 7745 RVA: 0x0008DD66 File Offset: 0x0008BF66
		[ConfigurationProperty("type")]
		[TypeConverter(typeof(WebRequestModuleElement.TypeTypeConverter))]
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

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0008DD7A File Offset: 0x0008BF7A
		internal string Key
		{
			get
			{
				return this.Prefix;
			}
		}

		// Token: 0x04001CBD RID: 7357
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001CBE RID: 7358
		private readonly ConfigurationProperty prefix = new ConfigurationProperty("prefix", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001CBF RID: 7359
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(WebRequestModuleElement.TypeAndName), null, new WebRequestModuleElement.TypeTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x020007C8 RID: 1992
		private class TypeAndName
		{
			// Token: 0x06004399 RID: 17305 RVA: 0x0011D1FF File Offset: 0x0011B3FF
			public TypeAndName(string name)
			{
				this.type = Type.GetType(name, true, true);
				this.name = name;
			}

			// Token: 0x0600439A RID: 17306 RVA: 0x0011D21C File Offset: 0x0011B41C
			public TypeAndName(Type type)
			{
				this.type = type;
			}

			// Token: 0x0600439B RID: 17307 RVA: 0x0011D22B File Offset: 0x0011B42B
			public override int GetHashCode()
			{
				return this.type.GetHashCode();
			}

			// Token: 0x0600439C RID: 17308 RVA: 0x0011D238 File Offset: 0x0011B438
			public override bool Equals(object comparand)
			{
				return this.type.Equals(((WebRequestModuleElement.TypeAndName)comparand).type);
			}

			// Token: 0x04003482 RID: 13442
			public readonly Type type;

			// Token: 0x04003483 RID: 13443
			public readonly string name;
		}

		// Token: 0x020007C9 RID: 1993
		private class TypeTypeConverter : TypeConverter
		{
			// Token: 0x0600439D RID: 17309 RVA: 0x0011D250 File Offset: 0x0011B450
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x0600439E RID: 17310 RVA: 0x0011D26E File Offset: 0x0011B46E
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return new WebRequestModuleElement.TypeAndName((string)value);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x0600439F RID: 17311 RVA: 0x0011D290 File Offset: 0x0011B490
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (!(destinationType == typeof(string)))
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
