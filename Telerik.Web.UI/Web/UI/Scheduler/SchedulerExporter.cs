using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;
using Telerik.Web.Apoc;
using Telerik.Web.Apoc.Render.Pdf;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x02000EDA RID: 3802
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class SchedulerExporter
	{
		// Token: 0x0600904B RID: 36939 RVA: 0x00207A3E File Offset: 0x00205C3E
		public SchedulerExporter(RadScheduler scheduler) : this(scheduler, scheduler.ExportSettings)
		{
		}

		// Token: 0x0600904C RID: 36940 RVA: 0x00207A50 File Offset: 0x00205C50
		public SchedulerExporter(RadScheduler scheduler, SchedulerExportSettings setting)
		{
			this.SCHEDULER = scheduler;
			this.EXPORTSETTINGS = setting;
		}

		// Token: 0x0600904D RID: 36941 RVA: 0x00207AA8 File Offset: 0x00205CA8
		public void ExportToPdf()
		{
			Page page = this.GetPage();
			page.SetRenderMethodDelegate(new RenderMethod(this.PdfExportRenderPage));
		}

		// Token: 0x0600904E RID: 36942 RVA: 0x00207AD0 File Offset: 0x00205CD0
		private void PdfExportRenderForm(HtmlTextWriter nullWriter, System.Web.UI.Control form)
		{
			Page page = this.GetPage();
			this.RenderControl(page);
			HttpResponse response = page.Response;
			this.ConfigureResponse("application/pdf", ".pdf", response);
			StringWriter writer = new StringWriter();
			this._htmlWriter = new HtmlTextWriter(writer);
			this._htmlWriter.InnerWriter.ToString();
			string text = string.Empty;
			foreach (byte[] inArray in this._gifArray)
			{
				text += ControlRenderer.GetControlHtml(new System.Web.UI.WebControls.Image
				{
					ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(inArray)
				}).Replace("../../../", "~/");
			}
			string pageTitle = this.EXPORTSETTINGS.Pdf.PageTitle;
			XmlDocument xmlDocument = new XmlDocument();
			string text2 = text;
			string xhtmlEntitiesDtd = SchedulerExporter.GetXhtmlEntitiesDtd();
			text2 = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n{0}\r\n\t<html xmlns=\"http://www.w3.org/1999/xhtml\" \r\n\t  xmlns:fo=\"http://www.w3.org/1999/XSL/Format\">\r\n<head>\r\n\t<title>{2}</title>\r\n</head>\r\n\t<body>{1}</body>\r\n</html>", xhtmlEntitiesDtd, text2, pageTitle);
			try
			{
				xmlDocument.LoadXml(text2);
			}
			catch (XmlException ex)
			{
				string[] array = Regex.Split(text2, Environment.NewLine);
				string text3 = array[ex.LineNumber - 1];
				string message = string.Format("Invalid XHTML. RadScheduler has to render correct XHTML in order to export to PDF.\r\nParse error:\r\n{0}\r\nat line:\r\n{1}", ex.Message, text3.Trim());
				throw new SchedulerPdfExportException(message);
			}
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform(false);
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Telerik.Web.UI.Grid.Resources.xhtml2fo.xsl"))
			{
				XmlDocument xmlDocument2 = new XmlDocument();
				if (manifestResourceStream != null)
				{
					xmlDocument2.Load(manifestResourceStream);
				}
				xslCompiledTransform.Load(xmlDocument2);
			}
			StringWriter stringWriter = new StringWriter();
			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			this.AddXhtmlToXslFoTransformParameters(xsltArgumentList);
			xslCompiledTransform.Transform(xmlDocument, xsltArgumentList, stringWriter);
			string s = stringWriter.ToString().Replace("pxpx", "px");
			StringReader inputReader = new StringReader(s);
			ApocDriver apocDriver = ApocDriver.Make();
			apocDriver.BaseDirectory = new DirectoryInfo(SchedulerExporter.GetTemporaryDir());
			PdfRendererOptions options = new PdfRendererOptions();
			this.ConfigurePdfOptions(options);
			apocDriver.Options = options;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				apocDriver.Render(inputReader, memoryStream);
				byte[] bytes = memoryStream.ToArray();
				Encoding encoding = Encoding.GetEncoding(1252);
				string @string = encoding.GetString(bytes);
				SchedulerPdfExportingEventArgs e = new SchedulerPdfExportingEventArgs();
				this.SCHEDULER.CallOnPdfExporting(e);
				response.BinaryWrite(encoding.GetBytes(@string));
			}
		}

		// Token: 0x0600904F RID: 36943 RVA: 0x00207D6C File Offset: 0x00205F6C
		private string GetStyleSheets(Page page)
		{
			string text = this.GetStyleSheetFor("Scheduler", typeof(RadScheduler), page);
			if (this.SCHEDULER.ResolvedRenderMode == RenderMode.Mobile)
			{
				text += this.GetMobileStyleSheetFor("Scheduler", typeof(RadScheduler), page);
			}
			if (this.SCHEDULER.FormContainer.Mode != SchedulerFormMode.Hidden)
			{
				text += this.GetStyleSheetFor("ComboBox", typeof(RadComboBox), page);
				text += this.GetStyleSheetFor("Input", typeof(RadInputControl), page);
			}
			return text;
		}

		// Token: 0x06009050 RID: 36944 RVA: 0x00207E08 File Offset: 0x00206008
		private string GetStyleSheetFor(string controlName, Type controlType, Page page)
		{
			string pathFormatString = "Telerik.Web.UI.Skins.{1}.{0}.{1}.css";
			string defaultPathFormatString = "Telerik.Web.UI.Skins.{0}.css";
			return this.GetStyleSheetFor(controlName, controlType, page, pathFormatString, defaultPathFormatString);
		}

		// Token: 0x06009051 RID: 36945 RVA: 0x00207E2C File Offset: 0x0020602C
		private string GetMobileStyleSheetFor(string controlName, Type controlType, Page page)
		{
			string pathFormatString = "Telerik.Web.UI.Skins.{1}Mobile.{0}.{1}.css";
			string defaultPathFormatString = "Telerik.Web.UI.Skins.{0}Mobile.css";
			return this.GetStyleSheetFor(controlName, controlType, page, pathFormatString, defaultPathFormatString);
		}

		// Token: 0x06009052 RID: 36946 RVA: 0x00207E50 File Offset: 0x00206050
		private string GetStyleSheetFor(string controlName, Type controlType, Page page, string pathFormatString, string defaultPathFormatString)
		{
			string resourceName = string.Format(pathFormatString, controlName, this.SCHEDULER.RuntimeSkin);
			string resourceName2 = string.Format(defaultPathFormatString, controlName, this.SCHEDULER.RuntimeSkin);
			string text = SkinRegistrar.GetWebResourceUrl(page, controlType, resourceName2);
			text = SchedulerExporter.UpdateWebResourceUrl(text);
			string str = "";
			if (this.SCHEDULER.EnableEmbeddedSkins)
			{
				string text2 = SkinRegistrar.GetWebResourceUrl(page, controlType, resourceName);
				text2 = SchedulerExporter.UpdateWebResourceUrl(text2);
				str = this.GetStyleSheet(text2);
			}
			return this.GetStyleSheet(text) + str;
		}

		// Token: 0x06009053 RID: 36947 RVA: 0x00207ED8 File Offset: 0x002060D8
		private string GetStyleSheet(string url)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.UseDefaultCredentials = true;
			WebResponse response = httpWebRequest.GetResponse();
			Encoding utf = Encoding.UTF8;
			StreamReader streamReader = new StreamReader(response.GetResponseStream(), utf);
			string text = streamReader.ReadToEnd();
			streamReader.Close();
			response.Close();
			HttpContext httpContext = HttpContext.Current;
			string[] array = text.Split(new string[]
			{
				"url('",
				"')"
			}, StringSplitOptions.None);
			List<string> list = new List<string>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains("WebResource.axd?") && !list.Contains(array[i]))
				{
					list.Add(array[i]);
				}
			}
			List<string> list2 = new List<string>();
			foreach (string str in list)
			{
				HttpRequest request = httpContext.Request;
				string components = request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
				StringBuilder stringBuilder = new StringBuilder(components);
				stringBuilder.Append(httpContext.Request.ApplicationPath);
				stringBuilder.Append("/" + str);
				stringBuilder.Replace("~", "");
				list2.Add(stringBuilder.ToString());
			}
			StringBuilder stringBuilder2 = new StringBuilder(text);
			for (int j = 0; j < list2.Count; j++)
			{
				stringBuilder2.Replace(list[j], list2[j]);
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x06009054 RID: 36948 RVA: 0x00208088 File Offset: 0x00206288
		private string GetExternalStyleSheets(Page page)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this.EXPORTSETTINGS.Pdf.StyleSheets)
			{
				string text2 = text;
				if (text2.StartsWith("~"))
				{
					text2 = text2.Substring(1);
				}
				if (!text2.StartsWith("http"))
				{
					text2 = new Uri(page.Request.Url, text2).AbsoluteUri;
				}
				stringBuilder.Append("<link href=\"");
				stringBuilder.Append(text2);
				stringBuilder.Append("\" rel=\"stylesheet\" />");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06009055 RID: 36949 RVA: 0x00208124 File Offset: 0x00206324
		[STAThread]
		private void RenderControl(Page page)
		{
			this._thread = new Thread(new ThreadStart(this.GetPageDimensions));
			this._thread.SetApartmentState(ApartmentState.STA);
			this._thread.Start();
			this._thread.Join();
			if (this.EXPORTSETTINGS.Pdf.AllowPaging || this.SCHEDULER.Width == Unit.Empty || this.SCHEDULER.Width.Type == UnitType.Percentage)
			{
				this.SCHEDULER.Width = new Unit(this._availPageWidth);
			}
			string text = ControlRenderer.GetControlHtml(this.SCHEDULER);
			text = this.RemoveNonBreakingSpaces(text);
			text = SchedulerExporter.AddDummyContentToEmptyCells(text);
			text = this.FixDatePickersStyles(text);
			text = this.FixTableCellSpacing(text);
			this._html = string.Format(this.HTML_CONTENT_TEMPLATE, this.GetStyleSheets(page), this.GetExternalStyleSheets(page), text);
			this._thread = new Thread(new ThreadStart(this.SaveSchedulerHtml));
			this._thread.SetApartmentState(ApartmentState.STA);
			this._thread.Start();
			this._thread.Join();
		}

		// Token: 0x06009056 RID: 36950 RVA: 0x00208244 File Offset: 0x00206444
		private void SaveSchedulerHtml()
		{
			this.GetSchedulerDimensions();
			if (this.EXPORTSETTINGS.Pdf.AllowPaging)
			{
				int width = (int)this._availPageWidth;
				int num = (int)this._availPageHeight;
				double num2 = (double)num - this._schedulerHeadersHeight;
				int numberOfPages = this.GetNumberOfPages();
				for (int i = 0; i < numberOfPages; i++)
				{
					double num3 = (double)i * num2;
					if (i == numberOfPages - 1)
					{
						num2 = (this._schedulerFullHeight - this._schedulerHeadersHeight) % (this._availPageHeight - this._schedulerHeadersHeight);
						num = (int)(num2 + this._schedulerHeadersHeight);
					}
					string scrollContainerSelector = this.GetScrollContainerSelector();
					string str = string.Format(this.SCROLLING_STYLES_TEMPLATE, scrollContainerSelector, num3, num2);
					this.LoadHtmlInBrowser(this.HTML_HEAD + str + this._html);
					this.SaveRenderedHtmlToBitmap(width, num);
				}
				return;
			}
			this.LoadHtmlInBrowser(this.HTML_HEAD + this._html);
			this.SaveRenderedHtmlToBitmap((int)this._schedulerFullWidth, (int)this._schedulerFullHeight);
		}

		// Token: 0x06009057 RID: 36951 RVA: 0x00208348 File Offset: 0x00206548
		private void GetSchedulerDimensions()
		{
			this.LoadHtmlInBrowser(this.HTML_HEAD + this._html);
			this._schedulerFullHeight = (double)this._web.Document.Forms[0].Children[0].ClientRectangle.Height;
			this._schedulerFullWidth = (double)this._web.Document.Forms[0].Children[0].ClientRectangle.Width;
			this.LoadHtmlInBrowser(this.HTML_HEAD + this.HIDDEN_CONTENT_STYLES + this._html);
			this._schedulerHeadersHeight = (double)this._web.Document.Forms[0].Children[0].ClientRectangle.Height;
		}

		// Token: 0x06009058 RID: 36952 RVA: 0x00208428 File Offset: 0x00206628
		private void GetPageDimensions()
		{
			string arg = ".testDiv {width: 100mm;height: 4in;}";
			string arg2 = "<div class=\"testDiv\"></div>";
			string str = string.Format(this.HTML_CONTENT_TEMPLATE, arg, string.Empty, arg2);
			this.LoadHtmlInBrowser(this.HTML_HEAD + str);
			Rectangle clientRectangle = this._web.Document.Forms[0].Children[0].ClientRectangle;
			SchedulerPdfSettings pdf = this.EXPORTSETTINGS.Pdf;
			double num = 1.0;
			if (pdf.PageHeight.Type == UnitType.Inch)
			{
				num = (double)(clientRectangle.Height / 4);
			}
			else if (pdf.PageHeight.Type == UnitType.Mm)
			{
				num = (double)(clientRectangle.Width / 100);
			}
			this._availPageWidth = (pdf.PageWidth.Value - pdf.PageLeftMargin.Value - pdf.PageRightMargin.Value) * num;
			this._availPageHeight = (pdf.PageHeight.Value - pdf.PageTopMargin.Value - pdf.PageBottomMargin.Value) * num;
		}

		// Token: 0x06009059 RID: 36953 RVA: 0x0020855C File Offset: 0x0020675C
		private int GetNumberOfPages()
		{
			return (int)Math.Ceiling((this._schedulerFullHeight - this._schedulerHeadersHeight) / (this._availPageHeight - this._schedulerHeadersHeight));
		}

		// Token: 0x0600905A RID: 36954 RVA: 0x00208580 File Offset: 0x00206780
		private string GetScrollContainerSelector()
		{
			string text;
			if (this.SCHEDULER.SelectedView == SchedulerViewType.TimelineView)
			{
				text = ".rsAllDayTable";
			}
			else if (this.SCHEDULER.SelectedView == SchedulerViewType.AgendaView)
			{
				text = ".rsAgendaTable";
				if (this.SCHEDULER.AgendaView.GroupingDirectionResolved == GroupingDirection.Vertical)
				{
					if (string.IsNullOrEmpty(this.SCHEDULER.AgendaView.GroupByResolved) && !this.SCHEDULER.AgendaView.GroupByResolved.StartsWith("Date,"))
					{
						if (this.SCHEDULER.AgendaView.ShowResourceHeaders)
						{
							text = ".rsSubHeader";
						}
					}
					else if (this.SCHEDULER.AgendaView.ShowDateHeaders)
					{
						text = ".rsSubHeader";
					}
				}
				text += ":first-child";
			}
			else
			{
				text = ".rsContentTable:first-child";
			}
			return text;
		}

		// Token: 0x0600905B RID: 36955 RVA: 0x0020864C File Offset: 0x0020684C
		private void SaveRenderedHtmlToBitmap(int width, int height)
		{
			this._web.Width = width;
			this._web.Height = height;
			Bitmap bitmap = new Bitmap(width, height);
			this._web.DrawToBitmap(bitmap, new Rectangle(0, 0, width, height));
			this.ConvertBitmapToJpeg(bitmap);
		}

		// Token: 0x0600905C RID: 36956 RVA: 0x00208698 File Offset: 0x00206898
		private void LoadHtmlInBrowser(string html)
		{
			this._web = new WebBrowser
			{
				ScrollBarsEnabled = false,
				ScriptErrorsSuppressed = true
			};
			this._web.Navigate("about:blank");
			if (this._web.Document != null)
			{
				this._web.Document.Write(html);
			}
			while (this._web.ReadyState != WebBrowserReadyState.Interactive)
			{
				Application.DoEvents();
			}
		}

		// Token: 0x0600905D RID: 36957 RVA: 0x0020870C File Offset: 0x0020690C
		private Page GetPage()
		{
			Page page = this.SCHEDULER.Page;
			if (page == null)
			{
				throw new InvalidOperationException("RadScheduler must be databound before exporting.");
			}
			return page;
		}

		// Token: 0x0600905E RID: 36958 RVA: 0x00208734 File Offset: 0x00206934
		private void PdfExportRenderPage(HtmlTextWriter nullWriter, System.Web.UI.Control page)
		{
			HtmlForm form = SchedulerExporter.GetForm(this.SCHEDULER);
			form.SetRenderMethodDelegate(new RenderMethod(this.PdfExportRenderForm));
			HtmlTextWriter writer = new HtmlTextWriter(TextWriter.Null);
			form.RenderControl(writer);
		}

		// Token: 0x0600905F RID: 36959 RVA: 0x00208774 File Offset: 0x00206974
		internal static HtmlForm GetForm(System.Web.UI.Control control)
		{
			HtmlForm htmlForm = SchedulerExporter.SafeGetForm(control);
			if (htmlForm == null)
			{
				throw new Exception("Telerik RadScheduler must be placed inside a <form> tag with runat='server'.");
			}
			return htmlForm;
		}

		// Token: 0x06009060 RID: 36960 RVA: 0x00208798 File Offset: 0x00206998
		private static HtmlForm SafeGetForm(System.Web.UI.Control control)
		{
			RadScheduler radScheduler = control as RadScheduler;
			if (radScheduler == null)
			{
				return control.Page.Form;
			}
			return radScheduler.Page.Form;
		}

		// Token: 0x06009061 RID: 36961 RVA: 0x002087C8 File Offset: 0x002069C8
		private void ConfigurePdfOptions(PdfRendererOptions options)
		{
			options.FontType = this.EXPORTSETTINGS.Pdf.FontType;
			options.EnableAdd = this.EXPORTSETTINGS.Pdf.AllowAdd;
			options.EnableCopy = this.EXPORTSETTINGS.Pdf.AllowCopy;
			options.EnableModify = this.EXPORTSETTINGS.Pdf.AllowModify;
			options.EnablePrinting = this.EXPORTSETTINGS.Pdf.AllowPrinting;
			if (!string.IsNullOrEmpty(this.EXPORTSETTINGS.Pdf.UserPassword))
			{
				options.UserPassword = this.EXPORTSETTINGS.Pdf.UserPassword;
			}
			if (!string.IsNullOrEmpty(this.EXPORTSETTINGS.Pdf.Author))
			{
				options.Author = this.EXPORTSETTINGS.Pdf.Author;
			}
			if (!string.IsNullOrEmpty(this.EXPORTSETTINGS.Pdf.Title))
			{
				options.Title = this.EXPORTSETTINGS.Pdf.Title;
			}
			if (!string.IsNullOrEmpty(this.EXPORTSETTINGS.Pdf.Subject))
			{
				options.Subject = this.EXPORTSETTINGS.Pdf.Subject;
			}
			if (!string.IsNullOrEmpty(this.EXPORTSETTINGS.Pdf.Creator))
			{
				options.Creator = this.EXPORTSETTINGS.Pdf.Creator;
			}
			if (!string.IsNullOrEmpty(this.EXPORTSETTINGS.Pdf.Producer))
			{
				options.Producer = this.EXPORTSETTINGS.Pdf.Producer;
			}
			if (!string.IsNullOrEmpty(this.EXPORTSETTINGS.Pdf.DefaultFontFamily))
			{
				options.DefaultFontFamily = this.EXPORTSETTINGS.Pdf.DefaultFontFamily;
			}
			foreach (string keyword in this.EXPORTSETTINGS.Pdf.Keywords)
			{
				options.AddKeyword(keyword);
			}
		}

		// Token: 0x06009062 RID: 36962 RVA: 0x002089A8 File Offset: 0x00206BA8
		private void AddXhtmlToXslFoTransformParameters(XsltArgumentList xslArg)
		{
			if (this.EXPORTSETTINGS.Pdf.PageWidth != Unit.Empty)
			{
				xslArg.AddParam("page-width", "", this.EXPORTSETTINGS.Pdf.PageWidth.ToString());
			}
			if (this.EXPORTSETTINGS.Pdf.PageHeight != Unit.Empty)
			{
				xslArg.AddParam("page-height", "", this.EXPORTSETTINGS.Pdf.PageHeight.ToString());
			}
			if (this.EXPORTSETTINGS.Pdf.PageTopMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-top", "", this.EXPORTSETTINGS.Pdf.PageTopMargin.ToString());
			}
			if (this.EXPORTSETTINGS.Pdf.PageBottomMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-bottom", "", this.EXPORTSETTINGS.Pdf.PageBottomMargin.ToString());
			}
			if (this.EXPORTSETTINGS.Pdf.PageLeftMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-left", "", this.EXPORTSETTINGS.Pdf.PageLeftMargin.ToString());
			}
			if (this.EXPORTSETTINGS.Pdf.PageRightMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-right", "", this.EXPORTSETTINGS.Pdf.PageRightMargin.ToString());
			}
			if (this.EXPORTSETTINGS.Pdf.PageHeaderMargin != Unit.Empty)
			{
				xslArg.AddParam("page-header-margin", "", this.EXPORTSETTINGS.Pdf.PageHeaderMargin.ToString());
			}
			if (this.EXPORTSETTINGS.Pdf.PageFooterMargin != Unit.Empty)
			{
				xslArg.AddParam("page-footer-margin", "", this.EXPORTSETTINGS.Pdf.PageFooterMargin.ToString());
			}
		}

		// Token: 0x06009063 RID: 36963 RVA: 0x00208C0C File Offset: 0x00206E0C
		private static string UpdateWebResourceUrl(string path)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			string components = request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
			StringBuilder stringBuilder = new StringBuilder(components);
			stringBuilder.Append(path);
			stringBuilder.Replace("~", "");
			path = stringBuilder.ToString();
			return path;
		}

		// Token: 0x06009064 RID: 36964 RVA: 0x00208C5E File Offset: 0x00206E5E
		internal string RemoveNonBreakingSpaces(string input)
		{
			return input.Replace("&nbsp;", string.Empty);
		}

		// Token: 0x06009065 RID: 36965 RVA: 0x00208C70 File Offset: 0x00206E70
		private static string AddDummyContentToEmptyCells(string input)
		{
			return Regex.Replace(input, "<td([^>]*)></td>", "<td$1><span><!-- --></span>");
		}

		// Token: 0x06009066 RID: 36966 RVA: 0x00208C8F File Offset: 0x00206E8F
		private string FixDatePickersStyles(string input)
		{
			return input.Replace("riTextBox riEmpty riError", "riTextBox riEnabled");
		}

		// Token: 0x06009067 RID: 36967 RVA: 0x00208CA1 File Offset: 0x00206EA1
		private string FixTableCellSpacing(string input)
		{
			return input.Replace("class=\"rsContentTable\"", "class=\"rsContentTable\" cellspacing=\"0\"").Replace("class=\"rsAllDayTable\"", "class=\"rsAllDayTable\" cellspacing=\"0\"");
		}

		// Token: 0x06009068 RID: 36968 RVA: 0x00208CC4 File Offset: 0x00206EC4
		private void ConvertBitmapToJpeg(Bitmap bmp)
		{
			MemoryStream memoryStream = new MemoryStream();
			bmp.Save(memoryStream, ImageFormat.Jpeg);
			this._gifArray.Add(memoryStream.ToArray());
		}

		// Token: 0x06009069 RID: 36969 RVA: 0x00208CF4 File Offset: 0x00206EF4
		private static string GetXhtmlEntitiesDtd()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string result;
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Telerik.Web.UI.Grid.Resources.XhtmlEntities.dtd"))
			{
				using (TextReader textReader = new StreamReader(manifestResourceStream))
				{
					result = textReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x0600906A RID: 36970 RVA: 0x00208D54 File Offset: 0x00206F54
		public static string GetTemporaryDir()
		{
			string text = Path.GetTempPath();
			if (string.IsNullOrEmpty(text))
			{
				foreach (string text2 in SchedulerExporter.TEMP_DIR_ENV_VARS)
				{
					text = Environment.GetEnvironmentVariable(text2);
					if (!string.IsNullOrEmpty(text2))
					{
						break;
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = "/tmp";
				}
			}
			return text;
		}

		// Token: 0x0600906B RID: 36971 RVA: 0x00208DA5 File Offset: 0x00206FA5
		internal static string EscapeAmpersands(string input)
		{
			return Regex.Replace(input, "&(?![#0-9a-zA-Z]+;)", "&amp;");
		}

		// Token: 0x0600906C RID: 36972 RVA: 0x00208DB8 File Offset: 0x00206FB8
		private void ConfigureResponse(string contentType, string fileExtension, HttpResponse response)
		{
			response.Clear();
			response.Buffer = true;
			response.ContentType = contentType;
			response.ContentEncoding = Encoding.UTF8;
			response.Charset = string.Empty;
			fileExtension = fileExtension.Replace("\n", " ").Replace("\r", " ");
			if (!this.EXPORTSETTINGS.OpenInNewWindow)
			{
				response.AddHeader("Content-Disposition", "inline;filename=\"" + this.EXPORTSETTINGS.FileName + fileExtension + "\"");
				return;
			}
			response.AddHeader("Content-Disposition", "attachment;filename=\"" + this.EXPORTSETTINGS.FileName + fileExtension + "\"");
		}

		// Token: 0x040028CF RID: 10447
		private readonly RadScheduler SCHEDULER;

		// Token: 0x040028D0 RID: 10448
		private readonly SchedulerExportSettings EXPORTSETTINGS;

		// Token: 0x040028D1 RID: 10449
		private static readonly string[] TEMP_DIR_ENV_VARS = new string[]
		{
			"Temp",
			"TMP",
			"TEMP"
		};

		// Token: 0x040028D2 RID: 10450
		private readonly string HTML_HEAD = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n    <title></title>";

		// Token: 0x040028D3 RID: 10451
		private readonly string HTML_CONTENT_TEMPLATE = "<style type=\"text/css\">\r\n        {0}\r\n    </style>\r\n    <style type=\"text/css\">.RadScheduler .rsContentTable, .RadScheduler .rsAllDayTable {{ border-collapse: separate !important }}</style>\r\n    {1}\r\n</head>\r\n<body style=\"margin:0px;\">\r\n\t<form name=\"form1\" method=\"post\" action=\"Default.aspx\" id=\"form1\">\r\n        {2}\r\n    </form>\r\n</body>\r\n</html>";

		// Token: 0x040028D4 RID: 10452
		private readonly string SCROLLING_STYLES_TEMPLATE = "<style type=\"text/css\">\r\n{0},\r\n.rsVerticalHeaderTable {{\r\n    margin-top:-{1}px !important;\r\n}}\r\n.rsContentScrollArea {{\r\n    overflow: hidden !important;\r\n}}\r\n.rsVerticalHeaderWrapper>div,\r\n.rsContentScrollArea {{\r\n    height: {2}px !important;\r\n}}\r\n</style>";

		// Token: 0x040028D5 RID: 10453
		private readonly string HIDDEN_CONTENT_STYLES = "\r\n<style type=\"text/css\">\r\n.rsVerticalHeaderWrapper,\r\n.rsContentWrapper {\r\n    display: none !important\r\n}\r\n</style>";

		// Token: 0x040028D6 RID: 10454
		private WebBrowser _web;

		// Token: 0x040028D7 RID: 10455
		private string _html;

		// Token: 0x040028D8 RID: 10456
		private List<byte[]> _gifArray = new List<byte[]>();

		// Token: 0x040028D9 RID: 10457
		private Thread _thread;

		// Token: 0x040028DA RID: 10458
		private double _availPageWidth;

		// Token: 0x040028DB RID: 10459
		private double _availPageHeight;

		// Token: 0x040028DC RID: 10460
		private double _schedulerFullHeight;

		// Token: 0x040028DD RID: 10461
		private double _schedulerFullWidth;

		// Token: 0x040028DE RID: 10462
		private double _schedulerHeadersHeight;

		// Token: 0x040028DF RID: 10463
		private HtmlTextWriter _htmlWriter;
	}
}
