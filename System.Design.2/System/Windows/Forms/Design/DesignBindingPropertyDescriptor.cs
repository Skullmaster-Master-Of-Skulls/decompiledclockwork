using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002CF RID: 719
	internal class DesignBindingPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06001C80 RID: 7296 RVA: 0x000AC1B8 File Offset: 0x000AA3B8
		internal DesignBindingPropertyDescriptor(PropertyDescriptor property, Attribute[] attrs, bool readOnly) : base(property.Name, attrs)
		{
			this.property = property;
			this.readOnly = readOnly;
			if (base.AttributeArray != null && base.AttributeArray.Length != 0)
			{
				Attribute[] array = new Attribute[this.AttributeArray.Length + 2];
				this.AttributeArray.CopyTo(array, 0);
				array[this.AttributeArray.Length - 1] = NotifyParentPropertyAttribute.Yes;
				array[this.AttributeArray.Length] = RefreshPropertiesAttribute.Repaint;
				base.AttributeArray = array;
				return;
			}
			base.AttributeArray = new Attribute[]
			{
				NotifyParentPropertyAttribute.Yes,
				RefreshPropertiesAttribute.Repaint
			};
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0008BF6B File Offset: 0x0008A16B
		public override Type ComponentType
		{
			get
			{
				return typeof(ControlBindingsCollection);
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x000AC252 File Offset: 0x000AA452
		public override TypeConverter Converter
		{
			get
			{
				return DesignBindingPropertyDescriptor.designBindingConverter;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x000AC259 File Offset: 0x000AA459
		public override bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000AC261 File Offset: 0x000AA461
		public override Type PropertyType
		{
			get
			{
				return typeof(DesignBinding);
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000AC26D File Offset: 0x000AA46D
		public override bool CanResetValue(object component)
		{
			return !DesignBindingPropertyDescriptor.GetBinding((ControlBindingsCollection)component, this.property).IsNull;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000AC288 File Offset: 0x000AA488
		public override object GetValue(object component)
		{
			return DesignBindingPropertyDescriptor.GetBinding((ControlBindingsCollection)component, this.property);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000AC29B File Offset: 0x000AA49B
		public override void ResetValue(object component)
		{
			DesignBindingPropertyDescriptor.SetBinding((ControlBindingsCollection)component, this.property, DesignBinding.Null);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000AC2B3 File Offset: 0x000AA4B3
		public override void SetValue(object component, object value)
		{
			DesignBindingPropertyDescriptor.SetBinding((ControlBindingsCollection)component, this.property, (DesignBinding)value);
			this.OnValueChanged(component, EventArgs.Empty);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000AC2D8 File Offset: 0x000AA4D8
		private static void SetBinding(ControlBindingsCollection bindings, PropertyDescriptor property, DesignBinding designBinding)
		{
			if (designBinding == null)
			{
				return;
			}
			Binding binding = bindings[property.Name];
			if (binding != null)
			{
				bindings.Remove(binding);
			}
			if (!designBinding.IsNull)
			{
				bindings.Add(property.Name, designBinding.DataSource, designBinding.DataMember);
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x000AC324 File Offset: 0x000AA524
		private static DesignBinding GetBinding(ControlBindingsCollection bindings, PropertyDescriptor property)
		{
			Binding binding = bindings[property.Name];
			if (binding == null)
			{
				return DesignBinding.Null;
			}
			return new DesignBinding(binding.DataSource, binding.BindingMemberInfo.BindingMember);
		}

		// Token: 0x040016F1 RID: 5873
		private static TypeConverter designBindingConverter = new DesignBindingConverter();

		// Token: 0x040016F2 RID: 5874
		private PropertyDescriptor property;

		// Token: 0x040016F3 RID: 5875
		private bool readOnly;
	}
}
