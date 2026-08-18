using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200011C RID: 284
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(true)]
	public class ClientDataSourceGroupExpressionCollection : StronglyTypedStateManagedCollection<ClientDataSourceGroupExpression>
	{
	}
}
