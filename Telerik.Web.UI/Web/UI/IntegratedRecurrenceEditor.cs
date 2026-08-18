using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A17 RID: 6679
	internal class IntegratedRecurrenceEditor : RecurrenceEditor
	{
		// Token: 0x17004E03 RID: 19971
		// (get) Token: 0x06010274 RID: 66164 RVA: 0x0039FB6B File Offset: 0x0039DD6B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public override IRecurrenceEditorStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new IntegratedRecurrenceEditorStrings(new LocalizationProvider("RadScheduler.Main", this));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x06010275 RID: 66165 RVA: 0x0039FBA9 File Offset: 0x0039DDA9
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_baseId", this.NamingContainer.ClientID);
		}

		// Token: 0x04004924 RID: 18724
		private IRecurrenceEditorStrings _localization;
	}
}
