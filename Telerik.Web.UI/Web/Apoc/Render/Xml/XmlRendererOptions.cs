using System;

namespace Telerik.Web.Apoc.Render.Xml
{
	// Token: 0x020016A4 RID: 5796
	public sealed class XmlRendererOptions : IRendererOptions
	{
		// Token: 0x1700449D RID: 17565
		// (get) Token: 0x0600DFE0 RID: 57312 RVA: 0x0031D208 File Offset: 0x0031B408
		// (set) Token: 0x0600DFE1 RID: 57313 RVA: 0x0031D210 File Offset: 0x0031B410
		public bool FineDetail
		{
			get
			{
				return this.fineDetail;
			}
			set
			{
				this.fineDetail = value;
			}
		}

		// Token: 0x040040B8 RID: 16568
		public static readonly XmlRendererOptions Default = new XmlRendererOptions();

		// Token: 0x040040B9 RID: 16569
		private bool fineDetail = true;
	}
}
