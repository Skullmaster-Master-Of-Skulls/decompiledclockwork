using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000115 RID: 277
	[PersistChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ClientDataSourceAggregatesCollection : StronglyTypedStateManagedCollection<ClientDataSourceAggregate>
	{
	}
}
