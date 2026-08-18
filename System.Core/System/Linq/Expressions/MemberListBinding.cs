using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000252 RID: 594
	[__DynamicallyInvokable]
	public sealed class MemberListBinding : MemberBinding
	{
		// Token: 0x060015AB RID: 5547 RVA: 0x000489F7 File Offset: 0x00046BF7
		internal MemberListBinding(MemberInfo member, ReadOnlyCollection<ElementInit> initializers) : base(MemberBindingType.ListBinding, member)
		{
			this._initializers = initializers;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00048A08 File Offset: 0x00046C08
		[__DynamicallyInvokable]
		public ReadOnlyCollection<ElementInit> Initializers
		{
			[__DynamicallyInvokable]
			get
			{
				return this._initializers;
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00048A10 File Offset: 0x00046C10
		[__DynamicallyInvokable]
		public MemberListBinding Update(IEnumerable<ElementInit> initializers)
		{
			if (initializers == this.Initializers)
			{
				return this;
			}
			return Expression.ListBind(base.Member, initializers);
		}

		// Token: 0x04000A29 RID: 2601
		private ReadOnlyCollection<ElementInit> _initializers;
	}
}
