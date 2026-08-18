using System;
using System.Collections.Generic;

namespace Telerik.Charting
{
	// Token: 0x020016F5 RID: 5877
	public class ProductsBL
	{
		// Token: 0x0600E444 RID: 58436 RVA: 0x0032AC78 File Offset: 0x00328E78
		public static List<Product> GetProductsList()
		{
			return new List<Product>
			{
				new Product(0, "Cars", 10000),
				new Product(1, "Bikes", 3000),
				new Product(2, "Trailers", 7000)
			};
		}
	}
}
