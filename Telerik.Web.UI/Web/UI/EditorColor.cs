using System;
using System.Drawing;

namespace Telerik.Web.UI
{
	// Token: 0x0200106D RID: 4205
	public class EditorColor : ColorPickerItem
	{
		// Token: 0x0600A9A1 RID: 43425 RVA: 0x0024D824 File Offset: 0x0024BA24
		public EditorColor()
		{
		}

		// Token: 0x0600A9A2 RID: 43426 RVA: 0x0024D82C File Offset: 0x0024BA2C
		public EditorColor(Color color) : base(color)
		{
		}

		// Token: 0x0600A9A3 RID: 43427 RVA: 0x0024D835 File Offset: 0x0024BA35
		public EditorColor(Color color, string title) : base(color, title)
		{
		}
	}
}
