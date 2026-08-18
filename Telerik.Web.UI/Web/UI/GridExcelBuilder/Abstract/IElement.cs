using System;
using System.Text;

namespace Telerik.Web.UI.GridExcelBuilder.Abstract
{
	// Token: 0x02000B77 RID: 2935
	public interface IElement
	{
		// Token: 0x1700245C RID: 9308
		// (get) Token: 0x06006EDB RID: 28379
		IElementsCollection InnerElements { get; }

		// Token: 0x1700245D RID: 9309
		// (get) Token: 0x06006EDC RID: 28380
		IAttributesCollection Attributes { get; }

		// Token: 0x06006EDD RID: 28381
		void Render(StringBuilder sb);
	}
}
