using System;
using System.IO;
using System.util.zlib;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000E0 RID: 224
	public class PdfContents : PdfStream
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x0002B00C File Offset: 0x0002A00C
		internal PdfContents(PdfContentByte under, PdfContentByte content, PdfContentByte text, PdfContentByte secondContent, Rectangle page)
		{
			this.streamBytes = new MemoryStream();
			Stream stream;
			if (Document.Compress)
			{
				this.compressed = true;
				stream = new ZDeflaterOutputStream(this.streamBytes, text.PdfWriter.CompressionLevel);
			}
			else
			{
				stream = this.streamBytes;
			}
			int rotation = page.Rotation;
			int num = rotation;
			if (num != 90)
			{
				if (num != 180)
				{
					if (num == 270)
					{
						stream.Write(PdfContents.ROTATE270, 0, PdfContents.ROTATE270.Length);
						stream.WriteByte(48);
						stream.WriteByte(32);
						byte[] isobytes = DocWriter.GetISOBytes(ByteBuffer.FormatDouble((double)page.Right));
						stream.Write(isobytes, 0, isobytes.Length);
						stream.Write(PdfContents.ROTATEFINAL, 0, PdfContents.ROTATEFINAL.Length);
					}
				}
				else
				{
					stream.Write(PdfContents.ROTATE180, 0, PdfContents.ROTATE180.Length);
					byte[] isobytes = DocWriter.GetISOBytes(ByteBuffer.FormatDouble((double)page.Right));
					stream.Write(isobytes, 0, isobytes.Length);
					stream.WriteByte(32);
					isobytes = DocWriter.GetISOBytes(ByteBuffer.FormatDouble((double)page.Top));
					stream.Write(isobytes, 0, isobytes.Length);
					stream.Write(PdfContents.ROTATEFINAL, 0, PdfContents.ROTATEFINAL.Length);
				}
			}
			else
			{
				stream.Write(PdfContents.ROTATE90, 0, PdfContents.ROTATE90.Length);
				byte[] isobytes = DocWriter.GetISOBytes(ByteBuffer.FormatDouble((double)page.Top));
				stream.Write(isobytes, 0, isobytes.Length);
				stream.WriteByte(32);
				stream.WriteByte(48);
				stream.Write(PdfContents.ROTATEFINAL, 0, PdfContents.ROTATEFINAL.Length);
			}
			if (under.Size > 0)
			{
				stream.Write(PdfContents.SAVESTATE, 0, PdfContents.SAVESTATE.Length);
				under.InternalBuffer.WriteTo(stream);
				stream.Write(PdfContents.RESTORESTATE, 0, PdfContents.RESTORESTATE.Length);
			}
			if (content.Size > 0)
			{
				stream.Write(PdfContents.SAVESTATE, 0, PdfContents.SAVESTATE.Length);
				content.InternalBuffer.WriteTo(stream);
				stream.Write(PdfContents.RESTORESTATE, 0, PdfContents.RESTORESTATE.Length);
			}
			if (text != null)
			{
				stream.Write(PdfContents.SAVESTATE, 0, PdfContents.SAVESTATE.Length);
				text.InternalBuffer.WriteTo(stream);
				stream.Write(PdfContents.RESTORESTATE, 0, PdfContents.RESTORESTATE.Length);
			}
			if (secondContent.Size > 0)
			{
				secondContent.InternalBuffer.WriteTo(stream);
			}
			if (stream is ZDeflaterOutputStream)
			{
				((ZDeflaterOutputStream)stream).Finish();
			}
			base.Put(PdfName.LENGTH, new PdfNumber((float)this.streamBytes.Length));
			if (this.compressed)
			{
				base.Put(PdfName.FILTER, PdfName.FLATEDECODE);
			}
		}

		// Token: 0x040006D4 RID: 1748
		internal static byte[] SAVESTATE = DocWriter.GetISOBytes("q\n");

		// Token: 0x040006D5 RID: 1749
		internal static byte[] RESTORESTATE = DocWriter.GetISOBytes("Q\n");

		// Token: 0x040006D6 RID: 1750
		internal static byte[] ROTATE90 = DocWriter.GetISOBytes("0 1 -1 0 ");

		// Token: 0x040006D7 RID: 1751
		internal static byte[] ROTATE180 = DocWriter.GetISOBytes("-1 0 0 -1 ");

		// Token: 0x040006D8 RID: 1752
		internal static byte[] ROTATE270 = DocWriter.GetISOBytes("0 -1 1 0 ");

		// Token: 0x040006D9 RID: 1753
		internal static byte[] ROTATEFINAL = DocWriter.GetISOBytes(" cm\n");
	}
}
