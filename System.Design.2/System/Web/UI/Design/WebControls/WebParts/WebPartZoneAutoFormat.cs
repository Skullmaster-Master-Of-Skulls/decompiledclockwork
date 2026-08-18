using System;
using System.Design;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000153 RID: 339
	internal sealed class WebPartZoneAutoFormat : ReflectionBasedAutoFormat
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0004B507 File Offset: 0x00049707
		public WebPartZoneAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 250;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0004B528 File Offset: 0x00049728
		public override Control GetPreviewControl(Control runtimeControl)
		{
			WebPartZone webPartZone = (WebPartZone)base.GetPreviewControl(runtimeControl);
			if (webPartZone != null && webPartZone.WebParts.Count == 0)
			{
				webPartZone.ZoneTemplate = new WebPartZoneAutoFormat.AutoFormatTemplate();
			}
			return webPartZone;
		}

		// Token: 0x02000460 RID: 1120
		private sealed class AutoFormatTemplate : ITemplate
		{
			// Token: 0x0600298E RID: 10638 RVA: 0x000FABA5 File Offset: 0x000F8DA5
			public void InstantiateIn(Control container)
			{
				container.Controls.Add(new WebPartZoneAutoFormat.AutoFormatTemplate.SampleWebPart());
			}

			// Token: 0x020005CA RID: 1482
			private sealed class SampleWebPart : WebPart
			{
				// Token: 0x06003412 RID: 13330 RVA: 0x0011C438 File Offset: 0x0011A638
				public SampleWebPart()
				{
					this.Title = SR.GetString("WebPartZoneAutoFormat_SampleWebPartTitle");
					this.ID = "SampleWebPart";
				}

				// Token: 0x06003413 RID: 13331 RVA: 0x0011C45B File Offset: 0x0011A65B
				protected internal override void RenderContents(HtmlTextWriter writer)
				{
					writer.Write(SR.GetString("WebPartZoneAutoFormat_SampleWebPartContents"));
				}
			}
		}
	}
}
