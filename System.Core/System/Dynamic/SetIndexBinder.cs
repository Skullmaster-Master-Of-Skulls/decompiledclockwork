using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000CE RID: 206
	[__DynamicallyInvokable]
	public abstract class SetIndexBinder : DynamicMetaObjectBinder
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x00012574 File Offset: 0x00010774
		[__DynamicallyInvokable]
		protected SetIndexBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this._callInfo = callInfo;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001258E File Offset: 0x0001078E
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001259A File Offset: 0x0001079A
		[__DynamicallyInvokable]
		public CallInfo CallInfo
		{
			[__DynamicallyInvokable]
			get
			{
				return this._callInfo;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000125A4 File Offset: 0x000107A4
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length >= 2, "args");
			DynamicMetaObject value = args[args.Length - 1];
			DynamicMetaObject[] array = args.RemoveLast<DynamicMetaObject>();
			ContractUtils.RequiresNotNull(value, "args");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(array, "args");
			return target.BindSetIndex(this, array, value);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00012608 File Offset: 0x00010808
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001260B File Offset: 0x0001080B
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackSetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			return this.FallbackSetIndex(target, indexes, value, null);
		}

		// Token: 0x0600061F RID: 1567
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackSetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject value, DynamicMetaObject errorSuggestion);

		// Token: 0x040005BB RID: 1467
		private readonly CallInfo _callInfo;
	}
}
