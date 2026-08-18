using System;
using System.Collections.Specialized;

namespace AutoMapper.Mappers
{
	// Token: 0x02000087 RID: 135
	public class NameValueCollectionMapper : IObjectMapper
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x000116DC File Offset: 0x0000F8DC
		public object Map(ResolutionContext context)
		{
			if (context.SourceValue == null)
			{
				return null;
			}
			NameValueCollection nameValueCollection = new NameValueCollection();
			NameValueCollection nameValueCollection2 = (NameValueCollection)context.SourceValue;
			foreach (string name in nameValueCollection2.AllKeys)
			{
				nameValueCollection.Add(name, nameValueCollection2[name]);
			}
			return nameValueCollection;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00011730 File Offset: 0x0000F930
		public bool IsMatch(TypePair context)
		{
			return context.SourceType == typeof(NameValueCollection) && context.DestinationType == typeof(NameValueCollection);
		}
	}
}
