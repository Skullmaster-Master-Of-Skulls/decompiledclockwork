using System;
using System.IO;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.Calendar.Utils;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.Calendar.View
{
	// Token: 0x02001017 RID: 4119
	internal class CalendarRenderer : RendererBase
	{
		// Token: 0x0600A217 RID: 41495 RVA: 0x00240B06 File Offset: 0x0023ED06
		public CalendarRenderer(RadCalendar radCalendar)
		{
			this._RadCalendar = radCalendar;
		}

		// Token: 0x0600A218 RID: 41496 RVA: 0x00240B15 File Offset: 0x0023ED15
		public virtual void WriteHiddenFieldRegistration(HtmlTextWriter writer, string fieldPostfix, object data)
		{
			writer.Write(this.GetHiddenRegistration(fieldPostfix, data));
		}

		// Token: 0x0600A219 RID: 41497 RVA: 0x00240B28 File Offset: 0x0023ED28
		public virtual string GetHiddenRegistration(string fieldPostfix, object data)
		{
			string clientID = this._RadCalendar.ClientID;
			return string.Format(string.Concat(new string[]
			{
				"<input type=\"hidden\" name=\"{0}",
				fieldPostfix,
				"\" id=\"{0}",
				fieldPostfix,
				"\" value=\"{1}\" />"
			}), clientID, Utility.ConvertToClientArray1D(data));
		}

		// Token: 0x0600A21A RID: 41498 RVA: 0x00240B84 File Offset: 0x0023ED84
		internal static HtmlTextWriter CreateHtmlWriter(StringBuilder tempStream)
		{
			StringWriter writer = new StringWriter(tempStream);
			return new HtmlTextWriter(writer);
		}

		// Token: 0x04002D18 RID: 11544
		private RadCalendar _RadCalendar;
	}
}
