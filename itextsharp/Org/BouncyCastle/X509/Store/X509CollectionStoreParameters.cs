using System;
using System.Collections;
using System.Text;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x020000F9 RID: 249
	public class X509CollectionStoreParameters : IX509StoreParameters
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x00032B7F File Offset: 0x00031B7F
		public X509CollectionStoreParameters(ICollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.collection = new ArrayList(collection);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00032BA1 File Offset: 0x00031BA1
		public ICollection GetCollection()
		{
			return new ArrayList(this.collection);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00032BB0 File Offset: 0x00031BB0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("X509CollectionStoreParameters: [\n");
			stringBuilder.Append("  collection: " + this.collection + "\n");
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000805 RID: 2053
		private readonly ArrayList collection;
	}
}
