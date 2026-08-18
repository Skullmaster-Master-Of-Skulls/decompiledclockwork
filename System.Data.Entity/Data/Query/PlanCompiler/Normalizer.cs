using System;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000090 RID: 144
	internal class Normalizer : SubqueryTrackingVisitor
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x000348C1 File Offset: 0x00032AC1
		private Normalizer(PlanCompiler planCompilerState) : base(planCompilerState)
		{
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000348CC File Offset: 0x00032ACC
		internal static void Process(PlanCompiler planCompilerState)
		{
			Normalizer normalizer = new Normalizer(planCompilerState);
			normalizer.Process();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000348E6 File Offset: 0x00032AE6
		private void Process()
		{
			base.m_command.Root = base.VisitNode(base.m_command.Root);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00034904 File Offset: 0x00032B04
		public override Node Visit(ExistsOp op, Node n)
		{
			this.VisitChildren(n);
			n.Child0 = this.BuildDummyProjectForExists(n.Child0);
			return n;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00034920 File Offset: 0x00032B20
		private Node BuildDummyProjectForExists(Node child)
		{
			Var var;
			return base.m_command.BuildProject(child, base.m_command.CreateNode(base.m_command.CreateInternalConstantOp(base.m_command.IntegerType, 1)), out var);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00034964 File Offset: 0x00032B64
		private Node BuildUnnest(Node collectionNode)
		{
			PlanCompiler.Assert(collectionNode.Op.IsScalarOp, "non-scalar usage of Unnest?");
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(collectionNode.Op.Type), "non-collection usage for Unnest?");
			Var v;
			Node arg = base.m_command.CreateVarDefNode(collectionNode, out v);
			UnnestOp op = base.m_command.CreateUnnestOp(v);
			return base.m_command.CreateNode(op, arg);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000349CC File Offset: 0x00032BCC
		private Node VisitCollectionFunction(FunctionOp op, Node n)
		{
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(op.Type), "non-TVF function?");
			Node node = this.BuildUnnest(n);
			UnnestOp unnestOp = node.Op as UnnestOp;
			PhysicalProjectOp op2 = base.m_command.CreatePhysicalProjectOp(unnestOp.Table.Columns[0]);
			Node arg = base.m_command.CreateNode(op2, node);
			CollectOp op3 = base.m_command.CreateCollectOp(n.Op.Type);
			return base.m_command.CreateNode(op3, arg);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00034A58 File Offset: 0x00032C58
		private Node VisitCollectionAggregateFunction(FunctionOp op, Node n)
		{
			TypeUsage typeUsage = null;
			Node child = n.Child0;
			if (OpType.SoftCast == child.Op.OpType)
			{
				typeUsage = TypeHelpers.GetEdmType<CollectionType>(child.Op.Type).TypeUsage;
				child = child.Child0;
				while (OpType.SoftCast == child.Op.OpType)
				{
					child = child.Child0;
				}
			}
			Node node = this.BuildUnnest(child);
			UnnestOp unnestOp = node.Op as UnnestOp;
			Var v = unnestOp.Table.Columns[0];
			AggregateOp op2 = base.m_command.CreateAggregateOp(op.Function, false);
			VarRefOp op3 = base.m_command.CreateVarRefOp(v);
			Node arg = base.m_command.CreateNode(op3);
			if (typeUsage != null)
			{
				arg = base.m_command.CreateNode(base.m_command.CreateSoftCastOp(typeUsage), arg);
			}
			Node definingExpr = base.m_command.CreateNode(op2, arg);
			VarVec gbyKeys = base.m_command.CreateVarVec();
			Node arg2 = base.m_command.CreateNode(base.m_command.CreateVarDefListOp());
			VarVec varVec = base.m_command.CreateVarVec();
			Var var;
			Node arg3 = base.m_command.CreateVarDefListNode(definingExpr, out var);
			varVec.Set(var);
			GroupByOp op4 = base.m_command.CreateGroupByOp(gbyKeys, varVec);
			Node subquery = base.m_command.CreateNode(op4, node, arg2, arg3);
			return base.AddSubqueryToParentRelOp(var, subquery);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00034BBC File Offset: 0x00032DBC
		public override Node Visit(FunctionOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Node node;
			if (TypeSemantics.IsCollectionType(op.Type))
			{
				node = this.VisitCollectionFunction(op, n);
			}
			else if (PlanCompilerUtil.IsCollectionAggregateFunction(op, n))
			{
				node = this.VisitCollectionAggregateFunction(op, n);
			}
			else
			{
				node = n;
			}
			PlanCompiler.Assert(node != null, "failure to construct a functionOp?");
			return node;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00034C11 File Offset: 0x00032E11
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (base.ProcessJoinOp(op, n))
			{
				n.Child2.Child0 = this.BuildDummyProjectForExists(n.Child2.Child0);
			}
			return n;
		}
	}
}
