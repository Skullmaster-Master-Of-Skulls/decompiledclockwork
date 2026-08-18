using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Web
{
	// Token: 0x0200005F RID: 95
	internal class DynamicCompileErrorFormatter : ErrorFormatter
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x00009C82 File Offset: 0x00007E82
		internal DynamicCompileErrorFormatter(HttpCompileException excep)
		{
			this._excep = excep;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00009C91 File Offset: 0x00007E91
		protected override Exception Exception
		{
			get
			{
				return this._excep;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00007722 File Offset: 0x00005922
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00009C99 File Offset: 0x00007E99
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("TmplCompilerErrorTitle");
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00009CA5 File Offset: 0x00007EA5
		protected override string Description
		{
			get
			{
				return SR.GetString("TmplCompilerErrorDesc");
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00009CB1 File Offset: 0x00007EB1
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("TmplCompilerErrorSecTitle");
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00009CC0 File Offset: 0x00007EC0
		protected override string MiscSectionContent
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(128);
				CompilerResults resultsWithoutDemand = this._excep.ResultsWithoutDemand;
				if (resultsWithoutDemand.Errors.Count == 0 && resultsWithoutDemand.NativeCompilerReturnValue != 0)
				{
					string @string = SR.GetString("TmplCompilerFatalError", new object[]
					{
						resultsWithoutDemand.NativeCompilerReturnValue.ToString("G", CultureInfo.CurrentCulture)
					});
					this.AdaptiveMiscContent.Add(@string);
					stringBuilder.Append(@string);
					stringBuilder.Append("<br><br>\r\n");
				}
				if (resultsWithoutDemand.Errors.HasErrors)
				{
					CompilerError firstCompileError = this._excep.FirstCompileError;
					if (firstCompileError != null)
					{
						string text = HttpUtility.HtmlEncode(firstCompileError.ErrorNumber);
						string text2 = text;
						stringBuilder.Append(text);
						if (HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
						{
							text = HttpUtility.HtmlEncode(firstCompileError.ErrorText);
							stringBuilder.Append(": ");
							stringBuilder.Append(text);
							text2 = text2 + ": " + text;
						}
						this.AdaptiveMiscContent.Add(text2);
						stringBuilder.Append("<br><br>\r\n");
						stringBuilder.Append("<b>");
						stringBuilder.Append(SR.GetString("TmplCompilerSourceSecTitle"));
						stringBuilder.Append(":</b><br><br>\r\n");
						stringBuilder.Append("            <table width=100% bgcolor=\"#ffffcc\">\r\n");
						stringBuilder.Append("               <tr><td>\r\n");
						stringBuilder.Append("               ");
						stringBuilder.Append("               </td></tr>\r\n");
						stringBuilder.Append("               <tr>\r\n");
						stringBuilder.Append("                  <td>\r\n");
						stringBuilder.Append("                      <code><pre>\r\n\r\n");
						stringBuilder.Append(FormatterWithFileInfo.GetSourceFileLines(firstCompileError.FileName, Encoding.Default, this._excep.SourceCodeWithoutDemand, firstCompileError.Line));
						stringBuilder.Append("</pre></code>\r\n\r\n");
						stringBuilder.Append("                  </td>\r\n");
						stringBuilder.Append("               </tr>\r\n");
						stringBuilder.Append("            </table>\r\n\r\n");
						stringBuilder.Append("            <br>\r\n\r\n");
						stringBuilder.Append("            <b>");
						stringBuilder.Append(SR.GetString("TmplCompilerSourceFileTitle"));
						stringBuilder.Append(":</b> ");
						this._sourceFilePath = ErrorFormatter.GetSafePath(firstCompileError.FileName);
						stringBuilder.Append(HttpUtility.HtmlEncode(this._sourceFilePath));
						stringBuilder.Append("\r\n");
						TypeConverter typeConverter = new Int32Converter();
						stringBuilder.Append("            &nbsp;&nbsp; <b>");
						stringBuilder.Append(SR.GetString("TmplCompilerSourceFileLine"));
						stringBuilder.Append(":</b>  ");
						this._sourceFileLineNumber = firstCompileError.Line;
						stringBuilder.Append(HttpUtility.HtmlEncode(typeConverter.ConvertToString(this._sourceFileLineNumber)));
						stringBuilder.Append("\r\n");
						stringBuilder.Append("            <br><br>\r\n");
					}
				}
				if (resultsWithoutDemand.Errors.HasWarnings)
				{
					stringBuilder.Append("<br><div class=\"expandable\" onclick=\"OnToggleTOCLevel1('warningDiv')\">");
					stringBuilder.Append(SR.GetString("TmplCompilerWarningBanner"));
					stringBuilder.Append(":</div>\r\n");
					stringBuilder.Append("<div id=\"warningDiv\" style=\"display: none;\">\r\n");
					foreach (object obj in resultsWithoutDemand.Errors)
					{
						CompilerError compilerError = (CompilerError)obj;
						if (compilerError.IsWarning)
						{
							stringBuilder.Append("<b>");
							stringBuilder.Append(SR.GetString("TmplCompilerWarningSecTitle"));
							stringBuilder.Append(":</b> ");
							stringBuilder.Append(HttpUtility.HtmlEncode(compilerError.ErrorNumber));
							if (HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
							{
								stringBuilder.Append(": ");
								stringBuilder.Append(HttpUtility.HtmlEncode(compilerError.ErrorText));
							}
							stringBuilder.Append("<br>\r\n");
							stringBuilder.Append("<b>");
							stringBuilder.Append(SR.GetString("TmplCompilerSourceSecTitle"));
							stringBuilder.Append(":</b><br><br>\r\n");
							stringBuilder.Append("            <table width=100% bgcolor=\"#ffffcc\">\r\n");
							stringBuilder.Append("               <tr><td>\r\n");
							stringBuilder.Append("               <b>");
							stringBuilder.Append(HttpUtility.HtmlEncode(HttpRuntime.GetSafePath(compilerError.FileName)));
							stringBuilder.Append("</b>\r\n");
							stringBuilder.Append("               </td></tr>\r\n");
							stringBuilder.Append("               <tr>\r\n");
							stringBuilder.Append("                  <td>\r\n");
							stringBuilder.Append("                      <code><pre>\r\n\r\n");
							stringBuilder.Append(FormatterWithFileInfo.GetSourceFileLines(compilerError.FileName, Encoding.Default, this._excep.SourceCodeWithoutDemand, compilerError.Line));
							stringBuilder.Append("</pre></code>\r\n\r\n");
							stringBuilder.Append("                  </td>\r\n");
							stringBuilder.Append("               </tr>\r\n");
							stringBuilder.Append("            </table>\r\n\r\n");
							stringBuilder.Append("            <br>\r\n\r\n");
						}
					}
					stringBuilder.Append("</div>\r\n");
				}
				if (!this._hideDetailedCompilerOutput)
				{
					if (resultsWithoutDemand.Output.Count > 0 && HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
					{
						stringBuilder.Append(string.Format(CultureInfo.CurrentCulture, "<br><div class=\"expandable\" onclick=\"OnToggleTOCLevel1('{0}')\">{1}:</div>\r\n<div id=\"{0}\" style=\"display: none;\">\r\n            <br>", new object[]
						{
							"compilerOutputDiv",
							SR.GetString("TmplCompilerCompleteOutput")
						}));
						stringBuilder.Append("            <table width=100% bgcolor=\"#ffffcc\">\r\n               <tr>\r\n                  <td>\r\n                      <code><pre>");
						foreach (string s in resultsWithoutDemand.Output)
						{
							stringBuilder.Append(HttpUtility.HtmlEncode(s));
							stringBuilder.Append("\r\n");
						}
						stringBuilder.Append("</pre>                      </code>\r\n\r\n                  </td>\r\n               </tr>\r\n            </table>\r\n\r\n");
						stringBuilder.Append("            \r\n\r\n</div>\r\n");
					}
					if (this._excep.SourceCodeWithoutDemand != null && HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
					{
						stringBuilder.Append(string.Format(CultureInfo.CurrentCulture, "<br><div class=\"expandable\" onclick=\"OnToggleTOCLevel1('{0}')\">{1}:</div>\r\n<div id=\"{0}\" style=\"display: none;\">\r\n            <br>", new object[]
						{
							"dynamicCodeDiv",
							SR.GetString("TmplCompilerGeneratedFile")
						}));
						stringBuilder.Append("            <table width=100% bgcolor=\"#ffffcc\">\r\n               <tr>\r\n                  <td>\r\n                      <code><pre>");
						string[] array = this._excep.SourceCodeWithoutDemand.Split(new char[]
						{
							'\n'
						});
						int num = 1;
						foreach (string s2 in array)
						{
							string text3 = num.ToString("G", CultureInfo.CurrentCulture);
							stringBuilder.Append(SR.GetString("TmplCompilerLineHeader", new object[]
							{
								text3
							}));
							if (text3.Length < 5)
							{
								stringBuilder.Append(' ', 5 - text3.Length);
							}
							num++;
							stringBuilder.Append(HttpUtility.HtmlEncode(s2));
						}
						stringBuilder.Append("</pre>                      </code>\r\n\r\n                  </td>\r\n               </tr>\r\n            </table>\r\n\r\n");
						stringBuilder.Append("            \r\n\r\n</div>\r\n");
					}
					stringBuilder.Append("\r\n        <script type=\"text/javascript\">\r\n        function OnToggleTOCLevel1(level2ID)\r\n        {\r\n        var elemLevel2 = document.getElementById(level2ID);\r\n        if (elemLevel2.style.display == 'none')\r\n        {\r\n            elemLevel2.style.display = '';\r\n        }\r\n        else {\r\n            elemLevel2.style.display = 'none';\r\n        }\r\n        }\r\n        </script>\r\n                            ");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0000A3D0 File Offset: 0x000085D0
		protected override string PhysicalPath
		{
			get
			{
				return this._sourceFilePath;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0000A3D8 File Offset: 0x000085D8
		protected override int SourceFileLineNumber
		{
			get
			{
				return this._sourceFileLineNumber;
			}
		}

		// Token: 0x04000183 RID: 387
		private const int errorRange = 2;

		// Token: 0x04000184 RID: 388
		private HttpCompileException _excep;

		// Token: 0x04000185 RID: 389
		private string _sourceFilePath;

		// Token: 0x04000186 RID: 390
		private int _sourceFileLineNumber;

		// Token: 0x04000187 RID: 391
		protected bool _hideDetailedCompilerOutput;
	}
}
