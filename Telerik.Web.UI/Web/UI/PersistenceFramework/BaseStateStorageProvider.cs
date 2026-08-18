using System;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000C1C RID: 3100
	public abstract class BaseStateStorageProvider : IStateStorageProvider
	{
		// Token: 0x060075FD RID: 30205 RVA: 0x001B6835 File Offset: 0x001B4A35
		public BaseStateStorageProvider()
		{
		}

		// Token: 0x060075FE RID: 30206 RVA: 0x001B683D File Offset: 0x001B4A3D
		public virtual void SaveStateToStorage(string key, string serializedState)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060075FF RID: 30207 RVA: 0x001B6844 File Offset: 0x001B4A44
		public virtual string LoadStateFromStorage(string key)
		{
			throw new NotImplementedException();
		}
	}
}
