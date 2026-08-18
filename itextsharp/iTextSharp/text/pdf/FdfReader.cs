using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002D5 RID: 725
	public class FdfReader : PdfReader
	{
		// Token: 0x06001B02 RID: 6914 RVA: 0x0009F62C File Offset: 0x0009E62C
		public FdfReader(string filename) : base(filename)
		{
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0009F635 File Offset: 0x0009E635
		public FdfReader(byte[] pdfIn) : base(pdfIn)
		{
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0009F63E File Offset: 0x0009E63E
		public FdfReader(Uri url) : base(url)
		{
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0009F647 File Offset: 0x0009E647
		public FdfReader(Stream isp) : base(isp)
		{
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0009F650 File Offset: 0x0009E650
		protected internal override void ReadPdf()
		{
			this.fields = new Dictionary<string, PdfDictionary>();
			try
			{
				this.tokens.CheckFdfHeader();
				base.RebuildXref();
				base.ReadDocObj();
			}
			finally
			{
				try
				{
					this.tokens.Close();
				}
				catch
				{
				}
			}
			this.ReadFields();
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0009F6B4 File Offset: 0x0009E6B4
		protected virtual void KidNode(PdfDictionary merged, string name)
		{
			PdfArray asArray = merged.GetAsArray(PdfName.KIDS);
			if (asArray == null || asArray.Size == 0)
			{
				if (name.Length > 0)
				{
					name = name.Substring(1);
				}
				this.fields[name] = merged;
				return;
			}
			merged.Remove(PdfName.KIDS);
			for (int i = 0; i < asArray.Size; i++)
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Merge(merged);
				PdfDictionary asDict = asArray.GetAsDict(i);
				PdfString asString = asDict.GetAsString(PdfName.T);
				string text = name;
				if (asString != null)
				{
					text = text + "." + asString.ToUnicodeString();
				}
				pdfDictionary.Merge(asDict);
				pdfDictionary.Remove(PdfName.T);
				this.KidNode(pdfDictionary, text);
			}
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0009F770 File Offset: 0x0009E770
		protected virtual void ReadFields()
		{
			this.catalog = this.trailer.GetAsDict(PdfName.ROOT);
			PdfDictionary asDict = this.catalog.GetAsDict(PdfName.FDF);
			if (asDict == null)
			{
				return;
			}
			PdfString asString = asDict.GetAsString(PdfName.F);
			if (asString != null)
			{
				this.fileSpec = asString.ToUnicodeString();
			}
			PdfArray asArray = asDict.GetAsArray(PdfName.FIELDS);
			if (asArray == null)
			{
				return;
			}
			this.encoding = asDict.GetAsName(PdfName.ENCODING);
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.KIDS, asArray);
			this.KidNode(pdfDictionary, "");
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0009F802 File Offset: 0x0009E802
		public Dictionary<string, PdfDictionary> Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0009F80C File Offset: 0x0009E80C
		public PdfDictionary GetField(string name)
		{
			PdfDictionary result;
			this.fields.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0009F82C File Offset: 0x0009E82C
		public byte[] GetAttachedFile(string name)
		{
			PdfDictionary field = this.GetField(name);
			if (field != null)
			{
				PdfIndirectReference pdfIndirectReference = (PRIndirectReference)field.Get(PdfName.V);
				PdfDictionary pdfDictionary = (PdfDictionary)base.GetPdfObject(pdfIndirectReference.Number);
				PdfDictionary asDict = pdfDictionary.GetAsDict(PdfName.EF);
				pdfIndirectReference = (PRIndirectReference)asDict.Get(PdfName.F);
				PRStream stream = (PRStream)base.GetPdfObject(pdfIndirectReference.Number);
				return PdfReader.GetStreamBytes(stream);
			}
			return new byte[0];
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0009F8A8 File Offset: 0x0009E8A8
		public string GetFieldValue(string name)
		{
			PdfDictionary field = this.GetField(name);
			if (field == null)
			{
				return null;
			}
			PdfObject pdfObject = PdfReader.GetPdfObject(field.Get(PdfName.V));
			if (pdfObject == null)
			{
				return null;
			}
			if (pdfObject.IsName())
			{
				return PdfName.DecodeName(((PdfName)pdfObject).ToString());
			}
			if (!pdfObject.IsString())
			{
				return null;
			}
			PdfString pdfString = (PdfString)pdfObject;
			if (this.encoding == null || pdfString.Encoding != null)
			{
				return pdfString.ToUnicodeString();
			}
			byte[] bytes = pdfString.GetBytes();
			if (bytes.Length >= 2 && bytes[0] == 254 && bytes[1] == 255)
			{
				return pdfString.ToUnicodeString();
			}
			try
			{
				if (this.encoding.Equals(PdfName.SHIFT_JIS))
				{
					return Encoding.GetEncoding(932).GetString(bytes);
				}
				if (this.encoding.Equals(PdfName.UHC))
				{
					return Encoding.GetEncoding(949).GetString(bytes);
				}
				if (this.encoding.Equals(PdfName.GBK))
				{
					return Encoding.GetEncoding(936).GetString(bytes);
				}
				if (this.encoding.Equals(PdfName.BIGFIVE))
				{
					return Encoding.GetEncoding(950).GetString(bytes);
				}
			}
			catch
			{
			}
			return pdfString.ToUnicodeString();
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0009FA00 File Offset: 0x0009EA00
		public string FileSpec
		{
			get
			{
				return this.fileSpec;
			}
		}

		// Token: 0x040011F9 RID: 4601
		internal Dictionary<string, PdfDictionary> fields;

		// Token: 0x040011FA RID: 4602
		internal string fileSpec;

		// Token: 0x040011FB RID: 4603
		internal PdfName encoding;
	}
}
