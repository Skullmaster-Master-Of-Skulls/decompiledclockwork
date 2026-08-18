using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x020006F8 RID: 1784
	[DataContract]
	public abstract class OlapLevelGroupDescription : OlapGroupDescriptionBase
	{
		// Token: 0x06003F7C RID: 16252 RVA: 0x000C95AC File Offset: 0x000C77AC
		internal OlapLevelGroupDescription()
		{
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x000C95B4 File Offset: 0x000C77B4
		internal OlapLevelGroupDescription(OlapHierarchyFieldInfo fieldInfo)
		{
			base.FieldInfo = fieldInfo;
		}
	}
}
