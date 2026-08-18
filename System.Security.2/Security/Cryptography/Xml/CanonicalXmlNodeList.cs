using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200002C RID: 44
	internal class CanonicalXmlNodeList : XmlNodeList, IList, ICollection, IEnumerable
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00006120 File Offset: 0x00004320
		internal CanonicalXmlNodeList()
		{
			this.m_nodeArray = new ArrayList();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006133 File Offset: 0x00004333
		public override XmlNode Item(int index)
		{
			return (XmlNode)this.m_nodeArray[index];
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006146 File Offset: 0x00004346
		public override IEnumerator GetEnumerator()
		{
			return this.m_nodeArray.GetEnumerator();
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006153 File Offset: 0x00004353
		public override int Count
		{
			get
			{
				return this.m_nodeArray.Count;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006160 File Offset: 0x00004360
		public int Add(object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "node");
			}
			return this.m_nodeArray.Add(value);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000618B File Offset: 0x0000438B
		public void Clear()
		{
			this.m_nodeArray.Clear();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006198 File Offset: 0x00004398
		public bool Contains(object value)
		{
			return this.m_nodeArray.Contains(value);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000061A6 File Offset: 0x000043A6
		public int IndexOf(object value)
		{
			return this.m_nodeArray.IndexOf(value);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000061B4 File Offset: 0x000043B4
		public void Insert(int index, object value)
		{
			if (!(value is XmlNode))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_IncorrectObjectType"), "value");
			}
			this.m_nodeArray.Insert(index, value);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000061E0 File Offset: 0x000043E0
		public void Remove(object value)
		{
			this.m_nodeArray.Remove(value);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000061EE File Offset: 0x000043EE
		public void RemoveAt(int index)
		{
			this.m_nodeArray.RemoveAt(index);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000061FC File Offset: 0x000043FC
		public bool IsFixedSize
		{
			get
			{
				return this.m_nodeArray.IsFixedSize;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006209 File Offset: 0x00004409
		public bool IsReadOnly
		{
			get
			{
				return this.m_nodeArray.IsReadOnly;
			}
		}

		// Token: 0x17000030 RID: 48
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

		// Token: 0x06000129 RID: 297 RVA: 0x00006250 File Offset: 0x00004450
		public void CopyTo(Array array, int index)
		{
			this.m_nodeArray.CopyTo(array, index);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000625F File Offset: 0x0000445F
		public object SyncRoot
		{
			get
			{
				return this.m_nodeArray.SyncRoot;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000626C File Offset: 0x0000446C
		public bool IsSynchronized
		{
			get
			{
				return this.m_nodeArray.IsSynchronized;
			}
		}

		// Token: 0x040003A0 RID: 928
		private ArrayList m_nodeArray;
	}
}
