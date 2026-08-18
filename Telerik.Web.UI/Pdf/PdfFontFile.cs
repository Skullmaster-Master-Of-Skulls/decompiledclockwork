using System;
using Telerik.Pdf.Filter;
using Telerik.Web.Apoc.Pdf;

namespace Telerik.Pdf
{
	// Token: 0x02001657 RID: 5719
	public class PdfFontFile : PdfStream
	{
		// Token: 0x0600DDB8 RID: 56760 RVA: 0x0030722C File Offset: 0x0030542C
		internal PdfFontFile(PdfObjectId id, byte[] fontData, PdfCreator creator) : base(fontData, id)
		{
			IFilter activeFilter = creator.RendererOptions.GetActiveFilter();
			if (activeFilter != null)
			{
				base.AddFilter(activeFilter);
			}
			base.m_dictionary[PdfName.Names.Length1] = new PdfNumeric(fontData.Length);
		}
	}
}
