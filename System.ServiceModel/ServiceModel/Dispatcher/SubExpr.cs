using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000500 RID: 1280
	internal class SubExpr
	{
		// Token: 0x06003078 RID: 12408 RVA: 0x000B986C File Offset: 0x000B7A6C
		internal SubExpr(SubExpr parent, Opcode ops, int var)
		{
			this.children = new List<SubExpr>(2);
			this.var = var;
			this.parent = parent;
			this.useSpecial = false;
			if (parent != null)
			{
				this.ops = new InternalSubExprOpcode(parent);
				this.ops.Attach(ops);
				this.useSpecial = (parent is SubExprHeader && ((SelectOpcode)ops).Criteria.Axis.Type == QueryAxisType.Child);
				return;
			}
			this.ops = ops;
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x000B98EF File Offset: 0x000B7AEF
		internal Opcode FirstOp
		{
			get
			{
				if (this.parent == null)
				{
					return this.ops;
				}
				return this.ops.Next;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x000B990B File Offset: 0x000B7B0B
		internal int Variable
		{
			get
			{
				return this.var;
			}
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000B9914 File Offset: 0x000B7B14
		internal SubExprOpcode Add(Opcode opseq, SubExprEliminator elim)
		{
			Opcode opcode = this.FirstOp;
			Opcode opcode2 = opseq;
			while (opcode != null && opcode2 != null && opcode.Equals(opcode2))
			{
				opcode = opcode.Next;
				opcode2 = opcode2.Next;
			}
			if (opcode2 == null)
			{
				if (opcode == null)
				{
					return new SubExprOpcode(this);
				}
				SubExpr expr = this.BranchAt(opcode, elim);
				return new SubExprOpcode(expr);
			}
			else
			{
				if (opcode == null)
				{
					opcode2.DetachFromParent();
					for (int i = 0; i < this.children.Count; i++)
					{
						if (this.children[i].FirstOp.Equals(opcode2))
						{
							return this.children[i].Add(opcode2, elim);
						}
					}
					SubExpr expr2 = new SubExpr(this, opcode2, elim.NewVarID());
					this.AddChild(expr2);
					return new SubExprOpcode(expr2);
				}
				SubExpr subExpr = this.BranchAt(opcode, elim);
				opcode2.DetachFromParent();
				SubExpr expr3 = new SubExpr(subExpr, opcode2, elim.NewVarID());
				subExpr.AddChild(expr3);
				return new SubExprOpcode(expr3);
			}
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000B9A04 File Offset: 0x000B7C04
		internal virtual void AddChild(SubExpr expr)
		{
			this.children.Add(expr);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000B9A14 File Offset: 0x000B7C14
		private SubExpr BranchAt(Opcode op, SubExprEliminator elim)
		{
			Opcode firstOp = this.FirstOp;
			if (this.parent != null)
			{
				this.parent.RemoveChild(this);
			}
			else
			{
				elim.Exprs.Remove(this);
			}
			firstOp.DetachFromParent();
			op.DetachFromParent();
			SubExpr subExpr = new SubExpr(this.parent, firstOp, elim.NewVarID());
			if (this.parent != null)
			{
				this.parent.AddChild(subExpr);
			}
			else
			{
				elim.Exprs.Add(subExpr);
			}
			subExpr.AddChild(this);
			this.parent = subExpr;
			this.ops = new InternalSubExprOpcode(subExpr);
			this.ops.Attach(op);
			return subExpr;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000B9AB4 File Offset: 0x000B7CB4
		internal void CleanUp(SubExprEliminator elim)
		{
			if (this.refCount == 0)
			{
				if (this.children.Count == 0)
				{
					if (this.parent == null)
					{
						elim.Exprs.Remove(this);
						return;
					}
					this.parent.RemoveChild(this);
					this.parent.CleanUp(elim);
					return;
				}
				else if (this.children.Count == 1)
				{
					SubExpr subExpr = this.children[0];
					Opcode firstOp = subExpr.FirstOp;
					firstOp.DetachFromParent();
					Opcode next = this.ops;
					while (next.Next != null)
					{
						next = next.Next;
					}
					next.Attach(firstOp);
					subExpr.ops = this.ops;
					if (this.parent == null)
					{
						elim.Exprs.Remove(this);
						elim.Exprs.Add(subExpr);
						subExpr.parent = null;
						return;
					}
					this.parent.RemoveChild(this);
					this.parent.AddChild(subExpr);
					subExpr.parent = this.parent;
				}
			}
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000B9BAB File Offset: 0x000B7DAB
		internal void DecRef(SubExprEliminator elim)
		{
			this.refCount--;
			this.CleanUp(elim);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000B9BC4 File Offset: 0x000B7DC4
		internal void Eval(ProcessingContext context)
		{
			int counterMarker = context.Processor.CounterMarker;
			Opcode opcode = this.ops;
			if (this.useSpecial)
			{
				opcode.EvalSpecial(context);
				context.LoadVariable(this.var);
				return;
			}
			while (opcode != null)
			{
				opcode = opcode.Eval(context);
			}
			int count = context.Processor.ElapsedCount(counterMarker);
			context.SaveVariable(this.var, count);
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000B9C29 File Offset: 0x000B7E29
		internal virtual void EvalSpecial(ProcessingContext context)
		{
			this.Eval(context);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000B9C32 File Offset: 0x000B7E32
		internal void IncRef()
		{
			this.refCount++;
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000B9C42 File Offset: 0x000B7E42
		internal virtual void RemoveChild(SubExpr expr)
		{
			this.children.Remove(expr);
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000B9C54 File Offset: 0x000B7E54
		internal void Renumber(SubExprEliminator elim)
		{
			this.var = elim.NewVarID();
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].Renumber(elim);
			}
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000B9C98 File Offset: 0x000B7E98
		internal void Trim()
		{
			this.children.Capacity = this.children.Count;
			this.ops.Trim();
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].Trim();
			}
		}

		// Token: 0x04002600 RID: 9728
		internal int var;

		// Token: 0x04002601 RID: 9729
		internal int refCount;

		// Token: 0x04002602 RID: 9730
		internal bool useSpecial;

		// Token: 0x04002603 RID: 9731
		private Opcode ops;

		// Token: 0x04002604 RID: 9732
		private SubExpr parent;

		// Token: 0x04002605 RID: 9733
		protected List<SubExpr> children;
	}
}
