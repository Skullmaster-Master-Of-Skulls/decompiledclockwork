using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A35 RID: 6709
	internal sealed class SchedulerHeader : WebControl
	{
		// Token: 0x17004EE2 RID: 20194
		// (get) Token: 0x06010470 RID: 66672 RVA: 0x003A374E File Offset: 0x003A194E
		public List<SchedulerHeader> SubHeaders
		{
			get
			{
				return this._subHeaders;
			}
		}

		// Token: 0x17004EE3 RID: 20195
		// (get) Token: 0x06010471 RID: 66673 RVA: 0x003A3756 File Offset: 0x003A1956
		public bool SubHeadersVisible
		{
			get
			{
				return this._subHeadersVisible;
			}
		}

		// Token: 0x17004EE4 RID: 20196
		// (get) Token: 0x06010472 RID: 66674 RVA: 0x003A375E File Offset: 0x003A195E
		// (set) Token: 0x06010473 RID: 66675 RVA: 0x003A3766 File Offset: 0x003A1966
		public Unit? InnerHeight { get; set; }

		// Token: 0x17004EE5 RID: 20197
		// (get) Token: 0x06010474 RID: 66676 RVA: 0x003A376F File Offset: 0x003A196F
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this._tagKey;
			}
		}

		// Token: 0x06010475 RID: 66677 RVA: 0x003A3777 File Offset: 0x003A1977
		public SchedulerHeader(string text) : this(text, false, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x06010476 RID: 66678 RVA: 0x003A3783 File Offset: 0x003A1983
		public SchedulerHeader(string text, bool subHeadersVisible) : this(text, subHeadersVisible, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x06010477 RID: 66679 RVA: 0x003A3790 File Offset: 0x003A1990
		public SchedulerHeader(string text, bool subHeadersVisible, HtmlTextWriterTag tagKey)
		{
			this._subHeaders = new List<SchedulerHeader>();
			this._subHeadersVisible = true;
			this._tagKey = HtmlTextWriterTag.Div;
			base..ctor();
			this._subHeadersVisible = subHeadersVisible;
			this._tagKey = tagKey;
			this.Controls.Add(new LiteralControl(text));
		}

		// Token: 0x06010478 RID: 66680 RVA: 0x003A37DC File Offset: 0x003A19DC
		public SchedulerHeader(Control control)
		{
			this._subHeaders = new List<SchedulerHeader>();
			this._subHeadersVisible = true;
			this._tagKey = HtmlTextWriterTag.Div;
			base..ctor();
			this.Controls.Add(control);
		}

		// Token: 0x06010479 RID: 66681 RVA: 0x003A380A File Offset: 0x003A1A0A
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (this.TagKey != HtmlTextWriterTag.Unknown)
			{
				base.RenderBeginTag(writer);
			}
		}

		// Token: 0x0601047A RID: 66682 RVA: 0x003A381B File Offset: 0x003A1A1B
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this.TagKey != HtmlTextWriterTag.Unknown)
			{
				base.RenderEndTag(writer);
			}
		}

		// Token: 0x04004950 RID: 18768
		private readonly List<SchedulerHeader> _subHeaders;

		// Token: 0x04004951 RID: 18769
		private readonly bool _subHeadersVisible;

		// Token: 0x04004952 RID: 18770
		private readonly HtmlTextWriterTag _tagKey;
	}
}
