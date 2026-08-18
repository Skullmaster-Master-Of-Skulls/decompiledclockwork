using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000327 RID: 807
	public interface IResourceData : IResourceBase
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001AEC RID: 6892
		// (set) Token: 0x06001AED RID: 6893
		string Color { get; set; }

		// Token: 0x06001AEE RID: 6894
		void CopyFrom(IResource srcResource);
	}
}
