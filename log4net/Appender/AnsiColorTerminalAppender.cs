using System;
using System.Text;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x0200000A RID: 10
	public class AnsiColorTerminalAppender : AppenderSkeleton
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000031C0 File Offset: 0x000013C0
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000031D8 File Offset: 0x000013D8
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

		// Token: 0x0600006D RID: 109 RVA: 0x00003208 File Offset: 0x00001408
		public void AddMapping(AnsiColorTerminalAppender.LevelColors mapping)
		{
			this.m_levelMapping.Add(mapping);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003218 File Offset: 0x00001418
		protected override void Append(LoggingEvent loggingEvent)
		{
			string text = base.RenderLoggingEvent(loggingEvent);
			AnsiColorTerminalAppender.LevelColors levelColors = this.m_levelMapping.Lookup(loggingEvent.Level) as AnsiColorTerminalAppender.LevelColors;
			if (levelColors != null)
			{
				text = levelColors.CombinedColor + text;
			}
			if (text.Length > 1)
			{
				if (text.EndsWith("\r\n") || text.EndsWith("\n\r"))
				{
					text = text.Insert(text.Length - 2, "\u001b[0m");
				}
				else if (text.EndsWith("\n") || text.EndsWith("\r"))
				{
					text = text.Insert(text.Length - 1, "\u001b[0m");
				}
				else
				{
					text += "\u001b[0m";
				}
			}
			else if (text[0] == '\n' || text[0] == '\r')
			{
				text = "\u001b[0m" + text;
			}
			else
			{
				text += "\u001b[0m";
			}
			if (this.m_writeToErrorStream)
			{
				Console.Error.Write(text);
				return;
			}
			Console.Write(text);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003315 File Offset: 0x00001515
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003318 File Offset: 0x00001518
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			this.m_levelMapping.ActivateOptions();
		}

		// Token: 0x04000028 RID: 40
		public const string ConsoleOut = "Console.Out";

		// Token: 0x04000029 RID: 41
		public const string ConsoleError = "Console.Error";

		// Token: 0x0400002A RID: 42
		private const string PostEventCodes = "\u001b[0m";

		// Token: 0x0400002B RID: 43
		private bool m_writeToErrorStream;

		// Token: 0x0400002C RID: 44
		private LevelMapping m_levelMapping = new LevelMapping();

		// Token: 0x0200000B RID: 11
		[Flags]
		public enum AnsiAttributes
		{
			// Token: 0x0400002E RID: 46
			Bright = 1,
			// Token: 0x0400002F RID: 47
			Dim = 2,
			// Token: 0x04000030 RID: 48
			Underscore = 4,
			// Token: 0x04000031 RID: 49
			Blink = 8,
			// Token: 0x04000032 RID: 50
			Reverse = 16,
			// Token: 0x04000033 RID: 51
			Hidden = 32,
			// Token: 0x04000034 RID: 52
			Strikethrough = 64,
			// Token: 0x04000035 RID: 53
			Light = 128
		}

		// Token: 0x0200000C RID: 12
		public enum AnsiColor
		{
			// Token: 0x04000037 RID: 55
			Black,
			// Token: 0x04000038 RID: 56
			Red,
			// Token: 0x04000039 RID: 57
			Green,
			// Token: 0x0400003A RID: 58
			Yellow,
			// Token: 0x0400003B RID: 59
			Blue,
			// Token: 0x0400003C RID: 60
			Magenta,
			// Token: 0x0400003D RID: 61
			Cyan,
			// Token: 0x0400003E RID: 62
			White
		}

		// Token: 0x0200000E RID: 14
		public class LevelColors : LevelMappingEntry
		{
			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000075 RID: 117 RVA: 0x00003346 File Offset: 0x00001546
			// (set) Token: 0x06000076 RID: 118 RVA: 0x0000334E File Offset: 0x0000154E
			public AnsiColorTerminalAppender.AnsiColor ForeColor
			{
				get
				{
					return this.m_foreColor;
				}
				set
				{
					this.m_foreColor = value;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000077 RID: 119 RVA: 0x00003357 File Offset: 0x00001557
			// (set) Token: 0x06000078 RID: 120 RVA: 0x0000335F File Offset: 0x0000155F
			public AnsiColorTerminalAppender.AnsiColor BackColor
			{
				get
				{
					return this.m_backColor;
				}
				set
				{
					this.m_backColor = value;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000079 RID: 121 RVA: 0x00003368 File Offset: 0x00001568
			// (set) Token: 0x0600007A RID: 122 RVA: 0x00003370 File Offset: 0x00001570
			public AnsiColorTerminalAppender.AnsiAttributes Attributes
			{
				get
				{
					return this.m_attributes;
				}
				set
				{
					this.m_attributes = value;
				}
			}

			// Token: 0x0600007B RID: 123 RVA: 0x0000337C File Offset: 0x0000157C
			public override void ActivateOptions()
			{
				base.ActivateOptions();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("\u001b[0;");
				int num = ((this.m_attributes & AnsiColorTerminalAppender.AnsiAttributes.Light) > (AnsiColorTerminalAppender.AnsiAttributes)0) ? 60 : 0;
				stringBuilder.Append((int)(30 + num + this.m_foreColor));
				stringBuilder.Append(';');
				stringBuilder.Append((int)(40 + num + this.m_backColor));
				if ((this.m_attributes & AnsiColorTerminalAppender.AnsiAttributes.Bright) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";1");
				}
				if ((this.m_attributes & AnsiColorTerminalAppender.AnsiAttributes.Dim) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";2");
				}
				if ((this.m_attributes & AnsiColorTerminalAppender.AnsiAttributes.Underscore) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";4");
				}
				if ((this.m_attributes & AnsiColorTerminalAppender.AnsiAttributes.Blink) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";5");
				}
				if ((this.m_attributes & AnsiColorTerminalAppender.AnsiAttributes.Reverse) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";7");
				}
				if ((this.m_attributes & AnsiColorTerminalAppender.AnsiAttributes.Hidden) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";8");
				}
				if ((this.m_attributes & AnsiColorTerminalAppender.AnsiAttributes.Strikethrough) > (AnsiColorTerminalAppender.AnsiAttributes)0)
				{
					stringBuilder.Append(";9");
				}
				stringBuilder.Append('m');
				this.m_combinedColor = stringBuilder.ToString();
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600007C RID: 124 RVA: 0x0000349C File Offset: 0x0000169C
			internal string CombinedColor
			{
				get
				{
					return this.m_combinedColor;
				}
			}

			// Token: 0x04000040 RID: 64
			private AnsiColorTerminalAppender.AnsiColor m_foreColor;

			// Token: 0x04000041 RID: 65
			private AnsiColorTerminalAppender.AnsiColor m_backColor;

			// Token: 0x04000042 RID: 66
			private AnsiColorTerminalAppender.AnsiAttributes m_attributes;

			// Token: 0x04000043 RID: 67
			private string m_combinedColor = "";
		}
	}
}
