using System;
using System.Collections.Generic;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200004D RID: 77
	public class PdfDictionary : PdfObject
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000A7A2 File Offset: 0x000097A2
		public PdfDictionary() : base(6)
		{
			this.hashMap = new Dictionary<PdfName, PdfObject>();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A7B6 File Offset: 0x000097B6
		public PdfDictionary(PdfName type) : this()
		{
			this.dictionaryType = type;
			this.Put(PdfName.TYPE, this.dictionaryType);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A7D8 File Offset: 0x000097D8
		public override void ToPdf(PdfWriter writer, Stream os)
		{
			os.WriteByte(60);
			os.WriteByte(60);
			foreach (PdfName pdfName in this.hashMap.Keys)
			{
				PdfObject pdfObject = this.hashMap[pdfName];
				pdfName.ToPdf(writer, os);
				int type = pdfObject.Type;
				if (type != 5 && type != 6 && type != 4 && type != 3)
				{
					os.WriteByte(32);
				}
				pdfObject.ToPdf(writer, os);
			}
			os.WriteByte(62);
			os.WriteByte(62);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A888 File Offset: 0x00009888
		public void Put(PdfName key, PdfObject value)
		{
			if (value == null || value.IsNull())
			{
				this.hashMap.Remove(key);
				return;
			}
			this.hashMap[key] = value;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000A8B0 File Offset: 0x000098B0
		public void PutEx(PdfName key, PdfObject value)
		{
			if (value == null)
			{
				return;
			}
			this.Put(key, value);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A8BE File Offset: 0x000098BE
		public void Remove(PdfName key)
		{
			this.hashMap.Remove(key);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A8CD File Offset: 0x000098CD
		public void Clear()
		{
			this.hashMap.Clear();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A8DC File Offset: 0x000098DC
		public PdfObject Get(PdfName key)
		{
			PdfObject result;
			if (this.hashMap.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A8FC File Offset: 0x000098FC
		public bool IsFont()
		{
			return PdfDictionary.FONT.Equals(this.dictionaryType);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A90E File Offset: 0x0000990E
		public bool IsPage()
		{
			return PdfDictionary.PAGE.Equals(this.dictionaryType);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A920 File Offset: 0x00009920
		public bool IsPages()
		{
			return PdfDictionary.PAGES.Equals(this.dictionaryType);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A932 File Offset: 0x00009932
		public bool IsCatalog()
		{
			return PdfDictionary.CATALOG.Equals(this.dictionaryType);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A944 File Offset: 0x00009944
		public bool IsOutlineTree()
		{
			return PdfDictionary.OUTLINES.Equals(this.dictionaryType);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A958 File Offset: 0x00009958
		public void Merge(PdfDictionary other)
		{
			foreach (PdfName key in other.hashMap.Keys)
			{
				this.hashMap[key] = other.hashMap[key];
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A9C4 File Offset: 0x000099C4
		public void MergeDifferent(PdfDictionary other)
		{
			foreach (PdfName key in other.hashMap.Keys)
			{
				if (!this.hashMap.ContainsKey(key))
				{
					this.hashMap[key] = other.hashMap[key];
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000AA3C File Offset: 0x00009A3C
		public Dictionary<PdfName, PdfObject>.KeyCollection Keys
		{
			get
			{
				return this.hashMap.Keys;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000AA49 File Offset: 0x00009A49
		public int Size
		{
			get
			{
				return this.hashMap.Count;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000AA56 File Offset: 0x00009A56
		public bool Contains(PdfName key)
		{
			return this.hashMap.ContainsKey(key);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000AA64 File Offset: 0x00009A64
		public virtual Dictionary<PdfName, PdfObject>.Enumerator GetEnumerator()
		{
			return this.hashMap.GetEnumerator();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000AA71 File Offset: 0x00009A71
		public override string ToString()
		{
			if (this.Get(PdfName.TYPE) == null)
			{
				return "Dictionary";
			}
			return "Dictionary of type: " + this.Get(PdfName.TYPE);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000AA9B File Offset: 0x00009A9B
		public virtual PdfObject GetDirectObject(PdfName key)
		{
			return PdfReader.GetPdfObject(this.Get(key));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000AAAC File Offset: 0x00009AAC
		public PdfDictionary GetAsDict(PdfName key)
		{
			PdfDictionary result = null;
			PdfObject directObject = this.GetDirectObject(key);
			if (directObject != null && directObject.IsDictionary())
			{
				result = (PdfDictionary)directObject;
			}
			return result;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000AAD8 File Offset: 0x00009AD8
		public PdfArray GetAsArray(PdfName key)
		{
			PdfArray result = null;
			PdfObject directObject = this.GetDirectObject(key);
			if (directObject != null && directObject.IsArray())
			{
				result = (PdfArray)directObject;
			}
			return result;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000AB04 File Offset: 0x00009B04
		public PdfStream GetAsStream(PdfName key)
		{
			PdfStream result = null;
			PdfObject directObject = this.GetDirectObject(key);
			if (directObject != null && directObject.IsStream())
			{
				result = (PdfStream)directObject;
			}
			return result;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000AB30 File Offset: 0x00009B30
		public PdfString GetAsString(PdfName key)
		{
			PdfString result = null;
			PdfObject directObject = this.GetDirectObject(key);
			if (directObject != null && directObject.IsString())
			{
				result = (PdfString)directObject;
			}
			return result;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000AB5C File Offset: 0x00009B5C
		public PdfNumber GetAsNumber(PdfName key)
		{
			PdfNumber result = null;
			PdfObject directObject = this.GetDirectObject(key);
			if (directObject != null && directObject.IsNumber())
			{
				result = (PdfNumber)directObject;
			}
			return result;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000AB88 File Offset: 0x00009B88
		public PdfName GetAsName(PdfName key)
		{
			PdfName result = null;
			PdfObject directObject = this.GetDirectObject(key);
			if (directObject != null && directObject.IsName())
			{
				result = (PdfName)directObject;
			}
			return result;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000ABB4 File Offset: 0x00009BB4
		public PdfBoolean GetAsBoolean(PdfName key)
		{
			PdfBoolean result = null;
			PdfObject directObject = this.GetDirectObject(key);
			if (directObject != null && directObject.IsBoolean())
			{
				result = (PdfBoolean)directObject;
			}
			return result;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000ABE0 File Offset: 0x00009BE0
		public PdfIndirectReference GetAsIndirectObject(PdfName key)
		{
			PdfIndirectReference result = null;
			PdfObject pdfObject = this.Get(key);
			if (pdfObject != null && pdfObject.IsIndirect())
			{
				result = (PdfIndirectReference)pdfObject;
			}
			return result;
		}

		// Token: 0x040000EB RID: 235
		public static PdfName FONT = PdfName.FONT;

		// Token: 0x040000EC RID: 236
		public static PdfName OUTLINES = PdfName.OUTLINES;

		// Token: 0x040000ED RID: 237
		public static PdfName PAGE = PdfName.PAGE;

		// Token: 0x040000EE RID: 238
		public static PdfName PAGES = PdfName.PAGES;

		// Token: 0x040000EF RID: 239
		public static PdfName CATALOG = PdfName.CATALOG;

		// Token: 0x040000F0 RID: 240
		private PdfName dictionaryType;

		// Token: 0x040000F1 RID: 241
		protected internal Dictionary<PdfName, PdfObject> hashMap;
	}
}
