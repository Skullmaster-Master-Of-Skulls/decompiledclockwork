using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200030C RID: 780
	public class XmlElementAttributes : CollectionBase
	{
		// Token: 0x17000919 RID: 2329
		public XmlElementAttribute this[int index]
		{
			get
			{
				return (XmlElementAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000ADF1B File Offset: 0x000ACF1B
		public int Add(XmlElementAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000ADF29 File Offset: 0x000ACF29
		public void Insert(int index, XmlElementAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000ADF38 File Offset: 0x000ACF38
		public int IndexOf(XmlElementAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000ADF46 File Offset: 0x000ACF46
		public bool Contains(XmlElementAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000ADF54 File Offset: 0x000ACF54
		public void Remove(XmlElementAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000ADF62 File Offset: 0x000ACF62
		public void CopyTo(XmlElementAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
