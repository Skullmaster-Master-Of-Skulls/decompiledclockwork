using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000CC RID: 204
	[__DynamicallyInvokable]
	public abstract class InvokeBinder : DynamicMetaObjectBinder
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00012493 File Offset: 0x00010693
		[__DynamicallyInvokable]
		protected InvokeBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this._callInfo = callInfo;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x000124AD File Offset: 0x000106AD
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x000124B9 File Offset: 0x000106B9
		[__DynamicallyInvokable]
		public CallInfo CallInfo
		{
			[__DynamicallyInvokable]
			get
			{
				return this._callInfo;
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000124C1 File Offset: 0x000106C1
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackInvoke(target, args, null);
		}

		// Token: 0x0600060C RID: 1548
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackInvoke(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		// Token: 0x0600060D RID: 1549 RVA: 0x000124CC File Offset: 0x000106CC
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindInvoke(this, args);
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000124EC File Offset: 0x000106EC
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040005B7 RID: 1463
		private readonly CallInfo _callInfo;
	}
}
