using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000C0 RID: 192
	[__DynamicallyInvokable]
	public abstract class CreateInstanceBinder : DynamicMetaObjectBinder
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x00011314 File Offset: 0x0000F514
		[__DynamicallyInvokable]
		protected CreateInstanceBinder(CallInfo callInfo)
		{
			ContractUtils.RequiresNotNull(callInfo, "callInfo");
			this._callInfo = callInfo;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001132E File Offset: 0x0000F52E
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001133A File Offset: 0x0000F53A
		[__DynamicallyInvokable]
		public CallInfo CallInfo
		{
			[__DynamicallyInvokable]
			get
			{
				return this._callInfo;
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00011342 File Offset: 0x0000F542
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackCreateInstance(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			return this.FallbackCreateInstance(target, args, null);
		}

		// Token: 0x06000587 RID: 1415
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackCreateInstance(DynamicMetaObject target, DynamicMetaObject[] args, DynamicMetaObject errorSuggestion);

		// Token: 0x06000588 RID: 1416 RVA: 0x0001134D File Offset: 0x0000F54D
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNullItems<DynamicMetaObject>(args, "args");
			return target.BindCreateInstance(this, args);
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001136D File Offset: 0x0000F56D
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400059E RID: 1438
		private readonly CallInfo _callInfo;
	}
}
