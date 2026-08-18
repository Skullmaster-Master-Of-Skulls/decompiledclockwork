using System;
using System.CodeDom;
using System.Reflection;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000837 RID: 2103
	internal class DataBindingExpressionBuilder : ExpressionBuilder
	{
		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x0600644B RID: 25675 RVA: 0x001600E4 File Offset: 0x0015E2E4
		internal static EventInfo Event
		{
			get
			{
				if (DataBindingExpressionBuilder.eventInfo == null)
				{
					DataBindingExpressionBuilder.eventInfo = typeof(Control).GetEvent("DataBinding");
				}
				return DataBindingExpressionBuilder.eventInfo;
			}
		}

		// Token: 0x0600644C RID: 25676 RVA: 0x00160114 File Offset: 0x0015E314
		internal static void BuildEvalExpression(string field, string formatString, string propertyName, Type propertyType, ControlBuilder controlBuilder, CodeStatementCollection methodStatements, CodeStatementCollection statements, CodeLinePragma linePragma, bool isEncoded, ref bool hasTempObject)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "Eval";
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(field));
			if (!string.IsNullOrEmpty(formatString))
			{
				codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(formatString));
			}
			CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
			DataBindingExpressionBuilder.BuildPropertySetExpression(codeMethodInvokeExpression, propertyName, propertyType, controlBuilder, methodStatements, codeStatementCollection, linePragma, isEncoded, ref hasTempObject);
			CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression2.Method.TargetObject = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Page");
			codeMethodInvokeExpression2.Method.MethodName = "GetDataItem";
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = new CodeBinaryOperatorExpression(codeMethodInvokeExpression2, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null));
			codeConditionStatement.TrueStatements.AddRange(codeStatementCollection);
			statements.Add(codeConditionStatement);
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x001601EC File Offset: 0x0015E3EC
		private static void BuildPropertySetExpression(CodeExpression expression, string propertyName, Type propertyType, ControlBuilder controlBuilder, CodeStatementCollection methodStatements, CodeStatementCollection statements, CodeLinePragma linePragma, bool isEncoded, ref bool hasTempObject)
		{
			if (isEncoded)
			{
				expression = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(HttpUtility)), "HtmlEncode"), new CodeExpression[]
				{
					expression
				});
			}
			CodeDomUtility.CreatePropertySetStatements(methodStatements, statements, new CodeVariableReferenceExpression("dataBindingExpressionBuilderTarget"), propertyName, propertyType, expression, linePragma);
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x00160240 File Offset: 0x0015E440
		internal static void BuildExpressionSetup(ControlBuilder controlBuilder, CodeStatementCollection methodStatements, CodeStatementCollection statements, CodeLinePragma linePragma, bool isTwoWayBound, bool designerMode)
		{
			CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement(controlBuilder.ControlType, "dataBindingExpressionBuilderTarget");
			methodStatements.Add(codeVariableDeclarationStatement);
			CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression(codeVariableDeclarationStatement.Name);
			statements.Add(new CodeAssignStatement(codeVariableReferenceExpression, new CodeCastExpression(controlBuilder.ControlType, new CodeArgumentReferenceExpression("sender")))
			{
				LinePragma = linePragma
			});
			Type bindingContainerType = controlBuilder.BindingContainerType;
			CodeVariableDeclarationStatement codeVariableDeclarationStatement2 = new CodeVariableDeclarationStatement(bindingContainerType, "Container");
			methodStatements.Add(codeVariableDeclarationStatement2);
			statements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(codeVariableDeclarationStatement2.Name), new CodeCastExpression(bindingContainerType, new CodePropertyReferenceExpression(codeVariableReferenceExpression, "BindingContainer")))
			{
				LinePragma = linePragma
			});
			string variableName = isTwoWayBound ? "BindItem" : "Item";
			DataBindingExpressionBuilder.GenerateItemTypeExpressions(controlBuilder, methodStatements, statements, linePragma, variableName);
			if (designerMode)
			{
				DataBindingExpressionBuilder.GenerateItemTypeExpressions(controlBuilder, methodStatements, statements, linePragma, isTwoWayBound ? "Item" : "BindItem");
			}
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x0016032C File Offset: 0x0015E52C
		internal static void GenerateItemTypeExpressions(ControlBuilder controlBuilder, CodeStatementCollection declarationStatements, CodeStatementCollection codeStatements, CodeLinePragma linePragma, string variableName)
		{
			string itemType = controlBuilder.ItemType;
			if (!string.IsNullOrEmpty(itemType))
			{
				CodeVariableDeclarationStatement codeVariableDeclarationStatement = new CodeVariableDeclarationStatement(itemType, variableName);
				declarationStatements.Add(codeVariableDeclarationStatement);
				codeStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(codeVariableDeclarationStatement.Name), new CodeCastExpression(itemType, new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("Container"), "DataItem")))
				{
					LinePragma = linePragma
				});
			}
		}

		// Token: 0x06006450 RID: 25680 RVA: 0x00160395 File Offset: 0x0015E595
		internal override void BuildExpression(BoundPropertyEntry bpe, ControlBuilder controlBuilder, CodeExpression controlReference, CodeStatementCollection methodStatements, CodeStatementCollection statements, CodeLinePragma linePragma, ref bool hasTempObject)
		{
			DataBindingExpressionBuilder.BuildExpressionStatic(bpe, controlBuilder, controlReference, methodStatements, statements, linePragma, bpe.IsEncoded, ref hasTempObject);
		}

		// Token: 0x06006451 RID: 25681 RVA: 0x001603B0 File Offset: 0x0015E5B0
		internal static void BuildExpressionStatic(BoundPropertyEntry bpe, ControlBuilder controlBuilder, CodeExpression controlReference, CodeStatementCollection methodStatements, CodeStatementCollection statements, CodeLinePragma linePragma, bool isEncoded, ref bool hasTempObject)
		{
			CodeExpression expression = new CodeSnippetExpression(bpe.Expression);
			DataBindingExpressionBuilder.BuildPropertySetExpression(expression, bpe.Name, bpe.Type, controlBuilder, methodStatements, statements, linePragma, isEncoded, ref hasTempObject);
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x0000298D File Offset: 0x00000B8D
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			return null;
		}

		// Token: 0x040033DC RID: 13276
		private static EventInfo eventInfo;

		// Token: 0x040033DD RID: 13277
		private const string EvalMethodName = "Eval";

		// Token: 0x040033DE RID: 13278
		private const string GetDataItemMethodName = "GetDataItem";
	}
}
