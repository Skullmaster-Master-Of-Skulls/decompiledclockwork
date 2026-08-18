using System;
using System.CodeDom;
using System.Web.Routing;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200085E RID: 2142
	[ExpressionPrefix("Routes")]
	[ExpressionEditor("System.Web.UI.Design.RouteUrlExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class RouteUrlExpressionBuilder : ExpressionBuilder
	{
		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x06006550 RID: 25936 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x001649CD File Offset: 0x00162BCD
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(base.GetType()), "GetRouteUrl", new CodeExpression[]
			{
				new CodeThisReferenceExpression(),
				new CodePrimitiveExpression(entry.Expression.Trim())
			});
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x00164A05 File Offset: 0x00162C05
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return RouteUrlExpressionBuilder.GetRouteUrl(context.TemplateControl, entry.Expression.Trim());
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x00164A20 File Offset: 0x00162C20
		public static bool TryParseRouteExpression(string expression, RouteValueDictionary routeValues, out string routeName)
		{
			routeName = null;
			if (string.IsNullOrEmpty(expression))
			{
				return false;
			}
			string[] array = expression.Split(new char[]
			{
				','
			});
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					'='
				});
				if (array3.Length != 2)
				{
					return false;
				}
				string text2 = array3[0].Trim();
				string text3 = array3[1].Trim();
				if (string.IsNullOrEmpty(text2))
				{
					return false;
				}
				if (text2.Equals("RouteName", StringComparison.OrdinalIgnoreCase))
				{
					routeName = text3;
				}
				else
				{
					routeValues[text2] = text3;
				}
			}
			return true;
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x00164AC0 File Offset: 0x00162CC0
		public static string GetRouteUrl(Control control, string expression)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			string routeName = null;
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			if (RouteUrlExpressionBuilder.TryParseRouteExpression(expression, routeValueDictionary, out routeName))
			{
				return control.GetRouteUrl(routeName, routeValueDictionary);
			}
			throw new InvalidOperationException(SR.GetString("RouteUrlExpression_InvalidExpression"));
		}
	}
}
