using System;
using System.CodeDom;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x020007F4 RID: 2036
	[ExpressionPrefix("AppSettings")]
	[ExpressionEditor("System.Web.UI.Design.AppSettingsExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class AppSettingsExpressionBuilder : ExpressionBuilder
	{
		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x060060FC RID: 24828 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x0014E7DC File Offset: 0x0014C9DC
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			if (entry.DeclaringType == null || entry.PropertyInfo == null)
			{
				return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(base.GetType()), "GetAppSetting", new CodeExpression[]
				{
					new CodePrimitiveExpression(entry.Expression.Trim())
				});
			}
			return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(base.GetType()), "GetAppSetting", new CodeExpression[]
			{
				new CodePrimitiveExpression(entry.Expression.Trim()),
				new CodeTypeOfExpression(entry.DeclaringType),
				new CodePrimitiveExpression(entry.PropertyInfo.Name)
			});
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x0014E883 File Offset: 0x0014CA83
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return AppSettingsExpressionBuilder.GetAppSetting(entry.Expression, target.GetType(), entry.PropertyInfo.Name);
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x0014E8A4 File Offset: 0x0014CAA4
		public static object GetAppSetting(string key)
		{
			string text = ConfigurationManager.AppSettings[key];
			if (text == null)
			{
				throw new InvalidOperationException(SR.GetString("AppSetting_not_found", new object[]
				{
					key
				}));
			}
			return text;
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x0014E8DC File Offset: 0x0014CADC
		public static object GetAppSetting(string key, Type targetType, string propertyName)
		{
			string text = ConfigurationManager.AppSettings[key];
			if (targetType != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(targetType)[propertyName];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType != typeof(string))
				{
					TypeConverter converter = propertyDescriptor.Converter;
					if (converter.CanConvertFrom(typeof(string)))
					{
						return converter.ConvertFrom(text);
					}
					throw new InvalidOperationException(SR.GetString("AppSetting_not_convertible", new object[]
					{
						text,
						propertyDescriptor.PropertyType.Name,
						propertyDescriptor.Name
					}));
				}
			}
			if (text == null)
			{
				throw new InvalidOperationException(SR.GetString("AppSetting_not_found", new object[]
				{
					key
				}));
			}
			return text;
		}
	}
}
