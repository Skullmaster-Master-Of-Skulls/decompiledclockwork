using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020003E7 RID: 999
	public class HtmlChartAppearance : ObjectWithState
	{
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x00079421 File Offset: 0x00077621
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public FillStyle FillStyle
		{
			get
			{
				if (this._fillStyle == null)
				{
					this._fillStyle = new FillStyle(this._prefix, base.OwnerViewState, this._isSeries);
				}
				return this._fillStyle;
			}
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0007944E File Offset: 0x0007764E
		public HtmlChartAppearance(string prefix, StateBag OwnerStateBag) : base("hca" + prefix, OwnerStateBag)
		{
			this._prefix = prefix;
			this._isSeries = false;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x00079470 File Offset: 0x00077670
		public HtmlChartAppearance(string prefix, StateBag OwnerStateBag, bool isSeries) : base("sa" + prefix, OwnerStateBag)
		{
			this._prefix = prefix;
			this._isSeries = isSeries;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x00079492 File Offset: 0x00077692
		internal virtual string Serialize()
		{
			return this.FillStyle.Serialize();
		}

		// Token: 0x04000961 RID: 2401
		private readonly string _prefix;

		// Token: 0x04000962 RID: 2402
		private readonly bool _isSeries;

		// Token: 0x04000963 RID: 2403
		private FillStyle _fillStyle;
	}
}
