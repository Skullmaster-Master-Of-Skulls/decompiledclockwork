using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000CD RID: 205
	[__DynamicallyInvokable]
	public abstract class InvokeMemberBinder : DynamicMetaObjectBinder
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x000124EF File Offset: 0x000106EF
		[__DynamicallyInvokable]
		protected InvokeMemberBinder(string name, bool ignoreCase, CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(name, "name");
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this._name = name;
			this._ignoreCase = ignoreCase;
			this._callInfo = callInfo;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00012522 File Offset: 0x00010722
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001252E File Offset: 0x0001072E
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00012536 File Offset: 0x00010736
		[__DynamicallyInvokable]
		public bool IgnoreCase
		{
			[__DynamicallyInvokable]
			get
			{
				return this._ignoreCase;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001253E File Offset: 0x0001073E
		[__DynamicallyInvokable]
		public CallInfo CallInfo
		{
			[__DynamicallyInvokable]
			get
			{
				return this._callInfo;
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00012546 File Offset: 0x00010746
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindInvokeMember(this, args);
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00012566 File Offset: 0x00010766
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00012569 File Offset: 0x00010769
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackInvokeMember(target, args, null);
		}

		// Token: 0x06000617 RID: 1559
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackInvokeMember(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		// Token: 0x06000618 RID: 1560
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		// Token: 0x040005B8 RID: 1464
		private readonly string _name;

		// Token: 0x040005B9 RID: 1465
		private readonly bool _ignoreCase;

		// Token: 0x040005BA RID: 1466
		private readonly CallInfo _callInfo;
	}
}
