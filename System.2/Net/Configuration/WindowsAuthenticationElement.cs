using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200034F RID: 847
	public sealed class WindowsAuthenticationElement : ConfigurationElement
	{
		// Token: 0x06001E5F RID: 7775 RVA: 0x0008E1CC File Offset: 0x0008C3CC
		public WindowsAuthenticationElement()
		{
			this.defaultCredentialsHandleCacheSize = new ConfigurationProperty("defaultCredentialsHandleCacheSize", typeof(int), 0, null, new WindowsAuthenticationElement.CacheSizeValidator(), ConfigurationPropertyOptions.None);
			this.properties = new ConfigurationPropertyCollection();
			this.properties.Add(this.defaultCredentialsHandleCacheSize);
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0008E222 File Offset: 0x0008C422
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x0008E22A File Offset: 0x0008C42A
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x0008E23D File Offset: 0x0008C43D
		[ConfigurationProperty("defaultCredentialsHandleCacheSize", DefaultValue = 0)]
		public int DefaultCredentialsHandleCacheSize
		{
			get
			{
				return (int)base[this.defaultCredentialsHandleCacheSize];
			}
			set
			{
				base[this.defaultCredentialsHandleCacheSize] = value;
			}
		}

		// Token: 0x04001CC7 RID: 7367
		private ConfigurationPropertyCollection properties;

		// Token: 0x04001CC8 RID: 7368
		private readonly ConfigurationProperty defaultCredentialsHandleCacheSize;

		// Token: 0x020007CB RID: 1995
		private class CacheSizeValidator : ConfigurationValidatorBase
		{
			// Token: 0x060043A4 RID: 17316 RVA: 0x0011D33E File Offset: 0x0011B53E
			public override bool CanValidate(Type type)
			{
				return type == typeof(int);
			}

			// Token: 0x060043A5 RID: 17317 RVA: 0x0011D350 File Offset: 0x0011B550
			public override void Validate(object value)
			{
				int num = (int)value;
				if (num < 0)
				{
					throw new ArgumentOutOfRangeException("value", num, SR.GetString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[]
					{
						0,
						int.MaxValue
					}));
				}
			}
		}
	}
}
