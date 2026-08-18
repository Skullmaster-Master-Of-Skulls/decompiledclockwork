using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200005D RID: 93
	public class HtmlToPdf
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x00021119 File Offset: 0x0001F319
		public HtmlToPdf(Rectangle size)
		{
			this.PageSize = size;
			this._Pages = new List<HtmlPdfPage>();
			this._Styles = new StyleSheet();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060004AE RID: 1198 RVA: 0x00021144 File Offset: 0x0001F344
		// (remove) Token: 0x060004AF RID: 1199 RVA: 0x0002117C File Offset: 0x0001F37C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event RenderEvent BeforeRender;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060004B0 RID: 1200 RVA: 0x000211B4 File Offset: 0x0001F3B4
		// (remove) Token: 0x060004B1 RID: 1201 RVA: 0x000211EC File Offset: 0x0001F3EC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event RenderEvent AfterRender;

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00021221 File Offset: 0x0001F421
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00021229 File Offset: 0x0001F429
		public Rectangle PageSize { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00021232 File Offset: 0x0001F432
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0002123A File Offset: 0x0001F43A
		public float marginRight { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00021243 File Offset: 0x0001F443
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0002124B File Offset: 0x0001F44B
		public float marginTop { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00021254 File Offset: 0x0001F454
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0002125C File Offset: 0x0001F45C
		public float marginLeft { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00021265 File Offset: 0x0001F465
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0002126D File Offset: 0x0001F46D
		public float marginBottom { get; set; }

		// Token: 0x17000189 RID: 393
		public HtmlPdfPage this[int index]
		{
			get
			{
				return this._Pages[index];
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00021298 File Offset: 0x0001F498
		public HtmlPdfPage[] Pages
		{
			get
			{
				return this._Pages.ToArray();
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000212B8 File Offset: 0x0001F4B8
		public HtmlPdfPage AddPage()
		{
			HtmlPdfPage htmlPdfPage = new HtmlPdfPage();
			this._Pages.Add(htmlPdfPage);
			return htmlPdfPage;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000212DE File Offset: 0x0001F4DE
		public void RemovePage(HtmlPdfPage page)
		{
			this._Pages.Remove(page);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000212EE File Offset: 0x0001F4EE
		public void AddStyle(string selector, string styles)
		{
			this._Styles.LoadTagStyle(selector, "style", styles);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00021304 File Offset: 0x0001F504
		public void ImportStylesheet(string path)
		{
			string input = File.ReadAllText(path);
			foreach (object obj in Regex.Matches(input, "(?<selector>[^\\{\\s]+\\w+(\\s\\[^\\{\\s]+)?)\\s?\\{(?<style>[^\\}]*)\\}"))
			{
				Match match = (Match)obj;
				string value = match.Groups["selector"].Value;
				string value2 = match.Groups["style"].Value;
				this.AddStyle(value, value2);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000213A4 File Offset: 0x0001F5A4
		public void InsertBefore(HtmlPdfPage page, HtmlPdfPage before)
		{
			this._Pages.Remove(page);
			this._Pages.Insert(Math.Max(this._Pages.IndexOf(before), 0), page);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000213D3 File Offset: 0x0001F5D3
		public void InsertAfter(HtmlPdfPage page, HtmlPdfPage after)
		{
			this._Pages.Remove(page);
			this._Pages.Insert(Math.Min(this._Pages.IndexOf(after) + 1, this._Pages.Count), page);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00021410 File Offset: 0x0001F610
		public byte[] RenderPdf()
		{
			MemoryStream memoryStream = new MemoryStream();
			Document document = new Document(this.PageSize);
			document.SetMargins(this.marginLeft, this.marginRight, this.marginTop, this.marginBottom);
			PdfWriter instance = PdfWriter.GetInstance(document, memoryStream);
			bool flag = this.BeforeRender != null;
			if (flag)
			{
				this.BeforeRender(instance, document);
			}
			document.Add(new Header("stylesheet", string.Empty));
			document.Open();
			foreach (HtmlPdfPage htmlPdfPage in this._Pages)
			{
				document.NewPage();
				MemoryStream memoryStream2 = new MemoryStream();
				StreamWriter streamWriter = new StreamWriter(memoryStream2, Encoding.UTF8);
				streamWriter.Write("<html><head></head><body>" + htmlPdfPage._Html.ToString() + "</body></html>");
				streamWriter.Close();
				streamWriter.Dispose();
				MemoryStream memoryStream3 = new MemoryStream(memoryStream2.ToArray());
				StreamReader streamReader = new StreamReader(memoryStream3);
				StyleSheet style = new StyleSheet();
				try
				{
					FontFactory.Register("c:\\windows\\fonts\\GOUDOS.ttf");
				}
				catch
				{
				}
				List<IElement> list;
				try
				{
					try
					{
						list = HTMLWorker.ParseToList(streamReader, style);
					}
					catch (Exception ex)
					{
						list = new List<IElement>();
						throw ex;
					}
				}
				catch
				{
					list = new List<IElement>();
				}
				foreach (object obj in list)
				{
					document.Add((IElement)obj);
				}
				streamWriter.Dispose();
				streamReader.Dispose();
				memoryStream2.Dispose();
				memoryStream3.Dispose();
			}
			bool flag2 = this.AfterRender != null;
			if (flag2)
			{
				this.AfterRender(instance, document);
			}
			document.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00021688 File Offset: 0x0001F888
		public static byte[] RenderPdf(string html0)
		{
			string text = html0;
			int num = text.IndexOf("<img src=\"data:image/gif;base64");
			if (num > 0)
			{
				int num2 = text.IndexOf("\"", num + 15);
				int num3 = text.IndexOf("base64,", num + 1);
				num3 += 7;
				string s = text.Substring(num3, num2 - num3);
				string tempFilename = TemplatesClass.GetTempFilename(".gif");
				byte[] bytes = Convert.FromBase64String(s);
				File.WriteAllBytes(tempFilename, bytes);
				num2 = text.IndexOf("</a>", num + 1);
				num2 += 4;
				string oldValue = text.Substring(num, num2 - num);
				text = text.Replace(oldValue, string.Format("<img src='{0}'></img>", tempFilename));
				num = text.IndexOf("<img src=\"data:image/gif;base64");
			}
			HtmlToPdf htmlToPdf = new HtmlToPdf(new Rectangle(iTextSharp.text.PageSize.LETTER));
			htmlToPdf.marginBottom = 72f;
			htmlToPdf.marginTop = 72f;
			htmlToPdf.marginLeft = 72f;
			htmlToPdf.marginRight = 72f;
			List<string> list = HtmlToPdf.SplitByString(text, "*pagebreak*", true);
			foreach (string content in list)
			{
				HtmlPdfPage htmlPdfPage = htmlToPdf.AddPage();
				htmlPdfPage.AppendHtml(content, Array.Empty<object>());
			}
			return htmlToPdf.RenderPdf();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00021808 File Offset: 0x0001FA08
		private static List<string> SplitByString(string s, string delimiter, bool discardBlankItems)
		{
			int length = delimiter.Length;
			int num = 0;
			List<string> list = new List<string>();
			for (int i = s.IndexOf(delimiter, num); i >= 0; i = s.IndexOf(delimiter, num))
			{
				int num2 = i - num;
				string text = (num2 > 0) ? s.Substring(num, i) : "";
				bool flag = !discardBlankItems || !string.IsNullOrEmpty(text.Trim());
				if (flag)
				{
					list.Add(text);
				}
				num = i + length;
			}
			list.Add(s.Substring(num));
			return list;
		}

		// Token: 0x04000277 RID: 631
		private const string STYLE_DEFAULT_TYPE = "style";

		// Token: 0x04000278 RID: 632
		private const string DOCUMENT_HTML_START = "<html><head></head><body>";

		// Token: 0x04000279 RID: 633
		private const string DOCUMENT_HTML_END = "</body></html>";

		// Token: 0x0400027A RID: 634
		private const string REGEX_GROUP_SELECTOR = "selector";

		// Token: 0x0400027B RID: 635
		private const string REGEX_GROUP_STYLE = "style";

		// Token: 0x0400027C RID: 636
		private const string REGEX_GET_STYLES = "(?<selector>[^\\{\\s]+\\w+(\\s\\[^\\{\\s]+)?)\\s?\\{(?<style>[^\\}]*)\\}";

		// Token: 0x04000284 RID: 644
		private List<HtmlPdfPage> _Pages;

		// Token: 0x04000285 RID: 645
		private StyleSheet _Styles;
	}
}
