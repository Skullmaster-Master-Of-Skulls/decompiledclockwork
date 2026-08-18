using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000F1F RID: 3871
	public class SkinReferenceCollection : StronglyTypedStateManagedCollection<SkinReference>
	{
		// Token: 0x060093CE RID: 37838 RVA: 0x00212BF8 File Offset: 0x00210DF8
		public List<string> GetAssemblyNames()
		{
			List<string> list = new List<string>();
			foreach (object obj in base.List)
			{
				SkinReference skinReference = (SkinReference)obj;
				if (!string.IsNullOrEmpty(skinReference.Assembly))
				{
					list.Add(skinReference.Assembly);
				}
			}
			return list;
		}

		// Token: 0x060093CF RID: 37839 RVA: 0x00212C6C File Offset: 0x00210E6C
		protected override void SetDirtyObject(object o)
		{
			if (o is SkinReference)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}
