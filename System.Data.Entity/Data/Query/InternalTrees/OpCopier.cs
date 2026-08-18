using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000B9 RID: 185
	internal class OpCopier : BasicOpVisitorOfNode
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x0003AEA8 File Offset: 0x000390A8
		internal static Node Copy(Command cmd, Node n)
		{
			VarMap varMap;
			return OpCopier.Copy(cmd, n, out varMap);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003AEC0 File Offset: 0x000390C0
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

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003AF2C File Offset: 0x0003912C
		internal static Node Copy(Command cmd, Node n, out VarMap varMap)
		{
			OpCopier opCopier = new OpCopier(cmd);
			Node result = opCopier.CopyNode(n);
			varMap = opCopier.m_varMap;
			return result;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0003AF54 File Offset: 0x00039154
		internal static List<SortKey> Copy(Command cmd, List<SortKey> sortKeys)
		{
			OpCopier opCopier = new OpCopier(cmd);
			return opCopier.Copy(sortKeys);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0003AF6F File Offset: 0x0003916F
		protected OpCopier(Command cmd) : this(cmd, cmd)
		{
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0003AF79 File Offset: 0x00039179
		private OpCopier(Command destCommand, Command sourceCommand)
		{
			this.m_srcCmd = sourceCommand;
			this.m_destCmd = destCommand;
			this.m_varMap = new VarMap();
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0003AF9C File Offset: 0x0003919C
		private Var GetMappedVar(Var v)
		{
			Var result;
			if (this.m_varMap.TryGetValue(v, out result))
			{
				return result;
			}
			if (this.m_destCmd != this.m_srcCmd)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnknownVar, 6);
			}
			return v;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0003AFD6 File Offset: 0x000391D6
		private void SetMappedVar(Var v, Var mappedVar)
		{
			this.m_varMap.Add(v, mappedVar);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0003AFE8 File Offset: 0x000391E8
		private void MapTable(Table newTable, Table oldTable)
		{
			for (int i = 0; i < oldTable.Columns.Count; i++)
			{
				this.SetMappedVar(oldTable.Columns[i], newTable.Columns[i]);
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0003B029 File Offset: 0x00039229
		private IEnumerable<Var> MapVars(IEnumerable<Var> vars)
		{
			foreach (Var v in vars)
			{
				Var mappedVar = this.GetMappedVar(v);
				yield return mappedVar;
			}
			IEnumerator<Var> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0003B040 File Offset: 0x00039240
		private VarVec Copy(VarVec vars)
		{
			return this.m_destCmd.CreateVarVec(this.MapVars(vars));
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0003B064 File Offset: 0x00039264
		private VarList Copy(VarList varList)
		{
			return Command.CreateVarList(this.MapVars(varList));
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0003B07F File Offset: 0x0003927F
		private SortKey Copy(SortKey sortKey)
		{
			return Command.CreateSortKey(this.GetMappedVar(sortKey.Var), sortKey.AscendingSort, sortKey.Collation);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0003B0A0 File Offset: 0x000392A0
		private List<SortKey> Copy(List<SortKey> sortKeys)
		{
			List<SortKey> list = new List<SortKey>();
			foreach (SortKey sortKey in sortKeys)
			{
				list.Add(this.Copy(sortKey));
			}
			return list;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0003B0FC File Offset: 0x000392FC
		protected Node CopyNode(Node n)
		{
			return n.Op.Accept<Node>(this, n);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0003B10C File Offset: 0x0003930C
		private List<Node> ProcessChildren(Node n)
		{
			List<Node> list = new List<Node>();
			foreach (Node n2 in n.Children)
			{
				list.Add(this.CopyNode(n2));
			}
			return list;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0003B16C File Offset: 0x0003936C
		private Node CopyDefault(Op op, Node original)
		{
			return this.m_destCmd.CreateNode(op, this.ProcessChildren(original));
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0003B181 File Offset: 0x00039381
		public override Node Visit(Op op, Node n)
		{
			throw new NotSupportedException(Strings.Iqt_General_UnsupportedOp(op.GetType().FullName));
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0003B198 File Offset: 0x00039398
		public override Node Visit(ConstantOp op, Node n)
		{
			ConstantBaseOp op2 = this.m_destCmd.CreateConstantOp(op.Type, op.Value);
			return this.m_destCmd.CreateNode(op2);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0003B1C9 File Offset: 0x000393C9
		public override Node Visit(NullOp op, Node n)
		{
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateNullOp(op.Type));
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0003B1E7 File Offset: 0x000393E7
		public override Node Visit(ConstantPredicateOp op, Node n)
		{
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateConstantPredicateOp(op.Value));
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0003B208 File Offset: 0x00039408
		public override Node Visit(InternalConstantOp op, Node n)
		{
			InternalConstantOp op2 = this.m_destCmd.CreateInternalConstantOp(op.Type, op.Value);
			return this.m_destCmd.CreateNode(op2);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0003B23C File Offset: 0x0003943C
		public override Node Visit(NullSentinelOp op, Node n)
		{
			NullSentinelOp op2 = this.m_destCmd.CreateNullSentinelOp();
			return this.m_destCmd.CreateNode(op2);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0003B261 File Offset: 0x00039461
		public override Node Visit(FunctionOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateFunctionOp(op.Function), n);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0003B27B File Offset: 0x0003947B
		public override Node Visit(PropertyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreatePropertyOp(op.PropertyInfo), n);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003B295 File Offset: 0x00039495
		public override Node Visit(RelPropertyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateRelPropertyOp(op.PropertyInfo), n);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0003B2AF File Offset: 0x000394AF
		public override Node Visit(CaseOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCaseOp(op.Type), n);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003B2C9 File Offset: 0x000394C9
		public override Node Visit(ComparisonOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateComparisonOp(op.OpType), n);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0003B2E3 File Offset: 0x000394E3
		public override Node Visit(LikeOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateLikeOp(), n);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0003B2F7 File Offset: 0x000394F7
		public override Node Visit(AggregateOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateAggregateOp(op.AggFunc, op.IsDistinctAggregate), n);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003B317 File Offset: 0x00039517
		public override Node Visit(NewInstanceOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNewInstanceOp(op.Type), n);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0003B334 File Offset: 0x00039534
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

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0003B389 File Offset: 0x00039589
		public override Node Visit(DiscriminatedNewEntityOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateDiscriminatedNewEntityOp(op.Type, op.DiscriminatorMap, op.EntitySet, op.RelationshipProperties), n);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0003B3B5 File Offset: 0x000395B5
		public override Node Visit(NewMultisetOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNewMultisetOp(op.Type), n);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0003B3CF File Offset: 0x000395CF
		public override Node Visit(NewRecordOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNewRecordOp(op.Type), n);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0003B3E9 File Offset: 0x000395E9
		public override Node Visit(RefOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateRefOp(op.EntitySet, op.Type), n);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0003B40C File Offset: 0x0003960C
		public override Node Visit(VarRefOp op, Node n)
		{
			Var var;
			if (!this.m_varMap.TryGetValue(op.Var, out var))
			{
				var = op.Var;
			}
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateVarRefOp(var));
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0003B44C File Offset: 0x0003964C
		public override Node Visit(ConditionalOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateConditionalOp(op.OpType), n);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0003B466 File Offset: 0x00039666
		public override Node Visit(ArithmeticOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateArithmeticOp(op.OpType, op.Type), n);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0003B488 File Offset: 0x00039688
		public override Node Visit(TreatOp op, Node n)
		{
			TreatOp op2 = op.IsFakeTreat ? this.m_destCmd.CreateFakeTreatOp(op.Type) : this.m_destCmd.CreateTreatOp(op.Type);
			return this.CopyDefault(op2, n);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0003B4CA File Offset: 0x000396CA
		public override Node Visit(CastOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCastOp(op.Type), n);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0003B4E4 File Offset: 0x000396E4
		public override Node Visit(SoftCastOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateSoftCastOp(op.Type), n);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0003B4FE File Offset: 0x000396FE
		public override Node Visit(DerefOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateDerefOp(op.Type), n);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0003B518 File Offset: 0x00039718
		public override Node Visit(NavigateOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateNavigateOp(op.Type, op.RelProperty), n);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0003B538 File Offset: 0x00039738
		public override Node Visit(IsOfOp op, Node n)
		{
			if (op.IsOfOnly)
			{
				return this.CopyDefault(this.m_destCmd.CreateIsOfOnlyOp(op.IsOfType), n);
			}
			return this.CopyDefault(this.m_destCmd.CreateIsOfOp(op.IsOfType), n);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0003B573 File Offset: 0x00039773
		public override Node Visit(ExistsOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateExistsOp(), n);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0003B587 File Offset: 0x00039787
		public override Node Visit(ElementOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateElementOp(op.Type), n);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003B5A1 File Offset: 0x000397A1
		public override Node Visit(GetRefKeyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateGetRefKeyOp(op.Type), n);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0003B5BB File Offset: 0x000397BB
		public override Node Visit(GetEntityRefOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateGetEntityRefOp(op.Type), n);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0003B5D5 File Offset: 0x000397D5
		public override Node Visit(CollectOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCollectOp(op.Type), n);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0003B5F0 File Offset: 0x000397F0
		public override Node Visit(ScanTableOp op, Node n)
		{
			ScanTableOp scanTableOp = this.m_destCmd.CreateScanTableOp(op.Table.TableMetadata);
			this.MapTable(scanTableOp.Table, op.Table);
			return this.m_destCmd.CreateNode(scanTableOp);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0003B634 File Offset: 0x00039834
		public override Node Visit(ScanViewOp op, Node n)
		{
			ScanViewOp scanViewOp = this.m_destCmd.CreateScanViewOp(op.Table.TableMetadata);
			this.MapTable(scanViewOp.Table, op.Table);
			List<Node> args = this.ProcessChildren(n);
			return this.m_destCmd.CreateNode(scanViewOp, args);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0003B680 File Offset: 0x00039880
		public override Node Visit(UnnestOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			Var mappedVar = this.GetMappedVar(op.Var);
			Table t = this.m_destCmd.CreateTableInstance(op.Table.TableMetadata);
			UnnestOp unnestOp = this.m_destCmd.CreateUnnestOp(mappedVar, t);
			this.MapTable(unnestOp.Table, op.Table);
			return this.m_destCmd.CreateNode(unnestOp, args);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0003B6E8 File Offset: 0x000398E8
		public override Node Visit(ProjectOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			VarVec vars = this.Copy(op.Outputs);
			ProjectOp op2 = this.m_destCmd.CreateProjectOp(vars);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0003B724 File Offset: 0x00039924
		public override Node Visit(FilterOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateFilterOp(), n);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0003B738 File Offset: 0x00039938
		public override Node Visit(SortOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			List<SortKey> sortKeys = this.Copy(op.Keys);
			SortOp op2 = this.m_destCmd.CreateSortOp(sortKeys);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0003B774 File Offset: 0x00039974
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			List<SortKey> sortKeys = this.Copy(op.Keys);
			ConstrainedSortOp op2 = this.m_destCmd.CreateConstrainedSortOp(sortKeys, op.WithTies);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0003B7B8 File Offset: 0x000399B8
		public override Node Visit(GroupByOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			GroupByOp op2 = this.m_destCmd.CreateGroupByOp(this.Copy(op.Keys), this.Copy(op.Outputs));
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0003B800 File Offset: 0x00039A00
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			GroupByIntoOp op2 = this.m_destCmd.CreateGroupByIntoOp(this.Copy(op.Keys), this.Copy(op.Inputs), this.Copy(op.Outputs));
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0003B852 File Offset: 0x00039A52
		public override Node Visit(CrossJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCrossJoinOp(), n);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003B866 File Offset: 0x00039A66
		public override Node Visit(InnerJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateInnerJoinOp(), n);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0003B87A File Offset: 0x00039A7A
		public override Node Visit(LeftOuterJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateLeftOuterJoinOp(), n);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0003B88E File Offset: 0x00039A8E
		public override Node Visit(FullOuterJoinOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateFullOuterJoinOp(), n);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003B8A2 File Offset: 0x00039AA2
		public override Node Visit(CrossApplyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateCrossApplyOp(), n);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0003B8B6 File Offset: 0x00039AB6
		public override Node Visit(OuterApplyOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateOuterApplyOp(), n);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0003B8CC File Offset: 0x00039ACC
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

		// Token: 0x06000BDA RID: 3034 RVA: 0x0003BA0C File Offset: 0x00039C0C
		public override Node Visit(UnionAllOp op, Node n)
		{
			return this.CopySetOp(op, n);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0003BA0C File Offset: 0x00039C0C
		public override Node Visit(IntersectOp op, Node n)
		{
			return this.CopySetOp(op, n);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0003BA0C File Offset: 0x00039C0C
		public override Node Visit(ExceptOp op, Node n)
		{
			return this.CopySetOp(op, n);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0003BA18 File Offset: 0x00039C18
		public override Node Visit(DistinctOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			VarVec keyVars = this.Copy(op.Keys);
			DistinctOp op2 = this.m_destCmd.CreateDistinctOp(keyVars);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0003BA54 File Offset: 0x00039C54
		public override Node Visit(SingleRowOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateSingleRowOp(), n);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0003BA68 File Offset: 0x00039C68
		public override Node Visit(SingleRowTableOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateSingleRowTableOp(), n);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003BA7C File Offset: 0x00039C7C
		public override Node Visit(VarDefOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			Var var = this.m_destCmd.CreateComputedVar(op.Var.Type);
			this.SetMappedVar(op.Var, var);
			return this.m_destCmd.CreateNode(this.m_destCmd.CreateVarDefOp(var), args);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0003BACD File Offset: 0x00039CCD
		public override Node Visit(VarDefListOp op, Node n)
		{
			return this.CopyDefault(this.m_destCmd.CreateVarDefListOp(), n);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0003BAE1 File Offset: 0x00039CE1
		private ColumnMap Copy(ColumnMap columnMap)
		{
			return ColumnMapCopier.Copy(columnMap, this.m_varMap);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0003BAF0 File Offset: 0x00039CF0
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			List<Node> args = this.ProcessChildren(n);
			VarList outputVars = this.Copy(op.Outputs);
			SimpleCollectionColumnMap columnMap = this.Copy(op.ColumnMap) as SimpleCollectionColumnMap;
			PhysicalProjectOp op2 = this.m_destCmd.CreatePhysicalProjectOp(outputVars, columnMap);
			return this.m_destCmd.CreateNode(op2, args);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0003BB40 File Offset: 0x00039D40
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

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0003BCC8 File Offset: 0x00039EC8
		public override Node Visit(SingleStreamNestOp op, Node n)
		{
			return this.VisitNestOp(n);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0003BCC8 File Offset: 0x00039EC8
		public override Node Visit(MultiStreamNestOp op, Node n)
		{
			return this.VisitNestOp(n);
		}

		// Token: 0x040008FF RID: 2303
		private Command m_srcCmd;

		// Token: 0x04000900 RID: 2304
		protected Command m_destCmd;

		// Token: 0x04000901 RID: 2305
		protected VarMap m_varMap;
	}
}
