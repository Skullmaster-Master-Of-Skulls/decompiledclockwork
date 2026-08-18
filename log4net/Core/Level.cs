using System;

namespace log4net.Core
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public sealed class Level : IComparable
	{
		// Token: 0x0600031E RID: 798 RVA: 0x0000B3A3 File Offset: 0x000095A3
		public Level(int level, string levelName, string displayName)
		{
			if (levelName == null)
			{
				throw new ArgumentNullException("levelName");
			}
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			this.m_levelValue = level;
			this.m_levelName = string.Intern(levelName);
			this.m_levelDisplayName = displayName;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B3E1 File Offset: 0x000095E1
		public Level(int level, string levelName) : this(level, levelName, levelName)
		{
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000B3EC File Offset: 0x000095EC
		public string Name
		{
			get
			{
				return this.m_levelName;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000B3F4 File Offset: 0x000095F4
		public int Value
		{
			get
			{
				return this.m_levelValue;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B3FC File Offset: 0x000095FC
		public string DisplayName
		{
			get
			{
				return this.m_levelDisplayName;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000B404 File Offset: 0x00009604
		public override string ToString()
		{
			return this.m_levelName;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000B40C File Offset: 0x0000960C
		public override bool Equals(object o)
		{
			Level level = o as Level;
			if (level != null)
			{
				return this.m_levelValue == level.m_levelValue;
			}
			return base.Equals(o);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000B43F File Offset: 0x0000963F
		public override int GetHashCode()
		{
			return this.m_levelValue;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000B448 File Offset: 0x00009648
		public int CompareTo(object r)
		{
			Level level = r as Level;
			if (level != null)
			{
				return Level.Compare(this, level);
			}
			throw new ArgumentException("Parameter: r, Value: [" + r + "] is not an instance of Level");
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000B482 File Offset: 0x00009682
		public static bool operator >(Level l, Level r)
		{
			return l.m_levelValue > r.m_levelValue;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000B492 File Offset: 0x00009692
		public static bool operator <(Level l, Level r)
		{
			return l.m_levelValue < r.m_levelValue;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B4A2 File Offset: 0x000096A2
		public static bool operator >=(Level l, Level r)
		{
			return l.m_levelValue >= r.m_levelValue;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B4B5 File Offset: 0x000096B5
		public static bool operator <=(Level l, Level r)
		{
			return l.m_levelValue <= r.m_levelValue;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B4C8 File Offset: 0x000096C8
		public static bool operator ==(Level l, Level r)
		{
			if (l != null && r != null)
			{
				return l.m_levelValue == r.m_levelValue;
			}
			return l == r;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000B4E3 File Offset: 0x000096E3
		public static bool operator !=(Level l, Level r)
		{
			return !(l == r);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000B4F0 File Offset: 0x000096F0
		public static int Compare(Level l, Level r)
		{
			if (l == r)
			{
				return 0;
			}
			if (l == null && r == null)
			{
				return 0;
			}
			if (l == null)
			{
				return -1;
			}
			if (r == null)
			{
				return 1;
			}
			return l.m_levelValue.CompareTo(r.m_levelValue);
		}

		// Token: 0x0400016F RID: 367
		public static readonly Level Off = new Level(int.MaxValue, "OFF");

		// Token: 0x04000170 RID: 368
		public static readonly Level Log4Net_Debug = new Level(120000, "log4net:DEBUG");

		// Token: 0x04000171 RID: 369
		public static readonly Level Emergency = new Level(120000, "EMERGENCY");

		// Token: 0x04000172 RID: 370
		public static readonly Level Fatal = new Level(110000, "FATAL");

		// Token: 0x04000173 RID: 371
		public static readonly Level Alert = new Level(100000, "ALERT");

		// Token: 0x04000174 RID: 372
		public static readonly Level Critical = new Level(90000, "CRITICAL");

		// Token: 0x04000175 RID: 373
		public static readonly Level Severe = new Level(80000, "SEVERE");

		// Token: 0x04000176 RID: 374
		public static readonly Level Error = new Level(70000, "ERROR");

		// Token: 0x04000177 RID: 375
		public static readonly Level Warn = new Level(60000, "WARN");

		// Token: 0x04000178 RID: 376
		public static readonly Level Notice = new Level(50000, "NOTICE");

		// Token: 0x04000179 RID: 377
		public static readonly Level Info = new Level(40000, "INFO");

		// Token: 0x0400017A RID: 378
		public static readonly Level Debug = new Level(30000, "DEBUG");

		// Token: 0x0400017B RID: 379
		public static readonly Level Fine = new Level(30000, "FINE");

		// Token: 0x0400017C RID: 380
		public static readonly Level Trace = new Level(20000, "TRACE");

		// Token: 0x0400017D RID: 381
		public static readonly Level Finer = new Level(20000, "FINER");

		// Token: 0x0400017E RID: 382
		public static readonly Level Verbose = new Level(10000, "VERBOSE");

		// Token: 0x0400017F RID: 383
		public static readonly Level Finest = new Level(10000, "FINEST");

		// Token: 0x04000180 RID: 384
		public static readonly Level All = new Level(int.MinValue, "ALL");

		// Token: 0x04000181 RID: 385
		private readonly int m_levelValue;

		// Token: 0x04000182 RID: 386
		private readonly string m_levelName;

		// Token: 0x04000183 RID: 387
		private readonly string m_levelDisplayName;
	}
}
