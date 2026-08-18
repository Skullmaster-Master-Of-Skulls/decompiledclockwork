using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.Axes.BaseUnitSteps
{
	// Token: 0x020003AA RID: 938
	[ParseChildren(typeof(BaseUnitStep))]
	public class BaseUnitStepCollection : StronglyTypedStateManagedCollection<BaseUnitStep>
	{
		// Token: 0x06002305 RID: 8965 RVA: 0x0007534D File Offset: 0x0007354D
		public override void Add(BaseUnitStep item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00075360 File Offset: 0x00073560
		public void Add(int value)
		{
			BaseUnitStep item = new BaseUnitStep(value);
			this.Add(item);
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x0007537C File Offset: 0x0007357C
		public void AddRange(IEnumerable<int> values)
		{
			foreach (int value in values)
			{
				this.Add(value);
			}
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x0007556C File Offset: 0x0007376C
		public IEnumerable<int> ToIntList()
		{
			foreach (object obj in base.List)
			{
				BaseUnitStep item = (BaseUnitStep)obj;
				yield return item.Value;
			}
			yield break;
		}
	}
}
