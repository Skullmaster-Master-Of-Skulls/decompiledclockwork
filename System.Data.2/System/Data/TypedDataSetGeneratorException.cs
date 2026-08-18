using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	public class TypedDataSetGeneratorException : DataException
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x00056188 File Offset: 0x00055588
		protected TypedDataSetGeneratorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			int num = (int)info.GetValue(this.KEY_ARRAYCOUNT, typeof(int));
			if (num > 0)
			{
				this.errorList = new ArrayList();
				for (int i = 0; i < num; i++)
				{
					this.errorList.Add(info.GetValue(this.KEY_ARRAYVALUES + i.ToString(), typeof(string)));
				}
				return;
			}
			this.errorList = null;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00056220 File Offset: 0x00055620
		public TypedDataSetGeneratorException()
		{
			this.errorList = null;
			base.HResult = -2146232021;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0005625C File Offset: 0x0005565C
		public TypedDataSetGeneratorException(string message) : base(message)
		{
			base.HResult = -2146232021;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00056294 File Offset: 0x00055694
		public TypedDataSetGeneratorException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232021;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000562CC File Offset: 0x000556CC
		public TypedDataSetGeneratorException(ArrayList list) : this()
		{
			this.errorList = list;
			base.HResult = -2146232021;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x000562F4 File Offset: 0x000556F4
		public ArrayList ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00056308 File Offset: 0x00055708
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (this.errorList != null)
			{
				info.AddValue(this.KEY_ARRAYCOUNT, this.errorList.Count);
				for (int i = 0; i < this.errorList.Count; i++)
				{
					info.AddValue(this.KEY_ARRAYVALUES + i.ToString(), this.errorList[i].ToString());
				}
				return;
			}
			info.AddValue(this.KEY_ARRAYCOUNT, 0);
		}

		// Token: 0x040002BB RID: 699
		private ArrayList errorList;

		// Token: 0x040002BC RID: 700
		private string KEY_ARRAYCOUNT = "KEY_ARRAYCOUNT";

		// Token: 0x040002BD RID: 701
		private string KEY_ARRAYVALUES = "KEY_ARRAYVALUES";
	}
}
