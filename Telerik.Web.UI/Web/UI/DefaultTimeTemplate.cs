using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200100E RID: 4110
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class DefaultTimeTemplate : ITemplate
	{
		// Token: 0x170032F9 RID: 13049
		// (get) Token: 0x0600A12B RID: 41259 RVA: 0x0023DA17 File Offset: 0x0023BC17
		// (set) Token: 0x0600A12C RID: 41260 RVA: 0x0023DA1F File Offset: 0x0023BC1F
		[Localizable(true)]
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
			}
		}

		// Token: 0x170032FA RID: 13050
		// (get) Token: 0x0600A12D RID: 41261 RVA: 0x0023DA28 File Offset: 0x0023BC28
		// (set) Token: 0x0600A12E RID: 41262 RVA: 0x0023DA30 File Offset: 0x0023BC30
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x0600A130 RID: 41264 RVA: 0x0023DA44 File Offset: 0x0023BC44
		void ITemplate.InstantiateIn(Control owner)
		{
			this.link = new HtmlGenericControl();
			this.link.TagName = "a";
			this.link.Attributes["href"] = "#";
			this.link.DataBinding += this.span_DataBinding;
			owner.Controls.Add(this.link);
		}

		// Token: 0x0600A131 RID: 41265 RVA: 0x0023DAB0 File Offset: 0x0023BCB0
		private void span_DataBinding(object sender, EventArgs e)
		{
			HtmlGenericControl htmlGenericControl = (HtmlGenericControl)sender;
			DataListItem dataListItem = (DataListItem)htmlGenericControl.NamingContainer;
			if (dataListItem.DataItem is DataRowView)
			{
				htmlGenericControl.InnerText = ((DateTime)((DataRowView)dataListItem.DataItem)[RadTimeView.TimeColName]).ToString(this.format, this.Culture);
				return;
			}
			if (dataListItem.DataItem is DateTime)
			{
				htmlGenericControl.InnerText = ((DateTime)dataListItem.DataItem).ToString(this.format, this.Culture);
			}
		}

		// Token: 0x04002CFC RID: 11516
		private HtmlGenericControl link;

		// Token: 0x04002CFD RID: 11517
		private string format;

		// Token: 0x04002CFE RID: 11518
		private CultureInfo culture;
	}
}
