using System;
using System.Collections;
using System.Text;

namespace Telerik.Pdf
{
	// Token: 0x0200167C RID: 5756
	internal class XRefSubSection
	{
		// Token: 0x0600DE90 RID: 56976 RVA: 0x00309BFB File Offset: 0x00307DFB
		internal XRefSubSection()
		{
			this.entries = new ArrayList();
		}

		// Token: 0x0600DE91 RID: 56977 RVA: 0x00309C0E File Offset: 0x00307E0E
		internal void Add(PdfObjectId objectId, long offset)
		{
			this.entries.Add(new XRefSubSection.Entry(objectId, offset));
		}

		// Token: 0x0600DE92 RID: 56978 RVA: 0x00309C28 File Offset: 0x00307E28
		internal void Write(PdfWriter writer)
		{
			string text = "{0:0000000000} {1:00000} {2}";
			if (writer.NewLine.Length == 1)
			{
				text += " ";
			}
			this.entries.Sort();
			int num = 0;
			PdfObjectId objectId = ((XRefSubSection.Entry)this.entries[this.entries.Count - 1]).objectId;
			int objectNumber = objectId.ObjectNumber;
			int val = objectNumber - num + 1;
			writer.Write(num);
			writer.WriteSpace();
			writer.WriteLine(val);
			byte[] bytes = Encoding.ASCII.GetBytes(string.Format(text, 0, 65535, "f"));
			writer.WriteLine(bytes);
			foreach (object obj in this.entries)
			{
				XRefSubSection.Entry entry = (XRefSubSection.Entry)obj;
				Encoding ascii = Encoding.ASCII;
				string format = text;
				object arg = entry.offset;
				PdfObjectId objectId2 = entry.objectId;
				bytes = ascii.GetBytes(string.Format(format, arg, objectId2.GenerationNumber, "n"));
				writer.WriteLine(bytes);
			}
		}

		// Token: 0x04004002 RID: 16386
		private ArrayList entries;

		// Token: 0x0200167D RID: 5757
		private struct Entry : IComparable
		{
			// Token: 0x0600DE93 RID: 56979 RVA: 0x00309D64 File Offset: 0x00307F64
			internal Entry(PdfObjectId objectId, long offset)
			{
				this.objectId = objectId;
				this.offset = offset;
			}

			// Token: 0x0600DE94 RID: 56980 RVA: 0x00309D74 File Offset: 0x00307F74
			public int CompareTo(object obj)
			{
				int objectNumber = this.objectId.ObjectNumber;
				PdfObjectId pdfObjectId = ((XRefSubSection.Entry)obj).objectId;
				return objectNumber.CompareTo(pdfObjectId.ObjectNumber);
			}

			// Token: 0x04004003 RID: 16387
			internal PdfObjectId objectId;

			// Token: 0x04004004 RID: 16388
			internal long offset;
		}
	}
}
