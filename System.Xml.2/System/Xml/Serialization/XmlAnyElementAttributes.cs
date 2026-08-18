using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000186 RID: 390
	[__DynamicallyInvokable]
	public class XmlAnyElementAttributes : CollectionBase
	{
		// Token: 0x1700058C RID: 1420
		[__DynamicallyInvokable]
		public XmlAnyElementAttribute this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return (XmlAnyElementAttribute)base.List[index];
			}
			[__DynamicallyInvokable]
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000731CA File Offset: 0x000713CA
		[__DynamicallyInvokable]
		public int Add(XmlAnyElementAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000731D8 File Offset: 0x000713D8
		[__DynamicallyInvokable]
		public void Insert(int index, XmlAnyElementAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000731E7 File Offset: 0x000713E7
		[__DynamicallyInvokable]
		public int IndexOf(XmlAnyElementAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x000731F5 File Offset: 0x000713F5
		[__DynamicallyInvokable]
		public bool Contains(XmlAnyElementAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00073203 File Offset: 0x00071403
		[__DynamicallyInvokable]
		public void Remove(XmlAnyElementAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00073211 File Offset: 0x00071411
		[__DynamicallyInvokable]
		public void CopyTo(XmlAnyElementAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00073220 File Offset: 0x00071420
		[__DynamicallyInvokable]
		public XmlAnyElementAttributes()
		{
		}
	}
}
