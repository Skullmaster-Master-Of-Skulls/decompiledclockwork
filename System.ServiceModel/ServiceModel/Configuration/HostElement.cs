using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000620 RID: 1568
	public sealed class HostElement : ConfigurationElement
	{
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003C3F RID: 15423 RVA: 0x000E640D File Offset: 0x000E460D
		[ConfigurationProperty("baseAddresses", Options = ConfigurationPropertyOptions.None)]
		public BaseAddressElementCollection BaseAddresses
		{
			get
			{
				return (BaseAddressElementCollection)base["baseAddresses"];
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x000E641F File Offset: 0x000E461F
		[ConfigurationProperty("timeouts", Options = ConfigurationPropertyOptions.None)]
		public HostTimeoutsElement Timeouts
		{
			get
			{
				return (HostTimeoutsElement)base["timeouts"];
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x000E6434 File Offset: 0x000E4634
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("baseAddresses", typeof(BaseAddressElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("timeouts", typeof(HostTimeoutsElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C7E RID: 11390
		private ConfigurationPropertyCollection properties;
	}
}
