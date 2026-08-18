using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000685 RID: 1669
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class NominalTypeEliminator : BasicOpVisitorOfNode
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06004198 RID: 16792 RVA: 0x00132E70 File Offset: 0x00131070
		private Command m_command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x00132E80 File Offset: 0x00131080
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

		// Token: 0x0600419A RID: 16794 RVA: 0x00132ED4 File Offset: 0x001310D4
		internal static void Process(PlanCompiler compilerState, StructuredTypeInfo structuredTypeInfo, Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys)
		{
			Dictionary<Var, PropertyRefList> varPropertyMap;
			Dictionary<Node, PropertyRefList> nodePropertyMap;
			PropertyPushdownHelper.Process(compilerState.Command, out varPropertyMap, out nodePropertyMap);
			NominalTypeEliminator nominalTypeEliminator = new NominalTypeEliminator(compilerState, structuredTypeInfo, varPropertyMap, nodePropertyMap, tvfResultKeys);
			nominalTypeEliminator.Process();
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x00132F20 File Offset: 0x00131120
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "PhysicalProjectOp")]
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

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x00132FE5 File Offset: 0x001311E5
		private TypeUsage DefaultTypeIdType
		{
			get
			{
				return this.m_command.StringType;
			}
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x00132FF4 File Offset: 0x001311F4
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

		// Token: 0x0600419E RID: 16798 RVA: 0x00133084 File Offset: 0x00131284
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

		// Token: 0x0600419F RID: 16799 RVA: 0x001330F0 File Offset: 0x001312F0
		private Node BuildAccessorWithNulls(Node input, EdmProperty property)
		{
			Node node = this.BuildAccessor(input, property);
			if (node == null)
			{
				node = this.CreateNullConstantNode(Helper.GetModelTypeUsage(property));
			}
			return node;
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x00133118 File Offset: 0x00131318
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

		// Token: 0x060041A1 RID: 16801 RVA: 0x00133148 File Offset: 0x00131348
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SoftCast")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-ScalarOp")]
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

		// Token: 0x060041A2 RID: 16802 RVA: 0x001331B0 File Offset: 0x001313B0
		private Node Copy(Node n)
		{
			return OpCopier.Copy(this.m_command, n);
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x001331BE File Offset: 0x001313BE
		private Node CreateNullConstantNode(TypeUsage type)
		{
			return this.m_command.CreateNode(this.m_command.CreateNullOp(type));
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x001331D8 File Offset: 0x001313D8
		private Node CreateNullSentinelConstant()
		{
			NullSentinelOp op = this.m_command.CreateNullSentinelOp();
			return this.m_command.CreateNode(op);
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x00133200 File Offset: 0x00131400
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

		// Token: 0x060041A6 RID: 16806 RVA: 0x0013325C File Offset: 0x0013145C
		private Node CreateTypeIdConstantForPrefixMatch(TypeInfo typeInfo)
		{
			string value = typeInfo.TypeId + "%";
			InternalConstantOp op = this.m_command.CreateInternalConstantOp(this.DefaultTypeIdType, value);
			return this.m_command.CreateNode(op);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x00133880 File Offset: 0x00131A80
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "isNull")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "opKind")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IsNull")]
		private IEnumerable<PropertyRef> GetPropertyRefsForComparisonAndIsNull(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind)
		{
			PlanCompiler.Assert(opKind == NominalTypeEliminator.OperationKind.IsNull || opKind == NominalTypeEliminator.OperationKind.Equality, "Unexpected opKind: " + opKind + "; Can only handle IsNull and Equality");
			TypeUsage currentType = typeInfo.Type;
			RowType recordType = null;
			if (TypeHelpers.TryGetEdmType<RowType>(currentType, out recordType))
			{
				if (opKind == NominalTypeEliminator.OperationKind.IsNull && typeInfo.HasNullSentinelProperty)
				{
					yield return NullSentinelPropertyRef.Instance;
				}
				else
				{
					foreach (EdmProperty i in recordType.Properties)
					{
						if (!TypeUtils.IsStructuredType(Helper.GetModelTypeUsage(i)))
						{
							yield return new SimplePropertyRef(i);
						}
						else
						{
							TypeInfo nestedTypeInfo = this.m_typeInfo.GetTypeInfo(Helper.GetModelTypeUsage(i));
							foreach (PropertyRef p in this.GetPropertyRefs(nestedTypeInfo, opKind))
							{
								PropertyRef nestedPropertyRef = p.CreateNestedPropertyRef(i);
								yield return nestedPropertyRef;
							}
						}
					}
				}
			}
			else
			{
				EntityType entityType = null;
				if (TypeHelpers.TryGetEdmType<EntityType>(currentType, out entityType))
				{
					if (opKind == NominalTypeEliminator.OperationKind.Equality || (opKind == NominalTypeEliminator.OperationKind.IsNull && !typeInfo.HasTypeIdProperty))
					{
						foreach (PropertyRef p2 in typeInfo.GetIdentityPropertyRefs())
						{
							yield return p2;
						}
					}
					else
					{
						yield return TypeIdPropertyRef.Instance;
					}
				}
				else
				{
					ComplexType complexType = null;
					if (TypeHelpers.TryGetEdmType<ComplexType>(currentType, out complexType))
					{
						PlanCompiler.Assert(opKind == NominalTypeEliminator.OperationKind.IsNull, "complex types not equality-comparable");
						PlanCompiler.Assert(typeInfo.HasNullSentinelProperty, "complex type with no null sentinel property: can't handle isNull");
						yield return NullSentinelPropertyRef.Instance;
					}
					else
					{
						RefType refType = null;
						if (TypeHelpers.TryGetEdmType<RefType>(currentType, out refType))
						{
							foreach (PropertyRef p3 in typeInfo.GetAllPropertyRefs())
							{
								yield return p3;
							}
						}
						else
						{
							PlanCompiler.Assert(false, "Unknown type");
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x001338AB File Offset: 0x00131AAB
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OperationKind")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetPropertyRefs")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060041A9 RID: 16809 RVA: 0x00133B64 File Offset: 0x00131D64
		private IEnumerable<EdmProperty> GetProperties(TypeInfo typeInfo, NominalTypeEliminator.OperationKind opKind)
		{
			if (opKind == NominalTypeEliminator.OperationKind.All)
			{
				foreach (EdmProperty p in typeInfo.GetAllProperties())
				{
					yield return p;
				}
			}
			else
			{
				foreach (PropertyRef p2 in this.GetPropertyRefs(typeInfo, opKind))
				{
					yield return typeInfo.GetNewProperty(p2);
				}
			}
			yield break;
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x00133B90 File Offset: 0x00131D90
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

		// Token: 0x060041AB RID: 16811 RVA: 0x00133C1C File Offset: 0x00131E1C
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

		// Token: 0x060041AC RID: 16812 RVA: 0x00133C4C File Offset: 0x00131E4C
		private List<System.Data.Entity.Core.Query.InternalTrees.SortKey> HandleSortKeys(List<System.Data.Entity.Core.Query.InternalTrees.SortKey> keys)
		{
			List<System.Data.Entity.Core.Query.InternalTrees.SortKey> list = new List<System.Data.Entity.Core.Query.InternalTrees.SortKey>();
			bool flag = false;
			foreach (System.Data.Entity.Core.Query.InternalTrees.SortKey sortKey in keys)
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
						System.Data.Entity.Core.Query.InternalTrees.SortKey item = Command.CreateSortKey(v, sortKey.AscendingSort, sortKey.Collation);
						list.Add(item);
					}
					flag = true;
				}
			}
			return flag ? list : keys;
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x00133D48 File Offset: 0x00131F48
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TVFs")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node CreateTVFProjection(Node unnestNode, List<Var> unnestOpTableColumns, TypeInfo unnestOpTableTypeInfo, out List<Var> newVars)
		{
			RowType rowType = unnestOpTableTypeInfo.Type.EdmType as RowType;
			PlanCompiler.Assert(rowType != null, "Unexpected TVF return type (must be row): " + unnestOpTableTypeInfo.Type);
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

		// Token: 0x060041AE RID: 16814 RVA: 0x00133F10 File Offset: 0x00132110
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDefOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060041AF RID: 16815 RVA: 0x00134064 File Offset: 0x00132264
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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
					if (!flag && NominalTypeEliminator.IsNullSentinelPropertyRef(propertyRef))
					{
						flag = true;
					}
				}
			}
			this.m_varInfoMap.CreateStructuredVarInfo(v, typeInfo.FlattenedType, list, list2, flag);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x001341F0 File Offset: 0x001323F0
		private static bool IsNullSentinelPropertyRef(PropertyRef propertyRef)
		{
			if (propertyRef is NullSentinelPropertyRef)
			{
				return true;
			}
			NestedPropertyRef nestedPropertyRef = propertyRef as NestedPropertyRef;
			return nestedPropertyRef != null && nestedPropertyRef.OuterProperty is NullSentinelPropertyRef;
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x00134224 File Offset: 0x00132424
		private Node FlattenEnumOrStrongSpatialVar(VarDefOp varDefOp, Node node)
		{
			Var newVar;
			Node result = this.m_command.CreateVarDefNode(node, out newVar);
			this.m_varInfoMap.CreatePrimitiveTypeVarInfo(varDefOp.Var, newVar);
			return result;
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x00134254 File Offset: 0x00132454
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			this.VisitChildren(n);
			VarList outputVars = this.FlattenVarList(op.Outputs);
			SimpleCollectionColumnMap columnMap = this.ExpandColumnMap(op.ColumnMap);
			PhysicalProjectOp op2 = this.m_command.CreatePhysicalProjectOp(outputVars, columnMap);
			n.Op = op2;
			return n;
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x00134298 File Offset: 0x00132498
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarRefColumnMap")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SimpleCollectionColumnMap")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NominalTypeEliminator")]
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
				PlanCompiler.Assert(typeInfo.RootType.FlattenedType.Properties.Count == varInfo.NewVars.Count, string.Concat(new object[]
				{
					"Var count mismatch; Expected ",
					typeInfo.RootType.FlattenedType.Properties.Count,
					"; got ",
					varInfo.NewVars.Count,
					" instead."
				}));
			}
			ColumnMapProcessor columnMapProcessor = new ColumnMapProcessor(varRefColumnMap, varInfo, this.m_typeInfo);
			ColumnMap columnMap2 = columnMapProcessor.ExpandColumnMap();
			return new SimpleCollectionColumnMap(TypeUtils.CreateCollectionType(columnMap2.Type), columnMap2.Name, columnMap2, columnMap.Keys, columnMap.ForeignKeys);
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x00134644 File Offset: 0x00132844
		private IEnumerable<Var> FlattenVars(IEnumerable<Var> vars)
		{
			foreach (Var v in vars)
			{
				VarInfo varInfo;
				if (!this.m_varInfoMap.TryGetVarInfo(v, out varInfo))
				{
					yield return v;
				}
				else
				{
					foreach (Var newVar in varInfo.NewVars)
					{
						yield return newVar;
					}
				}
			}
			yield break;
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x00134668 File Offset: 0x00132868
		private VarVec FlattenVarSet(VarVec varSet)
		{
			return this.m_command.CreateVarVec(this.FlattenVars(varSet));
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x0013468C File Offset: 0x0013288C
		private VarList FlattenVarList(VarList varList)
		{
			return Command.CreateVarList(this.FlattenVars(varList));
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x001346A8 File Offset: 0x001328A8
		public override Node Visit(DistinctOp op, Node n)
		{
			this.VisitChildren(n);
			VarVec keyVars = this.FlattenVarSet(op.Keys);
			n.Op = this.m_command.CreateDistinctOp(keyVars);
			return n;
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x001346DC File Offset: 0x001328DC
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

		// Token: 0x060041B9 RID: 16825 RVA: 0x00134730 File Offset: 0x00132930
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

		// Token: 0x060041BA RID: 16826 RVA: 0x0013479C File Offset: 0x0013299C
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

		// Token: 0x060041BB RID: 16827 RVA: 0x001347E8 File Offset: 0x001329E8
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
			this.m_varInfoMap.CreateStructuredVarInfo(var, flattenedType, table.Columns, list);
			n.Op = this.m_command.CreateScanTableOp(table);
			return n;
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x00134998 File Offset: 0x00132B98
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

		// Token: 0x060041BD RID: 16829 RVA: 0x00134B00 File Offset: 0x00132D00
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "inputVar")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "scanViewOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScanViewOp")]
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
			this.m_typeInfo.GetTypeInfo(var.Type);
			this.m_varInfoMap.CreateStructuredVarInfo(var, structuredVarInfo.NewType, structuredVarInfo.NewVars, structuredVarInfo.Fields);
			return result;
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x00134BC0 File Offset: 0x00132DC0
		public override Node Visit(SortOp op, Node n)
		{
			this.VisitChildren(n);
			List<System.Data.Entity.Core.Query.InternalTrees.SortKey> list = this.HandleSortKeys(op.Keys);
			if (list != op.Keys)
			{
				n.Op = this.m_command.CreateSortOp(list);
			}
			return n;
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x00134C00 File Offset: 0x00132E00
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "newUnnestVar")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TVFs")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "unnest")]
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
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1006));
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

		// Token: 0x060041C0 RID: 16832 RVA: 0x00134EA4 File Offset: 0x001330A4
		private IEnumerable<EdmProperty> GetTvfResultKeys(EdmFunction tvf)
		{
			EdmProperty[] result;
			if (this.m_tvfResultKeys.TryGetValue(tvf, out result))
			{
				return result;
			}
			return Enumerable.Empty<EdmProperty>();
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x00134EC8 File Offset: 0x001330C8
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

		// Token: 0x060041C2 RID: 16834 RVA: 0x00134F90 File Offset: 0x00133190
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "newComputedVars")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "varMap")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "setOpChild")]
		private Node FixupSetOpChild(Node setOpChild, VarMap varMap, List<ComputedVar> newComputedVars)
		{
			PlanCompiler.Assert(null != setOpChild, "null setOpChild?");
			PlanCompiler.Assert(null != varMap, "null varMap?");
			PlanCompiler.Assert(null != newComputedVars, "null newComputedVars?");
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

		// Token: 0x060041C3 RID: 16835 RVA: 0x001350D0 File Offset: 0x001332D0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarInfo")]
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

		// Token: 0x060041C4 RID: 16836 RVA: 0x001352A4 File Offset: 0x001334A4
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
					if (!flag && NominalTypeEliminator.IsNullSentinelPropertyRef(propertyRef))
					{
						flag = true;
					}
				}
			}
			return this.m_varInfoMap.CreateStructuredVarInfo(v, typeInfo.FlattenedType, list, list2, flag);
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x00135404 File Offset: 0x00133604
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "NullSentinelProperty")]
		public override Node Visit(SoftCastOp op, Node n)
		{
			TypeUsage type = n.Child0.Op.Type;
			TypeUsage type2 = op.Type;
			this.VisitChildren(n);
			TypeUsage newType = this.GetNewType(type2);
			if (TypeSemantics.IsRowType(type2))
			{
				PlanCompiler.Assert(n.Child0.Op.OpType == OpType.NewRecord, "Expected a record constructor here. Found " + n.Child0.Op.OpType + " instead");
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

		// Token: 0x060041C6 RID: 16838 RVA: 0x00135634 File Offset: 0x00133834
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060041C7 RID: 16839 RVA: 0x001356F8 File Offset: 0x001338F8
		private Node RewriteAsCastToUnderlyingType(PrimitiveType underlyingType, CastOp op, Node n)
		{
			if (underlyingType.PrimitiveTypeKind == ((PrimitiveType)n.Child0.Op.Type.EdmType).PrimitiveTypeKind)
			{
				return n.Child0;
			}
			return this.m_command.CreateNode(this.m_command.CreateCastOp(TypeUsage.Create(underlyingType, op.Type.Facets)), n.Child0);
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x00135760 File Offset: 0x00133960
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override Node Visit(ConstantOp op, Node n)
		{
			PlanCompiler.Assert(n.Children.Count == 0, "Constant operations don't have children.");
			PlanCompiler.Assert(op.Value != null, "Value must not be null");
			if (TypeSemantics.IsEnumerationType(op.Type))
			{
				object value = op.Value.GetType().IsEnum() ? Convert.ChangeType(op.Value, op.Value.GetType().GetEnumUnderlyingType(), CultureInfo.InvariantCulture) : op.Value;
				return this.m_command.CreateNode(this.m_command.CreateConstantOp(TypeHelpers.CreateEnumUnderlyingTypeUsage(op.Type), value));
			}
			if (TypeSemantics.IsStrongSpatialType(op.Type))
			{
				op.Type = TypeHelpers.CreateSpatialUnionTypeUsage(op.Type);
			}
			return n;
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x00135828 File Offset: 0x00133A28
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
				return this.FlattenCaseOp(n, this.m_typeInfo.GetTypeInfo(op.Type), desiredProperties);
			}
			return n;
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x001358DC File Offset: 0x00133ADC
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060041CB RID: 16843 RVA: 0x001359D8 File Offset: 0x00133BD8
		private Node FlattenCaseOp(Node n, TypeInfo typeInfo, PropertyRefList desiredProperties)
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
			NewRecordOp op = this.m_command.CreateNewRecordOp(typeInfo.FlattenedTypeUsage, list);
			return this.m_command.CreateNode(op, list2);
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x00135B2C File Offset: 0x00133D2C
		public override Node Visit(CollectOp op, Node n)
		{
			this.VisitChildren(n);
			n.Op = this.m_command.CreateCollectOp(this.GetNewType(op.Type));
			return n;
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x00135B54 File Offset: 0x00133D54
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override Node Visit(ComparisonOp op, Node n)
		{
			TypeUsage type = n.Child0.Op.Type;
			TypeUsage type2 = n.Child1.Op.Type;
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
				ComparisonOp op2 = this.m_command.CreateComparisonOp(op.OpType, op.UseDatabaseNullSemantics);
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

		// Token: 0x060041CE RID: 16846 RVA: 0x00135CC4 File Offset: 0x00133EC4
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetPropertyValues")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IsNull")]
		public override Node Visit(ConditionalOp op, Node n)
		{
			if (op.OpType != OpType.IsNull)
			{
				return this.VisitScalarOpDefault(op, n);
			}
			TypeUsage type = n.Child0.Op.Type;
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

		// Token: 0x060041CF RID: 16847 RVA: 0x00135DDC File Offset: 0x00133FDC
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			this.VisitChildren(n);
			List<System.Data.Entity.Core.Query.InternalTrees.SortKey> list = this.HandleSortKeys(op.Keys);
			if (list != op.Keys)
			{
				n.Op = this.m_command.CreateConstrainedSortOp(list, op.WithTies);
			}
			return n;
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x00135E1F File Offset: 0x0013401F
		public override Node Visit(GetEntityRefOp op, Node n)
		{
			return this.FlattenGetKeyOp(op, n);
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x00135E29 File Offset: 0x00134029
		public override Node Visit(GetRefKeyOp op, Node n)
		{
			return this.FlattenGetKeyOp(op, n);
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x00135E34 File Offset: 0x00134034
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "fieldTypes")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OpType")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetEntityRef")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetRefKey")]
		private Node FlattenGetKeyOp(ScalarOp op, Node n)
		{
			PlanCompiler.Assert(op.OpType == OpType.GetEntityRef || op.OpType == OpType.GetRefKey, "Expecting GetEntityRef or GetRefKey ops");
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(n.Child0.Op.Type);
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
				PlanCompiler.Assert(op.OpType == OpType.GetEntityRef, "Expected OpType.GetEntityRef: Found " + op.OpType);
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

		// Token: 0x060041D3 RID: 16851 RVA: 0x00135F5C File Offset: 0x0013415C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "optype")]
		private Node VisitPropertyOp(Op op, Node n, PropertyRef propertyRef, bool throwIfMissing)
		{
			PlanCompiler.Assert(op.OpType == OpType.Property || op.OpType == OpType.RelProperty, "Unexpected optype: " + op.OpType);
			TypeUsage type = n.Child0.Op.Type;
			TypeUsage type2 = op.Type;
			this.VisitChildren(n);
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

		// Token: 0x060041D4 RID: 16852 RVA: 0x001360D4 File Offset: 0x001342D4
		public override Node Visit(PropertyOp op, Node n)
		{
			return this.VisitPropertyOp(op, n, new SimplePropertyRef(op.PropertyInfo), true);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x001360EA File Offset: 0x001342EA
		public override Node Visit(RelPropertyOp op, Node n)
		{
			return this.VisitPropertyOp(op, n, new RelPropertyRef(op.PropertyInfo), false);
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x00136100 File Offset: 0x00134300
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "entitySetId")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override Node Visit(RefOp op, Node n)
		{
			TypeInfo typeInfo = this.m_typeInfo.GetTypeInfo(n.Child0.Op.Type);
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
					PlanCompiler.Assert(list3.Count == list.Count, string.Concat(new object[]
					{
						"Mismatched field count: Expected ",
						list.Count,
						"; Got ",
						list3.Count
					}));
					NominalTypeEliminator.RemoveNullSentinel(typeInfo, list, list2);
				}
				else
				{
					PlanCompiler.Assert(list3.Count == list.Count + 1, string.Concat(new object[]
					{
						"Mismatched field count: Expected ",
						list.Count + 1,
						"; Got ",
						list3.Count
					}));
				}
				int entitySetId = this.m_typeInfo.GetEntitySetId(op.EntitySet);
				list2.Insert(0, this.m_command.CreateNode(this.m_command.CreateInternalConstantOp(Helper.GetModelTypeUsage(typeInfo2.EntitySetIdProperty), entitySetId)));
			}
			else
			{
				if (typeInfo.HasNullSentinelProperty && !typeInfo2.HasNullSentinelProperty)
				{
					NominalTypeEliminator.RemoveNullSentinel(typeInfo, list, list2);
				}
				PlanCompiler.Assert(list3.Count == list.Count, string.Concat(new object[]
				{
					"Mismatched field count: Expected ",
					list.Count,
					"; Got ",
					list3.Count
				}));
			}
			NewRecordOp op2 = this.m_command.CreateNewRecordOp(typeInfo2.FlattenedTypeUsage, list3);
			return this.m_command.CreateNode(op2, list2);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x00136329 File Offset: 0x00134529
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private static void RemoveNullSentinel(TypeInfo inputTypeInfo, List<EdmProperty> inputFields, List<Node> inputFieldValues)
		{
			PlanCompiler.Assert(inputFields[0] == inputTypeInfo.NullSentinelProperty, "InputField0 must be the null sentinel property");
			inputFields.RemoveAt(0);
			inputFieldValues.RemoveAt(0);
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x00136354 File Offset: 0x00134554
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "varInfo")]
		public override Node Visit(VarRefOp op, Node n)
		{
			VarInfo varInfo;
			if (!this.m_varInfoMap.TryGetVarInfo(op.Var, out varInfo))
			{
				PlanCompiler.Assert(!TypeUtils.IsStructuredType(op.Type), string.Concat(new object[]
				{
					"No varInfo for a structured type var: Id = ",
					op.Var.Id,
					" Type = ",
					op.Type
				}));
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

		// Token: 0x060041D9 RID: 16857 RVA: 0x001364B0 File Offset: 0x001346B0
		public override Node Visit(NewEntityOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x001364BA File Offset: 0x001346BA
		public override Node Visit(NewInstanceOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x001364C4 File Offset: 0x001346C4
		public override Node Visit(DiscriminatedNewEntityOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x001364D0 File Offset: 0x001346D0
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
					ComparisonOp op4 = this.m_command.CreateComparisonOp(OpType.EQ, false);
					Node item2 = this.m_command.CreateNode(op4, discriminator, arg);
					list.Add(item2);
					list.Add(item);
				}
			}
			discriminator = this.m_command.CreateNode(op2, list);
			return discriminator;
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x00136630 File Offset: 0x00134830
		public override Node Visit(NewRecordOp op, Node n)
		{
			return this.FlattenConstructor(op, n);
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x0013663C File Offset: 0x0013483C
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

		// Token: 0x060041DF RID: 16863 RVA: 0x00136698 File Offset: 0x00134898
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "optype")]
		private Node FlattenConstructor(ScalarOp op, Node n)
		{
			PlanCompiler.Assert(op.OpType == OpType.NewInstance || op.OpType == OpType.NewRecord || op.OpType == OpType.DiscriminatedNewEntity || op.OpType == OpType.NewEntity, "unexpected op: " + op.OpType + "?");
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
				PlanCompiler.Assert(newEntityBaseOp != null, "unexpected optype:" + op.OpType);
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
						goto IL_28E;
					}
					goto IL_269;
				}
				goto IL_269;
				IL_28E:
				num++;
				continue;
				IL_269:
				PropertyRef propertyRef = new SimplePropertyRef(edmMember);
				EdmProperty newProperty = typeInfo.GetNewProperty(propertyRef);
				list.Add(newProperty);
				list2.Add(node2);
				goto IL_28E;
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

		// Token: 0x060041E0 RID: 16864 RVA: 0x00136AA4 File Offset: 0x00134CA4
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

		// Token: 0x060041E1 RID: 16865 RVA: 0x00136B68 File Offset: 0x00134D68
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

		// Token: 0x060041E2 RID: 16866 RVA: 0x00136BB4 File Offset: 0x00134DB4
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
			return this.FlattenCaseOp(n2, typeInfo, desiredProperties);
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x00136C90 File Offset: 0x00134E90
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

		// Token: 0x060041E4 RID: 16868 RVA: 0x00136D14 File Offset: 0x00134F14
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DiscriminatorMap")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060041E5 RID: 16869 RVA: 0x00136DE8 File Offset: 0x00134FE8
		private Node CreateTypeEqualsOp(TypeInfo typeInfo, Node typeIdProperty)
		{
			Node arg = this.CreateTypeIdConstant(typeInfo);
			ComparisonOp op = this.m_command.CreateComparisonOp(OpType.EQ, false);
			return this.m_command.CreateNode(op, typeIdProperty, arg);
		}

		// Token: 0x0400185E RID: 6238
		private const string PrefixMatchCharacter = "%";

		// Token: 0x0400185F RID: 6239
		private readonly Dictionary<Var, PropertyRefList> m_varPropertyMap;

		// Token: 0x04001860 RID: 6240
		private readonly Dictionary<Node, PropertyRefList> m_nodePropertyMap;

		// Token: 0x04001861 RID: 6241
		private readonly VarInfoMap m_varInfoMap;

		// Token: 0x04001862 RID: 6242
		private readonly PlanCompiler m_compilerState;

		// Token: 0x04001863 RID: 6243
		private readonly StructuredTypeInfo m_typeInfo;

		// Token: 0x04001864 RID: 6244
		private readonly Dictionary<EdmFunction, EdmProperty[]> m_tvfResultKeys;

		// Token: 0x04001865 RID: 6245
		private readonly Dictionary<TypeUsage, TypeUsage> m_typeToNewTypeMap;

		// Token: 0x02000686 RID: 1670
		internal enum OperationKind
		{
			// Token: 0x04001869 RID: 6249
			Equality,
			// Token: 0x0400186A RID: 6250
			IsNull,
			// Token: 0x0400186B RID: 6251
			GetIdentity,
			// Token: 0x0400186C RID: 6252
			GetKeys,
			// Token: 0x0400186D RID: 6253
			All
		}
	}
}
