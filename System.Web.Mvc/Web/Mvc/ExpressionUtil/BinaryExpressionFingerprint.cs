using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000A7 RID: 167
	internal sealed class BinaryExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		public BinaryExpressionFingerprint(ExpressionType nodeType, Type type, MethodInfo method) : base(nodeType, type)
		{
			this.Method = method;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000D1D5 File Offset: 0x0000B3D5
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0000D1DD File Offset: 0x0000B3DD
		public MethodInfo Method { get; private set; }

		// Token: 0x0600048F RID: 1167 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		public override bool Equals(object obj)
		{
			BinaryExpressionFingerprint binaryExpressionFingerprint = obj as BinaryExpressionFingerprint;
			return binaryExpressionFingerprint != null && object.Equals(this.Method, binaryExpressionFingerprint.Method) && base.Equals(binaryExpressionFingerprint);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000D21B File Offset: 0x0000B41B
		internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
		{
			combiner.AddObject(this.Method);
			base.AddToHashCodeCombiner(combiner);
		}
	}
}
