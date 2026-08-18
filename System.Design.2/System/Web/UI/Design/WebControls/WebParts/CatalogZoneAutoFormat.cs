using System;
using System.Design;
using System.Globalization;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000141 RID: 321
	internal sealed class CatalogZoneAutoFormat : ReflectionBasedAutoFormat
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x0004A7DE File Offset: 0x000489DE
		public CatalogZoneAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 300;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0004A800 File Offset: 0x00048A00
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

		// Token: 0x04000701 RID: 1793
		internal const string PreviewControlID = "AutoFormatPreviewControl";

		// Token: 0x02000458 RID: 1112
		private sealed class AutoFormatTemplate : ITemplate
		{
			// Token: 0x06002977 RID: 10615 RVA: 0x000FAA04 File Offset: 0x000F8C04
			public void InstantiateIn(Control container)
			{
				DeclarativeCatalogPart declarativeCatalogPart = new DeclarativeCatalogPart();
				declarativeCatalogPart.WebPartsTemplate = new CatalogZoneAutoFormat.AutoFormatTemplate.SampleCatalogPartTemplate();
				declarativeCatalogPart.ID = "SampleCatalogPart";
				container.Controls.Add(declarativeCatalogPart);
			}

			// Token: 0x020005C9 RID: 1481
			private sealed class SampleCatalogPartTemplate : ITemplate
			{
				// Token: 0x06003410 RID: 13328 RVA: 0x0011C35C File Offset: 0x0011A55C
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

				// Token: 0x020005F5 RID: 1525
				private sealed class SampleWebPart : WebPart
				{
				}
			}
		}
	}
}
