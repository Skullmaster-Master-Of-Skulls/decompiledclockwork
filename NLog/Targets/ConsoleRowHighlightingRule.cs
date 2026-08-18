using System;
using System.ComponentModel;
using NLog.Conditions;
using NLog.Config;

namespace NLog.Targets
{
	// Token: 0x0200014F RID: 335
	[NLogConfigurationItem]
	public class ConsoleRowHighlightingRule
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001BE36 File Offset: 0x0001A036
		public ConsoleRowHighlightingRule() : this(null, ConsoleOutputColor.NoChange, ConsoleOutputColor.NoChange)
		{
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001BE43 File Offset: 0x0001A043
		public ConsoleRowHighlightingRule(ConditionExpression condition, ConsoleOutputColor foregroundColor, ConsoleOutputColor backgroundColor)
		{
			this.Condition = condition;
			this.ForegroundColor = foregroundColor;
			this.BackgroundColor = backgroundColor;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0001BE60 File Offset: 0x0001A060
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0001BE67 File Offset: 0x0001A067
		public static ConsoleRowHighlightingRule Default { get; private set; } = new ConsoleRowHighlightingRule(null, ConsoleOutputColor.NoChange, ConsoleOutputColor.NoChange);

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0001BE6F File Offset: 0x0001A06F
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0001BE77 File Offset: 0x0001A077
		[RequiredParameter]
		public ConditionExpression Condition { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001BE80 File Offset: 0x0001A080
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0001BE88 File Offset: 0x0001A088
		[DefaultValue("NoChange")]
		public ConsoleOutputColor ForegroundColor { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0001BE91 File Offset: 0x0001A091
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0001BE99 File Offset: 0x0001A099
		[DefaultValue("NoChange")]
		public ConsoleOutputColor BackgroundColor { get; set; }

		// Token: 0x06000BFB RID: 3067 RVA: 0x0001BEA4 File Offset: 0x0001A0A4
		public bool CheckCondition(LogEventInfo logEvent)
		{
			return this.Condition == null || true.Equals(this.Condition.Evaluate(logEvent));
		}
	}
}
