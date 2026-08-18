using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000058 RID: 88
	internal class UnhandledErrorFormatter : ErrorFormatter
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00008FFD File Offset: 0x000071FD
		internal UnhandledErrorFormatter(Exception e) : this(e, null, null)
		{
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00009008 File Offset: 0x00007208
		internal UnhandledErrorFormatter(Exception e, string message, string postMessage)
		{
			this._message = message;
			this._postMessage = postMessage;
			this._e = e;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00009030 File Offset: 0x00007230
		internal override void PrepareFormatter()
		{
			for (Exception ex = this._e; ex != null; ex = ex.InnerException)
			{
				this._exStack.Add(ex);
				this._initialException = ex;
			}
			this._coloredSquare2Content = this.ColoredSquare2Content;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00009070 File Offset: 0x00007270
		protected override Exception Exception
		{
			get
			{
				return this._e;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00009078 File Offset: 0x00007278
		protected override string ErrorTitle
		{
			get
			{
				string message = this._initialException.Message;
				if (!string.IsNullOrEmpty(message))
				{
					return HttpUtility.FormatPlainTextAsHtml(message);
				}
				return SR.GetString("Unhandled_Err_Error");
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000090AA File Offset: 0x000072AA
		protected override string Description
		{
			get
			{
				if (this._message != null)
				{
					return this._message;
				}
				return SR.GetString("Unhandled_Err_Desc");
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000090C5 File Offset: 0x000072C5
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("Unhandled_Err_Exception_Details");
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000090D4 File Offset: 0x000072D4
		protected override string MiscSectionContent
		{
			get
			{
				string fullName = this._initialException.GetType().FullName;
				StringBuilder stringBuilder = new StringBuilder(fullName);
				string text = fullName;
				if (this._initialException.Message != null)
				{
					string text2 = HttpUtility.FormatPlainTextAsHtml(this._initialException.Message);
					stringBuilder.Append(": ");
					stringBuilder.Append(text2);
					text = text + ": " + text2;
				}
				this.AdaptiveMiscContent.Add(text);
				if (this._initialException is UnauthorizedAccessException)
				{
					stringBuilder.Append("\r\n<br><br>");
					string text3 = SR.GetString("Unauthorized_Err_Desc1");
					text3 = HttpUtility.HtmlEncode(text3);
					stringBuilder.Append(text3);
					this.AdaptiveMiscContent.Add(text3);
					stringBuilder.Append("\r\n<br><br>");
					text3 = SR.GetString("Unauthorized_Err_Desc2");
					text3 = HttpUtility.HtmlEncode(text3);
					stringBuilder.Append(text3);
					this.AdaptiveMiscContent.Add(text3);
				}
				else if (this._initialException is HostingEnvironmentException)
				{
					string details = ((HostingEnvironmentException)this._initialException).Details;
					if (!string.IsNullOrEmpty(details))
					{
						stringBuilder.Append("\r\n<br><br><b>");
						stringBuilder.Append(details);
						stringBuilder.Append("</b>");
						this.AdaptiveMiscContent.Add(details);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00009223 File Offset: 0x00007423
		protected override string ColoredSquareTitle
		{
			get
			{
				return SR.GetString("TmplCompilerSourceSecTitle");
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00009230 File Offset: 0x00007430
		protected override string ColoredSquareContent
		{
			get
			{
				if (this._physicalPath == null)
				{
					bool flag = false;
					string text;
					if (!this._fGeneratedCodeOnStack || !HttpRuntime.HasAspNetHostingPermission(AspNetHostingPermissionLevel.Medium))
					{
						text = SR.GetString("Src_not_available_nodebug");
					}
					else
					{
						if (ErrorFormatter.IsTextRightToLeft)
						{
							flag = true;
						}
						text = SR.GetString("Src_not_available", new object[]
						{
							flag ? "BeginMarker" : string.Empty,
							flag ? "EndMarker" : string.Empty,
							flag ? "BeginMarker" : string.Empty,
							flag ? "EndMarker" : string.Empty
						});
					}
					text = HttpUtility.FormatPlainTextAsHtml(text);
					if (flag)
					{
						text = text.Replace("BeginMarker", "</code><div dir=\"ltr\"><code>");
						text = text.Replace("EndMarker", "</code></div><code>");
					}
					return text;
				}
				return FormatterWithFileInfo.GetSourceFileLines(this._physicalPath, Encoding.Default, null, this._line);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00009312 File Offset: 0x00007512
		protected override bool WrapColoredSquareContentLines
		{
			get
			{
				return this._physicalPath == null;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000931D File Offset: 0x0000751D
		protected override string ColoredSquare2Title
		{
			get
			{
				return SR.GetString("Unhandled_Err_Stack_Trace");
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000932C File Offset: 0x0000752C
		protected override string ColoredSquare2Content
		{
			get
			{
				if (this._coloredSquare2Content != null)
				{
					return this._coloredSquare2Content;
				}
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				int num = 0;
				for (int i = this._exStack.Count - 1; i >= 0; i--)
				{
					if (i < this._exStack.Count - 1)
					{
						stringBuilder.Append("\r\n");
					}
					Exception ex = (Exception)this._exStack[i];
					stringBuilder.Append("[" + this._exStack[i].GetType().Name);
					if (ex is ExternalException && ((ExternalException)ex).ErrorCode != 0)
					{
						stringBuilder.Append(" (0x" + ((ExternalException)ex).ErrorCode.ToString("x", CultureInfo.CurrentCulture) + ")");
					}
					if (ex.Message != null && ex.Message.Length > 0)
					{
						stringBuilder.Append(": " + ex.Message);
					}
					stringBuilder.Append("]\r\n");
					StackTrace stackTrace = new StackTrace(ex, true);
					for (int j = 0; j < stackTrace.FrameCount; j++)
					{
						if (flag)
						{
							num = stringBuilder.Length;
						}
						StackFrame frame = stackTrace.GetFrame(j);
						MethodBase method = frame.GetMethod();
						Type declaringType = method.DeclaringType;
						string text = string.Empty;
						if (declaringType != null)
						{
							string text2 = null;
							try
							{
								text2 = Util.GetAssemblyCodeBase(declaringType.Assembly);
							}
							catch
							{
							}
							if (text2 != null)
							{
								text2 = Path.GetDirectoryName(text2);
								if (string.Compare(text2, HttpRuntime.CodegenDirInternal, StringComparison.OrdinalIgnoreCase) == 0 && frame.GetNativeOffset() > 0)
								{
									this._fGeneratedCodeOnStack = true;
								}
							}
							text = declaringType.Namespace;
						}
						if (text != null)
						{
							text += ".";
						}
						if (declaringType == null)
						{
							stringBuilder.Append("   " + method.Name + "(");
						}
						else
						{
							stringBuilder.Append(string.Concat(new string[]
							{
								"   ",
								text,
								declaringType.Name,
								".",
								method.Name,
								"("
							}));
						}
						ParameterInfo[] parameters = method.GetParameters();
						for (int k = 0; k < parameters.Length; k++)
						{
							stringBuilder.Append(((k != 0) ? ", " : string.Empty) + parameters[k].ParameterType.Name + " " + parameters[k].Name);
						}
						stringBuilder.Append(")");
						string text3 = this.GetFileName(frame);
						if (text3 != null)
						{
							text3 = ErrorFormatter.ResolveHttpFileName(text3);
							if (text3 != null)
							{
								if (this._physicalPath == null && FileUtil.FileExists(text3))
								{
									this._physicalPath = text3;
									this._line = frame.GetFileLineNumber();
								}
								stringBuilder.Append(" in " + HttpRuntime.GetSafePath(text3) + ":" + frame.GetFileLineNumber().ToString());
							}
						}
						else
						{
							stringBuilder.Append(" +" + frame.GetNativeOffset().ToString());
						}
						if (flag)
						{
							string s = stringBuilder.ToString(num, stringBuilder.Length - num);
							this.AdaptiveStackTrace.Add(HttpUtility.HtmlEncode(s));
						}
						stringBuilder.Append("\r\n");
					}
					flag = false;
				}
				this._coloredSquare2Content = HttpUtility.HtmlEncode(stringBuilder.ToString());
				this._coloredSquare2Content = base.WrapWithLeftToRightTextFormatIfNeeded(this._coloredSquare2Content);
				return this._coloredSquare2Content;
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000096D8 File Offset: 0x000078D8
		private string GetFileName(StackFrame sf)
		{
			string result = null;
			try
			{
				result = sf.GetFileName();
			}
			catch (SecurityException)
			{
			}
			return result;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00009704 File Offset: 0x00007904
		protected override string PostMessage
		{
			get
			{
				return this._postMessage;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000970C File Offset: 0x0000790C
		protected override bool ShowSourceFileInfo
		{
			get
			{
				return this._physicalPath != null;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00009717 File Offset: 0x00007917
		protected override string PhysicalPath
		{
			get
			{
				return this._physicalPath;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000971F File Offset: 0x0000791F
		protected override int SourceFileLineNumber
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x0400016F RID: 367
		protected Exception _e;

		// Token: 0x04000170 RID: 368
		protected Exception _initialException;

		// Token: 0x04000171 RID: 369
		protected ArrayList _exStack = new ArrayList();

		// Token: 0x04000172 RID: 370
		protected string _physicalPath;

		// Token: 0x04000173 RID: 371
		protected int _line;

		// Token: 0x04000174 RID: 372
		private string _coloredSquare2Content;

		// Token: 0x04000175 RID: 373
		private bool _fGeneratedCodeOnStack;

		// Token: 0x04000176 RID: 374
		protected string _message;

		// Token: 0x04000177 RID: 375
		protected string _postMessage;
	}
}
