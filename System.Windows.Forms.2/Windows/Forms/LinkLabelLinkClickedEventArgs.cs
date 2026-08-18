using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002C7 RID: 711
	[ComVisible(true)]
	public class LinkLabelLinkClickedEventArgs : EventArgs
	{
		// Token: 0x06002BAD RID: 11181 RVA: 0x000C4A0C File Offset: 0x000C2C0C
		public LinkLabelLinkClickedEventArgs(LinkLabel.Link link)
		{
			this.link = link;
			this.button = MouseButtons.Left;
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000C4A26 File Offset: 0x000C2C26
		public LinkLabelLinkClickedEventArgs(LinkLabel.Link link, MouseButtons button) : this(link)
		{
			this.button = button;
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000C4A36 File Offset: 0x000C2C36
		public MouseButtons Button
		{
			get
			{
				return this.button;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000C4A3E File Offset: 0x000C2C3E
		public LinkLabel.Link Link
		{
			get
			{
				return this.link;
			}
		}

		// Token: 0x0400124B RID: 4683
		private readonly LinkLabel.Link link;

		// Token: 0x0400124C RID: 4684
		private readonly MouseButtons button;
	}
}
