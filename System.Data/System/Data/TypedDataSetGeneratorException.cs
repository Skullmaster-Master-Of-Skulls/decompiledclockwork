using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	public class TypedDataSetGeneratorException : DataException
	{
		// Token: 0x06000F42 RID: 3906 RVA: 0x0022CAF8 File Offset: 0x0022BEF8
		protected TypedDataSetGeneratorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			int num = (int)info.GetValue(this.KEY_ARRAYCOUNT, typeof(int));
			if (num > 0)
			{
				this.errorList = new ArrayList();
				for (int i = 0; i < num; i++)
				{
					this.errorList.Add(info.GetValue(this.KEY_ARRAYVALUES + i, typeof(string)));
				}
				return;
			}
			this.errorList = null;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0022CB98 File Offset: 0x0022BF98
		public TypedDataSetGeneratorException()
		{
			this.errorList = null;
			base.HResult = -2146232021;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0022CBD8 File Offset: 0x0022BFD8
		public TypedDataSetGeneratorException(string message) : base(message)
		{
			base.HResult = -2146232021;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0022CC18 File Offset: 0x0022C018
		public TypedDataSetGeneratorException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232021;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0022CC58 File Offset: 0x0022C058
		public TypedDataSetGeneratorException(ArrayList list) : this()
		{
			this.errorList = list;
			base.HResult = -2146232021;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0022CC88 File Offset: 0x0022C088
		public ArrayList ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0022CCA8 File Offset: 0x0022C0A8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (this.errorList != null)
			{
				info.AddValue(this.KEY_ARRAYCOUNT, this.errorList.Count);
				for (int i = 0; i < this.errorList.Count; i++)
				{
					info.AddValue(this.KEY_ARRAYVALUES + i, this.errorList[i].ToString());
				}
				return;
			}
			info.AddValue(this.KEY_ARRAYCOUNT, 0);
		}

		// Token: 0x04000ABA RID: 2746
		private ArrayList errorList;

		// Token: 0x04000ABB RID: 2747
		private string KEY_ARRAYCOUNT = "KEY_ARRAYCOUNT";

		// Token: 0x04000ABC RID: 2748
		private string KEY_ARRAYVALUES = "KEY_ARRAYVALUES";
	}
}
