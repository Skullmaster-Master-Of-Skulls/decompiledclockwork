using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000AE RID: 174
	internal sealed class FingerprintingExpressionVisitor : ExpressionVisitor
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x0000D69B File Offset: 0x0000B89B
		private FingerprintingExpressionVisitor()
		{
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		private T GiveUp<T>(T node)
		{
			this._gaveUp = true;
			return node;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		public static ExpressionFingerprintChain GetFingerprintChain(Expression expr, out List<object> capturedConstants)
		{
			FingerprintingExpressionVisitor fingerprintingExpressionVisitor = new FingerprintingExpressionVisitor();
			fingerprintingExpressionVisitor.Visit(expr);
			if (fingerprintingExpressionVisitor._gaveUp)
			{
				capturedConstants = null;
				return null;
			}
			capturedConstants = fingerprintingExpressionVisitor._seenConstants;
			return fingerprintingExpressionVisitor._currentChain;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000D706 File Offset: 0x0000B906
		public override Expression Visit(Expression node)
		{
			if (node == null)
			{
				this._currentChain.Elements.Add(null);
				return null;
			}
			return base.Visit(node);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000D725 File Offset: 0x0000B925
		protected override Expression VisitBinary(BinaryExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._currentChain.Elements.Add(new BinaryExpressionFingerprint(node.NodeType, node.Type, node.Method));
			return base.VisitBinary(node);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000D75F File Offset: 0x0000B95F
		protected override Expression VisitBlock(BlockExpression node)
		{
			return this.GiveUp<BlockExpression>(node);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000D768 File Offset: 0x0000B968
		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			return this.GiveUp<CatchBlock>(node);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000D771 File Offset: 0x0000B971
		protected override Expression VisitConditional(ConditionalExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._currentChain.Elements.Add(new ConditionalExpressionFingerprint(node.NodeType, node.Type));
			return base.VisitConditional(node);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000D7A8 File Offset: 0x0000B9A8
		protected override Expression VisitConstant(ConstantExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._seenConstants.Add(node.Value);
			this._currentChain.Elements.Add(new ConstantExpressionFingerprint(node.NodeType, node.Type));
			return base.VisitConstant(node);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		protected override Expression VisitDebugInfo(DebugInfoExpression node)
		{
			return this.GiveUp<DebugInfoExpression>(node);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000D801 File Offset: 0x0000BA01
		protected override Expression VisitDefault(DefaultExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._currentChain.Elements.Add(new DefaultExpressionFingerprint(node.NodeType, node.Type));
			return base.VisitDefault(node);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000D835 File Offset: 0x0000BA35
		protected override Expression VisitDynamic(DynamicExpression node)
		{
			return this.GiveUp<DynamicExpression>(node);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000D83E File Offset: 0x0000BA3E
		protected override ElementInit VisitElementInit(ElementInit node)
		{
			return this.GiveUp<ElementInit>(node);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000D847 File Offset: 0x0000BA47
		protected override Expression VisitExtension(Expression node)
		{
			return this.GiveUp<Expression>(node);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000D850 File Offset: 0x0000BA50
		protected override Expression VisitGoto(GotoExpression node)
		{
			return this.GiveUp<GotoExpression>(node);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000D859 File Offset: 0x0000BA59
		protected override Expression VisitIndex(IndexExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._currentChain.Elements.Add(new IndexExpressionFingerprint(node.NodeType, node.Type, node.Indexer));
			return base.VisitIndex(node);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000D893 File Offset: 0x0000BA93
		protected override Expression VisitInvocation(InvocationExpression node)
		{
			return this.GiveUp<InvocationExpression>(node);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000D89C File Offset: 0x0000BA9C
		protected override Expression VisitLabel(LabelExpression node)
		{
			return this.GiveUp<LabelExpression>(node);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000D8A5 File Offset: 0x0000BAA5
		protected override LabelTarget VisitLabelTarget(LabelTarget node)
		{
			return this.GiveUp<LabelTarget>(node);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._currentChain.Elements.Add(new LambdaExpressionFingerprint(node.NodeType, node.Type));
			return base.VisitLambda<T>(node);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000D8E2 File Offset: 0x0000BAE2
		protected override Expression VisitListInit(ListInitExpression node)
		{
			return this.GiveUp<ListInitExpression>(node);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000D8EB File Offset: 0x0000BAEB
		protected override Expression VisitLoop(LoopExpression node)
		{
			return this.GiveUp<LoopExpression>(node);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
		protected override Expression VisitMember(MemberExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._currentChain.Elements.Add(new MemberExpressionFingerprint(node.NodeType, node.Type, node.Member));
			return base.VisitMember(node);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000D92E File Offset: 0x0000BB2E
		protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
		{
			return this.GiveUp<MemberAssignment>(node);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000D937 File Offset: 0x0000BB37
		protected override MemberBinding VisitMemberBinding(MemberBinding node)
		{
			return this.GiveUp<MemberBinding>(node);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000D940 File Offset: 0x0000BB40
		protected override Expression VisitMemberInit(MemberInitExpression node)
		{
			return this.GiveUp<MemberInitExpression>(node);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000D949 File Offset: 0x0000BB49
		protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
		{
			return this.GiveUp<MemberListBinding>(node);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000D952 File Offset: 0x0000BB52
		protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
		{
			return this.GiveUp<MemberMemberBinding>(node);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000D95B File Offset: 0x0000BB5B
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._currentChain.Elements.Add(new MethodCallExpressionFingerprint(node.NodeType, node.Type, node.Method));
			return base.VisitMethodCall(node);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000D995 File Offset: 0x0000BB95
		protected override Expression VisitNew(NewExpression node)
		{
			return this.GiveUp<NewExpression>(node);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000D99E File Offset: 0x0000BB9E
		protected override Expression VisitNewArray(NewArrayExpression node)
		{
			return this.GiveUp<NewArrayExpression>(node);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
		protected override Expression VisitParameter(ParameterExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			int num = this._seenParameters.IndexOf(node);
			if (num < 0)
			{
				num = this._seenParameters.Count;
				this._seenParameters.Add(node);
			}
			this._currentChain.Elements.Add(new ParameterExpressionFingerprint(node.NodeType, node.Type, num));
			return base.VisitParameter(node);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000DA11 File Offset: 0x0000BC11
		protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			return this.GiveUp<RuntimeVariablesExpression>(node);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000DA1A File Offset: 0x0000BC1A
		protected override Expression VisitSwitch(SwitchExpression node)
		{
			return this.GiveUp<SwitchExpression>(node);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000DA23 File Offset: 0x0000BC23
		protected override SwitchCase VisitSwitchCase(SwitchCase node)
		{
			return this.GiveUp<SwitchCase>(node);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		protected override Expression VisitTry(TryExpression node)
		{
			return this.GiveUp<TryExpression>(node);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000DA35 File Offset: 0x0000BC35
		protected override Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._currentChain.Elements.Add(new TypeBinaryExpressionFingerprint(node.NodeType, node.Type, node.TypeOperand));
			return base.VisitTypeBinary(node);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000DA6F File Offset: 0x0000BC6F
		protected override Expression VisitUnary(UnaryExpression node)
		{
			if (this._gaveUp)
			{
				return node;
			}
			this._currentChain.Elements.Add(new UnaryExpressionFingerprint(node.NodeType, node.Type, node.Method));
			return base.VisitUnary(node);
		}

		// Token: 0x0400014B RID: 331
		private readonly List<object> _seenConstants = new List<object>();

		// Token: 0x0400014C RID: 332
		private readonly List<ParameterExpression> _seenParameters = new List<ParameterExpression>();

		// Token: 0x0400014D RID: 333
		private readonly ExpressionFingerprintChain _currentChain = new ExpressionFingerprintChain();

		// Token: 0x0400014E RID: 334
		private bool _gaveUp;
	}
}
