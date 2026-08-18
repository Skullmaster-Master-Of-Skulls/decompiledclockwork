using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000B5 RID: 181
	internal sealed class MethodCallExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public MethodCallExpressionFingerprint(ExpressionType nodeType, Type type, MethodInfo method) : base(nodeType, type)
		{
			this.Method = method;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0000DD55 File Offset: 0x0000BF55
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0000DD5D File Offset: 0x0000BF5D
		public MethodInfo Method { get; private set; }

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000DD68 File Offset: 0x0000BF68
		public override bool Equals(object obj)
		{
			MethodCallExpressionFingerprint methodCallExpressionFingerprint = obj as MethodCallExpressionFingerprint;
			return methodCallExpressionFingerprint != null && object.Equals(this.Method, methodCallExpressionFingerprint.Method) && base.Equals(methodCallExpressionFingerprint);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000DD9B File Offset: 0x0000BF9B
		internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
		{
			combiner.AddObject(this.Method);
			base.AddToHashCodeCombiner(combiner);
		}
	}
}
