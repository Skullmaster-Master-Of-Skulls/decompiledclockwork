using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000F6F RID: 3951
	public abstract class RadFilterExpressionEvaluatorBase
	{
		// Token: 0x17002FD6 RID: 12246
		// (get) Token: 0x0600976C RID: 38764 RVA: 0x0021F572 File Offset: 0x0021D772
		// (set) Token: 0x0600976D RID: 38765 RVA: 0x0021F57A File Offset: 0x0021D77A
		public bool IsCaseSensitive { get; set; }

		// Token: 0x0600976E RID: 38766 RVA: 0x0021F584 File Offset: 0x0021D784
		public virtual string Evaluate(RadFilterNonGroupExpression expression)
		{
			RadFilterEvaluationData evaluationData = this.GetEvaluationData(expression);
			if (this.OnExpressionEvaluated != null)
			{
				this.OnExpressionEvaluated(evaluationData);
			}
			return this.FormatEvaluationData(evaluationData);
		}

		// Token: 0x0600976F RID: 38767 RVA: 0x0021F5B4 File Offset: 0x0021D7B4
		public virtual RadFilterEvaluationData GetEvaluationData(RadFilterNonGroupExpression expression)
		{
			return new RadFilterEvaluationData(expression, new ArrayList(), string.Empty);
		}

		// Token: 0x06009770 RID: 38768 RVA: 0x0021F5C6 File Offset: 0x0021D7C6
		protected virtual string FormatEvaluationData(RadFilterEvaluationData evaluationData)
		{
			return evaluationData.Format();
		}

		// Token: 0x17002FD7 RID: 12247
		// (get) Token: 0x06009771 RID: 38769 RVA: 0x0021F5CE File Offset: 0x0021D7CE
		// (set) Token: 0x06009772 RID: 38770 RVA: 0x0021F5D6 File Offset: 0x0021D7D6
		public Action<RadFilterEvaluationData> OnExpressionEvaluated { get; set; }
	}
}
