using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x020004FD RID: 1277
	public class ProcessModuleCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600306A RID: 12394 RVA: 0x000DBAED File Offset: 0x000D9CED
		protected ProcessModuleCollection()
		{
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000DBAF5 File Offset: 0x000D9CF5
		public ProcessModuleCollection(ProcessModule[] processModules)
		{
			base.InnerList.AddRange(processModules);
		}

		// Token: 0x17000BD4 RID: 3028
		public ProcessModule this[int index]
		{
			get
			{
				return (ProcessModule)base.InnerList[index];
			}
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000DBB1C File Offset: 0x000D9D1C
		public int IndexOf(ProcessModule module)
		{
			return base.InnerList.IndexOf(module);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000DBB2A File Offset: 0x000D9D2A
		public bool Contains(ProcessModule module)
		{
			return base.InnerList.Contains(module);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000DBB38 File Offset: 0x000D9D38
		public void CopyTo(ProcessModule[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
