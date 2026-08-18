using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000C2 RID: 194
	[__DynamicallyInvokable]
	public abstract class DeleteMemberBinder : DynamicMetaObjectBinder
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x000113CC File Offset: 0x0000F5CC
		[__DynamicallyInvokable]
		protected DeleteMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this._name = name;
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x000113ED File Offset: 0x0000F5ED
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000113F5 File Offset: 0x0000F5F5
		[__DynamicallyInvokable]
		public bool IgnoreCase
		{
			[__DynamicallyInvokable]
			get
			{
				return this._ignoreCase;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x000113FD File Offset: 0x0000F5FD
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(void);
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00011409 File Offset: 0x0000F609
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackDeleteMember(DynamicMetaObject target)
		{
			return this.FallbackDeleteMember(target, null);
		}

		// Token: 0x06000596 RID: 1430
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackDeleteMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		// Token: 0x06000597 RID: 1431 RVA: 0x00011413 File Offset: 0x0000F613
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0);
			return target.BindDeleteMember(this);
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00011437 File Offset: 0x0000F637
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040005A0 RID: 1440
		private readonly string _name;

		// Token: 0x040005A1 RID: 1441
		private readonly bool _ignoreCase;
	}
}
