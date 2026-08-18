using System;
using System.Linq;

namespace TechnoPro.Common.UI.Web.Entity.Web
{
	// Token: 0x02000011 RID: 17
	public class CaptchaQuestionAttribute : Attribute
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002050 File Offset: 0x00000250
		public CaptchaQuestionAttribute()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002450 File Offset: 0x00000650
		public CaptchaQuestionAttribute(string question, params string[] possibleAnswers)
		{
			this.Question = question;
			this.PossibleAnswers = possibleAnswers;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000246A File Offset: 0x0000066A
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002472 File Offset: 0x00000672
		public string Question { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000247B File Offset: 0x0000067B
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002483 File Offset: 0x00000683
		public string[] PossibleAnswers { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x0000248C File Offset: 0x0000068C
		public static CaptchaQuestionAttribute GetAttribute(eCaptchaQuestion e)
		{
			return CaptchaQuestionAttribute.GetAttribute<CaptchaQuestionAttribute>(e);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000024AC File Offset: 0x000006AC
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
