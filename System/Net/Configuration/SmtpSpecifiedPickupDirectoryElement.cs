using System;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000666 RID: 1638
	public sealed class SmtpSpecifiedPickupDirectoryElement : ConfigurationElement
	{
		// Token: 0x060032BB RID: 12987 RVA: 0x000D73AD File Offset: 0x000D63AD
		public SmtpSpecifiedPickupDirectoryElement()
		{
			this.properties.Add(this.pickupDirectoryLocation);
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060032BC RID: 12988 RVA: 0x000D73ED File Offset: 0x000D63ED
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x000D73F5 File Offset: 0x000D63F5
		// (set) Token: 0x060032BE RID: 12990 RVA: 0x000D7408 File Offset: 0x000D6408
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

		// Token: 0x04002F6E RID: 12142
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F6F RID: 12143
		private readonly ConfigurationProperty pickupDirectoryLocation = new ConfigurationProperty("pickupDirectoryLocation", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}
