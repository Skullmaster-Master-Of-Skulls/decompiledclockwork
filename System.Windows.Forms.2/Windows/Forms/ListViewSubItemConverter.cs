using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020002D2 RID: 722
	internal class ListViewSubItemConverter : ExpandableObjectConverter
	{
		// Token: 0x06002CB9 RID: 11449 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000C8F88 File Offset: 0x000C7188
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is ListViewItem.ListViewSubItem)
			{
				ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)value;
				ConstructorInfo constructor;
				if (listViewSubItem.CustomStyle)
				{
					constructor = typeof(ListViewItem.ListViewSubItem).GetConstructor(new Type[]
					{
						typeof(ListViewItem),
						typeof(string),
						typeof(Color),
						typeof(Color),
						typeof(Font)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							null,
							listViewSubItem.Text,
							listViewSubItem.ForeColor,
							listViewSubItem.BackColor,
							listViewSubItem.Font
						}, true);
					}
				}
				constructor = typeof(ListViewItem.ListViewSubItem).GetConstructor(new Type[]
				{
					typeof(ListViewItem),
					typeof(string)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						null,
						listViewSubItem.Text
					}, true);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
