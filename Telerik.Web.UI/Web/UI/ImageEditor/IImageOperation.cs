using System;
using System.Drawing;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x0200051B RID: 1307
	public interface IImageOperation
	{
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06002EB1 RID: 11953
		// (set) Token: 0x06002EB2 RID: 11954
		int Index { get; set; }

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06002EB3 RID: 11955
		string Name { get; }

		// Token: 0x06002EB4 RID: 11956
		Image Apply(Image image);
	}
}
