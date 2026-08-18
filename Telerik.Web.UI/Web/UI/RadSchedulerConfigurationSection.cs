using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x020012E1 RID: 4833
	public class RadSchedulerConfigurationSection : ConfigurationSection
	{
		// Token: 0x17004182 RID: 16770
		// (get) Token: 0x0600CAEB RID: 51947 RVA: 0x002D571E File Offset: 0x002D391E
		[ConfigurationProperty("appointmentProviders")]
		public ProviderSettingsCollection AppointmentProviders
		{
			get
			{
				return (ProviderSettingsCollection)base["appointmentProviders"];
			}
		}

		// Token: 0x17004183 RID: 16771
		// (get) Token: 0x0600CAEC RID: 51948 RVA: 0x002D5730 File Offset: 0x002D3930
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultAppointmentProvider", DefaultValue = "Integrated")]
		public string DefaultAppointmentProvider
		{
			get
			{
				return (string)base["defaultAppointmentProvider"];
			}
		}
	}
}
