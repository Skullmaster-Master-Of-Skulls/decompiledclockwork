using System;
using System.ComponentModel;

namespace System.Net
{
	// Token: 0x0200018E RID: 398
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class WriteStreamClosedEventArgs : EventArgs
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x0004F843 File Offset: 0x0004DA43
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public WriteStreamClosedEventArgs()
		{
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0004F84B File Offset: 0x0004DA4B
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Exception Error
		{
			get
			{
				return null;
			}
		}
	}
}
