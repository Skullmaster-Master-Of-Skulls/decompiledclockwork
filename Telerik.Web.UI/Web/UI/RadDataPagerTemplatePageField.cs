using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001959 RID: 6489
	public class RadDataPagerTemplatePageField : RadDataPagerField
	{
		// Token: 0x17004BF1 RID: 19441
		// (get) Token: 0x0600FB4F RID: 64335 RVA: 0x00389F1B File Offset: 0x0038811B
		// (set) Token: 0x0600FB50 RID: 64336 RVA: 0x00389F23 File Offset: 0x00388123
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadDataPagerFieldItem))]
		[NotifyParentProperty(true)]
		public ITemplate PagerTemplate
		{
			get
			{
				return this._pagerTemplate;
			}
			set
			{
				this._pagerTemplate = value;
			}
		}

		// Token: 0x0600FB51 RID: 64337 RVA: 0x00389F2C File Offset: 0x0038812C
		public override void InitializeFieldControls(RadDataPagerFieldItem inItem)
		{
			if (this.PagerTemplate != null)
			{
				this.PagerTemplate.InstantiateIn(inItem);
			}
		}

		// Token: 0x0400476F RID: 18287
		private ITemplate _pagerTemplate;
	}
}
