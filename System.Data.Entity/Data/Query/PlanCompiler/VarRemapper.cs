using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000087 RID: 135
	internal class VarRemapper : BasicOpVisitor
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x00033767 File Offset: 0x00031967
		internal VarRemapper(Command command) : this(command, new Dictionary<Var, Var>())
		{
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00033775 File Offset: 0x00031975
		internal VarRemapper(Command command, Dictionary<Var, Var> varMap)
		{
			this.m_command = command;
			this.m_varMap = varMap;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0003378B File Offset: 0x0003198B
		internal void AddMapping(Var oldVar, Var newVar)
		{
			this.m_varMap[oldVar] = newVar;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0003379A File Offset: 0x0003199A
		internal virtual void RemapNode(Node node)
		{
			if (this.m_varMap.Count == 0)
			{
				return;
			}
			this.VisitNode(node);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000337B4 File Offset: 0x000319B4
		internal virtual void RemapSubtree(Node subTree)
		{
			if (this.m_varMap.Count == 0)
			{
				return;
			}
			foreach (Node subTree2 in subTree.Children)
			{
				this.RemapSubtree(subTree2);
			}
			this.RemapNode(subTree);
			this.m_command.RecomputeNodeInfo(subTree);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00033828 File Offset: 0x00031A28
		internal VarList RemapVarList(VarList varList)
		{
			return Command.CreateVarList(this.MapVars(varList));
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00033838 File Offset: 0x00031A38
		internal static VarList RemapVarList(Command command, Dictionary<Var, Var> varMap, VarList varList)
		{
			VarRemapper varRemapper = new VarRemapper(command, varMap);
			return varRemapper.RemapVarList(varList);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00033854 File Offset: 0x00031A54
		private Var Map(Var v)
		{
			Var var;
			while (this.m_varMap.TryGetValue(v, out var))
			{
				v = var;
			}
			return v;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00033877 File Offset: 0x00031A77
		private IEnumerable<Var> MapVars(IEnumerable<Var> vars)
		{
			foreach (Var v in vars)
			{
				yield return this.Map(v);
			}
			IEnumerator<Var> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00033890 File Offset: 0x00031A90
		private void Map(VarVec vec)
		{
			VarVec other = this.m_command.CreateVarVec(this.MapVars(vec));
			vec.InitFrom(other);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000338B8 File Offset: 0x00031AB8
		private void Map(VarList varList)
		{
			VarList collection = Command.CreateVarList(this.MapVars(varList));
			varList.Clear();
			varList.AddRange(collection);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000338E0 File Offset: 0x00031AE0
		private void Map(VarMap varMap)
		{
			VarMap varMap2 = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in varMap)
			{
				Var value = this.Map(keyValuePair.Value);
				varMap2.Add(keyValuePair.Key, value);
			}
			varMap.Clear();
			foreach (KeyValuePair<Var, Var> keyValuePair2 in varMap2)
			{
				varMap.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0003399C File Offset: 0x00031B9C
		private void Map(List<SortKey> sortKeys)
		{
			VarVec varVec = this.m_command.CreateVarVec();
			bool flag = false;
			foreach (SortKey sortKey in sortKeys)
			{
				sortKey.Var = this.Map(sortKey.Var);
				if (varVec.IsSet(sortKey.Var))
				{
					flag = true;
				}
				varVec.Set(sortKey.Var);
			}
			if (flag)
			{
				List<SortKey> list = new List<SortKey>(sortKeys);
				sortKeys.Clear();
				varVec.Clear();
				foreach (SortKey sortKey2 in list)
				{
					if (!varVec.IsSet(sortKey2.Var))
					{
						sortKeys.Add(sortKey2);
					}
					varVec.Set(sortKey2.Var);
				}
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected override void VisitDefault(Node n)
		{
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00033A94 File Offset: 0x00031C94
		public override void Visit(VarRefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Var var = this.Map(op.Var);
			if (var != op.Var)
			{
				n.Op = this.m_command.CreateVarRefOp(var);
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00013A81 File Offset: 0x00011C81
		protected override void VisitNestOp(NestBaseOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00033AD4 File Offset: 0x00031CD4
		public override void Visit(PhysicalProjectOp op, Node n)
		{
			this.VisitPhysicalOpDefault(op, n);
			this.Map(op.Outputs);
			SimpleCollectionColumnMap columnMap = (SimpleCollectionColumnMap)ColumnMapTranslator.Translate(op.ColumnMap, this.m_varMap);
			n.Op = this.m_command.CreatePhysicalProjectOp(op.Outputs, columnMap);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00033B24 File Offset: 0x00031D24
		protected override void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Outputs);
			this.Map(op.Keys);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00033B46 File Offset: 0x00031D46
		public override void Visit(GroupByIntoOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
			this.Map(op.Inputs);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00033B5C File Offset: 0x00031D5C
		public override void Visit(DistinctOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Keys);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00033B72 File Offset: 0x00031D72
		public override void Visit(ProjectOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Outputs);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00033B88 File Offset: 0x00031D88
		public override void Visit(UnnestOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			Var var = this.Map(op.Var);
			if (var != op.Var)
			{
				n.Op = this.m_command.CreateUnnestOp(var, op.Table);
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00033BCB File Offset: 0x00031DCB
		protected override void VisitSetOp(SetOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.VarMap[0]);
			this.Map(op.VarMap[1]);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00033BF1 File Offset: 0x00031DF1
		protected override void VisitSortOp(SortBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Keys);
		}

		// Token: 0x0400088D RID: 2189
		private readonly Dictionary<Var, Var> m_varMap;

		// Token: 0x0400088E RID: 2190
		protected readonly Command m_command;
	}
}
