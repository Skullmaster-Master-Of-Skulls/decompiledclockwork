using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000030 RID: 48
	public class DesignerAutoFormatStyle : Style
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000CB98 File Offset: 0x0000AD98
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		public VerticalAlign VerticalAlign
		{
			get
			{
				return this._verticalAlign;
			}
			set
			{
				this._verticalAlign = value;
			}
		}

		// Token: 0x0400011B RID: 283
		private VerticalAlign _verticalAlign;
	}
}
