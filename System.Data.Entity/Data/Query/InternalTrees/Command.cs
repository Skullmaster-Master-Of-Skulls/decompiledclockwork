using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Query.PlanCompiler;
using System.Linq;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000AE RID: 174
	internal class Command
	{
		// Token: 0x06000A7A RID: 2682 RVA: 0x00036C94 File Offset: 0x00034E94
		internal Command(MetadataWorkspace metadataWorkspace)
		{
			this.m_parameterMap = new Dictionary<string, ParameterVar>();
			this.m_vars = new List<Var>();
			this.m_tables = new List<Table>();
			this.m_metadataWorkspace = metadataWorkspace;
			if (!this.TryGetPrimitiveType(PrimitiveTypeKind.Boolean, out this.m_boolType))
			{
				throw EntityUtil.ProviderIncompatible(Strings.Cqt_General_NoProviderBooleanType);
			}
			if (!this.TryGetPrimitiveType(PrimitiveTypeKind.Int32, out this.m_intType))
			{
				throw EntityUtil.ProviderIncompatible(Strings.Cqt_General_NoProviderIntegerType);
			}
			if (!this.TryGetPrimitiveType(PrimitiveTypeKind.String, out this.m_stringType))
			{
				throw EntityUtil.ProviderIncompatible(Strings.Cqt_General_NoProviderStringType);
			}
			this.m_trueOp = new ConstantPredicateOp(this.m_boolType, true);
			this.m_falseOp = new ConstantPredicateOp(this.m_boolType, false);
			this.m_nodeInfoVisitor = new NodeInfoVisitor(this);
			this.m_keyPullupVisitor = new KeyPullup(this);
			this.m_freeVarVecEnumerators = new Stack<VarVec.VarVecEnumerator>();
			this.m_freeVarVecs = new Stack<VarVec>();
			this.m_referencedRelProperties = new HashSet<RelProperty>();
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00036D87 File Offset: 0x00034F87
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.m_metadataWorkspace;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00036D8F File Offset: 0x00034F8F
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00036D97 File Offset: 0x00034F97
		internal Node Root
		{
			get
			{
				return this.m_root;
			}
			set
			{
				this.m_root = value;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00036DA0 File Offset: 0x00034FA0
		internal void DisableVarVecEnumCaching()
		{
			this.m_disableVarVecEnumCaching = true;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00036DAC File Offset: 0x00034FAC
		internal int NextBranchDiscriminatorValue
		{
			get
			{
				int nextBranchDiscriminatorValue = this.m_nextBranchDiscriminatorValue;
				this.m_nextBranchDiscriminatorValue = nextBranchDiscriminatorValue + 1;
				return nextBranchDiscriminatorValue;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00036DCA File Offset: 0x00034FCA
		internal int NextNodeId
		{
			get
			{
				return this.m_nextNodeId;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00036DD2 File Offset: 0x00034FD2
		internal TypeUsage BooleanType
		{
			get
			{
				return this.m_boolType;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00036DDA File Offset: 0x00034FDA
		internal TypeUsage IntegerType
		{
			get
			{
				return this.m_intType;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00036DE2 File Offset: 0x00034FE2
		internal TypeUsage StringType
		{
			get
			{
				return this.m_stringType;
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00036DEA File Offset: 0x00034FEA
		private bool TryGetPrimitiveType(PrimitiveTypeKind modelType, out TypeUsage type)
		{
			type = null;
			if (modelType == PrimitiveTypeKind.String)
			{
				type = TypeUsage.CreateStringTypeUsage(this.m_metadataWorkspace.GetModelPrimitiveType(modelType), false, false);
			}
			else
			{
				type = this.m_metadataWorkspace.GetCanonicalModelTypeUsage(modelType);
			}
			return type != null;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00036E20 File Offset: 0x00035020
		internal VarVec CreateVarVec()
		{
			VarVec varVec;
			if (this.m_freeVarVecs.Count == 0)
			{
				varVec = new VarVec(this);
			}
			else
			{
				varVec = this.m_freeVarVecs.Pop();
				varVec.Clear();
			}
			return varVec;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00036E58 File Offset: 0x00035058
		internal VarVec CreateVarVec(Var v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.Set(v);
			return varVec;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00036E74 File Offset: 0x00035074
		internal VarVec CreateVarVec(IEnumerable<Var> v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.InitFrom(v);
			return varVec;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00036E90 File Offset: 0x00035090
		internal VarVec CreateVarVec(VarVec v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.InitFrom(v);
			return varVec;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00036EAC File Offset: 0x000350AC
		internal void ReleaseVarVec(VarVec vec)
		{
			this.m_freeVarVecs.Push(vec);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00036EBC File Offset: 0x000350BC
		internal VarVec.VarVecEnumerator GetVarVecEnumerator(VarVec vec)
		{
			VarVec.VarVecEnumerator varVecEnumerator;
			if (this.m_disableVarVecEnumCaching || this.m_freeVarVecEnumerators.Count == 0)
			{
				varVecEnumerator = new VarVec.VarVecEnumerator(vec);
			}
			else
			{
				varVecEnumerator = this.m_freeVarVecEnumerators.Pop();
				varVecEnumerator.Init(vec);
			}
			return varVecEnumerator;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00036EFB File Offset: 0x000350FB
		internal void ReleaseVarVecEnumerator(VarVec.VarVecEnumerator enumerator)
		{
			if (!this.m_disableVarVecEnumCaching)
			{
				this.m_freeVarVecEnumerators.Push(enumerator);
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00036F11 File Offset: 0x00035111
		internal static VarList CreateVarList()
		{
			return new VarList();
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00036F18 File Offset: 0x00035118
		internal static VarList CreateVarList(IEnumerable<Var> vars)
		{
			return new VarList(vars);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00036F20 File Offset: 0x00035120
		internal VarMap CreateVarMap()
		{
			return new VarMap();
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00036F27 File Offset: 0x00035127
		private int NewTableId()
		{
			return this.m_tables.Count;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00036F34 File Offset: 0x00035134
		internal static TableMD CreateTableDefinition(TypeUsage elementType)
		{
			return new TableMD(elementType, null);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00036F3D File Offset: 0x0003513D
		internal static TableMD CreateTableDefinition(EntitySetBase extent)
		{
			return new TableMD(TypeUsage.Create(extent.ElementType), extent);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00036F50 File Offset: 0x00035150
		internal TableMD CreateFlatTableDefinition(RowType type)
		{
			return this.CreateFlatTableDefinition(type.Properties, new List<EdmMember>(), null);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00036F64 File Offset: 0x00035164
		internal TableMD CreateFlatTableDefinition(IEnumerable<EdmProperty> properties, IEnumerable<EdmMember> keyMembers, EntitySetBase entitySet)
		{
			return new TableMD(properties, keyMembers, entitySet);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00036F70 File Offset: 0x00035170
		internal Table CreateTableInstance(TableMD tableMetadata)
		{
			Table table = new Table(this, tableMetadata, this.NewTableId());
			this.m_tables.Add(table);
			return table;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00036F98 File Offset: 0x00035198
		internal IEnumerable<Var> Vars
		{
			get
			{
				return from v in this.m_vars
				where v.VarType != VarType.NotValid
				select v;
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00036FC4 File Offset: 0x000351C4
		internal Var GetVar(int id)
		{
			return this.m_vars[id];
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00036FD2 File Offset: 0x000351D2
		internal ParameterVar GetParameter(string paramName)
		{
			return this.m_parameterMap[paramName];
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00036FE0 File Offset: 0x000351E0
		private int NewVarId()
		{
			return this.m_vars.Count;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00036FF0 File Offset: 0x000351F0
		internal ParameterVar CreateParameterVar(string parameterName, TypeUsage parameterType)
		{
			if (this.m_parameterMap.ContainsKey(parameterName))
			{
				throw new Exception("duplicate parameter name: " + parameterName);
			}
			ParameterVar parameterVar = new ParameterVar(this.NewVarId(), parameterType, parameterName);
			this.m_vars.Add(parameterVar);
			this.m_parameterMap[parameterName] = parameterVar;
			return parameterVar;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00037044 File Offset: 0x00035244
		private ParameterVar ReplaceParameterVar(ParameterVar oldVar, Func<TypeUsage, TypeUsage> generateReplacementType)
		{
			ParameterVar parameterVar = new ParameterVar(this.NewVarId(), generateReplacementType(oldVar.Type), oldVar.ParameterName);
			this.m_parameterMap[oldVar.ParameterName] = parameterVar;
			this.m_vars.Add(parameterVar);
			return parameterVar;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0003708E File Offset: 0x0003528E
		internal ParameterVar ReplaceEnumParameterVar(ParameterVar oldVar)
		{
			return this.ReplaceParameterVar(oldVar, (TypeUsage t) => TypeHelpers.CreateEnumUnderlyingTypeUsage(t));
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000370B6 File Offset: 0x000352B6
		internal ParameterVar ReplaceStrongSpatialParameterVar(ParameterVar oldVar)
		{
			return this.ReplaceParameterVar(oldVar, (TypeUsage t) => TypeHelpers.CreateSpatialUnionTypeUsage(t));
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000370E0 File Offset: 0x000352E0
		internal ColumnVar CreateColumnVar(Table table, ColumnMD columnMD)
		{
			ColumnVar columnVar = new ColumnVar(this.NewVarId(), table, columnMD);
			table.Columns.Add(columnVar);
			this.m_vars.Add(columnVar);
			return columnVar;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00037114 File Offset: 0x00035314
		internal ComputedVar CreateComputedVar(TypeUsage type)
		{
			ComputedVar computedVar = new ComputedVar(this.NewVarId(), type);
			this.m_vars.Add(computedVar);
			return computedVar;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0003713C File Offset: 0x0003533C
		internal SetOpVar CreateSetOpVar(TypeUsage type)
		{
			SetOpVar setOpVar = new SetOpVar(this.NewVarId(), type);
			this.m_vars.Add(setOpVar);
			return setOpVar;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00037163 File Offset: 0x00035363
		internal Node CreateNode(Op op)
		{
			return this.CreateNode(op, new List<Node>());
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00037174 File Offset: 0x00035374
		internal Node CreateNode(Op op, Node arg1)
		{
			return this.CreateNode(op, new List<Node>
			{
				arg1
			});
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00037198 File Offset: 0x00035398
		internal Node CreateNode(Op op, Node arg1, Node arg2)
		{
			return this.CreateNode(op, new List<Node>
			{
				arg1,
				arg2
			});
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000371C4 File Offset: 0x000353C4
		internal Node CreateNode(Op op, Node arg1, Node arg2, Node arg3)
		{
			return this.CreateNode(op, new List<Node>
			{
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000371F8 File Offset: 0x000353F8
		internal Node CreateNode(Op op, IList<Node> args)
		{
			int nextNodeId = this.m_nextNodeId;
			this.m_nextNodeId = nextNodeId + 1;
			return new Node(nextNodeId, op, new List<Node>(args));
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00037224 File Offset: 0x00035424
		internal Node CreateNode(Op op, List<Node> args)
		{
			int nextNodeId = this.m_nextNodeId;
			this.m_nextNodeId = nextNodeId + 1;
			return new Node(nextNodeId, op, args);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00037249 File Offset: 0x00035449
		internal ConstantBaseOp CreateConstantOp(TypeUsage type, object value)
		{
			if (value == null)
			{
				return new NullOp(type);
			}
			if (TypeSemantics.IsBooleanType(type))
			{
				return new InternalConstantOp(type, value);
			}
			return new ConstantOp(type, value);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0003726C File Offset: 0x0003546C
		internal InternalConstantOp CreateInternalConstantOp(TypeUsage type, object value)
		{
			return new InternalConstantOp(type, value);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00037275 File Offset: 0x00035475
		internal NullSentinelOp CreateNullSentinelOp()
		{
			return new NullSentinelOp(this.IntegerType, 1);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00037288 File Offset: 0x00035488
		internal NullOp CreateNullOp(TypeUsage type)
		{
			return new NullOp(type);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00037290 File Offset: 0x00035490
		internal ConstantPredicateOp CreateConstantPredicateOp(bool value)
		{
			if (!value)
			{
				return this.m_falseOp;
			}
			return this.m_trueOp;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000372A2 File Offset: 0x000354A2
		internal ConstantPredicateOp CreateTrueOp()
		{
			return this.m_trueOp;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000372AA File Offset: 0x000354AA
		internal ConstantPredicateOp CreateFalseOp()
		{
			return this.m_falseOp;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000372B2 File Offset: 0x000354B2
		internal FunctionOp CreateFunctionOp(EdmFunction function)
		{
			return new FunctionOp(function);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000372BA File Offset: 0x000354BA
		internal TreatOp CreateTreatOp(TypeUsage type)
		{
			return new TreatOp(type, false);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000372C3 File Offset: 0x000354C3
		internal TreatOp CreateFakeTreatOp(TypeUsage type)
		{
			return new TreatOp(type, true);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000372CC File Offset: 0x000354CC
		internal IsOfOp CreateIsOfOp(TypeUsage isOfType)
		{
			return new IsOfOp(isOfType, false, this.m_boolType);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000372DB File Offset: 0x000354DB
		internal IsOfOp CreateIsOfOnlyOp(TypeUsage isOfType)
		{
			return new IsOfOp(isOfType, true, this.m_boolType);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000372EA File Offset: 0x000354EA
		internal CastOp CreateCastOp(TypeUsage type)
		{
			return new CastOp(type);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000372F2 File Offset: 0x000354F2
		internal SoftCastOp CreateSoftCastOp(TypeUsage type)
		{
			return new SoftCastOp(type);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000372FA File Offset: 0x000354FA
		internal ComparisonOp CreateComparisonOp(OpType opType)
		{
			return new ComparisonOp(opType, this.BooleanType);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00037308 File Offset: 0x00035508
		internal LikeOp CreateLikeOp()
		{
			return new LikeOp(this.BooleanType);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00037315 File Offset: 0x00035515
		internal ConditionalOp CreateConditionalOp(OpType opType)
		{
			return new ConditionalOp(opType, this.BooleanType);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00037323 File Offset: 0x00035523
		internal CaseOp CreateCaseOp(TypeUsage type)
		{
			return new CaseOp(type);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0003732B File Offset: 0x0003552B
		internal AggregateOp CreateAggregateOp(EdmFunction aggFunc, bool distinctAgg)
		{
			return new AggregateOp(aggFunc, distinctAgg);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00037334 File Offset: 0x00035534
		internal NewInstanceOp CreateNewInstanceOp(TypeUsage type)
		{
			return new NewInstanceOp(type);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0003733C File Offset: 0x0003553C
		internal NewEntityOp CreateScopedNewEntityOp(TypeUsage type, List<RelProperty> relProperties, EntitySet entitySet)
		{
			return new NewEntityOp(type, relProperties, true, entitySet);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00037347 File Offset: 0x00035547
		internal NewEntityOp CreateNewEntityOp(TypeUsage type, List<RelProperty> relProperties)
		{
			return new NewEntityOp(type, relProperties, false, null);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00037352 File Offset: 0x00035552
		internal DiscriminatedNewEntityOp CreateDiscriminatedNewEntityOp(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap, EntitySet entitySet, List<RelProperty> relProperties)
		{
			return new DiscriminatedNewEntityOp(type, discriminatorMap, entitySet, relProperties);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0003735E File Offset: 0x0003555E
		internal NewMultisetOp CreateNewMultisetOp(TypeUsage type)
		{
			return new NewMultisetOp(type);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00037366 File Offset: 0x00035566
		internal NewRecordOp CreateNewRecordOp(TypeUsage type)
		{
			return new NewRecordOp(type);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0003736E File Offset: 0x0003556E
		internal NewRecordOp CreateNewRecordOp(RowType type)
		{
			return new NewRecordOp(TypeUsage.Create(type));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0003737B File Offset: 0x0003557B
		internal NewRecordOp CreateNewRecordOp(TypeUsage type, List<EdmProperty> fields)
		{
			return new NewRecordOp(type, fields);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00037384 File Offset: 0x00035584
		internal VarRefOp CreateVarRefOp(Var v)
		{
			return new VarRefOp(v);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0003738C File Offset: 0x0003558C
		internal ArithmeticOp CreateArithmeticOp(OpType opType, TypeUsage type)
		{
			return new ArithmeticOp(opType, type);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00037398 File Offset: 0x00035598
		internal PropertyOp CreatePropertyOp(EdmMember prop)
		{
			NavigationProperty navigationProperty = prop as NavigationProperty;
			if (navigationProperty != null)
			{
				RelProperty relProperty = new RelProperty(navigationProperty.RelationshipType, navigationProperty.FromEndMember, navigationProperty.ToEndMember);
				this.AddRelPropertyReference(relProperty);
				RelProperty relProperty2 = new RelProperty(navigationProperty.RelationshipType, navigationProperty.ToEndMember, navigationProperty.FromEndMember);
				this.AddRelPropertyReference(relProperty2);
			}
			return new PropertyOp(Helper.GetModelTypeUsage(prop), prop);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000373F9 File Offset: 0x000355F9
		internal RelPropertyOp CreateRelPropertyOp(RelProperty prop)
		{
			this.AddRelPropertyReference(prop);
			return new RelPropertyOp(prop.ToEnd.TypeUsage, prop);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00037413 File Offset: 0x00035613
		internal RefOp CreateRefOp(EntitySet entitySet, TypeUsage type)
		{
			return new RefOp(entitySet, type);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0003741C File Offset: 0x0003561C
		internal ExistsOp CreateExistsOp()
		{
			return new ExistsOp(this.BooleanType);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00037429 File Offset: 0x00035629
		internal ElementOp CreateElementOp(TypeUsage type)
		{
			return new ElementOp(type);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00037431 File Offset: 0x00035631
		internal GetEntityRefOp CreateGetEntityRefOp(TypeUsage type)
		{
			return new GetEntityRefOp(type);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00037439 File Offset: 0x00035639
		internal GetRefKeyOp CreateGetRefKeyOp(TypeUsage type)
		{
			return new GetRefKeyOp(type);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00037441 File Offset: 0x00035641
		internal CollectOp CreateCollectOp(TypeUsage type)
		{
			return new CollectOp(type);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00037449 File Offset: 0x00035649
		internal DerefOp CreateDerefOp(TypeUsage type)
		{
			return new DerefOp(type);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00037451 File Offset: 0x00035651
		internal NavigateOp CreateNavigateOp(TypeUsage type, RelProperty relProperty)
		{
			this.AddRelPropertyReference(relProperty);
			return new NavigateOp(type, relProperty);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00037461 File Offset: 0x00035661
		internal VarDefListOp CreateVarDefListOp()
		{
			return VarDefListOp.Instance;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00037468 File Offset: 0x00035668
		internal VarDefOp CreateVarDefOp(Var v)
		{
			return new VarDefOp(v);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00037470 File Offset: 0x00035670
		internal Node CreateVarDefNode(Node definingExpr, out Var computedVar)
		{
			ScalarOp scalarOp = definingExpr.Op as ScalarOp;
			computedVar = this.CreateComputedVar(scalarOp.Type);
			VarDefOp op = this.CreateVarDefOp(computedVar);
			return this.CreateNode(op, definingExpr);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000374AC File Offset: 0x000356AC
		internal Node CreateVarDefListNode(Node definingExpr, out Var computedVar)
		{
			Node arg = this.CreateVarDefNode(definingExpr, out computedVar);
			VarDefListOp op = this.CreateVarDefListOp();
			return this.CreateNode(op, arg);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000374D4 File Offset: 0x000356D4
		internal ScanTableOp CreateScanTableOp(TableMD tableMetadata)
		{
			Table table = this.CreateTableInstance(tableMetadata);
			return this.CreateScanTableOp(table);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000374F0 File Offset: 0x000356F0
		internal ScanTableOp CreateScanTableOp(Table table)
		{
			return new ScanTableOp(table);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000374F8 File Offset: 0x000356F8
		internal ScanViewOp CreateScanViewOp(Table table)
		{
			return new ScanViewOp(table);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00037500 File Offset: 0x00035700
		internal ScanViewOp CreateScanViewOp(TableMD tableMetadata)
		{
			Table table = this.CreateTableInstance(tableMetadata);
			return this.CreateScanViewOp(table);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0003751C File Offset: 0x0003571C
		internal UnnestOp CreateUnnestOp(Var v)
		{
			Table t = this.CreateTableInstance(Command.CreateTableDefinition(TypeHelpers.GetEdmType<CollectionType>(v.Type).TypeUsage));
			return this.CreateUnnestOp(v, t);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0003754D File Offset: 0x0003574D
		internal UnnestOp CreateUnnestOp(Var v, Table t)
		{
			return new UnnestOp(v, t);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00037556 File Offset: 0x00035756
		internal FilterOp CreateFilterOp()
		{
			return FilterOp.Instance;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0003755D File Offset: 0x0003575D
		internal ProjectOp CreateProjectOp(VarVec vars)
		{
			return new ProjectOp(vars);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00037568 File Offset: 0x00035768
		internal ProjectOp CreateProjectOp(Var v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.Set(v);
			return new ProjectOp(varVec);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00037589 File Offset: 0x00035789
		internal InnerJoinOp CreateInnerJoinOp()
		{
			return InnerJoinOp.Instance;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00037590 File Offset: 0x00035790
		internal LeftOuterJoinOp CreateLeftOuterJoinOp()
		{
			return LeftOuterJoinOp.Instance;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00037597 File Offset: 0x00035797
		internal FullOuterJoinOp CreateFullOuterJoinOp()
		{
			return FullOuterJoinOp.Instance;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0003759E File Offset: 0x0003579E
		internal CrossJoinOp CreateCrossJoinOp()
		{
			return CrossJoinOp.Instance;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000375A5 File Offset: 0x000357A5
		internal CrossApplyOp CreateCrossApplyOp()
		{
			return CrossApplyOp.Instance;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000375AC File Offset: 0x000357AC
		internal OuterApplyOp CreateOuterApplyOp()
		{
			return OuterApplyOp.Instance;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000375B3 File Offset: 0x000357B3
		internal static SortKey CreateSortKey(Var v, bool asc, string collation)
		{
			return new SortKey(v, asc, collation);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000375BD File Offset: 0x000357BD
		internal static SortKey CreateSortKey(Var v, bool asc)
		{
			return new SortKey(v, asc, "");
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000375CB File Offset: 0x000357CB
		internal static SortKey CreateSortKey(Var v)
		{
			return new SortKey(v, true, "");
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000375D9 File Offset: 0x000357D9
		internal SortOp CreateSortOp(List<SortKey> sortKeys)
		{
			return new SortOp(sortKeys);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000375E1 File Offset: 0x000357E1
		internal ConstrainedSortOp CreateConstrainedSortOp(List<SortKey> sortKeys)
		{
			return new ConstrainedSortOp(sortKeys, false);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000375EA File Offset: 0x000357EA
		internal ConstrainedSortOp CreateConstrainedSortOp(List<SortKey> sortKeys, bool withTies)
		{
			return new ConstrainedSortOp(sortKeys, withTies);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000375F3 File Offset: 0x000357F3
		internal GroupByOp CreateGroupByOp(VarVec gbyKeys, VarVec outputs)
		{
			return new GroupByOp(gbyKeys, outputs);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000375FC File Offset: 0x000357FC
		internal GroupByIntoOp CreateGroupByIntoOp(VarVec gbyKeys, VarVec inputs, VarVec outputs)
		{
			return new GroupByIntoOp(gbyKeys, inputs, outputs);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00037606 File Offset: 0x00035806
		internal DistinctOp CreateDistinctOp(VarVec keyVars)
		{
			return new DistinctOp(keyVars);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0003760E File Offset: 0x0003580E
		internal DistinctOp CreateDistinctOp(Var keyVar)
		{
			return new DistinctOp(this.CreateVarVec(keyVar));
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0003761C File Offset: 0x0003581C
		internal UnionAllOp CreateUnionAllOp(VarMap leftMap, VarMap rightMap)
		{
			return this.CreateUnionAllOp(leftMap, rightMap, null);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00037628 File Offset: 0x00035828
		internal UnionAllOp CreateUnionAllOp(VarMap leftMap, VarMap rightMap, Var branchDiscriminator)
		{
			VarVec varVec = this.CreateVarVec();
			foreach (Var v in leftMap.Keys)
			{
				varVec.Set(v);
			}
			return new UnionAllOp(varVec, leftMap, rightMap, branchDiscriminator);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0003768C File Offset: 0x0003588C
		internal IntersectOp CreateIntersectOp(VarMap leftMap, VarMap rightMap)
		{
			VarVec varVec = this.CreateVarVec();
			foreach (Var v in leftMap.Keys)
			{
				varVec.Set(v);
			}
			return new IntersectOp(varVec, leftMap, rightMap);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000376F0 File Offset: 0x000358F0
		internal ExceptOp CreateExceptOp(VarMap leftMap, VarMap rightMap)
		{
			VarVec varVec = this.CreateVarVec();
			foreach (Var v in leftMap.Keys)
			{
				varVec.Set(v);
			}
			return new ExceptOp(varVec, leftMap, rightMap);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00037754 File Offset: 0x00035954
		internal SingleRowOp CreateSingleRowOp()
		{
			return SingleRowOp.Instance;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0003775B File Offset: 0x0003595B
		internal SingleRowTableOp CreateSingleRowTableOp()
		{
			return SingleRowTableOp.Instance;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00037762 File Offset: 0x00035962
		internal PhysicalProjectOp CreatePhysicalProjectOp(VarList outputVars, SimpleCollectionColumnMap columnMap)
		{
			return new PhysicalProjectOp(outputVars, columnMap);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0003776C File Offset: 0x0003596C
		internal PhysicalProjectOp CreatePhysicalProjectOp(Var outputVar)
		{
			VarList varList = Command.CreateVarList();
			varList.Add(outputVar);
			VarRefColumnMap varRefColumnMap = new VarRefColumnMap(outputVar);
			SimpleCollectionColumnMap columnMap = new SimpleCollectionColumnMap(TypeUtils.CreateCollectionType(varRefColumnMap.Type), null, varRefColumnMap, new SimpleColumnMap[0], new SimpleColumnMap[0]);
			return this.CreatePhysicalProjectOp(varList, columnMap);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000377B4 File Offset: 0x000359B4
		internal static CollectionInfo CreateCollectionInfo(Var collectionVar, ColumnMap columnMap, VarList flattenedElementVars, VarVec keys, List<SortKey> sortKeys, object discriminatorValue)
		{
			return new CollectionInfo(collectionVar, columnMap, flattenedElementVars, keys, sortKeys, discriminatorValue);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000377C3 File Offset: 0x000359C3
		internal SingleStreamNestOp CreateSingleStreamNestOp(VarVec keys, List<SortKey> prefixSortKeys, List<SortKey> postfixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList, Var discriminatorVar)
		{
			return new SingleStreamNestOp(keys, prefixSortKeys, postfixSortKeys, outputVars, collectionInfoList, discriminatorVar);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000377D3 File Offset: 0x000359D3
		internal MultiStreamNestOp CreateMultiStreamNestOp(List<SortKey> prefixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList)
		{
			return new MultiStreamNestOp(prefixSortKeys, outputVars, collectionInfoList);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000377DD File Offset: 0x000359DD
		internal NodeInfo GetNodeInfo(Node n)
		{
			return n.GetNodeInfo(this);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000377E6 File Offset: 0x000359E6
		internal ExtendedNodeInfo GetExtendedNodeInfo(Node n)
		{
			return n.GetExtendedNodeInfo(this);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000377EF File Offset: 0x000359EF
		internal void RecomputeNodeInfo(Node n)
		{
			this.m_nodeInfoVisitor.RecomputeNodeInfo(n);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000377FD File Offset: 0x000359FD
		internal KeyVec PullupKeys(Node n)
		{
			return this.m_keyPullupVisitor.GetKeys(n);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0003780B File Offset: 0x00035A0B
		internal static bool EqualTypes(TypeUsage x, TypeUsage y)
		{
			return TypeUsageEqualityComparer.Instance.Equals(x, y);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00037819 File Offset: 0x00035A19
		internal static bool EqualTypes(EdmType x, EdmType y)
		{
			return TypeUsageEqualityComparer.Equals(x, y);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00037824 File Offset: 0x00035A24
		internal void BuildUnionAllLadder(IList<Node> inputNodes, IList<Var> inputVars, out Node resultNode, out IList<Var> resultVars)
		{
			if (inputNodes.Count == 0)
			{
				resultNode = null;
				resultVars = null;
				return;
			}
			int num = inputVars.Count / inputNodes.Count;
			if (inputNodes.Count == 1)
			{
				resultNode = inputNodes[0];
				resultVars = inputVars;
				return;
			}
			List<Var> list = new List<Var>();
			Node node = inputNodes[0];
			for (int i = 0; i < num; i++)
			{
				list.Add(inputVars[i]);
			}
			for (int j = 1; j < inputNodes.Count; j++)
			{
				VarMap varMap = this.CreateVarMap();
				VarMap varMap2 = this.CreateVarMap();
				List<Var> list2 = new List<Var>();
				for (int k = 0; k < num; k++)
				{
					SetOpVar setOpVar = this.CreateSetOpVar(list[k].Type);
					list2.Add(setOpVar);
					varMap.Add(setOpVar, list[k]);
					varMap2.Add(setOpVar, inputVars[j * num + k]);
				}
				Op op = this.CreateUnionAllOp(varMap, varMap2);
				node = this.CreateNode(op, node, inputNodes[j]);
				list = list2;
			}
			resultNode = node;
			resultVars = list;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0003793C File Offset: 0x00035B3C
		internal void BuildUnionAllLadder(IList<Node> inputNodes, IList<Var> inputVars, out Node resultNode, out Var resultVar)
		{
			IList<Var> list;
			this.BuildUnionAllLadder(inputNodes, inputVars, out resultNode, out list);
			if (list != null && list.Count > 0)
			{
				resultVar = list[0];
				return;
			}
			resultVar = null;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00037970 File Offset: 0x00035B70
		internal Node BuildProject(Node inputNode, IEnumerable<Var> inputVars, IEnumerable<Node> computedExpressions)
		{
			VarDefListOp op = this.CreateVarDefListOp();
			Node node = this.CreateNode(op);
			VarVec varVec = this.CreateVarVec(inputVars);
			foreach (Node node2 in computedExpressions)
			{
				Var v = this.CreateComputedVar(node2.Op.Type);
				varVec.Set(v);
				VarDefOp op2 = this.CreateVarDefOp(v);
				Node item = this.CreateNode(op2, node2);
				node.Children.Add(item);
			}
			return this.CreateNode(this.CreateProjectOp(varVec), inputNode, node);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00037A20 File Offset: 0x00035C20
		internal Node BuildProject(Node input, Node computedExpression, out Var projectVar)
		{
			Node node = this.BuildProject(input, new Var[0], new Node[]
			{
				computedExpression
			});
			projectVar = ((ProjectOp)node.Op).Outputs.First;
			return node;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00037A60 File Offset: 0x00035C60
		internal void BuildOfTypeTree(Node inputNode, Var inputVar, TypeUsage desiredType, bool includeSubtypes, out Node resultNode, out Var resultVar)
		{
			Op op = includeSubtypes ? this.CreateIsOfOp(desiredType) : this.CreateIsOfOnlyOp(desiredType);
			Node arg = this.CreateNode(op, this.CreateNode(this.CreateVarRefOp(inputVar)));
			Node inputNode2 = this.CreateNode(this.CreateFilterOp(), inputNode, arg);
			resultNode = this.BuildFakeTreatProject(inputNode2, inputVar, desiredType, out resultVar);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00037AB4 File Offset: 0x00035CB4
		internal Node BuildFakeTreatProject(Node inputNode, Var inputVar, TypeUsage desiredType, out Var resultVar)
		{
			Node computedExpression = this.CreateNode(this.CreateFakeTreatOp(desiredType), this.CreateNode(this.CreateVarRefOp(inputVar)));
			return this.BuildProject(inputNode, computedExpression, out resultVar);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00037AE8 File Offset: 0x00035CE8
		internal Node BuildComparison(OpType opType, Node arg0, Node arg1)
		{
			if (!Command.EqualTypes(arg0.Op.Type, arg1.Op.Type))
			{
				TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(arg0.Op.Type, arg1.Op.Type);
				if (!Command.EqualTypes(commonTypeUsage, arg0.Op.Type))
				{
					arg0 = this.CreateNode(this.CreateSoftCastOp(commonTypeUsage), arg0);
				}
				if (!Command.EqualTypes(commonTypeUsage, arg1.Op.Type))
				{
					arg1 = this.CreateNode(this.CreateSoftCastOp(commonTypeUsage), arg1);
				}
			}
			return this.CreateNode(this.CreateComparisonOp(opType), arg0, arg1);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00037B88 File Offset: 0x00035D88
		internal Node BuildCollect(Node relOpNode, Var relOpVar)
		{
			Node arg = this.CreateNode(this.CreatePhysicalProjectOp(relOpVar), relOpNode);
			TypeUsage type = TypeHelpers.CreateCollectionTypeUsage(relOpVar.Type);
			return this.CreateNode(this.CreateCollectOp(type), arg);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00037BC0 File Offset: 0x00035DC0
		private void AddRelPropertyReference(RelProperty relProperty)
		{
			if (relProperty.ToEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many && !this.m_referencedRelProperties.Contains(relProperty))
			{
				this.m_referencedRelProperties.Add(relProperty);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00037BEB File Offset: 0x00035DEB
		internal HashSet<RelProperty> ReferencedRelProperties
		{
			get
			{
				return this.m_referencedRelProperties;
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00037BF4 File Offset: 0x00035DF4
		internal bool IsRelPropertyReferenced(RelProperty relProperty)
		{
			return this.m_referencedRelProperties.Contains(relProperty);
		}

		// Token: 0x040008CA RID: 2250
		private Dictionary<string, ParameterVar> m_parameterMap;

		// Token: 0x040008CB RID: 2251
		private List<Var> m_vars;

		// Token: 0x040008CC RID: 2252
		private List<Table> m_tables;

		// Token: 0x040008CD RID: 2253
		private Node m_root;

		// Token: 0x040008CE RID: 2254
		private MetadataWorkspace m_metadataWorkspace;

		// Token: 0x040008CF RID: 2255
		private TypeUsage m_boolType;

		// Token: 0x040008D0 RID: 2256
		private TypeUsage m_intType;

		// Token: 0x040008D1 RID: 2257
		private TypeUsage m_stringType;

		// Token: 0x040008D2 RID: 2258
		private ConstantPredicateOp m_trueOp;

		// Token: 0x040008D3 RID: 2259
		private ConstantPredicateOp m_falseOp;

		// Token: 0x040008D4 RID: 2260
		private NodeInfoVisitor m_nodeInfoVisitor;

		// Token: 0x040008D5 RID: 2261
		private KeyPullup m_keyPullupVisitor;

		// Token: 0x040008D6 RID: 2262
		private int m_nextNodeId;

		// Token: 0x040008D7 RID: 2263
		private int m_nextBranchDiscriminatorValue = 1000;

		// Token: 0x040008D8 RID: 2264
		private bool m_disableVarVecEnumCaching;

		// Token: 0x040008D9 RID: 2265
		private Stack<VarVec.VarVecEnumerator> m_freeVarVecEnumerators;

		// Token: 0x040008DA RID: 2266
		private Stack<VarVec> m_freeVarVecs;

		// Token: 0x040008DB RID: 2267
		private HashSet<RelProperty> m_referencedRelProperties;
	}
}
