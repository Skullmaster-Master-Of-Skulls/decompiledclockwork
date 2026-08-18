using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200024D RID: 589
	[__DynamicallyInvokable]
	public abstract class MemberBinding
	{
		// Token: 0x0600158D RID: 5517 RVA: 0x00048725 File Offset: 0x00046925
		[Obsolete("Do not use this constructor. It will be removed in future releases.")]
		protected MemberBinding(MemberBindingType type, MemberInfo member)
		{
			this._type = type;
			this._member = member;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0004873B File Offset: 0x0004693B
		[__DynamicallyInvokable]
		public MemberBindingType BindingType
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00048743 File Offset: 0x00046943
		[__DynamicallyInvokable]
		public MemberInfo Member
		{
			[__DynamicallyInvokable]
			get
			{
				return this._member;
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0004874B File Offset: 0x0004694B
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return ExpressionStringBuilder.MemberBindingToString(this);
		}

		// Token: 0x04000A22 RID: 2594
		private MemberBindingType _type;

		// Token: 0x04000A23 RID: 2595
		private MemberInfo _member;
	}
}
