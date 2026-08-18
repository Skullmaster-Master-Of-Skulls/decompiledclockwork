using System;
using System.Configuration;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000675 RID: 1653
	public class PrivacyNoticeElement : BindingElementExtensionElement
	{
		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000F08E1 File Offset: 0x000EEAE1
		// (set) Token: 0x06003F61 RID: 16225 RVA: 0x000F08F3 File Offset: 0x000EEAF3
		[ConfigurationProperty("url")]
		public Uri Url
		{
			get
			{
				return (Uri)base["url"];
			}
			set
			{
				base["url"] = value;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x000F0901 File Offset: 0x000EEB01
		// (set) Token: 0x06003F63 RID: 16227 RVA: 0x000F0913 File Offset: 0x000EEB13
		[ConfigurationProperty("version", DefaultValue = 0)]
		[IntegerValidator(MinValue = 0)]
		public int Version
		{
			get
			{
				return (int)base["version"];
			}
			set
			{
				base["version"] = value;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000F0926 File Offset: 0x000EEB26
		public override Type BindingElementType
		{
			get
			{
				return typeof(PrivacyNoticeBindingElement);
			}
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000F0934 File Offset: 0x000EEB34
		public override void ApplyConfiguration(BindingElement bindingElement)
		{
			base.ApplyConfiguration(bindingElement);
			PrivacyNoticeBindingElement privacyNoticeBindingElement = (PrivacyNoticeBindingElement)bindingElement;
			privacyNoticeBindingElement.Url = this.Url;
			privacyNoticeBindingElement.Version = this.Version;
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000F0968 File Offset: 0x000EEB68
		protected internal override BindingElement CreateBindingElement()
		{
			PrivacyNoticeBindingElement privacyNoticeBindingElement = new PrivacyNoticeBindingElement();
			this.ApplyConfiguration(privacyNoticeBindingElement);
			return privacyNoticeBindingElement;
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000F0984 File Offset: 0x000EEB84
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			PrivacyNoticeElement privacyNoticeElement = (PrivacyNoticeElement)from;
			this.Url = privacyNoticeElement.Url;
			this.Version = privacyNoticeElement.Version;
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x000F09B8 File Offset: 0x000EEBB8
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			PrivacyNoticeBindingElement privacyNoticeBindingElement = (PrivacyNoticeBindingElement)bindingElement;
			base.SetPropertyValueIfNotDefaultValue<Uri>("url", privacyNoticeBindingElement.Url);
			base.SetPropertyValueIfNotDefaultValue<int>("version", privacyNoticeBindingElement.Version);
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x000F09F8 File Offset: 0x000EEBF8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("url", typeof(Uri), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("version", typeof(int), 0, null, new IntegerValidator(0, int.MaxValue, false), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CB6 RID: 11446
		private ConfigurationPropertyCollection properties;
	}
}
