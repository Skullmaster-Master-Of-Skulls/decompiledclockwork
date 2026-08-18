using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C8 RID: 712
	internal class DataSourceConverter : ReferenceConverter
	{
		// Token: 0x06001C38 RID: 7224 RVA: 0x000AA1C0 File Offset: 0x000A83C0
		public DataSourceConverter() : base(typeof(IListSource))
		{
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000AA1E8 File Offset: 0x000A83E8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ArrayList arrayList = new ArrayList(base.GetStandardValues(context));
			TypeConverter.StandardValuesCollection standardValues = this.listConverter.GetStandardValues(context);
			ArrayList arrayList2 = new ArrayList();
			BindingSource bindingSource = context.Instance as BindingSource;
			foreach (object obj in arrayList)
			{
				if (obj != null)
				{
					ListBindableAttribute listBindableAttribute = (ListBindableAttribute)TypeDescriptor.GetAttributes(obj)[typeof(ListBindableAttribute)];
					if ((listBindableAttribute == null || listBindableAttribute.ListBindable) && (bindingSource == null || bindingSource != obj))
					{
						DataTable dataTable = obj as DataTable;
						if (dataTable == null || !arrayList.Contains(dataTable.DataSet))
						{
							arrayList2.Add(obj);
						}
					}
				}
			}
			foreach (object obj2 in standardValues)
			{
				if (obj2 != null)
				{
					ListBindableAttribute listBindableAttribute2 = (ListBindableAttribute)TypeDescriptor.GetAttributes(obj2)[typeof(ListBindableAttribute)];
					if ((listBindableAttribute2 == null || listBindableAttribute2.ListBindable) && (bindingSource == null || bindingSource != obj2))
					{
						arrayList2.Add(obj2);
					}
				}
			}
			arrayList2.Add(null);
			return new TypeConverter.StandardValuesCollection(arrayList2);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000AA34C File Offset: 0x000A854C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is Type)
			{
				return value.ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x040016CE RID: 5838
		private ReferenceConverter listConverter = new ReferenceConverter(typeof(IList));
	}
}
