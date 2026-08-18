using System;
using System.Reflection;
using System.Resources;

namespace Spire.DataExport.ResourceMgr
{
	// Token: 0x02000002 RID: 2
	public abstract class ResManager
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000010D0
		public static ResourceManager GetResourceManager()
		{
			if (ResManager._resourceManager == null)
			{
				ResManager._resourceManager = new ResourceManager("Spire.DataExport.ResourceMgr.ResManager", Assembly.GetExecutingAssembly());
			}
			return ResManager._resourceManager;
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager _resourceManager;
	}
}
