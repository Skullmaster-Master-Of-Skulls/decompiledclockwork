using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x020006EB RID: 1771
	[Obsolete("Not used. Obsoleted after 2013.Q2.SP1. Use Collection<T> instead.")]
	[CollectionDataContract]
	public sealed class GenericDescriptionCollection<T> : Collection<T> where T : class
	{
	}
}
