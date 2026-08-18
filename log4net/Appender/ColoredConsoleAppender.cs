using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000017 RID: 23
	public class ColoredConsoleAppender : AppenderSkeleton
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00004090 File Offset: 0x00002290
		public ColoredConsoleAppender()
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000040A3 File Offset: 0x000022A3
		[Obsolete("Instead use the default constructor and set the Layout property")]
		public ColoredConsoleAppender(ILayout layout) : this(layout, false)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000040AD File Offset: 0x000022AD
		[Obsolete("Instead use the default constructor and set the Layout & Target properties")]
		public ColoredConsoleAppender(ILayout layout, bool writeToErrorStream)
		{
			this.Layout = layout;
			this.m_writeToErrorStream = writeToErrorStream;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000040CE File Offset: 0x000022CE
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000040E4 File Offset: 0x000022E4
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
				string strB = value.Trim();
				if (string.Compare("Console.Error", strB, true, CultureInfo.InvariantCulture) == 0)
				{
					this.m_writeToErrorStream = true;
					return;
				}
				this.m_writeToErrorStream = false;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000411A File Offset: 0x0000231A
		public void AddMapping(ColoredConsoleAppender.LevelColors mapping)
		{
			this.m_levelMapping.Add(mapping);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004128 File Offset: 0x00002328
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		protected override void Append(LoggingEvent loggingEvent)
		{
			if (this.m_consoleOutputWriter != null)
			{
				IntPtr consoleHandle = IntPtr.Zero;
				if (this.m_writeToErrorStream)
				{
					consoleHandle = ColoredConsoleAppender.GetStdHandle(4294967284U);
				}
				else
				{
					consoleHandle = ColoredConsoleAppender.GetStdHandle(4294967285U);
				}
				ushort attributes = 7;
				ColoredConsoleAppender.LevelColors levelColors = this.m_levelMapping.Lookup(loggingEvent.Level) as ColoredConsoleAppender.LevelColors;
				if (levelColors != null)
				{
					attributes = levelColors.CombinedColor;
				}
				string text = base.RenderLoggingEvent(loggingEvent);
				ColoredConsoleAppender.CONSOLE_SCREEN_BUFFER_INFO console_SCREEN_BUFFER_INFO;
				ColoredConsoleAppender.GetConsoleScreenBufferInfo(consoleHandle, out console_SCREEN_BUFFER_INFO);
				ColoredConsoleAppender.SetConsoleTextAttribute(consoleHandle, attributes);
				char[] array = text.ToCharArray();
				int num = array.Length;
				bool flag = false;
				if (num > 1 && array[num - 2] == '\r' && array[num - 1] == '\n')
				{
					num -= 2;
					flag = true;
				}
				this.m_consoleOutputWriter.Write(array, 0, num);
				ColoredConsoleAppender.SetConsoleTextAttribute(consoleHandle, console_SCREEN_BUFFER_INFO.wAttributes);
				if (flag)
				{
					this.m_consoleOutputWriter.Write(ColoredConsoleAppender.s_windowsNewline, 0, 2);
				}
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004205 File Offset: 0x00002405
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004208 File Offset: 0x00002408
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			this.m_levelMapping.ActivateOptions();
			Stream stream;
			if (this.m_writeToErrorStream)
			{
				stream = Console.OpenStandardError();
			}
			else
			{
				stream = Console.OpenStandardOutput();
			}
			Encoding encoding = Encoding.GetEncoding(ColoredConsoleAppender.GetConsoleOutputCP());
			this.m_consoleOutputWriter = new StreamWriter(stream, encoding, 256);
			this.m_consoleOutputWriter.AutoFlush = true;
			GC.SuppressFinalize(this.m_consoleOutputWriter);
		}

		// Token: 0x060000EA RID: 234
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetConsoleOutputCP();

		// Token: 0x060000EB RID: 235
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetConsoleTextAttribute(IntPtr consoleHandle, ushort attributes);

		// Token: 0x060000EC RID: 236
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool GetConsoleScreenBufferInfo(IntPtr consoleHandle, out ColoredConsoleAppender.CONSOLE_SCREEN_BUFFER_INFO bufferInfo);

		// Token: 0x060000ED RID: 237
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetStdHandle(uint type);

		// Token: 0x04000051 RID: 81
		public const string ConsoleOut = "Console.Out";

		// Token: 0x04000052 RID: 82
		public const string ConsoleError = "Console.Error";

		// Token: 0x04000053 RID: 83
		private const uint STD_OUTPUT_HANDLE = 4294967285U;

		// Token: 0x04000054 RID: 84
		private const uint STD_ERROR_HANDLE = 4294967284U;

		// Token: 0x04000055 RID: 85
		private static readonly char[] s_windowsNewline = new char[]
		{
			'\r',
			'\n'
		};

		// Token: 0x04000056 RID: 86
		private bool m_writeToErrorStream;

		// Token: 0x04000057 RID: 87
		private LevelMapping m_levelMapping = new LevelMapping();

		// Token: 0x04000058 RID: 88
		private StreamWriter m_consoleOutputWriter;

		// Token: 0x02000018 RID: 24
		[Flags]
		public enum Colors
		{
			// Token: 0x0400005A RID: 90
			Blue = 1,
			// Token: 0x0400005B RID: 91
			Green = 2,
			// Token: 0x0400005C RID: 92
			Red = 4,
			// Token: 0x0400005D RID: 93
			White = 7,
			// Token: 0x0400005E RID: 94
			Yellow = 6,
			// Token: 0x0400005F RID: 95
			Purple = 5,
			// Token: 0x04000060 RID: 96
			Cyan = 3,
			// Token: 0x04000061 RID: 97
			HighIntensity = 8
		}

		// Token: 0x02000019 RID: 25
		private struct COORD
		{
			// Token: 0x04000062 RID: 98
			public ushort x;

			// Token: 0x04000063 RID: 99
			public ushort y;
		}

		// Token: 0x0200001A RID: 26
		private struct SMALL_RECT
		{
			// Token: 0x04000064 RID: 100
			public ushort Left;

			// Token: 0x04000065 RID: 101
			public ushort Top;

			// Token: 0x04000066 RID: 102
			public ushort Right;

			// Token: 0x04000067 RID: 103
			public ushort Bottom;
		}

		// Token: 0x0200001B RID: 27
		private struct CONSOLE_SCREEN_BUFFER_INFO
		{
			// Token: 0x04000068 RID: 104
			public ColoredConsoleAppender.COORD dwSize;

			// Token: 0x04000069 RID: 105
			public ColoredConsoleAppender.COORD dwCursorPosition;

			// Token: 0x0400006A RID: 106
			public ushort wAttributes;

			// Token: 0x0400006B RID: 107
			public ColoredConsoleAppender.SMALL_RECT srWindow;

			// Token: 0x0400006C RID: 108
			public ColoredConsoleAppender.COORD dwMaximumWindowSize;
		}

		// Token: 0x0200001C RID: 28
		public class LevelColors : LevelMappingEntry
		{
			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000EF RID: 239 RVA: 0x00004298 File Offset: 0x00002498
			// (set) Token: 0x060000F0 RID: 240 RVA: 0x000042A0 File Offset: 0x000024A0
			public ColoredConsoleAppender.Colors ForeColor
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

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000F1 RID: 241 RVA: 0x000042A9 File Offset: 0x000024A9
			// (set) Token: 0x060000F2 RID: 242 RVA: 0x000042B1 File Offset: 0x000024B1
			public ColoredConsoleAppender.Colors BackColor
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

			// Token: 0x060000F3 RID: 243 RVA: 0x000042BA File Offset: 0x000024BA
			public override void ActivateOptions()
			{
				base.ActivateOptions();
				this.m_combinedColor = (ushort)(this.m_foreColor + (int)((int)this.m_backColor << 4));
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x000042D8 File Offset: 0x000024D8
			internal ushort CombinedColor
			{
				get
				{
					return this.m_combinedColor;
				}
			}

			// Token: 0x0400006D RID: 109
			private ColoredConsoleAppender.Colors m_foreColor;

			// Token: 0x0400006E RID: 110
			private ColoredConsoleAppender.Colors m_backColor;

			// Token: 0x0400006F RID: 111
			private ushort m_combinedColor;
		}
	}
}
