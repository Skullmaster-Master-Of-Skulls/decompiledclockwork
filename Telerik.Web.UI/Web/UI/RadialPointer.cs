using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Gauge;

namespace Telerik.Web.UI
{
	// Token: 0x02000B67 RID: 2919
	[ToolboxItem(false)]
	public class RadialPointer : PointerBase
	{
		// Token: 0x17002421 RID: 9249
		// (get) Token: 0x06006E2A RID: 28202 RVA: 0x00198C6D File Offset: 0x00196E6D
		[Description("Defines the settings of the cap of the RadialGauge's pointer.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Behavior")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public Cap Cap
		{
			get
			{
				if (this._cap == null)
				{
					this._cap = new Cap();
				}
				return this._cap;
			}
		}

		// Token: 0x06006E2B RID: 28203 RVA: 0x00198C88 File Offset: 0x00196E88
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Cap).LoadViewState(array[1]);
		}

		// Token: 0x06006E2C RID: 28204 RVA: 0x00198CB4 File Offset: 0x00196EB4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Cap).SaveViewState()
			};
		}

		// Token: 0x06006E2D RID: 28205 RVA: 0x00198CE2 File Offset: 0x00196EE2
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Cap).TrackViewState();
		}

		// Token: 0x04001DC6 RID: 7622
		private Cap _cap;
	}
}
