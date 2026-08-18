using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000B03 RID: 2819
	[Obsolete]
	public class ODataProperty
	{
		// Token: 0x1700229A RID: 8858
		// (get) Token: 0x060069A4 RID: 27044 RVA: 0x0018D1B0 File Offset: 0x0018B3B0
		// (set) Token: 0x060069A5 RID: 27045 RVA: 0x0018D1B8 File Offset: 0x0018B3B8
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Gets or sets the name of the Property to be requested")]
		public string Name { get; set; }
	}
}
