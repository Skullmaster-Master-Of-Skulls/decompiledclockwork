using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000253 RID: 595
	[__DynamicallyInvokable]
	public sealed class MemberMemberBinding : MemberBinding
	{
		// Token: 0x060015AE RID: 5550 RVA: 0x00048A29 File Offset: 0x00046C29
		internal MemberMemberBinding(MemberInfo member, ReadOnlyCollection<MemberBinding> bindings) : base(MemberBindingType.MemberBinding, member)
		{
			this._bindings = bindings;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x00048A3A File Offset: 0x00046C3A
		[__DynamicallyInvokable]
		public ReadOnlyCollection<MemberBinding> Bindings
		{
			[__DynamicallyInvokable]
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00048A42 File Offset: 0x00046C42
		[__DynamicallyInvokable]
		public MemberMemberBinding Update(IEnumerable<MemberBinding> bindings)
		{
			if (bindings == this.Bindings)
			{
				return this;
			}
			return Expression.MemberBind(base.Member, bindings);
		}

		// Token: 0x04000A2A RID: 2602
		private ReadOnlyCollection<MemberBinding> _bindings;
	}
}
