using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F9 RID: 1529
	public sealed class CertificateElement : ConfigurationElement
	{
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x000E220D File Offset: 0x000E040D
		// (set) Token: 0x06003AF3 RID: 15091 RVA: 0x000E221F File Offset: 0x000E041F
		[ConfigurationProperty("encodedValue", DefaultValue = "")]
		[StringValidator(MinLength = 0)]
		public string EncodedValue
		{
			get
			{
				return (string)base["encodedValue"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["encodedValue"] = value;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000E223C File Offset: 0x000E043C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("encodedValue", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A7A RID: 10874
		private ConfigurationPropertyCollection properties;
	}
}
