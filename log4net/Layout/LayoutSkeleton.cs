using System;
using System.Globalization;
using System.IO;
using log4net.Core;

namespace log4net.Layout
{
	// Token: 0x020000A7 RID: 167
	public abstract class LayoutSkeleton : ILayout, IOptionHandler
	{
		// Token: 0x060004E5 RID: 1253
		public abstract void ActivateOptions();

		// Token: 0x060004E6 RID: 1254
		public abstract void Format(TextWriter writer, LoggingEvent loggingEvent);

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000F638 File Offset: 0x0000D838
		public string Format(LoggingEvent loggingEvent)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.Format(stringWriter, loggingEvent);
			return stringWriter.ToString();
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000F65E File Offset: 0x0000D85E
		public virtual string ContentType
		{
			get
			{
				return "text/plain";
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000F665 File Offset: 0x0000D865
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000F66D File Offset: 0x0000D86D
		public virtual string Header
		{
			get
			{
				return this.m_header;
			}
			set
			{
				this.m_header = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000F676 File Offset: 0x0000D876
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000F67E File Offset: 0x0000D87E
		public virtual string Footer
		{
			get
			{
				return this.m_footer;
			}
			set
			{
				this.m_footer = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000F687 File Offset: 0x0000D887
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0000F68F File Offset: 0x0000D88F
		public virtual bool IgnoresException
		{
			get
			{
				return this.m_ignoresException;
			}
			set
			{
				this.m_ignoresException = value;
			}
		}

		// Token: 0x04000207 RID: 519
		private string m_header;

		// Token: 0x04000208 RID: 520
		private string m_footer;

		// Token: 0x04000209 RID: 521
		private bool m_ignoresException = true;
	}
}
