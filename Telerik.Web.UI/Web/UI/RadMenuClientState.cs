using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001B42 RID: 6978
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadMenuClientState
	{
		// Token: 0x17005245 RID: 21061
		// (get) Token: 0x06010DF3 RID: 69107 RVA: 0x003BE087 File Offset: 0x003BC287
		// (set) Token: 0x06010DF4 RID: 69108 RVA: 0x003BE08F File Offset: 0x003BC28F
		public ClientStateLogEntry[] LogEntries { get; set; }

		// Token: 0x17005246 RID: 21062
		// (get) Token: 0x06010DF5 RID: 69109 RVA: 0x003BE098 File Offset: 0x003BC298
		// (set) Token: 0x06010DF6 RID: 69110 RVA: 0x003BE0A0 File Offset: 0x003BC2A0
		public string SelectedItemIndex { get; set; }
	}
}
