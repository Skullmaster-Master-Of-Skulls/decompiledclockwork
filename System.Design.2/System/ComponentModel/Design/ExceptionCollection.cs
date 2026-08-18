using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security;

namespace System.ComponentModel.Design
{
	// Token: 0x020001CC RID: 460
	[Serializable]
	public sealed class ExceptionCollection : Exception
	{
		// Token: 0x0600112B RID: 4395 RVA: 0x0005EFB0 File Offset: 0x0005D1B0
		public ExceptionCollection(ArrayList exceptions)
		{
			this.exceptions = exceptions;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0005EFBF File Offset: 0x0005D1BF
		private ExceptionCollection(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptions = (ArrayList)info.GetValue("exceptions", typeof(ArrayList));
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0005EFE9 File Offset: 0x0005D1E9
		public ArrayList Exceptions
		{
			get
			{
				if (this.exceptions != null)
				{
					return (ArrayList)this.exceptions.Clone();
				}
				return null;
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0005F005 File Offset: 0x0005D205
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("exceptions", this.exceptions);
			base.GetObjectData(info, context);
		}

		// Token: 0x040009B0 RID: 2480
		private ArrayList exceptions;
	}
}
