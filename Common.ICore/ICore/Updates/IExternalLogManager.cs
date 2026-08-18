using System;
using System.Threading.Tasks;

namespace TechnoPro.Common.ICore.Updates
{
	// Token: 0x0200001A RID: 26
	public interface IExternalLogManager
	{
		// Token: 0x060000AD RID: 173
		void Log(string text);

		// Token: 0x060000AE RID: 174
		Task LogAsync(string text);
	}
}
