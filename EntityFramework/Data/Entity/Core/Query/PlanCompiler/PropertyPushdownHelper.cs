using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000695 RID: 1685
	internal class PropertyPushdownHelper : BasicOpVisitor
	{
		// Token: 0x060042A4 RID: 17060 RVA: 0x0013BEB2 File Offset: 0x0013A0B2
		private PropertyPushdownHelper()
		{
			this.m_varPropertyRefMap = new Dictionary<Var, PropertyRefList>();
			this.m_nodePropertyRefMap = new Dictionary<Node, PropertyRefList>();
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x0013BED0 File Offset: 0x0013A0D0
		internal static void Process(Command itree, out Dictionary<Var, PropertyRefList> varPropertyRefs, out Dictionary<Node, PropertyRefList> nodePropertyRefs)
		{
			PropertyPushdownHelper propertyPushdownHelper = new PropertyPushdownHelper();
			propertyPushdownHelper.Process(itree.Root);
			varPropertyRefs = propertyPushdownHelper.m_varPropertyRefMap;
			nodePropertyRefs = propertyPushdownHelper.m_nodePropertyRefMap;
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x0013BEFF File Offset: 0x0013A0FF
		private void Process(Node rootNode)
		{
			rootNode.Op.Accept(this, rootNode);
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x0013BF10 File Offset: 0x0013A110
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

		// Token: 0x060042A8 RID: 17064 RVA: 0x0013BF44 File Offset: 0x0013A144
		private void AddPropertyRefs(Node node, PropertyRefList propertyRefs)
		{
			PropertyRefList propertyRefList = this.GetPropertyRefList(node);
			propertyRefList.Append(propertyRefs);
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x0013BF60 File Offset: 0x0013A160
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

		// Token: 0x060042AA RID: 17066 RVA: 0x0013BF94 File Offset: 0x0013A194
		private void AddPropertyRefs(Var v, PropertyRefList propertyRefs)
		{
			PropertyRefList propertyRefList = this.GetPropertyRefList(v);
			propertyRefList.Append(propertyRefs);
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x0013BFB0 File Offset: 0x0013A1B0
		private static PropertyRefList GetIdentityProperties(EntityType type)
		{
			PropertyRefList keyProperties = PropertyPushdownHelper.GetKeyProperties(type);
			keyProperties.Add(EntitySetIdPropertyRef.Instance);
			return keyProperties;
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x0013BFD0 File Offset: 0x0013A1D0
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-EdmProperty")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EntityType")]
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

		// Token: 0x060042AD RID: 17069 RVA: 0x0013C04C File Offset: 0x0013A24C
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

		// Token: 0x060042AE RID: 17070 RVA: 0x0013C0C8 File Offset: 0x0013A2C8
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

		// Token: 0x060042AF RID: 17071 RVA: 0x0013C13C File Offset: 0x0013A33C
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

		// Token: 0x060042B0 RID: 17072 RVA: 0x0013C1AE File Offset: 0x0013A3AE
		public override void Visit(CollectOp op, Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x0013C1B8 File Offset: 0x0013A3B8
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "childOpType")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060042B2 RID: 17074 RVA: 0x0013C270 File Offset: 0x0013A470
		public override void Visit(ElementOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x0013C278 File Offset: 0x0013A478
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScalarOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetEntityRefOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override void Visit(GetEntityRefOp op, Node n)
		{
			ScalarOp scalarOp = n.Child0.Op as ScalarOp;
			PlanCompiler.Assert(scalarOp != null, "input to GetEntityRefOp is not a ScalarOp?");
			EntityType edmType = TypeHelpers.GetEdmType<EntityType>(scalarOp.Type);
			PropertyRefList identityProperties = PropertyPushdownHelper.GetIdentityProperties(edmType);
			this.AddPropertyRefs(n.Child0, identityProperties);
			this.VisitNode(n.Child0);
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x0013C2D4 File Offset: 0x0013A4D4
		public override void Visit(IsOfOp op, Node n)
		{
			PropertyRefList propertyRefList = new PropertyRefList();
			propertyRefList.Add(TypeIdPropertyRef.Instance);
			this.AddPropertyRefs(n.Child0, propertyRefList);
			this.VisitChildren(n);
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x0013C308 File Offset: 0x0013A508
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

		// Token: 0x060042B6 RID: 17078 RVA: 0x0013C3A0 File Offset: 0x0013A5A0
		public override void Visit(RelPropertyOp op, Node n)
		{
			this.VisitPropertyOp(op, n, new RelPropertyRef(op.PropertyInfo));
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x0013C3B5 File Offset: 0x0013A5B5
		public override void Visit(PropertyOp op, Node n)
		{
			this.VisitPropertyOp(op, n, new SimplePropertyRef(op.PropertyInfo));
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0013C3CC File Offset: 0x0013A5CC
		public override void Visit(TreatOp op, Node n)
		{
			PropertyRefList propertyRefList = this.GetPropertyRefList(n);
			PropertyRefList propertyRefList2 = propertyRefList.Clone();
			propertyRefList2.Add(TypeIdPropertyRef.Instance);
			this.AddPropertyRefs(n.Child0, propertyRefList2);
			this.VisitChildren(n);
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x0013C408 File Offset: 0x0013A608
		public override void Visit(VarRefOp op, Node n)
		{
			if (TypeUtils.IsStructuredType(op.Var.Type))
			{
				PropertyRefList propertyRefList = this.GetPropertyRefList(n);
				this.AddPropertyRefs(op.Var, propertyRefList);
			}
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x0013C43C File Offset: 0x0013A63C
		public override void Visit(VarDefOp op, Node n)
		{
			if (TypeUtils.IsStructuredType(op.Var.Type))
			{
				PropertyRefList propertyRefList = this.GetPropertyRefList(op.Var);
				this.AddPropertyRefs(n.Child0, propertyRefList);
			}
			this.VisitChildren(n);
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x0013C47C File Offset: 0x0013A67C
		public override void Visit(VarDefListOp op, Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x0013C485 File Offset: 0x0013A685
		protected override void VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.VisitNode(n.Child1);
			this.VisitNode(n.Child0);
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x0013C4A0 File Offset: 0x0013A6A0
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

		// Token: 0x060042BE RID: 17086 RVA: 0x0013C508 File Offset: 0x0013A708
		public override void Visit(FilterOp op, Node n)
		{
			this.VisitNode(n.Child1);
			this.VisitNode(n.Child0);
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x0013C524 File Offset: 0x0013A724
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

		// Token: 0x060042C0 RID: 17088 RVA: 0x0013C58C File Offset: 0x0013A78C
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

		// Token: 0x060042C1 RID: 17089 RVA: 0x0013C5C9 File Offset: 0x0013A7C9
		public override void Visit(ProjectOp op, Node n)
		{
			this.VisitNode(n.Child1);
			this.VisitNode(n.Child0);
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x0013C5E3 File Offset: 0x0013A7E3
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "scanTableOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override void Visit(ScanTableOp op, Node n)
		{
			PlanCompiler.Assert(!n.HasChild0, "scanTableOp with an input?");
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x0013C5F8 File Offset: 0x0013A7F8
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScanViewOp's")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScanViewOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060042C4 RID: 17092 RVA: 0x0013C670 File Offset: 0x0013A870
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

		// Token: 0x060042C5 RID: 17093 RVA: 0x0013C744 File Offset: 0x0013A944
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

		// Token: 0x060042C6 RID: 17094 RVA: 0x0013C7D4 File Offset: 0x0013A9D4
		public override void Visit(UnnestOp op, Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x0013C7E0 File Offset: 0x0013A9E0
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

		// Token: 0x060042C8 RID: 17096 RVA: 0x0013C84C File Offset: 0x0013AA4C
		public override void Visit(MultiStreamNestOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x0013C853 File Offset: 0x0013AA53
		public override void Visit(SingleStreamNestOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040018AD RID: 6317
		private readonly Dictionary<Node, PropertyRefList> m_nodePropertyRefMap;

		// Token: 0x040018AE RID: 6318
		private readonly Dictionary<Var, PropertyRefList> m_varPropertyRefMap;
	}
}
