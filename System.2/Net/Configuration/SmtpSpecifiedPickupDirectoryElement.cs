using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000346 RID: 838
	public sealed class SmtpSpecifiedPickupDirectoryElement : ConfigurationElement
	{
		// Token: 0x06001E24 RID: 7716 RVA: 0x0008D8E9 File Offset: 0x0008BAE9
		public SmtpSpecifiedPickupDirectoryElement()
		{
			this.properties.Add(this.pickupDirectoryLocation);
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x0008D929 File Offset: 0x0008BB29
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x0008D931 File Offset: 0x0008BB31
		// (set) Token: 0x06001E27 RID: 7719 RVA: 0x0008D944 File Offset: 0x0008BB44
		[ConfigurationProperty("pickupDirectoryLocation")]
		public string PickupDirectoryLocation
		{
			get
			{
				return (string)base[this.pickupDirectoryLocation];
			}
			set
			{
				base[this.pickupDirectoryLocation] = value;
			}
		}

		// Token: 0x04001CB3 RID: 7347
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001CB4 RID: 7348
		private readonly ConfigurationProperty pickupDirectoryLocation = new ConfigurationProperty("pickupDirectoryLocation", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}
