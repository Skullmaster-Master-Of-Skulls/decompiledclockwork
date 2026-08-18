using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003B3 RID: 947
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ConstructorNeedsTagAttribute : Attribute
	{
		// Token: 0x06002E3A RID: 11834 RVA: 0x000CF5C9 File Offset: 0x000CE5C9
		public ConstructorNeedsTagAttribute()
		{
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000CF5D1 File Offset: 0x000CE5D1
		public ConstructorNeedsTagAttribute(bool needsTag)
		{
			this.needsTag = needsTag;
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x000CF5E0 File Offset: 0x000CE5E0
		public bool NeedsTag
		{
			get
			{
				return this.needsTag;
			}
		}

		// Token: 0x0400217B RID: 8571
		private bool needsTag;
	}
}
