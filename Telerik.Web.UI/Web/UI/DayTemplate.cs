using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI
{
	// Token: 0x0200101A RID: 4122
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxItem(false)]
	public class DayTemplate : Control
	{
		// Token: 0x17003379 RID: 13177
		// (get) Token: 0x0600A2C6 RID: 41670 RVA: 0x00243E00 File Offset: 0x00242000
		// (set) Token: 0x0600A2C7 RID: 41671 RVA: 0x00243E08 File Offset: 0x00242008
		[TemplateContainer(typeof(TemplateContainer))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("calendar day template")]
		[NotifyParentProperty(true)]
		public virtual ITemplate Content
		{
			get
			{
				return this._itemTemplate;
			}
			set
			{
				this._itemTemplate = value;
			}
		}

		// Token: 0x04002D49 RID: 11593
		private ITemplate _itemTemplate;
	}
}
