using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000117 RID: 279
	[ParseChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ClientDataSourceFilterBaseCollection : StronglyTypedStateManagedCollection<ClientDataSourceFilterBase>
	{
	}
}
