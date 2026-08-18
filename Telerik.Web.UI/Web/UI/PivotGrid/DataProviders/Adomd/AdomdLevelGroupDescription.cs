using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000723 RID: 1827
	[DataContract]
	public class AdomdLevelGroupDescription : OlapLevelGroupDescription, IDistinctValuesDescription
	{
		// Token: 0x060040CF RID: 16591 RVA: 0x000CC416 File Offset: 0x000CA616
		protected override Cloneable CreateInstanceCore()
		{
			return new AdomdLevelGroupDescription();
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x000CC420 File Offset: 0x000CA620
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			AdomdDataProvider adomdDataProvider = base.Provider as AdomdDataProvider;
			return new AdomdDisctinctValuesProvider(adomdDataProvider.ConnectionSettings, base.FieldInfo, adomdDataProvider.SetConditionListCapacity);
		}
	}
}
