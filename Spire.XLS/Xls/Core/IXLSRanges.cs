using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001E1 RID: 481
	public interface IXLSRanges : IXLSRange
	{
		// Token: 0x06001B0F RID: 6927
		void Remove(IXLSRange range);

		// Token: 0x17000A2A RID: 2602
		IXLSRange this[int index]
		{
			get;
		}
	}
}
