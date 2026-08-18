using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.GoogleBooks;
using TechnoPro.Common.ICore.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.AlternativeFormat.BookSearch
{
	// Token: 0x02000160 RID: 352
	public static class BookSearchProviderFactory
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x00074FB4 File Offset: 0x000731B4
		public static IBookSearchProvider GetBookSearchProvider(eBookSearchProviderName providerName, OperationContext opContext)
		{
			IBookSearchProvider result;
			if (providerName != eBookSearchProviderName.Google)
			{
				if (providerName != eBookSearchProviderName.SchoolBookProvider)
				{
					result = null;
				}
				else
				{
					result = new SchoolBookSearchProvider(opContext);
				}
			}
			else
			{
				result = new GoogleBookSearchProvider(opContext);
			}
			return result;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00074FE8 File Offset: 0x000731E8
		public static IList<IBookSearchProvider> GetBookSearchProviderList(OperationContext opContext, eBookSearchProviderType searchProviderType)
		{
			switch (searchProviderType)
			{
			case eBookSearchProviderType.ExternalOnly:
				return new List<IBookSearchProvider>
				{
					BookSearchProviderFactory.GetBookSearchProvider(eBookSearchProviderName.Google, opContext)
				};
			case eBookSearchProviderType.LocalOnly:
				return new List<IBookSearchProvider>
				{
					BookSearchProviderFactory.GetBookSearchProvider(eBookSearchProviderName.SchoolBookProvider, opContext)
				};
			}
			return new List<IBookSearchProvider>
			{
				BookSearchProviderFactory.GetBookSearchProvider(eBookSearchProviderName.Google, opContext),
				BookSearchProviderFactory.GetBookSearchProvider(eBookSearchProviderName.SchoolBookProvider, opContext)
			};
		}
	}
}
