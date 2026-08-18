using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Security;
using System.Text;

namespace System.Web
{
	// Token: 0x02000061 RID: 97
	internal class ConfigErrorFormatter : FormatterWithFileInfo
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0000A468 File Offset: 0x00008668
		internal ConfigErrorFormatter(ConfigurationException e) : base(null, e.Filename, null, e.Line)
		{
			this._e = e;
			PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_PRE_PROCESSING);
			PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_TOTAL);
			this._message = HttpUtility.FormatPlainTextAsHtml(e.BareMessage);
			this._adaptiveMiscContent.Add(this._message);
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0000A4CC File Offset: 0x000086CC
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0000A4D4 File Offset: 0x000086D4
		public bool AllowSourceCode
		{
			get
			{
				return this._allowSourceCode;
			}
			set
			{
				this._allowSourceCode = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0000A4DD File Offset: 0x000086DD
		protected override Encoding SourceFileEncoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0000A4E4 File Offset: 0x000086E4
		protected override Exception Exception
		{
			get
			{
				return this._e;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0000A4EC File Offset: 0x000086EC
		protected override string ErrorTitle
		{
			get
			{
				return SR.GetString("Config_Error");
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0000A4F8 File Offset: 0x000086F8
		protected override string Description
		{
			get
			{
				return SR.GetString("Config_Desc");
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000A43E File Offset: 0x0000863E
		protected override string MiscSectionTitle
		{
			get
			{
				return SR.GetString("Parser_Error_Message");
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0000A504 File Offset: 0x00008704
		protected override string MiscSectionContent
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000A452 File Offset: 0x00008652
		protected override string ColoredSquareTitle
		{
			get
			{
				return SR.GetString("Parser_Source_Error");
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0000A50C File Offset: 0x0000870C
		protected override StringCollection AdaptiveMiscContent
		{
			get
			{
				return this._adaptiveMiscContent;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0000A514 File Offset: 0x00008714
		protected override string ColoredSquareContent
		{
			get
			{
				if (!this.AllowSourceCode)
				{
					return SR.GetString("Generic_Err_Remote_Desc");
				}
				return base.ColoredSquareContent;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0000A52F File Offset: 0x0000872F
		protected override bool WrapColoredSquareContentLines
		{
			get
			{
				return !this.AllowSourceCode || base.WrapColoredSquareContentLines;
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000A544 File Offset: 0x00008744
		protected override void WriteSecondaryBox(StringBuilder sb, bool dontShowSensitiveInfo)
		{
			ErrorFormatter formatterForInnerException = ConfigErrorFormatter.GetFormatterForInnerException((ConfigurationException)this._e);
			if (formatterForInnerException == null || this._fusionLogWritten)
			{
				base.WriteSecondaryBox(sb, dontShowSensitiveInfo);
				return;
			}
			sb.Append(string.Format(CultureInfo.InvariantCulture, "<br><div class=\"expandable\" onclick=\"OnToggleTOCLevel1('{0}')\">{1}:</div>\r\n<div id=\"{0}\" style=\"display: none;\">\r\n            <br>", new object[]
			{
				"additionalConfigErrorInfo",
				SR.GetString("AdditionalConfigErrorInfo")
			}));
			formatterForInnerException.PrepareFormatter();
			formatterForInnerException.WriteErrorDetails(sb, dontShowSensitiveInfo);
			sb.Append("            \r\n\r\n</div>\r\n");
			sb.Append("\r\n        <script type=\"text/javascript\">\r\n        function OnToggleTOCLevel1(level2ID)\r\n        {\r\n        var elemLevel2 = document.getElementById(level2ID);\r\n        if (elemLevel2.style.display == 'none')\r\n        {\r\n            elemLevel2.style.display = '';\r\n        }\r\n        else {\r\n            elemLevel2.style.display = 'none';\r\n        }\r\n        }\r\n        </script>\r\n                            ");
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000A5D0 File Offset: 0x000087D0
		private static ErrorFormatter GetFormatterForInnerException(Exception e)
		{
			ErrorFormatter errorFormatter = null;
			Exception innerException = e.InnerException;
			while (innerException != null && innerException is ConfigurationException)
			{
				innerException = innerException.InnerException;
			}
			if (innerException == null)
			{
				return null;
			}
			if (innerException is SecurityException)
			{
				return new SecurityErrorFormatter(e);
			}
			if (errorFormatter == null)
			{
				errorFormatter = HttpException.GetErrorFormatter(innerException);
				if (errorFormatter is ConfigErrorFormatter)
				{
					errorFormatter = null;
				}
			}
			if (errorFormatter == null)
			{
				errorFormatter = new UnhandledErrorFormatter(innerException);
			}
			return errorFormatter;
		}

		// Token: 0x0400018B RID: 395
		protected string _message;

		// Token: 0x0400018C RID: 396
		private Exception _e;

		// Token: 0x0400018D RID: 397
		private StringCollection _adaptiveMiscContent = new StringCollection();

		// Token: 0x0400018E RID: 398
		private bool _allowSourceCode;
	}
}
