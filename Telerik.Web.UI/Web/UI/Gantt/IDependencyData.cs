using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000330 RID: 816
	public interface IDependencyData : IDependencyBase
	{
		// Token: 0x06001C27 RID: 7207
		void CopyFrom(IDependency srcDependency);

		// Token: 0x06001C28 RID: 7208
		void CopyTo(IDependency destDependency);
	}
}
