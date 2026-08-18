using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000C9 RID: 201
	[__DynamicallyInvokable]
	public abstract class GetMemberBinder : DynamicMetaObjectBinder
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00012420 File Offset: 0x00010620
		[__DynamicallyInvokable]
		protected GetMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this._name = name;
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00012441 File Offset: 0x00010641
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001244D File Offset: 0x0001064D
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00012455 File Offset: 0x00010655
		[__DynamicallyInvokable]
		public bool IgnoreCase
		{
			[__DynamicallyInvokable]
			get
			{
				return this._ignoreCase;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001245D File Offset: 0x0001065D
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackGetMember(DynamicMetaObject target)
		{
			return this.FallbackGetMember(target, null);
		}

		// Token: 0x06000603 RID: 1539
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		// Token: 0x06000604 RID: 1540 RVA: 0x00012467 File Offset: 0x00010667
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindGetMember(this);
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00012490 File Offset: 0x00010690
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040005B5 RID: 1461
		private readonly string _name;

		// Token: 0x040005B6 RID: 1462
		private readonly bool _ignoreCase;
	}
}
