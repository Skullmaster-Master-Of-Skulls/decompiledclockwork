using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200064F RID: 1615
	public sealed class NonDualMessageSecurityOverHttpElement : MessageSecurityOverHttpElement
	{
		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x000ED614 File Offset: 0x000EB814
		// (set) Token: 0x06003E46 RID: 15942 RVA: 0x000ED626 File Offset: 0x000EB826
		[ConfigurationProperty("establishSecurityContext", DefaultValue = true)]
		public bool EstablishSecurityContext
		{
			get
			{
				return (bool)base["establishSecurityContext"];
			}
			set
			{
				base["establishSecurityContext"] = value;
			}
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000ED639 File Offset: 0x000EB839
		internal void ApplyConfiguration(NonDualMessageSecurityOverHttp security)
		{
			base.ApplyConfiguration(security);
			security.EstablishSecurityContext = this.EstablishSecurityContext;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x000ED64E File Offset: 0x000EB84E
		internal void InitializeFrom(NonDualMessageSecurityOverHttp security)
		{
			base.InitializeFrom(security);
			base.SetPropertyValueIfNotDefaultValue<bool>("establishSecurityContext", security.EstablishSecurityContext);
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003E49 RID: 15945 RVA: 0x000ED668 File Offset: 0x000EB868
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("establishSecurityContext", typeof(bool), true, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x04002CA2 RID: 11426
		private ConfigurationPropertyCollection properties;
	}
}
