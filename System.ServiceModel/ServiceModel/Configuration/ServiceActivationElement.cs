using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000683 RID: 1667
	public sealed class ServiceActivationElement : ConfigurationElement
	{
		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x000F40F0 File Offset: 0x000F22F0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("relativeAddress", typeof(string), null, null, new ServiceActivationElement.RelativeAddressValidator(), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey),
						new ConfigurationProperty("service", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("factory", typeof(string), string.Empty, null, new StringValidator(0, int.MaxValue, null), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x000F4197 File Offset: 0x000F2397
		public ServiceActivationElement()
		{
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x000F419F File Offset: 0x000F239F
		public ServiceActivationElement(string relativeAddress) : this()
		{
			if (relativeAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("relativeAddress");
			}
			this.RelativeAddress = relativeAddress;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x000F41C1 File Offset: 0x000F23C1
		public ServiceActivationElement(string relativeAddress, string service) : this(relativeAddress)
		{
			this.Service = service;
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x000F41D1 File Offset: 0x000F23D1
		public ServiceActivationElement(string relativeAddress, string service, string factory) : this(relativeAddress, service)
		{
			this.Factory = factory;
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x000F41E2 File Offset: 0x000F23E2
		// (set) Token: 0x06004042 RID: 16450 RVA: 0x000F41F4 File Offset: 0x000F23F4
		[ConfigurationProperty("relativeAddress", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		[ServiceActivationElement.RelativeAddressValidatorAttribute]
		public string RelativeAddress
		{
			get
			{
				return (string)base["relativeAddress"];
			}
			set
			{
				base["relativeAddress"] = value;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x000F4202 File Offset: 0x000F2402
		// (set) Token: 0x06004044 RID: 16452 RVA: 0x000F4214 File Offset: 0x000F2414
		[ConfigurationProperty("service", Options = ConfigurationPropertyOptions.None)]
		[StringValidator(MinLength = 0)]
		public string Service
		{
			get
			{
				return (string)base["service"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["service"] = value;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x000F4231 File Offset: 0x000F2431
		// (set) Token: 0x06004046 RID: 16454 RVA: 0x000F4243 File Offset: 0x000F2443
		[ConfigurationProperty("factory", Options = ConfigurationPropertyOptions.None)]
		[StringValidator(MinLength = 0)]
		public string Factory
		{
			get
			{
				return (string)base["factory"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = string.Empty;
				}
				base["factory"] = value;
			}
		}

		// Token: 0x04002CCC RID: 11468
		private ConfigurationPropertyCollection properties;

		// Token: 0x04002CCD RID: 11469
		private const string PathSeparatorString = "/";

		// Token: 0x04002CCE RID: 11470
		private const string ReversSlashString = "\\";

		// Token: 0x02000CC3 RID: 3267
		private class RelativeAddressValidator : ConfigurationValidatorBase
		{
			// Token: 0x0600798E RID: 31118 RVA: 0x001C57FA File Offset: 0x001C39FA
			public override bool CanValidate(Type type)
			{
				return type == typeof(string);
			}

			// Token: 0x0600798F RID: 31119 RVA: 0x001C580C File Offset: 0x001C3A0C
			public override void Validate(object value)
			{
				string text = value as string;
				if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text.Trim()) || text.Length < 3)
				{
					throw FxTrace.Exception.AsError(new ArgumentException(SR.GetString("Hosting_RelativeAddressFormatError", new object[]
					{
						text
					})));
				}
				if (text.StartsWith("/", StringComparison.CurrentCultureIgnoreCase) || text.StartsWith("\\", StringComparison.CurrentCultureIgnoreCase))
				{
					throw FxTrace.Exception.AsError(new ArgumentException(SR.GetString("Hosting_NoAbsoluteRelativeAddress", new object[]
					{
						text
					})));
				}
			}
		}

		// Token: 0x02000CC4 RID: 3268
		[AttributeUsage(AttributeTargets.Property)]
		private sealed class RelativeAddressValidatorAttribute : ConfigurationValidatorAttribute
		{
			// Token: 0x17001B97 RID: 7063
			// (get) Token: 0x06007991 RID: 31121 RVA: 0x001C58AA File Offset: 0x001C3AAA
			public override ConfigurationValidatorBase ValidatorInstance
			{
				get
				{
					return new ServiceActivationElement.RelativeAddressValidator();
				}
			}
		}
	}
}
