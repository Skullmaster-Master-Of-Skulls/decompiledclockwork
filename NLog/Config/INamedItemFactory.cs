using System;

namespace NLog.Config
{
	// Token: 0x02000047 RID: 71
	public interface INamedItemFactory<TInstanceType, TDefinitionType> where TInstanceType : class
	{
		// Token: 0x0600014C RID: 332
		void RegisterDefinition(string itemName, TDefinitionType itemDefinition);

		// Token: 0x0600014D RID: 333
		bool TryGetDefinition(string itemName, out TDefinitionType result);

		// Token: 0x0600014E RID: 334
		TInstanceType CreateInstance(string itemName);

		// Token: 0x0600014F RID: 335
		bool TryCreateInstance(string itemName, out TInstanceType result);
	}
}
