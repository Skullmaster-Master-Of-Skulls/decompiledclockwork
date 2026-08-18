using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200062A RID: 1578
	public class OidTokenizer
	{
		// Token: 0x06003582 RID: 13698 RVA: 0x0014BBF1 File Offset: 0x0014ABF1
		public OidTokenizer(string oid)
		{
			this.oid = oid;
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x0014BC00 File Offset: 0x0014AC00
		public bool HasMoreTokens
		{
			get
			{
				return this.index != -1;
			}
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x0014BC10 File Offset: 0x0014AC10
		public string NextToken()
		{
			if (this.index == -1)
			{
				return null;
			}
			int num = this.oid.IndexOf('.', this.index);
			if (num == -1)
			{
				string result = this.oid.Substring(this.index);
				this.index = -1;
				return result;
			}
			string result2 = this.oid.Substring(this.index, num - this.index);
			this.index = num + 1;
			return result2;
		}

		// Token: 0x040023CE RID: 9166
		private string oid;

		// Token: 0x040023CF RID: 9167
		private int index;
	}
}
