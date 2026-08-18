using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	public class DeletedRowInaccessibleException : DataException
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x001ED5E8 File Offset: 0x001EC9E8
		protected DeletedRowInaccessibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x001ED608 File Offset: 0x001ECA08
		public DeletedRowInaccessibleException() : base(Res.GetString("DataSet_DefaultDeletedRowInaccessibleException"))
		{
			base.HResult = -2146232031;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x001ED638 File Offset: 0x001ECA38
		public DeletedRowInaccessibleException(string s) : base(s)
		{
			base.HResult = -2146232031;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x001ED658 File Offset: 0x001ECA58
		public DeletedRowInaccessibleException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232031;
		}
	}
}
