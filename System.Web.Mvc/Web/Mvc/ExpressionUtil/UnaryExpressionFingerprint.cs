using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000B8 RID: 184
	internal sealed class UnaryExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000DE84 File Offset: 0x0000C084
		public UnaryExpressionFingerprint(ExpressionType nodeType, Type type, MethodInfo method) : base(nodeType, type)
		{
			this.Method = method;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000DE95 File Offset: 0x0000C095
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0000DE9D File Offset: 0x0000C09D
		public MethodInfo Method { get; private set; }

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		public override bool Equals(object obj)
		{
			UnaryExpressionFingerprint unaryExpressionFingerprint = obj as UnaryExpressionFingerprint;
			return unaryExpressionFingerprint != null && object.Equals(this.Method, unaryExpressionFingerprint.Method) && base.Equals(unaryExpressionFingerprint);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000DEDB File Offset: 0x0000C0DB
		internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
		{
			combiner.AddObject(this.Method);
			base.AddToHashCodeCombiner(combiner);
		}
	}
}
