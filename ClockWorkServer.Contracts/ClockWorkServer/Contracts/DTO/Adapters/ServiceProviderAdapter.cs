using System;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C8D RID: 3213
	public static class ServiceProviderAdapter
	{
		// Token: 0x060042F4 RID: 17140 RVA: 0x00023830 File Offset: 0x00021A30
		public static string GetName(this ServiceProviderBaseDTO providerBase, bool showStudentNumber = false)
		{
			bool flag = providerBase == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string text = string.Join(" ", (from g in new string[]
				{
					(providerBase.FirstName ?? "").Trim(),
					(providerBase.LastName ?? "").Trim()
				}
				where g.Length > 0
				select g).ToArray<string>());
				bool flag2 = !showStudentNumber || (providerBase.StudentNumber ?? "").Trim().Length < 1;
				if (flag2)
				{
					result = text;
				}
				else
				{
					result = text + " (" + providerBase.StudentNumber + ")";
				}
			}
			return result;
		}
	}
}
