using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using NLog.Filters;
using NLog.Targets;

namespace NLog.Config
{
	// Token: 0x02000051 RID: 81
	[NLogConfigurationItem]
	public class LoggingRule
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00006173 File Offset: 0x00004373
		public LoggingRule()
		{
			this.Filters = new List<Filter>();
			this.ChildRules = new List<LoggingRule>();
			this.Targets = new List<Target>();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000061B3 File Offset: 0x000043B3
		public LoggingRule(string loggerNamePattern, LogLevel minLevel, LogLevel maxLevel, Target target) : this()
		{
			this.LoggerNamePattern = loggerNamePattern;
			this.Targets.Add(target);
			this.EnableLoggingForLevels(minLevel, maxLevel);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000061D7 File Offset: 0x000043D7
		public LoggingRule(string loggerNamePattern, LogLevel minLevel, Target target) : this()
		{
			this.LoggerNamePattern = loggerNamePattern;
			this.Targets.Add(target);
			this.EnableLoggingForLevels(minLevel, LogLevel.MaxLevel);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000061FE File Offset: 0x000043FE
		public LoggingRule(string loggerNamePattern, Target target) : this()
		{
			this.LoggerNamePattern = loggerNamePattern;
			this.Targets.Add(target);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006219 File Offset: 0x00004419
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00006221 File Offset: 0x00004421
		public IList<Target> Targets { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000622A File Offset: 0x0000442A
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00006232 File Offset: 0x00004432
		public IList<LoggingRule> ChildRules { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000623B File Offset: 0x0000443B
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00006243 File Offset: 0x00004443
		public IList<Filter> Filters { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000624C File Offset: 0x0000444C
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00006254 File Offset: 0x00004454
		public bool Final { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000625D File Offset: 0x0000445D
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00006268 File Offset: 0x00004468
		public string LoggerNamePattern
		{
			get
			{
				return this.loggerNamePattern;
			}
			set
			{
				this.loggerNamePattern = value;
				int num = this.loggerNamePattern.IndexOf('*');
				int num2 = this.loggerNamePattern.LastIndexOf('*');
				if (num < 0)
				{
					this.loggerNameMatchMode = LoggingRule.MatchMode.Equals;
					this.loggerNameMatchArgument = value;
					return;
				}
				if (num == num2)
				{
					string text = this.LoggerNamePattern.Substring(0, num);
					string text2 = this.LoggerNamePattern.Substring(num + 1);
					if (text.Length > 0)
					{
						this.loggerNameMatchMode = LoggingRule.MatchMode.StartsWith;
						this.loggerNameMatchArgument = text;
						return;
					}
					if (text2.Length > 0)
					{
						this.loggerNameMatchMode = LoggingRule.MatchMode.EndsWith;
						this.loggerNameMatchArgument = text2;
					}
					return;
				}
				else
				{
					if (num == 0 && num2 == this.LoggerNamePattern.Length - 1)
					{
						string text3 = this.LoggerNamePattern.Substring(1, this.LoggerNamePattern.Length - 2);
						this.loggerNameMatchMode = LoggingRule.MatchMode.Contains;
						this.loggerNameMatchArgument = text3;
						return;
					}
					this.loggerNameMatchMode = LoggingRule.MatchMode.None;
					this.loggerNameMatchArgument = string.Empty;
					return;
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000634C File Offset: 0x0000454C
		public ReadOnlyCollection<LogLevel> Levels
		{
			get
			{
				List<LogLevel> list = new List<LogLevel>();
				for (int i = LogLevel.MinLevel.Ordinal; i <= LogLevel.MaxLevel.Ordinal; i++)
				{
					if (this.logLevels[i])
					{
						list.Add(LogLevel.FromOrdinal(i));
					}
				}
				return list.AsReadOnly();
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006399 File Offset: 0x00004599
		public void EnableLoggingForLevel(LogLevel level)
		{
			if (level == LogLevel.Off)
			{
				return;
			}
			this.logLevels[level.Ordinal] = true;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000063B8 File Offset: 0x000045B8
		public void EnableLoggingForLevels(LogLevel minLevel, LogLevel maxLevel)
		{
			for (int i = minLevel.Ordinal; i <= maxLevel.Ordinal; i++)
			{
				this.EnableLoggingForLevel(LogLevel.FromOrdinal(i));
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000063E7 File Offset: 0x000045E7
		public void DisableLoggingForLevel(LogLevel level)
		{
			if (level == LogLevel.Off)
			{
				return;
			}
			this.logLevels[level.Ordinal] = false;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006408 File Offset: 0x00004608
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "logNamePattern: ({0}:{1})", new object[]
			{
				this.loggerNameMatchArgument,
				this.loggerNameMatchMode
			});
			stringBuilder.Append(" levels: [ ");
			for (int i = 0; i < this.logLevels.Length; i++)
			{
				if (this.logLevels[i])
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} ", new object[]
					{
						LogLevel.FromOrdinal(i).ToString()
					});
				}
			}
			stringBuilder.Append("] appendTo: [ ");
			foreach (Target target in this.Targets)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0} ", new object[]
				{
					target.Name
				});
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006520 File Offset: 0x00004720
		public bool IsLoggingEnabledForLevel(LogLevel level)
		{
			return !(level == LogLevel.Off) && this.logLevels[level.Ordinal];
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006540 File Offset: 0x00004740
		public bool NameMatches(string loggerName)
		{
			switch (this.loggerNameMatchMode)
			{
			case LoggingRule.MatchMode.All:
				return true;
			case LoggingRule.MatchMode.Equals:
				return loggerName.Equals(this.loggerNameMatchArgument, StringComparison.Ordinal);
			case LoggingRule.MatchMode.StartsWith:
				return loggerName.StartsWith(this.loggerNameMatchArgument, StringComparison.Ordinal);
			case LoggingRule.MatchMode.EndsWith:
				return loggerName.EndsWith(this.loggerNameMatchArgument, StringComparison.Ordinal);
			case LoggingRule.MatchMode.Contains:
				return loggerName.IndexOf(this.loggerNameMatchArgument, StringComparison.Ordinal) >= 0;
			}
			return false;
		}

		// Token: 0x04000098 RID: 152
		private readonly bool[] logLevels = new bool[LogLevel.MaxLevel.Ordinal + 1];

		// Token: 0x04000099 RID: 153
		private string loggerNamePattern;

		// Token: 0x0400009A RID: 154
		private LoggingRule.MatchMode loggerNameMatchMode;

		// Token: 0x0400009B RID: 155
		private string loggerNameMatchArgument;

		// Token: 0x02000052 RID: 82
		internal enum MatchMode
		{
			// Token: 0x040000A1 RID: 161
			All,
			// Token: 0x040000A2 RID: 162
			None,
			// Token: 0x040000A3 RID: 163
			Equals,
			// Token: 0x040000A4 RID: 164
			StartsWith,
			// Token: 0x040000A5 RID: 165
			EndsWith,
			// Token: 0x040000A6 RID: 166
			Contains
		}
	}
}
