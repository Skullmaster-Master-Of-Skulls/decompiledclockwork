using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000007 RID: 7
	public abstract class ParameterValueElement : DeserializableConfigurationElement
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002411 File Offset: 0x00000611
		protected ParameterValueElement()
		{
			this.valueNumber = Interlocked.Increment(ref ParameterValueElement.valueElementCount);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000242C File Offset: 0x0000062C
		public string Key
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "value:{0}", new object[]
				{
					this.valueNumber
				});
			}
		}

		// Token: 0x06000021 RID: 33
		public abstract InjectionParameterValue GetInjectionParameterValue(IUnityContainer container, Type parameterType);

		// Token: 0x06000022 RID: 34 RVA: 0x00002460 File Offset: 0x00000660
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		protected static void GuardPropertyValueIsPresent(IDictionary<string, string> propertyValues, string requiredProperty)
		{
			Guard.ArgumentNotNull(propertyValues, "propertyValues");
			if (!propertyValues.ContainsKey(requiredProperty) || string.IsNullOrEmpty(propertyValues[requiredProperty]))
			{
				throw new ConfigurationErrorsException(string.Format(CultureInfo.CurrentCulture, Resources.RequiredPropertyMissing, new object[]
				{
					requiredProperty
				}));
			}
		}

		// Token: 0x04000005 RID: 5
		private static int valueElementCount;

		// Token: 0x04000006 RID: 6
		private readonly int valueNumber;
	}
}
