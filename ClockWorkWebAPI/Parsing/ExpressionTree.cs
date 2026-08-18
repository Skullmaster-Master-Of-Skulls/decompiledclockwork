using System;

namespace ClockWorkWebAPI.Parsing
{
	// Token: 0x0200004C RID: 76
	public class ExpressionTree
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0001A76C File Offset: 0x0001896C
		public ExpressionTree(string expression)
		{
			this.Root = new ExpressionNode(expression.Trim());
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001A788 File Offset: 0x00018988
		public bool Evaluate(int[] cids)
		{
			return this.Root.Evaluate(cids);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001A7A8 File Offset: 0x000189A8
		public string ShowStack()
		{
			string text = "**********" + Environment.NewLine;
			bool flag = this.Root != null;
			if (flag)
			{
				text = text + "> " + this.Root.Expression + Environment.NewLine;
				text += this.Root.ShowStack(0);
			}
			return text;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001A80C File Offset: 0x00018A0C
		public string RebuildExpression()
		{
			bool flag = this.Root == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = this.Root.RebuildExpression();
			}
			return result;
		}

		// Token: 0x040001D9 RID: 473
		public ExpressionNode Root;
	}
}
