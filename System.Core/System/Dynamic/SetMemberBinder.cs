using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000CF RID: 207
	[__DynamicallyInvokable]
	public abstract class SetMemberBinder : DynamicMetaObjectBinder
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x00012617 File Offset: 0x00010817
		[__DynamicallyInvokable]
		protected SetMemberBinder(string name, bool ignoreCase)
		{
			ContractUtils.RequiresNotNull(name, "name");
			this._name = name;
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00012638 File Offset: 0x00010838
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00012644 File Offset: 0x00010844
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001264C File Offset: 0x0001084C
		[__DynamicallyInvokable]
		public bool IgnoreCase
		{
			[__DynamicallyInvokable]
			get
			{
				return this._ignoreCase;
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00012654 File Offset: 0x00010854
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.RequiresNotNull(args, "args");
			ContractUtils.Requires(args.Length == 1, "args");
			DynamicMetaObject value = args[0];
			ContractUtils.RequiresNotNull(value, "args");
			return target.BindSetMember(this, value);
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001269E File Offset: 0x0001089E
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000126A1 File Offset: 0x000108A1
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value)
		{
			return this.FallbackSetMember(target, value, null);
		}

		// Token: 0x06000627 RID: 1575
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion);

		// Token: 0x040005BC RID: 1468
		private readonly string _name;

		// Token: 0x040005BD RID: 1469
		private readonly bool _ignoreCase;
	}
}
