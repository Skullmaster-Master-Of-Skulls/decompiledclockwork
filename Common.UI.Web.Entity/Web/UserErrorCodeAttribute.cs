using System;
using System.Linq;

namespace TechnoPro.Common.UI.Web.Entity.Web
{
	// Token: 0x02000015 RID: 21
	public class UserErrorCodeAttribute : Attribute
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002050 File Offset: 0x00000250
		public UserErrorCodeAttribute()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000267D File Offset: 0x0000087D
		public UserErrorCodeAttribute(string url)
		{
			this.Url = url;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000268F File Offset: 0x0000088F
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002697 File Offset: 0x00000897
		public string Url { get; set; }

		// Token: 0x06000058 RID: 88 RVA: 0x000026A0 File Offset: 0x000008A0
		public static UserErrorCodeAttribute GetAttribute(UserErrorCode errorCode)
		{
			return UserErrorCodeAttribute.GetAttribute<UserErrorCodeAttribute>(errorCode);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000026C0 File Offset: 0x000008C0
		public static T GetAttribute<T>(Enum enumeration) where T : Attribute
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
