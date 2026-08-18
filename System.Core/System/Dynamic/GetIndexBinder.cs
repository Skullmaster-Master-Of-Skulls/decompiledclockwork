using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000C8 RID: 200
	[__DynamicallyInvokable]
	public abstract class GetIndexBinder : DynamicMetaObjectBinder
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x000123C4 File Offset: 0x000105C4
		[__DynamicallyInvokable]
		protected GetIndexBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this._callInfo = callInfo;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x000123DE File Offset: 0x000105DE
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x000123EA File Offset: 0x000105EA
		[__DynamicallyInvokable]
		public CallInfo CallInfo
		{
			[__DynamicallyInvokable]
			get
			{
				return this._callInfo;
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000123F2 File Offset: 0x000105F2
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindGetIndex(this, args);
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00012412 File Offset: 0x00010612
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00012415 File Offset: 0x00010615
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackGetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes)
		{
			return this.FallbackGetIndex(target, indexes, null);
		}

		// Token: 0x060005FD RID: 1533
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackGetIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject errorSuggestion);

		// Token: 0x040005B4 RID: 1460
		private readonly CallInfo _callInfo;
	}
}
