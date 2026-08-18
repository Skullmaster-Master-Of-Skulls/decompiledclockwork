using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E4B RID: 3659
	public class MissingEditableImageException : ApplicationException
	{
		// Token: 0x06008AD5 RID: 35541 RVA: 0x001F9FAC File Offset: 0x001F81AC
		public MissingEditableImageException()
		{
		}

		// Token: 0x06008AD6 RID: 35542 RVA: 0x001F9FB4 File Offset: 0x001F81B4
		public MissingEditableImageException(string message) : base(message)
		{
		}

		// Token: 0x06008AD7 RID: 35543 RVA: 0x001F9FBD File Offset: 0x001F81BD
		public MissingEditableImageException(string message, Exception ex) : base(message, ex)
		{
		}
	}
}
