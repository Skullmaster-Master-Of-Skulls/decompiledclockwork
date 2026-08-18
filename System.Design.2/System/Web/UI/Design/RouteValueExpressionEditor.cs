using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000067 RID: 103
	public class RouteValueExpressionEditor : ExpressionEditor
	{
		// Token: 0x0600030A RID: 778 RVA: 0x000105CC File Offset: 0x0000E7CC
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			return "RouteValue: " + expression;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000105D9 File Offset: 0x0000E7D9
		public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			return new RouteValueExpressionEditorSheet(expression, serviceProvider);
		}
	}
}
