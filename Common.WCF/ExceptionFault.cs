using System;
using System.Runtime.Serialization;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000007 RID: 7
	[DataContract(Namespace = "http://tpro.ca")]
	public abstract class ExceptionFault<T> : GenericFault where T : Exception
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002EB9 File Offset: 0x000010B9
		protected ExceptionFault()
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002EC3 File Offset: 0x000010C3
		protected ExceptionFault(string message) : base(message)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002ECE File Offset: 0x000010CE
		public virtual void ConvertFrom(T exception)
		{
			base.Message = exception.Message;
		}
	}
}
