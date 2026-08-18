using System;
using System.Collections.Generic;
using System.IO;
using System.util;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000162 RID: 354
	public class PdfArray : PdfObject
	{
		// Token: 0x06000D5D RID: 3421 RVA: 0x00049A5E File Offset: 0x00048A5E
		public PdfArray() : base(5)
		{
			this.arrayList = new List<PdfObject>();
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00049A72 File Offset: 0x00048A72
		public PdfArray(PdfObject obj) : base(5)
		{
			this.arrayList = new List<PdfObject>();
			this.arrayList.Add(obj);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00049A92 File Offset: 0x00048A92
		public PdfArray(float[] values) : base(5)
		{
			this.arrayList = new List<PdfObject>();
			this.Add(values);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00049AAE File Offset: 0x00048AAE
		public PdfArray(int[] values) : base(5)
		{
			this.arrayList = new List<PdfObject>();
			this.Add(values);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00049ACC File Offset: 0x00048ACC
		public PdfArray(List<PdfObject> l) : this()
		{
			foreach (PdfObject obj in l)
			{
				this.Add(obj);
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00049B24 File Offset: 0x00048B24
		public PdfArray(PdfArray array) : base(5)
		{
			this.arrayList = new List<PdfObject>(array.arrayList);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00049B40 File Offset: 0x00048B40
		public override void ToPdf(PdfWriter writer, Stream os)
		{
			os.WriteByte(91);
			bool flag = true;
			foreach (PdfObject pdfObject in this.arrayList)
			{
				PdfObject pdfObject2 = (pdfObject == null) ? PdfNull.PDFNULL : pdfObject;
				this.type = pdfObject2.Type;
				if (!flag && this.type != 5 && this.type != 6 && this.type != 4 && this.type != 3)
				{
					os.WriteByte(32);
				}
				flag = false;
				pdfObject2.ToPdf(writer, os);
			}
			os.WriteByte(93);
		}

		// Token: 0x17000297 RID: 663
		public PdfObject this[int idx]
		{
			get
			{
				return this.arrayList[idx];
			}
			set
			{
				this.arrayList[idx] = value;
			}
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00049C14 File Offset: 0x00048C14
		public PdfObject Remove(int idx)
		{
			PdfObject result = this.arrayList[idx];
			this.arrayList.RemoveAt(idx);
			return result;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00049C3B File Offset: 0x00048C3B
		public List<PdfObject> ArrayList
		{
			get
			{
				return this.arrayList;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00049C43 File Offset: 0x00048C43
		public int Size
		{
			get
			{
				return this.arrayList.Count;
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00049C50 File Offset: 0x00048C50
		public bool IsEmpty()
		{
			return this.arrayList.Count == 0;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00049C60 File Offset: 0x00048C60
		public virtual bool Add(PdfObject obj)
		{
			this.arrayList.Add(obj);
			return true;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00049C70 File Offset: 0x00048C70
		public virtual bool Add(float[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				this.arrayList.Add(new PdfNumber(values[i]));
			}
			return true;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00049CA0 File Offset: 0x00048CA0
		public virtual bool Add(int[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				this.arrayList.Add(new PdfNumber(values[i]));
			}
			return true;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00049CCF File Offset: 0x00048CCF
		public virtual void Add(int index, PdfObject element)
		{
			this.arrayList.Insert(index, element);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00049CDE File Offset: 0x00048CDE
		public virtual void AddFirst(PdfObject obj)
		{
			this.arrayList.Insert(0, obj);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00049CED File Offset: 0x00048CED
		public bool Contains(PdfObject obj)
		{
			return this.arrayList.Contains(obj);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00049CFB File Offset: 0x00048CFB
		public ListIterator<PdfObject> GetListIterator()
		{
			return new ListIterator<PdfObject>(this.arrayList);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00049D08 File Offset: 0x00048D08
		public override string ToString()
		{
			return this.arrayList.ToString();
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00049D15 File Offset: 0x00048D15
		public PdfObject GetDirectObject(int idx)
		{
			return PdfReader.GetPdfObject(this[idx]);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00049D24 File Offset: 0x00048D24
		public PdfDictionary GetAsDict(int idx)
		{
			PdfDictionary result = null;
			PdfObject directObject = this.GetDirectObject(idx);
			if (directObject != null && directObject.IsDictionary())
			{
				result = (PdfDictionary)directObject;
			}
			return result;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00049D50 File Offset: 0x00048D50
		public PdfArray GetAsArray(int idx)
		{
			PdfArray result = null;
			PdfObject directObject = this.GetDirectObject(idx);
			if (directObject != null && directObject.IsArray())
			{
				result = (PdfArray)directObject;
			}
			return result;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00049D7C File Offset: 0x00048D7C
		public PdfStream GetAsStream(int idx)
		{
			PdfStream result = null;
			PdfObject directObject = this.GetDirectObject(idx);
			if (directObject != null && directObject.IsStream())
			{
				result = (PdfStream)directObject;
			}
			return result;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00049DA8 File Offset: 0x00048DA8
		public PdfString GetAsString(int idx)
		{
			PdfString result = null;
			PdfObject directObject = this.GetDirectObject(idx);
			if (directObject != null && directObject.IsString())
			{
				result = (PdfString)directObject;
			}
			return result;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00049DD4 File Offset: 0x00048DD4
		public PdfNumber GetAsNumber(int idx)
		{
			PdfNumber result = null;
			PdfObject directObject = this.GetDirectObject(idx);
			if (directObject != null && directObject.IsNumber())
			{
				result = (PdfNumber)directObject;
			}
			return result;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00049E00 File Offset: 0x00048E00
		public PdfName GetAsName(int idx)
		{
			PdfName result = null;
			PdfObject directObject = this.GetDirectObject(idx);
			if (directObject != null && directObject.IsName())
			{
				result = (PdfName)directObject;
			}
			return result;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00049E2C File Offset: 0x00048E2C
		public PdfBoolean GetAsBoolean(int idx)
		{
			PdfBoolean result = null;
			PdfObject directObject = this.GetDirectObject(idx);
			if (directObject != null && directObject.IsBoolean())
			{
				result = (PdfBoolean)directObject;
			}
			return result;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00049E58 File Offset: 0x00048E58
		public PdfIndirectReference GetAsIndirectObject(int idx)
		{
			PdfIndirectReference result = null;
			PdfObject pdfObject = this[idx];
			if (pdfObject != null && pdfObject.IsIndirect())
			{
				result = (PdfIndirectReference)pdfObject;
			}
			return result;
		}

		// Token: 0x040009F4 RID: 2548
		protected List<PdfObject> arrayList;
	}
}
