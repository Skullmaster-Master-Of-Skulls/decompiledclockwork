using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020003A2 RID: 930
	internal class TextBoxAutoCompleteSourceConverter : EnumConverter
	{
		// Token: 0x06003CE6 RID: 15590 RVA: 0x00018355 File Offset: 0x00016555
		public TextBoxAutoCompleteSourceConverter(Type type) : base(type)
		{
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x00108528 File Offset: 0x00106728
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
			ArrayList arrayList = new ArrayList();
			int count = standardValues.Count;
			for (int i = 0; i < count; i++)
			{
				string text = standardValues[i].ToString();
				if (!text.Equals("ListItems"))
				{
					arrayList.Add(standardValues[i]);
				}
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}
	}
}
