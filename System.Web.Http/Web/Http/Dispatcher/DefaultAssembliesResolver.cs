using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x020000B3 RID: 179
	public class DefaultAssembliesResolver : IAssembliesResolver
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000CAD5 File Offset: 0x0000ACD5
		public virtual ICollection<Assembly> GetAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies().ToList<Assembly>();
		}
	}
}
