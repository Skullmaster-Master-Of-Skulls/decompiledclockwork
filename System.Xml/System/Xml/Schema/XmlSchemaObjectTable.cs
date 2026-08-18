using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x0200026B RID: 619
	public class XmlSchemaObjectTable
	{
		// Token: 0x06001CC8 RID: 7368 RVA: 0x00083708 File Offset: 0x00082708
		internal XmlSchemaObjectTable()
		{
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00083726 File Offset: 0x00082726
		internal void Add(XmlQualifiedName name, XmlSchemaObject value)
		{
			this.table.Add(name, value);
			this.entries.Add(new XmlSchemaObjectTable.XmlSchemaObjectEntry(name, value));
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00083748 File Offset: 0x00082748
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

		// Token: 0x06001CCB RID: 7371 RVA: 0x00083798 File Offset: 0x00082798
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

		// Token: 0x06001CCC RID: 7372 RVA: 0x000837DD File Offset: 0x000827DD
		internal void Clear()
		{
			this.table.Clear();
			this.entries.Clear();
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000837F8 File Offset: 0x000827F8
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

		// Token: 0x06001CCE RID: 7374 RVA: 0x00083838 File Offset: 0x00082838
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

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x00083872 File Offset: 0x00082872
		public int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0008387F File Offset: 0x0008287F
		public bool Contains(XmlQualifiedName name)
		{
			return this.table.ContainsKey(name);
		}

		// Token: 0x17000769 RID: 1897
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

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x000838B0 File Offset: 0x000828B0
		public ICollection Names
		{
			get
			{
				return new XmlSchemaObjectTable.NamesCollection(this.entries, this.table.Count);
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x000838C8 File Offset: 0x000828C8
		public ICollection Values
		{
			get
			{
				return new XmlSchemaObjectTable.ValuesCollection(this.entries, this.table.Count);
			}
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000838E0 File Offset: 0x000828E0
		public IDictionaryEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectTable.XSODictionaryEnumerator(this.entries, this.table.Count, XmlSchemaObjectTable.EnumeratorType.DictionaryEntry);
		}

		// Token: 0x040011A4 RID: 4516
		private Dictionary<XmlQualifiedName, XmlSchemaObject> table = new Dictionary<XmlQualifiedName, XmlSchemaObject>();

		// Token: 0x040011A5 RID: 4517
		private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries = new List<XmlSchemaObjectTable.XmlSchemaObjectEntry>();

		// Token: 0x0200026C RID: 620
		internal enum EnumeratorType
		{
			// Token: 0x040011A7 RID: 4519
			Keys,
			// Token: 0x040011A8 RID: 4520
			Values,
			// Token: 0x040011A9 RID: 4521
			DictionaryEntry
		}

		// Token: 0x0200026D RID: 621
		internal struct XmlSchemaObjectEntry
		{
			// Token: 0x06001CD5 RID: 7381 RVA: 0x000838F9 File Offset: 0x000828F9
			public XmlSchemaObjectEntry(XmlQualifiedName name, XmlSchemaObject value)
			{
				this.qname = name;
				this.xso = value;
			}

			// Token: 0x06001CD6 RID: 7382 RVA: 0x00083909 File Offset: 0x00082909
			public XmlSchemaObject IsMatch(string localName, string ns)
			{
				if (localName == this.qname.Name && ns == this.qname.Namespace)
				{
					return this.xso;
				}
				return null;
			}

			// Token: 0x06001CD7 RID: 7383 RVA: 0x00083939 File Offset: 0x00082939
			public void Reset()
			{
				this.qname = null;
				this.xso = null;
			}

			// Token: 0x040011AA RID: 4522
			internal XmlQualifiedName qname;

			// Token: 0x040011AB RID: 4523
			internal XmlSchemaObject xso;
		}

		// Token: 0x0200026E RID: 622
		internal class NamesCollection : ICollection, IEnumerable
		{
			// Token: 0x06001CD8 RID: 7384 RVA: 0x00083949 File Offset: 0x00082949
			internal NamesCollection(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size)
			{
				this.entries = entries;
				this.size = size;
			}

			// Token: 0x1700076C RID: 1900
			// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x0008395F File Offset: 0x0008295F
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x1700076D RID: 1901
			// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00083967 File Offset: 0x00082967
			public object SyncRoot
			{
				get
				{
					return ((ICollection)this.entries).SyncRoot;
				}
			}

			// Token: 0x1700076E RID: 1902
			// (get) Token: 0x06001CDB RID: 7387 RVA: 0x00083974 File Offset: 0x00082974
			public bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.entries).IsSynchronized;
				}
			}

			// Token: 0x06001CDC RID: 7388 RVA: 0x00083984 File Offset: 0x00082984
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

			// Token: 0x06001CDD RID: 7389 RVA: 0x000839DC File Offset: 0x000829DC
			public IEnumerator GetEnumerator()
			{
				return new XmlSchemaObjectTable.XSOEnumerator(this.entries, this.size, XmlSchemaObjectTable.EnumeratorType.Keys);
			}

			// Token: 0x040011AC RID: 4524
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x040011AD RID: 4525
			private int size;
		}

		// Token: 0x0200026F RID: 623
		internal class ValuesCollection : ICollection, IEnumerable
		{
			// Token: 0x06001CDE RID: 7390 RVA: 0x000839F0 File Offset: 0x000829F0
			internal ValuesCollection(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size)
			{
				this.entries = entries;
				this.size = size;
			}

			// Token: 0x1700076F RID: 1903
			// (get) Token: 0x06001CDF RID: 7391 RVA: 0x00083A06 File Offset: 0x00082A06
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x17000770 RID: 1904
			// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00083A0E File Offset: 0x00082A0E
			public object SyncRoot
			{
				get
				{
					return ((ICollection)this.entries).SyncRoot;
				}
			}

			// Token: 0x17000771 RID: 1905
			// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00083A1B File Offset: 0x00082A1B
			public bool IsSynchronized
			{
				get
				{
					return ((ICollection)this.entries).IsSynchronized;
				}
			}

			// Token: 0x06001CE2 RID: 7394 RVA: 0x00083A28 File Offset: 0x00082A28
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

			// Token: 0x06001CE3 RID: 7395 RVA: 0x00083A80 File Offset: 0x00082A80
			public IEnumerator GetEnumerator()
			{
				return new XmlSchemaObjectTable.XSOEnumerator(this.entries, this.size, XmlSchemaObjectTable.EnumeratorType.Values);
			}

			// Token: 0x040011AE RID: 4526
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x040011AF RID: 4527
			private int size;
		}

		// Token: 0x02000270 RID: 624
		internal class XSOEnumerator : IEnumerator
		{
			// Token: 0x06001CE4 RID: 7396 RVA: 0x00083A94 File Offset: 0x00082A94
			internal XSOEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType)
			{
				this.entries = entries;
				this.size = size;
				this.enumType = enumType;
				this.currentIndex = -1;
			}

			// Token: 0x17000772 RID: 1906
			// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x00083AB8 File Offset: 0x00082AB8
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

			// Token: 0x06001CE6 RID: 7398 RVA: 0x00083B60 File Offset: 0x00082B60
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

			// Token: 0x06001CE7 RID: 7399 RVA: 0x00083BD4 File Offset: 0x00082BD4
			public void Reset()
			{
				this.currentIndex = -1;
				this.currentValue = null;
				this.currentKey = null;
			}

			// Token: 0x040011B0 RID: 4528
			private List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries;

			// Token: 0x040011B1 RID: 4529
			private XmlSchemaObjectTable.EnumeratorType enumType;

			// Token: 0x040011B2 RID: 4530
			protected int currentIndex;

			// Token: 0x040011B3 RID: 4531
			protected int size;

			// Token: 0x040011B4 RID: 4532
			protected XmlQualifiedName currentKey;

			// Token: 0x040011B5 RID: 4533
			protected XmlSchemaObject currentValue;
		}

		// Token: 0x02000271 RID: 625
		internal class XSODictionaryEnumerator : XmlSchemaObjectTable.XSOEnumerator, IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06001CE8 RID: 7400 RVA: 0x00083BEB File Offset: 0x00082BEB
			internal XSODictionaryEnumerator(List<XmlSchemaObjectTable.XmlSchemaObjectEntry> entries, int size, XmlSchemaObjectTable.EnumeratorType enumType) : base(entries, size, enumType)
			{
			}

			// Token: 0x17000773 RID: 1907
			// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x00083BF8 File Offset: 0x00082BF8
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

			// Token: 0x17000774 RID: 1908
			// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00083C70 File Offset: 0x00082C70
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

			// Token: 0x17000775 RID: 1909
			// (get) Token: 0x06001CEB RID: 7403 RVA: 0x00083CDC File Offset: 0x00082CDC
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
