using System;
using System.Collections.Generic;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000F6 RID: 246
	public class IndexedProtocolEndpointDictionary : SortedList<int, IndexedProtocolEndpoint>
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001AA30 File Offset: 0x00018C30
		public IndexedProtocolEndpoint Default
		{
			get
			{
				IndexedProtocolEndpoint indexedProtocolEndpoint = null;
				foreach (KeyValuePair<int, IndexedProtocolEndpoint> keyValuePair in this)
				{
					bool? isDefault = keyValuePair.Value.IsDefault;
					bool flag = true;
					if (isDefault.GetValueOrDefault() == flag & isDefault != null)
					{
						return keyValuePair.Value;
					}
					if (keyValuePair.Value.IsDefault == null && indexedProtocolEndpoint == null)
					{
						indexedProtocolEndpoint = keyValuePair.Value;
					}
				}
				if (indexedProtocolEndpoint != null)
				{
					return indexedProtocolEndpoint;
				}
				if (base.Count > 0)
				{
					return base[base.Keys[0]];
				}
				return null;
			}
		}
	}
}
