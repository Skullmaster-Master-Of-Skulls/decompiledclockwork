using System;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x0200011C RID: 284
	public sealed class FunctionNode : AstNode
	{
		// Token: 0x06001158 RID: 4440 RVA: 0x0004C3DD File Offset: 0x0004A5DD
		public FunctionNode(string functionName, ExprNode exprNode)
		{
			this.FunctionName = functionName;
			this.ExprNode = exprNode;
			if (this.ExprNode != null)
			{
				this.ExprNode.UsesBinary = this.usesBinary();
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0004C40C File Offset: 0x0004A60C
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x0004C414 File Offset: 0x0004A614
		public string FunctionName { get; private set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0004C41D File Offset: 0x0004A61D
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x0004C425 File Offset: 0x0004A625
		public ExprNode ExprNode { get; private set; }

		// Token: 0x0600115D RID: 4445 RVA: 0x0004C42E File Offset: 0x0004A62E
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitFunctionNode(this);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0004C437 File Offset: 0x0004A637
		private bool usesBinary()
		{
			return Array.IndexOf<string>(FunctionNode.BinaryOpererableFunctionNames, this.FunctionName) > -1;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0004C44C File Offset: 0x0004A64C
		public bool Equals(FunctionNode functionNode)
		{
			return this.FunctionName == functionNode.FunctionName && this.ExprNode.Equals(functionNode.ExprNode);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0004C474 File Offset: 0x0004A674
		public static bool IsBinaryOperator(string binaryOperator)
		{
			return Array.IndexOf<string>(FunctionNode.binaryOperators, binaryOperator) > -1;
		}

		// Token: 0x040006F6 RID: 1782
		private static string[] BinaryOpererableFunctionNames = new string[]
		{
			"-webkit-calc",
			"calc",
			"min",
			"max"
		};

		// Token: 0x040006F7 RID: 1783
		private static string[] binaryOperators = new string[]
		{
			"-",
			"+"
		};
	}
}
