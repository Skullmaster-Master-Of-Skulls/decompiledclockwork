using System;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.Conditions
{
	// Token: 0x02000029 RID: 41
	[ThreadAgnostic]
	[NLogConfigurationItem]
	public abstract class ConditionExpression
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000033D4 File Offset: 0x000015D4
		public static implicit operator ConditionExpression(string conditionExpressionText)
		{
			return ConditionParser.ParseExpression(conditionExpressionText);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000033DC File Offset: 0x000015DC
		public object Evaluate(LogEventInfo context)
		{
			object result;
			try
			{
				result = this.EvaluateNode(context);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Exception occurred when evaluating condition");
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				throw new ConditionEvaluationException("Exception occurred when evaluating condition", ex);
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176
		public abstract override string ToString();

		// Token: 0x060000B1 RID: 177
		protected abstract object EvaluateNode(LogEventInfo context);
	}
}
