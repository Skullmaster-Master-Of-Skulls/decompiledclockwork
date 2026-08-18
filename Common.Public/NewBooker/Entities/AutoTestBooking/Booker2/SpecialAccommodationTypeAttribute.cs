using System;
using System.Reflection;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x0200009E RID: 158
	public class SpecialAccommodationTypeAttribute : Attribute
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000CD92 File Offset: 0x0000AF92
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000CD9A File Offset: 0x0000AF9A
		public int OrderNum { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000CDA3 File Offset: 0x0000AFA3
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000CDAB File Offset: 0x0000AFAB
		public string Id { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		public eSpecialAccommodationApplyMethod ApplyMethod { get; set; }

		// Token: 0x060003A4 RID: 932 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		public static SpecialAccommodationTypeAttribute GetAttribute(eSpecialAccommodationType specialAccommodationType)
		{
			return SpecialAccommodationTypeAttribute.GetAttribute<SpecialAccommodationTypeAttribute>(specialAccommodationType);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		private static T GetAttribute<T>(Enum item) where T : Attribute
		{
			Type type = item.GetType();
			FieldInfo field = type.GetField(item.ToString());
			T[] array = field.GetCustomAttributes(typeof(T), false) as T[];
			return (array != null && array.Length != 0) ? array[0] : default(T);
		}
	}
}
