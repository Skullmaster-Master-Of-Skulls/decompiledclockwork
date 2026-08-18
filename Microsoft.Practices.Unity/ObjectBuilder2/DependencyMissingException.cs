using System;
using System.Globalization;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000039 RID: 57
	public class DependencyMissingException : Exception
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00003B55 File Offset: 0x00001D55
		public DependencyMissingException()
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003B5D File Offset: 0x00001D5D
		public DependencyMissingException(string message) : base(message)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003B66 File Offset: 0x00001D66
		public DependencyMissingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003B70 File Offset: 0x00001D70
		public DependencyMissingException(object buildKey) : base(string.Format(CultureInfo.CurrentCulture, Resources.MissingDependency, new object[]
		{
			buildKey
		}))
		{
		}
	}
}
