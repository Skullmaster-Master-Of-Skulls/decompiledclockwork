using System;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x0200022D RID: 557
	[Serializable]
	public class ReportBuildInDynamicFormAttribute : Attribute
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public ReportBuildInDynamicFormAttribute()
		{
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00017E55 File Offset: 0x00016055
		public ReportBuildInDynamicFormAttribute(string dynamicFormParameters)
		{
			this.DynamicFormParameters = dynamicFormParameters;
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00017E67 File Offset: 0x00016067
		// (set) Token: 0x06001110 RID: 4368 RVA: 0x00017E6F File Offset: 0x0001606F
		public string DynamicFormParameters { get; set; }

		// Token: 0x06001111 RID: 4369 RVA: 0x00017E78 File Offset: 0x00016078
		public static ReportBuildInDynamicFormAttribute GetAttribute(eReportBuiltInDynamicForm builtInDynamicForm)
		{
			return ReportBuildInDynamicFormAttribute.GetAttribute<ReportBuildInDynamicFormAttribute>(builtInDynamicForm);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00017E98 File Offset: 0x00016098
		private static T GetAttribute<T>(Enum enumeration) where T : Attribute
		{
			T t = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault<T>();
			bool flag = t == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				result = t;
			}
			return result;
		}
	}
}
