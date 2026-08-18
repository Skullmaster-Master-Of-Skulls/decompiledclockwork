using System;

namespace System.Web.UI
{
	// Token: 0x02000326 RID: 806
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
	public sealed class VerificationAttribute : Attribute
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x0007C8BC File Offset: 0x0007AABC
		public VerificationAttribute(string guideline, string checkpoint, VerificationReportLevel reportLevel, int priority, string message) : this(guideline, checkpoint, reportLevel, priority, message, VerificationRule.Required, string.Empty, VerificationConditionalOperator.Equals, string.Empty, string.Empty)
		{
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0007C8E8 File Offset: 0x0007AAE8
		public VerificationAttribute(string guideline, string checkpoint, VerificationReportLevel reportLevel, int priority, string message, VerificationRule rule, string conditionalProperty) : this(guideline, checkpoint, reportLevel, priority, message, rule, conditionalProperty, VerificationConditionalOperator.NotEquals, string.Empty, string.Empty)
		{
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0007C914 File Offset: 0x0007AB14
		internal VerificationAttribute(string guideline, string checkpoint, VerificationReportLevel reportLevel, int priority, string message, VerificationRule rule, string conditionalProperty, VerificationConditionalOperator conditionalOperator, string conditionalValue) : this(guideline, checkpoint, reportLevel, priority, message, rule, conditionalProperty, conditionalOperator, conditionalValue, string.Empty)
		{
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0007C93C File Offset: 0x0007AB3C
		public VerificationAttribute(string guideline, string checkpoint, VerificationReportLevel reportLevel, int priority, string message, VerificationRule rule, string conditionalProperty, VerificationConditionalOperator conditionalOperator, string conditionalValue, string guidelineUrl)
		{
			this._guideline = guideline;
			this._checkpoint = checkpoint;
			this._reportLevel = reportLevel;
			this._priority = priority;
			this._message = message;
			this._rule = rule;
			this._conditionalProperty = conditionalProperty;
			this._conditionalOperator = conditionalOperator;
			this._conditionalValue = conditionalValue;
			this._guidelineUrl = guidelineUrl;
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00049A60 File Offset: 0x00047C60
		private VerificationAttribute()
		{
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x0007C99C File Offset: 0x0007AB9C
		public string Guideline
		{
			get
			{
				return this._guideline;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x0007C9A4 File Offset: 0x0007ABA4
		public string Checkpoint
		{
			get
			{
				return this._checkpoint;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x0007C9AC File Offset: 0x0007ABAC
		public VerificationReportLevel VerificationReportLevel
		{
			get
			{
				return this._reportLevel;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x0007C9B4 File Offset: 0x0007ABB4
		public int Priority
		{
			get
			{
				return this._priority;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x0007C9BC File Offset: 0x0007ABBC
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x0007C9C4 File Offset: 0x0007ABC4
		public VerificationRule VerificationRule
		{
			get
			{
				return this._rule;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x0007C9CC File Offset: 0x0007ABCC
		public string ConditionalProperty
		{
			get
			{
				return this._conditionalProperty;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x0007C9D4 File Offset: 0x0007ABD4
		public VerificationConditionalOperator VerificationConditionalOperator
		{
			get
			{
				return this._conditionalOperator;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x0007C9DC File Offset: 0x0007ABDC
		public string ConditionalValue
		{
			get
			{
				return this._conditionalValue;
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x0007C9E4 File Offset: 0x0007ABE4
		public string GuidelineUrl
		{
			get
			{
				return this._guidelineUrl;
			}
		}

		// Token: 0x04001D7D RID: 7549
		private string _guideline;

		// Token: 0x04001D7E RID: 7550
		private string _checkpoint;

		// Token: 0x04001D7F RID: 7551
		private VerificationReportLevel _reportLevel;

		// Token: 0x04001D80 RID: 7552
		private int _priority;

		// Token: 0x04001D81 RID: 7553
		private string _message;

		// Token: 0x04001D82 RID: 7554
		private VerificationRule _rule;

		// Token: 0x04001D83 RID: 7555
		private string _conditionalProperty;

		// Token: 0x04001D84 RID: 7556
		private VerificationConditionalOperator _conditionalOperator;

		// Token: 0x04001D85 RID: 7557
		private string _conditionalValue;

		// Token: 0x04001D86 RID: 7558
		private string _guidelineUrl;
	}
}
