using System;
using System.Linq.Expressions;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BB7 RID: 7095
	internal abstract class MemberAccessExpressionBuilderBase : ExpressionBuilderBase
	{
		// Token: 0x06011271 RID: 70257 RVA: 0x003C855B File Offset: 0x003C675B
		protected MemberAccessExpressionBuilderBase(Type itemType, string memberName) : base(itemType)
		{
			this.memberName = memberName;
		}

		// Token: 0x170053AC RID: 21420
		// (get) Token: 0x06011272 RID: 70258 RVA: 0x003C856B File Offset: 0x003C676B
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x06011273 RID: 70259 RVA: 0x003C8573 File Offset: 0x003C6773
		public Expression CreateMemberAccessExpression()
		{
			if (string.IsNullOrEmpty(this.MemberName))
			{
				return base.ParameterExpression;
			}
			return this.CreateMemberAccessExpressionOverride();
		}

		// Token: 0x06011274 RID: 70260
		protected abstract Expression CreateMemberAccessExpressionOverride();

		// Token: 0x06011275 RID: 70261 RVA: 0x003C8590 File Offset: 0x003C6790
		internal LambdaExpression CreateLambdaExpression()
		{
			Expression body = this.CreateMemberAccessExpression();
			return Expression.Lambda(body, new ParameterExpression[]
			{
				base.ParameterExpression
			});
		}

		// Token: 0x04004CC5 RID: 19653
		private readonly string memberName;
	}
}
