using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000321 RID: 801
	public class ResourceFactory : IResourceFactory
	{
		// Token: 0x06001AC7 RID: 6855 RVA: 0x00056B8E File Offset: 0x00054D8E
		public IResource CreatResource()
		{
			return new Resource();
		}
	}
}
