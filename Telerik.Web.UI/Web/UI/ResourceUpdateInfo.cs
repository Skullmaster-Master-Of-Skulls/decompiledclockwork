using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001A3C RID: 6716
	public class ResourceUpdateInfo
	{
		// Token: 0x17004EF3 RID: 20211
		// (get) Token: 0x06010496 RID: 66710 RVA: 0x003A3931 File Offset: 0x003A1B31
		public Resource OldResource
		{
			get
			{
				return this._oldResource;
			}
		}

		// Token: 0x17004EF4 RID: 20212
		// (get) Token: 0x06010497 RID: 66711 RVA: 0x003A3939 File Offset: 0x003A1B39
		public Resource NewResource
		{
			get
			{
				return this._newResource;
			}
		}

		// Token: 0x06010498 RID: 66712 RVA: 0x003A3941 File Offset: 0x003A1B41
		public ResourceUpdateInfo(Resource oldResource, Resource newResource)
		{
			this._oldResource = oldResource;
			this._newResource = newResource;
		}

		// Token: 0x0400495D RID: 18781
		private readonly Resource _oldResource;

		// Token: 0x0400495E RID: 18782
		private readonly Resource _newResource;
	}
}
