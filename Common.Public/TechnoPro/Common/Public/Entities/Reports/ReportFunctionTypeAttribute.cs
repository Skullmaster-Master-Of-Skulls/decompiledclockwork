using System;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000220 RID: 544
	[Serializable]
	public class ReportFunctionTypeAttribute : Attribute
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000178D5 File Offset: 0x00015AD5
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x000178DD File Offset: 0x00015ADD
		public string Title { get; set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x000178E6 File Offset: 0x00015AE6
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x000178EE File Offset: 0x00015AEE
		public string Example { get; set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x000178F7 File Offset: 0x00015AF7
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x000178FF File Offset: 0x00015AFF
		public string Description { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00017908 File Offset: 0x00015B08
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x00017910 File Offset: 0x00015B10
		public bool IsHidden { get; set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00017919 File Offset: 0x00015B19
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x00017921 File Offset: 0x00015B21
		public string FunctionEditorWinFormsArgs { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0001792A File Offset: 0x00015B2A
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x00017932 File Offset: 0x00015B32
		public string FunctionEditorWinFormsType { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0001793B File Offset: 0x00015B3B
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x00017943 File Offset: 0x00015B43
		public string ExecutionClass { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0001794C File Offset: 0x00015B4C
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x00017954 File Offset: 0x00015B54
		public bool OnlyAvailableOnServer { get; set; }

		// Token: 0x0600109E RID: 4254 RVA: 0x0001795D File Offset: 0x00015B5D
		public ReportFunctionTypeAttribute(string title, string executionClass)
		{
			this.Title = title;
			this.ExecutionClass = executionClass;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00017977 File Offset: 0x00015B77
		public ReportFunctionTypeAttribute(string title, string executionClass, string description, bool isHidden = false)
		{
			this.Title = title;
			this.ExecutionClass = executionClass;
			this.IsHidden = isHidden;
			this.Description = description;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000179A2 File Offset: 0x00015BA2
		public ReportFunctionTypeAttribute(string title, string executionClass, string description, string FunctionEditorWinFormsType)
		{
			this.Title = title;
			this.ExecutionClass = executionClass;
			this.Description = description;
			this.FunctionEditorWinFormsType = FunctionEditorWinFormsType;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000179D0 File Offset: 0x00015BD0
		public static ReportFunctionTypeAttribute GetAttribute(eFunctionType functionType)
		{
			return ReportFunctionTypeAttribute.GetAttribute<ReportFunctionTypeAttribute>(functionType);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000179F0 File Offset: 0x00015BF0
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
