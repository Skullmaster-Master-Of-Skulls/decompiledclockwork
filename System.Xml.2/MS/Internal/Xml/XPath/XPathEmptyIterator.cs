using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000046 RID: 70
	internal sealed class XPathEmptyIterator : ResetableIterator
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00008280 File Offset: 0x00006480
		private XPathEmptyIterator()
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008288 File Offset: 0x00006488
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000828B File Offset: 0x0000648B
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000828E File Offset: 0x0000648E
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00008291 File Offset: 0x00006491
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008294 File Offset: 0x00006494
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008297 File Offset: 0x00006497
		public override void Reset()
		{
		}

		// Token: 0x040000DF RID: 223
		public static XPathEmptyIterator Instance = new XPathEmptyIterator();
	}
}
