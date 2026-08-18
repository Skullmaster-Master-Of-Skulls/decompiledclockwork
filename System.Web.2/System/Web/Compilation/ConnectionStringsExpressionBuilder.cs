using System;
using System.CodeDom;
using System.Configuration;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000836 RID: 2102
	[ExpressionPrefix("ConnectionStrings")]
	[ExpressionEditor("System.Web.UI.Design.ConnectionStringsExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ConnectionStringsExpressionBuilder : ExpressionBuilder
	{
		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x06006444 RID: 25668 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x0015FF30 File Offset: 0x0015E130
		public override object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
		{
			string x = string.Empty;
			bool flag = true;
			if (expression != null)
			{
				if (expression.EndsWith(".connectionstring", StringComparison.OrdinalIgnoreCase))
				{
					x = expression.Substring(0, expression.Length - ".connectionstring".Length);
				}
				else if (expression.EndsWith(".providername", StringComparison.OrdinalIgnoreCase))
				{
					flag = false;
					x = expression.Substring(0, expression.Length - ".providername".Length);
				}
				else
				{
					x = expression;
				}
			}
			return new Pair(x, flag);
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x0015FFAC File Offset: 0x0015E1AC
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			Pair pair = (Pair)parsedData;
			string value = (string)pair.First;
			bool flag = (bool)pair.Second;
			if (flag)
			{
				return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(base.GetType()), "GetConnectionString", new CodeExpression[]
				{
					new CodePrimitiveExpression(value)
				});
			}
			return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(base.GetType()), "GetConnectionStringProviderName", new CodeExpression[]
			{
				new CodePrimitiveExpression(value)
			});
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x00160024 File Offset: 0x0015E224
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			Pair pair = (Pair)parsedData;
			string text = (string)pair.First;
			bool flag = (bool)pair.Second;
			ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[text];
			if (flag)
			{
				return ConnectionStringsExpressionBuilder.GetConnectionString(text);
			}
			return ConnectionStringsExpressionBuilder.GetConnectionStringProviderName(text);
		}

		// Token: 0x06006448 RID: 25672 RVA: 0x0016006C File Offset: 0x0015E26C
		public static string GetConnectionStringProviderName(string connectionStringName)
		{
			ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
			if (connectionStringSettings == null)
			{
				throw new InvalidOperationException(SR.GetString("Connection_string_not_found", new object[]
				{
					connectionStringName
				}));
			}
			return connectionStringSettings.ProviderName;
		}

		// Token: 0x06006449 RID: 25673 RVA: 0x001600A8 File Offset: 0x0015E2A8
		public static string GetConnectionString(string connectionStringName)
		{
			ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
			if (connectionStringSettings == null)
			{
				throw new InvalidOperationException(SR.GetString("Connection_string_not_found", new object[]
				{
					connectionStringName
				}));
			}
			return connectionStringSettings.ConnectionString;
		}
	}
}
