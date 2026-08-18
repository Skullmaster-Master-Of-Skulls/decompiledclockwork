using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CBD RID: 3261
	[DataContract]
	public abstract class ObjectComparer : SettingsNode, IComparer
	{
		// Token: 0x060079E8 RID: 31208
		public abstract int Compare(object x, object y);
	}
}
