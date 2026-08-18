using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000864 RID: 2148
	public class AssemblyWhiteListCollection : Collection<AssemblyReference>
	{
		// Token: 0x06004F13 RID: 20243 RVA: 0x000F7F08 File Offset: 0x000F6108
		public AssemblyWhiteListCollection(IEnumerable<AssemblyReference> scriptReference)
		{
			if (scriptReference != null)
			{
				foreach (AssemblyReference item in scriptReference)
				{
					base.Add(item);
				}
			}
		}
	}
}
