using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.Handlers
{
	// Token: 0x020001A5 RID: 421
	public class TraceHandler : IHttpHandler
	{
		// Token: 0x0600161F RID: 5663 RVA: 0x000030B5 File Offset: 0x000012B5
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public TraceHandler()
		{
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00045187 File Offset: 0x00043387
		protected void ProcessRequest(HttpContext context)
		{
			((IHttpHandler)this).ProcessRequest(context);
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00045190 File Offset: 0x00043390
		protected bool IsReusable
		{
			get
			{
				return ((IHttpHandler)this).IsReusable;
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00045198 File Offset: 0x00043398
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			if (DeploymentSection.RetailInternal || (!context.Request.IsLocal && HttpRuntime.Profile.LocalOnly))
			{
				HttpException ex = new HttpException(403, null);
				ex.SetFormatter(new TraceHandlerErrorFormatter(!DeploymentSection.RetailInternal));
				throw ex;
			}
			this._context = context;
			this._response = this._context.Response;
			this._request = this._context.Request;
			this._writer = Page.CreateHtmlTextWriterInternal(this._response.Output, this._request);
			if (context.WorkerRequest is IIS7WorkerRequest)
			{
				this._response.ContentType = this._request.Browser.PreferredRenderingMime;
			}
			if (this._writer == null)
			{
				return;
			}
			this._context.Trace.IsEnabled = false;
			this._request.ValidateInput();
			this._writer.Write("<html>\r\n");
			this._writer.Write("<head>\r\n");
			this._writer.Write(TraceHandler.StyleSheet);
			this._writer.Write("</head>\r\n");
			this._writer.Write("<body>\r\n");
			this._writer.Write("<span class=\"tracecontent\">\r\n");
			if (!HttpRuntime.Profile.IsConfigEnabled)
			{
				HttpException ex2 = new HttpException();
				ex2.SetFormatter(new TraceHandlerErrorFormatter(false));
				throw ex2;
			}
			IList data = HttpRuntime.Profile.GetData();
			if (this._request.QueryString["clear"] != null)
			{
				HttpRuntime.Profile.Reset();
				string rawUrl = this._request.RawUrl;
				this._response.Redirect(rawUrl.Substring(0, rawUrl.IndexOf("?", StringComparison.Ordinal)));
			}
			string text = this._request.QueryString["id"];
			if (text != null)
			{
				int num = int.Parse(text, CultureInfo.InvariantCulture);
				if (num >= 0 && num < data.Count)
				{
					this.ShowDetails((DataSet)data[num]);
					this.ShowVersionDetails();
					this._writer.Write("</span>\r\n</body>\r\n</html>\r\n");
					return;
				}
			}
			this.ShowRequests(data);
			this.ShowVersionDetails();
			this._writer.Write("</span>\r\n</body>\r\n</html>\r\n");
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x00007722 File Offset: 0x00005922
		bool IHttpHandler.IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000453D0 File Offset: 0x000435D0
		protected void ShowDetails(DataSet data)
		{
			if (data == null)
			{
				return;
			}
			this._writer.Write("<h1>" + SR.GetString("Trace_Request_Details") + "</h1><br>");
			Table table = TraceHandler.CreateDetailsTable(data.Tables["Trace_Request"]);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateTraceTable(data.Tables["Trace_Trace_Information"]);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateControlTable(data.Tables["Trace_Control_Tree"]);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateTable(data.Tables["Trace_Session_State"], true);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateTable(data.Tables["Trace_Application_State"], true);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateTable(data.Tables["Trace_Request_Cookies_Collection"], true);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateTable(data.Tables["Trace_Response_Cookies_Collection"], true);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateTable(data.Tables["Trace_Headers_Collection"], true);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateTable(data.Tables["Trace_Form_Collection"]);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateTable(data.Tables["Trace_Querystring_Collection"]);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
			table = TraceHandler.CreateTable(data.Tables["Trace_Server_Variables"], true);
			if (table != null)
			{
				table.RenderControl(this._writer);
			}
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000455A4 File Offset: 0x000437A4
		protected void ShowVersionDetails()
		{
			this._writer.Write("<hr width=100% size=1 color=silver>\r\n\r\n");
			this._writer.Write(string.Concat(new string[]
			{
				SR.GetString("Error_Formatter_CLR_Build"),
				VersionInfo.ClrVersion,
				SR.GetString("Error_Formatter_ASPNET_Build"),
				VersionInfo.EngineVersion,
				"\r\n\r\n"
			}));
			this._writer.Write("</font>\r\n\r\n");
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0004561C File Offset: 0x0004381C
		protected void ShowRequests(IList data)
		{
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.Width = Unit.Percentage(100.0);
			TableRow tableRow = TraceHandler.AddRow(table);
			TraceHandler.AddCell(tableRow, SR.GetString("Trace_Application_Trace"));
			string applicationPath = this._request.ApplicationPath;
			int length = applicationPath.Length;
			tableRow = TraceHandler.AddRow(table);
			TraceHandler.AddCell(tableRow, "<h2>" + HttpUtility.HtmlEncode(applicationPath.Substring(1)) + "<h2><p>");
			tableRow = TraceHandler.AddRow(table);
			TraceHandler.AddCell(tableRow, "[ <a href=\"Trace.axd?clear=1\" class=\"link\">" + SR.GetString("Trace_Clear_Current") + "</a> ]");
			string text = "&nbsp";
			if (HttpRuntime.HasAppPathDiscoveryPermission())
			{
				text = SR.GetString("Trace_Physical_Directory") + this._request.PhysicalApplicationPath;
			}
			tableRow = TraceHandler.AddRow(table);
			TableCell tableCell = TraceHandler.AddCell(tableRow, text);
			table.RenderControl(this._writer);
			table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.Width = Unit.Percentage(100.0);
			tableRow = TraceHandler.AddRow(table);
			tableCell = TraceHandler.AddHeaderCell(tableRow, "<h3><b>" + SR.GetString("Trace_Requests_This") + "</b></h3>");
			tableCell.ColumnSpan = 5;
			tableCell.CssClass = "alt";
			tableCell.HorizontalAlign = HorizontalAlign.Left;
			tableCell = TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Remaining") + " " + HttpRuntime.Profile.RequestsRemaining.ToString(NumberFormatInfo.InvariantInfo));
			tableCell.CssClass = "alt";
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			tableRow = TraceHandler.AddRow(table);
			tableRow.HorizontalAlign = HorizontalAlign.Left;
			tableRow.CssClass = "subhead";
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_No"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Time_of_Request"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_File"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Status_Code"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Verb"));
			TraceHandler.AddHeaderCell(tableRow, "&nbsp");
			bool flag = true;
			for (int i = 0; i < data.Count; i++)
			{
				DataSet dataSet = (DataSet)data[i];
				tableRow = TraceHandler.AddRow(table);
				if (flag)
				{
					tableRow.CssClass = "alt";
				}
				TraceHandler.AddCell(tableRow, (i + 1).ToString(NumberFormatInfo.InvariantInfo));
				TraceHandler.AddCell(tableRow, (string)dataSet.Tables["Trace_Request"].Rows[0]["Trace_Time_of_Request"]);
				TraceHandler.AddCell(tableRow, HttpUtility.HtmlEncode((string)dataSet.Tables["Trace_Request"].Rows[0]["Trace_Url"]).Substring(length));
				TraceHandler.AddCell(tableRow, dataSet.Tables["Trace_Request"].Rows[0]["Trace_Status_Code"].ToString());
				TraceHandler.AddCell(tableRow, (string)dataSet.Tables["Trace_Request"].Rows[0]["Trace_Request_Type"]);
				TableCell tableCell2 = TraceHandler.AddCell(tableRow, string.Empty);
				HtmlAnchor htmlAnchor = new HtmlAnchor();
				htmlAnchor.HRef = "Trace.axd?id=" + i.ToString();
				htmlAnchor.InnerHtml = "<nobr>" + SR.GetString("Trace_View_Details");
				htmlAnchor.Attributes["class"] = "link";
				tableCell2.Controls.Add(htmlAnchor);
				flag = !flag;
			}
			table.RenderControl(this._writer);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000459E8 File Offset: 0x00043BE8
		private static TableRow AddRow(Table t)
		{
			TableRow tableRow = new TableRow();
			t.Rows.Add(tableRow);
			return tableRow;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00045A0C File Offset: 0x00043C0C
		private static TableCell AddHeaderCell(TableRow trow, string text)
		{
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			tableHeaderCell.Text = text;
			trow.Cells.Add(tableHeaderCell);
			return tableHeaderCell;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00045A34 File Offset: 0x00043C34
		private static TableCell AddCell(TableRow trow, string text)
		{
			TableCell tableCell = new TableCell();
			tableCell.Text = text;
			trow.Cells.Add(tableCell);
			return tableCell;
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x00045A5C File Offset: 0x00043C5C
		internal static string StyleSheet
		{
			get
			{
				return "<style type=\"text/css\">\r\nspan.tracecontent b { color:white }\r\nspan.tracecontent { background-color:white; color:black;font: 10pt verdana, arial; }\r\nspan.tracecontent table { clear:left; font: 10pt verdana, arial; cellspacing:0; cellpadding:0; margin-bottom:25}\r\nspan.tracecontent tr.subhead { background-color:#cccccc;}\r\nspan.tracecontent th { padding:0,3,0,3 }\r\nspan.tracecontent th.alt { background-color:black; color:white; padding:3,3,2,3; }\r\nspan.tracecontent td { color: black; padding:0,3,0,3; text-align: left }\r\nspan.tracecontent td.err { color: red; }\r\nspan.tracecontent tr.alt { background-color:#eeeeee }\r\nspan.tracecontent h1 { font: 24pt verdana, arial; margin:0,0,0,0}\r\nspan.tracecontent h2 { font: 18pt verdana, arial; margin:0,0,0,0}\r\nspan.tracecontent h3 { font: 12pt verdana, arial; margin:0,0,0,0}\r\nspan.tracecontent th a { color:darkblue; font: 8pt verdana, arial; }\r\nspan.tracecontent a { color:darkblue;text-decoration:none }\r\nspan.tracecontent a:hover { color:darkblue;text-decoration:underline; }\r\nspan.tracecontent div.outer { width:90%; margin:15,15,15,15}\r\nspan.tracecontent table.viewmenu td { background-color:#006699; color:white; padding:0,5,0,5; }\r\nspan.tracecontent table.viewmenu td.end { padding:0,0,0,0; }\r\nspan.tracecontent table.viewmenu a {color:white; font: 8pt verdana, arial; }\r\nspan.tracecontent table.viewmenu a:hover {color:white; font: 8pt verdana, arial; }\r\nspan.tracecontent a.tinylink {color:darkblue; background-color:black; font: 8pt verdana, arial;text-decoration:underline;}\r\nspan.tracecontent a.link {color:darkblue; text-decoration:underline;}\r\nspan.tracecontent div.buffer {padding-top:7; padding-bottom:17;}\r\nspan.tracecontent .small { font: 8pt verdana, arial }\r\nspan.tracecontent table td { padding-right:20 }\r\nspan.tracecontent table td.nopad { padding-right:5 }\r\n</style>\r\n";
			}
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00045A64 File Offset: 0x00043C64
		internal static Table CreateControlTable(DataTable datatable)
		{
			Table table = new Table();
			if (datatable == null)
			{
				return table;
			}
			Hashtable hashtable = new Hashtable();
			bool flag = false;
			table.Width = Unit.Percentage(100.0);
			table.CellPadding = 0;
			table.CellSpacing = 0;
			TableRow tableRow = TraceHandler.AddRow(table);
			TableCell tableCell = TraceHandler.AddHeaderCell(tableRow, "<h3><b>" + SR.GetString(datatable.TableName) + "</b></h3>");
			tableCell.CssClass = "alt";
			tableCell.ColumnSpan = 5;
			tableCell.HorizontalAlign = HorizontalAlign.Left;
			tableRow = TraceHandler.AddRow(table);
			tableRow.CssClass = "subhead";
			tableRow.HorizontalAlign = HorizontalAlign.Left;
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Control_Id"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Type"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Render_Size_children"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Viewstate_Size_Nochildren"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Controlstate_Size_Nochildren"));
			hashtable["ROOT"] = 0;
			foreach (object obj in datatable.Rows)
			{
				string key = HttpUtility.HtmlEncode((string)((DataRow)obj)["Trace_Parent_Id"]);
				IEnumerator enumerator;
				string text = HttpUtility.HtmlEncode((string)((DataRow)enumerator.Current)["Trace_Control_Id"]);
				int num = (int)hashtable[key];
				hashtable[text] = num + 1;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<nobr>");
				for (int i = 0; i < num; i++)
				{
					stringBuilder.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
				}
				if (text.Length == 0)
				{
					stringBuilder.Append(SR.GetString("Trace_Page"));
				}
				else
				{
					stringBuilder.Append(text);
				}
				tableRow = TraceHandler.AddRow(table);
				TraceHandler.AddCell(tableRow, stringBuilder.ToString());
				TraceHandler.AddCell(tableRow, (string)((DataRow)enumerator.Current)["Trace_Type"]);
				object obj2 = ((DataRow)enumerator.Current)["Trace_Render_Size"];
				if (obj2 != null)
				{
					TraceHandler.AddCell(tableRow, ((int)obj2).ToString(NumberFormatInfo.InvariantInfo));
				}
				else
				{
					TraceHandler.AddCell(tableRow, "---");
				}
				obj2 = ((DataRow)enumerator.Current)["Trace_Viewstate_Size"];
				if (obj2 != null)
				{
					TraceHandler.AddCell(tableRow, ((int)obj2).ToString(NumberFormatInfo.InvariantInfo));
				}
				else
				{
					TraceHandler.AddCell(tableRow, "---");
				}
				obj2 = ((DataRow)enumerator.Current)["Trace_Controlstate_Size"];
				if (obj2 != null)
				{
					TraceHandler.AddCell(tableRow, ((int)obj2).ToString(NumberFormatInfo.InvariantInfo));
				}
				else
				{
					TraceHandler.AddCell(tableRow, "---");
				}
				if (flag)
				{
					tableRow.CssClass = "alt";
				}
				flag = !flag;
			}
			return table;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00045D60 File Offset: 0x00043F60
		internal static Table CreateTraceTable(DataTable datatable)
		{
			Table table = new Table();
			table.Width = Unit.Percentage(100.0);
			table.CellPadding = 0;
			table.CellSpacing = 0;
			if (datatable == null)
			{
				return table;
			}
			bool flag = false;
			TableRow tableRow = TraceHandler.AddRow(table);
			TableCell tableCell = TraceHandler.AddHeaderCell(tableRow, "<h3><b>" + SR.GetString(datatable.TableName) + "</b></h3>");
			tableCell.CssClass = "alt";
			tableCell.ColumnSpan = 10;
			tableCell.HorizontalAlign = HorizontalAlign.Left;
			tableRow = TraceHandler.AddRow(table);
			tableRow.CssClass = "subhead";
			tableRow.HorizontalAlign = HorizontalAlign.Left;
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Category"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Message"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_From_First"));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_From_Last"));
			IEnumerator enumerator = datatable.DefaultView.GetEnumerator();
			while (enumerator.MoveNext())
			{
				tableRow = TraceHandler.AddRow(table);
				DataRow row = ((DataRowView)enumerator.Current).Row;
				bool flag2 = row["Trace_Warning"].Equals("yes");
				tableCell = TraceHandler.AddCell(tableRow, HttpUtility.FormatPlainTextAsHtml((string)row["Trace_Category"]));
				if (flag2)
				{
					tableCell.CssClass = "err";
				}
				StringBuilder stringBuilder = new StringBuilder(HttpUtility.FormatPlainTextAsHtml((string)row["Trace_Message"]));
				object obj = row["ErrorInfoMessage"];
				object obj2 = row["ErrorInfoStack"];
				if (!(obj is DBNull))
				{
					stringBuilder.Append("<br>" + HttpUtility.FormatPlainTextAsHtml((string)obj));
				}
				if (!(obj2 is DBNull))
				{
					stringBuilder.Append("<br>" + HttpUtility.FormatPlainTextAsHtml((string)obj2));
				}
				tableCell = TraceHandler.AddCell(tableRow, stringBuilder.ToString());
				if (flag2)
				{
					tableCell.CssClass = "err";
				}
				tableCell = TraceHandler.AddCell(tableRow, TraceHandler.FormatPotentialDouble(row["Trace_From_First"]));
				if (flag2)
				{
					tableCell.CssClass = "err";
				}
				tableCell = TraceHandler.AddCell(tableRow, TraceHandler.FormatPotentialDouble(row["Trace_From_Last"]));
				if (flag2)
				{
					tableCell.CssClass = "err";
				}
				if (flag)
				{
					tableRow.CssClass = "alt";
				}
				flag = !flag;
			}
			return table;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00045FC8 File Offset: 0x000441C8
		private static string FormatPotentialDouble(object o)
		{
			if (!(o is double))
			{
				return o.ToString();
			}
			return ((double)o).ToString("F6", CultureInfo.CurrentCulture);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00045FFC File Offset: 0x000441FC
		internal static Table CreateTable(DataTable datatable)
		{
			return TraceHandler.CreateTable(datatable, false);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00046008 File Offset: 0x00044208
		internal static Table CreateTable(DataTable datatable, bool encodeSpaces)
		{
			Table table = new Table();
			table.Width = Unit.Percentage(100.0);
			table.CellPadding = 0;
			table.CellSpacing = 0;
			if (datatable == null)
			{
				return table;
			}
			bool flag = false;
			TableRow tableRow = TraceHandler.AddRow(table);
			TableCell tableCell = TraceHandler.AddHeaderCell(tableRow, "<h3><b>" + SR.GetString(datatable.TableName) + "</b></h3>");
			tableCell.CssClass = "alt";
			tableCell.ColumnSpan = 10;
			tableCell.HorizontalAlign = HorizontalAlign.Left;
			tableRow = TraceHandler.AddRow(table);
			tableRow.CssClass = "subhead";
			tableRow.HorizontalAlign = HorizontalAlign.Left;
			foreach (object obj in datatable.Columns)
			{
				TraceHandler.AddHeaderCell(tableRow, SR.GetString(((DataColumn)obj).ColumnName));
			}
			foreach (object obj2 in datatable.Rows)
			{
				object[] itemArray = ((DataRow)obj2).ItemArray;
				tableRow = TraceHandler.AddRow(table);
				for (int i = 0; i < itemArray.Length; i++)
				{
					string text;
					if (encodeSpaces)
					{
						text = HttpUtility.FormatPlainTextSpacesAsHtml(HttpUtility.HtmlEncode(itemArray[i].ToString()));
					}
					else
					{
						text = HttpUtility.HtmlEncode(itemArray[i].ToString());
					}
					TraceHandler.AddCell(tableRow, (text.Length != 0) ? text : "&nbsp;");
				}
				if (flag)
				{
					tableRow.CssClass = "alt";
				}
				flag = !flag;
			}
			return table;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0004617C File Offset: 0x0004437C
		internal static Table CreateDetailsTable(DataTable datatable)
		{
			Table table = new Table();
			table.Width = Unit.Percentage(100.0);
			table.CellPadding = 0;
			table.CellSpacing = 0;
			if (datatable == null)
			{
				return table;
			}
			TableRow tableRow = TraceHandler.AddRow(table);
			TableCell tableCell = TraceHandler.AddHeaderCell(tableRow, "<h3><b>" + SR.GetString("Trace_Request_Details") + "</b></h3>");
			tableCell.ColumnSpan = 10;
			tableCell.CssClass = "alt";
			tableCell.HorizontalAlign = HorizontalAlign.Left;
			tableRow = TraceHandler.AddRow(table);
			tableRow.HorizontalAlign = HorizontalAlign.Left;
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Session_Id") + ":");
			TraceHandler.AddCell(tableRow, HttpUtility.HtmlEncode(datatable.Rows[0]["Trace_Session_Id"].ToString()));
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Request_Type") + ":");
			TraceHandler.AddCell(tableRow, datatable.Rows[0]["Trace_Request_Type"].ToString());
			tableRow = TraceHandler.AddRow(table);
			tableRow.HorizontalAlign = HorizontalAlign.Left;
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Time_of_Request") + ":");
			TraceHandler.AddCell(tableRow, datatable.Rows[0]["Trace_Time_of_Request"].ToString());
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Status_Code") + ":");
			TraceHandler.AddCell(tableRow, datatable.Rows[0]["Trace_Status_Code"].ToString());
			tableRow = TraceHandler.AddRow(table);
			tableRow.HorizontalAlign = HorizontalAlign.Left;
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Request_Encoding") + ":");
			TraceHandler.AddCell(tableRow, datatable.Rows[0]["Trace_Request_Encoding"].ToString());
			TraceHandler.AddHeaderCell(tableRow, SR.GetString("Trace_Response_Encoding") + ":");
			TraceHandler.AddCell(tableRow, datatable.Rows[0]["Trace_Response_Encoding"].ToString());
			return table;
		}

		// Token: 0x04001686 RID: 5766
		private const string _style = "<style type=\"text/css\">\r\nspan.tracecontent b { color:white }\r\nspan.tracecontent { background-color:white; color:black;font: 10pt verdana, arial; }\r\nspan.tracecontent table { clear:left; font: 10pt verdana, arial; cellspacing:0; cellpadding:0; margin-bottom:25}\r\nspan.tracecontent tr.subhead { background-color:#cccccc;}\r\nspan.tracecontent th { padding:0,3,0,3 }\r\nspan.tracecontent th.alt { background-color:black; color:white; padding:3,3,2,3; }\r\nspan.tracecontent td { color: black; padding:0,3,0,3; text-align: left }\r\nspan.tracecontent td.err { color: red; }\r\nspan.tracecontent tr.alt { background-color:#eeeeee }\r\nspan.tracecontent h1 { font: 24pt verdana, arial; margin:0,0,0,0}\r\nspan.tracecontent h2 { font: 18pt verdana, arial; margin:0,0,0,0}\r\nspan.tracecontent h3 { font: 12pt verdana, arial; margin:0,0,0,0}\r\nspan.tracecontent th a { color:darkblue; font: 8pt verdana, arial; }\r\nspan.tracecontent a { color:darkblue;text-decoration:none }\r\nspan.tracecontent a:hover { color:darkblue;text-decoration:underline; }\r\nspan.tracecontent div.outer { width:90%; margin:15,15,15,15}\r\nspan.tracecontent table.viewmenu td { background-color:#006699; color:white; padding:0,5,0,5; }\r\nspan.tracecontent table.viewmenu td.end { padding:0,0,0,0; }\r\nspan.tracecontent table.viewmenu a {color:white; font: 8pt verdana, arial; }\r\nspan.tracecontent table.viewmenu a:hover {color:white; font: 8pt verdana, arial; }\r\nspan.tracecontent a.tinylink {color:darkblue; background-color:black; font: 8pt verdana, arial;text-decoration:underline;}\r\nspan.tracecontent a.link {color:darkblue; text-decoration:underline;}\r\nspan.tracecontent div.buffer {padding-top:7; padding-bottom:17;}\r\nspan.tracecontent .small { font: 8pt verdana, arial }\r\nspan.tracecontent table td { padding-right:20 }\r\nspan.tracecontent table td.nopad { padding-right:5 }\r\n</style>\r\n";

		// Token: 0x04001687 RID: 5767
		private HttpContext _context;

		// Token: 0x04001688 RID: 5768
		private HttpResponse _response;

		// Token: 0x04001689 RID: 5769
		private HttpRequest _request;

		// Token: 0x0400168A RID: 5770
		private HtmlTextWriter _writer;
	}
}
