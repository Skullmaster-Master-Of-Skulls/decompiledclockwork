using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000189 RID: 393
	[__DynamicallyInvokable]
	public class XmlArrayItemAttributes : CollectionBase
	{
		// Token: 0x1700059A RID: 1434
		[__DynamicallyInvokable]
		public XmlArrayItemAttribute this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return (XmlArrayItemAttribute)base.List[index];
			}
			[__DynamicallyInvokable]
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000733C9 File Offset: 0x000715C9
		[__DynamicallyInvokable]
		public int Add(XmlArrayItemAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000733D7 File Offset: 0x000715D7
		[__DynamicallyInvokable]
		public void Insert(int index, XmlArrayItemAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000733E6 File Offset: 0x000715E6
		[__DynamicallyInvokable]
		public int IndexOf(XmlArrayItemAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000733F4 File Offset: 0x000715F4
		[__DynamicallyInvokable]
		public bool Contains(XmlArrayItemAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00073402 File Offset: 0x00071602
		[__DynamicallyInvokable]
		public void Remove(XmlArrayItemAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00073410 File Offset: 0x00071610
		[__DynamicallyInvokable]
		public void CopyTo(XmlArrayItemAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0007341F File Offset: 0x0007161F
		[__DynamicallyInvokable]
		public XmlArrayItemAttributes()
		{
		}
	}
}
