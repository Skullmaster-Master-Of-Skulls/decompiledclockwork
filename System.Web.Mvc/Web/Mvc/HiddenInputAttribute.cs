using System;

namespace System.Web.Mvc
{
	// Token: 0x0200013D RID: 317
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class HiddenInputAttribute : Attribute
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x000168DE File Offset: 0x00014ADE
		public HiddenInputAttribute()
		{
			this.DisplayValue = true;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x000168ED File Offset: 0x00014AED
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x000168F5 File Offset: 0x00014AF5
		public bool DisplayValue { get; set; }
	}
}
