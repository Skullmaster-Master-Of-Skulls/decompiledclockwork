using System;
using System.CodeDom;
using System.Configuration;

namespace System.Web.Services.Description
{
	// Token: 0x020000DA RID: 218
	internal class ProtocolImporterUtil
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0001C119 File Offset: 0x0001B119
		private ProtocolImporterUtil()
		{
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001C124 File Offset: 0x0001B124
		internal static void GenerateConstructorStatements(CodeConstructor ctor, string url, string appSettingUrlKey, string appSettingBaseUrl, bool soap11)
		{
			bool flag = url != null && url.Length > 0;
			bool flag2 = appSettingUrlKey != null && appSettingUrlKey.Length > 0;
			CodeAssignStatement codeAssignStatement = null;
			if (!flag && !flag2)
			{
				return;
			}
			CodePropertyReferenceExpression left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Url");
			if (flag)
			{
				CodeExpression codeExpression = new CodePrimitiveExpression(url);
				codeAssignStatement = new CodeAssignStatement(left, codeExpression);
			}
			if (flag && !flag2)
			{
				ctor.Statements.Add(codeAssignStatement);
				return;
			}
			if (flag2)
			{
				CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("urlSetting");
				CodeTypeReferenceExpression targetObject = new CodeTypeReferenceExpression(typeof(ConfigurationManager));
				CodePropertyReferenceExpression targetObject2 = new CodePropertyReferenceExpression(targetObject, "AppSettings");
				CodeExpression codeExpression = new CodeIndexerExpression(targetObject2, new CodeExpression[]
				{
					new CodePrimitiveExpression(appSettingUrlKey)
				});
				ctor.Statements.Add(new CodeVariableDeclarationStatement(typeof(string), "urlSetting", codeExpression));
				if (appSettingBaseUrl == null || appSettingBaseUrl.Length == 0)
				{
					codeExpression = codeVariableReferenceExpression;
				}
				else
				{
					if (url == null || url.Length == 0)
					{
						throw new ArgumentException(Res.GetString("IfAppSettingBaseUrlArgumentIsSpecifiedThen0"));
					}
					string value = new Uri(appSettingBaseUrl).MakeRelative(new Uri(url));
					CodeExpression[] parameters = new CodeExpression[]
					{
						codeVariableReferenceExpression,
						new CodePrimitiveExpression(value)
					};
					codeExpression = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(string)), "Concat", parameters);
				}
				CodeStatement[] trueStatements = new CodeStatement[]
				{
					new CodeAssignStatement(left, codeExpression)
				};
				CodeBinaryOperatorExpression condition = new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
				if (flag)
				{
					ctor.Statements.Add(new CodeConditionStatement(condition, trueStatements, new CodeStatement[]
					{
						codeAssignStatement
					}));
					return;
				}
				ctor.Statements.Add(new CodeConditionStatement(condition, trueStatements));
			}
		}
	}
}
