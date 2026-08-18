using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x02000788 RID: 1928
	public class ProcessModuleCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06003B85 RID: 15237 RVA: 0x000FDEFE File Offset: 0x000FCEFE
		protected ProcessModuleCollection()
		{
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000FDF06 File Offset: 0x000FCF06
		public ProcessModuleCollection(ProcessModule[] processModules)
		{
			base.InnerList.AddRange(processModules);
		}

		// Token: 0x17000DF4 RID: 3572
		public ProcessModule this[int index]
		{
			get
			{
				return (ProcessModule)base.InnerList[index];
			}
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000FDF2D File Offset: 0x000FCF2D
		public int IndexOf(ProcessModule module)
		{
			return base.InnerList.IndexOf(module);
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000FDF3B File Offset: 0x000FCF3B
		public bool Contains(ProcessModule module)
		{
			return base.InnerList.Contains(module);
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000FDF49 File Offset: 0x000FCF49
		public void CopyTo(ProcessModule[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
