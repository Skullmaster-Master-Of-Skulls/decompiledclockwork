using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200036D RID: 877
	internal class SpecialFolderEnumConverter : AlphaSortedEnumConverter
	{
		// Token: 0x060038BA RID: 14522 RVA: 0x000FC484 File Offset: 0x000FA684
		public SpecialFolderEnumConverter(Type type) : base(type)
		{
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000FC490 File Offset: 0x000FA690
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
			ArrayList arrayList = new ArrayList();
			int count = standardValues.Count;
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				if (standardValues[i] is Environment.SpecialFolder && standardValues[i].Equals(Environment.SpecialFolder.Personal))
				{
					if (!flag)
					{
						flag = true;
						arrayList.Add(standardValues[i]);
					}
				}
				else
				{
					arrayList.Add(standardValues[i]);
				}
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}
	}
}
