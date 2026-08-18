using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000AF RID: 175
	[Serializable]
	public class DeletedRowInaccessibleException : DataException
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x0005C390 File Offset: 0x0005B790
		protected DeletedRowInaccessibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0005C3A8 File Offset: 0x0005B7A8
		public DeletedRowInaccessibleException() : base(Res.GetString("DataSet_DefaultDeletedRowInaccessibleException"))
		{
			base.HResult = -2146232031;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0005C3D0 File Offset: 0x0005B7D0
		public DeletedRowInaccessibleException(string s) : base(s)
		{
			base.HResult = -2146232031;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0005C3F0 File Offset: 0x0005B7F0
		public DeletedRowInaccessibleException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232031;
		}
	}
}
