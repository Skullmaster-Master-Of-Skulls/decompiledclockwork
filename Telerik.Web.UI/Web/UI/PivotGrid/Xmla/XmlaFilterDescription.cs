using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x0200073D RID: 1853
	[DataContract]
	public sealed class XmlaFilterDescription : OlapFilterDescription
	{
		// Token: 0x060041E7 RID: 16871 RVA: 0x000CEDED File Offset: 0x000CCFED
		protected override Cloneable CreateInstanceCore()
		{
			return new XmlaFilterDescription();
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000CEDF4 File Offset: 0x000CCFF4
		internal override DistinctValuesProvider GetDisctinctValuesProvider()
		{
			XmlaDataProvider xmlaDataProvider = base.Provider as XmlaDataProvider;
			return new XmlaDisctinctValuesProvider(xmlaDataProvider.ConnectionSettings, base.FieldInfo, xmlaDataProvider.SetConditionListCapacity);
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x000CEE26 File Offset: 0x000CD026
		internal override OlapLevelFilterDescription CreateFilterDescription(OlapHierarchyFieldInfo info)
		{
			return new XmlaLevelFilterDescription(info, base.FieldInfo);
		}
	}
}
