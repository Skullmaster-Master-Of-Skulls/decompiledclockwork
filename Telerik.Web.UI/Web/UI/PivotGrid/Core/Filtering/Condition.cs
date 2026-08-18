using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006BA RID: 1722
	[DataContract]
	public abstract class Condition : SettingsNode
	{
		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06003DEC RID: 15852 RVA: 0x000C7361 File Offset: 0x000C5561
		public virtual bool IsActive
		{
			get
			{
				return true;
			}
		}
	}
}
