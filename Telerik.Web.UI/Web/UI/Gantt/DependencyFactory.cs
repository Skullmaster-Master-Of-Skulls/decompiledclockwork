using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000357 RID: 855
	public class DependencyFactory : IDependencyFactory
	{
		// Token: 0x06001DAA RID: 7594 RVA: 0x0005CF1E File Offset: 0x0005B11E
		public IDependency CreateDependency()
		{
			return new Dependency();
		}
	}
}
