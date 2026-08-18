using System;
using Telerik.Web.UI.SchedulerRecurrenceEditor.Classic;
using Telerik.Web.UI.SchedulerRecurrenceEditor.Lite;
using Telerik.Web.UI.SchedulerRecurrenceEditor.Native;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor
{
	// Token: 0x020007FC RID: 2044
	internal class RendererFactory
	{
		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x000E8656 File Offset: 0x000E6856
		public RecurrenceEditor Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x000E865E File Offset: 0x000E685E
		public RendererFactory(RecurrenceEditor owner)
		{
			this._owner = owner;
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x000E8670 File Offset: 0x000E6870
		public IRecurrenceEditorRenderer CreateRenderer()
		{
			switch (this.Owner.ResolvedRenderMode)
			{
			case RenderMode.Lightweight:
				return new Telerik.Web.UI.SchedulerRecurrenceEditor.Lite.Renderer(this.Owner.View);
			case RenderMode.Mobile:
				return new Telerik.Web.UI.SchedulerRecurrenceEditor.Native.Renderer(this.Owner.View);
			}
			return new Telerik.Web.UI.SchedulerRecurrenceEditor.Classic.Renderer(this.Owner.View);
		}

		// Token: 0x040012CA RID: 4810
		private readonly RecurrenceEditor _owner;
	}
}
