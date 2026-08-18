using System;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x0200001D RID: 29
	public class ConsoleAppender : AppenderSkeleton
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000042E8 File Offset: 0x000024E8
		public ConsoleAppender()
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000042F0 File Offset: 0x000024F0
		[Obsolete("Instead use the default constructor and set the Layout property")]
		public ConsoleAppender(ILayout layout) : this(layout, false)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000042FA File Offset: 0x000024FA
		[Obsolete("Instead use the default constructor and set the Layout & Target properties")]
		public ConsoleAppender(ILayout layout, bool writeToErrorStream)
		{
			this.Layout = layout;
			this.m_writeToErrorStream = writeToErrorStream;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004310 File Offset: 0x00002510
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004328 File Offset: 0x00002528
		public virtual string Target
		{
			get
			{
				if (!this.m_writeToErrorStream)
				{
					return "Console.Out";
				}
				return "Console.Error";
			}
			set
			{
				string b = value.Trim();
				if (SystemInfo.EqualsIgnoringCase("Console.Error", b))
				{
					this.m_writeToErrorStream = true;
					return;
				}
				this.m_writeToErrorStream = false;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004358 File Offset: 0x00002558
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_writeToErrorStream)
			{
				Console.Error.Write(base.RenderLoggingEvent(loggingEvent));
				return;
			}
			Console.Write(base.RenderLoggingEvent(loggingEvent));
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004380 File Offset: 0x00002580
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000070 RID: 112
		public const string ConsoleOut = "Console.Out";

		// Token: 0x04000071 RID: 113
		public const string ConsoleError = "Console.Error";

		// Token: 0x04000072 RID: 114
		private bool m_writeToErrorStream;
	}
}
