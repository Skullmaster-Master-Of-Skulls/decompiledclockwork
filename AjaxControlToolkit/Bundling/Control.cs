using System;
using System.Xml.Serialization;

namespace AjaxControlToolkit.Bundling
{
	// Token: 0x0200005D RID: 93
	public class Control
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000A4B5 File Offset: 0x000086B5
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000A4BD File Offset: 0x000086BD
		[XmlAttribute("name")]
		public string Name { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000A4C6 File Offset: 0x000086C6
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000A4CE File Offset: 0x000086CE
		[XmlAttribute("assembly")]
		public string Assembly { get; set; }
	}
}
