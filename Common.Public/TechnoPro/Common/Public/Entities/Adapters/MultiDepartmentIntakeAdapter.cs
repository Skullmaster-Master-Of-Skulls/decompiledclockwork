using System;
using Newtonsoft.Json;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005C6 RID: 1478
	public static class MultiDepartmentIntakeAdapter
	{
		// Token: 0x06002F85 RID: 12165 RVA: 0x00036A24 File Offset: 0x00034C24
		public static string SerializeMultiDepartmentIntakeSettings(this MultiDepartmentIntakeSettings settings)
		{
			return (settings == null) ? string.Empty : JsonConvert.SerializeObject(settings);
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x00036A48 File Offset: 0x00034C48
		public static MultiDepartmentIntakeSettings DeserializeMultiDepartmentIntakeSettings(this string s)
		{
			bool flag = string.IsNullOrWhiteSpace(s);
			MultiDepartmentIntakeSettings result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					result = JsonConvert.DeserializeObject<MultiDepartmentIntakeSettings>(s);
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}
	}
}
