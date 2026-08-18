using System;
using System.Collections;

namespace Telerik.Pdf
{
	// Token: 0x02001648 RID: 5704
	public class PdfDictionary : PdfObject, IEnumerable
	{
		// Token: 0x0600DD28 RID: 56616 RVA: 0x003053E0 File Offset: 0x003035E0
		public PdfDictionary()
		{
		}

		// Token: 0x0600DD29 RID: 56617 RVA: 0x003053F3 File Offset: 0x003035F3
		public PdfDictionary(PdfObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x170043B0 RID: 17328
		// (get) Token: 0x0600DD2A RID: 56618 RVA: 0x00305407 File Offset: 0x00303607
		// (set) Token: 0x0600DD2B RID: 56619 RVA: 0x0030540F File Offset: 0x0030360F
		protected Hashtable entries
		{
			get
			{
				return this._entries;
			}
			set
			{
				this._entries = value;
			}
		}

		// Token: 0x0600DD2C RID: 56620 RVA: 0x00305418 File Offset: 0x00303618
		public void Add(PdfName key, PdfObject value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.entries.ContainsKey(key))
			{
				throw new ArgumentException("Already contains entry " + key);
			}
			this.entries.Add(key, value);
		}

		// Token: 0x0600DD2D RID: 56621 RVA: 0x00305454 File Offset: 0x00303654
		public void Clear()
		{
			this.entries.Clear();
		}

		// Token: 0x0600DD2E RID: 56622 RVA: 0x00305461 File Offset: 0x00303661
		public bool Contains(PdfName key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.entries.ContainsKey(key);
		}

		// Token: 0x0600DD2F RID: 56623 RVA: 0x0030547D File Offset: 0x0030367D
		public void Remove(PdfName key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.entries.Remove(key);
		}

		// Token: 0x0600DD30 RID: 56624 RVA: 0x00305499 File Offset: 0x00303699
		public IEnumerator GetEnumerator()
		{
			return this.entries.GetEnumerator();
		}

		// Token: 0x170043B1 RID: 17329
		public PdfObject this[PdfName key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (PdfObject)this.entries[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.entries[key] = value;
			}
		}

		// Token: 0x170043B2 RID: 17330
		// (get) Token: 0x0600DD33 RID: 56627 RVA: 0x003054E4 File Offset: 0x003036E4
		public ICollection Keys
		{
			get
			{
				return this.entries.Keys;
			}
		}

		// Token: 0x170043B3 RID: 17331
		// (get) Token: 0x0600DD34 RID: 56628 RVA: 0x003054F1 File Offset: 0x003036F1
		public ICollection Values
		{
			get
			{
				return this.entries.Values;
			}
		}

		// Token: 0x170043B4 RID: 17332
		// (get) Token: 0x0600DD35 RID: 56629 RVA: 0x003054FE File Offset: 0x003036FE
		public int Count
		{
			get
			{
				return this.entries.Count;
			}
		}

		// Token: 0x0600DD36 RID: 56630 RVA: 0x0030550C File Offset: 0x0030370C
		protected internal override void Write(PdfWriter writer)
		{
			writer.WriteKeywordLine(Keyword.DictionaryBegin);
			foreach (object obj in this.entries)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				writer.Write((PdfName)dictionaryEntry.Key);
				writer.WriteSpace();
				writer.WriteLine((PdfObject)dictionaryEntry.Value);
			}
			writer.WriteKeyword(Keyword.DictionaryEnd);
		}

		// Token: 0x04003EEE RID: 16110
		private Hashtable _entries = new Hashtable();
	}
}
