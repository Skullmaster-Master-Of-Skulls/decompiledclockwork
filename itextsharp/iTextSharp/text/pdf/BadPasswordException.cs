using System;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x020001D1 RID: 465
	public class BadPasswordException : IOException
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x00068228 File Offset: 0x00067228
		public BadPasswordException(string message) : base(message)
		{
		}
	}
}
