using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000B7 RID: 183
	internal sealed class TypeBinaryExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0000DE17 File Offset: 0x0000C017
		public TypeBinaryExpressionFingerprint(ExpressionType nodeType, Type type, Type typeOperand) : base(nodeType, type)
		{
			this.TypeOperand = typeOperand;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000DE28 File Offset: 0x0000C028
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x0000DE30 File Offset: 0x0000C030
		public Type TypeOperand { get; private set; }

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000DE3C File Offset: 0x0000C03C
		public override bool Equals(object obj)
		{
			TypeBinaryExpressionFingerprint typeBinaryExpressionFingerprint = obj as TypeBinaryExpressionFingerprint;
			return typeBinaryExpressionFingerprint != null && object.Equals(this.TypeOperand, typeBinaryExpressionFingerprint.TypeOperand) && base.Equals(typeBinaryExpressionFingerprint);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000DE6F File Offset: 0x0000C06F
		internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
		{
			combiner.AddObject(this.TypeOperand);
			base.AddToHashCodeCombiner(combiner);
		}
	}
}
