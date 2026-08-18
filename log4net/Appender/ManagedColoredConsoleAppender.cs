using System;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x0200002F RID: 47
	public class ManagedColoredConsoleAppender : AppenderSkeleton
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005E7B File Offset: 0x0000407B
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00005E90 File Offset: 0x00004090
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

		// Token: 0x060001B1 RID: 433 RVA: 0x00005EC0 File Offset: 0x000040C0
		public void AddMapping(ManagedColoredConsoleAppender.LevelColors mapping)
		{
			this.m_levelMapping.Add(mapping);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005ED0 File Offset: 0x000040D0
		protected override void Append(LoggingEvent loggingEvent)
		{
			TextWriter textWriter;
			if (this.m_writeToErrorStream)
			{
				textWriter = Console.Error;
			}
			else
			{
				textWriter = Console.Out;
			}
			Console.ResetColor();
			ManagedColoredConsoleAppender.LevelColors levelColors = this.m_levelMapping.Lookup(loggingEvent.Level) as ManagedColoredConsoleAppender.LevelColors;
			if (levelColors != null)
			{
				if (levelColors.HasBackColor)
				{
					Console.BackgroundColor = levelColors.BackColor;
				}
				if (levelColors.HasForeColor)
				{
					Console.ForegroundColor = levelColors.ForeColor;
				}
			}
			string value = base.RenderLoggingEvent(loggingEvent);
			textWriter.Write(value);
			Console.ResetColor();
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00005F4C File Offset: 0x0000414C
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005F4F File Offset: 0x0000414F
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			this.m_levelMapping.ActivateOptions();
		}

		// Token: 0x040000BF RID: 191
		public const string ConsoleOut = "Console.Out";

		// Token: 0x040000C0 RID: 192
		public const string ConsoleError = "Console.Error";

		// Token: 0x040000C1 RID: 193
		private bool m_writeToErrorStream;

		// Token: 0x040000C2 RID: 194
		private LevelMapping m_levelMapping = new LevelMapping();

		// Token: 0x02000030 RID: 48
		public class LevelColors : LevelMappingEntry
		{
			// Token: 0x17000064 RID: 100
			// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005F62 File Offset: 0x00004162
			// (set) Token: 0x060001B6 RID: 438 RVA: 0x00005F6A File Offset: 0x0000416A
			public ConsoleColor ForeColor
			{
				get
				{
					return this.foreColor;
				}
				set
				{
					this.foreColor = value;
					this.hasForeColor = true;
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005F7A File Offset: 0x0000417A
			internal bool HasForeColor
			{
				get
				{
					return this.hasForeColor;
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005F82 File Offset: 0x00004182
			// (set) Token: 0x060001B9 RID: 441 RVA: 0x00005F8A File Offset: 0x0000418A
			public ConsoleColor BackColor
			{
				get
				{
					return this.backColor;
				}
				set
				{
					this.backColor = value;
					this.hasBackColor = true;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x060001BA RID: 442 RVA: 0x00005F9A File Offset: 0x0000419A
			internal bool HasBackColor
			{
				get
				{
					return this.hasBackColor;
				}
			}

			// Token: 0x040000C3 RID: 195
			private ConsoleColor foreColor;

			// Token: 0x040000C4 RID: 196
			private bool hasForeColor;

			// Token: 0x040000C5 RID: 197
			private ConsoleColor backColor;

			// Token: 0x040000C6 RID: 198
			private bool hasBackColor;
		}
	}
}
