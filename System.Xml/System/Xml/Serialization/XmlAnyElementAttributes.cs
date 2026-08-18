using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002FF RID: 767
	public class XmlAnyElementAttributes : CollectionBase
	{
		// Token: 0x170008C8 RID: 2248
		public XmlAnyElementAttribute this[int index]
		{
			get
			{
				return (XmlAnyElementAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000AA39B File Offset: 0x000A939B
		public int Add(XmlAnyElementAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000AA3A9 File Offset: 0x000A93A9
		public void Insert(int index, XmlAnyElementAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000AA3B8 File Offset: 0x000A93B8
		public int IndexOf(XmlAnyElementAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000AA3C6 File Offset: 0x000A93C6
		public bool Contains(XmlAnyElementAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000AA3D4 File Offset: 0x000A93D4
		public void Remove(XmlAnyElementAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000AA3E2 File Offset: 0x000A93E2
		public void CopyTo(XmlAnyElementAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
