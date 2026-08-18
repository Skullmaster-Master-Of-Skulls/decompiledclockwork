using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200003A RID: 58
	public class IllegalInjectionMethodException : Exception
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00003B9E File Offset: 0x00001D9E
		public IllegalInjectionMethodException()
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003BA6 File Offset: 0x00001DA6
		public IllegalInjectionMethodException(string message) : base(message)
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003BAF File Offset: 0x00001DAF
		public IllegalInjectionMethodException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
