using System;
using System.IO;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000021 RID: 33
	public class TextWriterAppender : AppenderSkeleton
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00004A44 File Offset: 0x00002C44
		public TextWriterAppender()
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004A53 File Offset: 0x00002C53
		[Obsolete("Instead use the default constructor and set the Layout & Writer properties")]
		public TextWriterAppender(ILayout layout, Stream os) : this(layout, new StreamWriter(os))
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004A62 File Offset: 0x00002C62
		[Obsolete("Instead use the default constructor and set the Layout & Writer properties")]
		public TextWriterAppender(ILayout layout, TextWriter writer)
		{
			this.Layout = layout;
			this.Writer = writer;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004A7F File Offset: 0x00002C7F
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00004A87 File Offset: 0x00002C87
		public bool ImmediateFlush
		{
			get
			{
				return this.m_immediateFlush;
			}
			set
			{
				this.m_immediateFlush = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004A90 File Offset: 0x00002C90
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00004A98 File Offset: 0x00002C98
		public virtual TextWriter Writer
		{
			get
			{
				return this.m_qtw;
			}
			set
			{
				lock (this)
				{
					this.Reset();
					if (value != null)
					{
						this.m_qtw = new QuietTextWriter(value, this.ErrorHandler);
						this.WriteHeader();
					}
				}
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004AF0 File Offset: 0x00002CF0
		protected override bool PreAppendCheck()
		{
			if (!base.PreAppendCheck())
			{
				return false;
			}
			if (this.m_qtw == null)
			{
				this.PrepareWriter();
				if (this.m_qtw == null)
				{
					this.ErrorHandler.Error("No output stream or file set for the appender named [" + base.Name + "].");
					return false;
				}
			}
			if (this.m_qtw.Closed)
			{
				this.ErrorHandler.Error("Output stream for appender named [" + base.Name + "] has been closed.");
				return false;
			}
			return true;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004B6F File Offset: 0x00002D6F
		protected override void Append(LoggingEvent loggingEvent)
		{
			base.RenderLoggingEvent(this.m_qtw, loggingEvent);
			if (this.m_immediateFlush)
			{
				this.m_qtw.Flush();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004B94 File Offset: 0x00002D94
		protected override void Append(LoggingEvent[] loggingEvents)
		{
			foreach (LoggingEvent loggingEvent in loggingEvents)
			{
				base.RenderLoggingEvent(this.m_qtw, loggingEvent);
			}
			if (this.m_immediateFlush)
			{
				this.m_qtw.Flush();
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004BD8 File Offset: 0x00002DD8
		protected override void OnClose()
		{
			lock (this)
			{
				this.Reset();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004C14 File Offset: 0x00002E14
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00004C1C File Offset: 0x00002E1C
		public override IErrorHandler ErrorHandler
		{
			get
			{
				return base.ErrorHandler;
			}
			set
			{
				lock (this)
				{
					if (value == null)
					{
						LogLog.Warn(TextWriterAppender.declaringType, "TextWriterAppender: You have tried to set a null error-handler.");
					}
					else
					{
						base.ErrorHandler = value;
						if (this.m_qtw != null)
						{
							this.m_qtw.ErrorHandler = value;
						}
					}
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004C80 File Offset: 0x00002E80
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004C83 File Offset: 0x00002E83
		protected virtual void WriteFooterAndCloseWriter()
		{
			this.WriteFooter();
			this.CloseWriter();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004C94 File Offset: 0x00002E94
		protected virtual void CloseWriter()
		{
			if (this.m_qtw != null)
			{
				try
				{
					this.m_qtw.Close();
				}
				catch (Exception e)
				{
					this.ErrorHandler.Error("Could not close writer [" + this.m_qtw + "]", e);
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004CEC File Offset: 0x00002EEC
		protected virtual void Reset()
		{
			this.WriteFooterAndCloseWriter();
			this.m_qtw = null;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004CFC File Offset: 0x00002EFC
		protected virtual void WriteFooter()
		{
			if (this.Layout != null && this.m_qtw != null && !this.m_qtw.Closed)
			{
				string footer = this.Layout.Footer;
				if (footer != null)
				{
					this.m_qtw.Write(footer);
				}
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004D44 File Offset: 0x00002F44
		protected virtual void WriteHeader()
		{
			if (this.Layout != null && this.m_qtw != null && !this.m_qtw.Closed)
			{
				string header = this.Layout.Header;
				if (header != null)
				{
					this.m_qtw.Write(header);
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004D89 File Offset: 0x00002F89
		protected virtual void PrepareWriter()
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004D8B File Offset: 0x00002F8B
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00004D93 File Offset: 0x00002F93
		protected QuietTextWriter QuietWriter
		{
			get
			{
				return this.m_qtw;
			}
			set
			{
				this.m_qtw = value;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004D9C File Offset: 0x00002F9C
		public override bool Flush(int millisecondsTimeout)
		{
			if (this.m_immediateFlush)
			{
				return true;
			}
			lock (this)
			{
				this.m_qtw.Flush();
			}
			return true;
		}

		// Token: 0x04000081 RID: 129
		private QuietTextWriter m_qtw;

		// Token: 0x04000082 RID: 130
		private bool m_immediateFlush = true;

		// Token: 0x04000083 RID: 131
		private static readonly Type declaringType = typeof(TextWriterAppender);
	}
}
