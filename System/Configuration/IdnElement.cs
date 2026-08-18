using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000674 RID: 1652
	public sealed class IdnElement : ConfigurationElement
	{
		// Token: 0x06003304 RID: 13060 RVA: 0x000D7E38 File Offset: 0x000D6E38
		public IdnElement()
		{
			this.properties.Add(this.enabled);
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000D7E8E File Offset: 0x000D6E8E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x000D7E96 File Offset: 0x000D6E96
		// (set) Token: 0x06003307 RID: 13063 RVA: 0x000D7EA9 File Offset: 0x000D6EA9
		[ConfigurationProperty("enabled", DefaultValue = UriIdnScope.None)]
		public UriIdnScope Enabled
		{
			get
			{
				return (UriIdnScope)base[this.enabled];
			}
			set
			{
				base[this.enabled] = value;
			}
		}

		// Token: 0x04002F88 RID: 12168
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F89 RID: 12169
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(UriIdnScope), UriIdnScope.None, new IdnElement.UriIdnScopeTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x02000675 RID: 1653
		private class UriIdnScopeTypeConverter : TypeConverter
		{
			// Token: 0x06003308 RID: 13064 RVA: 0x000D7EBD File Offset: 0x000D6EBD
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06003309 RID: 13065 RVA: 0x000D7ED8 File Offset: 0x000D6ED8
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text != null)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					string a;
					if ((a = text) != null)
					{
						if (a == "all")
						{
							return UriIdnScope.All;
						}
						if (a == "none")
						{
							return UriIdnScope.None;
						}
						if (a == "allexceptintranet")
						{
							return UriIdnScope.AllExceptIntranet;
						}
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
}
