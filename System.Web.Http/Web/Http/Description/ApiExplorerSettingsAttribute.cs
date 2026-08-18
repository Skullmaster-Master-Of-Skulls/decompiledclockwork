using System;

namespace System.Web.Http.Description
{
	// Token: 0x020000BE RID: 190
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ApiExplorerSettingsAttribute : Attribute
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000E483 File Offset: 0x0000C683
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0000E48B File Offset: 0x0000C68B
		public bool IgnoreApi { get; set; }
	}
}
