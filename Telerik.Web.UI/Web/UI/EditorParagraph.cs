using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001079 RID: 4217
	public class EditorParagraph : EditorNameValueItem
	{
		// Token: 0x0600A9D1 RID: 43473 RVA: 0x0024DD55 File Offset: 0x0024BF55
		public EditorParagraph()
		{
		}

		// Token: 0x0600A9D2 RID: 43474 RVA: 0x0024DD5D File Offset: 0x0024BF5D
		public EditorParagraph(string tag, string title)
		{
			this.Tag = tag;
			this.Title = title;
		}

		// Token: 0x17003686 RID: 13958
		// (get) Token: 0x0600A9D3 RID: 43475 RVA: 0x0024DD73 File Offset: 0x0024BF73
		// (set) Token: 0x0600A9D4 RID: 43476 RVA: 0x0024DD7B File Offset: 0x0024BF7B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17003687 RID: 13959
		// (get) Token: 0x0600A9D5 RID: 43477 RVA: 0x0024DD84 File Offset: 0x0024BF84
		// (set) Token: 0x0600A9D6 RID: 43478 RVA: 0x0024DD8C File Offset: 0x0024BF8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x17003688 RID: 13960
		// (get) Token: 0x0600A9D7 RID: 43479 RVA: 0x0024DD95 File Offset: 0x0024BF95
		// (set) Token: 0x0600A9D8 RID: 43480 RVA: 0x0024DD9D File Offset: 0x0024BF9D
		public string Title
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17003689 RID: 13961
		// (get) Token: 0x0600A9D9 RID: 43481 RVA: 0x0024DDA6 File Offset: 0x0024BFA6
		// (set) Token: 0x0600A9DA RID: 43482 RVA: 0x0024DDAE File Offset: 0x0024BFAE
		public string Tag
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}
	}
}
