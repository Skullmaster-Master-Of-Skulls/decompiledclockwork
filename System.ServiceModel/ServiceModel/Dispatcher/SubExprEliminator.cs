using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000502 RID: 1282
	internal class SubExprEliminator
	{
		// Token: 0x0600308B RID: 12427 RVA: 0x000BA140 File Offset: 0x000B8340
		internal SubExprEliminator()
		{
			this.removalMapping = new Dictionary<object, List<SubExpr>>();
			this.exprList = new List<SubExpr>();
			Opcode ops = new XPathMessageFunctionCallOpcode(XPathMessageContext.HeaderFun, 0);
			SubExprHeader item = new SubExprHeader(ops, 0);
			this.exprList.Add(item);
			this.nextVar = 1;
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x000BA190 File Offset: 0x000B8390
		internal List<SubExpr> Exprs
		{
			get
			{
				return this.exprList;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000BA198 File Offset: 0x000B8398
		internal int VariableCount
		{
			get
			{
				return this.nextVar;
			}
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000BA1A0 File Offset: 0x000B83A0
		internal Opcode Add(object item, Opcode ops)
		{
			List<SubExpr> list = new List<SubExpr>();
			this.removalMapping.Add(item, list);
			while (ops.Next != null)
			{
				ops = ops.Next;
			}
			Opcode result = ops;
			while (ops != null)
			{
				if (SubExprEliminator.IsExprStarter(ops))
				{
					Opcode opcode = ops;
					Opcode prev = ops.Prev;
					ops.DetachFromParent();
					ops = ops.Next;
					while (ops.ID == OpcodeID.Select)
					{
						ops = ops.Next;
					}
					ops.DetachFromParent();
					SubExpr subExpr = null;
					for (int i = 0; i < this.exprList.Count; i++)
					{
						if (this.exprList[i].FirstOp.Equals(opcode))
						{
							subExpr = this.exprList[i];
							break;
						}
					}
					SubExprOpcode subExprOpcode;
					if (subExpr == null)
					{
						subExpr = new SubExpr(null, opcode, this.NewVarID());
						this.exprList.Add(subExpr);
						subExprOpcode = new SubExprOpcode(subExpr);
					}
					else
					{
						subExprOpcode = subExpr.Add(opcode, this);
					}
					subExprOpcode.Expr.IncRef();
					list.Add(subExprOpcode.Expr);
					subExprOpcode.Attach(ops);
					ops = subExprOpcode;
					if (prev != null)
					{
						prev.Attach(ops);
					}
				}
				result = ops;
				ops = ops.Prev;
			}
			return result;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000BA2D4 File Offset: 0x000B84D4
		internal static bool IsExprStarter(Opcode op)
		{
			if (op.ID == OpcodeID.SelectRoot)
			{
				return true;
			}
			if (op.ID == OpcodeID.XsltInternalFunction)
			{
				XPathMessageFunctionCallOpcode xpathMessageFunctionCallOpcode = (XPathMessageFunctionCallOpcode)op;
				if (xpathMessageFunctionCallOpcode.ReturnType == XPathResultType.NodeSet && xpathMessageFunctionCallOpcode.ArgCount == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000BA314 File Offset: 0x000B8514
		internal int NewVarID()
		{
			int num = this.nextVar;
			this.nextVar = num + 1;
			return num;
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000BA334 File Offset: 0x000B8534
		internal void Remove(object item)
		{
			List<SubExpr> list;
			if (this.removalMapping.TryGetValue(item, out list))
			{
				for (int i = 0; i < list.Count; i++)
				{
					list[i].DecRef(this);
				}
				this.removalMapping.Remove(item);
				this.Renumber();
			}
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000BA384 File Offset: 0x000B8584
		private void Renumber()
		{
			this.nextVar = 0;
			for (int i = 0; i < this.exprList.Count; i++)
			{
				this.exprList[i].Renumber(this);
			}
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000BA3C0 File Offset: 0x000B85C0
		internal void Trim()
		{
			this.exprList.Capacity = this.exprList.Count;
			for (int i = 0; i < this.exprList.Count; i++)
			{
				this.exprList[i].Trim();
			}
		}

		// Token: 0x04002608 RID: 9736
		private List<SubExpr> exprList;

		// Token: 0x04002609 RID: 9737
		private int nextVar;

		// Token: 0x0400260A RID: 9738
		private Dictionary<object, List<SubExpr>> removalMapping;
	}
}
