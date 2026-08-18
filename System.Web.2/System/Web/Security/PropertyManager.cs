using System;
using System.Configuration.Provider;
using System.DirectoryServices;

namespace System.Web.Security
{
	// Token: 0x020005C9 RID: 1481
	internal static class PropertyManager
	{
		// Token: 0x06004B3C RID: 19260 RVA: 0x000FF0D8 File Offset: 0x000FD2D8
		public static object GetPropertyValue(DirectoryEntry directoryEntry, string propertyName)
		{
			if (directoryEntry.Properties[propertyName].Count != 0)
			{
				return directoryEntry.Properties[propertyName].Value;
			}
			if (directoryEntry.Properties["distinguishedName"].Count != 0)
			{
				throw new ProviderException(SR.GetString("ADMembership_Property_not_found_on_object", new object[]
				{
					propertyName,
					(string)directoryEntry.Properties["distinguishedName"].Value
				}));
			}
			throw new ProviderException(SR.GetString("ADMembership_Property_not_found", new object[]
			{
				propertyName
			}));
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x000FF174 File Offset: 0x000FD374
		public static object GetSearchResultPropertyValue(SearchResult res, string propertyName)
		{
			ResultPropertyValueCollection resultPropertyValueCollection = res.Properties[propertyName];
			if (resultPropertyValueCollection == null || resultPropertyValueCollection.Count < 1)
			{
				throw new ProviderException(SR.GetString("ADMembership_Property_not_found", new object[]
				{
					propertyName
				}));
			}
			return resultPropertyValueCollection[0];
		}
	}
}
