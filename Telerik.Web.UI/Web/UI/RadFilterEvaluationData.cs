using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000F75 RID: 3957
	public class RadFilterEvaluationData
	{
		// Token: 0x17002FE1 RID: 12257
		// (get) Token: 0x0600979B RID: 38811 RVA: 0x0021FE32 File Offset: 0x0021E032
		// (set) Token: 0x0600979C RID: 38812 RVA: 0x0021FE3A File Offset: 0x0021E03A
		public RadFilterNonGroupExpression Expression { get; set; }

		// Token: 0x17002FE2 RID: 12258
		// (get) Token: 0x0600979D RID: 38813 RVA: 0x0021FE43 File Offset: 0x0021E043
		// (set) Token: 0x0600979E RID: 38814 RVA: 0x0021FE4B File Offset: 0x0021E04B
		public ArrayList Values { get; set; }

		// Token: 0x17002FE3 RID: 12259
		// (get) Token: 0x0600979F RID: 38815 RVA: 0x0021FE54 File Offset: 0x0021E054
		// (set) Token: 0x060097A0 RID: 38816 RVA: 0x0021FE5C File Offset: 0x0021E05C
		public string ExpressionFormat { get; set; }

		// Token: 0x060097A1 RID: 38817 RVA: 0x0021FE65 File Offset: 0x0021E065
		public RadFilterEvaluationData()
		{
		}

		// Token: 0x060097A2 RID: 38818 RVA: 0x0021FE6D File Offset: 0x0021E06D
		public RadFilterEvaluationData(RadFilterNonGroupExpression expression, ArrayList values, string expressionFormat)
		{
			this.Expression = expression;
			this.Values = values;
			this.ExpressionFormat = expressionFormat;
		}

		// Token: 0x060097A3 RID: 38819 RVA: 0x0021FE8A File Offset: 0x0021E08A
		public virtual void CopyTo(RadFilterEvaluationData data)
		{
			data.Expression = this.Expression;
			data.Values = this.Values;
			data.ExpressionFormat = this.ExpressionFormat;
		}

		// Token: 0x060097A4 RID: 38820 RVA: 0x0021FEB0 File Offset: 0x0021E0B0
		public virtual string Format()
		{
			return string.Format(this.ExpressionFormat, this.Values.ToArray());
		}
	}
}
