using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020002D8 RID: 728
	internal class ListViewGroupConverter : TypeConverter
	{
		// Token: 0x06002E12 RID: 11794 RVA: 0x000D140B File Offset: 0x000CF60B
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(string) && context != null && context.Instance is ListViewItem) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000D143C File Offset: 0x000CF63C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || (destinationType == typeof(string) && context != null && context.Instance is ListViewItem) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000D148C File Offset: 0x000CF68C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string b = ((string)value).Trim();
				if (context != null && context.Instance != null)
				{
					ListViewItem listViewItem = context.Instance as ListViewItem;
					if (listViewItem != null && listViewItem.ListView != null)
					{
						foreach (object obj in listViewItem.ListView.Groups)
						{
							ListViewGroup listViewGroup = (ListViewGroup)obj;
							if (listViewGroup.Header == b)
							{
								return listViewGroup;
							}
						}
					}
				}
			}
			if (value == null || value.Equals(SR.GetString("toStringNone")))
			{
				return null;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000D1554 File Offset: 0x000CF754
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is ListViewGroup)
			{
				ListViewGroup listViewGroup = (ListViewGroup)value;
				ConstructorInfo constructor = typeof(ListViewGroup).GetConstructor(new Type[]
				{
					typeof(string),
					typeof(HorizontalAlignment)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						listViewGroup.Header,
						listViewGroup.HeaderAlignment
					}, false);
				}
			}
			if (destinationType == typeof(string) && value == null)
			{
				return SR.GetString("toStringNone");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000D1624 File Offset: 0x000CF824
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null)
			{
				ListViewItem listViewItem = context.Instance as ListViewItem;
				if (listViewItem != null && listViewItem.ListView != null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in listViewItem.ListView.Groups)
					{
						ListViewGroup value = (ListViewGroup)obj;
						arrayList.Add(value);
					}
					arrayList.Add(null);
					return new TypeConverter.StandardValuesCollection(arrayList);
				}
			}
			return null;
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
