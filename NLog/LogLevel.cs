using System;
using System.Collections.Generic;
using NLog.Internal;

namespace NLog
{
	// Token: 0x02000125 RID: 293
	public sealed class LogLevel : IComparable, IEquatable<LogLevel>
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00018506 File Offset: 0x00016706
		public static IEnumerable<LogLevel> AllLevels
		{
			get
			{
				return LogLevel.allLevels;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0001850D File Offset: 0x0001670D
		public static IEnumerable<LogLevel> AllLoggingLevels
		{
			get
			{
				return LogLevel.allLoggingLevels;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00018514 File Offset: 0x00016714
		private LogLevel(string name, int ordinal)
		{
			this.name = name;
			this.ordinal = ordinal;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0001852A File Offset: 0x0001672A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00018532 File Offset: 0x00016732
		internal static LogLevel MaxLevel
		{
			get
			{
				return LogLevel.Fatal;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00018539 File Offset: 0x00016739
		internal static LogLevel MinLevel
		{
			get
			{
				return LogLevel.Trace;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00018540 File Offset: 0x00016740
		public int Ordinal
		{
			get
			{
				return this.ordinal;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00018548 File Offset: 0x00016748
		public static bool operator ==(LogLevel level1, LogLevel level2)
		{
			if (object.ReferenceEquals(level1, null))
			{
				return object.ReferenceEquals(level2, null);
			}
			return !object.ReferenceEquals(level2, null) && level1.Ordinal == level2.Ordinal;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00018574 File Offset: 0x00016774
		public static bool operator !=(LogLevel level1, LogLevel level2)
		{
			if (object.ReferenceEquals(level1, null))
			{
				return !object.ReferenceEquals(level2, null);
			}
			return object.ReferenceEquals(level2, null) || level1.Ordinal != level2.Ordinal;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000185A6 File Offset: 0x000167A6
		public static bool operator >(LogLevel level1, LogLevel level2)
		{
			ParameterUtils.AssertNotNull(level1, "level1");
			ParameterUtils.AssertNotNull(level2, "level2");
			return level1.Ordinal > level2.Ordinal;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000185CC File Offset: 0x000167CC
		public static bool operator >=(LogLevel level1, LogLevel level2)
		{
			ParameterUtils.AssertNotNull(level1, "level1");
			ParameterUtils.AssertNotNull(level2, "level2");
			return level1.Ordinal >= level2.Ordinal;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000185F5 File Offset: 0x000167F5
		public static bool operator <(LogLevel level1, LogLevel level2)
		{
			ParameterUtils.AssertNotNull(level1, "level1");
			ParameterUtils.AssertNotNull(level2, "level2");
			return level1.Ordinal < level2.Ordinal;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001861B File Offset: 0x0001681B
		public static bool operator <=(LogLevel level1, LogLevel level2)
		{
			ParameterUtils.AssertNotNull(level1, "level1");
			ParameterUtils.AssertNotNull(level2, "level2");
			return level1.Ordinal <= level2.Ordinal;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00018644 File Offset: 0x00016844
		public static LogLevel FromOrdinal(int ordinal)
		{
			switch (ordinal)
			{
			case 0:
				return LogLevel.Trace;
			case 1:
				return LogLevel.Debug;
			case 2:
				return LogLevel.Info;
			case 3:
				return LogLevel.Warn;
			case 4:
				return LogLevel.Error;
			case 5:
				return LogLevel.Fatal;
			case 6:
				return LogLevel.Off;
			default:
				throw new ArgumentException("Invalid ordinal.");
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000186AC File Offset: 0x000168AC
		public static LogLevel FromString(string levelName)
		{
			if (levelName == null)
			{
				throw new ArgumentNullException("levelName");
			}
			if (levelName.Equals("Trace", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Trace;
			}
			if (levelName.Equals("Debug", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Debug;
			}
			if (levelName.Equals("Info", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Info;
			}
			if (levelName.Equals("Warn", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Warn;
			}
			if (levelName.Equals("Error", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Error;
			}
			if (levelName.Equals("Fatal", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Fatal;
			}
			if (levelName.Equals("Off", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Off;
			}
			throw new ArgumentException("Unknown log level: " + levelName);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00018763 File Offset: 0x00016963
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001876B File Offset: 0x0001696B
		public override int GetHashCode()
		{
			return this.Ordinal;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00018774 File Offset: 0x00016974
		public override bool Equals(object obj)
		{
			LogLevel logLevel = obj as LogLevel;
			return logLevel != null && this.Ordinal == logLevel.Ordinal;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001879B File Offset: 0x0001699B
		public bool Equals(LogLevel other)
		{
			return !(other == null) && this.Ordinal == other.Ordinal;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000187B8 File Offset: 0x000169B8
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			LogLevel logLevel = (LogLevel)obj;
			return this.Ordinal - logLevel.Ordinal;
		}

		// Token: 0x04000295 RID: 661
		public static readonly LogLevel Trace = new LogLevel("Trace", 0);

		// Token: 0x04000296 RID: 662
		public static readonly LogLevel Debug = new LogLevel("Debug", 1);

		// Token: 0x04000297 RID: 663
		public static readonly LogLevel Info = new LogLevel("Info", 2);

		// Token: 0x04000298 RID: 664
		public static readonly LogLevel Warn = new LogLevel("Warn", 3);

		// Token: 0x04000299 RID: 665
		public static readonly LogLevel Error = new LogLevel("Error", 4);

		// Token: 0x0400029A RID: 666
		public static readonly LogLevel Fatal = new LogLevel("Fatal", 5);

		// Token: 0x0400029B RID: 667
		public static readonly LogLevel Off = new LogLevel("Off", 6);

		// Token: 0x0400029C RID: 668
		private static readonly IList<LogLevel> allLevels = new List<LogLevel>
		{
			LogLevel.Trace,
			LogLevel.Debug,
			LogLevel.Info,
			LogLevel.Warn,
			LogLevel.Error,
			LogLevel.Fatal,
			LogLevel.Off
		};

		// Token: 0x0400029D RID: 669
		private static readonly IList<LogLevel> allLoggingLevels = new List<LogLevel>
		{
			LogLevel.Trace,
			LogLevel.Debug,
			LogLevel.Info,
			LogLevel.Warn,
			LogLevel.Error,
			LogLevel.Fatal
		};

		// Token: 0x0400029E RID: 670
		private readonly int ordinal;

		// Token: 0x0400029F RID: 671
		private readonly string name;
	}
}
