using System;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.ICore.Updates
{
	// Token: 0x0200001B RID: 27
	public interface IUpdateClientVersion
	{
		// Token: 0x060000AF RID: 175
		CurrentVersionInfo CurrentVersionOnClient(FileType fileType, int addressSize);
	}
}
