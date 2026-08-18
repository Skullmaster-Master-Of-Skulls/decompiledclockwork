using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.codec
{
	// Token: 0x020000F1 RID: 241
	public class JBIG2Image
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x000308B0 File Offset: 0x0002F8B0
		public static byte[] GetGlobalSegment(RandomAccessFileOrArray ra)
		{
			byte[] result;
			try
			{
				JBIG2SegmentReader jbig2SegmentReader = new JBIG2SegmentReader(ra);
				jbig2SegmentReader.Read();
				result = jbig2SegmentReader.GetGlobal(true);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000308EC File Offset: 0x0002F8EC
		public static Image GetJbig2Image(RandomAccessFileOrArray ra, int page)
		{
			if (page < 1)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.page.number.must.be.gt.eq.1"));
			}
			JBIG2SegmentReader jbig2SegmentReader = new JBIG2SegmentReader(ra);
			jbig2SegmentReader.Read();
			JBIG2SegmentReader.JBIG2Page page2 = jbig2SegmentReader.GetPage(page);
			return new ImgJBIG2(page2.pageBitmapWidth, page2.pageBitmapHeight, page2.GetData(true), jbig2SegmentReader.GetGlobal(true));
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00030944 File Offset: 0x0002F944
		public static int GetNumberOfPages(RandomAccessFileOrArray ra)
		{
			JBIG2SegmentReader jbig2SegmentReader = new JBIG2SegmentReader(ra);
			jbig2SegmentReader.Read();
			return jbig2SegmentReader.NumberOfPages();
		}
	}
}
