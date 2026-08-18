using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TechnoPro.Common.DataStructure
{
	// Token: 0x02000004 RID: 4
	public class DynamicClassFields : ICustomTypeDescriptor
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002429 File Offset: 0x00000629
		public DynamicClassFields()
		{
			this.args = new Dictionary<string, object>();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000243C File Offset: 0x0000063C
		public DynamicClassFields(Dictionary<string, object> args)
		{
			this.args = args;
		}

		// Token: 0x17000003 RID: 3
		public object this[string name]
		{
			get
			{
				return this.args[name];
			}
			set
			{
				this.args[name] = value;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002468 File Offset: 0x00000668
		public bool ContainsKey(string key)
		{
			return this.args.ContainsKey(key);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002476 File Offset: 0x00000676
		public Dictionary<string, object>.KeyCollection Keys
		{
			get
			{
				return this.args.Keys;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002483 File Offset: 0x00000683
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000248B File Offset: 0x0000068B
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000248B File Offset: 0x0000068B
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000248B File Offset: 0x0000068B
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000248B File Offset: 0x0000068B
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000248B File Offset: 0x0000068B
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000248B File Offset: 0x0000068B
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000248E File Offset: 0x0000068E
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000248E File Offset: 0x0000068E
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002498 File Offset: 0x00000698
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (string key in this.Keys)
			{
				list.Add(new TestResultPropertyDescriptor(key));
			}
			foreach (object obj in TypeDescriptor.GetProperties(base.GetType(), attributes))
			{
				PropertyDescriptor item = (PropertyDescriptor)obj;
				list.Add(item);
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002554 File Offset: 0x00000754
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000255D File Offset: 0x0000075D
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000004 RID: 4
		private Dictionary<string, object> args;
	}
}
