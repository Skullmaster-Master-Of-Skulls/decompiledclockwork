using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Ast
{
	// Token: 0x02000134 RID: 308
	public sealed class TermNode : AstNode
	{
		// Token: 0x06001207 RID: 4615 RVA: 0x0004D0EC File Offset: 0x0004B2EC
		public TermNode(string unaryOperator, string numberBasedValue, string stringBasedValue, string hexColor, FunctionNode functionNode, ReadOnlyCollection<ImportantCommentNode> importantComments, string replacementTokenBasedValue = null)
		{
			bool flag = false;
			bool flag2 = false;
			if (!string.IsNullOrWhiteSpace(numberBasedValue))
			{
				flag = true;
			}
			if (!string.IsNullOrWhiteSpace(stringBasedValue))
			{
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					flag = true;
				}
			}
			if (!string.IsNullOrWhiteSpace(hexColor))
			{
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					flag = true;
				}
			}
			if (functionNode != null && flag)
			{
				flag2 = true;
			}
			if (flag2)
			{
				throw new AstException(CssStrings.ExpectedSingleValue);
			}
			this.UnaryOperator = unaryOperator;
			this.NumberBasedValue = numberBasedValue;
			this.StringBasedValue = stringBasedValue;
			this.Hexcolor = hexColor;
			this.FunctionNode = functionNode;
			this.ImportantComments = (importantComments ?? new List<ImportantCommentNode>().AsReadOnly());
			this.IsBinary = false;
			this.ReplacementTokenBasedValue = replacementTokenBasedValue;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0004D191 File Offset: 0x0004B391
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x0004D199 File Offset: 0x0004B399
		public string ReplacementTokenBasedValue { get; set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004D1A2 File Offset: 0x0004B3A2
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x0004D1AA File Offset: 0x0004B3AA
		public ReadOnlyCollection<ImportantCommentNode> ImportantComments { get; private set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0004D1B3 File Offset: 0x0004B3B3
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x0004D1BB File Offset: 0x0004B3BB
		public bool IsBinary { get; set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0004D1C4 File Offset: 0x0004B3C4
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x0004D1CC File Offset: 0x0004B3CC
		public string UnaryOperator { get; private set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0004D1D5 File Offset: 0x0004B3D5
		// (set) Token: 0x06001211 RID: 4625 RVA: 0x0004D1DD File Offset: 0x0004B3DD
		public string NumberBasedValue { get; private set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004D1E6 File Offset: 0x0004B3E6
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x0004D1EE File Offset: 0x0004B3EE
		public string StringBasedValue { get; private set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x0004D1F7 File Offset: 0x0004B3F7
		// (set) Token: 0x06001215 RID: 4629 RVA: 0x0004D1FF File Offset: 0x0004B3FF
		public string Hexcolor { get; private set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0004D208 File Offset: 0x0004B408
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x0004D210 File Offset: 0x0004B410
		public FunctionNode FunctionNode { get; private set; }

		// Token: 0x06001218 RID: 4632 RVA: 0x0004D21C File Offset: 0x0004B41C
		public bool Equals(TermNode termNode)
		{
			bool flag = termNode.IsBinary == this.IsBinary && termNode.UnaryOperator == this.UnaryOperator && termNode.NumberBasedValue == this.NumberBasedValue && termNode.StringBasedValue == this.StringBasedValue && termNode.Hexcolor == this.Hexcolor;
			if (this.FunctionNode != null && termNode.FunctionNode != null)
			{
				return flag && termNode.FunctionNode.Equals(this.FunctionNode);
			}
			return this.FunctionNode == null && termNode.FunctionNode == null && flag;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0004D2BF File Offset: 0x0004B4BF
		public override AstNode Accept(NodeVisitor nodeVisitor)
		{
			return nodeVisitor.VisitTermNode(this);
		}
	}
}
