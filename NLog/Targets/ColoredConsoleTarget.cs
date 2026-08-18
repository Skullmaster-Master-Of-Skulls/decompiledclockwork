using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using NLog.Config;

namespace NLog.Targets
{
	// Token: 0x0200014C RID: 332
	[Target("ColoredConsole")]
	public sealed class ColoredConsoleTarget : TargetWithLayoutHeaderAndFooter
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x0001B8B2 File Offset: 0x00019AB2
		public ColoredConsoleTarget()
		{
			this.WordHighlightingRules = new List<ConsoleWordHighlightingRule>();
			this.RowHighlightingRules = new List<ConsoleRowHighlightingRule>();
			this.UseDefaultRowHighlightingRules = true;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0001B8D7 File Offset: 0x00019AD7
		public ColoredConsoleTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0001B8E6 File Offset: 0x00019AE6
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0001B8EE File Offset: 0x00019AEE
		[DefaultValue(false)]
		public bool ErrorStream { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0001B8F7 File Offset: 0x00019AF7
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0001B8FF File Offset: 0x00019AFF
		[DefaultValue(true)]
		public bool UseDefaultRowHighlightingRules { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0001B908 File Offset: 0x00019B08
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0001B90F File Offset: 0x00019B0F
		public Encoding Encoding
		{
			get
			{
				return Console.OutputEncoding;
			}
			set
			{
				Console.OutputEncoding = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0001B917 File Offset: 0x00019B17
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0001B91F File Offset: 0x00019B1F
		[ArrayParameter(typeof(ConsoleRowHighlightingRule), "highlight-row")]
		public IList<ConsoleRowHighlightingRule> RowHighlightingRules { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0001B928 File Offset: 0x00019B28
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0001B930 File Offset: 0x00019B30
		[ArrayParameter(typeof(ConsoleWordHighlightingRule), "highlight-word")]
		public IList<ConsoleWordHighlightingRule> WordHighlightingRules { get; private set; }

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001B93C File Offset: 0x00019B3C
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (base.Header != null)
			{
				LogEventInfo logEvent = LogEventInfo.CreateNullEvent();
				this.Output(logEvent, base.Header.Render(logEvent));
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0001B970 File Offset: 0x00019B70
		protected override void CloseTarget()
		{
			if (base.Footer != null)
			{
				LogEventInfo logEvent = LogEventInfo.CreateNullEvent();
				this.Output(logEvent, base.Footer.Render(logEvent));
			}
			base.CloseTarget();
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0001B9A4 File Offset: 0x00019BA4
		protected override void Write(LogEventInfo logEvent)
		{
			this.Output(logEvent, this.Layout.Render(logEvent));
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0001B9BC File Offset: 0x00019BBC
		private void Output(LogEventInfo logEvent, string message)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			ConsoleColor backgroundColor = Console.BackgroundColor;
			bool flag = false;
			bool flag2 = false;
			try
			{
				ConsoleRowHighlightingRule matchingRowHighlightingRule = this.GetMatchingRowHighlightingRule(logEvent);
				flag = ColoredConsoleTarget.IsColorChange(matchingRowHighlightingRule.ForegroundColor, foregroundColor);
				if (flag)
				{
					Console.ForegroundColor = (ConsoleColor)matchingRowHighlightingRule.ForegroundColor;
				}
				flag2 = ColoredConsoleTarget.IsColorChange(matchingRowHighlightingRule.BackgroundColor, backgroundColor);
				if (flag2)
				{
					Console.BackgroundColor = (ConsoleColor)matchingRowHighlightingRule.BackgroundColor;
				}
				TextWriter textWriter = this.ErrorStream ? Console.Error : Console.Out;
				if (this.WordHighlightingRules.Count == 0)
				{
					textWriter.WriteLine(message);
				}
				else
				{
					message = message.Replace("\a", "\a\a");
					foreach (ConsoleWordHighlightingRule consoleWordHighlightingRule in this.WordHighlightingRules)
					{
						message = consoleWordHighlightingRule.ReplaceWithEscapeSequences(message);
					}
					ColoredConsoleTarget.ColorizeEscapeSequences(textWriter, message, new ColoredConsoleTarget.ColorPair(Console.ForegroundColor, Console.BackgroundColor), new ColoredConsoleTarget.ColorPair(foregroundColor, backgroundColor));
					textWriter.WriteLine();
					flag2 = (flag = true);
				}
			}
			finally
			{
				if (flag)
				{
					Console.ForegroundColor = foregroundColor;
				}
				if (flag2)
				{
					Console.BackgroundColor = backgroundColor;
				}
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001BAF0 File Offset: 0x00019CF0
		private ConsoleRowHighlightingRule GetMatchingRowHighlightingRule(LogEventInfo logEvent)
		{
			foreach (ConsoleRowHighlightingRule consoleRowHighlightingRule in this.RowHighlightingRules)
			{
				if (consoleRowHighlightingRule.CheckCondition(logEvent))
				{
					return consoleRowHighlightingRule;
				}
			}
			if (this.UseDefaultRowHighlightingRules)
			{
				foreach (ConsoleRowHighlightingRule consoleRowHighlightingRule2 in ColoredConsoleTarget.defaultConsoleRowHighlightingRules)
				{
					if (consoleRowHighlightingRule2.CheckCondition(logEvent))
					{
						return consoleRowHighlightingRule2;
					}
				}
			}
			return ConsoleRowHighlightingRule.Default;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001BB98 File Offset: 0x00019D98
		private static bool IsColorChange(ConsoleOutputColor targetColor, ConsoleColor oldColor)
		{
			return targetColor != ConsoleOutputColor.NoChange && targetColor != (ConsoleOutputColor)oldColor;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		private static void ColorizeEscapeSequences(TextWriter output, string message, ColoredConsoleTarget.ColorPair startingColor, ColoredConsoleTarget.ColorPair defaultColor)
		{
			Stack<ColoredConsoleTarget.ColorPair> stack = new Stack<ColoredConsoleTarget.ColorPair>();
			stack.Push(startingColor);
			int i = 0;
			while (i < message.Length)
			{
				int num = i;
				while (num < message.Length && message[num] >= ' ')
				{
					num++;
				}
				if (num != i)
				{
					output.Write(message.Substring(i, num - i));
				}
				if (num >= message.Length)
				{
					i = num;
					break;
				}
				char c = message[num];
				char c2 = '\0';
				if (num + 1 < message.Length)
				{
					c2 = message[num + 1];
				}
				if (c == '\a' && c2 == '\a')
				{
					output.Write('\a');
					i = num + 2;
				}
				else if (c == '\r' || c == '\n')
				{
					Console.ForegroundColor = defaultColor.ForegroundColor;
					Console.BackgroundColor = defaultColor.BackgroundColor;
					output.Write(c);
					Console.ForegroundColor = stack.Peek().ForegroundColor;
					Console.BackgroundColor = stack.Peek().BackgroundColor;
					i = num + 1;
				}
				else if (c == '\a')
				{
					if (c2 == 'X')
					{
						stack.Pop();
						Console.ForegroundColor = stack.Peek().ForegroundColor;
						Console.BackgroundColor = stack.Peek().BackgroundColor;
						i = num + 2;
					}
					else
					{
						ConsoleOutputColor consoleOutputColor = (ConsoleOutputColor)(c2 - 'A');
						ConsoleOutputColor consoleOutputColor2 = (ConsoleOutputColor)(message[num + 2] - 'A');
						if (consoleOutputColor != ConsoleOutputColor.NoChange)
						{
							Console.ForegroundColor = (ConsoleColor)consoleOutputColor;
						}
						if (consoleOutputColor2 != ConsoleOutputColor.NoChange)
						{
							Console.BackgroundColor = (ConsoleColor)consoleOutputColor2;
						}
						stack.Push(new ColoredConsoleTarget.ColorPair(Console.ForegroundColor, Console.BackgroundColor));
						i = num + 3;
					}
				}
				else
				{
					output.Write(c);
					i = num + 1;
				}
			}
			if (i < message.Length)
			{
				output.Write(message.Substring(i));
			}
		}

		// Token: 0x040002E2 RID: 738
		private static readonly IList<ConsoleRowHighlightingRule> defaultConsoleRowHighlightingRules = new List<ConsoleRowHighlightingRule>
		{
			new ConsoleRowHighlightingRule("level == LogLevel.Fatal", ConsoleOutputColor.Red, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Error", ConsoleOutputColor.Yellow, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Warn", ConsoleOutputColor.Magenta, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Info", ConsoleOutputColor.White, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Debug", ConsoleOutputColor.Gray, ConsoleOutputColor.NoChange),
			new ConsoleRowHighlightingRule("level == LogLevel.Trace", ConsoleOutputColor.DarkGray, ConsoleOutputColor.NoChange)
		};

		// Token: 0x0200014D RID: 333
		internal struct ColorPair
		{
			// Token: 0x06000BED RID: 3053 RVA: 0x0001BE05 File Offset: 0x0001A005
			internal ColorPair(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
			{
				this.foregroundColor = foregroundColor;
				this.backgroundColor = backgroundColor;
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0001BE15 File Offset: 0x0001A015
			internal ConsoleColor BackgroundColor
			{
				get
				{
					return this.backgroundColor;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0001BE1D File Offset: 0x0001A01D
			internal ConsoleColor ForegroundColor
			{
				get
				{
					return this.foregroundColor;
				}
			}

			// Token: 0x040002E7 RID: 743
			private readonly ConsoleColor foregroundColor;

			// Token: 0x040002E8 RID: 744
			private readonly ConsoleColor backgroundColor;
		}
	}
}
