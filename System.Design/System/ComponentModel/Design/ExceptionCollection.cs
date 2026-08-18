using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x02000564 RID: 1380
	[Serializable]
	public sealed class ExceptionCollection : Exception
	{
		// Token: 0x060030CB RID: 12491 RVA: 0x00113FC7 File Offset: 0x00112FC7
		public ExceptionCollection(ArrayList exceptions)
		{
			this.exceptions = exceptions;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x00113FD6 File Offset: 0x00112FD6
		private ExceptionCollection(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptions = (ArrayList)info.GetValue("exceptions", typeof(ArrayList));
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x00114000 File Offset: 0x00113000
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

		// Token: 0x060030CE RID: 12494 RVA: 0x0011401C File Offset: 0x0011301C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("exceptions", this.exceptions);
			base.GetObjectData(info, context);
		}

		// Token: 0x040020B8 RID: 8376
		private ArrayList exceptions;
	}
}
