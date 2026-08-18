using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000091 RID: 145
	internal class CanonicalXmlNodeList : XmlNodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000EA90 File Offset: 0x0000DA90
		internal CanonicalXmlNodeList()
		{
			this.m_nodeArray = new ArrayList();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000EAA3 File Offset: 0x0000DAA3
		public override XmlNode Item(int index)
		{
			return (XmlNode)this.m_nodeArray[index];
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000EAB6 File Offset: 0x0000DAB6
		public override IEnumerator GetEnumerator()
		{
			return this.m_nodeArray.GetEnumerator();
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000EAC3 File Offset: 0x0000DAC3
		public override int Count
		{
			get
			{
				return this.m_nodeArray.Count;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000EAD0 File Offset: 0x0000DAD0
		public int Add(object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "node");
			}
			return this.m_nodeArray.Add(value);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000EAFB File Offset: 0x0000DAFB
		public void Clear()
		{
			this.m_nodeArray.Clear();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000EB08 File Offset: 0x0000DB08
		public bool Contains(object value)
		{
			return this.m_nodeArray.Contains(value);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000EB16 File Offset: 0x0000DB16
		public int IndexOf(object value)
		{
			return this.m_nodeArray.IndexOf(value);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000EB24 File Offset: 0x0000DB24
		public void Insert(int index, object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			this.m_nodeArray.Insert(index, value);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000EB50 File Offset: 0x0000DB50
		public void Remove(object value)
		{
			this.m_nodeArray.Remove(value);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000EB5E File Offset: 0x0000DB5E
		public void RemoveAt(int index)
		{
			this.m_nodeArray.RemoveAt(index);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000EB6C File Offset: 0x0000DB6C
		public bool IsFixedSize
		{
			get
			{
				return this.m_nodeArray.IsFixedSize;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000EB79 File Offset: 0x0000DB79
		public bool IsReadOnly
		{
			get
			{
				return this.m_nodeArray.IsReadOnly;
			}
		}

		// Token: 0x17000074 RID: 116
		object IList.this[int index]
		{
			get
			{
				return this.m_nodeArray[index];
			}
			set
			{
				if (!(value is XmlNode))
				{
					throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
				}
				this.m_nodeArray[index] = value;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000EBC0 File Offset: 0x0000DBC0
		public void CopyTo(Array array, int index)
		{
			this.m_nodeArray.CopyTo(array, index);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000EBCF File Offset: 0x0000DBCF
		public object SyncRoot
		{
			get
			{
				return this.m_nodeArray.SyncRoot;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000EBDC File Offset: 0x0000DBDC
		public bool IsSynchronized
		{
			get
			{
				return this.m_nodeArray.IsSynchronized;
			}
		}

		// Token: 0x040004F1 RID: 1265
		private ArrayList m_nodeArray;
	}
}
