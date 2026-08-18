using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000B6 RID: 182
	internal sealed class ParameterExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public ParameterExpressionFingerprint(ExpressionType nodeType, Type type, int parameterIndex) : base(nodeType, type)
		{
			this.ParameterIndex = parameterIndex;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000DDC1 File Offset: 0x0000BFC1
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000DDC9 File Offset: 0x0000BFC9
		public int ParameterIndex { get; private set; }

		// Token: 0x060004EB RID: 1259 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		public override bool Equals(object obj)
		{
			ParameterExpressionFingerprint parameterExpressionFingerprint = obj as ParameterExpressionFingerprint;
			return parameterExpressionFingerprint != null && this.ParameterIndex == parameterExpressionFingerprint.ParameterIndex && base.Equals(parameterExpressionFingerprint);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000DE02 File Offset: 0x0000C002
		internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
		{
			combiner.AddInt32(this.ParameterIndex);
			base.AddToHashCodeCombiner(combiner);
		}
	}
}
