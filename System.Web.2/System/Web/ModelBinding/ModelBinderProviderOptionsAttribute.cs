using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200067A RID: 1658
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ModelBinderProviderOptionsAttribute : Attribute
	{
		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x060050A1 RID: 20641 RVA: 0x00116254 File Offset: 0x00114454
		// (set) Token: 0x060050A2 RID: 20642 RVA: 0x0011625C File Offset: 0x0011445C
		public bool FrontOfList { get; set; }
	}
}
