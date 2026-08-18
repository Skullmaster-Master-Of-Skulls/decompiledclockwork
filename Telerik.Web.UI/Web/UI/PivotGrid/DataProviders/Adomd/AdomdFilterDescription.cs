using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000721 RID: 1825
	[DataContract]
	public sealed class AdomdFilterDescription : OlapFilterDescription
	{
		// Token: 0x060040C6 RID: 16582 RVA: 0x000CC371 File Offset: 0x000CA571
		protected override Cloneable CreateInstanceCore()
		{
			return new AdomdFilterDescription();
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x000CC378 File Offset: 0x000CA578
		internal override DistinctValuesProvider GetDisctinctValuesProvider()
		{
			AdomdDataProvider adomdDataProvider = base.Provider as AdomdDataProvider;
			return new AdomdDisctinctValuesProvider(adomdDataProvider.ConnectionSettings, base.FieldInfo, adomdDataProvider.SetConditionListCapacity);
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x000CC3AA File Offset: 0x000CA5AA
		internal override OlapLevelFilterDescription CreateFilterDescription(OlapHierarchyFieldInfo info)
		{
			return new AdomdLevelFilterDescription(info, base.FieldInfo);
		}
	}
}
