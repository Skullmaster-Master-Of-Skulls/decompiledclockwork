using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050D RID: 1293
	internal class ImmutablePropertyDescriptorGridEntry : PropertyDescriptorGridEntry
	{
		// Token: 0x060054C6 RID: 21702 RVA: 0x00163574 File Offset: 0x00161774
		internal ImmutablePropertyDescriptorGridEntry(PropertyGrid ownerGrid, GridEntry peParent, PropertyDescriptor propInfo, bool hide) : base(ownerGrid, peParent, propInfo, hide)
		{
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x060054C7 RID: 21703 RVA: 0x00163581 File Offset: 0x00161781
		internal override bool IsPropertyReadOnly
		{
			get
			{
				return this.ShouldRenderReadOnly;
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x060054C8 RID: 21704 RVA: 0x00163589 File Offset: 0x00161789
		// (set) Token: 0x060054C9 RID: 21705 RVA: 0x00163594 File Offset: 0x00161794
		public override object PropertyValue
		{
			get
			{
				return base.PropertyValue;
			}
			set
			{
				object valueOwner = this.GetValueOwner();
				GridEntry instanceParentGridEntry = this.InstanceParentGridEntry;
				TypeConverter typeConverter = instanceParentGridEntry.TypeConverter;
				PropertyDescriptorCollection properties = typeConverter.GetProperties(instanceParentGridEntry, valueOwner);
				IDictionary dictionary = new Hashtable(properties.Count);
				object obj = null;
				for (int i = 0; i < properties.Count; i++)
				{
					if (this.propertyInfo.Name != null && this.propertyInfo.Name.Equals(properties[i].Name))
					{
						dictionary[properties[i].Name] = value;
					}
					else
					{
						dictionary[properties[i].Name] = properties[i].GetValue(valueOwner);
					}
				}
				try
				{
					obj = typeConverter.CreateInstance(instanceParentGridEntry, dictionary);
				}
				catch (Exception ex)
				{
					if (string.IsNullOrEmpty(ex.Message))
					{
						throw new TargetInvocationException(SR.GetString("ExceptionCreatingObject", new object[]
						{
							this.InstanceParentGridEntry.PropertyType.FullName,
							ex.ToString()
						}), ex);
					}
					throw;
				}
				if (obj != null)
				{
					instanceParentGridEntry.PropertyValue = obj;
				}
			}
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x001636BC File Offset: 0x001618BC
		internal override bool NotifyValueGivenParent(object obj, int type)
		{
			return this.ParentGridEntry.NotifyValue(type);
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x060054CB RID: 21707 RVA: 0x001636CA File Offset: 0x001618CA
		public override bool ShouldRenderReadOnly
		{
			get
			{
				return this.InstanceParentGridEntry.ShouldRenderReadOnly;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x060054CC RID: 21708 RVA: 0x001636D8 File Offset: 0x001618D8
		private GridEntry InstanceParentGridEntry
		{
			get
			{
				GridEntry parentGridEntry = this.ParentGridEntry;
				if (parentGridEntry is CategoryGridEntry)
				{
					parentGridEntry = parentGridEntry.ParentGridEntry;
				}
				return parentGridEntry;
			}
		}
	}
}
