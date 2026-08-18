using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000722 RID: 1826
	[DataContract]
	public sealed class AdomdLevelFilterDescription : OlapLevelFilterDescription
	{
		// Token: 0x060040CA RID: 16586 RVA: 0x000CC3C0 File Offset: 0x000CA5C0
		public AdomdLevelFilterDescription()
		{
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x000CC3C8 File Offset: 0x000CA5C8
		internal AdomdLevelFilterDescription(OlapHierarchyFieldInfo fieldInfo, OlapHierarchyFieldInfo parentInfo) : base(fieldInfo, parentInfo)
		{
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x000CC3D2 File Offset: 0x000CA5D2
		protected override Cloneable CreateInstanceCore()
		{
			return new AdomdLevelFilterDescription();
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x000CC3DC File Offset: 0x000CA5DC
		internal override DistinctValuesProvider GetDisctinctValuesProvider()
		{
			AdomdDataProvider adomdDataProvider = base.Provider as AdomdDataProvider;
			return new AdomdDisctinctValuesProvider(adomdDataProvider.ConnectionSettings, base.FieldInfo, adomdDataProvider.SetConditionListCapacity);
		}
	}
}
