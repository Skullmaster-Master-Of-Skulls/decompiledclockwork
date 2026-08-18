using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000678 RID: 1656
	public class WSHttpBindingElement : WSHttpBindingBaseElement
	{
		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x000F13E4 File Offset: 0x000EF5E4
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
							configurationPropertyCollection.Add(new ConfigurationProperty("allowCookies", typeof(bool), false, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(WSHttpSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x000F1484 File Offset: 0x000EF684
		public WSHttpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x000F148D File Offset: 0x000EF68D
		public WSHttpBindingElement() : this(null)
		{
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x000F1496 File Offset: 0x000EF696
		protected override Type BindingElementType
		{
			get
			{
				return typeof(WSHttpBinding);
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06003F9C RID: 16284 RVA: 0x000F14A2 File Offset: 0x000EF6A2
		// (set) Token: 0x06003F9D RID: 16285 RVA: 0x000F14B4 File Offset: 0x000EF6B4
		[ConfigurationProperty("allowCookies", DefaultValue = false)]
		public bool AllowCookies
		{
			get
			{
				return (bool)base["allowCookies"];
			}
			set
			{
				base["allowCookies"] = value;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06003F9E RID: 16286 RVA: 0x000F14C7 File Offset: 0x000EF6C7
		[ConfigurationProperty("security")]
		public WSHttpSecurityElement Security
		{
			get
			{
				return (WSHttpSecurityElement)base["security"];
			}
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000F14DC File Offset: 0x000EF6DC
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			WSHttpBinding wshttpBinding = (WSHttpBinding)binding;
			base.SetPropertyValueIfNotDefaultValue<bool>("allowCookies", wshttpBinding.AllowCookies);
			this.Security.InitializeFrom(wshttpBinding.Security);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x000F151C File Offset: 0x000EF71C
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			WSHttpBinding wshttpBinding = (WSHttpBinding)binding;
			wshttpBinding.AllowCookies = this.AllowCookies;
			this.Security.ApplyConfiguration(wshttpBinding.Security);
		}

		// Token: 0x04002CBA RID: 11450
		private ConfigurationPropertyCollection properties;
	}
}
