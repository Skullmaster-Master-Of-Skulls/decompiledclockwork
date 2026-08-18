using System;
using Telerik.Web.UI.SchedulerRecurrenceEditor.Classic;
using Telerik.Web.UI.SchedulerRecurrenceEditor.Lite;
using Telerik.Web.UI.SchedulerRecurrenceEditor.Native;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor
{
	// Token: 0x02000803 RID: 2051
	internal class ViewFactory : IViewFactory
	{
		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x06004B11 RID: 19217 RVA: 0x000EA636 File Offset: 0x000E8836
		public RecurrenceEditor Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x000EA63E File Offset: 0x000E883E
		public ViewFactory(RecurrenceEditor owner)
		{
			this._owner = owner;
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x000EA650 File Offset: 0x000E8850
		public IRecurrenceEditorView CreateView()
		{
			switch (this.Owner.ResolvedRenderMode)
			{
			case RenderMode.Lightweight:
				return new Telerik.Web.UI.SchedulerRecurrenceEditor.Lite.View(this.Owner);
			case RenderMode.Mobile:
				return new Telerik.Web.UI.SchedulerRecurrenceEditor.Native.View(this.Owner);
			}
			return new Telerik.Web.UI.SchedulerRecurrenceEditor.Classic.View(this.Owner);
		}

		// Token: 0x040012F5 RID: 4853
		private readonly RecurrenceEditor _owner;
	}
}
