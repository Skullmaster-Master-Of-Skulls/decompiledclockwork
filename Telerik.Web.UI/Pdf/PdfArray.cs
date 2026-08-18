using System;
using System.Collections;

namespace Telerik.Pdf
{
	// Token: 0x02001645 RID: 5701
	public class PdfArray : PdfObject, IEnumerable
	{
		// Token: 0x0600DD15 RID: 56597 RVA: 0x003051FE File Offset: 0x003033FE
		public PdfArray()
		{
		}

		// Token: 0x0600DD16 RID: 56598 RVA: 0x00305211 File Offset: 0x00303411
		public PdfArray(PdfObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x0600DD17 RID: 56599 RVA: 0x00305225 File Offset: 0x00303425
		public int Add(PdfObject value)
		{
			return this.elements.Add(value);
		}

		// Token: 0x0600DD18 RID: 56600 RVA: 0x00305233 File Offset: 0x00303433
		public void Clear()
		{
			this.elements.Clear();
		}

		// Token: 0x0600DD19 RID: 56601 RVA: 0x00305240 File Offset: 0x00303440
		public bool Contains(PdfObject value)
		{
			return this.elements.Contains(value);
		}

		// Token: 0x0600DD1A RID: 56602 RVA: 0x0030524E File Offset: 0x0030344E
		public int IndexOf(PdfObject value)
		{
			return this.elements.IndexOf(value);
		}

		// Token: 0x0600DD1B RID: 56603 RVA: 0x0030525C File Offset: 0x0030345C
		public void Insert(int index, PdfObject value)
		{
			this.elements.Insert(index, value);
		}

		// Token: 0x0600DD1C RID: 56604 RVA: 0x0030526B File Offset: 0x0030346B
		public void Remove(PdfObject value)
		{
			this.elements.Remove(value);
		}

		// Token: 0x0600DD1D RID: 56605 RVA: 0x00305279 File Offset: 0x00303479
		public void RemoveAt(int index)
		{
			this.elements.RemoveAt(index);
		}

		// Token: 0x0600DD1E RID: 56606 RVA: 0x00305287 File Offset: 0x00303487
		public IEnumerator GetEnumerator()
		{
			return this.elements.GetEnumerator();
		}

		// Token: 0x170043AE RID: 17326
		public PdfObject this[int index]
		{
			get
			{
				return (PdfObject)this.elements[index];
			}
			set
			{
				this.elements[index] = value;
			}
		}

		// Token: 0x170043AF RID: 17327
		// (get) Token: 0x0600DD21 RID: 56609 RVA: 0x003052B6 File Offset: 0x003034B6
		public int Count
		{
			get
			{
				return this.elements.Count;
			}
		}

		// Token: 0x0600DD22 RID: 56610 RVA: 0x003052C4 File Offset: 0x003034C4
		public void AddArray(Array data)
		{
			foreach (object value in data)
			{
				this.Add(new PdfNumeric(Convert.ToDecimal(value)));
			}
		}

		// Token: 0x0600DD23 RID: 56611 RVA: 0x00305320 File Offset: 0x00303520
		protected internal override void Write(PdfWriter writer)
		{
			writer.WriteKeyword(Keyword.ArrayBegin);
			bool flag = true;
			foreach (object obj in this.elements)
			{
				PdfObject obj2 = (PdfObject)obj;
				if (!flag)
				{
					writer.WriteSpace();
				}
				else
				{
					flag = false;
				}
				writer.Write(obj2);
			}
			writer.WriteKeyword(Keyword.ArrayEnd);
		}

		// Token: 0x04003EEC RID: 16108
		private ArrayList elements = new ArrayList();
	}
}
