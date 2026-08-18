using System;
using System.Configuration;
using System.Web.UI;

namespace System.Web.Configuration
{
	// Token: 0x02000709 RID: 1801
	public sealed class IgnoreDeviceFilterElement : ConfigurationElement
	{
		// Token: 0x060056EC RID: 22252 RVA: 0x001300F8 File Offset: 0x0012E2F8
		static IgnoreDeviceFilterElement()
		{
			IgnoreDeviceFilterElement._properties = new ConfigurationPropertyCollection();
			IgnoreDeviceFilterElement._properties.Add(IgnoreDeviceFilterElement._propName);
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x00117E9E File Offset: 0x0011609E
		internal IgnoreDeviceFilterElement()
		{
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x00130164 File Offset: 0x0012E364
		public IgnoreDeviceFilterElement(string name)
		{
			base[IgnoreDeviceFilterElement._propName] = name;
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x060056EF RID: 22255 RVA: 0x00130178 File Offset: 0x0012E378
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IgnoreDeviceFilterElement._properties;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x060056F0 RID: 22256 RVA: 0x0013017F File Offset: 0x0012E37F
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		[StringValidator(MinLength = 1)]
		public string Name
		{
			get
			{
				return (string)base[IgnoreDeviceFilterElement._propName];
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x060056F1 RID: 22257 RVA: 0x00130191 File Offset: 0x0012E391
		protected override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return IgnoreDeviceFilterElement.s_elemProperty;
			}
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x00130198 File Offset: 0x0012E398
		private static void ValidateElement(object value)
		{
			IgnoreDeviceFilterElement ignoreDeviceFilterElement = (IgnoreDeviceFilterElement)value;
			if (Util.ContainsWhiteSpace(ignoreDeviceFilterElement.Name))
			{
				throw new ConfigurationErrorsException(SR.GetString("Space_attribute", new object[]
				{
					"name"
				}));
			}
		}

		// Token: 0x04002E31 RID: 11825
		private static readonly ConfigurationElementProperty s_elemProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(IgnoreDeviceFilterElement), new ValidatorCallback(IgnoreDeviceFilterElement.ValidateElement)));

		// Token: 0x04002E32 RID: 11826
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002E33 RID: 11827
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
