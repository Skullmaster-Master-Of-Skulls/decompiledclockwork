using System;
using System.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Web
{
	// Token: 0x02000008 RID: 8
	public interface IControl
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000036 RID: 54
		Page Page { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000037 RID: 55
		string ID { get; }

		// Token: 0x06000038 RID: 56
		void DescribeComponent(IScriptDescriptor descriptor);

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000039 RID: 57
		// (set) Token: 0x0600003A RID: 58
		bool RegisterWithScriptManager { get; set; }

		// Token: 0x0600003B RID: 59
		void EnsureChildControlsCreated();
	}
}
