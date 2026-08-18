using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000302 RID: 770
	public class XmlArrayItemAttributes : CollectionBase
	{
		// Token: 0x170008D6 RID: 2262
		public XmlArrayItemAttribute this[int index]
		{
			get
			{
				return (XmlArrayItemAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000AA59A File Offset: 0x000A959A
		public int Add(XmlArrayItemAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000AA5A8 File Offset: 0x000A95A8
		public void Insert(int index, XmlArrayItemAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000AA5B7 File Offset: 0x000A95B7
		public int IndexOf(XmlArrayItemAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000AA5C5 File Offset: 0x000A95C5
		public bool Contains(XmlArrayItemAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000AA5D3 File Offset: 0x000A95D3
		public void Remove(XmlArrayItemAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000AA5E1 File Offset: 0x000A95E1
		public void CopyTo(XmlArrayItemAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
