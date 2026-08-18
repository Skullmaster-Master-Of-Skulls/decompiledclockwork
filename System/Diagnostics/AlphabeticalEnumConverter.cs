using System;
using System.Collections;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x02000740 RID: 1856
	internal class AlphabeticalEnumConverter : EnumConverter
	{
		// Token: 0x060038A6 RID: 14502 RVA: 0x000EF236 File Offset: 0x000EE236
		public AlphabeticalEnumConverter(Type type) : base(type)
		{
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000EF240 File Offset: 0x000EE240
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
