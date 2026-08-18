using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000653 RID: 1619
	internal class VarRemapper : BasicOpVisitor
	{
		// Token: 0x06003F47 RID: 16199 RVA: 0x00121E72 File Offset: 0x00120072
		internal VarRemapper(Command command) : this(command, new Dictionary<Var, Var>())
		{
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x00121E80 File Offset: 0x00120080
		internal VarRemapper(Command command, Dictionary<Var, Var> varMap)
		{
			this.m_command = command;
			this.m_varMap = varMap;
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x00121E96 File Offset: 0x00120096
		internal void AddMapping(Var oldVar, Var newVar)
		{
			this.m_varMap[oldVar] = newVar;
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x00121EA5 File Offset: 0x001200A5
		internal virtual void RemapNode(Node node)
		{
			if (this.m_varMap.Count == 0)
			{
				return;
			}
			this.VisitNode(node);
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x00121EBC File Offset: 0x001200BC
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

		// Token: 0x06003F4C RID: 16204 RVA: 0x00121F30 File Offset: 0x00120130
		internal VarList RemapVarList(VarList varList)
		{
			return Command.CreateVarList(this.MapVars(varList));
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x00121F40 File Offset: 0x00120140
		internal static VarList RemapVarList(Command command, Dictionary<Var, Var> varMap, VarList varList)
		{
			VarRemapper varRemapper = new VarRemapper(command, varMap);
			return varRemapper.RemapVarList(varList);
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x00121F5C File Offset: 0x0012015C
		private Var Map(Var v)
		{
			Var var;
			while (this.m_varMap.TryGetValue(v, out var))
			{
				v = var;
			}
			return v;
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x0012211C File Offset: 0x0012031C
		private IEnumerable<Var> MapVars(IEnumerable<Var> vars)
		{
			foreach (Var v in vars)
			{
				yield return this.Map(v);
			}
			yield break;
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x00122140 File Offset: 0x00120340
		private void Map(VarVec vec)
		{
			VarVec other = this.m_command.CreateVarVec(this.MapVars(vec));
			vec.InitFrom(other);
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x00122168 File Offset: 0x00120368
		private void Map(VarList varList)
		{
			VarList collection = Command.CreateVarList(this.MapVars(varList));
			varList.Clear();
			varList.AddRange(collection);
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x00122190 File Offset: 0x00120390
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

		// Token: 0x06003F53 RID: 16211 RVA: 0x0012224C File Offset: 0x0012044C
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

		// Token: 0x06003F54 RID: 16212 RVA: 0x00122344 File Offset: 0x00120544
		protected override void VisitDefault(Node n)
		{
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x00122348 File Offset: 0x00120548
		public override void Visit(VarRefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Var var = this.Map(op.Var);
			if (var != op.Var)
			{
				n.Op = this.m_command.CreateVarRefOp(var);
			}
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x00122385 File Offset: 0x00120585
		protected override void VisitNestOp(NestBaseOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x0012238C File Offset: 0x0012058C
		public override void Visit(PhysicalProjectOp op, Node n)
		{
			this.VisitPhysicalOpDefault(op, n);
			this.Map(op.Outputs);
			SimpleCollectionColumnMap columnMap = (SimpleCollectionColumnMap)ColumnMapTranslator.Translate(op.ColumnMap, this.m_varMap);
			n.Op = this.m_command.CreatePhysicalProjectOp(op.Outputs, columnMap);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x001223DC File Offset: 0x001205DC
		protected override void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Outputs);
			this.Map(op.Keys);
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x001223FE File Offset: 0x001205FE
		public override void Visit(GroupByIntoOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
			this.Map(op.Inputs);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x00122414 File Offset: 0x00120614
		public override void Visit(DistinctOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Keys);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x0012242A File Offset: 0x0012062A
		public override void Visit(ProjectOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Outputs);
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x00122440 File Offset: 0x00120640
		public override void Visit(UnnestOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			Var var = this.Map(op.Var);
			if (var != op.Var)
			{
				n.Op = this.m_command.CreateUnnestOp(var, op.Table);
			}
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x00122483 File Offset: 0x00120683
		protected override void VisitSetOp(SetOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.VarMap[0]);
			this.Map(op.VarMap[1]);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x001224A9 File Offset: 0x001206A9
		protected override void VisitSortOp(SortBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Keys);
		}

		// Token: 0x040017A2 RID: 6050
		private readonly Dictionary<Var, Var> m_varMap;

		// Token: 0x040017A3 RID: 6051
		protected readonly Command m_command;
	}
}
