using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x02000270 RID: 624
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartsSection : ConfigurationSection
	{
		// Token: 0x060020AF RID: 8367 RVA: 0x0008E428 File Offset: 0x0008D428
		static WebPartsSection()
		{
			WebPartsSection._properties = new ConfigurationPropertyCollection();
			WebPartsSection._properties.Add(WebPartsSection._propEnableExport);
			WebPartsSection._properties.Add(WebPartsSection._propPersonalization);
			WebPartsSection._properties.Add(WebPartsSection._propTransformers);
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x0008E4CA File Offset: 0x0008D4CA
		// (set) Token: 0x060020B2 RID: 8370 RVA: 0x0008E4DC File Offset: 0x0008D4DC
		[ConfigurationProperty("enableExport", DefaultValue = false)]
		public bool EnableExport
		{
			get
			{
				return (bool)base[WebPartsSection._propEnableExport];
			}
			set
			{
				base[WebPartsSection._propEnableExport] = value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0008E4EF File Offset: 0x0008D4EF
		[ConfigurationProperty("personalization")]
		public WebPartsPersonalization Personalization
		{
			get
			{
				return (WebPartsPersonalization)base[WebPartsSection._propPersonalization];
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x0008E501 File Offset: 0x0008D501
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsSection._properties;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x0008E508 File Offset: 0x0008D508
		[ConfigurationProperty("transformers")]
		public TransformerInfoCollection Transformers
		{
			get
			{
				return (TransformerInfoCollection)base[WebPartsSection._propTransformers];
			}
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0008E51A File Offset: 0x0008D51A
		protected override object GetRuntimeObject()
		{
			this.Personalization.ValidateAuthorization();
			return base.GetRuntimeObject();
		}

		// Token: 0x04001AB8 RID: 6840
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04001AB9 RID: 6841
		private static readonly ConfigurationProperty _propEnableExport = new ConfigurationProperty("enableExport", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001ABA RID: 6842
		private static readonly ConfigurationProperty _propPersonalization = new ConfigurationProperty("personalization", typeof(WebPartsPersonalization), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001ABB RID: 6843
		private static readonly ConfigurationProperty _propTransformers = new ConfigurationProperty("transformers", typeof(TransformerInfoCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
