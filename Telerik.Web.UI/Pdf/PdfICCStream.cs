using System;

namespace Telerik.Pdf
{
	// Token: 0x0200165C RID: 5724
	public class PdfICCStream : PdfStream
	{
		// Token: 0x0600DDC4 RID: 56772 RVA: 0x00307444 File Offset: 0x00305644
		public PdfICCStream(PdfObjectId id, byte[] profileData) : base(id)
		{
			base.data = profileData;
		}

		// Token: 0x170043E2 RID: 17378
		// (set) Token: 0x0600DDC5 RID: 56773 RVA: 0x00307454 File Offset: 0x00305654
		public PdfNumeric NumComponents
		{
			set
			{
				base.m_dictionary[PdfName.Names.N] = value;
			}
		}

		// Token: 0x170043E3 RID: 17379
		// (set) Token: 0x0600DDC6 RID: 56774 RVA: 0x00307467 File Offset: 0x00305667
		public PdfString AlternativeColorSpace
		{
			set
			{
				base.m_dictionary[PdfName.Names.Alternate] = value;
			}
		}
	}
}
