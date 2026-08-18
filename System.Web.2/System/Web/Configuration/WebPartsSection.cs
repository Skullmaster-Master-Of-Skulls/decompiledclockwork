using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200077C RID: 1916
	public sealed class WebPartsSection : ConfigurationSection
	{
		// Token: 0x06005C2D RID: 23597 RVA: 0x0013F304 File Offset: 0x0013D504
		static WebPartsSection()
		{
			WebPartsSection._properties = new ConfigurationPropertyCollection();
			WebPartsSection._properties.Add(WebPartsSection._propEnableExport);
			WebPartsSection._properties.Add(WebPartsSection._propPersonalization);
			WebPartsSection._properties.Add(WebPartsSection._propTransformers);
		}

		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x06005C2F RID: 23599 RVA: 0x0013F39E File Offset: 0x0013D59E
		// (set) Token: 0x06005C30 RID: 23600 RVA: 0x0013F3B0 File Offset: 0x0013D5B0
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

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x06005C31 RID: 23601 RVA: 0x0013F3C3 File Offset: 0x0013D5C3
		[ConfigurationProperty("personalization")]
		public WebPartsPersonalization Personalization
		{
			get
			{
				return (WebPartsPersonalization)base[WebPartsSection._propPersonalization];
			}
		}

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x06005C32 RID: 23602 RVA: 0x0013F3D5 File Offset: 0x0013D5D5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsSection._properties;
			}
		}

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x06005C33 RID: 23603 RVA: 0x0013F3DC File Offset: 0x0013D5DC
		[ConfigurationProperty("transformers")]
		public TransformerInfoCollection Transformers
		{
			get
			{
				return (TransformerInfoCollection)base[WebPartsSection._propTransformers];
			}
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x0013F3EE File Offset: 0x0013D5EE
		protected override object GetRuntimeObject()
		{
			this.Personalization.ValidateAuthorization();
			return base.GetRuntimeObject();
		}

		// Token: 0x0400307B RID: 12411
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x0400307C RID: 12412
		private static readonly ConfigurationProperty _propEnableExport = new ConfigurationProperty("enableExport", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x0400307D RID: 12413
		private static readonly ConfigurationProperty _propPersonalization = new ConfigurationProperty("personalization", typeof(WebPartsPersonalization), null, ConfigurationPropertyOptions.None);

		// Token: 0x0400307E RID: 12414
		private static readonly ConfigurationProperty _propTransformers = new ConfigurationProperty("transformers", typeof(TransformerInfoCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
