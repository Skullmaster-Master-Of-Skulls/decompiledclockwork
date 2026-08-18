using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Web.Hosting;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000057 RID: 87
	internal abstract class ErrorFormatter
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x000080BC File Offset: 0x000062BC
		internal static bool RequiresAdaptiveErrorReporting(HttpContext context)
		{
			if (HttpRuntime.HostingInitFailed)
			{
				return false;
			}
			HttpRequest httpRequest = (context != null) ? context.Request : null;
			if (context != null && context.WorkerRequest is StateHttpWorkerRequest)
			{
				return false;
			}
			HttpBrowserCapabilities httpBrowserCapabilities = null;
			try
			{
				httpBrowserCapabilities = ((httpRequest != null) ? httpRequest.Browser : null);
			}
			catch
			{
				return false;
			}
			return httpBrowserCapabilities != null && httpBrowserCapabilities["requiresAdaptiveErrorReporting"] == "true";
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00008138 File Offset: 0x00006338
		private Literal CreateBreakLiteral()
		{
			return new Literal
			{
				Text = "<br/>"
			};
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00008158 File Offset: 0x00006358
		private Label CreateLabelFromText(string text)
		{
			return new Label
			{
				Text = text
			};
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00008174 File Offset: 0x00006374
		internal virtual string GetAdaptiveErrorMessage(HttpContext context, bool dontShowSensitiveInfo)
		{
			this.GetHtmlErrorMessage(dontShowSensitiveInfo);
			context.Response.UseAdaptiveError = true;
			string result;
			try
			{
				Page page = new ErrorFormatterPage();
				page.EnableViewState = false;
				HtmlForm htmlForm = new HtmlForm();
				page.Controls.Add(htmlForm);
				IParserAccessor parserAccessor = htmlForm;
				Label label = this.CreateLabelFromText(SR.GetString("Error_Formatter_ASPNET_Error", new object[]
				{
					HttpRuntime.AppDomainAppVirtualPath
				}));
				label.ForeColor = Color.Red;
				label.Font.Bold = true;
				label.Font.Size = FontUnit.Large;
				parserAccessor.AddParsedSubObject(label);
				parserAccessor.AddParsedSubObject(this.CreateBreakLiteral());
				label = this.CreateLabelFromText(this.ErrorTitle);
				label.ForeColor = Color.Maroon;
				label.Font.Bold = true;
				label.Font.Italic = true;
				parserAccessor.AddParsedSubObject(label);
				parserAccessor.AddParsedSubObject(this.CreateBreakLiteral());
				parserAccessor.AddParsedSubObject(this.CreateLabelFromText(SR.GetString("Error_Formatter_Description") + " " + this.Description));
				parserAccessor.AddParsedSubObject(this.CreateBreakLiteral());
				string miscSectionTitle = this.MiscSectionTitle;
				if (!string.IsNullOrEmpty(miscSectionTitle))
				{
					parserAccessor.AddParsedSubObject(this.CreateLabelFromText(miscSectionTitle));
					parserAccessor.AddParsedSubObject(this.CreateBreakLiteral());
				}
				StringCollection adaptiveMiscContent = this.AdaptiveMiscContent;
				if (adaptiveMiscContent != null && adaptiveMiscContent.Count > 0)
				{
					foreach (string text in adaptiveMiscContent)
					{
						parserAccessor.AddParsedSubObject(this.CreateLabelFromText(text));
						parserAccessor.AddParsedSubObject(this.CreateBreakLiteral());
					}
				}
				string displayPath = this.GetDisplayPath();
				if (!string.IsNullOrEmpty(displayPath))
				{
					string text2 = SR.GetString("Error_Formatter_Source_File") + " " + displayPath;
					parserAccessor.AddParsedSubObject(this.CreateLabelFromText(text2));
					parserAccessor.AddParsedSubObject(this.CreateBreakLiteral());
					text2 = SR.GetString("Error_Formatter_Line") + " " + this.SourceFileLineNumber.ToString();
					parserAccessor.AddParsedSubObject(this.CreateLabelFromText(text2));
					parserAccessor.AddParsedSubObject(this.CreateBreakLiteral());
				}
				StringCollection adaptiveStackTrace = this.AdaptiveStackTrace;
				if (adaptiveStackTrace != null && adaptiveStackTrace.Count > 0)
				{
					foreach (string text3 in adaptiveStackTrace)
					{
						parserAccessor.AddParsedSubObject(this.CreateLabelFromText(text3));
						parserAccessor.AddParsedSubObject(this.CreateBreakLiteral());
					}
				}
				StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
				TextWriter writer = context.Response.SwitchWriter(stringWriter);
				page.ProcessRequest(context);
				context.Response.SwitchWriter(writer);
				result = stringWriter.ToString();
			}
			catch
			{
				result = this.GetStaticErrorMessage(context);
			}
			return result;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00008490 File Offset: 0x00006690
		private string GetPreferredRenderingType(HttpContext context)
		{
			HttpRequest httpRequest = (context != null) ? context.Request : null;
			HttpBrowserCapabilities httpBrowserCapabilities = null;
			try
			{
				httpBrowserCapabilities = ((httpRequest != null) ? httpRequest.Browser : null);
			}
			catch
			{
				return string.Empty;
			}
			if (httpBrowserCapabilities == null)
			{
				return string.Empty;
			}
			return httpBrowserCapabilities["preferredRenderingType"];
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000084EC File Offset: 0x000066EC
		private string GetStaticErrorMessage(HttpContext context)
		{
			string preferredRenderingType = this.GetPreferredRenderingType(context);
			string result;
			if (StringUtil.StringStartsWithIgnoreCase(preferredRenderingType, "xhtml"))
			{
				result = this.FormatStaticErrorMessage("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<!DOCTYPE html PUBLIC \"-//WAPFORUM//DTD XHTML Mobile 1.0//EN\" \"http://www.wapforum.org/DTD/xhtml-mobile10.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<title></title>\r\n</head>\r\n<body>\r\n<form>\r\n<div>\r\n<span style=\"color:Red;font-size:Large;font-weight:bold;\">{0}</span><br/>\r\n<span style=\"color:Maroon;font-weight:bold;font-style:italic;\">{1}</span><br/>\r\n", "</div>\r\n</form>\r\n</body>\r\n</html>");
			}
			else if (StringUtil.StringStartsWithIgnoreCase(preferredRenderingType, "wml"))
			{
				result = this.FormatStaticErrorMessage("<?xml version='1.0'?>\r\n<!DOCTYPE wml PUBLIC '-//WAPFORUM//DTD WML 1.1//EN' 'http://www.wapforum.org/DTD/wml_1.1.xml'><wml><head>\r\n<meta http-equiv=\"Cache-Control\" content=\"max-age=0\" forua=\"true\"/>\r\n</head>\r\n<card>\r\n<p>\r\n<b><big>{0}</big></b><br/>\r\n<b><i>{1}</i></b><br/>\r\n", "</p>\r\n</card>\r\n</wml>\r\n");
				if (string.Compare(context.Response.ContentType, 0, "text/vnd.wap.wml", 0, "text/vnd.wap.wml".Length, StringComparison.OrdinalIgnoreCase) != 0)
				{
					context.Response.ContentType = "text/vnd.wap.wml";
				}
			}
			else
			{
				result = this.FormatStaticErrorMessage("<html>\r\n<body>\r\n<form>\r\n<font color=\"Red\" size=\"5\">{0}</font><br/>\r\n<font color=\"Maroon\">{1}</font><br/>\r\n", "</form>\r\n</body>\r\n</html>");
			}
			return result;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00008588 File Offset: 0x00006788
		private string FormatStaticErrorMessage(string errorBeginTemplate, string errorEndTemplate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string @string = SR.GetString("Error_Formatter_ASPNET_Error", new object[]
			{
				HttpRuntime.AppDomainAppVirtualPath
			});
			stringBuilder.Append(string.Format(CultureInfo.CurrentCulture, errorBeginTemplate, new object[]
			{
				@string,
				this.ErrorTitle
			}));
			stringBuilder.Append(SR.GetString("Error_Formatter_Description") + " " + this.Description);
			stringBuilder.Append("<br/>\r\n");
			string miscSectionTitle = this.MiscSectionTitle;
			if (miscSectionTitle != null && miscSectionTitle.Length > 0)
			{
				stringBuilder.Append(miscSectionTitle);
				stringBuilder.Append("<br/>\r\n");
			}
			StringCollection adaptiveMiscContent = this.AdaptiveMiscContent;
			if (adaptiveMiscContent != null && adaptiveMiscContent.Count > 0)
			{
				foreach (string value in adaptiveMiscContent)
				{
					stringBuilder.Append(value);
					stringBuilder.Append("<br/>\r\n");
				}
			}
			string displayPath = this.GetDisplayPath();
			if (!string.IsNullOrEmpty(displayPath))
			{
				string value2 = SR.GetString("Error_Formatter_Source_File") + " " + displayPath;
				stringBuilder.Append(value2);
				stringBuilder.Append("<br/>\r\n");
				value2 = SR.GetString("Error_Formatter_Line") + " " + this.SourceFileLineNumber.ToString();
				stringBuilder.Append(value2);
				stringBuilder.Append("<br/>\r\n");
			}
			StringCollection adaptiveStackTrace = this.AdaptiveStackTrace;
			if (adaptiveStackTrace != null && adaptiveStackTrace.Count > 0)
			{
				foreach (string value3 in adaptiveStackTrace)
				{
					stringBuilder.Append(value3);
					stringBuilder.Append("<br/>\r\n");
				}
			}
			stringBuilder.Append(errorEndTemplate);
			return stringBuilder.ToString();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000878C File Offset: 0x0000698C
		internal string GetErrorMessage()
		{
			return this.GetErrorMessage(HttpContext.Current, true);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000879A File Offset: 0x0000699A
		internal virtual string GetErrorMessage(HttpContext context, bool dontShowSensitiveInfo)
		{
			if (ErrorFormatter.RequiresAdaptiveErrorReporting(context))
			{
				return this.GetAdaptiveErrorMessage(context, dontShowSensitiveInfo);
			}
			return this.GetHtmlErrorMessage(dontShowSensitiveInfo);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000087B4 File Offset: 0x000069B4
		internal string GetHtmlErrorMessage()
		{
			return this.GetHtmlErrorMessage(true);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000087C0 File Offset: 0x000069C0
		internal string GetHtmlErrorMessage(bool dontShowSensitiveInfo)
		{
			this.PrepareFormatter();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<!DOCTYPE html>\r\n");
			stringBuilder.Append("<html");
			if (ErrorFormatter.IsTextRightToLeft)
			{
				stringBuilder.Append(" dir=\"rtl\"");
			}
			stringBuilder.Append(">\r\n");
			stringBuilder.Append("    <head>\r\n");
			stringBuilder.Append("        <title>" + this.ErrorTitle + "</title>\r\n");
			stringBuilder.Append("        <meta name=\"viewport\" content=\"width=device-width\" />\r\n");
			stringBuilder.Append("        <style>\r\n");
			stringBuilder.Append("         body {font-family:\"Verdana\";font-weight:normal;font-size: .7em;color:black;} \r\n");
			stringBuilder.Append("         p {font-family:\"Verdana\";font-weight:normal;color:black;margin-top: -5px}\r\n");
			stringBuilder.Append("         b {font-family:\"Verdana\";font-weight:bold;color:black;margin-top: -5px}\r\n");
			stringBuilder.Append("         H1 { font-family:\"Verdana\";font-weight:normal;font-size:18pt;color:red }\r\n");
			stringBuilder.Append("         H2 { font-family:\"Verdana\";font-weight:normal;font-size:14pt;color:maroon }\r\n");
			stringBuilder.Append("         pre {font-family:\"Consolas\",\"Lucida Console\",Monospace;font-size:11pt;margin:0;padding:0.5em;line-height:14pt}\r\n");
			stringBuilder.Append("         .marker {font-weight: bold; color: black;text-decoration: none;}\r\n");
			stringBuilder.Append("         .version {color: gray;}\r\n");
			stringBuilder.Append("         .error {margin-bottom: 10px;}\r\n");
			stringBuilder.Append("         .expandable { text-decoration:underline; font-weight:bold; color:navy; cursor:pointer; }\r\n");
			stringBuilder.Append("         @media screen and (max-width: 639px) {\r\n");
			stringBuilder.Append("          pre { width: 440px; overflow: auto; white-space: pre-wrap; word-wrap: break-word; }\r\n");
			stringBuilder.Append("         }\r\n");
			stringBuilder.Append("         @media screen and (max-width: 479px) {\r\n");
			stringBuilder.Append("          pre { width: 280px; }\r\n");
			stringBuilder.Append("         }\r\n");
			stringBuilder.Append("        </style>\r\n");
			stringBuilder.Append("    </head>\r\n\r\n");
			stringBuilder.Append("    <body bgcolor=\"white\">\r\n\r\n");
			stringBuilder.Append("            <span><H1>" + SR.GetString("Error_Formatter_ASPNET_Error", new object[]
			{
				HttpRuntime.AppDomainAppVirtualPath
			}) + "<hr width=100% size=1 color=silver></H1>\r\n\r\n");
			stringBuilder.Append("            <h2> <i>" + this.ErrorTitle + "</i> </h2></span>\r\n\r\n");
			stringBuilder.Append("            <font face=\"Arial, Helvetica, Geneva, SunSans-Regular, sans-serif \">\r\n\r\n");
			stringBuilder.Append(string.Concat(new string[]
			{
				"            <b> ",
				SR.GetString("Error_Formatter_Description"),
				" </b>",
				this.Description,
				"\r\n"
			}));
			stringBuilder.Append("            <br><br>\r\n\r\n");
			this.WriteErrorDetails(stringBuilder, dontShowSensitiveInfo);
			if (!dontShowSensitiveInfo && !this._dontShowVersion)
			{
				stringBuilder.Append("            <hr width=100% size=1 color=silver>\r\n\r\n");
				stringBuilder.Append(string.Concat(new string[]
				{
					"            <b>",
					SR.GetString("Error_Formatter_Version"),
					"</b>&nbsp;",
					SR.GetString("Error_Formatter_CLR_Build"),
					VersionInfo.ClrVersion,
					SR.GetString("Error_Formatter_ASPNET_Build"),
					VersionInfo.EngineVersion,
					"\r\n\r\n"
				}));
			}
			stringBuilder.Append("            </font>\r\n\r\n");
			stringBuilder.Append("    </body>\r\n");
			stringBuilder.Append("</html>\r\n");
			stringBuilder.Append(this.PostMessage);
			return stringBuilder.ToString();
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00008A90 File Offset: 0x00006C90
		internal void WriteErrorDetails(StringBuilder sb, bool dontShowSensitiveInfo)
		{
			if (this.MiscSectionTitle != null)
			{
				sb.Append(string.Concat(new string[]
				{
					"            <b> ",
					this.MiscSectionTitle,
					": </b>",
					this.MiscSectionContent,
					"<br><br>\r\n\r\n"
				}));
			}
			this.WritePrimaryBox(sb, dontShowSensitiveInfo);
			ConfigurationErrorsException ex = this.Exception as ConfigurationErrorsException;
			if (ex != null && ex.Errors.Count > 1)
			{
				sb.Append(string.Format(CultureInfo.InvariantCulture, "<br><div class=\"expandable\" onclick=\"OnToggleTOCLevel1('{0}')\">{1}:</div>\r\n<div id=\"{0}\" style=\"display: none;\">\r\n            <br>", new object[]
				{
					"additionalConfigurationErrors",
					SR.GetString("TmplConfigurationAdditionalError")
				}));
				sb.Append("            <table width=100% bgcolor=\"#ffffcc\">\r\n               <tr>\r\n                  <td>\r\n                      <code><pre>");
				bool flag = false;
				try
				{
					PermissionSet namedPermissionSet = HttpRuntime.NamedPermissionSet;
					if (namedPermissionSet != null)
					{
						namedPermissionSet.PermitOnly();
						flag = true;
					}
					int num = 0;
					foreach (object obj in ex.Errors)
					{
						ConfigurationException ex2 = (ConfigurationException)obj;
						if (num > 0)
						{
							sb.Append(ex2.Message);
							sb.Append("<BR/>\r\n");
						}
						num++;
					}
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertPermitOnly();
					}
				}
				sb.Append("</pre>                      </code>\r\n\r\n                  </td>\r\n               </tr>\r\n            </table>\r\n\r\n");
				sb.Append("            \r\n\r\n</div>\r\n");
				sb.Append("\r\n        <script type=\"text/javascript\">\r\n        function OnToggleTOCLevel1(level2ID)\r\n        {\r\n        var elemLevel2 = document.getElementById(level2ID);\r\n        if (elemLevel2.style.display == 'none')\r\n        {\r\n            elemLevel2.style.display = '';\r\n        }\r\n        else {\r\n            elemLevel2.style.display = 'none';\r\n        }\r\n        }\r\n        </script>\r\n                            ");
			}
			if (!dontShowSensitiveInfo && this.Exception != null && HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
			{
				this.WriteFusionLogWithAssert(sb);
			}
			this.WriteSecondaryBox(sb, dontShowSensitiveInfo);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00008C2C File Offset: 0x00006E2C
		protected virtual void WritePrimaryBox(StringBuilder sb, bool dontShowSensitiveInfo)
		{
			this.WriteColoredSquare(sb, this.ColoredSquareTitle, this.ColoredSquareDescription, this.ColoredSquareContent, this.WrapColoredSquareContentLines);
			if (this.ShowSourceFileInfo)
			{
				string text = this.GetDisplayPath();
				if (text == null)
				{
					text = SR.GetString("Error_Formatter_No_Source_File");
				}
				sb.Append(string.Concat(new string[]
				{
					"            <b> ",
					SR.GetString("Error_Formatter_Source_File"),
					" </b> ",
					text,
					"<b> &nbsp;&nbsp; ",
					SR.GetString("Error_Formatter_Line"),
					" </b> ",
					this.SourceFileLineNumber.ToString(),
					"\r\n"
				}));
				sb.Append("            <br><br>\r\n\r\n");
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00008CEE File Offset: 0x00006EEE
		protected virtual void WriteSecondaryBox(StringBuilder sb, bool dontShowSensitiveInfo)
		{
			this.WriteColoredSquare(sb, this.ColoredSquare2Title, this.ColoredSquare2Description, this.ColoredSquare2Content, false);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00008D0C File Offset: 0x00006F0C
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void WriteFusionLogWithAssert(StringBuilder sb)
		{
			for (Exception ex = this.Exception; ex != null; ex = ex.InnerException)
			{
				string text = null;
				string text2 = null;
				FileNotFoundException ex2 = ex as FileNotFoundException;
				if (ex2 != null)
				{
					text = ex2.FusionLog;
					text2 = ex2.FileName;
				}
				FileLoadException ex3 = ex as FileLoadException;
				if (ex3 != null)
				{
					text = ex3.FusionLog;
					text2 = ex3.FileName;
				}
				BadImageFormatException ex4 = ex as BadImageFormatException;
				if (ex4 != null)
				{
					text = ex4.FusionLog;
					text2 = ex4.FileName;
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.WriteColoredSquare(sb, SR.GetString("Error_Formatter_FusionLog"), SR.GetString("Error_Formatter_FusionLogDesc", new object[]
					{
						text2
					}), HttpUtility.HtmlEncode(text), false);
					this._fusionLogWritten = true;
					return;
				}
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00008DC4 File Offset: 0x00006FC4
		protected void WriteColoredSquare(StringBuilder sb, string title, string description, string content, bool wrapContentLines)
		{
			if (title != null)
			{
				sb.Append(string.Concat(new string[]
				{
					"            <b>",
					title,
					":</b> ",
					description,
					"<br><br>\r\n\r\n"
				}));
				sb.Append("            <table width=100% bgcolor=\"#ffffcc\">\r\n               <tr>\r\n                  <td>\r\n                      <code>");
				if (!wrapContentLines)
				{
					sb.Append("<pre>");
				}
				sb.Append("\r\n\r\n");
				sb.Append(content);
				if (!wrapContentLines)
				{
					sb.Append("</pre>");
				}
				sb.Append("                      </code>\r\n\r\n                  </td>\r\n               </tr>\r\n            </table>\r\n\r\n");
				sb.Append("            <br>\r\n\r\n");
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00008E62 File Offset: 0x00007062
		internal virtual void PrepareFormatter()
		{
			if (this._adaptiveMiscContent != null)
			{
				this._adaptiveMiscContent.Clear();
			}
			if (this._adaptiveStackTrace != null)
			{
				this._adaptiveStackTrace.Clear();
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual Exception Exception
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060005F1 RID: 1521
		protected abstract string ErrorTitle { get; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060005F2 RID: 1522
		protected abstract string Description { get; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060005F3 RID: 1523
		protected abstract string MiscSectionTitle { get; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060005F4 RID: 1524
		protected abstract string MiscSectionContent { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string ColoredSquareTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string ColoredSquareDescription
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string ColoredSquareContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool WrapColoredSquareContentLines
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string ColoredSquare2Title
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string ColoredSquare2Description
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string ColoredSquare2Content
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00008E8A File Offset: 0x0000708A
		protected virtual StringCollection AdaptiveMiscContent
		{
			get
			{
				if (this._adaptiveMiscContent == null)
				{
					this._adaptiveMiscContent = new StringCollection();
				}
				return this._adaptiveMiscContent;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00008EA5 File Offset: 0x000070A5
		protected virtual StringCollection AdaptiveStackTrace
		{
			get
			{
				if (this._adaptiveStackTrace == null)
				{
					this._adaptiveStackTrace = new StringCollection();
				}
				return this._adaptiveStackTrace;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060005FE RID: 1534
		protected abstract bool ShowSourceFileInfo { get; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string PhysicalPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string VirtualPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual int SourceFileLineNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string PostMessage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00007722 File Offset: 0x00005922
		internal virtual bool CanBeShownToAllUsers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00008EC0 File Offset: 0x000070C0
		protected static bool IsTextRightToLeft
		{
			get
			{
				return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00008ED1 File Offset: 0x000070D1
		protected string WrapWithLeftToRightTextFormatIfNeeded(string content)
		{
			if (ErrorFormatter.IsTextRightToLeft)
			{
				content = "<div dir=\"ltr\">" + content + "</div>";
			}
			return content;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00008EF0 File Offset: 0x000070F0
		internal static string MakeHttpLinePragma(string virtualPath)
		{
			string str = "http://server";
			if (virtualPath != null && !virtualPath.StartsWith("/", StringComparison.Ordinal))
			{
				str += "/";
			}
			return new Uri(str + virtualPath).ToString();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00008F34 File Offset: 0x00007134
		internal static string GetSafePath(string linePragma)
		{
			string virtualPathFromHttpLinePragma = ErrorFormatter.GetVirtualPathFromHttpLinePragma(linePragma);
			if (virtualPathFromHttpLinePragma != null)
			{
				return virtualPathFromHttpLinePragma;
			}
			return HttpRuntime.GetSafePath(linePragma);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00008F54 File Offset: 0x00007154
		internal static string GetVirtualPathFromHttpLinePragma(string linePragma)
		{
			if (string.IsNullOrEmpty(linePragma))
			{
				return null;
			}
			try
			{
				Uri uri = new Uri(linePragma);
				if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
				{
					return uri.LocalPath;
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00008FB8 File Offset: 0x000071B8
		internal static string ResolveHttpFileName(string linePragma)
		{
			string virtualPathFromHttpLinePragma = ErrorFormatter.GetVirtualPathFromHttpLinePragma(linePragma);
			if (virtualPathFromHttpLinePragma == null)
			{
				return linePragma;
			}
			return HostingEnvironment.MapPathInternal(virtualPathFromHttpLinePragma);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00008FD7 File Offset: 0x000071D7
		private string GetDisplayPath()
		{
			if (this.VirtualPath != null)
			{
				return this.VirtualPath;
			}
			if (this.PhysicalPath != null)
			{
				return HttpRuntime.GetSafePath(this.PhysicalPath);
			}
			return null;
		}

		// Token: 0x04000164 RID: 356
		private StringCollection _adaptiveMiscContent;

		// Token: 0x04000165 RID: 357
		private StringCollection _adaptiveStackTrace;

		// Token: 0x04000166 RID: 358
		protected bool _dontShowVersion;

		// Token: 0x04000167 RID: 359
		internal bool _fusionLogWritten;

		// Token: 0x04000168 RID: 360
		internal const string startExpandableBlock = "<br><div class=\"expandable\" onclick=\"OnToggleTOCLevel1('{0}')\">{1}:</div>\r\n<div id=\"{0}\" style=\"display: none;\">\r\n            <br>";

		// Token: 0x04000169 RID: 361
		internal const string startColoredSquare = "            <table width=100% bgcolor=\"#ffffcc\">\r\n               <tr>\r\n                  <td>\r\n                      <code>";

		// Token: 0x0400016A RID: 362
		internal const string endColoredSquare = "                      </code>\r\n\r\n                  </td>\r\n               </tr>\r\n            </table>\r\n\r\n";

		// Token: 0x0400016B RID: 363
		internal const string endExpandableBlock = "            \r\n\r\n</div>\r\n";

		// Token: 0x0400016C RID: 364
		internal const string toggleScript = "\r\n        <script type=\"text/javascript\">\r\n        function OnToggleTOCLevel1(level2ID)\r\n        {\r\n        var elemLevel2 = document.getElementById(level2ID);\r\n        if (elemLevel2.style.display == 'none')\r\n        {\r\n            elemLevel2.style.display = '';\r\n        }\r\n        else {\r\n            elemLevel2.style.display = 'none';\r\n        }\r\n        }\r\n        </script>\r\n                            ";

		// Token: 0x0400016D RID: 365
		protected const string BeginLeftToRightTag = "<div dir=\"ltr\">";

		// Token: 0x0400016E RID: 366
		protected const string EndLeftToRightTag = "</div>";
	}
}
