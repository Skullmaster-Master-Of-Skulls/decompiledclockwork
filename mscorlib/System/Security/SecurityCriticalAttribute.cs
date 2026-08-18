using System;

namespace System.Security
{
	// Token: 0x0200066A RID: 1642
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class SecurityCriticalAttribute : Attribute
	{
		// Token: 0x06003B23 RID: 15139 RVA: 0x000C875F File Offset: 0x000C775F
		public SecurityCriticalAttribute()
		{
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000C8767 File Offset: 0x000C7767
		public SecurityCriticalAttribute(SecurityCriticalScope scope)
		{
			this._val = scope;
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x000C8776 File Offset: 0x000C7776
		public SecurityCriticalScope Scope
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001E97 RID: 7831
		internal SecurityCriticalScope _val;
	}
}
