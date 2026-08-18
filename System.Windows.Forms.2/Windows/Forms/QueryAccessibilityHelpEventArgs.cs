using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000338 RID: 824
	[ComVisible(true)]
	public class QueryAccessibilityHelpEventArgs : EventArgs
	{
		// Token: 0x0600356F RID: 13679 RVA: 0x00090A2B File Offset: 0x0008EC2B
		public QueryAccessibilityHelpEventArgs()
		{
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000F28C7 File Offset: 0x000F0AC7
		public QueryAccessibilityHelpEventArgs(string helpNamespace, string helpString, string helpKeyword)
		{
			this.helpNamespace = helpNamespace;
			this.helpString = helpString;
			this.helpKeyword = helpKeyword;
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x000F28E4 File Offset: 0x000F0AE4
		// (set) Token: 0x06003572 RID: 13682 RVA: 0x000F28EC File Offset: 0x000F0AEC
		public string HelpNamespace
		{
			get
			{
				return this.helpNamespace;
			}
			set
			{
				this.helpNamespace = value;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000F28F5 File Offset: 0x000F0AF5
		// (set) Token: 0x06003574 RID: 13684 RVA: 0x000F28FD File Offset: 0x000F0AFD
		public string HelpString
		{
			get
			{
				return this.helpString;
			}
			set
			{
				this.helpString = value;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000F2906 File Offset: 0x000F0B06
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x000F290E File Offset: 0x000F0B0E
		public string HelpKeyword
		{
			get
			{
				return this.helpKeyword;
			}
			set
			{
				this.helpKeyword = value;
			}
		}

		// Token: 0x04001F57 RID: 8023
		private string helpNamespace;

		// Token: 0x04001F58 RID: 8024
		private string helpString;

		// Token: 0x04001F59 RID: 8025
		private string helpKeyword;
	}
}
