using System;
using System.CodeDom;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x020007F0 RID: 2032
	internal class WebObjectActivatorParseRecorder : ParseRecorder
	{
		// Token: 0x060060EC RID: 24812 RVA: 0x0014E3CC File Offset: 0x0014C5CC
		public override void ProcessGeneratedCode(ControlBuilder builder, CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
		{
			if (derivedType != null && typeof(Control).IsAssignableFrom(builder.ControlType) && buildMethod != null)
			{
				CodeAssignStatement codeAssignStatement = WebObjectActivatorParseRecorder.FindControlCreateStatement(builder.ControlType, buildMethod.Statements);
				if (codeAssignStatement != null)
				{
					WebObjectActivatorParseRecorder.ReplaceControlCreateStatement(builder.ControlType, codeAssignStatement, buildMethod.Statements);
				}
			}
			base.ProcessGeneratedCode(builder, codeCompileUnit, baseType, derivedType, buildMethod, dataBindingMethod);
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x0014E430 File Offset: 0x0014C630
		private static CodeAssignStatement FindControlCreateStatement(Type controlType, CodeStatementCollection statements)
		{
			foreach (object obj in statements)
			{
				CodeAssignStatement codeAssignStatement = obj as CodeAssignStatement;
				if (codeAssignStatement != null)
				{
					CodeObjectCreateExpression codeObjectCreateExpression = codeAssignStatement.Right as CodeObjectCreateExpression;
					if (codeObjectCreateExpression != null && codeObjectCreateExpression.CreateType.BaseType == controlType.ToString() && codeObjectCreateExpression.Parameters.Count == 0 && codeAssignStatement.Left is CodeVariableReferenceExpression)
					{
						return codeAssignStatement;
					}
				}
			}
			return null;
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x0014E4D0 File Offset: 0x0014C6D0
		private static void ReplaceControlCreateStatement(Type ctrlType, CodeAssignStatement objAssignStatement, CodeStatementCollection statements)
		{
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("System.Web.HttpRuntime"), "WebObjectActivator");
			CodeVariableReferenceExpression left = new CodeVariableReferenceExpression("__activator");
			CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(codePropertyReferenceExpression, "GetService", new CodeExpression[]
			{
				new CodeTypeOfExpression(ctrlType)
			});
			CodeCastExpression right = new CodeCastExpression(new CodeTypeReference(ctrlType), expression);
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement
			{
				Condition = new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null))
			};
			codeConditionStatement.TrueStatements.Add(new CodeAssignStatement(objAssignStatement.Left, right));
			if (WebObjectActivatorParseRecorder.DoesTypeHaveDefaultCtor(ctrlType))
			{
				codeConditionStatement.FalseStatements.Add(objAssignStatement);
			}
			else
			{
				CodeThrowExceptionStatement value = new CodeThrowExceptionStatement(new CodeObjectCreateExpression(new CodeTypeReference(typeof(InvalidOperationException)), new CodeExpression[]
				{
					new CodePrimitiveExpression(SR.GetString("Could_not_create_type_instance", new object[]
					{
						ctrlType
					}))
				}));
				codeConditionStatement.FalseStatements.Add(value);
			}
			int index = statements.IndexOf(objAssignStatement);
			statements.Insert(index, codeConditionStatement);
			statements.Insert(index, new CodeAssignStatement(left, codePropertyReferenceExpression));
			statements.Insert(index, new CodeVariableDeclarationStatement(typeof(IServiceProvider), "__activator"));
			statements.Remove(objAssignStatement);
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x0014E604 File Offset: 0x0014C804
		private static bool DoesTypeHaveDefaultCtor(Type type)
		{
			if (type.GetConstructor(Type.EmptyTypes) != null)
			{
				return true;
			}
			foreach (ConstructorInfo ctor in type.GetConstructors(BindingFlags.Instance | BindingFlags.Public))
			{
				if (WebObjectActivatorParseRecorder.DoesAllConstructorParametersHaveDefaultValue(ctor))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x0014E64C File Offset: 0x0014C84C
		private static bool DoesAllConstructorParametersHaveDefaultValue(ConstructorInfo ctor)
		{
			foreach (ParameterInfo parameterInfo in ctor.GetParameters())
			{
				bool flag = false;
				foreach (CustomAttributeData customAttributeData in parameterInfo.CustomAttributes)
				{
					if (customAttributeData.AttributeType == typeof(OptionalAttribute))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}
	}
}
