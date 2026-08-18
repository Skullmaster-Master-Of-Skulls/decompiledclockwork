using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x0200073F RID: 1855
	[DataContract]
	public sealed class XmlaLevelGroupDescription : OlapLevelGroupDescription, IDistinctValuesDescription
	{
		// Token: 0x060041EF RID: 16879 RVA: 0x000CEE8A File Offset: 0x000CD08A
		protected override Cloneable CreateInstanceCore()
		{
			return new XmlaLevelGroupDescription();
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000CEE94 File Offset: 0x000CD094
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			XmlaDataProvider xmlaDataProvider = base.Provider as XmlaDataProvider;
			return new XmlaDisctinctValuesProvider(xmlaDataProvider.ConnectionSettings, base.FieldInfo, xmlaDataProvider.SetConditionListCapacity);
		}
	}
}
