using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200007B RID: 123
	public class CommaOperator : BinaryOperator
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00023390 File Offset: 0x00021590
		public CommaOperator(Context context) : base(context)
		{
			base.OperatorToken = JSToken.Comma;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000233A4 File Offset: 0x000215A4
		public static AstNode CombineWithComma(Context context, AstNode operand1, AstNode operand2)
		{
			CommaOperator commaOperator = new CommaOperator(context);
			BinaryOperator binaryOperator = operand1 as BinaryOperator;
			BinaryOperator binaryOperator2 = operand2 as BinaryOperator;
			if (binaryOperator != null && binaryOperator.OperatorToken == JSToken.Comma)
			{
				commaOperator.Operand1 = binaryOperator.Operand1;
				AstNodeList astNodeList;
				if (binaryOperator2 != null && binaryOperator2.OperatorToken == JSToken.Comma)
				{
					astNodeList = new AstNodeList(binaryOperator.Context.FlattenToStart());
					astNodeList.Append(binaryOperator.Operand2).Append(binaryOperator2.Operand1).Append(binaryOperator2.Operand2);
				}
				else
				{
					astNodeList = (binaryOperator.Operand2 as AstNodeList);
					if (astNodeList == null)
					{
						astNodeList = new AstNodeList(binaryOperator.Operand2.Context.FlattenToStart());
						astNodeList.Append(binaryOperator.Operand2);
					}
					astNodeList.Append(operand2);
				}
				commaOperator.Operand2 = astNodeList;
			}
			else if (binaryOperator2 != null && binaryOperator2.OperatorToken == JSToken.Comma)
			{
				commaOperator.Operand1 = operand1;
				AstNodeList astNodeList2 = binaryOperator2.Operand2 as AstNodeList;
				if (astNodeList2 != null)
				{
					astNodeList2.Insert(0, binaryOperator2.Operand1);
				}
				else
				{
					astNodeList2 = new AstNodeList(binaryOperator2.Context);
					astNodeList2.Append(binaryOperator2.Operand1);
					astNodeList2.Append(binaryOperator2.Operand2);
				}
				commaOperator.Operand2 = astNodeList2;
			}
			else
			{
				commaOperator.Operand1 = operand1;
				commaOperator.Operand2 = operand2;
			}
			return commaOperator;
		}
	}
}
