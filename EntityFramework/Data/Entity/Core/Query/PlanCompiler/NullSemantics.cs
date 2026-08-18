using System;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020000D5 RID: 213
	internal class NullSemantics : BasicOpVisitorOfNode
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x00024069 File Offset: 0x00022269
		private NullSemantics(Command command)
		{
			this._command = command;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00024088 File Offset: 0x00022288
		public static bool Process(Command command)
		{
			NullSemantics nullSemantics = new NullSemantics(command);
			command.Root = nullSemantics.VisitNode(command.Root);
			return nullSemantics._modified;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000240B4 File Offset: 0x000222B4
		protected override Node VisitDefault(Node n)
		{
			bool negated = this._negated;
			OpType opType = n.Op.OpType;
			switch (opType)
			{
			case OpType.EQ:
				this._negated = false;
				n = this.HandleEQ(n, negated);
				break;
			case OpType.NE:
				n = this.HandleNE(n);
				break;
			default:
				switch (opType)
				{
				case OpType.And:
					n = base.VisitDefault(n);
					goto IL_9D;
				case OpType.Or:
					n = this.HandleOr(n);
					goto IL_9D;
				case OpType.Not:
					this._negated = !this._negated;
					n = base.VisitDefault(n);
					goto IL_9D;
				}
				this._negated = false;
				n = base.VisitDefault(n);
				break;
			}
			IL_9D:
			this._negated = negated;
			return n;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00024168 File Offset: 0x00022368
		private Node HandleOr(Node n)
		{
			Node node = (n.Child0.Op.OpType == OpType.IsNull) ? n.Child0 : null;
			if (node == null || node.Child0.Op.OpType != OpType.VarRef)
			{
				return base.VisitDefault(n);
			}
			Var var = ((VarRefOp)node.Child0.Op).Var;
			bool value = this._variableNullabilityTable[var];
			this._variableNullabilityTable[var] = false;
			n.Child1 = base.VisitNode(n.Child1);
			this._variableNullabilityTable[var] = value;
			return n;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00024204 File Offset: 0x00022404
		private Node HandleEQ(Node n, bool negated)
		{
			this._modified |= (!object.ReferenceEquals(n.Child0, n.Child0 = base.VisitNode(n.Child0)) || !object.ReferenceEquals(n.Child1, n.Child1 = base.VisitNode(n.Child1)) || !object.ReferenceEquals(n, n = this.ImplementEquality(n, negated)));
			return n;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0002427C File Offset: 0x0002247C
		private Node HandleNE(Node n)
		{
			ComparisonOp comparisonOp = (ComparisonOp)n.Op;
			n = this._command.CreateNode(this._command.CreateConditionalOp(OpType.Not), this._command.CreateNode(this._command.CreateComparisonOp(OpType.EQ, comparisonOp.UseDatabaseNullSemantics), n.Child0, n.Child1));
			this._modified = true;
			return base.VisitDefault(n);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000242E7 File Offset: 0x000224E7
		private bool IsNullableVarRef(Node n)
		{
			return n.Op.OpType == OpType.VarRef && this._variableNullabilityTable[((VarRefOp)n.Op).Var];
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00024314 File Offset: 0x00022514
		private Node ImplementEquality(Node n, bool negated)
		{
			ComparisonOp comparisonOp = (ComparisonOp)n.Op;
			if (comparisonOp.UseDatabaseNullSemantics)
			{
				return n;
			}
			Node child = n.Child0;
			Node child2 = n.Child1;
			switch (child.Op.OpType)
			{
			case OpType.Constant:
			case OpType.InternalConstant:
			case OpType.NullSentinel:
				switch (child2.Op.OpType)
				{
				case OpType.Constant:
				case OpType.InternalConstant:
				case OpType.NullSentinel:
					return n;
				case OpType.Null:
					return this.False();
				default:
					if (!negated)
					{
						return n;
					}
					return this.And(n, this.Not(this.IsNull(this.Clone(child2))));
				}
				break;
			case OpType.Null:
				switch (child2.Op.OpType)
				{
				case OpType.Constant:
				case OpType.InternalConstant:
				case OpType.NullSentinel:
					return this.False();
				case OpType.Null:
					return this.True();
				default:
					return this.IsNull(child2);
				}
				break;
			default:
				switch (child2.Op.OpType)
				{
				case OpType.Constant:
				case OpType.InternalConstant:
				case OpType.NullSentinel:
					if (!negated || !this.IsNullableVarRef(n))
					{
						return n;
					}
					return this.And(n, this.Not(this.IsNull(this.Clone(child))));
				case OpType.Null:
					return this.IsNull(child);
				default:
					if (!negated)
					{
						return this.Or(n, this.And(this.IsNull(this.Clone(child)), this.IsNull(this.Clone(child2))));
					}
					return this.And(n, this.NotXor(this.Clone(child), this.Clone(child2)));
				}
				break;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00024494 File Offset: 0x00022694
		private Node Clone(Node x)
		{
			return OpCopier.Copy(this._command, x);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000244A2 File Offset: 0x000226A2
		private Node False()
		{
			return this._command.CreateNode(this._command.CreateFalseOp());
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000244BA File Offset: 0x000226BA
		private Node True()
		{
			return this._command.CreateNode(this._command.CreateTrueOp());
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000244D2 File Offset: 0x000226D2
		private Node IsNull(Node x)
		{
			return this._command.CreateNode(this._command.CreateConditionalOp(OpType.IsNull), x);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000244ED File Offset: 0x000226ED
		private Node Not(Node x)
		{
			return this._command.CreateNode(this._command.CreateConditionalOp(OpType.Not), x);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00024508 File Offset: 0x00022708
		private Node And(Node x, Node y)
		{
			return this._command.CreateNode(this._command.CreateConditionalOp(OpType.And), x, y);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00024524 File Offset: 0x00022724
		private Node Or(Node x, Node y)
		{
			return this._command.CreateNode(this._command.CreateConditionalOp(OpType.Or), x, y);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00024540 File Offset: 0x00022740
		private Node Boolean(bool value)
		{
			return this._command.CreateNode(this._command.CreateConstantOp(this._command.BooleanType, value));
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0002456C File Offset: 0x0002276C
		private Node NotXor(Node x, Node y)
		{
			return this._command.CreateNode(this._command.CreateComparisonOp(OpType.EQ, false), this._command.CreateNode(this._command.CreateCaseOp(this._command.BooleanType), this.IsNull(x), this.Boolean(true), this.Boolean(false)), this._command.CreateNode(this._command.CreateCaseOp(this._command.BooleanType), this.IsNull(y), this.Boolean(true), this.Boolean(false)));
		}

		// Token: 0x040001AA RID: 426
		private Command _command;

		// Token: 0x040001AB RID: 427
		private bool _modified;

		// Token: 0x040001AC RID: 428
		private bool _negated;

		// Token: 0x040001AD RID: 429
		private NullSemantics.VariableNullabilityTable _variableNullabilityTable = new NullSemantics.VariableNullabilityTable(32);

		// Token: 0x020000D6 RID: 214
		private struct VariableNullabilityTable
		{
			// Token: 0x06000564 RID: 1380 RVA: 0x000245FE File Offset: 0x000227FE
			public VariableNullabilityTable(int capacity)
			{
				this._entries = Enumerable.Repeat<bool>(true, capacity).ToArray<bool>();
			}

			// Token: 0x17000034 RID: 52
			public bool this[Var variable]
			{
				get
				{
					return variable.Id >= this._entries.Length || this._entries[variable.Id];
				}
				set
				{
					this.EnsureCapacity(variable.Id + 1);
					this._entries[variable.Id] = value;
				}
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x00024654 File Offset: 0x00022854
			private void EnsureCapacity(int minimum)
			{
				if (this._entries.Length < minimum)
				{
					int num = this._entries.Length * 2;
					if (num < minimum)
					{
						num = minimum;
					}
					bool[] array = Enumerable.Repeat<bool>(true, num).ToArray<bool>();
					Array.Copy(this._entries, 0, array, 0, this._entries.Length);
					this._entries = array;
				}
			}

			// Token: 0x040001AE RID: 430
			private bool[] _entries;
		}
	}
}
