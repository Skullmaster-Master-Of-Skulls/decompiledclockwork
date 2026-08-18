using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B7 RID: 951
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class OpCopier : BasicOpVisitorOfNode
	{
		// Token: 0x06002298 RID: 8856 RVA: 0x000A1D28 File Offset: 0x0009FF28
		internal static Node Copy(Command cmd, Node n)
		{
			VarMap varMap;
			return OpCopier.Copy(cmd, n, out varMap);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000A1D40 File Offset: 0x0009FF40
		internal static Node Copy(Command cmd, Node node, VarList varList, out VarList newVarList)
		{
			VarMap varMap;
			Node result = OpCopier.Copy(cmd, node, out varMap);
			newVarList = Command.CreateVarList();
			foreach (Var key in varList)
			{
				Var item = varMap[key];
				newVarList.Add(item);
			}
			return result;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000A1DAC File Offset: 0x0009FFAC
		internal static Node Copy(Command cmd, Node n, out VarMap varMap)
		{
			OpCopier opCopier = new OpCopier(cmd);
			Node result = opCopier.CopyNode(n);
			varMap = opCopier.m_varMap;
			return result;
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000A1DD4 File Offset: 0x0009FFD4
		internal static List<SortKey> Copy(Command cmd, List<SortKey> sortKeys)
		{
			OpCopier opCopier = new OpCopier(cmd);
			return opCopier.Copy(sortKeys);
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000A1DEF File Offset: 0x0009FFEF
		protected OpCopier(Command cmd) : this(cmd, cmd)
		{
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000A1DF9 File Offset: 0x0009FFF9
		private OpCopier(Command destCommand, Command sourceCommand)
		{
			this.m_srcCmd = sourceCommand;
			this.m_destCmd = destCommand;
			this.m_varMap = new VarMap();
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000A1E1C File Offset: 0x000A001C
		private Var GetMappedVar(Var v)
		{
			Var result;
			if (this.m_varMap.TryGetValue(v, out result))
			{
				return result;
			}
			if (this.m_destCmd != this.m_srcCmd)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnknownVar, 6, null);
			}
			return v;
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000A1E57 File Offset: 0x000A0057
		private void SetMappedVar(Var v, Var mappedVar)
		{
			this.m_varMap.Add(v, mappedVar);
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000A1E68 File Offset: 0x000A0068
		private void MapTable(Table newTable, Table oldTable)
		{
			for (int i = 0; i < oldTable.Columns.Count; i++)
			{
				this.SetMappedVar(oldTable.Columns[i], newTable.Columns[i]);
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000A2054 File Offset: 0x000A0254
		private IEnumerable<Var> MapVars(IEnumerable<Var> vars)
		{
			foreach (Var v in vars)
			{
				Var mappedVar = this.GetMappedVar(v);
				yield return mappedVar;
			}
			yield break;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x000A2078 File Offset: 0x000A0278
		private VarVec Copy(VarVec vars)
		{
			return this.m_destCmd.CreateVarVec(this.MapVars(vars));
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000A209C File Offset: 0x000A029C
		private VarList Copy(VarList varList)
		{
			return Command.CreateVarList(this.MapVars(varList));
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000A20B7 File Offset: 0x000A02B7
		private SortKey Copy(SortKey sortKey)
		{
			return Command.CreateSortKey(this.GetMappedVar(sortKey.Var), sortKey.AscendingSort, sortKey.Collation);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000A20D8 File Offset: 0x000A02D8
		private List<SortKey> Copy(List<SortKey> sortKeys)
		{
			List<SortKey> list = new List<SortKey>();
			foreach (SortKey sortKey in sortKeys)
			{
				list.Add(this.Copy(sortKey));
			}
			return list;
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000A2134 File Offset: 0x000A0334
		protected Node CopyNode(Node n)
		{
			return n.Op.Accept<Node>(this, n);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000A2144 File Offset: 0x000A0344
		private List<Node> ProcessChildren(Node n)
		{
			List<Node> list = new List<Node>();
			foreach (Node n2 in n.Children)
			{
				list.Add(this.CopyNode(n2));
			}
			return list;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000A21A4 File Offset: 0x000A03A4
		private Node CopyDefault(Op op, Node original)
		{
			return this.m_destCmd.CreateNode(op, this.ProcessChildren(original));
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000A21B9 File Offset: 0x000A03B9
		public override Node Visit(Op op, Node n)
		{
			throw new NotSupportedException(Strings.Iqt_General_UnsupportedOp(op.GetType().FullName));
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000A21D0 File Offset: 0x000A03D0
		public override Node Visit(ConstantOp op, Node n)
		{
			ConstantBaseOp op2 = this.m_destCmd.CreateConstantOp(op.Type, op.Value);
			return this.m_destCmd.CreateNode(op2);
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000A2201 File Offset: 0x000A0401
		public override Node Visit(NullOp op, Node n)
		{
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateNullOp(op.Type));
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000A221F File Offset: 0x000A041F
		public override Node Visit(ConstantPredicateOp op, Node n)
		{
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateConstantPredicateOp(op.Value));
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000A2240 File Offset: 0x000A0440
		public override Node Visit(InternalConstantOp op, Node n)
		{
			InternalConstantOp op2 = this.m_destCmd.CreateInternalConstantOp(op.Type, op.Value);
			return this.m_destCmd.CreateNode(op2);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000A2274 File Offset: 0x000A0474
		public override Node Visit(NullSentinelOp op, Node n)
		{
			NullSentinelOp op2 = this.m_destCmd.CreateNullSentinelOp();
			return this.m_destCmd.CreateNode(op2);
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000A2299 File Offset: 0x000A0499
		public override Node Visit(FunctionOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateFunctionOp(op.Function), n);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000A22B3 File Offset: 0x000A04B3
		public override Node Visit(PropertyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreatePropertyOp(op.PropertyInfo), n);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000A22CD File Offset: 0x000A04CD
		public override Node Visit(RelPropertyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateRelPropertyOp(op.PropertyInfo), n);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000A22E7 File Offset: 0x000A04E7
		public override Node Visit(CaseOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCaseOp(op.Type), n);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000A2301 File Offset: 0x000A0501
		public override Node Visit(ComparisonOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateComparisonOp(op.OpType, op.UseDatabaseNullSemantics), n);
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000A2321 File Offset: 0x000A0521
		public override Node Visit(LikeOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateLikeOp(), n);
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000A2335 File Offset: 0x000A0535
		public override Node Visit(AggregateOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateAggregateOp(op.AggFunc, op.IsDistinctAggregate), n);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x000A2355 File Offset: 0x000A0555
		public override Node Visit(NewInstanceOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNewInstanceOp(op.Type), n);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000A2370 File Offset: 0x000A0570
		public override Node Visit(NewEntityOp op, Node n)
		{
			NewEntityOp op2;
			if (op.Scoped)
			{
				op2 = this.m_destCmd.CreateScopedNewEntityOp(op.Type, op.RelationshipProperties, op.EntitySet);
			}
			else
			{
				op2 = this.m_destCmd.CreateNewEntityOp(op.Type, op.RelationshipProperties);
			}
			return this.CopyDefault(op2, n);
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000A23C5 File Offset: 0x000A05C5
		public override Node Visit(DiscriminatedNewEntityOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateDiscriminatedNewEntityOp(op.Type, op.DiscriminatorMap, op.EntitySet, op.RelationshipProperties), n);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000A23F1 File Offset: 0x000A05F1
		public override Node Visit(NewMultisetOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNewMultisetOp(op.Type), n);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000A240B File Offset: 0x000A060B
		public override Node Visit(NewRecordOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNewRecordOp(op.Type), n);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000A2425 File Offset: 0x000A0625
		public override Node Visit(RefOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateRefOp(op.EntitySet, op.Type), n);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000A2448 File Offset: 0x000A0648
		public override Node Visit(VarRefOp op, Node n)
		{
			Var var;
			if (!this.m_varMap.TryGetValue(op.Var, out var))
			{
				var = op.Var;
			}
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateVarRefOp(var));
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000A2488 File Offset: 0x000A0688
		public override Node Visit(ConditionalOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateConditionalOp(op.OpType), n);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000A24A2 File Offset: 0x000A06A2
		public override Node Visit(ArithmeticOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateArithmeticOp(op.OpType, op.Type), n);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000A24C4 File Offset: 0x000A06C4
		public override Node Visit(TreatOp op, Node n)
		{
			TreatOp op2 = op.IsFakeTreat ? this.m_destCmd.CreateFakeTreatOp(op.Type) : this.m_destCmd.CreateTreatOp(op.Type);
			return this.CopyDefault(op2, n);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000A2506 File Offset: 0x000A0706
		public override Node Visit(CastOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCastOp(op.Type), n);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000A2520 File Offset: 0x000A0720
		public override Node Visit(SoftCastOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateSoftCastOp(op.Type), n);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000A253A File Offset: 0x000A073A
		public override Node Visit(DerefOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateDerefOp(op.Type), n);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000A2554 File Offset: 0x000A0754
		public override Node Visit(NavigateOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNavigateOp(op.Type, op.RelProperty), n);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000A2574 File Offset: 0x000A0774
		public override Node Visit(IsOfOp op, Node n)
		{
			if (op.IsOfOnly)
			{
				return this.CopyDefault(this.m_destCmd.CreateIsOfOnlyOp(op.IsOfType), n);
			}
			return this.CopyDefault(this.m_destCmd.CreateIsOfOp(op.IsOfType), n);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000A25AF File Offset: 0x000A07AF
		public override Node Visit(ExistsOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateExistsOp(), n);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000A25C3 File Offset: 0x000A07C3
		public override Node Visit(ElementOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateElementOp(op.Type), n);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000A25DD File Offset: 0x000A07DD
		public override Node Visit(GetRefKeyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateGetRefKeyOp(op.Type), n);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000A25F7 File Offset: 0x000A07F7
		public override Node Visit(GetEntityRefOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateGetEntityRefOp(op.Type), n);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000A2611 File Offset: 0x000A0811
		public override Node Visit(CollectOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCollectOp(op.Type), n);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000A262C File Offset: 0x000A082C
		public override Node Visit(ScanTableOp op, Node n)
		{
			ScanTableOp scanTableOp = this.m_destCmd.CreateScanTableOp(op.Table.TableMetadata);
			this.MapTable(scanTableOp.Table, op.Table);
			return this.m_destCmd.CreateNode(scanTableOp);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000A2670 File Offset: 0x000A0870
		public override Node Visit(ScanViewOp op, Node n)
		{
			ScanViewOp scanViewOp = this.m_destCmd.CreateScanViewOp(op.Table.TableMetadata);
			this.MapTable(scanViewOp.Table, op.Table);
			List<Node> args = this.ProcessChildren(n);
			return this.m_destCmd.CreateNode(scanViewOp, args);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000A26BC File Offset: 0x000A08BC
		public override Node Visit(UnnestOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			Var mappedVar = this.GetMappedVar(op.Var);
			Table t = this.m_destCmd.CreateTableInstance(op.Table.TableMetadata);
			UnnestOp unnestOp = this.m_destCmd.CreateUnnestOp(mappedVar, t);
			this.MapTable(unnestOp.Table, op.Table);
			return this.m_destCmd.CreateNode(unnestOp, args);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000A2724 File Offset: 0x000A0924
		public override Node Visit(ProjectOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			VarVec vars = this.Copy(op.Outputs);
			ProjectOp op2 = this.m_destCmd.CreateProjectOp(vars);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000A2760 File Offset: 0x000A0960
		public override Node Visit(FilterOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateFilterOp(), n);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000A2774 File Offset: 0x000A0974
		public override Node Visit(SortOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			List<SortKey> sortKeys = this.Copy(op.Keys);
			SortOp op2 = this.m_destCmd.CreateSortOp(sortKeys);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000A27B0 File Offset: 0x000A09B0
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			List<SortKey> sortKeys = this.Copy(op.Keys);
			ConstrainedSortOp op2 = this.m_destCmd.CreateConstrainedSortOp(sortKeys, op.WithTies);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000A27F4 File Offset: 0x000A09F4
		public override Node Visit(GroupByOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			GroupByOp op2 = this.m_destCmd.CreateGroupByOp(this.Copy(op.Keys), this.Copy(op.Outputs));
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000A283C File Offset: 0x000A0A3C
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			GroupByIntoOp op2 = this.m_destCmd.CreateGroupByIntoOp(this.Copy(op.Keys), this.Copy(op.Inputs), this.Copy(op.Outputs));
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000A288E File Offset: 0x000A0A8E
		public override Node Visit(CrossJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCrossJoinOp(), n);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000A28A2 File Offset: 0x000A0AA2
		public override Node Visit(InnerJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateInnerJoinOp(), n);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000A28B6 File Offset: 0x000A0AB6
		public override Node Visit(LeftOuterJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateLeftOuterJoinOp(), n);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000A28CA File Offset: 0x000A0ACA
		public override Node Visit(FullOuterJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateFullOuterJoinOp(), n);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000A28DE File Offset: 0x000A0ADE
		public override Node Visit(CrossApplyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCrossApplyOp(), n);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000A28F2 File Offset: 0x000A0AF2
		public override Node Visit(OuterApplyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateOuterApplyOp(), n);
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x000A2908 File Offset: 0x000A0B08
		private Node CopySetOp(SetOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			VarMap varMap = new VarMap();
			VarMap varMap2 = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in op.VarMap[0])
			{
				Var var = this.m_destCmd.CreateSetOpVar(keyValuePair.Key.Type);
				this.SetMappedVar(keyValuePair.Key, var);
				varMap.Add(var, this.GetMappedVar(keyValuePair.Value));
				varMap2.Add(var, this.GetMappedVar(op.VarMap[1][keyValuePair.Key]));
			}
			SetOp op2 = null;
			switch (op.OpType)
			{
			case OpType.UnionAll:
			{
				Var var2 = ((UnionAllOp)op).BranchDiscriminator;
				if (var2 != null)
				{
					var2 = this.GetMappedVar(var2);
				}
				op2 = this.m_destCmd.CreateUnionAllOp(varMap, varMap2, var2);
				break;
			}
			case OpType.Intersect:
				op2 = this.m_destCmd.CreateIntersectOp(varMap, varMap2);
				break;
			case OpType.Except:
				op2 = this.m_destCmd.CreateExceptOp(varMap, varMap2);
				break;
			}
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000A2A4C File Offset: 0x000A0C4C
		public override Node Visit(UnionAllOp op, Node n)
		{
			return this.CopySetOp(op, n);
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000A2A56 File Offset: 0x000A0C56
		public override Node Visit(IntersectOp op, Node n)
		{
			return this.CopySetOp(op, n);
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000A2A60 File Offset: 0x000A0C60
		public override Node Visit(ExceptOp op, Node n)
		{
			return this.CopySetOp(op, n);
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000A2A6C File Offset: 0x000A0C6C
		public override Node Visit(DistinctOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			VarVec keyVars = this.Copy(op.Keys);
			DistinctOp op2 = this.m_destCmd.CreateDistinctOp(keyVars);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000A2AA8 File Offset: 0x000A0CA8
		public override Node Visit(SingleRowOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateSingleRowOp(), n);
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000A2ABC File Offset: 0x000A0CBC
		public override Node Visit(SingleRowTableOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateSingleRowTableOp(), n);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000A2AD0 File Offset: 0x000A0CD0
		public override Node Visit(VarDefOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			Var var = this.m_destCmd.CreateComputedVar(op.Var.Type);
			this.SetMappedVar(op.Var, var);
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateVarDefOp(var), args);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000A2B21 File Offset: 0x000A0D21
		public override Node Visit(VarDefListOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateVarDefListOp(), n);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000A2B35 File Offset: 0x000A0D35
		private ColumnMap Copy(ColumnMap columnMap)
		{
			return ColumnMapCopier.Copy(columnMap, this.m_varMap);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x000A2B44 File Offset: 0x000A0D44
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			VarList outputVars = this.Copy(op.Outputs);
			SimpleCollectionColumnMap columnMap = this.Copy(op.ColumnMap) as SimpleCollectionColumnMap;
			PhysicalProjectOp op2 = this.m_destCmd.CreatePhysicalProjectOp(outputVars, columnMap);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000A2B94 File Offset: 0x000A0D94
		private Node VisitNestOp(Node n)
		{
			NestBaseOp nestBaseOp = n.Op as NestBaseOp;
			SingleStreamNestOp singleStreamNestOp = nestBaseOp as SingleStreamNestOp;
			List<Node> args = this.ProcessChildren(n);
			Var discriminatorVar = null;
			if (singleStreamNestOp != null)
			{
				discriminatorVar = this.GetMappedVar(singleStreamNestOp.Discriminator);
			}
			List<CollectionInfo> list = new List<CollectionInfo>();
			foreach (CollectionInfo collectionInfo in nestBaseOp.CollectionInfo)
			{
				ColumnMap columnMap = this.Copy(collectionInfo.ColumnMap);
				Var var = this.m_destCmd.CreateComputedVar(collectionInfo.CollectionVar.Type);
				this.SetMappedVar(collectionInfo.CollectionVar, var);
				VarList flattenedElementVars = this.Copy(collectionInfo.FlattenedElementVars);
				VarVec keys = this.Copy(collectionInfo.Keys);
				List<SortKey> sortKeys = this.Copy(collectionInfo.SortKeys);
				CollectionInfo item = Command.CreateCollectionInfo(var, columnMap, flattenedElementVars, keys, sortKeys, collectionInfo.DiscriminatorValue);
				list.Add(item);
			}
			VarVec outputVars = this.Copy(nestBaseOp.Outputs);
			List<SortKey> prefixSortKeys = this.Copy(nestBaseOp.PrefixSortKeys);
			NestBaseOp op;
			if (singleStreamNestOp != null)
			{
				VarVec keys2 = this.Copy(singleStreamNestOp.Keys);
				List<SortKey> postfixSortKeys = this.Copy(singleStreamNestOp.PostfixSortKeys);
				op = this.m_destCmd.CreateSingleStreamNestOp(keys2, prefixSortKeys, postfixSortKeys, outputVars, list, discriminatorVar);
			}
			else
			{
				op = this.m_destCmd.CreateMultiStreamNestOp(prefixSortKeys, outputVars, list);
			}
			return this.m_destCmd.CreateNode(op, args);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000A2D1C File Offset: 0x000A0F1C
		public override Node Visit(SingleStreamNestOp op, Node n)
		{
			return this.VisitNestOp(n);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000A2D25 File Offset: 0x000A0F25
		public override Node Visit(MultiStreamNestOp op, Node n)
		{
			return this.VisitNestOp(n);
		}

		// Token: 0x04000C38 RID: 3128
		private readonly Command m_srcCmd;

		// Token: 0x04000C39 RID: 3129
		protected Command m_destCmd;

		// Token: 0x04000C3A RID: 3130
		protected VarMap m_varMap;
	}
}
