using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x020002A5 RID: 677
	public class XmlSchemaObjectTable
	{
		// Token: 0x0600276A RID: 10090 RVA: 0x000CF8E0 File Offset: 0x000CDAE0
		internal XmlSchemaObjectTable()
		{
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000CF8FE File Offset: 0x000CDAFE
		internal void Add(XmlQualifiedName name, XmlSchemaObject value)
		{
			this.table.Add(name, value);
			this.entries.Add(new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value));
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000CF920 File Offset: 0x000CDB20
		internal void Insert(XmlQualifiedName name, XmlSchemaObject value)
		{
			XmlSchemaObject xso = null;
			if (this.table.TryGetValue(name, out xso))
			{
				this.table[name] = value;
				int index = this.FindIndexByValue(xso);
				this.entries[index] = new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value);
				return;
			}
			this.Add(name, value);
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000CF970 File Offset: 0x000CDB70
		internal void Replace(XmlQualifiedName name, XmlSchemaObject value)
		{
			XmlSchemaObject xso;
			if (this.table.TryGetValue(name, out xso))
			{
				this.table[name] = value;
				int index = this.FindIndexByValue(xso);
				this.entries[index] = new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value);
			}
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000CF9B5 File Offset: 0x000CDBB5
		internal void Clear()
		{
			this.table.Clear();
			this.entries.Clear();
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000CF9D0 File Offset: 0x000CDBD0
		internal void Remove(XmlQualifiedName name)
		{
			XmlSchemaObject xso;
			if (this.table.TryGetValue(name, out xso))
			{
				this.table.Remove(name);
				int index = this.FindIndexByValue(xso);
				this.entries.RemoveAt(index);
			}
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000CFA10 File Offset: 0x000CDC10
		private int FindIndexByValue(XmlSchemaObject xso)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].xso == xso)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x000CFA4A File Offset: 0x000CDC4A
		public int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000CFA57 File Offset: 0x000CDC57
		public bool Contains(XmlQualifiedName name)
		{
			return this.table.ContainsKey(name);
		}

		// Token: 0x17000908 RID: 2312
		public XmlSchemaObject this[XmlQualifiedName name]
		{
			get
			{
				XmlSchemaObject result;
				if (this.table.TryGetValue(name, out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x000CFA88 File Offset: 0x000CDC88
		public ICollection Names
		{
			get
			{
				return new XmlSchemaObjectTable.NamesCollection(this.entries, this.table.Count);
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x000CFAA0 File Offset: 0x000CDCA0
		public ICollection Values
		{
			get
			{
				return new XmlSchemaObjectTable.ValuesCollection(this.entries, this.table.Count);
			}
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000CFAB8 File Offset: 0x000CDCB8
		public IDictionaryEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectTable.XSODictionaryEnumerator(this.entries, this.table.Count, XmlSchemaObjectTable.EnumeratorType.DictionaryEntry);
		}

		// Token: 0x04001126 RID: 4390
		private Dictionary<XmlQualifiedName, XmlSchemaObject> table = new Dictionary<XmlQualifiedName, XmlSchemaObject>();

		// Token: 0x04001127 RID: 4391
		private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries = new List<XmlSchemaObjectTable.XmlSchemaObjectEntry>();

		// Token: 0x020004A5 RID: 1189
		internal enum EnumeratorType
		{
			// Token: 0x04001F05 RID: 7941
			Keys,
			// Token: 0x04001F06 RID: 7942
			Values,
			// Token: 0x04001F07 RID: 7943
			DictionaryEntry
		}

		// Token: 0x020004A6 RID: 1190
		internal struct XmlSchemaObjectEntry
		{
			// Token: 0x0600316D RID: 12653 RVA: 0x0011FED7 File Offset: 0x0011E0D7
			public XmlSchemaObjectEntry(XmlQualifiedName name, XmlSchemaObject value)
			{
				this.qname = name;
				this.xso = value;
			}

			// Token: 0x0600316E RID: 12654 RVA: 0x0011FEE7 File Offset: 0x0011E0E7
			public XmlSchemaObject IsMatch(string localName, string ns)
			{
				if (localName == this.qname.Name && ns == this.qname.Namespace)
				{
					return this.xso;
				}
				return null;
			}

			// Token: 0x0600316F RID: 12655 RVA: 0x0011FF17 File Offset: 0x0011E117
			public void Reset()
			{
				this.qname = null;
				this.xso = null;
			}

			// Token: 0x04001F08 RID: 7944
			internal XmlQualifiedName qname;

			// Token: 0x04001F09 RID: 7945
			internal XmlSchemaObject xso;
		}

		// Token: 0x020004A7 RID: 1191
		internal class NamesCollection : ICollection, IEnumerable
		{
			// Token: 0x06003170 RID: 12656 RVA: 0x0011FF27 File Offset: 0x0011E127
			internal NamesCollection(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size)
			{
				this.entries = entries;
				this.size = size;
			}

			// Token: 0x17000A6D RID: 2669
			// (get) Token: 0x06003171 RID: 12657 RVA: 0x0011FF3D File Offset: 0x0011E13D
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x17000A6E RID: 2670
			// (get) Token: 0x06003172 RID: 12658 RVA: 0x0011FF45 File Offset: 0x0011E145
			public object SyncRoot
			{
				get
				{
					return ((ICollection)this.entries).SyncRoot;
				}
			}

			// Token: 0x17000A6F RID: 2671
			// (get) Token: 0x06003173 RID: 12659 RVA: 0x0011FF52 File Offset: 0x0011E152
			public bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.entries).IsSynchronized;
				}
			}

			// Token: 0x06003174 RID: 12660 RVA: 0x0011FF60 File Offset: 0x0011E160
			public void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				for (int i = 0; i < this.size; i++)
				{
					array.SetValue(this.entries[i].qname, arrayIndex++);
				}
			}

			// Token: 0x06003175 RID: 12661 RVA: 0x0011FFB8 File Offset: 0x0011E1B8
			public IEnumerator GetEnumerator()
			{
				return new XmlSchemaObjectTable.XSOEnumerator(this.entries, this.size, XmlSchemaObjectTable.EnumeratorType.Keys);
			}

			// Token: 0x04001F0A RID: 7946
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04001F0B RID: 7947
			private int size;
		}

		// Token: 0x020004A8 RID: 1192
		internal class ValuesCollection : ICollection, IEnumerable
		{
			// Token: 0x06003176 RID: 12662 RVA: 0x0011FFCC File Offset: 0x0011E1CC
			internal ValuesCollection(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size)
			{
				this.entries = entries;
				this.size = size;
			}

			// Token: 0x17000A70 RID: 2672
			// (get) Token: 0x06003177 RID: 12663 RVA: 0x0011FFE2 File Offset: 0x0011E1E2
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x17000A71 RID: 2673
			// (get) Token: 0x06003178 RID: 12664 RVA: 0x0011FFEA File Offset: 0x0011E1EA
			public object SyncRoot
			{
				get
				{
					return ((ICollection)this.entries).SyncRoot;
				}
			}

			// Token: 0x17000A72 RID: 2674
			// (get) Token: 0x06003179 RID: 12665 RVA: 0x0011FFF7 File Offset: 0x0011E1F7
			public bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.entries).IsSynchronized;
				}
			}

			// Token: 0x0600317A RID: 12666 RVA: 0x00120004 File Offset: 0x0011E204
			public void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				for (int i = 0; i < this.size; i++)
				{
					array.SetValue(this.entries[i].xso, arrayIndex++);
				}
			}

			// Token: 0x0600317B RID: 12667 RVA: 0x0012005C File Offset: 0x0011E25C
			public IEnumerator GetEnumerator()
			{
				return new XmlSchemaObjectTable.XSOEnumerator(this.entries, this.size, XmlSchemaObjectTable.EnumeratorType.Values);
			}

			// Token: 0x04001F0C RID: 7948
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04001F0D RID: 7949
			private int size;
		}

		// Token: 0x020004A9 RID: 1193
		internal class XSOEnumerator : IEnumerator
		{
			// Token: 0x0600317C RID: 12668 RVA: 0x00120070 File Offset: 0x0011E270
			internal XSOEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType)
			{
				this.entries = entries;
				this.size = size;
				this.enumType = enumType;
				this.currentIndex = -1;
			}

			// Token: 0x17000A73 RID: 2675
			// (get) Token: 0x0600317D RID: 12669 RVA: 0x00120094 File Offset: 0x0011E294
			public object Current
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumNotStarted", new object[]
						{
							string.Empty
						}));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumFinished", new object[]
						{
							string.Empty
						}));
					}
					switch (this.enumType)
					{
					case XmlSchemaObjectTable.EnumeratorType.Keys:
						return this.currentKey;
					case XmlSchemaObjectTable.EnumeratorType.Values:
						return this.currentValue;
					case XmlSchemaObjectTable.EnumeratorType.DictionaryEntry:
						return new DictionaryEntry(this.currentKey, this.currentValue);
					default:
						return null;
					}
				}
			}

			// Token: 0x0600317E RID: 12670 RVA: 0x00120138 File Offset: 0x0011E338
			public bool MoveNext()
			{
				if (this.currentIndex >= this.size - 1)
				{
					this.currentValue = null;
					this.currentKey = null;
					return false;
				}
				this.currentIndex++;
				this.currentValue = this.entries[this.currentIndex].xso;
				this.currentKey = this.entries[this.currentIndex].qname;
				return true;
			}

			// Token: 0x0600317F RID: 12671 RVA: 0x001201AC File Offset: 0x0011E3AC
			public void Reset()
			{
				this.currentIndex = -1;
				this.currentValue = null;
				this.currentKey = null;
			}

			// Token: 0x04001F0E RID: 7950
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x04001F0F RID: 7951
			private XmlSchemaObjectTable.EnumeratorType enumType;

			// Token: 0x04001F10 RID: 7952
			protected int currentIndex;

			// Token: 0x04001F11 RID: 7953
			protected int size;

			// Token: 0x04001F12 RID: 7954
			protected XmlQualifiedName currentKey;

			// Token: 0x04001F13 RID: 7955
			protected XmlSchemaObject currentValue;
		}

		// Token: 0x020004AA RID: 1194
		internal class XSODictionaryEnumerator : XmlSchemaObjectTable.XSOEnumerator, IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06003180 RID: 12672 RVA: 0x001201C3 File Offset: 0x0011E3C3
			internal XSODictionaryEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType) : base(entries, size, enumType)
			{
			}

			// Token: 0x17000A74 RID: 2676
			// (get) Token: 0x06003181 RID: 12673 RVA: 0x001201D0 File Offset: 0x0011E3D0
			public DictionaryEntry Entry
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumNotStarted", new object[]
						{
							string.Empty
						}));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumFinished", new object[]
						{
							string.Empty
						}));
					}
					return new DictionaryEntry(this.currentKey, this.currentValue);
				}
			}

			// Token: 0x17000A75 RID: 2677
			// (get) Token: 0x06003182 RID: 12674 RVA: 0x00120244 File Offset: 0x0011E444
			public object Key
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumNotStarted", new object[]
						{
							string.Empty
						}));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumFinished", new object[]
						{
							string.Empty
						}));
					}
					return this.currentKey;
				}
			}

			// Token: 0x17000A76 RID: 2678
			// (get) Token: 0x06003183 RID: 12675 RVA: 0x001202AC File Offset: 0x0011E4AC
			public object Value
			{
				get
				{
					if (this.currentIndex == -1)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumNotStarted", new object[]
						{
							string.Empty
						}));
					}
					if (this.currentIndex >= this.size)
					{
						throw new InvalidOperationException(Res.GetString("Sch_EnumFinished", new object[]
						{
							string.Empty
						}));
					}
					return this.currentValue;
				}
			}
		}
	}
}
