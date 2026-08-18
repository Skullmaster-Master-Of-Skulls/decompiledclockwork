using System;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x0200006D RID: 109
	public interface IFilterFormatter
	{
		// Token: 0x06000475 RID: 1141
		string[] Format(FileFilterCollection filters);

		// Token: 0x06000476 RID: 1142
		string Serialize(FileFilterCollection filters, bool format);
	}
}
