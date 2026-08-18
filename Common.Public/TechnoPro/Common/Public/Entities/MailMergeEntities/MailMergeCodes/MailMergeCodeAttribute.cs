using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeCodes
{
	// Token: 0x020002DD RID: 733
	public class MailMergeCodeAttribute : Attribute
	{
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x0001B687 File Offset: 0x00019887
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x0001B68F File Offset: 0x0001988F
		public eMailMergeCodeGroup Group { get; set; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0001B698 File Offset: 0x00019898
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x0001B6A0 File Offset: 0x000198A0
		public string Description { get; set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0001B6A9 File Offset: 0x000198A9
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x0001B6B1 File Offset: 0x000198B1
		public string CodeText { get; set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0001B6BA File Offset: 0x000198BA
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x0001B6C2 File Offset: 0x000198C2
		public string ExampleOutput { get; set; }

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0001B6CB File Offset: 0x000198CB
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x0001B6D3 File Offset: 0x000198D3
		public Type ValueType { get; set; }

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0001B6DC File Offset: 0x000198DC
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x0001B6E4 File Offset: 0x000198E4
		public IList<string> EquivalentCodeTexts { get; set; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0001B6ED File Offset: 0x000198ED
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x0001B6F5 File Offset: 0x000198F5
		public bool IsHidden { get; set; }

		// Token: 0x06001612 RID: 5650 RVA: 0x0001B6FE File Offset: 0x000198FE
		public MailMergeCodeAttribute()
		{
			this.EquivalentCodeTexts = null;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0001B710 File Offset: 0x00019910
		public MailMergeCodeAttribute(eMailMergeCodeGroup Group, string CodeText, string Description, Type ValueType, string ExampleOutput)
		{
			this.EquivalentCodeTexts = null;
			this.Group = Group;
			this.CodeText = CodeText;
			this.Description = Description;
			this.ValueType = ValueType;
			this.ExampleOutput = ExampleOutput;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0001B74C File Offset: 0x0001994C
		public MailMergeCodeAttribute(eMailMergeCodeGroup Group, string CodeText, string Description, Type ValueType, string ExampleOutput, params string[] EquivalentCodeTexts)
		{
			this.EquivalentCodeTexts = EquivalentCodeTexts;
			this.Group = Group;
			this.CodeText = CodeText;
			this.Description = Description;
			this.ValueType = ValueType;
			this.ExampleOutput = ExampleOutput;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0001B78C File Offset: 0x0001998C
		public static MailMergeCodeAttribute GetAttribute(eMailMergeCode code)
		{
			return MailMergeCodeAttribute.GetAttribute<MailMergeCodeAttribute>(code);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0001B7AC File Offset: 0x000199AC
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
