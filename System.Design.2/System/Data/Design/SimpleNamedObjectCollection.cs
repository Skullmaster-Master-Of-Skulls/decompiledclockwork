using System;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x02000260 RID: 608
	internal class SimpleNamedObjectCollection : ArrayList, INamedObjectCollection, ICollection, IEnumerable
	{
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x0008155C File Offset: 0x0007F75C
		protected virtual INameService NameService
		{
			get
			{
				if (SimpleNamedObjectCollection.myNameService == null)
				{
					SimpleNamedObjectCollection.myNameService = new SimpleNameService();
				}
				return SimpleNamedObjectCollection.myNameService;
			}
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00081574 File Offset: 0x0007F774
		public INameService GetNameService()
		{
			return this.NameService;
		}

		// Token: 0x04000BF2 RID: 3058
		private static SimpleNameService myNameService;
	}
}
