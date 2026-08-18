using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D5E RID: 3422
	[DataContract]
	public sealed class AdomdGroupDescription : OlapGroupDescription, IDistinctValuesDescription
	{
		// Token: 0x06007FA8 RID: 32680 RVA: 0x001D2C4E File Offset: 0x001D0E4E
		protected override Cloneable CreateInstanceCore()
		{
			return new AdomdGroupDescription();
		}

		// Token: 0x06007FA9 RID: 32681 RVA: 0x001D2C58 File Offset: 0x001D0E58
		internal override OlapLevelGroupDescription CreateLevelGroupDescription(OlapHierarchyFieldInfo fieldInfo)
		{
			return new AdomdLevelGroupDescription
			{
				GroupComparer = (GroupComparer)base.GroupComparer.Clone()
			};
		}

		// Token: 0x06007FAA RID: 32682 RVA: 0x001D2C84 File Offset: 0x001D0E84
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			AdomdDataProvider adomdDataProvider = base.Provider as AdomdDataProvider;
			return new AdomdDisctinctValuesProvider(adomdDataProvider.ConnectionSettings, base.FieldInfo, adomdDataProvider.SetConditionListCapacity);
		}
	}
}
