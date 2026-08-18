using System;
using System.Collections;

namespace Telerik.Pdf
{
	// Token: 0x020015FC RID: 5628
	internal class BfEntryList : IEnumerable
	{
		// Token: 0x0600DB70 RID: 56176 RVA: 0x0030021B File Offset: 0x002FE41B
		public void Add(BfEntry entry)
		{
			this.entries.Add(entry);
		}

		// Token: 0x17004326 RID: 17190
		public BfEntry this[int index]
		{
			get
			{
				return (BfEntry)this.entries[index];
			}
		}

		// Token: 0x17004327 RID: 17191
		// (get) Token: 0x0600DB72 RID: 56178 RVA: 0x0030023D File Offset: 0x002FE43D
		public int Count
		{
			get
			{
				return this.entries.Count;
			}
		}

		// Token: 0x17004328 RID: 17192
		// (get) Token: 0x0600DB73 RID: 56179 RVA: 0x0030024C File Offset: 0x002FE44C
		public int NumRanges
		{
			get
			{
				int num = 0;
				foreach (object obj in this.entries)
				{
					BfEntry bfEntry = (BfEntry)obj;
					if (bfEntry.IsRange)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17004329 RID: 17193
		// (get) Token: 0x0600DB74 RID: 56180 RVA: 0x003002B0 File Offset: 0x002FE4B0
		public BfEntry[] Ranges
		{
			get
			{
				ArrayList arrayList = new ArrayList(this.NumRanges);
				foreach (object obj in this)
				{
					BfEntry bfEntry = (BfEntry)obj;
					if (bfEntry.IsRange)
					{
						arrayList.Add(bfEntry);
					}
				}
				return (BfEntry[])arrayList.ToArray(typeof(BfEntry));
			}
		}

		// Token: 0x1700432A RID: 17194
		// (get) Token: 0x0600DB75 RID: 56181 RVA: 0x00300330 File Offset: 0x002FE530
		public int NumChars
		{
			get
			{
				return this.entries.Count - this.NumRanges;
			}
		}

		// Token: 0x1700432B RID: 17195
		// (get) Token: 0x0600DB76 RID: 56182 RVA: 0x00300344 File Offset: 0x002FE544
		public BfEntry[] Chars
		{
			get
			{
				ArrayList arrayList = new ArrayList(this.NumChars);
				foreach (object obj in this)
				{
					BfEntry bfEntry = (BfEntry)obj;
					if (bfEntry.IsChar)
					{
						arrayList.Add(bfEntry);
					}
				}
				return (BfEntry[])arrayList.ToArray(typeof(BfEntry));
			}
		}

		// Token: 0x0600DB77 RID: 56183 RVA: 0x003003C4 File Offset: 0x002FE5C4
		public IEnumerator GetEnumerator()
		{
			return ArrayList.ReadOnly(this.entries).GetEnumerator();
		}

		// Token: 0x04003D5F RID: 15711
		private ArrayList entries = new ArrayList();
	}
}
