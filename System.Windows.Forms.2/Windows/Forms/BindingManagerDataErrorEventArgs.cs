using System;

namespace System.Windows.Forms
{
	// Token: 0x02000137 RID: 311
	public class BindingManagerDataErrorEventArgs : EventArgs
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x00020355 File Offset: 0x0001E555
		public BindingManagerDataErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00020364 File Offset: 0x0001E564
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040006CB RID: 1739
		private Exception exception;
	}
}
