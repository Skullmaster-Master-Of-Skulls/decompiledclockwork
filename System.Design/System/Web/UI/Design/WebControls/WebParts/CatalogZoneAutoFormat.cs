using System;
using System.Data;
using System.Design;
using System.Globalization;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x0200052C RID: 1324
	internal sealed class CatalogZoneAutoFormat : BaseAutoFormat
	{
		// Token: 0x06002F1A RID: 12058 RVA: 0x0010D6DE File Offset: 0x0010C6DE
		public CatalogZoneAutoFormat(DataRow schemeData) : base(schemeData)
		{
			base.Style.Width = 300;
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x0010D6FC File Offset: 0x0010C6FC
		public override Control GetPreviewControl(Control runtimeControl)
		{
			CatalogZone catalogZone = (CatalogZone)base.GetPreviewControl(runtimeControl);
			if (catalogZone != null && catalogZone.CatalogParts.Count == 0)
			{
				catalogZone.ZoneTemplate = new CatalogZoneAutoFormat.AutoFormatTemplate();
			}
			catalogZone.ID = "AutoFormatPreviewControl";
			return catalogZone;
		}

		// Token: 0x0400202C RID: 8236
		internal const string PreviewControlID = "AutoFormatPreviewControl";

		// Token: 0x0200052D RID: 1325
		private sealed class AutoFormatTemplate : ITemplate
		{
			// Token: 0x06002F1C RID: 12060 RVA: 0x0010D740 File Offset: 0x0010C740
			public void InstantiateIn(Control container)
			{
				DeclarativeCatalogPart declarativeCatalogPart = new DeclarativeCatalogPart();
				declarativeCatalogPart.WebPartsTemplate = new CatalogZoneAutoFormat.AutoFormatTemplate.SampleCatalogPartTemplate();
				declarativeCatalogPart.ID = "SampleCatalogPart";
				container.Controls.Add(declarativeCatalogPart);
			}

			// Token: 0x0200052E RID: 1326
			private sealed class SampleCatalogPartTemplate : ITemplate
			{
				// Token: 0x06002F1E RID: 12062 RVA: 0x0010D780 File Offset: 0x0010C780
				public void InstantiateIn(Control container)
				{
					CatalogZoneAutoFormat.AutoFormatTemplate.SampleCatalogPartTemplate.SampleWebPart sampleWebPart = new CatalogZoneAutoFormat.AutoFormatTemplate.SampleCatalogPartTemplate.SampleWebPart();
					sampleWebPart.ID = "SampleWebPart1";
					sampleWebPart.Title = string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogZone_SampleWebPartTitle"), new object[]
					{
						"1"
					});
					container.Controls.Add(sampleWebPart);
					sampleWebPart = new CatalogZoneAutoFormat.AutoFormatTemplate.SampleCatalogPartTemplate.SampleWebPart();
					sampleWebPart.ID = "SampleWebPart2";
					sampleWebPart.Title = string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogZone_SampleWebPartTitle"), new object[]
					{
						"2"
					});
					container.Controls.Add(sampleWebPart);
					sampleWebPart = new CatalogZoneAutoFormat.AutoFormatTemplate.SampleCatalogPartTemplate.SampleWebPart();
					sampleWebPart.ID = "SampleWebPart3";
					sampleWebPart.Title = string.Format(CultureInfo.CurrentCulture, SR.GetString("CatalogZone_SampleWebPartTitle"), new object[]
					{
						"3"
					});
					container.Controls.Add(sampleWebPart);
				}

				// Token: 0x0200052F RID: 1327
				private sealed class SampleWebPart : WebPart
				{
				}
			}
		}
	}
}
