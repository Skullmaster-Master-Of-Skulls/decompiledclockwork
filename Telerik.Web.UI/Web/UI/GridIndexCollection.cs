using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001111 RID: 4369
	[Serializable]
	public class GridIndexCollection : ICollection, IEnumerable, IXmlSerializable
	{
		// Token: 0x0600B2D4 RID: 45780 RVA: 0x0026E185 File Offset: 0x0026C385
		internal GridIndexCollection()
		{
			this.list = new ArrayList();
		}

		// Token: 0x0600B2D5 RID: 45781 RVA: 0x0026E198 File Offset: 0x0026C398
		internal GridIndexCollection(ArrayList list)
		{
			this.list = list;
		}

		// Token: 0x0600B2D6 RID: 45782 RVA: 0x0026E1A7 File Offset: 0x0026C3A7
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x0600B2D7 RID: 45783 RVA: 0x0026E1B6 File Offset: 0x0026C3B6
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x170039EB RID: 14827
		// (get) Token: 0x0600B2D8 RID: 45784 RVA: 0x0026E1C3 File Offset: 0x0026C3C3
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170039EC RID: 14828
		// (get) Token: 0x0600B2D9 RID: 45785 RVA: 0x0026E1D0 File Offset: 0x0026C3D0
		public bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		// Token: 0x170039ED RID: 14829
		// (get) Token: 0x0600B2DA RID: 45786 RVA: 0x0026E1DD File Offset: 0x0026C3DD
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x170039EE RID: 14830
		public string this[int indexOfArray]
		{
			get
			{
				return (string)this.list[indexOfArray];
			}
		}

		// Token: 0x170039EF RID: 14831
		// (get) Token: 0x0600B2DC RID: 45788 RVA: 0x0026E1FD File Offset: 0x0026C3FD
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x0600B2DD RID: 45789 RVA: 0x0026E20A File Offset: 0x0026C40A
		public void Add(string hierarchicalIndex)
		{
			if (string.IsNullOrEmpty(hierarchicalIndex))
			{
				return;
			}
			if (!this.Contains(hierarchicalIndex))
			{
				this.list.Add(hierarchicalIndex);
			}
		}

		// Token: 0x0600B2DE RID: 45790 RVA: 0x0026E22B File Offset: 0x0026C42B
		internal void Remove(string hierarchicalIndex)
		{
			if (this.Contains(hierarchicalIndex))
			{
				this.list.Remove(hierarchicalIndex);
			}
		}

		// Token: 0x0600B2DF RID: 45791 RVA: 0x0026E244 File Offset: 0x0026C444
		public bool Contains(string hierarchicalIndex)
		{
			int num = this.list.IndexOf(hierarchicalIndex);
			return num != -1;
		}

		// Token: 0x0600B2E0 RID: 45792 RVA: 0x0026E265 File Offset: 0x0026C465
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x0600B2E1 RID: 45793 RVA: 0x0026E274 File Offset: 0x0026C474
		public void Add(params int[] indexes)
		{
			if (indexes == null || indexes.Length <= 0)
			{
				throw new GridException("Invalid item hierarchical index");
			}
			string text = indexes[0].ToString();
			for (int i = 1; i < indexes.Length; i++)
			{
				if (i % 2 == 0)
				{
					text = text + "_" + indexes[i].ToString();
				}
				else
				{
					text = text + ":" + indexes[i].ToString();
				}
			}
			this.Add(text);
		}

		// Token: 0x0600B2E2 RID: 45794 RVA: 0x0026E2F0 File Offset: 0x0026C4F0
		public bool ContainsChildIndex(string parentIndex)
		{
			foreach (object obj in this)
			{
				string text = (string)obj;
				if (text.StartsWith(parentIndex + ":"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B2E3 RID: 45795 RVA: 0x0026E358 File Offset: 0x0026C558
		public GridIndexCollection ChildIndexes(string parentIndex)
		{
			GridIndexCollection gridIndexCollection = new GridIndexCollection(new ArrayList());
			foreach (object obj in this)
			{
				string text = (string)obj;
				if (text.StartsWith(parentIndex + ":"))
				{
					gridIndexCollection.Add(text);
				}
			}
			return gridIndexCollection;
		}

		// Token: 0x0600B2E4 RID: 45796 RVA: 0x0026E3CC File Offset: 0x0026C5CC
		public GridIndexCollection ChildIndexes(string parentIndex, int detailTableIndex)
		{
			GridIndexCollection gridIndexCollection = new GridIndexCollection(new ArrayList());
			foreach (object obj in this)
			{
				string text = (string)obj;
				if (text.StartsWith(parentIndex + ":" + detailTableIndex.ToString() + "_"))
				{
					gridIndexCollection.Add(text);
				}
			}
			return gridIndexCollection;
		}

		// Token: 0x0600B2E5 RID: 45797 RVA: 0x0026E44C File Offset: 0x0026C64C
		public void RemoveChildIndexes(string parentIndex, int detailTableIndex)
		{
			new GridIndexCollection(new ArrayList());
			string value = "";
			if (!string.IsNullOrEmpty(parentIndex))
			{
				value = parentIndex + ":" + detailTableIndex.ToString() + "_";
			}
			for (int i = this.Count - 1; i >= 0; i--)
			{
				string text = this[i];
				if (text.StartsWith(value))
				{
					this.Remove(text);
				}
			}
		}

		// Token: 0x0600B2E6 RID: 45798 RVA: 0x0026E4B5 File Offset: 0x0026C6B5
		internal ArrayList GetArrayList()
		{
			return this.list;
		}

		// Token: 0x0600B2E7 RID: 45799 RVA: 0x0026E4BD File Offset: 0x0026C6BD
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x0600B2E8 RID: 45800 RVA: 0x0026E4C0 File Offset: 0x0026C6C0
		public void ReadXml(XmlReader reader)
		{
			this.list = new ArrayList();
			reader.Read();
			reader.ReadStartElement("Data");
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				reader.ReadStartElement("Itm");
				object value = reader.ReadContentAsObject();
				this.list.Add(value);
				reader.ReadEndElement();
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600B2E9 RID: 45801 RVA: 0x0026E524 File Offset: 0x0026C724
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Data");
			foreach (object value in this.list)
			{
				writer.WriteStartElement("Itm");
				writer.WriteValue(value);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x04002F21 RID: 12065
		private ArrayList list;
	}
}
