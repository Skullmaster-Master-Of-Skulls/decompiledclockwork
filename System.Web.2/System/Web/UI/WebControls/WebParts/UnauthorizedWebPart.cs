using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000572 RID: 1394
	[ToolboxItem(false)]
	public sealed class UnauthorizedWebPart : ProxyWebPart
	{
		// Token: 0x060046C0 RID: 18112 RVA: 0x000E9F24 File Offset: 0x000E8124
		public UnauthorizedWebPart(WebPart webPart) : base(webPart)
		{
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x000E3034 File Offset: 0x000E1234
		public UnauthorizedWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID) : base(originalID, originalTypeName, originalPath, genericWebPartID)
		{
		}
	}
}
