using System;
using System.CodeDom;
using System.ComponentModel;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200085F RID: 2143
	[ExpressionPrefix("Routes")]
	[ExpressionEditor("System.Web.UI.Design.RouteValueExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class RouteValueExpressionBuilder : ExpressionBuilder
	{
		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x06006556 RID: 25942 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x00164B08 File Offset: 0x00162D08
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(base.GetType()), "GetRouteValue", new CodeExpression[]
			{
				new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Page"),
				new CodePrimitiveExpression(entry.Expression.Trim()),
				new CodeTypeOfExpression(new CodeTypeReference(entry.ControlType)),
				new CodePrimitiveExpression(entry.Name)
			});
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x00164B78 File Offset: 0x00162D78
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			if (!(target is Control))
			{
				return null;
			}
			return RouteValueExpressionBuilder.GetRouteValue(context.TemplateControl.Page, entry.Expression.Trim(), entry.ControlType, entry.Name);
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x00164BBC File Offset: 0x00162DBC
		internal static object ConvertRouteValue(object value, Type controlType, string propertyName)
		{
			if (controlType != null && !string.IsNullOrEmpty(propertyName))
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(controlType)[propertyName];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType != typeof(string))
				{
					TypeConverter converter = propertyDescriptor.Converter;
					if (converter.CanConvertFrom(typeof(string)))
					{
						return converter.ConvertFrom(value);
					}
				}
			}
			return value;
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x00164C23 File Offset: 0x00162E23
		public static object GetRouteValue(Page page, string key, Type controlType, string propertyName)
		{
			if (page == null || string.IsNullOrEmpty(key) || page.RouteData == null)
			{
				return null;
			}
			return RouteValueExpressionBuilder.ConvertRouteValue(page.RouteData.Values[key], controlType, propertyName);
		}
	}
}
