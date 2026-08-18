using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x0200073E RID: 1854
	[DataContract]
	public sealed class XmlaLevelFilterDescription : OlapLevelFilterDescription
	{
		// Token: 0x060041EB RID: 16875 RVA: 0x000CEE3C File Offset: 0x000CD03C
		public XmlaLevelFilterDescription()
		{
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000CEE44 File Offset: 0x000CD044
		internal XmlaLevelFilterDescription(OlapHierarchyFieldInfo fieldInfo, OlapHierarchyFieldInfo parentInfo) : base(fieldInfo, parentInfo)
		{
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x000CEE4E File Offset: 0x000CD04E
		protected override Cloneable CreateInstanceCore()
		{
			return new XmlaLevelFilterDescription();
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000CEE58 File Offset: 0x000CD058
		internal override DistinctValuesProvider GetDisctinctValuesProvider()
		{
			XmlaDataProvider xmlaDataProvider = base.Provider as XmlaDataProvider;
			return new XmlaDisctinctValuesProvider(xmlaDataProvider.ConnectionSettings, base.FieldInfo, xmlaDataProvider.SetConditionListCapacity);
		}
	}
}
