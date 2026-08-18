using System;
using System.Design;
using System.Web.Compilation;
using System.Web.Routing;

namespace System.Web.UI.Design
{
	// Token: 0x02000065 RID: 101
	public class RouteUrlExpressionEditor : ExpressionEditor
	{
		// Token: 0x06000300 RID: 768 RVA: 0x000103FC File Offset: 0x0000E5FC
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			string text = null;
			RouteValueDictionary routeValues = new RouteValueDictionary();
			if (RouteUrlExpressionBuilder.TryParseRouteExpression(expression, routeValues, out text))
			{
				return "RouteUrl: " + expression;
			}
			return SR.GetString("RouteUrlExpressionEditor_InvalidExpression");
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00010432 File Offset: 0x0000E632
		public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			return new RouteUrlExpressionEditorSheet(expression, serviceProvider);
		}
	}
}
