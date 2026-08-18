using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D73 RID: 3443
	[DataContract]
	public sealed class XmlaGroupDescription : OlapGroupDescription, IDistinctValuesDescription
	{
		// Token: 0x06008098 RID: 32920 RVA: 0x001D6B50 File Offset: 0x001D4D50
		protected override Cloneable CreateInstanceCore()
		{
			return new XmlaGroupDescription();
		}

		// Token: 0x06008099 RID: 32921 RVA: 0x001D6B58 File Offset: 0x001D4D58
		internal override OlapLevelGroupDescription CreateLevelGroupDescription(OlapHierarchyFieldInfo fieldInfo)
		{
			return new XmlaLevelGroupDescription
			{
				GroupComparer = (GroupComparer)base.GroupComparer.Clone()
			};
		}

		// Token: 0x0600809A RID: 32922 RVA: 0x001D6B84 File Offset: 0x001D4D84
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			XmlaDataProvider xmlaDataProvider = base.Provider as XmlaDataProvider;
			return new XmlaDisctinctValuesProvider(xmlaDataProvider.ConnectionSettings, base.FieldInfo, xmlaDataProvider.SetConditionListCapacity);
		}
	}
}
