using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000634 RID: 1588
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class Command
	{
		// Token: 0x06003DC9 RID: 15817 RVA: 0x0011C36C File Offset: 0x0011A56C
		internal Command(MetadataWorkspace metadataWorkspace)
		{
			this.m_parameterMap = new Dictionary<string, ParameterVar>();
			this.m_vars = new List<Var>();
			this.m_tables = new List<Table>();
			this.m_metadataWorkspace = metadataWorkspace;
			if (!Command.TryGetPrimitiveType(PrimitiveTypeKind.Boolean, out this.m_boolType))
			{
				throw new ProviderIncompatibleException(Strings.Cqt_General_NoProviderBooleanType);
			}
			if (!Command.TryGetPrimitiveType(PrimitiveTypeKind.Int32, out this.m_intType))
			{
				throw new ProviderIncompatibleException(Strings.Cqt_General_NoProviderIntegerType);
			}
			if (!Command.TryGetPrimitiveType(PrimitiveTypeKind.String, out this.m_stringType))
			{
				throw new ProviderIncompatibleException(Strings.Cqt_General_NoProviderStringType);
			}
			this.m_trueOp = new ConstantPredicateOp(this.m_boolType, true);
			this.m_falseOp = new ConstantPredicateOp(this.m_boolType, false);
			this.m_nodeInfoVisitor = new NodeInfoVisitor(this);
			this.m_keyPullupVisitor = new KeyPullup(this);
			this.m_freeVarVecEnumerators = new Stack<VarVec.VarVecEnumerator>();
			this.m_freeVarVecs = new Stack<VarVec>();
			this.m_referencedRelProperties = new HashSet<RelProperty>();
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x0011C45C File Offset: 0x0011A65C
		internal Command()
		{
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x0011C46F File Offset: 0x0011A66F
		internal virtual MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.m_metadataWorkspace;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x0011C477 File Offset: 0x0011A677
		// (set) Token: 0x06003DCD RID: 15821 RVA: 0x0011C47F File Offset: 0x0011A67F
		internal virtual Node Root { get; set; }

		// Token: 0x06003DCE RID: 15822 RVA: 0x0011C488 File Offset: 0x0011A688
		internal virtual void DisableVarVecEnumCaching()
		{
			this.m_disableVarVecEnumCaching = true;
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06003DCF RID: 15823 RVA: 0x0011C494 File Offset: 0x0011A694
		internal virtual int NextBranchDiscriminatorValue
		{
			get
			{
				return this.m_nextBranchDiscriminatorValue++;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003DD0 RID: 15824 RVA: 0x0011C4B2 File Offset: 0x0011A6B2
		internal virtual int NextNodeId
		{
			get
			{
				return this.m_nextNodeId;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003DD1 RID: 15825 RVA: 0x0011C4BA File Offset: 0x0011A6BA
		internal virtual TypeUsage BooleanType
		{
			get
			{
				return this.m_boolType;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x0011C4C2 File Offset: 0x0011A6C2
		internal virtual TypeUsage IntegerType
		{
			get
			{
				return this.m_intType;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x0011C4CA File Offset: 0x0011A6CA
		internal virtual TypeUsage StringType
		{
			get
			{
				return this.m_stringType;
			}
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x0011C4D2 File Offset: 0x0011A6D2
		private static bool TryGetPrimitiveType(PrimitiveTypeKind modelType, out TypeUsage type)
		{
			type = null;
			if (modelType == PrimitiveTypeKind.String)
			{
				type = TypeUsage.CreateStringTypeUsage(MetadataWorkspace.GetModelPrimitiveType(modelType), false, false);
			}
			else
			{
				type = MetadataWorkspace.GetCanonicalModelTypeUsage(modelType);
			}
			return null != type;
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x0011C500 File Offset: 0x0011A700
		internal virtual VarVec CreateVarVec()
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

		// Token: 0x06003DD6 RID: 15830 RVA: 0x0011C538 File Offset: 0x0011A738
		internal virtual VarVec CreateVarVec(Var v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.Set(v);
			return varVec;
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x0011C554 File Offset: 0x0011A754
		internal virtual VarVec CreateVarVec(IEnumerable<Var> v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.InitFrom(v);
			return varVec;
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x0011C570 File Offset: 0x0011A770
		internal virtual VarVec CreateVarVec(VarVec v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.InitFrom(v);
			return varVec;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x0011C58C File Offset: 0x0011A78C
		internal virtual void ReleaseVarVec(VarVec vec)
		{
			this.m_freeVarVecs.Push(vec);
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x0011C59C File Offset: 0x0011A79C
		internal virtual VarVec.VarVecEnumerator GetVarVecEnumerator(VarVec vec)
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

		// Token: 0x06003DDB RID: 15835 RVA: 0x0011C5DB File Offset: 0x0011A7DB
		internal virtual void ReleaseVarVecEnumerator(VarVec.VarVecEnumerator enumerator)
		{
			if (!this.m_disableVarVecEnumCaching)
			{
				this.m_freeVarVecEnumerators.Push(enumerator);
			}
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x0011C5F1 File Offset: 0x0011A7F1
		internal static VarList CreateVarList()
		{
			return new VarList();
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x0011C5F8 File Offset: 0x0011A7F8
		internal static VarList CreateVarList(IEnumerable<Var> vars)
		{
			return new VarList(vars);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x0011C600 File Offset: 0x0011A800
		private int NewTableId()
		{
			return this.m_tables.Count;
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x0011C60D File Offset: 0x0011A80D
		internal static TableMD CreateTableDefinition(TypeUsage elementType)
		{
			return new TableMD(elementType, null);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x0011C616 File Offset: 0x0011A816
		internal static TableMD CreateTableDefinition(EntitySetBase extent)
		{
			return new TableMD(TypeUsage.Create(extent.ElementType), extent);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x0011C629 File Offset: 0x0011A829
		internal virtual TableMD CreateFlatTableDefinition(RowType type)
		{
			return this.CreateFlatTableDefinition(type.Properties, new List<EdmMember>(), null);
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x0011C63D File Offset: 0x0011A83D
		internal virtual TableMD CreateFlatTableDefinition(IEnumerable<EdmProperty> properties, IEnumerable<EdmMember> keyMembers, EntitySetBase entitySet)
		{
			return new TableMD(properties, keyMembers, entitySet);
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x0011C648 File Offset: 0x0011A848
		internal virtual Table CreateTableInstance(TableMD tableMetadata)
		{
			Table table = new Table(this, tableMetadata, this.NewTableId());
			this.m_tables.Add(table);
			return table;
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x0011C67E File Offset: 0x0011A87E
		internal virtual IEnumerable<Var> Vars
		{
			get
			{
				return from v in this.m_vars
				where v.VarType != VarType.NotValid
				select v;
			}
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x0011C6A8 File Offset: 0x0011A8A8
		internal virtual Var GetVar(int id)
		{
			return this.m_vars[id];
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x0011C6B6 File Offset: 0x0011A8B6
		internal virtual ParameterVar GetParameter(string paramName)
		{
			return this.m_parameterMap[paramName];
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x0011C6C4 File Offset: 0x0011A8C4
		private int NewVarId()
		{
			return this.m_vars.Count;
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x0011C6D4 File Offset: 0x0011A8D4
		internal virtual ParameterVar CreateParameterVar(string parameterName, TypeUsage parameterType)
		{
			if (this.m_parameterMap.ContainsKey(parameterName))
			{
				throw new ArgumentException(Strings.DuplicateParameterName(parameterName));
			}
			ParameterVar parameterVar = new ParameterVar(this.NewVarId(), parameterType, parameterName);
			this.m_vars.Add(parameterVar);
			this.m_parameterMap[parameterName] = parameterVar;
			return parameterVar;
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x0011C724 File Offset: 0x0011A924
		private ParameterVar ReplaceParameterVar(ParameterVar oldVar, Func<TypeUsage, TypeUsage> generateReplacementType)
		{
			ParameterVar parameterVar = new ParameterVar(this.NewVarId(), generateReplacementType(oldVar.Type), oldVar.ParameterName);
			this.m_parameterMap[oldVar.ParameterName] = parameterVar;
			this.m_vars.Add(parameterVar);
			return parameterVar;
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x0011C776 File Offset: 0x0011A976
		internal virtual ParameterVar ReplaceEnumParameterVar(ParameterVar oldVar)
		{
			return this.ReplaceParameterVar(oldVar, (TypeUsage t) => TypeHelpers.CreateEnumUnderlyingTypeUsage(t));
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x0011C7A4 File Offset: 0x0011A9A4
		internal virtual ParameterVar ReplaceStrongSpatialParameterVar(ParameterVar oldVar)
		{
			return this.ReplaceParameterVar(oldVar, (TypeUsage t) => TypeHelpers.CreateSpatialUnionTypeUsage(t));
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x0011C7CC File Offset: 0x0011A9CC
		internal virtual ColumnVar CreateColumnVar(Table table, ColumnMD columnMD)
		{
			ColumnVar columnVar = new ColumnVar(this.NewVarId(), table, columnMD);
			table.Columns.Add(columnVar);
			this.m_vars.Add(columnVar);
			return columnVar;
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x0011C800 File Offset: 0x0011AA00
		internal virtual ComputedVar CreateComputedVar(TypeUsage type)
		{
			ComputedVar computedVar = new ComputedVar(this.NewVarId(), type);
			this.m_vars.Add(computedVar);
			return computedVar;
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x0011C828 File Offset: 0x0011AA28
		internal virtual SetOpVar CreateSetOpVar(TypeUsage type)
		{
			SetOpVar setOpVar = new SetOpVar(this.NewVarId(), type);
			this.m_vars.Add(setOpVar);
			return setOpVar;
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x0011C84F File Offset: 0x0011AA4F
		internal virtual Node CreateNode(Op op)
		{
			return this.CreateNode(op, new List<Node>());
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x0011C860 File Offset: 0x0011AA60
		internal virtual Node CreateNode(Op op, Node arg1)
		{
			return this.CreateNode(op, new List<Node>
			{
				arg1
			});
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x0011C884 File Offset: 0x0011AA84
		internal virtual Node CreateNode(Op op, Node arg1, Node arg2)
		{
			return this.CreateNode(op, new List<Node>
			{
				arg1,
				arg2
			});
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x0011C8B0 File Offset: 0x0011AAB0
		internal virtual Node CreateNode(Op op, Node arg1, Node arg2, Node arg3)
		{
			return this.CreateNode(op, new List<Node>
			{
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x0011C8E4 File Offset: 0x0011AAE4
		internal virtual Node CreateNode(Op op, IList<Node> args)
		{
			return new Node(this.m_nextNodeId++, op, new List<Node>(args));
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x0011C910 File Offset: 0x0011AB10
		internal virtual Node CreateNode(Op op, List<Node> args)
		{
			return new Node(this.m_nextNodeId++, op, args);
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x0011C935 File Offset: 0x0011AB35
		internal virtual ConstantBaseOp CreateConstantOp(TypeUsage type, object value)
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

		// Token: 0x06003DF6 RID: 15862 RVA: 0x0011C958 File Offset: 0x0011AB58
		internal virtual InternalConstantOp CreateInternalConstantOp(TypeUsage type, object value)
		{
			return new InternalConstantOp(type, value);
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x0011C961 File Offset: 0x0011AB61
		internal virtual NullSentinelOp CreateNullSentinelOp()
		{
			return new NullSentinelOp(this.IntegerType, 1);
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x0011C974 File Offset: 0x0011AB74
		internal virtual NullOp CreateNullOp(TypeUsage type)
		{
			return new NullOp(type);
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x0011C97C File Offset: 0x0011AB7C
		internal virtual ConstantPredicateOp CreateConstantPredicateOp(bool value)
		{
			if (!value)
			{
				return this.m_falseOp;
			}
			return this.m_trueOp;
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x0011C98E File Offset: 0x0011AB8E
		internal virtual ConstantPredicateOp CreateTrueOp()
		{
			return this.m_trueOp;
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x0011C996 File Offset: 0x0011AB96
		internal virtual ConstantPredicateOp CreateFalseOp()
		{
			return this.m_falseOp;
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x0011C99E File Offset: 0x0011AB9E
		internal virtual FunctionOp CreateFunctionOp(EdmFunction function)
		{
			return new FunctionOp(function);
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x0011C9A6 File Offset: 0x0011ABA6
		internal virtual TreatOp CreateTreatOp(TypeUsage type)
		{
			return new TreatOp(type, false);
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x0011C9AF File Offset: 0x0011ABAF
		internal virtual TreatOp CreateFakeTreatOp(TypeUsage type)
		{
			return new TreatOp(type, true);
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x0011C9B8 File Offset: 0x0011ABB8
		internal virtual IsOfOp CreateIsOfOp(TypeUsage isOfType)
		{
			return new IsOfOp(isOfType, false, this.m_boolType);
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x0011C9C7 File Offset: 0x0011ABC7
		internal virtual IsOfOp CreateIsOfOnlyOp(TypeUsage isOfType)
		{
			return new IsOfOp(isOfType, true, this.m_boolType);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x0011C9D6 File Offset: 0x0011ABD6
		internal virtual CastOp CreateCastOp(TypeUsage type)
		{
			return new CastOp(type);
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x0011C9DE File Offset: 0x0011ABDE
		internal virtual SoftCastOp CreateSoftCastOp(TypeUsage type)
		{
			return new SoftCastOp(type);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x0011C9E8 File Offset: 0x0011ABE8
		internal virtual ComparisonOp CreateComparisonOp(OpType opType, bool useDatabaseNullSemantics = false)
		{
			return new ComparisonOp(opType, this.BooleanType)
			{
				UseDatabaseNullSemantics = useDatabaseNullSemantics
			};
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x0011CA0A File Offset: 0x0011AC0A
		internal virtual LikeOp CreateLikeOp()
		{
			return new LikeOp(this.BooleanType);
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x0011CA17 File Offset: 0x0011AC17
		internal virtual ConditionalOp CreateConditionalOp(OpType opType)
		{
			return new ConditionalOp(opType, this.BooleanType);
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x0011CA25 File Offset: 0x0011AC25
		internal virtual CaseOp CreateCaseOp(TypeUsage type)
		{
			return new CaseOp(type);
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x0011CA2D File Offset: 0x0011AC2D
		internal virtual AggregateOp CreateAggregateOp(EdmFunction aggFunc, bool distinctAgg)
		{
			return new AggregateOp(aggFunc, distinctAgg);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x0011CA36 File Offset: 0x0011AC36
		internal virtual NewInstanceOp CreateNewInstanceOp(TypeUsage type)
		{
			return new NewInstanceOp(type);
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x0011CA3E File Offset: 0x0011AC3E
		internal virtual NewEntityOp CreateScopedNewEntityOp(TypeUsage type, List<RelProperty> relProperties, EntitySet entitySet)
		{
			return new NewEntityOp(type, relProperties, true, entitySet);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x0011CA49 File Offset: 0x0011AC49
		internal virtual NewEntityOp CreateNewEntityOp(TypeUsage type, List<RelProperty> relProperties)
		{
			return new NewEntityOp(type, relProperties, false, null);
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x0011CA54 File Offset: 0x0011AC54
		internal virtual DiscriminatedNewEntityOp CreateDiscriminatedNewEntityOp(TypeUsage type, ExplicitDiscriminatorMap discriminatorMap, EntitySet entitySet, List<RelProperty> relProperties)
		{
			return new DiscriminatedNewEntityOp(type, discriminatorMap, entitySet, relProperties);
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x0011CA60 File Offset: 0x0011AC60
		internal virtual NewMultisetOp CreateNewMultisetOp(TypeUsage type)
		{
			return new NewMultisetOp(type);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x0011CA68 File Offset: 0x0011AC68
		internal virtual NewRecordOp CreateNewRecordOp(TypeUsage type)
		{
			return new NewRecordOp(type);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x0011CA70 File Offset: 0x0011AC70
		internal virtual NewRecordOp CreateNewRecordOp(RowType type)
		{
			return new NewRecordOp(TypeUsage.Create(type));
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x0011CA7D File Offset: 0x0011AC7D
		internal virtual NewRecordOp CreateNewRecordOp(TypeUsage type, List<EdmProperty> fields)
		{
			return new NewRecordOp(type, fields);
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x0011CA86 File Offset: 0x0011AC86
		internal virtual VarRefOp CreateVarRefOp(Var v)
		{
			return new VarRefOp(v);
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x0011CA8E File Offset: 0x0011AC8E
		internal virtual ArithmeticOp CreateArithmeticOp(OpType opType, TypeUsage type)
		{
			return new ArithmeticOp(opType, type);
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x0011CA98 File Offset: 0x0011AC98
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

		// Token: 0x06003E13 RID: 15891 RVA: 0x0011CAF9 File Offset: 0x0011ACF9
		internal RelPropertyOp CreateRelPropertyOp(RelProperty prop)
		{
			this.AddRelPropertyReference(prop);
			return new RelPropertyOp(prop.ToEnd.TypeUsage, prop);
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x0011CB13 File Offset: 0x0011AD13
		internal virtual RefOp CreateRefOp(EntitySet entitySet, TypeUsage type)
		{
			return new RefOp(entitySet, type);
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x0011CB1C File Offset: 0x0011AD1C
		internal ExistsOp CreateExistsOp()
		{
			return new ExistsOp(this.BooleanType);
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x0011CB29 File Offset: 0x0011AD29
		internal virtual ElementOp CreateElementOp(TypeUsage type)
		{
			return new ElementOp(type);
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x0011CB31 File Offset: 0x0011AD31
		internal virtual GetEntityRefOp CreateGetEntityRefOp(TypeUsage type)
		{
			return new GetEntityRefOp(type);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x0011CB39 File Offset: 0x0011AD39
		internal virtual GetRefKeyOp CreateGetRefKeyOp(TypeUsage type)
		{
			return new GetRefKeyOp(type);
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x0011CB41 File Offset: 0x0011AD41
		internal virtual CollectOp CreateCollectOp(TypeUsage type)
		{
			return new CollectOp(type);
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x0011CB49 File Offset: 0x0011AD49
		internal virtual DerefOp CreateDerefOp(TypeUsage type)
		{
			return new DerefOp(type);
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x0011CB51 File Offset: 0x0011AD51
		internal NavigateOp CreateNavigateOp(TypeUsage type, RelProperty relProperty)
		{
			this.AddRelPropertyReference(relProperty);
			return new NavigateOp(type, relProperty);
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x0011CB61 File Offset: 0x0011AD61
		internal virtual VarDefListOp CreateVarDefListOp()
		{
			return VarDefListOp.Instance;
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x0011CB68 File Offset: 0x0011AD68
		internal virtual VarDefOp CreateVarDefOp(Var v)
		{
			return new VarDefOp(v);
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x0011CB70 File Offset: 0x0011AD70
		internal Node CreateVarDefNode(Node definingExpr, out Var computedVar)
		{
			ScalarOp scalarOp = definingExpr.Op as ScalarOp;
			computedVar = this.CreateComputedVar(scalarOp.Type);
			VarDefOp op = this.CreateVarDefOp(computedVar);
			return this.CreateNode(op, definingExpr);
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x0011CBAC File Offset: 0x0011ADAC
		internal Node CreateVarDefListNode(Node definingExpr, out Var computedVar)
		{
			Node arg = this.CreateVarDefNode(definingExpr, out computedVar);
			VarDefListOp op = this.CreateVarDefListOp();
			return this.CreateNode(op, arg);
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x0011CBD4 File Offset: 0x0011ADD4
		internal ScanTableOp CreateScanTableOp(TableMD tableMetadata)
		{
			Table table = this.CreateTableInstance(tableMetadata);
			return this.CreateScanTableOp(table);
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x0011CBF0 File Offset: 0x0011ADF0
		internal virtual ScanTableOp CreateScanTableOp(Table table)
		{
			return new ScanTableOp(table);
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x0011CBF8 File Offset: 0x0011ADF8
		internal virtual ScanViewOp CreateScanViewOp(Table table)
		{
			return new ScanViewOp(table);
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x0011CC00 File Offset: 0x0011AE00
		internal virtual ScanViewOp CreateScanViewOp(TableMD tableMetadata)
		{
			Table table = this.CreateTableInstance(tableMetadata);
			return this.CreateScanViewOp(table);
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x0011CC1C File Offset: 0x0011AE1C
		internal virtual UnnestOp CreateUnnestOp(Var v)
		{
			Table t = this.CreateTableInstance(Command.CreateTableDefinition(TypeHelpers.GetEdmType<CollectionType>(v.Type).TypeUsage));
			return this.CreateUnnestOp(v, t);
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x0011CC4D File Offset: 0x0011AE4D
		internal virtual UnnestOp CreateUnnestOp(Var v, Table t)
		{
			return new UnnestOp(v, t);
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x0011CC56 File Offset: 0x0011AE56
		internal virtual FilterOp CreateFilterOp()
		{
			return FilterOp.Instance;
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x0011CC5D File Offset: 0x0011AE5D
		internal virtual ProjectOp CreateProjectOp(VarVec vars)
		{
			return new ProjectOp(vars);
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x0011CC68 File Offset: 0x0011AE68
		internal virtual ProjectOp CreateProjectOp(Var v)
		{
			VarVec varVec = this.CreateVarVec();
			varVec.Set(v);
			return new ProjectOp(varVec);
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x0011CC89 File Offset: 0x0011AE89
		internal virtual InnerJoinOp CreateInnerJoinOp()
		{
			return InnerJoinOp.Instance;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x0011CC90 File Offset: 0x0011AE90
		internal virtual LeftOuterJoinOp CreateLeftOuterJoinOp()
		{
			return LeftOuterJoinOp.Instance;
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x0011CC97 File Offset: 0x0011AE97
		internal virtual FullOuterJoinOp CreateFullOuterJoinOp()
		{
			return FullOuterJoinOp.Instance;
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x0011CC9E File Offset: 0x0011AE9E
		internal virtual CrossJoinOp CreateCrossJoinOp()
		{
			return CrossJoinOp.Instance;
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x0011CCA5 File Offset: 0x0011AEA5
		internal virtual CrossApplyOp CreateCrossApplyOp()
		{
			return CrossApplyOp.Instance;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x0011CCAC File Offset: 0x0011AEAC
		internal virtual OuterApplyOp CreateOuterApplyOp()
		{
			return OuterApplyOp.Instance;
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x0011CCB3 File Offset: 0x0011AEB3
		internal static SortKey CreateSortKey(Var v, bool asc, string collation)
		{
			return new SortKey(v, asc, collation);
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x0011CCBD File Offset: 0x0011AEBD
		internal static SortKey CreateSortKey(Var v, bool asc)
		{
			return new SortKey(v, asc, "");
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x0011CCCB File Offset: 0x0011AECB
		internal static SortKey CreateSortKey(Var v)
		{
			return new SortKey(v, true, "");
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x0011CCD9 File Offset: 0x0011AED9
		internal virtual SortOp CreateSortOp(List<SortKey> sortKeys)
		{
			return new SortOp(sortKeys);
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x0011CCE1 File Offset: 0x0011AEE1
		internal virtual ConstrainedSortOp CreateConstrainedSortOp(List<SortKey> sortKeys)
		{
			return new ConstrainedSortOp(sortKeys, false);
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x0011CCEA File Offset: 0x0011AEEA
		internal virtual ConstrainedSortOp CreateConstrainedSortOp(List<SortKey> sortKeys, bool withTies)
		{
			return new ConstrainedSortOp(sortKeys, withTies);
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x0011CCF3 File Offset: 0x0011AEF3
		internal virtual GroupByOp CreateGroupByOp(VarVec gbyKeys, VarVec outputs)
		{
			return new GroupByOp(gbyKeys, outputs);
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x0011CCFC File Offset: 0x0011AEFC
		internal virtual GroupByIntoOp CreateGroupByIntoOp(VarVec gbyKeys, VarVec inputs, VarVec outputs)
		{
			return new GroupByIntoOp(gbyKeys, inputs, outputs);
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x0011CD06 File Offset: 0x0011AF06
		internal virtual DistinctOp CreateDistinctOp(VarVec keyVars)
		{
			return new DistinctOp(keyVars);
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x0011CD0E File Offset: 0x0011AF0E
		internal virtual DistinctOp CreateDistinctOp(Var keyVar)
		{
			return new DistinctOp(this.CreateVarVec(keyVar));
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x0011CD1C File Offset: 0x0011AF1C
		internal virtual UnionAllOp CreateUnionAllOp(VarMap leftMap, VarMap rightMap)
		{
			return this.CreateUnionAllOp(leftMap, rightMap, null);
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x0011CD28 File Offset: 0x0011AF28
		internal virtual UnionAllOp CreateUnionAllOp(VarMap leftMap, VarMap rightMap, Var branchDiscriminator)
		{
			VarVec varVec = this.CreateVarVec();
			foreach (Var v in leftMap.Keys)
			{
				varVec.Set(v);
			}
			return new UnionAllOp(varVec, leftMap, rightMap, branchDiscriminator);
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x0011CD8C File Offset: 0x0011AF8C
		internal virtual IntersectOp CreateIntersectOp(VarMap leftMap, VarMap rightMap)
		{
			VarVec varVec = this.CreateVarVec();
			foreach (Var v in leftMap.Keys)
			{
				varVec.Set(v);
			}
			return new IntersectOp(varVec, leftMap, rightMap);
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x0011CDF0 File Offset: 0x0011AFF0
		internal virtual ExceptOp CreateExceptOp(VarMap leftMap, VarMap rightMap)
		{
			VarVec varVec = this.CreateVarVec();
			foreach (Var v in leftMap.Keys)
			{
				varVec.Set(v);
			}
			return new ExceptOp(varVec, leftMap, rightMap);
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x0011CE54 File Offset: 0x0011B054
		internal virtual SingleRowOp CreateSingleRowOp()
		{
			return SingleRowOp.Instance;
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x0011CE5B File Offset: 0x0011B05B
		internal virtual SingleRowTableOp CreateSingleRowTableOp()
		{
			return SingleRowTableOp.Instance;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x0011CE62 File Offset: 0x0011B062
		internal virtual PhysicalProjectOp CreatePhysicalProjectOp(VarList outputVars, SimpleCollectionColumnMap columnMap)
		{
			return new PhysicalProjectOp(outputVars, columnMap);
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x0011CE6C File Offset: 0x0011B06C
		internal virtual PhysicalProjectOp CreatePhysicalProjectOp(Var outputVar)
		{
			VarList varList = Command.CreateVarList();
			varList.Add(outputVar);
			VarRefColumnMap varRefColumnMap = new VarRefColumnMap(outputVar);
			SimpleCollectionColumnMap columnMap = new SimpleCollectionColumnMap(TypeUtils.CreateCollectionType(varRefColumnMap.Type), null, varRefColumnMap, new SimpleColumnMap[0], new SimpleColumnMap[0]);
			return this.CreatePhysicalProjectOp(varList, columnMap);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x0011CEB4 File Offset: 0x0011B0B4
		internal static CollectionInfo CreateCollectionInfo(Var collectionVar, ColumnMap columnMap, VarList flattenedElementVars, VarVec keys, List<SortKey> sortKeys, object discriminatorValue)
		{
			return new CollectionInfo(collectionVar, columnMap, flattenedElementVars, keys, sortKeys, discriminatorValue);
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x0011CEC3 File Offset: 0x0011B0C3
		internal virtual SingleStreamNestOp CreateSingleStreamNestOp(VarVec keys, List<SortKey> prefixSortKeys, List<SortKey> postfixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList, Var discriminatorVar)
		{
			return new SingleStreamNestOp(keys, prefixSortKeys, postfixSortKeys, outputVars, collectionInfoList, discriminatorVar);
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x0011CED3 File Offset: 0x0011B0D3
		internal virtual MultiStreamNestOp CreateMultiStreamNestOp(List<SortKey> prefixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList)
		{
			return new MultiStreamNestOp(prefixSortKeys, outputVars, collectionInfoList);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x0011CEDD File Offset: 0x0011B0DD
		internal virtual NodeInfo GetNodeInfo(Node n)
		{
			return n.GetNodeInfo(this);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x0011CEE6 File Offset: 0x0011B0E6
		internal virtual ExtendedNodeInfo GetExtendedNodeInfo(Node n)
		{
			return n.GetExtendedNodeInfo(this);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x0011CEEF File Offset: 0x0011B0EF
		internal virtual void RecomputeNodeInfo(Node n)
		{
			this.m_nodeInfoVisitor.RecomputeNodeInfo(n);
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x0011CEFD File Offset: 0x0011B0FD
		internal virtual KeyVec PullupKeys(Node n)
		{
			return this.m_keyPullupVisitor.GetKeys(n);
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x0011CF0B File Offset: 0x0011B10B
		internal static bool EqualTypes(TypeUsage x, TypeUsage y)
		{
			return TypeUsageEqualityComparer.Instance.Equals(x, y);
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x0011CF19 File Offset: 0x0011B119
		internal static bool EqualTypes(EdmType x, EdmType y)
		{
			return TypeUsageEqualityComparer.Equals(x, y);
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x0011CF24 File Offset: 0x0011B124
		internal virtual void BuildUnionAllLadder(IList<Node> inputNodes, IList<Var> inputVars, out Node resultNode, out IList<Var> resultVars)
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
				VarMap varMap = new VarMap();
				VarMap varMap2 = new VarMap();
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

		// Token: 0x06003E4B RID: 15947 RVA: 0x0011D03C File Offset: 0x0011B23C
		internal virtual void BuildUnionAllLadder(IList<Node> inputNodes, IList<Var> inputVars, out Node resultNode, out Var resultVar)
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

		// Token: 0x06003E4C RID: 15948 RVA: 0x0011D070 File Offset: 0x0011B270
		internal virtual Node BuildProject(Node inputNode, IEnumerable<Var> inputVars, IEnumerable<Node> computedExpressions)
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

		// Token: 0x06003E4D RID: 15949 RVA: 0x0011D120 File Offset: 0x0011B320
		internal virtual Node BuildProject(Node input, Node computedExpression, out Var projectVar)
		{
			Node node = this.BuildProject(input, new Var[0], new Node[]
			{
				computedExpression
			});
			projectVar = ((ProjectOp)node.Op).Outputs.First;
			return node;
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x0011D160 File Offset: 0x0011B360
		internal virtual void BuildOfTypeTree(Node inputNode, Var inputVar, TypeUsage desiredType, bool includeSubtypes, out Node resultNode, out Var resultVar)
		{
			Op op = includeSubtypes ? this.CreateIsOfOp(desiredType) : this.CreateIsOfOnlyOp(desiredType);
			Node arg = this.CreateNode(op, this.CreateNode(this.CreateVarRefOp(inputVar)));
			Node inputNode2 = this.CreateNode(this.CreateFilterOp(), inputNode, arg);
			resultNode = this.BuildFakeTreatProject(inputNode2, inputVar, desiredType, out resultVar);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x0011D1B4 File Offset: 0x0011B3B4
		internal virtual Node BuildFakeTreatProject(Node inputNode, Var inputVar, TypeUsage desiredType, out Var resultVar)
		{
			Node computedExpression = this.CreateNode(this.CreateFakeTreatOp(desiredType), this.CreateNode(this.CreateVarRefOp(inputVar)));
			return this.BuildProject(inputNode, computedExpression, out resultVar);
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x0011D1E8 File Offset: 0x0011B3E8
		internal Node BuildComparison(OpType opType, Node arg0, Node arg1, bool useDatabaseNullSemantics = false)
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
			return this.CreateNode(this.CreateComparisonOp(opType, useDatabaseNullSemantics), arg0, arg1);
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x0011D288 File Offset: 0x0011B488
		internal virtual Node BuildCollect(Node relOpNode, Var relOpVar)
		{
			Node arg = this.CreateNode(this.CreatePhysicalProjectOp(relOpVar), relOpNode);
			TypeUsage type = TypeHelpers.CreateCollectionTypeUsage(relOpVar.Type);
			return this.CreateNode(this.CreateCollectOp(type), arg);
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x0011D2C0 File Offset: 0x0011B4C0
		private void AddRelPropertyReference(RelProperty relProperty)
		{
			if (relProperty.ToEnd.RelationshipMultiplicity != RelationshipMultiplicity.Many && !this.m_referencedRelProperties.Contains(relProperty))
			{
				this.m_referencedRelProperties.Add(relProperty);
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003E53 RID: 15955 RVA: 0x0011D2EB File Offset: 0x0011B4EB
		internal virtual HashSet<RelProperty> ReferencedRelProperties
		{
			get
			{
				return this.m_referencedRelProperties;
			}
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x0011D2F4 File Offset: 0x0011B4F4
		internal virtual bool IsRelPropertyReferenced(RelProperty relProperty)
		{
			return this.m_referencedRelProperties.Contains(relProperty);
		}

		// Token: 0x04001751 RID: 5969
		private readonly Dictionary<string, ParameterVar> m_parameterMap;

		// Token: 0x04001752 RID: 5970
		private readonly List<Var> m_vars;

		// Token: 0x04001753 RID: 5971
		private readonly List<Table> m_tables;

		// Token: 0x04001754 RID: 5972
		private readonly MetadataWorkspace m_metadataWorkspace;

		// Token: 0x04001755 RID: 5973
		private readonly TypeUsage m_boolType;

		// Token: 0x04001756 RID: 5974
		private readonly TypeUsage m_intType;

		// Token: 0x04001757 RID: 5975
		private readonly TypeUsage m_stringType;

		// Token: 0x04001758 RID: 5976
		private readonly ConstantPredicateOp m_trueOp;

		// Token: 0x04001759 RID: 5977
		private readonly ConstantPredicateOp m_falseOp;

		// Token: 0x0400175A RID: 5978
		private readonly NodeInfoVisitor m_nodeInfoVisitor;

		// Token: 0x0400175B RID: 5979
		private readonly KeyPullup m_keyPullupVisitor;

		// Token: 0x0400175C RID: 5980
		private int m_nextNodeId;

		// Token: 0x0400175D RID: 5981
		private int m_nextBranchDiscriminatorValue = 1000;

		// Token: 0x0400175E RID: 5982
		private bool m_disableVarVecEnumCaching;

		// Token: 0x0400175F RID: 5983
		private readonly Stack<VarVec.VarVecEnumerator> m_freeVarVecEnumerators;

		// Token: 0x04001760 RID: 5984
		private readonly Stack<VarVec> m_freeVarVecs;

		// Token: 0x04001761 RID: 5985
		private readonly HashSet<RelProperty> m_referencedRelProperties;
	}
}
