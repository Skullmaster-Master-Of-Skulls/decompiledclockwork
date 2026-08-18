using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x020002CB RID: 715
	public class ListBindingConverter : TypeConverter
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000C4DB4 File Offset: 0x000C2FB4
		private static Type[] ConstructorParamaterTypes
		{
			get
			{
				if (ListBindingConverter.ctorTypes == null)
				{
					ListBindingConverter.ctorTypes = new Type[]
					{
						typeof(string),
						typeof(object),
						typeof(string),
						typeof(bool),
						typeof(DataSourceUpdateMode),
						typeof(object),
						typeof(string),
						typeof(IFormatProvider)
					};
				}
				return ListBindingConverter.ctorTypes;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000C4E40 File Offset: 0x000C3040
		private static string[] ConstructorParameterProperties
		{
			get
			{
				if (ListBindingConverter.ctorParamProps == null)
				{
					ListBindingConverter.ctorParamProps = new string[]
					{
						null,
						null,
						null,
						"FormattingEnabled",
						"DataSourceUpdateMode",
						"NullValue",
						"FormatString",
						"FormatInfo"
					};
				}
				return ListBindingConverter.ctorParamProps;
			}
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00027AC8 File Offset: 0x00025CC8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000C4E8C File Offset: 0x000C308C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Binding)
			{
				Binding b = (Binding)value;
				return this.GetInstanceDescriptorFromValues(b);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000C4EE4 File Offset: 0x000C30E4
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object result;
			try
			{
				result = new Binding((string)propertyValues["PropertyName"], propertyValues["DataSource"], (string)propertyValues["DataMember"]);
			}
			catch (InvalidCastException innerException)
			{
				throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"), innerException);
			}
			catch (NullReferenceException innerException2)
			{
				throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"), innerException2);
			}
			return result;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000C4F68 File Offset: 0x000C3168
		private InstanceDescriptor GetInstanceDescriptorFromValues(Binding b)
		{
			b.FormattingEnabled = true;
			bool isComplete = true;
			int num = ListBindingConverter.ConstructorParameterProperties.Length - 1;
			while (num >= 0 && ListBindingConverter.ConstructorParameterProperties[num] != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(b)[ListBindingConverter.ConstructorParameterProperties[num]];
				if (propertyDescriptor != null && propertyDescriptor.ShouldSerializeValue(b))
				{
					break;
				}
				num--;
			}
			Type[] array = new Type[num + 1];
			Array.Copy(ListBindingConverter.ConstructorParamaterTypes, 0, array, 0, array.Length);
			ConstructorInfo constructor = typeof(Binding).GetConstructor(array);
			if (constructor == null)
			{
				isComplete = false;
				constructor = typeof(Binding).GetConstructor(new Type[]
				{
					typeof(string),
					typeof(object),
					typeof(string)
				});
			}
			object[] array2 = new object[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				object obj;
				switch (i)
				{
				case 0:
					obj = b.PropertyName;
					break;
				case 1:
					obj = b.BindToObject.DataSource;
					break;
				case 2:
					obj = b.BindToObject.BindingMemberInfo.BindingMember;
					break;
				default:
					obj = TypeDescriptor.GetProperties(b)[ListBindingConverter.ConstructorParameterProperties[i]].GetValue(b);
					break;
				}
				array2[i] = obj;
			}
			return new InstanceDescriptor(constructor, array2, isComplete);
		}

		// Token: 0x0400125A RID: 4698
		private static Type[] ctorTypes;

		// Token: 0x0400125B RID: 4699
		private static string[] ctorParamProps;
	}
}
