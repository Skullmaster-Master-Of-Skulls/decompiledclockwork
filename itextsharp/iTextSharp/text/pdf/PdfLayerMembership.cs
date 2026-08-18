using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000CA RID: 202
	public class PdfLayerMembership : PdfDictionary, IPdfOCG
	{
		// Token: 0x0600070C RID: 1804 RVA: 0x000257A6 File Offset: 0x000247A6
		public PdfLayerMembership(PdfWriter writer) : base(PdfName.OCMD)
		{
			base.Put(PdfName.OCGS, this.members);
			this.refi = writer.PdfIndirectReference;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x000257E6 File Offset: 0x000247E6
		public PdfIndirectReference Ref
		{
			get
			{
				return this.refi;
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000257EE File Offset: 0x000247EE
		public void AddMember(PdfLayer layer)
		{
			if (!this.layers.ContainsKey(layer))
			{
				this.members.Add(layer.Ref);
				this.layers[layer] = null;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0002581D File Offset: 0x0002481D
		public Dictionary<PdfLayer, object>.KeyCollection Layers
		{
			get
			{
				return this.layers.Keys;
			}
		}

		// Token: 0x17000175 RID: 373
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0002582A File Offset: 0x0002482A
		public PdfName VisibilityPolicy
		{
			set
			{
				base.Put(PdfName.P, value);
			}
		}

		// Token: 0x17000176 RID: 374
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00025838 File Offset: 0x00024838
		public PdfVisibilityExpression VisibilityExpression
		{
			set
			{
				base.Put(PdfName.VE, value);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00025846 File Offset: 0x00024846
		public PdfObject PdfObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000608 RID: 1544
		public static readonly PdfName ALLON = new PdfName("AllOn");

		// Token: 0x04000609 RID: 1545
		public static readonly PdfName ANYON = new PdfName("AnyOn");

		// Token: 0x0400060A RID: 1546
		public static readonly PdfName ANYOFF = new PdfName("AnyOff");

		// Token: 0x0400060B RID: 1547
		public static readonly PdfName ALLOFF = new PdfName("AllOff");

		// Token: 0x0400060C RID: 1548
		internal PdfIndirectReference refi;

		// Token: 0x0400060D RID: 1549
		internal PdfArray members = new PdfArray();

		// Token: 0x0400060E RID: 1550
		internal Dictionary<PdfLayer, object> layers = new Dictionary<PdfLayer, object>();
	}
}
