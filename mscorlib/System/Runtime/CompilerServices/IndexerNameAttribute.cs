using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005E8 RID: 1512
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	[Serializable]
	public sealed class IndexerNameAttribute : Attribute
	{
		// Token: 0x060037EB RID: 14315 RVA: 0x000BBCA1 File Offset: 0x000BACA1
		public IndexerNameAttribute(string indexerName)
		{
		}
	}
}
