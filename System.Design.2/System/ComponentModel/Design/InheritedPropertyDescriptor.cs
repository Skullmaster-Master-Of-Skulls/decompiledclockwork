using System;
using System.Collections;
using System.Design;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel.Design
{
	// Token: 0x020001B5 RID: 437
	internal class InheritedPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x0005A214 File Offset: 0x00058414
		public InheritedPropertyDescriptor(PropertyDescriptor propertyDescriptor, object component, bool rootComponent) : base(propertyDescriptor, new Attribute[0])
		{
			this.propertyDescriptor = propertyDescriptor;
			this.InitInheritedDefaultValue(component, rootComponent);
			bool flag = false;
			if (typeof(ICollection).IsAssignableFrom(propertyDescriptor.PropertyType) && propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content))
			{
				ICollection collection = propertyDescriptor.GetValue(component) as ICollection;
				if (collection != null && collection.Count > 0)
				{
					bool flag2 = false;
					bool flag3 = false;
					foreach (MethodInfo methodInfo in TypeDescriptor.GetReflectionType(collection).GetMethods(BindingFlags.Instance | BindingFlags.Public))
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (parameters.Length == 1)
						{
							string name = methodInfo.Name;
							Type type = null;
							if (name.Equals("AddRange") && parameters[0].ParameterType.IsArray)
							{
								type = parameters[0].ParameterType.GetElementType();
							}
							else if (name.Equals("Add"))
							{
								type = parameters[0].ParameterType;
							}
							if (type != null)
							{
								if (typeof(IComponent).IsAssignableFrom(type))
								{
									flag2 = true;
									break;
								}
								flag3 = true;
							}
						}
					}
					if (flag3 && !flag2)
					{
						Attribute[] attributeArray = (Attribute[])new ArrayList(this.AttributeArray)
						{
							DesignerSerializationVisibilityAttribute.Hidden,
							ReadOnlyAttribute.Yes,
							new EditorAttribute(typeof(UITypeEditor), typeof(UITypeEditor)),
							new TypeConverterAttribute(typeof(InheritedPropertyDescriptor.ReadOnlyCollectionConverter))
						}.ToArray(typeof(Attribute));
						this.AttributeArray = attributeArray;
						flag = true;
					}
				}
			}
			if (!flag && this.defaultValue != InheritedPropertyDescriptor.noDefault)
			{
				ArrayList arrayList = new ArrayList(this.AttributeArray);
				arrayList.Add(new DefaultValueAttribute(this.defaultValue));
				Attribute[] array = new Attribute[arrayList.Count];
				arrayList.CopyTo(array, 0);
				this.AttributeArray = array;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0005A427 File Offset: 0x00058627
		public override Type ComponentType
		{
			get
			{
				return this.propertyDescriptor.ComponentType;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0005A434 File Offset: 0x00058634
		public override bool IsReadOnly
		{
			get
			{
				return this.propertyDescriptor.IsReadOnly || this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0005A464 File Offset: 0x00058664
		internal object OriginalValue
		{
			get
			{
				return this.originalValue;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0005A46C File Offset: 0x0005866C
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x0005A474 File Offset: 0x00058674
		internal PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propertyDescriptor;
			}
			set
			{
				this.propertyDescriptor = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0005A47D File Offset: 0x0005867D
		public override Type PropertyType
		{
			get
			{
				return this.propertyDescriptor.PropertyType;
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0005A48A File Offset: 0x0005868A
		public override bool CanResetValue(object component)
		{
			if (this.defaultValue == InheritedPropertyDescriptor.noDefault)
			{
				return this.propertyDescriptor.CanResetValue(component);
			}
			return !object.Equals(this.GetValue(component), this.defaultValue);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0005A4BC File Offset: 0x000586BC
		private object ClonedDefaultValue(object value)
		{
			DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = (DesignerSerializationVisibilityAttribute)this.propertyDescriptor.Attributes[typeof(DesignerSerializationVisibilityAttribute)];
			DesignerSerializationVisibility designerSerializationVisibility;
			if (designerSerializationVisibilityAttribute == null)
			{
				designerSerializationVisibility = DesignerSerializationVisibility.Visible;
			}
			else
			{
				designerSerializationVisibility = designerSerializationVisibilityAttribute.Visibility;
			}
			if (value != null && designerSerializationVisibility == DesignerSerializationVisibility.Content)
			{
				if (value is ICloneable)
				{
					value = ((ICloneable)value).Clone();
				}
				else
				{
					value = InheritedPropertyDescriptor.noDefault;
				}
			}
			return value;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0005A520 File Offset: 0x00058720
		protected override void FillAttributes(IList attributeList)
		{
			base.FillAttributes(attributeList);
			foreach (object obj in this.propertyDescriptor.Attributes)
			{
				Attribute value = (Attribute)obj;
				attributeList.Add(value);
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0005A588 File Offset: 0x00058788
		public override object GetValue(object component)
		{
			return this.propertyDescriptor.GetValue(component);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0005A598 File Offset: 0x00058798
		private void InitInheritedDefaultValue(object component, bool rootComponent)
		{
			try
			{
				object value;
				if (!this.propertyDescriptor.ShouldSerializeValue(component))
				{
					DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.propertyDescriptor.Attributes[typeof(DefaultValueAttribute)];
					if (defaultValueAttribute != null)
					{
						this.defaultValue = defaultValueAttribute.Value;
						value = this.defaultValue;
					}
					else
					{
						this.defaultValue = InheritedPropertyDescriptor.noDefault;
						value = this.propertyDescriptor.GetValue(component);
					}
				}
				else
				{
					this.defaultValue = this.propertyDescriptor.GetValue(component);
					value = this.defaultValue;
					this.defaultValue = this.ClonedDefaultValue(this.defaultValue);
				}
				this.SaveOriginalValue(value);
			}
			catch
			{
				this.defaultValue = InheritedPropertyDescriptor.noDefault;
			}
			this.initShouldSerialize = this.ShouldSerializeValue(component);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0005A664 File Offset: 0x00058864
		public override void ResetValue(object component)
		{
			if (this.defaultValue == InheritedPropertyDescriptor.noDefault)
			{
				this.propertyDescriptor.ResetValue(component);
				return;
			}
			this.SetValue(component, this.defaultValue);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0005A68D File Offset: 0x0005888D
		private void SaveOriginalValue(object value)
		{
			if (value is ICollection)
			{
				this.originalValue = new object[((ICollection)value).Count];
				((ICollection)value).CopyTo((Array)this.originalValue, 0);
				return;
			}
			this.originalValue = value;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0005A6CC File Offset: 0x000588CC
		public override void SetValue(object component, object value)
		{
			this.propertyDescriptor.SetValue(component, value);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0005A6DC File Offset: 0x000588DC
		public override bool ShouldSerializeValue(object component)
		{
			if (this.IsReadOnly)
			{
				return this.propertyDescriptor.ShouldSerializeValue(component) && this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.defaultValue == InheritedPropertyDescriptor.noDefault)
			{
				return this.propertyDescriptor.ShouldSerializeValue(component);
			}
			return !object.Equals(this.GetValue(component), this.defaultValue);
		}

		// Token: 0x0400093E RID: 2366
		private PropertyDescriptor propertyDescriptor;

		// Token: 0x0400093F RID: 2367
		private object defaultValue;

		// Token: 0x04000940 RID: 2368
		private static object noDefault = new object();

		// Token: 0x04000941 RID: 2369
		private bool initShouldSerialize;

		// Token: 0x04000942 RID: 2370
		private object originalValue;

		// Token: 0x02000491 RID: 1169
		private class ReadOnlyCollectionConverter : TypeConverter
		{
			// Token: 0x06002B1F RID: 11039 RVA: 0x00102618 File Offset: 0x00100818
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return SR.GetString("InheritanceServiceReadOnlyCollection");
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
