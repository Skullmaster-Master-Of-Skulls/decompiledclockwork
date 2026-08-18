using System;

namespace AutoMapper
{
	// Token: 0x0200001B RID: 27
	public interface IMemberAccessor : IMemberGetter, IMemberResolver, IValueResolver
	{
		// Token: 0x060000CE RID: 206
		void SetValue(object destination, object value);
	}
}
