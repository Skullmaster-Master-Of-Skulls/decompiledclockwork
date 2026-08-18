using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000171 RID: 369
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SchemaElementNameAttribute : Attribute
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00053B39 File Offset: 0x00051D39
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x00053B41 File Offset: 0x00051D41
		public string Value { get; private set; }

		// Token: 0x06000D27 RID: 3367 RVA: 0x00053B4A File Offset: 0x00051D4A
		public SchemaElementNameAttribute(string value)
		{
			this.Value = value;
		}
	}
}
