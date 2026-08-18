using System;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200035A RID: 858
	public class DynamicControlAttribute : Attribute
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0001EA4D File Offset: 0x0001CC4D
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x0001EA55 File Offset: 0x0001CC55
		public string WinFormsControlType { get; set; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0001EA5E File Offset: 0x0001CC5E
		// (set) Token: 0x06001AA4 RID: 6820 RVA: 0x0001EA66 File Offset: 0x0001CC66
		public string WebFormsControlType { get; set; }

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x0001EA6F File Offset: 0x0001CC6F
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x0001EA77 File Offset: 0x0001CC77
		public bool IsDataHolding { get; set; }

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x0001EA80 File Offset: 0x0001CC80
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x0001EA88 File Offset: 0x0001CC88
		public bool IsControlCollectionStart { get; set; }

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0001EA91 File Offset: 0x0001CC91
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x0001EA99 File Offset: 0x0001CC99
		public bool IsControlCollectionEnd { get; set; }

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0001EAA2 File Offset: 0x0001CCA2
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0001EAAA File Offset: 0x0001CCAA
		public string Title { get; set; }

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0001EAB3 File Offset: 0x0001CCB3
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x0001EABB File Offset: 0x0001CCBB
		public string Description { get; set; }

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0001EAC4 File Offset: 0x0001CCC4
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0001EACC File Offset: 0x0001CCCC
		public Type PresentationDataType { get; set; }

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0001EAD5 File Offset: 0x0001CCD5
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x0001EADD File Offset: 0x0001CCDD
		public eDynamicDataStorageLocation StorageLocation { get; set; }

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0001EAE6 File Offset: 0x0001CCE6
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x0001EAEE File Offset: 0x0001CCEE
		public eDynamicControlPropertyEncryptionProperty EncryptedFlagEncryptionProperty { get; set; }

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0001EAF7 File Offset: 0x0001CCF7
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x0001EAFF File Offset: 0x0001CCFF
		public int EncryptedFlagValue { get; set; }

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0001EB08 File Offset: 0x0001CD08
		// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x0001EB10 File Offset: 0x0001CD10
		public string DynamicDataItemClass { get; set; }

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0001EB19 File Offset: 0x0001CD19
		// (set) Token: 0x06001ABA RID: 6842 RVA: 0x0001EB21 File Offset: 0x0001CD21
		public Type ValueTypeForDataTable { get; set; }

		// Token: 0x06001ABB RID: 6843 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public DynamicControlAttribute()
		{
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0001EB2A File Offset: 0x0001CD2A
		public DynamicControlAttribute(string Title, string WinFormsControlType, string WebFormsControlType, bool IsDataHolding, string Description = "")
		{
			this.Title = Title;
			this.Description = Description;
			this.WinFormsControlType = WinFormsControlType;
			this.WebFormsControlType = WebFormsControlType;
			this.IsDataHolding = IsDataHolding;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0001EB60 File Offset: 0x0001CD60
		public DynamicControlAttribute(string Title, string WinFormsControlType, string WebFormsControlType, bool IsDataHolding, bool IsControlCollectionStart, bool IsControlCollectionEnd, string Description = "")
		{
			this.Title = Title;
			this.Description = Description;
			this.WinFormsControlType = WinFormsControlType;
			this.WebFormsControlType = WebFormsControlType;
			this.IsDataHolding = IsDataHolding;
			this.IsControlCollectionEnd = IsControlCollectionEnd;
			this.IsControlCollectionStart = IsControlCollectionStart;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
		public static DynamicControlAttribute GetAttribute(eControlCode controlCode)
		{
			return DynamicControlAttribute.GetAttribute<DynamicControlAttribute>(controlCode);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
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
