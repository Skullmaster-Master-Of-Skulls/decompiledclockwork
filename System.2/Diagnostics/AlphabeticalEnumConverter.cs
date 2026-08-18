using System;
using System.Collections;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020004BD RID: 1213
	internal class AlphabeticalEnumConverter : EnumConverter
	{
		// Token: 0x06002D5D RID: 11613 RVA: 0x000CC4CB File Offset: 0x000CA6CB
		public AlphabeticalEnumConverter(Type type) : base(type)
		{
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000CC4D4 File Offset: 0x000CA6D4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (base.Values == null)
			{
				Array values = Enum.GetValues(base.EnumType);
				object[] array = new object[values.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.ConvertTo(context, null, values.GetValue(i), typeof(string));
				}
				Array.Sort(array, values, 0, values.Length, System.Collections.Comparer.Default);
				base.Values = new TypeConverter.StandardValuesCollection(values);
			}
			return base.Values;
		}
	}
}
