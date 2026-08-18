using System;

namespace System.Web.WebPages
{
	// Token: 0x0200008F RID: 143
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class PageVirtualPathAttribute : Attribute
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0000E00D File Offset: 0x0000C20D
		public PageVirtualPathAttribute(string virtualPath)
		{
			this.VirtualPath = virtualPath;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000E01C File Offset: 0x0000C21C
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0000E024 File Offset: 0x0000C224
		public string VirtualPath { get; private set; }
	}
}
