using System;

namespace System.Xml
{
	// Token: 0x02000091 RID: 145
	[__DynamicallyInvokable]
	public abstract class XmlNameTable
	{
		// Token: 0x06000534 RID: 1332
		[__DynamicallyInvokable]
		public abstract string Get(char[] array, int offset, int length);

		// Token: 0x06000535 RID: 1333
		[__DynamicallyInvokable]
		public abstract string Get(string array);

		// Token: 0x06000536 RID: 1334
		[__DynamicallyInvokable]
		public abstract string Add(char[] array, int offset, int length);

		// Token: 0x06000537 RID: 1335
		[__DynamicallyInvokable]
		public abstract string Add(string array);

		// Token: 0x06000538 RID: 1336 RVA: 0x00013B68 File Offset: 0x00011D68
		[__DynamicallyInvokable]
		protected XmlNameTable()
		{
		}
	}
}
