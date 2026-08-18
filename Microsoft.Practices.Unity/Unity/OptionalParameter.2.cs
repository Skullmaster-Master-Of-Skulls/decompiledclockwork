using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200001C RID: 28
	public class OptionalParameter<T> : OptionalParameter
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003000 File Offset: 0x00001200
		public OptionalParameter() : base(typeof(T))
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003012 File Offset: 0x00001212
		public OptionalParameter(string name) : base(typeof(T), name)
		{
		}
	}
}
