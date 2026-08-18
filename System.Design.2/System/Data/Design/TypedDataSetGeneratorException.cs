using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data.Design
{
	// Token: 0x02000224 RID: 548
	[Serializable]
	public class TypedDataSetGeneratorException : DataException
	{
		// Token: 0x0600146D RID: 5229 RVA: 0x000755D0 File Offset: 0x000737D0
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

		// Token: 0x0600146E RID: 5230 RVA: 0x00075668 File Offset: 0x00073868
		public TypedDataSetGeneratorException()
		{
			this.errorList = null;
			base.HResult = -2146232021;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00075698 File Offset: 0x00073898
		public TypedDataSetGeneratorException(string message) : base(message)
		{
			base.HResult = -2146232021;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000756C2 File Offset: 0x000738C2
		public TypedDataSetGeneratorException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232021;
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000756ED File Offset: 0x000738ED
		public TypedDataSetGeneratorException(IList list) : this()
		{
			this.errorList = new ArrayList(list);
			base.HResult = -2146232021;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0007570C File Offset: 0x0007390C
		public IList ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x00075714 File Offset: 0x00073914
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
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

		// Token: 0x04000AD7 RID: 2775
		private ArrayList errorList;

		// Token: 0x04000AD8 RID: 2776
		private string KEY_ARRAYCOUNT = "KEY_ARRAYCOUNT";

		// Token: 0x04000AD9 RID: 2777
		private string KEY_ARRAYVALUES = "KEY_ARRAYVALUES";
	}
}
