using System;
using System.Configuration;
using System.IdentityModel.Selectors;

namespace System.IdentityModel.Configuration
{
	// Token: 0x020001C0 RID: 448
	[ConfigurationCollection(typeof(AudienceUriElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class AudienceUriElementCollection : ConfigurationElementCollection
	{
		// Token: 0x06000E77 RID: 3703 RVA: 0x00041CC5 File Offset: 0x0003FEC5
		protected override void Init()
		{
			base.Init();
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00041CCD File Offset: 0x0003FECD
		protected override ConfigurationElement CreateNewElement()
		{
			return new AudienceUriElement();
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00041CD4 File Offset: 0x0003FED4
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AudienceUriElement)element).Value;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00041CE1 File Offset: 0x0003FEE1
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x00041CF3 File Offset: 0x0003FEF3
		[ConfigurationProperty("mode", IsRequired = false, DefaultValue = AudienceUriMode.Always)]
		[StandardRuntimeEnumValidator(typeof(AudienceUriMode))]
		public AudienceUriMode Mode
		{
			get
			{
				return (AudienceUriMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00041D06 File Offset: 0x0003FF06
		internal bool IsConfigured
		{
			get
			{
				return base.ElementInformation.Properties["mode"].ValueOrigin != PropertyValueOrigin.Default || base.Count > 0;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00041D30 File Offset: 0x0003FF30
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(AudienceUriMode), AudienceUriMode.Always, null, new StandardRuntimeEnumValidator(typeof(AudienceUriMode)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04000D11 RID: 3345
		private const AudienceUriMode DefaultAudienceUriMode = AudienceUriMode.Always;

		// Token: 0x04000D12 RID: 3346
		private ConfigurationPropertyCollection properties;
	}
}
