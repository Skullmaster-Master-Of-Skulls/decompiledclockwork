using System;
using System.Xml.Serialization;

namespace AjaxControlToolkit.Bundling
{
	// Token: 0x0200005C RID: 92
	public class ControlBundle
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000A48B File Offset: 0x0000868B
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000A493 File Offset: 0x00008693
		[XmlAttribute("name")]
		public string Name { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000A49C File Offset: 0x0000869C
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000A4A4 File Offset: 0x000086A4
		[XmlElement("control", IsNullable = false)]
		public Control[] Controls { get; set; }
	}
}
