using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000193 RID: 403
	[__DynamicallyInvokable]
	public class XmlElementAttributes : CollectionBase
	{
		// Token: 0x170005DE RID: 1502
		[__DynamicallyInvokable]
		public XmlElementAttribute this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return (XmlElementAttribute)base.List[index];
			}
			[__DynamicallyInvokable]
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00076DA9 File Offset: 0x00074FA9
		[__DynamicallyInvokable]
		public int Add(XmlElementAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00076DB7 File Offset: 0x00074FB7
		[__DynamicallyInvokable]
		public void Insert(int index, XmlElementAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00076DC6 File Offset: 0x00074FC6
		[__DynamicallyInvokable]
		public int IndexOf(XmlElementAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00076DD4 File Offset: 0x00074FD4
		[__DynamicallyInvokable]
		public bool Contains(XmlElementAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00076DE2 File Offset: 0x00074FE2
		[__DynamicallyInvokable]
		public void Remove(XmlElementAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00076DF0 File Offset: 0x00074FF0
		[__DynamicallyInvokable]
		public void CopyTo(XmlElementAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00076DFF File Offset: 0x00074FFF
		[__DynamicallyInvokable]
		public XmlElementAttributes()
		{
		}
	}
}
