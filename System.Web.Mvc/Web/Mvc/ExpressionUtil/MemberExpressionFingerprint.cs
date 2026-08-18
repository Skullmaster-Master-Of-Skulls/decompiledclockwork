using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000B4 RID: 180
	internal sealed class MemberExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		public MemberExpressionFingerprint(ExpressionType nodeType, Type type, MemberInfo member) : base(nodeType, type)
		{
			this.Member = member;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000DCE9 File Offset: 0x0000BEE9
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000DCF1 File Offset: 0x0000BEF1
		public MemberInfo Member { get; private set; }

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		public override bool Equals(object obj)
		{
			MemberExpressionFingerprint memberExpressionFingerprint = obj as MemberExpressionFingerprint;
			return memberExpressionFingerprint != null && object.Equals(this.Member, memberExpressionFingerprint.Member) && base.Equals(memberExpressionFingerprint);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000DD2F File Offset: 0x0000BF2F
		internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
		{
			combiner.AddObject(this.Member);
			base.AddToHashCodeCombiner(combiner);
		}
	}
}
