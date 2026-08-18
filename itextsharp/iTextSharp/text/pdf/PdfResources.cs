using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000639 RID: 1593
	internal class PdfResources : PdfDictionary
	{
		// Token: 0x060035F4 RID: 13812 RVA: 0x0014F46D File Offset: 0x0014E46D
		internal PdfResources()
		{
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x0014F478 File Offset: 0x0014E478
		internal void Add(PdfName key, PdfDictionary resource)
		{
			if (resource.Size == 0)
			{
				return;
			}
			PdfDictionary asDict = base.GetAsDict(key);
			if (asDict == null)
			{
				base.Put(key, resource);
				return;
			}
			asDict.Merge(resource);
		}
	}
}
