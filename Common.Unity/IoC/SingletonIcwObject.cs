using System;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x0200000D RID: 13
	public class SingletonIcwObject : IcwObject
	{
		// Token: 0x06000053 RID: 83 RVA: 0x0000360B File Offset: 0x0000180B
		public SingletonIcwObject(Type internalObjectType)
		{
			this._object = Activator.CreateInstance(internalObjectType);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003624 File Offset: 0x00001824
		public T GetInternalImplementation<T>()
		{
			return (T)((object)this._object);
		}

		// Token: 0x0400000E RID: 14
		private readonly object _object;
	}
}
