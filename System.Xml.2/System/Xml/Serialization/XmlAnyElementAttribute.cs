using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000185 RID: 389
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	[__DynamicallyInvokable]
	public class XmlAnyElementAttribute : Attribute
	{
		// Token: 0x060019A2 RID: 6562 RVA: 0x000730F6 File Offset: 0x000712F6
		[__DynamicallyInvokable]
		public XmlAnyElementAttribute()
		{
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00073105 File Offset: 0x00071305
		[__DynamicallyInvokable]
		public XmlAnyElementAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0007311B File Offset: 0x0007131B
		[__DynamicallyInvokable]
		public XmlAnyElementAttribute(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
			this.nsSpecified = true;
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x0007313F File Offset: 0x0007133F
		// (set) Token: 0x060019A6 RID: 6566 RVA: 0x00073155 File Offset: 0x00071355
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			[__DynamicallyInvokable]
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x0007315E File Offset: 0x0007135E
		// (set) Token: 0x060019A8 RID: 6568 RVA: 0x00073166 File Offset: 0x00071366
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
				this.nsSpecified = true;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x00073176 File Offset: 0x00071376
		// (set) Token: 0x060019AA RID: 6570 RVA: 0x0007317E File Offset: 0x0007137E
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

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x000731A0 File Offset: 0x000713A0
		internal bool NamespaceSpecified
		{
			get
			{
				return this.nsSpecified;
			}
		}

		// Token: 0x04000BB1 RID: 2993
		private string name;

		// Token: 0x04000BB2 RID: 2994
		private string ns;

		// Token: 0x04000BB3 RID: 2995
		private int order = -1;

		// Token: 0x04000BB4 RID: 2996
		private bool nsSpecified;
	}
}
