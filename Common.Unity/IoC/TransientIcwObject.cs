using System;

namespace TechnoPro.Common.Unity.IoC
{
	// Token: 0x0200000F RID: 15
	public class TransientIcwObject : IcwObject
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003665 File Offset: 0x00001865
		public TransientIcwObject(Type internalObjectType)
		{
			this._internalObjectType = internalObjectType;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003678 File Offset: 0x00001878
		public T GetInternalImplementation<T>()
		{
			return (T)((object)Activator.CreateInstance(this._internalObjectType));
		}

		// Token: 0x0400000F RID: 15
		private readonly Type _internalObjectType;
	}
}
