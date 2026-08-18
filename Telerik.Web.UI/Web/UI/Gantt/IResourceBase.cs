using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000325 RID: 805
	public interface IResourceBase
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001AE2 RID: 6882
		// (set) Token: 0x06001AE3 RID: 6883
		object ID { get; set; }

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001AE4 RID: 6884
		// (set) Token: 0x06001AE5 RID: 6885
		string Text { get; set; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001AE6 RID: 6886
		// (set) Token: 0x06001AE7 RID: 6887
		string Format { get; set; }
	}
}
