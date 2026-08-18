using System;
using System.Xml.Serialization;

namespace AjaxControlToolkit.Bundling
{
	// Token: 0x0200005A RID: 90
	[XmlRoot("ajaxControlToolkit")]
	public class Settings
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000A459 File Offset: 0x00008659
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000A461 File Offset: 0x00008661
		[XmlElement("controlBundles", IsNullable = false)]
		public ControlBundleSection[] ControlBundleSections { get; set; }
	}
}
