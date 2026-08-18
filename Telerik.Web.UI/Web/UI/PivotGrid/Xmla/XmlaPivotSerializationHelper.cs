using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000742 RID: 1858
	public static class XmlaPivotSerializationHelper
	{
		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x000CF2AD File Offset: 0x000CD4AD
		public static IEnumerable<Type> KnownTypes
		{
			get
			{
				return PivotSerializationHelper.KnownTypes.Concat(XmlaPivotSerializationHelper.XmlaKnownTypes);
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x000CF414 File Offset: 0x000CD614
		private static IEnumerable<Type> XmlaKnownTypes
		{
			get
			{
				yield return typeof(XmlaAggregateDescription);
				yield return typeof(XmlaGroupDescription);
				yield return typeof(XmlaFilterDescription);
				yield return typeof(XmlaLevelFilterDescription);
				yield return typeof(XmlaLevelGroupDescription);
				yield break;
			}
		}
	}
}
