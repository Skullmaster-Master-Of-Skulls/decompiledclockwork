using System;

namespace TechnoPro.Common.DataStructure
{
	// Token: 0x02000006 RID: 6
	public interface ICloneable<T> : ICloneable where T : ICloneable<T>
	{
		// Token: 0x06000027 RID: 39
		T Clone();
	}
}
