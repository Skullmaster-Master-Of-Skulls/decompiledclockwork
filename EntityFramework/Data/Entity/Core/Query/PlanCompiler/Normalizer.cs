using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000688 RID: 1672
	internal class Normalizer : SubqueryTrackingVisitor
	{
		// Token: 0x060041F2 RID: 16882 RVA: 0x001371C5 File Offset: 0x001353C5
		private Normalizer(PlanCompiler planCompilerState) : base(planCompilerState)
		{
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x001371D0 File Offset: 0x001353D0
		internal static void Process(PlanCompiler planCompilerState)
		{
			Normalizer normalizer = new Normalizer(planCompilerState);
			normalizer.Process();
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x001371EA File Offset: 0x001353EA
		private void Process()
		{
			base.m_command.Root = base.VisitNode(base.m_command.Root);
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x00137208 File Offset: 0x00135408
		public override Node Visit(ExistsOp op, Node n)
		{
			this.VisitChildren(n);
			n.Child0 = this.BuildDummyProjectForExists(n.Child0);
			return n;
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x00137224 File Offset: 0x00135424
		private Node BuildDummyProjectForExists(Node child)
		{
			Var var;
			return base.m_command.BuildProject(child, base.m_command.CreateNode(base.m_command.CreateInternalConstantOp(base.m_command.IntegerType, 1)), out var);
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x00137268 File Offset: 0x00135468
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node BuildUnnest(Node collectionNode)
		{
			PlanCompiler.Assert(collectionNode.Op.IsScalarOp, "non-scalar usage of Un-nest?");
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(collectionNode.Op.Type), "non-collection usage for Un-nest?");
			Var v;
			Node arg = base.m_command.CreateVarDefNode(collectionNode, out v);
			UnnestOp op = base.m_command.CreateUnnestOp(v);
			return base.m_command.CreateNode(op, arg);
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x001372D0 File Offset: 0x001354D0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060041F9 RID: 16889 RVA: 0x0013735C File Offset: 0x0013555C
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

		// Token: 0x060041FA RID: 16890 RVA: 0x001374C0 File Offset: 0x001356C0
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "functionOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060041FB RID: 16891 RVA: 0x00137518 File Offset: 0x00135718
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (base.ProcessJoinOp(n))
			{
				n.Child2.Child0 = this.BuildDummyProjectForExists(n.Child2.Child0);
			}
			return n;
		}
	}
}
