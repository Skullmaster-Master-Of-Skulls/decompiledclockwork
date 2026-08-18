using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000085 RID: 133
	public abstract class UpdatePanelControlTrigger : UpdatePanelTrigger
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001A97F File Offset: 0x00018B7F
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0001A990 File Offset: 0x00018B90
		[Category("Behavior")]
		[DefaultValue("")]
		[IDReferenceProperty]
		[ResourceDescription("UpdatePanelControlTrigger_ControlID")]
		public string ControlID
		{
			get
			{
				return this._controlID ?? string.Empty;
			}
			set
			{
				this._controlID = value;
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001A99C File Offset: 0x00018B9C
		protected Control FindTargetControl(bool searchNamingContainers)
		{
			if (string.IsNullOrEmpty(this.ControlID))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanelControlTrigger_NoControlID, new object[]
				{
					base.Owner.ID
				}));
			}
			Control control = ControlUtil.FindTargetControl(this.ControlID, base.Owner, searchNamingContainers);
			if (control == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanelControlTrigger_ControlNotFound, new object[]
				{
					this.ControlID,
					base.Owner.ID
				}));
			}
			return control;
		}

		// Token: 0x04000217 RID: 535
		private string _controlID;
	}
}
