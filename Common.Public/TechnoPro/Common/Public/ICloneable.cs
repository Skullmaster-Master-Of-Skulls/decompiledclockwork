using System;

namespace TechnoPro.Common.Public
{
	// Token: 0x020000BD RID: 189
	public interface ICloneable<T> : ICloneable where T : ICloneable<T>
	{
		// Token: 0x060004DD RID: 1245
		T Clone();
	}
}
