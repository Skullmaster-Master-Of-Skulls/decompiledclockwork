using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000187 RID: 391
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public class XmlArrayAttribute : Attribute
	{
		// Token: 0x060019B5 RID: 6581 RVA: 0x00073228 File Offset: 0x00071428
		[__DynamicallyInvokable]
		public XmlArrayAttribute()
		{
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00073237 File Offset: 0x00071437
		[__DynamicallyInvokable]
		public XmlArrayAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0007324D File Offset: 0x0007144D
		// (set) Token: 0x060019B8 RID: 6584 RVA: 0x00073263 File Offset: 0x00071463
		[__DynamicallyInvokable]
		public string ElementName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			[__DynamicallyInvokable]
			set
			{
				this.elementName = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x0007326C File Offset: 0x0007146C
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x00073274 File Offset: 0x00071474
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x0007327D File Offset: 0x0007147D
		// (set) Token: 0x060019BC RID: 6588 RVA: 0x00073285 File Offset: 0x00071485
		[__DynamicallyInvokable]
		public bool IsNullable
		{
			[__DynamicallyInvokable]
			get
			{
				return this.nullable;
			}
			[__DynamicallyInvokable]
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x0007328E File Offset: 0x0007148E
		// (set) Token: 0x060019BE RID: 6590 RVA: 0x00073296 File Offset: 0x00071496
		[__DynamicallyInvokable]
		public XmlSchemaForm Form
		{
			[__DynamicallyInvokable]
			get
			{
				return this.form;
			}
			[__DynamicallyInvokable]
			set
			{
				this.form = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x0007329F File Offset: 0x0007149F
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x000732A7 File Offset: 0x000714A7
		[__DynamicallyInvokable]
		public int Order
		{
			[__DynamicallyInvokable]
			get
			{
				return this.order;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("XmlDisallowNegativeValues"), "Order");
				}
				this.order = value;
			}
		}

		// Token: 0x04000BB5 RID: 2997
		private string elementName;

		// Token: 0x04000BB6 RID: 2998
		private string ns;

		// Token: 0x04000BB7 RID: 2999
		private bool nullable;

		// Token: 0x04000BB8 RID: 3000
		private XmlSchemaForm form;

		// Token: 0x04000BB9 RID: 3001
		private int order = -1;
	}
}
