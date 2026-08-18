using System;

namespace System.Web.Http
{
	// Token: 0x020000CE RID: 206
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ActionNameAttribute : Attribute
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x0000FEF5 File Offset: 0x0000E0F5
		public ActionNameAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000FF04 File Offset: 0x0000E104
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0000FF0C File Offset: 0x0000E10C
		public string Name { get; private set; }
	}
}
