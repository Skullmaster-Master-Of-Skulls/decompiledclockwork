using System;
using System.Xml.Serialization;

namespace AjaxControlToolkit.Bundling
{
	// Token: 0x0200005B RID: 91
	public class ControlBundleSection
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000A472 File Offset: 0x00008672
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000A47A File Offset: 0x0000867A
		[XmlElement("controlBundle", IsNullable = false)]
		public ControlBundle[] ControlBundles { get; set; }
	}
}
