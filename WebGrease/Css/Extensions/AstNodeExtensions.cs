using System;
using System.Linq;
using System.Text;
using WebGrease.Css.Ast;
using WebGrease.Css.Ast.MediaQuery;
using WebGrease.Css.Ast.Selectors;
using WebGrease.Css.Visitor;

namespace WebGrease.Css.Extensions
{
	// Token: 0x02000184 RID: 388
	public static class AstNodeExtensions
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x0007725C File Offset: 0x0007545C
		public static string PrettyPrint(this AstNode node)
		{
			if (node != null)
			{
				return PrintVisitor.Print(node, true);
			}
			return string.Empty;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0007726E File Offset: 0x0007546E
		public static string MinifyPrint(this AstNode node)
		{
			if (node != null)
			{
				return PrintVisitor.Print(node, false);
			}
			return string.Empty;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00077288 File Offset: 0x00075488
		public static string PrintSelector(this MediaNode node)
		{
			return "@media " + string.Join(",", from mq in node.MediaQueries
			select mq.MinifyPrint());
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x000772F4 File Offset: 0x000754F4
		internal static string PrintSelector(this RulesetNode rulesetNode)
		{
			if (rulesetNode == null)
			{
				return string.Empty;
			}
			StringBuilder rulesetBuilder = new StringBuilder();
			rulesetNode.SelectorsGroupNode.SelectorNodes.ForEach(delegate(SelectorNode selector, bool last)
			{
				rulesetBuilder.Append(selector.MinifyPrint());
				if (!last)
				{
					rulesetBuilder.Append(',');
				}
			});
			return rulesetBuilder.ToString();
		}
	}
}
