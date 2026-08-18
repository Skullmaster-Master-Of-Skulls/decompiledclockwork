using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000522 RID: 1314
	internal class XPathToken
	{
		// Token: 0x060031F5 RID: 12789 RVA: 0x000BFE5F File Offset: 0x000BE05F
		internal XPathToken()
		{
			this.tokenID = XPathTokenID.Unknown;
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000BFE6E File Offset: 0x000BE06E
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000BFE76 File Offset: 0x000BE076
		internal double Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000BFE7E File Offset: 0x000BE07E
		internal string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000BFE86 File Offset: 0x000BE086
		internal XPathTokenID TokenID
		{
			get
			{
				return this.tokenID;
			}
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000BFE8E File Offset: 0x000BE08E
		internal void Clear()
		{
			this.number = double.NaN;
			this.prefix = string.Empty;
			this.name = string.Empty;
			this.tokenID = XPathTokenID.Unknown;
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000BFEBC File Offset: 0x000BE0BC
		internal void Set(XPathTokenID id)
		{
			this.Clear();
			this.tokenID = id;
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000BFECB File Offset: 0x000BE0CB
		internal void Set(XPathTokenID id, double number)
		{
			this.Set(id);
			this.number = number;
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000BFEDB File Offset: 0x000BE0DB
		internal void Set(XPathTokenID id, string name)
		{
			this.Clear();
			this.tokenID = id;
			this.name = name;
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000BFEF1 File Offset: 0x000BE0F1
		internal void Set(XPathTokenID id, XPathParser.QName qname)
		{
			this.Set(id, qname.Name);
			this.prefix = qname.Prefix;
		}

		// Token: 0x040026CF RID: 9935
		private string name;

		// Token: 0x040026D0 RID: 9936
		private double number;

		// Token: 0x040026D1 RID: 9937
		private string prefix;

		// Token: 0x040026D2 RID: 9938
		private XPathTokenID tokenID;
	}
}
