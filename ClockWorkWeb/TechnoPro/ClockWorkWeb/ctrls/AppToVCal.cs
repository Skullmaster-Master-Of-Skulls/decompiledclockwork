using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace TechnoPro.ClockWorkWeb.ctrls
{
	// Token: 0x02000120 RID: 288
	public class AppToVCal : Page
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x0003B8CC File Offset: 0x00039ACC
		protected void Page_Load(object sender, EventArgs e)
		{
			string text = Guid.NewGuid().ToString().Replace("-", "");
			string text2 = DateTime.Now.AddHours(1.0).ToString("yyyyMMddTHH0000");
			string text3 = this.Page.Request.Params["description"];
			string text4 = this.Page.Request.Params["memo"];
			string text5 = this.Page.Request.Params["location"];
			string text6 = this.Page.Request.Params["startdate"];
			string text7 = this.Page.Request.Params["enddate"];
			bool flag = text3 == null;
			if (flag)
			{
				text3 = "";
			}
			bool flag2 = text4 == null;
			if (flag2)
			{
				text4 = "";
			}
			bool flag3 = text5 == null;
			if (flag3)
			{
				text5 = "";
			}
			base.Response.Clear();
			base.Response.Charset = "";
			base.Response.ContentType = "text/x-vCalendar";
			base.Response.AddHeader("Content-Disposition", "filename=Event.vcs;");
			StringWriter writer = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(writer);
			string format = "BEGIN:VCALENDAR\nPRODID:-//Microsoft Corporation//Outlook 11.0 MIMEDIR//EN\nVERSION:1.0\nBEGIN:VEVENT\nDTSTART:{0}\nDTEND:{1}\nUID:{5}\nSUMMARY;ENCODING=QUOTED-PRINTABLE:{2}\nDESCRIPTION;ENCODING=QUOTED-PRINTABLE:{3}\nLOCATION;ENCODING=QUOTED-PRINTABLE:{4}\nPRIORITY:3\nEnd:VEVENT\nEnd:VCALENDAR";
			base.Response.Write(string.Format(format, new object[]
			{
				text6,
				text7,
				text3,
				text4,
				text5,
				text
			}));
			base.Response.End();
		}

		// Token: 0x04000660 RID: 1632
		protected HtmlForm form1;
	}
}
