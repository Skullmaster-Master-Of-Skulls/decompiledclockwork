using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000062 RID: 98
	internal class PropertyPushdownHelper : BasicOpVisitor
	{
		// Token: 0x06000850 RID: 2128 RVA: 0x0002C1FF File Offset: 0x0002A3FF
		private PropertyPushdownHelper(StructuredTypeInfo structuredTypeInfo)
		{
			this.m_structuredTypeInfo = structuredTypeInfo;
			this.m_varPropertyRefMap = new Dictionary<Var, PropertyRefList>();
			this.m_nodePropertyRefMap = new Dictionary<Node, PropertyRefList>();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002C224 File Offset: 0x0002A424
		internal static void Process(Command itree, StructuredTypeInfo structuredTypeInfo, out Dictionary<Var, PropertyRefList> varPropertyRefs, out Dictionary<Node, PropertyRefList> nodePropertyRefs)
		{
			PropertyPushdownHelper propertyPushdownHelper = new PropertyPushdownHelper(structuredTypeInfo);
			propertyPushdownHelper.Process(itree.Root);
			varPropertyRefs = propertyPushdownHelper.m_varPropertyRefMap;
			nodePropertyRefs = propertyPushdownHelper.m_nodePropertyRefMap;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002C254 File Offset: 0x0002A454
		private void Process(Node rootNode)
		{
			rootNode.Op.Accept(this, rootNode);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002C264 File Offset: 0x0002A464
		private PropertyRefList GetPropertyRefList(Node node)
		{
			PropertyRefList propertyRefList;
			if (!this.m_nodePropertyRefMap.TryGetValue(node, out propertyRefList))
			{
				propertyRefList = new PropertyRefList();
				this.m_nodePropertyRefMap[node] = propertyRefList;
			}
			return propertyRefList;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002C298 File Offset: 0x0002A498
		private void AddPropertyRefs(Node node, PropertyRefList propertyRefs)
		{
			PropertyRefList propertyRefList = this.GetPropertyRefList(node);
			propertyRefList.Append(propertyRefs);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
		private PropertyRefList GetPropertyRefList(Var v)
		{
			PropertyRefList propertyRefList;
			if (!this.m_varPropertyRefMap.TryGetValue(v, out propertyRefList))
			{
				propertyRefList = new PropertyRefList();
				this.m_varPropertyRefMap[v] = propertyRefList;
			}
			return propertyRefList;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002C2E8 File Offset: 0x0002A4E8
		private void AddPropertyRefs(Var v, PropertyRefList propertyRefs)
		{
			PropertyRefList propertyRefList = this.GetPropertyRefList(v);
			propertyRefList.Append(propertyRefs);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002C304 File Offset: 0x0002A504
		private static PropertyRefList GetIdentityProperties(EntityType type)
		{
			PropertyRefList keyProperties = PropertyPushdownHelper.GetKeyProperties(type);
			keyProperties.Add(EntitySetIdPropertyRef.Instance);
			return keyProperties;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002C324 File Offset: 0x0002A524
		private static PropertyRefList GetKeyProperties(EntityType entityType)
		{
			PropertyRefList propertyRefList = new PropertyRefList();
			foreach (EdmMember edmMember in entityType.KeyMembers)
			{
				EdmProperty edmProperty = edmMember as EdmProperty;
				PlanCompiler.Assert(edmProperty != null, "EntityType had non-EdmProperty key member?");
				SimplePropertyRef property = new SimplePropertyRef(edmProperty);
				propertyRefList.Add(property);
			}
			return propertyRefList;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002C39C File Offset: 0x0002A59C
		protected override void VisitDefault(Node n)
		{
			foreach (Node node in n.Children)
			{
				ScalarOp scalarOp = node.Op as ScalarOp;
				if (scalarOp != null && TypeUtils.IsStructuredType(scalarOp.Type))
				{
					this.AddPropertyRefs(node, PropertyRefList.All);
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002C418 File Offset: 0x0002A618
		public override void Visit(SoftCastOp op, Node n)
		{
			PropertyRefList propertyRefList = null;
			if (TypeSemantics.IsReferenceType(op.Type))
			{
				propertyRefList = PropertyRefList.All;
			}
			else if (TypeSemantics.IsNominalType(op.Type))
			{
				PropertyRefList propertyRefList2 = this.m_nodePropertyRefMap[n];
				propertyRefList = propertyRefList2.Clone();
			}
			else if (TypeSemantics.IsRowType(op.Type))
			{
				propertyRefList = PropertyRefList.All;
			}
			if (propertyRefList != null)
			{
				this.AddPropertyRefs(n.Child0, propertyRefList);
			}
			this.VisitChildren(n);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002C48C File Offset: 0x0002A68C
		public override void Visit(CaseOp op, Node n)
		{
			PropertyRefList propertyRefList = this.GetPropertyRefList(n);
			for (int i = 1; i < n.Children.Count - 1; i += 2)
			{
				PropertyRefList propertyRefs = propertyRefList.Clone();
				this.AddPropertyRefs(n.Children[i], propertyRefs);
			}
			this.AddPropertyRefs(n.Children[n.Children.Count - 1], propertyRefList.Clone());
			this.VisitChildren(n);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002C4FE File Offset: 0x0002A6FE
		public override void Visit(CollectOp op, Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002C508 File Offset: 0x0002A708
		public override void Visit(ComparisonOp op, Node n)
		{
			TypeUsage type = (n.Child0.Op as ScalarOp).Type;
			if (!TypeUtils.IsStructuredType(type))
			{
				this.VisitChildren(n);
				return;
			}
			if (TypeSemantics.IsRowType(type) || TypeSemantics.IsReferenceType(type))
			{
				this.VisitDefault(n);
				return;
			}
			PlanCompiler.Assert(TypeSemantics.IsEntityType(type), "unexpected childOpType?");
			PropertyRefList identityProperties = PropertyPushdownHelper.GetIdentityProperties(TypeHelpers.GetEdmType<EntityType>(type));
			foreach (Node node in n.Children)
			{
				this.AddPropertyRefs(node, identityProperties);
			}
			this.VisitChildren(n);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00013A81 File Offset: 0x00011C81
		public override void Visit(ElementOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002C5C0 File Offset: 0x0002A7C0
		public override void Visit(GetEntityRefOp op, Node n)
		{
			ScalarOp scalarOp = n.Child0.Op as ScalarOp;
			PlanCompiler.Assert(scalarOp != null, "input to GetEntityRefOp is not a ScalarOp?");
			EntityType edmType = TypeHelpers.GetEdmType<EntityType>(scalarOp.Type);
			PropertyRefList identityProperties = PropertyPushdownHelper.GetIdentityProperties(edmType);
			this.AddPropertyRefs(n.Child0, identityProperties);
			this.VisitNode(n.Child0);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002C618 File Offset: 0x0002A818
		public override void Visit(IsOfOp op, Node n)
		{
			PropertyRefList propertyRefList = new PropertyRefList();
			propertyRefList.Add(TypeIdPropertyRef.Instance);
			this.AddPropertyRefs(n.Child0, propertyRefList);
			this.VisitChildren(n);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002C64C File Offset: 0x0002A84C
		private void VisitPropertyOp(Op op, Node n, PropertyRef propertyRef)
		{
			PropertyRefList propertyRefList = new PropertyRefList();
			if (!TypeUtils.IsStructuredType(op.Type))
			{
				propertyRefList.Add(propertyRef);
			}
			else
			{
				PropertyRefList propertyRefList2 = this.GetPropertyRefList(n);
				if (propertyRefList2.AllProperties)
				{
					propertyRefList = propertyRefList2;
				}
				else
				{
					foreach (PropertyRef propertyRef2 in propertyRefList2.Properties)
					{
						propertyRefList.Add(propertyRef2.CreateNestedPropertyRef(propertyRef));
					}
				}
			}
			this.AddPropertyRefs(n.Child0, propertyRefList);
			this.VisitChildren(n);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
		public override void Visit(RelPropertyOp op, Node n)
		{
			this.VisitPropertyOp(op, n, new RelPropertyRef(op.PropertyInfo));
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002C6F9 File Offset: 0x0002A8F9
		public override void Visit(PropertyOp op, Node n)
		{
			this.VisitPropertyOp(op, n, new SimplePropertyRef(op.PropertyInfo));
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002C710 File Offset: 0x0002A910
		public override void Visit(TreatOp op, Node n)
		{
			PropertyRefList propertyRefList = this.GetPropertyRefList(n);
			PropertyRefList propertyRefList2 = propertyRefList.Clone();
			propertyRefList2.Add(TypeIdPropertyRef.Instance);
			this.AddPropertyRefs(n.Child0, propertyRefList2);
			this.VisitChildren(n);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002C74C File Offset: 0x0002A94C
		public override void Visit(VarRefOp op, Node n)
		{
			if (TypeUtils.IsStructuredType(op.Var.Type))
			{
				PropertyRefList propertyRefList = this.GetPropertyRefList(n);
				this.AddPropertyRefs(op.Var, propertyRefList);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002C780 File Offset: 0x0002A980
		public override void Visit(VarDefOp op, Node n)
		{
			if (TypeUtils.IsStructuredType(op.Var.Type))
			{
				PropertyRefList propertyRefList = this.GetPropertyRefList(op.Var);
				this.AddPropertyRefs(n.Child0, propertyRefList);
			}
			this.VisitChildren(n);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002C4FE File Offset: 0x0002A6FE
		public override void Visit(VarDefListOp op, Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002C7C0 File Offset: 0x0002A9C0
		protected override void VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.VisitNode(n.Child1);
			this.VisitNode(n.Child0);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002C7DC File Offset: 0x0002A9DC
		public override void Visit(DistinctOp op, Node n)
		{
			foreach (Var var in op.Keys)
			{
				if (TypeUtils.IsStructuredType(var.Type))
				{
					this.AddPropertyRefs(var, PropertyRefList.All);
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002C7C0 File Offset: 0x0002A9C0
		public override void Visit(FilterOp op, Node n)
		{
			this.VisitNode(n.Child1);
			this.VisitNode(n.Child0);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002C844 File Offset: 0x0002AA44
		protected override void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			foreach (Var var in op.Keys)
			{
				if (TypeUtils.IsStructuredType(var.Type))
				{
					this.AddPropertyRefs(var, PropertyRefList.All);
				}
			}
			this.VisitChildrenReverse(n);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002C8AC File Offset: 0x0002AAAC
		protected override void VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (n.Op.OpType == OpType.CrossJoin)
			{
				this.VisitChildren(n);
				return;
			}
			this.VisitNode(n.Child2);
			this.VisitNode(n.Child0);
			this.VisitNode(n.Child1);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002C7C0 File Offset: 0x0002A9C0
		public override void Visit(ProjectOp op, Node n)
		{
			this.VisitNode(n.Child1);
			this.VisitNode(n.Child0);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002C8E9 File Offset: 0x0002AAE9
		public override void Visit(ScanTableOp op, Node n)
		{
			PlanCompiler.Assert(!n.HasChild0, "scanTableOp with an input?");
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002C900 File Offset: 0x0002AB00
		public override void Visit(ScanViewOp op, Node n)
		{
			PlanCompiler.Assert(op.Table.Columns.Count == 1, "ScanViewOp with multiple columns?");
			Var v = op.Table.Columns[0];
			PropertyRefList propertyRefList = this.GetPropertyRefList(v);
			Var singletonVar = NominalTypeEliminator.GetSingletonVar(n.Child0);
			PlanCompiler.Assert(singletonVar != null, "cannot determine single Var from ScanViewOp's input");
			this.AddPropertyRefs(singletonVar, propertyRefList.Clone());
			this.VisitChildren(n);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002C974 File Offset: 0x0002AB74
		protected override void VisitSetOp(SetOp op, Node n)
		{
			foreach (VarMap varMap2 in op.VarMap)
			{
				foreach (KeyValuePair<Var, Var> keyValuePair in varMap2)
				{
					if (TypeUtils.IsStructuredType(keyValuePair.Key.Type))
					{
						PropertyRefList propertyRefList = this.GetPropertyRefList(keyValuePair.Key);
						if (op.OpType == OpType.Intersect || op.OpType == OpType.Except)
						{
							propertyRefList = PropertyRefList.All;
							this.AddPropertyRefs(keyValuePair.Key, propertyRefList);
						}
						else
						{
							propertyRefList = propertyRefList.Clone();
						}
						this.AddPropertyRefs(keyValuePair.Value, propertyRefList);
					}
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002CA48 File Offset: 0x0002AC48
		protected override void VisitSortOp(SortBaseOp op, Node n)
		{
			foreach (SortKey sortKey in op.Keys)
			{
				if (TypeUtils.IsStructuredType(sortKey.Var.Type))
				{
					this.AddPropertyRefs(sortKey.Var, PropertyRefList.All);
				}
			}
			if (n.HasChild1)
			{
				this.VisitNode(n.Child1);
			}
			this.VisitNode(n.Child0);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002C4FE File Offset: 0x0002A6FE
		public override void Visit(UnnestOp op, Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
		public override void Visit(PhysicalProjectOp op, Node n)
		{
			foreach (Var var in op.Outputs)
			{
				if (TypeUtils.IsStructuredType(var.Type))
				{
					this.AddPropertyRefs(var, PropertyRefList.All);
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00013A81 File Offset: 0x00011C81
		public override void Visit(MultiStreamNestOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00013A81 File Offset: 0x00011C81
		public override void Visit(SingleStreamNestOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x040007F4 RID: 2036
		private readonly Dictionary<Node, PropertyRefList> m_nodePropertyRefMap;

		// Token: 0x040007F5 RID: 2037
		private readonly Dictionary<Var, PropertyRefList> m_varPropertyRefMap;

		// Token: 0x040007F6 RID: 2038
		private readonly StructuredTypeInfo m_structuredTypeInfo;
	}
}
