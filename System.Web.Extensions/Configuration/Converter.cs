using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020000E0 RID: 224
	public class Converter : ConfigurationElement
	{
		// Token: 0x06000C89 RID: 3209 RVA: 0x0002A9EC File Offset: 0x00028BEC
		private static ConfigurationPropertyCollection BuildProperties()
		{
			return new ConfigurationPropertyCollection
			{
				Converter._propType,
				Converter._propName
			};
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0002AA16 File Offset: 0x00028C16
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Converter._properties;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002AA1D File Offset: 0x00028C1D
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0002AA2F File Offset: 0x00028C2F
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Type
		{
			get
			{
				return (string)base[Converter._propType];
			}
			set
			{
				base[Converter._propType] = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002AA3D File Offset: 0x00028C3D
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0002AA4F File Offset: 0x00028C4F
		[ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[Converter._propName];
			}
			set
			{
				base[Converter._propName] = value;
			}
		}

		// Token: 0x04000375 RID: 885
		private static TypeConverter _whiteSpaceTrimStringConverter = new WhiteSpaceTrimStringConverter();

		// Token: 0x04000376 RID: 886
		private static ConfigurationValidatorBase _nonEmptyStringValidator = new StringValidator(1);

		// Token: 0x04000377 RID: 887
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), null, Converter._whiteSpaceTrimStringConverter, Converter._nonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000378 RID: 888
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, Converter._whiteSpaceTrimStringConverter, Converter._nonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000379 RID: 889
		private static ConfigurationPropertyCollection _properties = Converter.BuildProperties();
	}
}
