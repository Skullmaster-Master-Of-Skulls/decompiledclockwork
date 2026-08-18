using System;
using System.Linq;

namespace TechnoPro.Common.UI.Web.Entity
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class ClockWorkWebPageModuleAttribute : Attribute
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002050 File Offset: 0x00000250
		public ClockWorkWebPageModuleAttribute()
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002124 File Offset: 0x00000324
		public ClockWorkWebPageModuleAttribute(string navigateUrlWithTrailingSlash, string title, string iconUrl)
		{
			this.NavigateUrlWithTrailingSlash = navigateUrlWithTrailingSlash;
			this.IconUrl = iconUrl;
			this.Title = title;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002146 File Offset: 0x00000346
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000214E File Offset: 0x0000034E
		public string NavigateUrlWithTrailingSlash { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002157 File Offset: 0x00000357
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000215F File Offset: 0x0000035F
		public string IconUrl { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002168 File Offset: 0x00000368
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002170 File Offset: 0x00000370
		public string Title { get; set; }

		// Token: 0x0600001A RID: 26 RVA: 0x0000217C File Offset: 0x0000037C
		public static ClockWorkWebPageModuleAttribute GetAttribute(eClockWorkWebPageModule clockWorkWebPageModule)
		{
			return ClockWorkWebPageModuleAttribute.GetAttribute<ClockWorkWebPageModuleAttribute>(clockWorkWebPageModule);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000219C File Offset: 0x0000039C
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
