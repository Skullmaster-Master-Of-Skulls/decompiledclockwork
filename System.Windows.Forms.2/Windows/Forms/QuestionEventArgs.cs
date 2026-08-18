using System;

namespace System.Windows.Forms
{
	// Token: 0x0200033C RID: 828
	public class QuestionEventArgs : EventArgs
	{
		// Token: 0x06003584 RID: 13700 RVA: 0x000F2955 File Offset: 0x000F0B55
		public QuestionEventArgs()
		{
			this.response = false;
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000F2964 File Offset: 0x000F0B64
		public QuestionEventArgs(bool response)
		{
			this.response = response;
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x000F2973 File Offset: 0x000F0B73
		// (set) Token: 0x06003587 RID: 13703 RVA: 0x000F297B File Offset: 0x000F0B7B
		public bool Response
		{
			get
			{
				return this.response;
			}
			set
			{
				this.response = value;
			}
		}

		// Token: 0x04001F5D RID: 8029
		private bool response;
	}
}
