using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B5 RID: 1717
	public class WSFederationHttpBindingElement : WSHttpBindingBaseElement
	{
		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x000FBBA8 File Offset: 0x000F9DA8
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
							configurationPropertyCollection.Add(new ConfigurationProperty("privacyNoticeAt", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("privacyNoticeVersion", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("security", typeof(WSFederationHttpSecurityElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000FBC74 File Offset: 0x000F9E74
		public WSFederationHttpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000FBC7D File Offset: 0x000F9E7D
		public WSFederationHttpBindingElement() : this(null)
		{
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x0600428C RID: 17036 RVA: 0x000FBC86 File Offset: 0x000F9E86
		protected override Type BindingElementType
		{
			get
			{
				return typeof(WSFederationHttpBinding);
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600428D RID: 17037 RVA: 0x000FBC92 File Offset: 0x000F9E92
		// (set) Token: 0x0600428E RID: 17038 RVA: 0x000FBCA4 File Offset: 0x000F9EA4
		[ConfigurationProperty("privacyNoticeAt", DefaultValue = null)]
		public Uri PrivacyNoticeAt
		{
			get
			{
				return (Uri)base["privacyNoticeAt"];
			}
			set
			{
				base["privacyNoticeAt"] = value;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x000FBCB2 File Offset: 0x000F9EB2
		// (set) Token: 0x06004290 RID: 17040 RVA: 0x000FBCC4 File Offset: 0x000F9EC4
		[ConfigurationProperty("privacyNoticeVersion", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int PrivacyNoticeVersion
		{
			get
			{
				return (int)base["privacyNoticeVersion"];
			}
			set
			{
				base["privacyNoticeVersion"] = value;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x000FBCD7 File Offset: 0x000F9ED7
		[ConfigurationProperty("security")]
		public WSFederationHttpSecurityElement Security
		{
			get
			{
				return (WSFederationHttpSecurityElement)base["security"];
			}
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x000FBCEC File Offset: 0x000F9EEC
		protected internal override void InitializeFrom(Binding binding)
		{
			base.InitializeFrom(binding);
			WSFederationHttpBinding wsfederationHttpBinding = (WSFederationHttpBinding)binding;
			if (wsfederationHttpBinding.PrivacyNoticeAt != null)
			{
				base.SetPropertyValueIfNotDefaultValue<Uri>("privacyNoticeAt", wsfederationHttpBinding.PrivacyNoticeAt);
				base.SetPropertyValueIfNotDefaultValue<int>("privacyNoticeVersion", wsfederationHttpBinding.PrivacyNoticeVersion);
			}
			this.Security.InitializeFrom(wsfederationHttpBinding.Security);
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x000FBD48 File Offset: 0x000F9F48
		protected override void OnApplyConfiguration(Binding binding)
		{
			base.OnApplyConfiguration(binding);
			WSFederationHttpBinding wsfederationHttpBinding = (WSFederationHttpBinding)binding;
			if (this.PrivacyNoticeAt != null)
			{
				wsfederationHttpBinding.PrivacyNoticeAt = this.PrivacyNoticeAt;
				wsfederationHttpBinding.PrivacyNoticeVersion = this.PrivacyNoticeVersion;
			}
			this.Security.ApplyConfiguration(wsfederationHttpBinding.Security);
		}

		// Token: 0x04002D04 RID: 11524
		private ConfigurationPropertyCollection properties;
	}
}
