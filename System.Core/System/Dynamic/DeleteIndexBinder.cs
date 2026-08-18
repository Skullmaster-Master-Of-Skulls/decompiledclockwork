using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000C1 RID: 193
	[__DynamicallyInvokable]
	public abstract class DeleteIndexBinder : DynamicMetaObjectBinder
	{
		// Token: 0x0600058A RID: 1418 RVA: 0x00011370 File Offset: 0x0000F570
		[__DynamicallyInvokable]
		protected DeleteIndexBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this._callInfo = callInfo;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001138A File Offset: 0x0000F58A
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(void);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00011396 File Offset: 0x0000F596
		[__DynamicallyInvokable]
		public CallInfo CallInfo
		{
			[__DynamicallyInvokable]
			get
			{
				return this._callInfo;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001139E File Offset: 0x0000F59E
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindDeleteIndex(this, args);
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000113BE File Offset: 0x0000F5BE
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000113C1 File Offset: 0x0000F5C1
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackDeleteIndex(DynamicMetaObject target, DynamicMetaObject[] indexes)
		{
			return this.FallbackDeleteIndex(target, indexes, null);
		}

		// Token: 0x06000590 RID: 1424
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackDeleteIndex(DynamicMetaObject target, DynamicMetaObject[] indexes, DynamicMetaObject errorSuggestion);

		// Token: 0x0400059F RID: 1439
		private readonly CallInfo _callInfo;
	}
}
