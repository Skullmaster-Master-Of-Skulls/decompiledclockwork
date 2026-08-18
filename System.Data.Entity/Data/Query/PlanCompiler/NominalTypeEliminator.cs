using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Globalization;
using System.Linq;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200005A RID: 90
	internal class NominalTypeEliminator : BasicOpVisitorOfNode
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00025295 File Offset: 0x00023495
		private Command m_command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000252A4 File Offset: 0x000234A4
		private NominalTypeEliminator(PlanCompiler compilerState, StructuredTypeInfo typeInfo, Dictionary<Var, PropertyRefList> varPropertyMap, Dictionary<Node, PropertyRefList> nodePropertyMap, Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			this.m_compilerState = compilerState;
			this.m_typeInfo = typeInfo;
			this.m_varPropertyMap = varPropertyMap;
			this.m_nodePropertyMap = nodePropertyMap;
			this.m_varInfoMap = new VarInfoMap();
			this.m_tvfResultKeys = tvfResultKeys;
			this.m_typeToNewTypeMap = new Dictionary<TypeUsage, TypeUsage>(TypeUsageEqualityComparer.Instance);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000252F8 File Offset: 0x000234F8
		internal static void Process(PlanCompiler compilerState, StructuredTypeInfo structuredTypeInfo, Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			Dictionary<Var, PropertyRefList> varPropertyMap;
			Dictionary<Node, PropertyRefList> nodePropertyMap;
			PropertyPushdownHelper.Process(compilerState.Command, structuredTypeInfo, out varPropertyMap, out nodePropertyMap);
			NominalTypeEliminator nominalTypeEliminator = new NominalTypeEliminator(compilerState, structuredTypeInfo, varPropertyMap, nodePropertyMap, tvfResultKeys);
			nominalTypeEliminator.Process();
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00025328 File Offset: 0x00023528
		private void Process()
		{
			foreach (ParameterVar parameterVar in (from v in this.m_command.Vars.OfType<ParameterVar>()
			where TypeSemantics.IsEnumerationType(v.Type) || TypeSemantics.IsStrongSpatialType(v.Type)
			select v).ToArray<ParameterVar>())
			{
				ParameterVar newVar = TypeSemantics.IsEnumerationType(parameterVar.Type) ? this.m_command.ReplaceEnumParameterVar(parameterVar) : this.m_command.ReplaceStrongSpatialParameterVar(parameterVar);
				this.m_varInfoMap.CreatePrimitiveTypeVarInfo(parameterVar, newVar);
			}
			Node root = this.m_command.Root;
			PlanCompiler.Assert(root.Op.OpType == OpType.PhysicalProject, "root node is not PhysicalProjectOp?");
			root.Op.Accept<Node>(this, root);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x000253EC File Offset: 0x000235EC
		private TypeUsage DefaultTypeIdType
		{
			get
			{
				return this.m_command.StringType;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000253FC File Offset: 0x000235FC
		private TypeUsage GetNewType(TypeUsage type)
		{
			TypeUsage typeUsage;
			if (this.m_typeToNewTypeMap.TryGetValue(type, out typeUsage))
			{
				return typeUsage;
			}
			CollectionType collectionType;
			if (TypeHelpers.TryGetEdmType<CollectionType>(type, out collectionType))
			{
				TypeUsage newType = this.GetNewType(collectionType.TypeUsage);
				typeUsage = TypeUtils.CreateCollectionType(newType);
			}
			else if (TypeUtils.IsStructuredType(type))
			{
				typeUsage = this.m_typeInfo.GetTypeInfo(type).FlattenedTypeUsage;
			}
			else if (TypeSemantics.IsEnumerationType(type))
			{
				typeUsage = TypeHelpers.CreateEnumUnderlyingTypeUsage(type);
			}
			else if (TypeSemantics.IsStrongSpatialType(type))
			{
				typeUsage = TypeHelpers.CreateSpatialUnionTypeUsage(type);
			}
			else
			{
				typeUsage = type;
			}
			this.m_typeToNewTypeMap[type] = typeUsage;
			return typeUsage;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0002548C File Offset: 0x0002368C
		private Node BuildAccessor(Node input, EdmProperty property)
		{
			Op op = input.Op;
			NewRecordOp newRecordOp = op as NewRecordOp;
			if (newRecordOp != null)
			{
				int index;
				if (newRecordOp.GetFieldPosition(property, out index))
				{
					return this.Copy(input.Children[index]);
				}
				return null;
			}
			else
			{
				if (op.OpType == OpType.Null)
				{
					return null;
				}
				PropertyOp op2 = this.m_command.CreatePropertyOp(property);
				return this.m_command.CreateNode(op2, this.Copy(input));
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000254F8 File Offset: 0x000236F8
		private Node BuildAccessorWithNulls(Node input, EdmProperty property)
		{
			Node node = this.BuildAccessor(input, property);
			if (node == null)
			{
				node = this.CreateNullConstantNode(Helper.GetModelTypeUsage(property));
			}
			return node;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00025520 File Offset: 0x00023720
		private Node BuildTypeIdAccessor(Node input, TypeInfo typeInfo)
		{
			Node result;
			if (typeInfo.HasTypeIdProperty)
			{
				result = this.BuildAccessorWithNulls(input, typeInfo.TypeIdProperty);
			}
			else
			{
				result = this.CreateTypeIdConstant(typeInfo);
			}
			return result;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00025550 File Offset: 0x00023750
		private Node BuildSoftCast(Node node, TypeUsage targetType)
		{
			PlanCompiler.Assert(node.Op.IsScalarOp, "Attempting SoftCast around non-ScalarOp?");
			if (Command.EqualTypes(node.Op.Type, targetType))
			{
				return node;
			}
			while (node.Op.OpType == OpType.SoftCast)
			{
				node = node.Child0;
			}
			return this.m_command.CreateNode(this.m_command.CreateSoftCastOp(targetType), node);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000255B8 File Offset: 0x000237B8
		private Node Copy(Node n)
		{
			return OpCopier.Copy(this.m_command, n);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000255C6 File Offset: 0x000237C6
		private Node CreateNullConstantNode(TypeUsage type)
		{
			return this.m_command.CreateNode(this.m_command.CreateNullOp(type));
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000255E0 File Offset: 0x000237E0
		private Node CreateNullSentinelConstant()
		{
			NullSentinelOp op = this.m_command.CreateNullSentinelOp();
			return this.m_command.CreateNode(op);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00025608 File Offset: 0x00023808
		private Node CreateTypeIdConstant(TypeInfo typeInfo)
		{
			object typeId = typeInfo.TypeId;
			TypeUsage type;
			if (typeInfo.RootType.DiscriminatorMap != null)
			{
				type = Helper.GetModelTypeUsage(typeInfo.RootType.DiscriminatorMap.DiscriminatorProperty);
			}
			else
			{
				type = this.DefaultTypeIdType;
			}
			InternalConstantOp op = this.m_command.CreateInternalConstantOp(type, typeId);
			return this.m_command.CreateNode(op);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00025664 File Offset: 0x00023864
		private Node CreateTypeIdConstantForPrefixMatch(TypeInfo typeInfo)
		{
			object typeId = typeInfo.TypeId;
			string value = ((typeId != null) ? typeId.ToString() : null) + "%";
			InternalConstantOp op = this.m_command.CreateInternalConstantOp(this.DefaultTypeIdType, value);
			return this.m_command.CreateNode(op);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000256AD File Offset: 0x000238AD
		private IEnumerable<PropertyRef> GetPropertyRefsForComparisonAndIsNull(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind)
		{
			PlanCompiler.Assert(opKind == NominalTypeEliminator.OperationKind.IsNull || opKind == NominalTypeEliminator.OperationKind.Equality, "Unexpected opKind: " + opKind.ToString() + "; Can only handle IsNull and Equality");
			TypeUsage type = typeInfo.Type;
			RowType rowType = null;
			if (TypeHelpers.TryGetEdmType<RowType>(type, out rowType))
			{
				if (opKind == NominalTypeEliminator.OperationKind.IsNull && typeInfo.HasNullSentinelProperty)
				{
					yield return NullSentinelPropertyRef.Instance;
				}
				else
				{
					foreach (EdmProperty i in rowType.Properties)
					{
						if (!TypeUtils.IsStructuredType(Helper.GetModelTypeUsage(i)))
						{
							yield return new SimplePropertyRef(i);
						}
						else
						{
							TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(Helper.GetModelTypeUsage(i));
							foreach (PropertyRef propertyRef in this.GetPropertyRefs(typeInfo2, opKind))
							{
								PropertyRef propertyRef2 = propertyRef.CreateNestedPropertyRef(i);
								yield return propertyRef2;
							}
							IEnumerator<PropertyRef> enumerator2 = null;
						}
						i = null;
					}
					ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = default(ReadOnlyMetadataCollection<EdmProperty>.Enumerator);
				}
				yield break;
			}
			EntityType entityType = null;
			if (TypeHelpers.TryGetEdmType<EntityType>(type, out entityType))
			{
				if (opKind == NominalTypeEliminator.OperationKind.Equality || (opKind == NominalTypeEliminator.OperationKind.IsNull && !typeInfo.HasTypeIdProperty))
				{
					foreach (PropertyRef propertyRef3 in typeInfo.GetIdentityPropertyRefs())
					{
						yield return propertyRef3;
					}
					IEnumerator<PropertyRef> enumerator2 = null;
				}
				else
				{
					yield return TypeIdPropertyRef.Instance;
				}
				yield break;
			}
			ComplexType complexType = null;
			if (TypeHelpers.TryGetEdmType<ComplexType>(type, out complexType))
			{
				PlanCompiler.Assert(opKind == NominalTypeEliminator.OperationKind.IsNull, "complex types not equality-comparable");
				PlanCompiler.Assert(typeInfo.HasNullSentinelProperty, "complex type with no null sentinel property: can't handle isNull");
				yield return NullSentinelPropertyRef.Instance;
				yield break;
			}
			RefType refType = null;
			if (TypeHelpers.TryGetEdmType<RefType>(type, out refType))
			{
				foreach (PropertyRef propertyRef4 in typeInfo.GetAllPropertyRefs())
				{
					yield return propertyRef4;
				}
				IEnumerator<PropertyRef> enumerator2 = null;
				yield break;
			}
			PlanCompiler.Assert(false, "Unknown type");
			yield break;
			yield break;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000256CB File Offset: 0x000238CB
		private IEnumerable<PropertyRef> GetPropertyRefs(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind)
		{
			PlanCompiler.Assert(opKind != NominalTypeEliminator.OperationKind.All, "unexpected attempt to GetPropertyRefs(...,OperationKind.All)");
			if (opKind == NominalTypeEliminator.OperationKind.GetKeys)
			{
				return typeInfo.GetKeyPropertyRefs();
			}
			if (opKind == NominalTypeEliminator.OperationKind.GetIdentity)
			{
				return typeInfo.GetIdentityPropertyRefs();
			}
			return this.GetPropertyRefsForComparisonAndIsNull(typeInfo, opKind);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000256FC File Offset: 0x000238FC
		private IEnumerable<EdmProperty> GetProperties(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind)
		{
			if (opKind == NominalTypeEliminator.OperationKind.All)
			{
				foreach (EdmProperty edmProperty in typeInfo.GetAllProperties())
				{
					yield return edmProperty;
				}
				IEnumerator<EdmProperty> enumerator = null;
			}
			else
			{
				foreach (PropertyRef propertyRef in this.GetPropertyRefs(typeInfo, opKind))
				{
					yield return typeInfo.GetNewProperty(propertyRef);
				}
				IEnumerator<PropertyRef> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0002571C File Offset: 0x0002391C
		private void GetPropertyValues(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind, Node input, bool ignoreMissingProperties, out List<EdmProperty> properties, out List<Node> values)
		{
			values = new List<Node>();
			properties = new List<EdmProperty>();
			foreach (EdmProperty property in this.GetProperties(typeInfo, opKind))
			{
				KeyValuePair<EdmProperty, Node> propertyValue = this.GetPropertyValue(input, property, ignoreMissingProperties);
				if (propertyValue.Value != null)
				{
					properties.Add(propertyValue.Key);
					values.Add(propertyValue.Value);
				}
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000257A8 File Offset: 0x000239A8
		private KeyValuePair<EdmProperty, Node> GetPropertyValue(Node input, EdmProperty property, bool ignoreMissingProperties)
		{
			Node value;
			if (!ignoreMissingProperties)
			{
				value = this.BuildAccessorWithNulls(input, property);
			}
			else
			{
				value = this.BuildAccessor(input, property);
			}
			return new KeyValuePair<EdmProperty, Node>(property, value);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000257D8 File Offset: 0x000239D8
		private List<System.Data.Query.InternalTrees.SortKey> HandleSortKeys(List<System.Data.Query.InternalTrees.SortKey> keys)
		{
			List<System.Data.Query.InternalTrees.SortKey> list = new List<System.Data.Query.InternalTrees.SortKey>();
			bool flag = false;
			foreach (System.Data.Query.InternalTrees.SortKey sortKey in keys)
			{
				VarInfo varInfo;
				if (!this.m_varInfoMap.TryGetVarInfo(sortKey.Var, out varInfo))
				{
					list.Add(sortKey);
				}
				else
				{
					StructuredVarInfo structuredVarInfo = varInfo as StructuredVarInfo;
					if (structuredVarInfo != null && structuredVarInfo.NewVarsIncludeNullSentinelVar)
					{
						this.m_compilerState.HasSortingOnNullSentinels = true;
					}
					foreach (Var v in varInfo.NewVars)
					{
						System.Data.Query.InternalTrees.SortKey item = Command.CreateSortKey(v, sortKey.AscendingSort, sortKey.Collation);
						list.Add(item);
					}
					flag = true;
				}
			}
			return flag ? list : keys;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000258D8 File Offset: 0x00023AD8
		private Node CreateTVFProjection(Node unnestNode, List<Var> unnestOpTableColumns, TypeInfo unnestOpTableTypeInfo, out List<Var> newVars)
		{
			RowType rowType = unnestOpTableTypeInfo.Type.EdmType as RowType;
			PlanCompiler.Assert(rowType != null, "Unexpected TVF return type (must be row): " + unnestOpTableTypeInfo.Type.ToString());
			List<Var> list = new List<Var>();
			List<Node> list2 = new List<Node>();
			PropertyRef[] array = unnestOpTableTypeInfo.PropertyRefList.ToArray<PropertyRef>();
			Dictionary<EdmProperty, PropertyRef> dictionary = new Dictionary<EdmProperty, PropertyRef>();
			foreach (PropertyRef propertyRef in array)
			{
				dictionary.Add(unnestOpTableTypeInfo.GetNewProperty(propertyRef), propertyRef);
			}
			foreach (EdmProperty key in unnestOpTableTypeInfo.FlattenedType.Properties)
			{
				PropertyRef propertyRef2 = dictionary[key];
				Var var = null;
				SimplePropertyRef simplePropertyRef = propertyRef2 as SimplePropertyRef;
				if (simplePropertyRef != null)
				{
					int num = rowType.Members.IndexOf(simplePropertyRef.Property);
					PlanCompiler.Assert(num >= 0, "Can't find a column in the TVF result type");
					list2.Add(this.m_command.CreateVarDefNode(this.m_command.CreateNode(this.m_command.CreateVarRefOp(unnestOpTableColumns[num])), out var));
				}
				else
				{
					NullSentinelPropertyRef nullSentinelPropertyRef = propertyRef2 as NullSentinelPropertyRef;
					if (nullSentinelPropertyRef != null)
					{
						list2.Add(this.m_command.CreateVarDefNode(this.CreateNullSentinelConstant(), out var));
					}
				}
				PlanCompiler.Assert(var != null, "TVFs returning a collection of rows with non-primitive properties are not supported");
				list.Add(var);
			}
			newVars = list;
			return this.m_command.CreateNode(this.m_command.CreateProjectOp(this.m_command.CreateVarVec(list)), unnestNode, this.m_command.CreateNode(this.m_command.CreateVarDefListOp(), list2));
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00025A9C File Offset: 0x00023C9C
		public override Node Visit(VarDefListOp op, Node n)
		{
			this.VisitChildren(n);
			List<Node> list = new List<Node>();
			foreach (Node node in n.Children)
			{
				PlanCompiler.Assert(node.Op is VarDefOp, "VarDefOp expected");
				VarDefOp varDefOp = (VarDefOp)node.Op;
				if (TypeUtils.IsStructuredType(varDefOp.Var.Type) || TypeUtils.IsCollectionType(varDefOp.Var.Type))
				{
					List<Node> list2;
					TypeUsage typeUsage;
					this.FlattenComputedVar((ComputedVar)varDefOp.Var, node, out list2, out typeUsage);
					using (List<Node>.Enumerator enumerator2 = list2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Node item = enumerator2.Current;
							list.Add(item);
						}
						continue;
					}
				}
				if (TypeSemantics.IsEnumerationType(varDefOp.Var.Type) || TypeSemantics.IsStrongSpatialType(varDefOp.Var.Type))
				{
					list.Add(this.FlattenEnumOrStrongSpatialVar(varDefOp, node.Child0));
				}
				else
				{
					list.Add(node);
				}
			}
			return this.m_command.CreateNode(n.Op, list);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00025BF8 File Offset: 0x00023DF8
		private void FlattenComputedVar(ComputedVar v, Node node, out List<Node> newNodes, out TypeUsage newType)
		{
			newNodes = new List<Node>();
			Node child = node.Child0;
			newType = null;
			if (TypeUtils.IsCollectionType(v.Type))
			{
				PlanCompiler.Assert(child.Op.OpType != OpType.Function, "Flattening of TVF output is not allowed.");
				newType = this.GetNewType(v.Type);
				Var newVar;
				Node item = this.m_command.CreateVarDefNode(child, out newVar);
				newNodes.Add(item);
				this.m_varInfoMap.CreateCollectionVarInfo(v, newVar);
				return;
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(v.Type);
			PropertyRefList propertyRefList = this.m_varPropertyMap[v];
			List<Var> list = new List<Var>();
			List<EdmProperty> list2 = new List<EdmProperty>();
			newNodes = new List<Node>();
			bool flag = false;
			foreach (PropertyRef propertyRef in typeInfo.PropertyRefList)
			{
				if (propertyRefList.Contains(propertyRef))
				{
					EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
					Node node2;
					if (propertyRefList.AllProperties)
					{
						node2 = this.BuildAccessorWithNulls(child, newProperty);
					}
					else
					{
						node2 = this.BuildAccessor(child, newProperty);
						if (node2 == null)
						{
							continue;
						}
					}
					list2.Add(newProperty);
					Var item3;
					Node item2 = this.m_command.CreateVarDefNode(node2, out item3);
					newNodes.Add(item2);
					list.Add(item3);
					if (!flag && this.IsNullSentinelPropertyRef(propertyRef))
					{
						flag = true;
					}
				}
			}
			this.m_varInfoMap.CreateStructuredVarInfo(v, typeInfo.FlattenedType, list, list2, flag);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00025D84 File Offset: 0x00023F84
		private bool IsNullSentinelPropertyRef(PropertyRef propertyRef)
		{
			if (propertyRef is NullSentinelPropertyRef)
			{
				return true;
			}
			NestedPropertyRef nestedPropertyRef = propertyRef as NestedPropertyRef;
			return nestedPropertyRef != null && nestedPropertyRef.OuterProperty is NullSentinelPropertyRef;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00025DB8 File Offset: 0x00023FB8
		private Node FlattenEnumOrStrongSpatialVar(VarDefOp varDefOp, Node node)
		{
			Var newVar;
			Node result = this.m_command.CreateVarDefNode(node, out newVar);
			this.m_varInfoMap.CreatePrimitiveTypeVarInfo(varDefOp.Var, newVar);
			return result;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00025DE8 File Offset: 0x00023FE8
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			this.VisitChildren(n);
			VarList outputVars = this.FlattenVarList(op.Outputs);
			SimpleCollectionColumnMap columnMap = this.ExpandColumnMap(op.ColumnMap);
			PhysicalProjectOp op2 = this.m_command.CreatePhysicalProjectOp(outputVars, columnMap);
			n.Op = op2;
			return n;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00025E2C File Offset: 0x0002402C
		private SimpleCollectionColumnMap ExpandColumnMap(SimpleCollectionColumnMap columnMap)
		{
			VarRefColumnMap varRefColumnMap = columnMap.Element as VarRefColumnMap;
			PlanCompiler.Assert(varRefColumnMap != null, "Encountered a SimpleCollectionColumnMap element that is not VarRefColumnMap when expanding a column map in NominalTypeEliminator.");
			VarInfo varInfo;
			if (!this.m_varInfoMap.TryGetVarInfo(varRefColumnMap.Var, out varInfo))
			{
				return columnMap;
			}
			if (TypeUtils.IsStructuredType(varRefColumnMap.Var.Type))
			{
				TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(varRefColumnMap.Var.Type);
				PlanCompiler.Assert(typeInfo.RootType.FlattenedType.Properties.Count == varInfo.NewVars.Count, string.Concat(new string[]
				{
					"Var count mismatch; Expected ",
					typeInfo.RootType.FlattenedType.Properties.Count.ToString(),
					"; got ",
					varInfo.NewVars.Count.ToString(),
					" instead."
				}));
			}
			ColumnMapProcessor columnMapProcessor = new ColumnMapProcessor(varRefColumnMap, varInfo, this.m_typeInfo);
			ColumnMap columnMap2 = columnMapProcessor.ExpandColumnMap();
			return new SimpleCollectionColumnMap(TypeUtils.CreateCollectionType(columnMap2.Type), columnMap2.Name, columnMap2, columnMap.Keys, columnMap.ForeignKeys);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00025F57 File Offset: 0x00024157
		private IEnumerable<Var> FlattenVars(IEnumerable<Var> vars)
		{
			foreach (Var var in vars)
			{
				VarInfo varInfo;
				if (!this.m_varInfoMap.TryGetVarInfo(var, out varInfo))
				{
					yield return var;
				}
				else
				{
					foreach (Var var2 in varInfo.NewVars)
					{
						yield return var2;
					}
					List<Var>.Enumerator enumerator2 = default(List<Var>.Enumerator);
				}
			}
			IEnumerator<Var> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00025F70 File Offset: 0x00024170
		private VarVec FlattenVarSet(VarVec varSet)
		{
			return this.m_command.CreateVarVec(this.FlattenVars(varSet));
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00025F94 File Offset: 0x00024194
		private VarList FlattenVarList(VarList varList)
		{
			return Command.CreateVarList(this.FlattenVars(varList));
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00025FB0 File Offset: 0x000241B0
		public override Node Visit(DistinctOp op, Node n)
		{
			this.VisitChildren(n);
			VarVec keyVars = this.FlattenVarSet(op.Keys);
			n.Op = this.m_command.CreateDistinctOp(keyVars);
			return n;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00025FE4 File Offset: 0x000241E4
		public override Node Visit(GroupByOp op, Node n)
		{
			this.VisitChildren(n);
			VarVec varVec = this.FlattenVarSet(op.Keys);
			VarVec varVec2 = this.FlattenVarSet(op.Outputs);
			if (varVec != op.Keys || varVec2 != op.Outputs)
			{
				n.Op = this.m_command.CreateGroupByOp(varVec, varVec2);
			}
			return n;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00026038 File Offset: 0x00024238
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			this.VisitChildren(n);
			VarVec varVec = this.FlattenVarSet(op.Keys);
			VarVec varVec2 = this.FlattenVarSet(op.Inputs);
			VarVec varVec3 = this.FlattenVarSet(op.Outputs);
			if (varVec != op.Keys || varVec2 != op.Inputs || varVec3 != op.Outputs)
			{
				n.Op = this.m_command.CreateGroupByIntoOp(varVec, varVec2, varVec3);
			}
			return n;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000260A4 File Offset: 0x000242A4
		public override Node Visit(ProjectOp op, Node n)
		{
			this.VisitChildren(n);
			VarVec varVec = this.FlattenVarSet(op.Outputs);
			if (op.Outputs != varVec)
			{
				if (varVec.IsEmpty)
				{
					return n.Child0;
				}
				n.Op = this.m_command.CreateProjectOp(varVec);
			}
			return n;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000260F0 File Offset: 0x000242F0
		public override Node Visit(ScanTableOp op, Node n)
		{
			Var var = op.Table.Columns[0];
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(var.Type);
			RowType flattenedType = typeInfo.FlattenedType;
			List<EdmProperty> list = new List<EdmProperty>();
			List<EdmMember> list2 = new List<EdmMember>();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (object obj in TypeHelpers.GetAllStructuralMembers(var.Type.EdmType))
			{
				EdmProperty edmProperty = (EdmProperty)obj;
				hashSet.Add(edmProperty.Name);
			}
			foreach (EdmProperty edmProperty2 in flattenedType.Properties)
			{
				if (hashSet.Contains(edmProperty2.Name))
				{
					list.Add(edmProperty2);
				}
			}
			foreach (PropertyRef propertyRef in typeInfo.GetKeyPropertyRefs())
			{
				EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
				list2.Add(newProperty);
			}
			TableMD tableMetadata = this.m_command.CreateFlatTableDefinition(list, list2, op.Table.TableMetadata.Extent);
			Table table = this.m_command.CreateTableInstance(tableMetadata);
			VarInfo varInfo = this.m_varInfoMap.CreateStructuredVarInfo(var, flattenedType, table.Columns, list);
			n.Op = this.m_command.CreateScanTableOp(table);
			return n;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000262A0 File Offset: 0x000244A0
		internal static Var GetSingletonVar(Node n)
		{
			switch (n.Op.OpType)
			{
			case OpType.ScanTable:
			{
				ScanTableOp scanTableOp = (ScanTableOp)n.Op;
				if (scanTableOp.Table.Columns.Count != 1)
				{
					return null;
				}
				return scanTableOp.Table.Columns[0];
			}
			case OpType.Filter:
			case OpType.Sort:
			case OpType.ConstrainedSort:
			case OpType.SingleRow:
				return NominalTypeEliminator.GetSingletonVar(n.Child0);
			case OpType.Project:
			{
				ProjectOp projectOp = (ProjectOp)n.Op;
				if (projectOp.Outputs.Count != 1)
				{
					return null;
				}
				return projectOp.Outputs.First;
			}
			case OpType.Unnest:
			{
				UnnestOp unnestOp = (UnnestOp)n.Op;
				if (unnestOp.Table.Columns.Count != 1)
				{
					return null;
				}
				return unnestOp.Table.Columns[0];
			}
			case OpType.UnionAll:
			case OpType.Intersect:
			case OpType.Except:
			{
				SetOp setOp = (SetOp)n.Op;
				if (setOp.Outputs.Count != 1)
				{
					return null;
				}
				return setOp.Outputs.First;
			}
			case OpType.Distinct:
			{
				DistinctOp distinctOp = (DistinctOp)n.Op;
				if (distinctOp.Keys.Count != 1)
				{
					return null;
				}
				return distinctOp.Keys.First;
			}
			}
			return null;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00026408 File Offset: 0x00024608
		public override Node Visit(ScanViewOp op, Node n)
		{
			Var singletonVar = NominalTypeEliminator.GetSingletonVar(n.Child0);
			PlanCompiler.Assert(singletonVar != null, "cannot identify Var for the input node to the ScanViewOp");
			PlanCompiler.Assert(op.Table.Columns.Count == 1, "table for scanViewOp has more than on column?");
			Var var = op.Table.Columns[0];
			Node result = base.VisitNode(n.Child0);
			VarInfo varInfo;
			if (!this.m_varInfoMap.TryGetVarInfo(singletonVar, out varInfo))
			{
				PlanCompiler.Assert(false, "didn't find inputVar for scanViewOp?");
			}
			StructuredVarInfo structuredVarInfo = (StructuredVarInfo)varInfo;
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(var.Type);
			this.m_varInfoMap.CreateStructuredVarInfo(var, structuredVarInfo.NewType, structuredVarInfo.NewVars, structuredVarInfo.Fields);
			return result;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000264C4 File Offset: 0x000246C4
		public override Node Visit(SortOp op, Node n)
		{
			this.VisitChildren(n);
			List<System.Data.Query.InternalTrees.SortKey> list = this.HandleSortKeys(op.Keys);
			if (list != op.Keys)
			{
				n.Op = this.m_command.CreateSortOp(list);
			}
			return n;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00026504 File Offset: 0x00024704
		public override Node Visit(UnnestOp op, Node n)
		{
			this.VisitChildren(n);
			Var var = null;
			EdmFunction edmFunction = null;
			if (n.HasChild0)
			{
				Node child = n.Child0;
				VarDefOp varDefOp = child.Op as VarDefOp;
				if (varDefOp != null && TypeUtils.IsCollectionType(varDefOp.Var.Type))
				{
					ComputedVar computedVar = (ComputedVar)varDefOp.Var;
					if (child.HasChild0 && child.Child0.Op.OpType == OpType.Function)
					{
						var = computedVar;
						edmFunction = ((FunctionOp)child.Child0.Op).Function;
					}
					else
					{
						List<Node> list = new List<Node>();
						TypeUsage typeUsage;
						this.FlattenComputedVar(computedVar, child, out list, out typeUsage);
						PlanCompiler.Assert(list.Count == 1, "Flattening unnest var produced more than one Var.");
						n.Child0 = list[0];
					}
				}
			}
			if (edmFunction != null)
			{
				PlanCompiler.Assert(var != null, "newUnnestVar must be initialized in the TVF case.");
			}
			else
			{
				VarInfo varInfo;
				if (!this.m_varInfoMap.TryGetVarInfo(op.Var, out varInfo) || varInfo.Kind != VarInfoKind.CollectionVarInfo)
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.WrongVarType);
				}
				var = ((CollectionVarInfo)varInfo).NewVar;
			}
			Var var2 = op.Table.Columns[0];
			if (!TypeUtils.IsStructuredType(var2.Type))
			{
				PlanCompiler.Assert(edmFunction == null, "TVFs returning a collection of values of a non-structured type are not supported");
				if (TypeSemantics.IsEnumerationType(var2.Type) || TypeSemantics.IsStrongSpatialType(var2.Type))
				{
					UnnestOp unnestOp = this.m_command.CreateUnnestOp(var);
					this.m_varInfoMap.CreatePrimitiveTypeVarInfo(var2, unnestOp.Table.Columns[0]);
					n.Op = unnestOp;
				}
				else
				{
					n.Op = this.m_command.CreateUnnestOp(var, op.Table);
				}
			}
			else
			{
				TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(var2.Type);
				TableMD tableMetadata;
				if (edmFunction != null)
				{
					RowType tvfReturnType = TypeHelpers.GetTvfReturnType(edmFunction);
					PlanCompiler.Assert(Command.EqualTypes(tvfReturnType, var2.Type.EdmType), "Unexpected TVF return type (row type is expected).");
					tableMetadata = this.m_command.CreateFlatTableDefinition(tvfReturnType.Properties, this.GetTvfResultKeys(edmFunction), null);
				}
				else
				{
					tableMetadata = this.m_command.CreateFlatTableDefinition(typeInfo.FlattenedType);
				}
				Table table = this.m_command.CreateTableInstance(tableMetadata);
				n.Op = this.m_command.CreateUnnestOp(var, table);
				List<Var> columns;
				if (edmFunction != null)
				{
					n = this.CreateTVFProjection(n, table.Columns, typeInfo, out columns);
				}
				else
				{
					columns = table.Columns;
				}
				this.m_varInfoMap.CreateStructuredVarInfo(var2, typeInfo.FlattenedType, columns, typeInfo.FlattenedType.Properties.ToList<EdmProperty>());
			}
			return n;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00026794 File Offset: 0x00024994
		private IEnumerable<EdmProperty> GetTvfResultKeys(EdmFunction tvf)
		{
			EdmProperty[] result;
			if (this.m_tvfResultKeys.TryGetValue(tvf, out result))
			{
				return result;
			}
			return Enumerable.Empty<EdmProperty>();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000267B8 File Offset: 0x000249B8
		protected override Node VisitSetOp(SetOp op, Node n)
		{
			this.VisitChildren(n);
			for (int i = 0; i < op.VarMap.Length; i++)
			{
				List<ComputedVar> list;
				op.VarMap[i] = this.FlattenVarMap(op.VarMap[i], out list);
				if (list != null)
				{
					n.Children[i] = this.FixupSetOpChild(n.Children[i], op.VarMap[i], list);
				}
			}
			op.Outputs.Clear();
			foreach (Var v in op.VarMap[0].Keys)
			{
				op.Outputs.Set(v);
			}
			return n;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00026880 File Offset: 0x00024A80
		private Node FixupSetOpChild(Node setOpChild, VarMap varMap, List<ComputedVar> newComputedVars)
		{
			PlanCompiler.Assert(setOpChild != null, "null setOpChild?");
			PlanCompiler.Assert(varMap != null, "null varMap?");
			PlanCompiler.Assert(newComputedVars != null, "null newComputedVars?");
			VarVec varVec = this.m_command.CreateVarVec();
			foreach (KeyValuePair<Var, Var> keyValuePair in varMap)
			{
				varVec.Set(keyValuePair.Value);
			}
			List<Node> list = new List<Node>();
			foreach (Var var in newComputedVars)
			{
				VarDefOp op = this.m_command.CreateVarDefOp(var);
				Node item = this.m_command.CreateNode(op, this.CreateNullConstantNode(var.Type));
				list.Add(item);
			}
			Node arg = this.m_command.CreateNode(this.m_command.CreateVarDefListOp(), list);
			ProjectOp op2 = this.m_command.CreateProjectOp(varVec);
			return this.m_command.CreateNode(op2, setOpChild, arg);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000269B4 File Offset: 0x00024BB4
		private VarMap FlattenVarMap(VarMap varMap, out List<ComputedVar> newComputedVars)
		{
			newComputedVars = null;
			VarMap varMap2 = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in varMap)
			{
				VarInfo varInfo;
				if (!this.m_varInfoMap.TryGetVarInfo(keyValuePair.Value, out varInfo))
				{
					varMap2.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					VarInfo varInfo2;
					if (!this.m_varInfoMap.TryGetVarInfo(keyValuePair.Key, out varInfo2))
					{
						varInfo2 = this.FlattenSetOpVar((SetOpVar)keyValuePair.Key);
					}
					if (varInfo2.Kind == VarInfoKind.CollectionVarInfo)
					{
						varMap2.Add(((CollectionVarInfo)varInfo2).NewVar, ((CollectionVarInfo)varInfo).NewVar);
					}
					else if (varInfo2.Kind == VarInfoKind.PrimitiveTypeVarInfo)
					{
						varMap2.Add(((PrimitiveTypeVarInfo)varInfo2).NewVar, ((PrimitiveTypeVarInfo)varInfo).NewVar);
					}
					else
					{
						StructuredVarInfo structuredVarInfo = (StructuredVarInfo)varInfo2;
						StructuredVarInfo structuredVarInfo2 = (StructuredVarInfo)varInfo;
						foreach (EdmProperty edmProperty in structuredVarInfo.Fields)
						{
							Var var;
							bool condition = structuredVarInfo.TryGetVar(edmProperty, out var);
							PlanCompiler.Assert(condition, "Could not find VarInfo for prop " + edmProperty.Name);
							Var var2;
							if (!structuredVarInfo2.TryGetVar(edmProperty, out var2))
							{
								var2 = this.m_command.CreateComputedVar(var.Type);
								if (newComputedVars == null)
								{
									newComputedVars = new List<ComputedVar>();
								}
								newComputedVars.Add((ComputedVar)var2);
							}
							varMap2.Add(var, var2);
						}
					}
				}
			}
			return varMap2;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00026B8C File Offset: 0x00024D8C
		private VarInfo FlattenSetOpVar(SetOpVar v)
		{
			if (TypeUtils.IsCollectionType(v.Type))
			{
				TypeUsage newType = this.GetNewType(v.Type);
				Var newVar = this.m_command.CreateSetOpVar(newType);
				return this.m_varInfoMap.CreateCollectionVarInfo(v, newVar);
			}
			if (TypeSemantics.IsEnumerationType(v.Type) || TypeSemantics.IsStrongSpatialType(v.Type))
			{
				TypeUsage newType2 = this.GetNewType(v.Type);
				Var newVar2 = this.m_command.CreateSetOpVar(newType2);
				return this.m_varInfoMap.CreatePrimitiveTypeVarInfo(v, newVar2);
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(v.Type);
			PropertyRefList propertyRefList = this.m_varPropertyMap[v];
			List<Var> list = new List<Var>();
			List<EdmProperty> list2 = new List<EdmProperty>();
			bool flag = false;
			foreach (PropertyRef propertyRef in typeInfo.PropertyRefList)
			{
				if (propertyRefList.Contains(propertyRef))
				{
					EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
					list2.Add(newProperty);
					SetOpVar item = this.m_command.CreateSetOpVar(Helper.GetModelTypeUsage(newProperty));
					list.Add(item);
					if (!flag && this.IsNullSentinelPropertyRef(propertyRef))
					{
						flag = true;
					}
				}
			}
			return this.m_varInfoMap.CreateStructuredVarInfo(v, typeInfo.FlattenedType, list, list2, flag);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00026CE8 File Offset: 0x00024EE8
		public override Node Visit(SoftCastOp op, Node n)
		{
			TypeUsage type = n.Child0.Op.Type;
			TypeUsage type2 = op.Type;
			this.VisitChildren(n);
			TypeUsage newType = this.GetNewType(type2);
			if (TypeSemantics.IsRowType(type2))
			{
				PlanCompiler.Assert(n.Child0.Op.OpType == OpType.NewRecord, "Expected a record constructor here. Found " + n.Child0.Op.OpType.ToString() + " instead");
				TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
				TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(op.Type);
				NewRecordOp newRecordOp = this.m_command.CreateNewRecordOp(newType);
				List<Node> list = new List<Node>();
				IEnumerator<EdmProperty> enumerator = newRecordOp.Properties.GetEnumerator();
				int num = newRecordOp.Properties.Count;
				enumerator.MoveNext();
				IEnumerator<Node> enumerator2 = n.Child0.Children.GetEnumerator();
				int i = n.Child0.Children.Count;
				enumerator2.MoveNext();
				while (i < num)
				{
					PlanCompiler.Assert(typeInfo2.HasNullSentinelProperty && !typeInfo.HasNullSentinelProperty, "NullSentinelProperty mismatch on input?");
					list.Add(this.CreateNullSentinelConstant());
					enumerator.MoveNext();
					num--;
				}
				while (i > num)
				{
					PlanCompiler.Assert(!typeInfo2.HasNullSentinelProperty && typeInfo.HasNullSentinelProperty, "NullSentinelProperty mismatch on output?");
					enumerator2.MoveNext();
					i--;
				}
				do
				{
					EdmProperty member = enumerator.Current;
					Node item = this.BuildSoftCast(enumerator2.Current, Helper.GetModelTypeUsage(member));
					list.Add(item);
					enumerator.MoveNext();
				}
				while (enumerator2.MoveNext());
				return this.m_command.CreateNode(newRecordOp, list);
			}
			if (TypeSemantics.IsCollectionType(type2))
			{
				return this.BuildSoftCast(n.Child0, newType);
			}
			if (TypeSemantics.IsPrimitiveType(type2))
			{
				return n;
			}
			PlanCompiler.Assert(TypeSemantics.IsNominalType(type2) || TypeSemantics.IsReferenceType(type2), "Gasp! Not a nominal type or even a reference type");
			PlanCompiler.Assert(Command.EqualTypes(newType, n.Child0.Op.Type), "Types are not equal");
			return n.Child0;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00026F20 File Offset: 0x00025120
		public override Node Visit(CastOp op, Node n)
		{
			this.VisitChildren(n);
			if (TypeSemantics.IsEnumerationType(op.Type))
			{
				PlanCompiler.Assert(TypeSemantics.IsPrimitiveType(n.Child0.Op.Type), "Primitive type expected.");
				PrimitiveType underlyingEdmTypeForEnumType = Helper.GetUnderlyingEdmTypeForEnumType(op.Type.EdmType);
				return this.RewriteAsCastToUnderlyingType(underlyingEdmTypeForEnumType, op, n);
			}
			if (TypeSemantics.IsSpatialType(op.Type))
			{
				PlanCompiler.Assert(TypeSemantics.IsPrimitiveType(n.Child0.Op.Type, PrimitiveTypeKind.Geography) || TypeSemantics.IsPrimitiveType(n.Child0.Op.Type, PrimitiveTypeKind.Geometry), "Union spatial type expected.");
				PrimitiveType spatialNormalizedPrimitiveType = Helper.GetSpatialNormalizedPrimitiveType(op.Type.EdmType);
				return this.RewriteAsCastToUnderlyingType(spatialNormalizedPrimitiveType, op, n);
			}
			return n;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00026FE4 File Offset: 0x000251E4
		private Node RewriteAsCastToUnderlyingType(PrimitiveType underlyingType, CastOp op, Node n)
		{
			if (underlyingType.PrimitiveTypeKind == ((PrimitiveType)n.Child0.Op.Type.EdmType).PrimitiveTypeKind)
			{
				return n.Child0;
			}
			return this.m_command.CreateNode(this.m_command.CreateCastOp(TypeUsage.Create(underlyingType, op.Type.Facets)), n.Child0);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0002704C File Offset: 0x0002524C
		public override Node Visit(ConstantOp op, Node n)
		{
			PlanCompiler.Assert(n.Children.Count == 0, "Constant operations don't have children.");
			PlanCompiler.Assert(op.Value != null, "Value must not be null");
			if (TypeSemantics.IsEnumerationType(op.Type))
			{
				object value = op.Value.GetType().IsEnum ? Convert.ChangeType(op.Value, op.Value.GetType().GetEnumUnderlyingType(), CultureInfo.InvariantCulture) : op.Value;
				return this.m_command.CreateNode(this.m_command.CreateConstantOp(TypeHelpers.CreateEnumUnderlyingTypeUsage(op.Type), value));
			}
			if (TypeSemantics.IsStrongSpatialType(op.Type))
			{
				op.Type = TypeHelpers.CreateSpatialUnionTypeUsage(op.Type);
			}
			return n;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00027110 File Offset: 0x00025310
		public override Node Visit(CaseOp op, Node n)
		{
			bool thenClauseIsNull;
			bool flag = PlanCompilerUtil.IsRowTypeCaseOpWithNullability(op, n, out thenClauseIsNull);
			this.VisitChildren(n);
			Node result;
			if (flag && this.TryRewriteCaseOp(n, thenClauseIsNull, out result))
			{
				return result;
			}
			if (TypeUtils.IsCollectionType(op.Type) || TypeSemantics.IsEnumerationType(op.Type) || TypeSemantics.IsStrongSpatialType(op.Type))
			{
				TypeUsage newType = this.GetNewType(op.Type);
				n.Op = this.m_command.CreateCaseOp(newType);
				return n;
			}
			if (TypeUtils.IsStructuredType(op.Type))
			{
				PropertyRefList desiredProperties = this.m_nodePropertyMap[n];
				return this.FlattenCaseOp(op, n, this.m_typeInfo.GetTypeInfo(op.Type), desiredProperties);
			}
			return n;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000271C4 File Offset: 0x000253C4
		private bool TryRewriteCaseOp(Node n, bool thenClauseIsNull, out Node rewrittenNode)
		{
			rewrittenNode = n;
			if (!this.m_typeInfo.GetTypeInfo(n.Op.Type).HasNullSentinelProperty)
			{
				return false;
			}
			Node node = thenClauseIsNull ? n.Child2 : n.Child1;
			if (node.Op.OpType != OpType.NewRecord)
			{
				return false;
			}
			Node child = node.Child0;
			TypeUsage integerType = this.m_command.IntegerType;
			PlanCompiler.Assert(child.Op.Type.EdmEquals(integerType), "Column that is expected to be a null sentinel is not of Integer type.");
			CaseOp op = this.m_command.CreateCaseOp(integerType);
			List<Node> list = new List<Node>(3);
			list.Add(n.Child0);
			Node node2 = this.m_command.CreateNode(this.m_command.CreateNullOp(integerType));
			Node item = thenClauseIsNull ? node2 : child;
			Node item2 = thenClauseIsNull ? child : node2;
			list.Add(item);
			list.Add(item2);
			node.Child0 = this.m_command.CreateNode(op, list);
			rewrittenNode = node;
			return true;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000272C0 File Offset: 0x000254C0
		private Node FlattenCaseOp(CaseOp op, Node n, TypeInfo typeInfo, PropertyRefList desiredProperties)
		{
			List<EdmProperty> list = new List<EdmProperty>();
			List<Node> list2 = new List<Node>();
			foreach (PropertyRef propertyRef in typeInfo.PropertyRefList)
			{
				if (desiredProperties.Contains(propertyRef))
				{
					EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
					List<Node> list3 = new List<Node>();
					for (int i = 0; i < n.Children.Count - 1; i++)
					{
						Node item = this.Copy(n.Children[i]);
						list3.Add(item);
						i++;
						Node item2 = this.BuildAccessorWithNulls(n.Children[i], newProperty);
						list3.Add(item2);
					}
					Node item3 = this.BuildAccessorWithNulls(n.Children[n.Children.Count - 1], newProperty);
					list3.Add(item3);
					Node item4 = this.m_command.CreateNode(this.m_command.CreateCaseOp(Helper.GetModelTypeUsage(newProperty)), list3);
					list.Add(newProperty);
					list2.Add(item4);
				}
			}
			NewRecordOp op2 = this.m_command.CreateNewRecordOp(typeInfo.FlattenedTypeUsage, list);
			return this.m_command.CreateNode(op2, list2);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00027414 File Offset: 0x00025614
		public override Node Visit(CollectOp op, Node n)
		{
			this.VisitChildren(n);
			n.Op = this.m_command.CreateCollectOp(this.GetNewType(op.Type));
			return n;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0002743C File Offset: 0x0002563C
		public override Node Visit(ComparisonOp op, Node n)
		{
			TypeUsage type = ((ScalarOp)n.Child0.Op).Type;
			TypeUsage type2 = ((ScalarOp)n.Child1.Op).Type;
			if (!TypeUtils.IsStructuredType(type))
			{
				return this.VisitScalarOpDefault(op, n);
			}
			this.VisitChildren(n);
			PlanCompiler.Assert(!TypeSemantics.IsComplexType(type) && !TypeSemantics.IsComplexType(type2), "complex type?");
			PlanCompiler.Assert(op.OpType == OpType.EQ || op.OpType == OpType.NE, "non-equality comparison of structured types?");
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
			TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(type2);
			List<EdmProperty> list;
			List<Node> list2;
			this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.Equality, n.Child0, false, out list, out list2);
			List<EdmProperty> list3;
			List<Node> list4;
			this.GetPropertyValues(typeInfo2, NominalTypeEliminator.OperationKind.Equality, n.Child1, false, out list3, out list4);
			PlanCompiler.Assert(list.Count == list3.Count && list2.Count == list4.Count, "different shaped structured types?");
			Node node = null;
			for (int i = 0; i < list2.Count; i++)
			{
				ComparisonOp op2 = this.m_command.CreateComparisonOp(op.OpType);
				Node node2 = this.m_command.CreateNode(op2, list2[i], list4[i]);
				if (node == null)
				{
					node = node2;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.And), node, node2);
				}
			}
			return node;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000275B0 File Offset: 0x000257B0
		public override Node Visit(ConditionalOp op, Node n)
		{
			if (op.OpType != OpType.IsNull)
			{
				return this.VisitScalarOpDefault(op, n);
			}
			TypeUsage type = ((ScalarOp)n.Child0.Op).Type;
			if (!TypeUtils.IsStructuredType(type))
			{
				return this.VisitScalarOpDefault(op, n);
			}
			this.VisitChildren(n);
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
			List<EdmProperty> list = null;
			List<Node> list2 = null;
			this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.IsNull, n.Child0, false, out list, out list2);
			PlanCompiler.Assert(list.Count == list2.Count && list.Count > 0, "No properties returned from GetPropertyValues(IsNull)?");
			Node node = null;
			foreach (Node arg in list2)
			{
				Node node2 = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.IsNull), arg);
				if (node == null)
				{
					node = node2;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.And), node, node2);
				}
			}
			return node;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000276CC File Offset: 0x000258CC
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			this.VisitChildren(n);
			List<System.Data.Query.InternalTrees.SortKey> list = this.HandleSortKeys(op.Keys);
			if (list != op.Keys)
			{
				n.Op = this.m_command.CreateConstrainedSortOp(list, op.WithTies);
			}
			return n;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002770F File Offset: 0x0002590F
		public override Node Visit(GetEntityRefOp op, Node n)
		{
			return this.FlattenGetKeyOp(op, n);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0002770F File Offset: 0x0002590F
		public override Node Visit(GetRefKeyOp op, Node n)
		{
			return this.FlattenGetKeyOp(op, n);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0002771C File Offset: 0x0002591C
		private Node FlattenGetKeyOp(ScalarOp op, Node n)
		{
			PlanCompiler.Assert(op.OpType == OpType.GetEntityRef || op.OpType == OpType.GetRefKey, "Expecting GetEntityRef or GetRefKey ops");
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(((ScalarOp)n.Child0.Op).Type);
			TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(op.Type);
			this.VisitChildren(n);
			List<Node> list2;
			if (op.OpType == OpType.GetRefKey)
			{
				List<EdmProperty> list;
				this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.GetKeys, n.Child0, false, out list, out list2);
			}
			else
			{
				PlanCompiler.Assert(op.OpType == OpType.GetEntityRef, "Expected OpType.GetEntityRef: Found " + op.OpType.ToString());
				List<EdmProperty> list;
				this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.GetIdentity, n.Child0, false, out list, out list2);
			}
			if (typeInfo2.HasNullSentinelProperty && !typeInfo.HasNullSentinelProperty)
			{
				list2.Insert(0, this.CreateNullSentinelConstant());
			}
			List<EdmProperty> list3 = new List<EdmProperty>(typeInfo2.FlattenedType.Properties);
			PlanCompiler.Assert(list2.Count == list3.Count, "fieldTypes.Count mismatch?");
			NewRecordOp op2 = this.m_command.CreateNewRecordOp(typeInfo2.FlattenedTypeUsage, list3);
			return this.m_command.CreateNode(op2, list2);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00027854 File Offset: 0x00025A54
		private Node VisitPropertyOp(Op op, Node n, PropertyRef propertyRef, bool throwIfMissing)
		{
			PlanCompiler.Assert(op.OpType == OpType.Property || op.OpType == OpType.RelProperty, "Unexpected optype: " + op.OpType.ToString());
			TypeUsage type = n.Child0.Op.Type;
			TypeUsage type2 = op.Type;
			this.VisitChildren(n);
			if (TypeUtils.IsUdt(type))
			{
				return n;
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(type);
			Node result;
			if (TypeUtils.IsStructuredType(type2))
			{
				TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(type2);
				List<EdmProperty> list = new List<EdmProperty>();
				List<Node> list2 = new List<Node>();
				PropertyRefList propertyRefList = this.m_nodePropertyMap[n];
				foreach (PropertyRef propertyRef2 in typeInfo2.PropertyRefList)
				{
					if (propertyRefList.Contains(propertyRef2))
					{
						PropertyRef propertyRef3 = propertyRef2.CreateNestedPropertyRef(propertyRef);
						EdmProperty property;
						if (typeInfo.TryGetNewProperty(propertyRef3, throwIfMissing, out property))
						{
							EdmProperty newProperty = typeInfo2.GetNewProperty(propertyRef2);
							Node node = this.BuildAccessor(n.Child0, property);
							if (node != null)
							{
								list.Add(newProperty);
								list2.Add(node);
							}
						}
					}
				}
				Op op2 = this.m_command.CreateNewRecordOp(typeInfo2.FlattenedTypeUsage, list);
				result = this.m_command.CreateNode(op2, list2);
			}
			else
			{
				EdmProperty newProperty2 = typeInfo.GetNewProperty(propertyRef);
				result = this.BuildAccessorWithNulls(n.Child0, newProperty2);
			}
			return result;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000279E0 File Offset: 0x00025BE0
		public override Node Visit(PropertyOp op, Node n)
		{
			return this.VisitPropertyOp(op, n, new SimplePropertyRef(op.PropertyInfo), true);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000279F6 File Offset: 0x00025BF6
		public override Node Visit(RelPropertyOp op, Node n)
		{
			return this.VisitPropertyOp(op, n, new RelPropertyRef(op.PropertyInfo), false);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00027A0C File Offset: 0x00025C0C
		public override Node Visit(RefOp op, Node n)
		{
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(((ScalarOp)n.Child0.Op).Type);
			TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(op.Type);
			this.VisitChildren(n);
			List<EdmProperty> list;
			List<Node> list2;
			this.GetPropertyValues(typeInfo, NominalTypeEliminator.OperationKind.All, n.Child0, false, out list, out list2);
			List<EdmProperty> list3 = new List<EdmProperty>(typeInfo2.FlattenedType.Properties);
			if (typeInfo2.HasEntitySetIdProperty)
			{
				PlanCompiler.Assert(list3[0] == typeInfo2.EntitySetIdProperty, "OutputField0 must be the entitySetId property");
				if (typeInfo.HasNullSentinelProperty && !typeInfo2.HasNullSentinelProperty)
				{
					PlanCompiler.Assert(list3.Count == list.Count, "Mismatched field count: Expected " + list.Count.ToString() + "; Got " + list3.Count.ToString());
					NominalTypeEliminator.RemoveNullSentinel(typeInfo, list, list2, list3);
				}
				else
				{
					PlanCompiler.Assert(list3.Count == list.Count + 1, "Mismatched field count: Expected " + (list.Count + 1).ToString() + "; Got " + list3.Count.ToString());
				}
				int entitySetId = this.m_typeInfo.GetEntitySetId(op.EntitySet);
				list2.Insert(0, this.m_command.CreateNode(this.m_command.CreateInternalConstantOp(Helper.GetModelTypeUsage(typeInfo2.EntitySetIdProperty), entitySetId)));
			}
			else
			{
				if (typeInfo.HasNullSentinelProperty && !typeInfo2.HasNullSentinelProperty)
				{
					NominalTypeEliminator.RemoveNullSentinel(typeInfo, list, list2, list3);
				}
				PlanCompiler.Assert(list3.Count == list.Count, "Mismatched field count: Expected " + list.Count.ToString() + "; Got " + list3.Count.ToString());
			}
			NewRecordOp op2 = this.m_command.CreateNewRecordOp(typeInfo2.FlattenedTypeUsage, list3);
			return this.m_command.CreateNode(op2, list2);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00027C08 File Offset: 0x00025E08
		private static void RemoveNullSentinel(TypeInfo inputTypeInfo, List<EdmProperty> inputFields, List<Node> inputFieldValues, List<EdmProperty> outputFields)
		{
			PlanCompiler.Assert(inputFields[0] == inputTypeInfo.NullSentinelProperty, "InputField0 must be the null sentinel property");
			inputFields.RemoveAt(0);
			inputFieldValues.RemoveAt(0);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00027C34 File Offset: 0x00025E34
		public override Node Visit(VarRefOp op, Node n)
		{
			VarInfo varInfo;
			if (!this.m_varInfoMap.TryGetVarInfo(op.Var, out varInfo))
			{
				bool condition = !TypeUtils.IsStructuredType(op.Type);
				string str = "No varInfo for a structured type var: Id = ";
				string str2 = op.Var.Id.ToString();
				string str3 = " Type = ";
				TypeUsage type = op.Type;
				PlanCompiler.Assert(condition, str + str2 + str3 + ((type != null) ? type.ToString() : null));
				return n;
			}
			if (varInfo.Kind == VarInfoKind.CollectionVarInfo)
			{
				n.Op = this.m_command.CreateVarRefOp(((CollectionVarInfo)varInfo).NewVar);
				return n;
			}
			if (varInfo.Kind == VarInfoKind.PrimitiveTypeVarInfo)
			{
				n.Op = this.m_command.CreateVarRefOp(((PrimitiveTypeVarInfo)varInfo).NewVar);
				return n;
			}
			StructuredVarInfo structuredVarInfo = (StructuredVarInfo)varInfo;
			NewRecordOp op2 = this.m_command.CreateNewRecordOp(structuredVarInfo.NewTypeUsage, structuredVarInfo.Fields);
			List<Node> list = new List<Node>();
			foreach (Var v in varInfo.NewVars)
			{
				VarRefOp op3 = this.m_command.CreateVarRefOp(v);
				list.Add(this.m_command.CreateNode(op3));
			}
			return this.m_command.CreateNode(op2, list);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00027D88 File Offset: 0x00025F88
		public override Node Visit(NewEntityOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00027D88 File Offset: 0x00025F88
		public override Node Visit(NewInstanceOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00027D88 File Offset: 0x00025F88
		public override Node Visit(DiscriminatedNewEntityOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00027D94 File Offset: 0x00025F94
		private Node NormalizeTypeDiscriminatorValues(DiscriminatedNewEntityOp op, Node discriminator)
		{
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.Type);
			CaseOp op2 = this.m_command.CreateCaseOp(typeInfo.RootType.TypeIdProperty.TypeUsage);
			List<Node> list = new List<Node>(op.DiscriminatorMap.TypeMap.Count * 2 - 1);
			for (int i = 0; i < op.DiscriminatorMap.TypeMap.Count; i++)
			{
				object key = op.DiscriminatorMap.TypeMap[i].Key;
				EntityType value = op.DiscriminatorMap.TypeMap[i].Value;
				TypeInfo typeInfo2 = this.m_typeInfo.GetTypeInfo(TypeUsage.Create(value));
				Node item = this.CreateTypeIdConstant(typeInfo2);
				if (i == op.DiscriminatorMap.TypeMap.Count - 1)
				{
					list.Add(item);
				}
				else
				{
					ConstantBaseOp op3 = this.m_command.CreateConstantOp(Helper.GetModelTypeUsage(op.DiscriminatorMap.DiscriminatorProperty.TypeUsage), key);
					Node arg = this.m_command.CreateNode(op3);
					ComparisonOp op4 = this.m_command.CreateComparisonOp(OpType.EQ);
					Node item2 = this.m_command.CreateNode(op4, discriminator, arg);
					list.Add(item2);
					list.Add(item);
				}
			}
			discriminator = this.m_command.CreateNode(op2, list);
			return discriminator;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00027D88 File Offset: 0x00025F88
		public override Node Visit(NewRecordOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00027EF4 File Offset: 0x000260F4
		private Node GetEntitySetIdExpr(EdmProperty entitySetIdProperty, NewEntityBaseOp op)
		{
			EntitySet entitySet = op.EntitySet;
			Node result;
			if (entitySet != null)
			{
				int entitySetId = this.m_typeInfo.GetEntitySetId(entitySet);
				InternalConstantOp op2 = this.m_command.CreateInternalConstantOp(Helper.GetModelTypeUsage(entitySetIdProperty), entitySetId);
				result = this.m_command.CreateNode(op2);
			}
			else
			{
				result = this.CreateNullConstantNode(Helper.GetModelTypeUsage(entitySetIdProperty));
			}
			return result;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00027F50 File Offset: 0x00026150
		private Node FlattenConstructor(ScalarOp op, Node n)
		{
			PlanCompiler.Assert(op.OpType == OpType.NewInstance || op.OpType == OpType.NewRecord || op.OpType == OpType.DiscriminatedNewEntity || op.OpType == OpType.NewEntity, "unexpected op: " + op.OpType.ToString() + "?");
			this.VisitChildren(n);
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.Type);
			RowType flattenedType = typeInfo.FlattenedType;
			NewEntityBaseOp newEntityBaseOp = op as NewEntityBaseOp;
			DiscriminatedNewEntityOp discriminatedNewEntityOp = null;
			IEnumerable enumerable;
			if (op.OpType == OpType.NewRecord)
			{
				enumerable = ((NewRecordOp)op).Properties;
			}
			else if (op.OpType == OpType.DiscriminatedNewEntity)
			{
				discriminatedNewEntityOp = (DiscriminatedNewEntityOp)op;
				enumerable = discriminatedNewEntityOp.DiscriminatorMap.Properties;
			}
			else
			{
				enumerable = TypeHelpers.GetAllStructuralMembers(op.Type);
			}
			List<EdmProperty> list = new List<EdmProperty>();
			List<Node> list2 = new List<Node>();
			if (typeInfo.HasTypeIdProperty)
			{
				list.Add(typeInfo.TypeIdProperty);
				if (discriminatedNewEntityOp == null)
				{
					list2.Add(this.CreateTypeIdConstant(typeInfo));
				}
				else
				{
					Node node = n.Children[0];
					if (typeInfo.RootType.DiscriminatorMap == null)
					{
						node = this.NormalizeTypeDiscriminatorValues(discriminatedNewEntityOp, node);
					}
					list2.Add(node);
				}
			}
			if (typeInfo.HasEntitySetIdProperty)
			{
				list.Add(typeInfo.EntitySetIdProperty);
				PlanCompiler.Assert(newEntityBaseOp != null, "unexpected optype:" + op.OpType.ToString());
				Node entitySetIdExpr = this.GetEntitySetIdExpr(typeInfo.EntitySetIdProperty, newEntityBaseOp);
				list2.Add(entitySetIdExpr);
			}
			if (typeInfo.HasNullSentinelProperty)
			{
				list.Add(typeInfo.NullSentinelProperty);
				list2.Add(this.CreateNullSentinelConstant());
			}
			int num = (discriminatedNewEntityOp == null) ? 0 : 1;
			foreach (object obj in enumerable)
			{
				EdmMember edmMember = (EdmMember)obj;
				Node node2 = n.Children[num];
				if (TypeUtils.IsStructuredType(Helper.GetModelTypeUsage(edmMember)))
				{
					RowType flattenedType2 = this.m_typeInfo.GetTypeInfo(Helper.GetModelTypeUsage(edmMember)).FlattenedType;
					int num2 = typeInfo.RootType.GetNestedStructureOffset(new SimplePropertyRef(edmMember));
					using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator2 = flattenedType2.Properties.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							EdmProperty property = enumerator2.Current;
							Node node3 = this.BuildAccessor(node2, property);
							if (node3 != null)
							{
								list.Add(flattenedType.Properties[num2]);
								list2.Add(node3);
							}
							num2++;
						}
						goto IL_29F;
					}
					goto IL_27A;
				}
				goto IL_27A;
				IL_29F:
				num++;
				continue;
				IL_27A:
				PropertyRef propertyRef = new SimplePropertyRef(edmMember);
				EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
				list.Add(newProperty);
				list2.Add(node2);
				goto IL_29F;
			}
			if (newEntityBaseOp != null)
			{
				foreach (RelProperty relProperty in newEntityBaseOp.RelationshipProperties)
				{
					Node input = n.Children[num];
					RowType flattenedType3 = this.m_typeInfo.GetTypeInfo(relProperty.ToEnd.TypeUsage).FlattenedType;
					int num3 = typeInfo.RootType.GetNestedStructureOffset(new RelPropertyRef(relProperty));
					foreach (EdmProperty property2 in flattenedType3.Properties)
					{
						Node node4 = this.BuildAccessor(input, property2);
						if (node4 != null)
						{
							list.Add(flattenedType.Properties[num3]);
							list2.Add(node4);
						}
						num3++;
					}
					num++;
				}
			}
			NewRecordOp op2 = this.m_command.CreateNewRecordOp(typeInfo.FlattenedTypeUsage, list);
			return this.m_command.CreateNode(op2, list2);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002836C File Offset: 0x0002656C
		public override Node Visit(NullOp op, Node n)
		{
			if (!TypeUtils.IsStructuredType(op.Type))
			{
				if (TypeSemantics.IsEnumerationType(op.Type))
				{
					op.Type = TypeHelpers.CreateEnumUnderlyingTypeUsage(op.Type);
				}
				else if (TypeSemantics.IsStrongSpatialType(op.Type))
				{
					op.Type = TypeHelpers.CreateSpatialUnionTypeUsage(op.Type);
				}
				return n;
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.Type);
			List<EdmProperty> list = new List<EdmProperty>();
			List<Node> list2 = new List<Node>();
			if (typeInfo.HasTypeIdProperty)
			{
				list.Add(typeInfo.TypeIdProperty);
				TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(typeInfo.TypeIdProperty);
				list2.Add(this.CreateNullConstantNode(modelTypeUsage));
			}
			NewRecordOp op2 = new NewRecordOp(typeInfo.FlattenedTypeUsage, list);
			return this.m_command.CreateNode(op2, list2);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00028430 File Offset: 0x00026630
		public override Node Visit(IsOfOp op, Node n)
		{
			this.VisitChildren(n);
			if (!TypeUtils.IsStructuredType(op.IsOfType))
			{
				return n;
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.IsOfType);
			return this.CreateTypeComparisonOp(n.Child0, typeInfo, op.IsOfOnly);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002847C File Offset: 0x0002667C
		public override Node Visit(TreatOp op, Node n)
		{
			this.VisitChildren(n);
			ScalarOp scalarOp = (ScalarOp)n.Child0.Op;
			if (op.IsFakeTreat || TypeSemantics.IsStructurallyEqual(scalarOp.Type, op.Type) || TypeSemantics.IsSubTypeOf(scalarOp.Type, op.Type))
			{
				return n.Child0;
			}
			if (!TypeUtils.IsStructuredType(op.Type))
			{
				return n;
			}
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(op.Type);
			Node arg = this.CreateTypeComparisonOp(n.Child0, typeInfo, false);
			CaseOp caseOp = this.m_command.CreateCaseOp(typeInfo.FlattenedTypeUsage);
			Node n2 = this.m_command.CreateNode(caseOp, arg, n.Child0, this.CreateNullConstantNode(caseOp.Type));
			PropertyRefList desiredProperties = this.m_nodePropertyMap[n];
			return this.FlattenCaseOp(caseOp, n2, typeInfo, desiredProperties);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00028558 File Offset: 0x00026758
		private Node CreateTypeComparisonOp(Node input, TypeInfo typeInfo, bool isExact)
		{
			Node node = this.BuildTypeIdAccessor(input, typeInfo);
			Node result;
			if (isExact)
			{
				result = this.CreateTypeEqualsOp(typeInfo, node);
			}
			else if (typeInfo.RootType.DiscriminatorMap != null)
			{
				result = this.CreateDisjunctiveTypeComparisonOp(typeInfo, node);
			}
			else
			{
				Node arg = this.CreateTypeIdConstantForPrefixMatch(typeInfo);
				LikeOp op = this.m_command.CreateLikeOp();
				result = this.m_command.CreateNode(op, node, arg, this.CreateNullConstantNode(this.DefaultTypeIdType));
			}
			return result;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000285C8 File Offset: 0x000267C8
		private Node CreateDisjunctiveTypeComparisonOp(TypeInfo typeInfo, Node typeIdProperty)
		{
			PlanCompiler.Assert(typeInfo.RootType.DiscriminatorMap != null, "should be used only for DiscriminatorMap type checks");
			IEnumerable<TypeInfo> enumerable = from t in typeInfo.GetTypeHierarchy()
			where !t.Type.EdmType.Abstract
			select t;
			Node node = null;
			foreach (TypeInfo typeInfo2 in enumerable)
			{
				Node node2 = this.CreateTypeEqualsOp(typeInfo2, typeIdProperty);
				if (node == null)
				{
					node = node2;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.Or), node, node2);
				}
			}
			if (node == null)
			{
				node = this.m_command.CreateNode(this.m_command.CreateFalseOp());
			}
			return node;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00028698 File Offset: 0x00026898
		private Node CreateTypeEqualsOp(TypeInfo typeInfo, Node typeIdProperty)
		{
			Node arg = this.CreateTypeIdConstant(typeInfo);
			ComparisonOp op = this.m_command.CreateComparisonOp(OpType.EQ);
			return this.m_command.CreateNode(op, typeIdProperty, arg);
		}

		// Token: 0x040007C5 RID: 1989
		private readonly Dictionary<Var, PropertyRefList> m_varPropertyMap;

		// Token: 0x040007C6 RID: 1990
		private readonly Dictionary<Node, PropertyRefList> m_nodePropertyMap;

		// Token: 0x040007C7 RID: 1991
		private readonly VarInfoMap m_varInfoMap;

		// Token: 0x040007C8 RID: 1992
		private readonly PlanCompiler m_compilerState;

		// Token: 0x040007C9 RID: 1993
		private readonly StructuredTypeInfo m_typeInfo;

		// Token: 0x040007CA RID: 1994
		private readonly Dictionary<EdmFunction, EdmProperty[]> m_tvfResultKeys;

		// Token: 0x040007CB RID: 1995
		private Dictionary<TypeUsage, TypeUsage> m_typeToNewTypeMap;

		// Token: 0x040007CC RID: 1996
		private const string PrefixMatchCharacter = "%";

		// Token: 0x02000476 RID: 1142
		internal enum OperationKind
		{
			// Token: 0x0400197B RID: 6523
			Equality,
			// Token: 0x0400197C RID: 6524
			IsNull,
			// Token: 0x0400197D RID: 6525
			GetIdentity,
			// Token: 0x0400197E RID: 6526
			GetKeys,
			// Token: 0x0400197F RID: 6527
			All
		}
	}
}
