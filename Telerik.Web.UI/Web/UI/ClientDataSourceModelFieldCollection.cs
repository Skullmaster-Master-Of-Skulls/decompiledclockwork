using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000112 RID: 274
	[PersistChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ClientDataSourceModelFieldCollection : StronglyTypedStateManagedCollection<ClientDataSourceModelField>
	{
	}
}
