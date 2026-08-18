using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002FE RID: 766
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	public class XmlAnyElementAttribute : Attribute
	{
		// Token: 0x060023D6 RID: 9174 RVA: 0x000AA2C7 File Offset: 0x000A92C7
		public XmlAnyElementAttribute()
		{
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000AA2D6 File Offset: 0x000A92D6
		public XmlAnyElementAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000AA2EC File Offset: 0x000A92EC
		public XmlAnyElementAttribute(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
			this.nsSpecified = true;
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x000AA310 File Offset: 0x000A9310
		// (set) Token: 0x060023DA RID: 9178 RVA: 0x000AA326 File Offset: 0x000A9326
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x000AA32F File Offset: 0x000A932F
		// (set) Token: 0x060023DC RID: 9180 RVA: 0x000AA337 File Offset: 0x000A9337
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
				this.nsSpecified = true;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x000AA347 File Offset: 0x000A9347
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x000AA34F File Offset: 0x000A934F
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("XmlDisallowNegativeValues"), "Order");
				}
				this.order = value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x000AA371 File Offset: 0x000A9371
		internal bool NamespaceSpecified
		{
			get
			{
				return this.nsSpecified;
			}
		}

		// Token: 0x0400153D RID: 5437
		private string name;

		// Token: 0x0400153E RID: 5438
		private string ns;

		// Token: 0x0400153F RID: 5439
		private int order = -1;

		// Token: 0x04001540 RID: 5440
		private bool nsSpecified;
	}
}
