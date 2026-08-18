using System;
using System.Dynamic.Utils;

namespace System.Dynamic
{
	// Token: 0x020000BF RID: 191
	[__DynamicallyInvokable]
	public abstract class ConvertBinder : DynamicMetaObjectBinder
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x000112A5 File Offset: 0x0000F4A5
		[__DynamicallyInvokable]
		protected ConvertBinder(Type type, bool @explicit)
		{
			ContractUtils.RequiresNotNull(type, "type");
			this._type = type;
			this._explicit = @explicit;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000112C6 File Offset: 0x0000F4C6
		[__DynamicallyInvokable]
		public Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000112CE File Offset: 0x0000F4CE
		[__DynamicallyInvokable]
		public bool Explicit
		{
			[__DynamicallyInvokable]
			get
			{
				return this._explicit;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000112D6 File Offset: 0x0000F4D6
		[__DynamicallyInvokable]
		public DynamicMetaObject FallbackConvert(DynamicMetaObject target)
		{
			return this.FallbackConvert(target, null);
		}

		// Token: 0x0600057F RID: 1407
		[__DynamicallyInvokable]
		public abstract DynamicMetaObject FallbackConvert(DynamicMetaObject target, DynamicMetaObject errorSuggestion);

		// Token: 0x06000580 RID: 1408 RVA: 0x000112E0 File Offset: 0x0000F4E0
		[__DynamicallyInvokable]
		public sealed override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
		{
			ContractUtils.RequiresNotNull(target, "target");
			ContractUtils.Requires(args == null || args.Length == 0, "args");
			return target.BindConvert(this);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00011309 File Offset: 0x0000F509
		internal sealed override bool IsStandardBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001130C File Offset: 0x0000F50C
		[__DynamicallyInvokable]
		public sealed override Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x0400059C RID: 1436
		private readonly Type _type;

		// Token: 0x0400059D RID: 1437
		private readonly bool _explicit;
	}
}
