using System;
using System.Reflection;

namespace TechnoPro.Common.Public.Entities.Templates
{
	// Token: 0x0200016F RID: 367
	public class TemplateGroupMeaningAttribute : Attribute
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x00012452 File Offset: 0x00010652
		public TemplateGroupMeaningAttribute(string GroupTitle, string Description, eTemplateType TemplateType = eTemplateType.Unknown)
		{
			this.GroupTitle = GroupTitle;
			this.Description = Description;
			this.TemplateType = eTemplateType.Unknown;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00012474 File Offset: 0x00010674
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0001247C File Offset: 0x0001067C
		public string GroupTitle { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00012485 File Offset: 0x00010685
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x0001248D File Offset: 0x0001068D
		public string Description { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00012496 File Offset: 0x00010696
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0001249E File Offset: 0x0001069E
		public eTemplateType TemplateType { get; set; }

		// Token: 0x060008E2 RID: 2274 RVA: 0x000124A8 File Offset: 0x000106A8
		public static TemplateGroupMeaningAttribute GetAttribute(eTemplateGroupMeaning meaning)
		{
			Type type = meaning.GetType();
			FieldInfo field = type.GetField(meaning.ToString());
			TemplateGroupMeaningAttribute[] array = field.GetCustomAttributes(typeof(TemplateGroupMeaningAttribute), false) as TemplateGroupMeaningAttribute[];
			return (array != null && array.Length != 0) ? array[0] : null;
		}
	}
}
