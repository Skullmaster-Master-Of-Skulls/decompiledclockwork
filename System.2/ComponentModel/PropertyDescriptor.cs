using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	// Token: 0x0200059C RID: 1436
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class PropertyDescriptor : MemberDescriptor
	{
		// Token: 0x0600353A RID: 13626 RVA: 0x000E7D7C File Offset: 0x000E5F7C
		protected PropertyDescriptor(string name, Attribute[] attrs) : base(name, attrs)
		{
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000E7D86 File Offset: 0x000E5F86
		protected PropertyDescriptor(MemberDescriptor descr) : base(descr)
		{
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000E7D8F File Offset: 0x000E5F8F
		protected PropertyDescriptor(MemberDescriptor descr, Attribute[] attrs) : base(descr, attrs)
		{
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000E7D99 File Offset: 0x000E5F99
		private object SyncObject
		{
			get
			{
				return LazyInitializer.EnsureInitialized<object>(ref this.syncObject);
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x0600353E RID: 13630
		public abstract Type ComponentType { get; }

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000E7DA8 File Offset: 0x000E5FA8
		public virtual TypeConverter Converter
		{
			get
			{
				AttributeCollection attributes = this.Attributes;
				if (this.converter == null)
				{
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)attributes[typeof(TypeConverterAttribute)];
					if (typeConverterAttribute.ConverterTypeName != null && typeConverterAttribute.ConverterTypeName.Length > 0)
					{
						Type typeFromName = this.GetTypeFromName(typeConverterAttribute.ConverterTypeName);
						if (typeFromName != null && typeof(TypeConverter).IsAssignableFrom(typeFromName))
						{
							this.converter = (TypeConverter)this.CreateInstance(typeFromName);
						}
					}
					if (this.converter == null)
					{
						this.converter = TypeDescriptor.GetConverter(this.PropertyType);
					}
				}
				return this.converter;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000E7E49 File Offset: 0x000E6049
		public virtual bool IsLocalizable
		{
			get
			{
				return LocalizableAttribute.Yes.Equals(this.Attributes[typeof(LocalizableAttribute)]);
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003541 RID: 13633
		public abstract bool IsReadOnly { get; }

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000E7E6C File Offset: 0x000E606C
		public DesignerSerializationVisibility SerializationVisibility
		{
			get
			{
				DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = (DesignerSerializationVisibilityAttribute)this.Attributes[typeof(DesignerSerializationVisibilityAttribute)];
				return designerSerializationVisibilityAttribute.Visibility;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06003543 RID: 13635
		public abstract Type PropertyType { get; }

		// Token: 0x06003544 RID: 13636 RVA: 0x000E7E9C File Offset: 0x000E609C
		public virtual void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object obj = this.SyncObject;
			lock (obj)
			{
				if (this.valueChangedHandlers == null)
				{
					this.valueChangedHandlers = new Hashtable();
				}
				EventHandler a = (EventHandler)this.valueChangedHandlers[component];
				this.valueChangedHandlers[component] = Delegate.Combine(a, handler);
			}
		}

		// Token: 0x06003545 RID: 13637
		public abstract bool CanResetValue(object component);

		// Token: 0x06003546 RID: 13638 RVA: 0x000E7F2C File Offset: 0x000E612C
		public override bool Equals(object obj)
		{
			try
			{
				if (obj == this)
				{
					return true;
				}
				if (obj == null)
				{
					return false;
				}
				PropertyDescriptor propertyDescriptor = obj as PropertyDescriptor;
				if (propertyDescriptor != null && propertyDescriptor.NameHashCode == this.NameHashCode && propertyDescriptor.PropertyType == this.PropertyType && propertyDescriptor.Name.Equals(this.Name))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000E7FA4 File Offset: 0x000E61A4
		protected object CreateInstance(Type type)
		{
			Type[] array = new Type[]
			{
				typeof(Type)
			};
			ConstructorInfo constructor = type.GetConstructor(array);
			if (constructor != null)
			{
				return TypeDescriptor.CreateInstance(null, type, array, new object[]
				{
					this.PropertyType
				});
			}
			return TypeDescriptor.CreateInstance(null, type, null, null);
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000E7FF7 File Offset: 0x000E61F7
		protected override void FillAttributes(IList attributeList)
		{
			this.converter = null;
			this.editors = null;
			this.editorTypes = null;
			this.editorCount = 0;
			base.FillAttributes(attributeList);
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000E801C File Offset: 0x000E621C
		public PropertyDescriptorCollection GetChildProperties()
		{
			return this.GetChildProperties(null, null);
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000E8026 File Offset: 0x000E6226
		public PropertyDescriptorCollection GetChildProperties(Attribute[] filter)
		{
			return this.GetChildProperties(null, filter);
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000E8030 File Offset: 0x000E6230
		public PropertyDescriptorCollection GetChildProperties(object instance)
		{
			return this.GetChildProperties(instance, null);
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000E803A File Offset: 0x000E623A
		public virtual PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
		{
			if (instance == null)
			{
				return TypeDescriptor.GetProperties(this.PropertyType, filter);
			}
			return TypeDescriptor.GetProperties(instance, filter);
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000E8054 File Offset: 0x000E6254
		public virtual object GetEditor(Type editorBaseType)
		{
			object obj = null;
			AttributeCollection attributes = this.Attributes;
			if (this.editorTypes != null)
			{
				for (int i = 0; i < this.editorCount; i++)
				{
					if (this.editorTypes[i] == editorBaseType)
					{
						return this.editors[i];
					}
				}
			}
			if (obj == null)
			{
				for (int j = 0; j < attributes.Count; j++)
				{
					EditorAttribute editorAttribute = attributes[j] as EditorAttribute;
					if (editorAttribute != null)
					{
						Type typeFromName = this.GetTypeFromName(editorAttribute.EditorBaseTypeName);
						if (editorBaseType == typeFromName)
						{
							Type typeFromName2 = this.GetTypeFromName(editorAttribute.EditorTypeName);
							if (typeFromName2 != null)
							{
								obj = this.CreateInstance(typeFromName2);
								break;
							}
						}
					}
				}
				if (obj == null)
				{
					obj = TypeDescriptor.GetEditor(this.PropertyType, editorBaseType);
				}
				if (this.editorTypes == null)
				{
					this.editorTypes = new Type[5];
					this.editors = new object[5];
				}
				if (this.editorCount >= this.editorTypes.Length)
				{
					Type[] destinationArray = new Type[this.editorTypes.Length * 2];
					object[] destinationArray2 = new object[this.editors.Length * 2];
					Array.Copy(this.editorTypes, destinationArray, this.editorTypes.Length);
					Array.Copy(this.editors, destinationArray2, this.editors.Length);
					this.editorTypes = destinationArray;
					this.editors = destinationArray2;
				}
				this.editorTypes[this.editorCount] = editorBaseType;
				object[] array = this.editors;
				int num = this.editorCount;
				this.editorCount = num + 1;
				array[num] = obj;
			}
			return obj;
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000E81C9 File Offset: 0x000E63C9
		public override int GetHashCode()
		{
			return this.NameHashCode ^ this.PropertyType.GetHashCode();
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000E81E0 File Offset: 0x000E63E0
		protected override object GetInvocationTarget(Type type, object instance)
		{
			object obj = base.GetInvocationTarget(type, instance);
			ICustomTypeDescriptor customTypeDescriptor = obj as ICustomTypeDescriptor;
			if (customTypeDescriptor != null)
			{
				obj = customTypeDescriptor.GetPropertyOwner(this);
			}
			return obj;
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000E820C File Offset: 0x000E640C
		protected Type GetTypeFromName(string typeName)
		{
			if (typeName == null || typeName.Length == 0)
			{
				return null;
			}
			Type type = Type.GetType(typeName);
			Type type2 = null;
			if (this.ComponentType != null && (type == null || this.ComponentType.Assembly.FullName.Equals(type.Assembly.FullName)))
			{
				int num = typeName.IndexOf(',');
				if (num != -1)
				{
					typeName = typeName.Substring(0, num);
				}
				type2 = this.ComponentType.Assembly.GetType(typeName);
			}
			return type2 ?? type;
		}

		// Token: 0x06003551 RID: 13649
		public abstract object GetValue(object component);

		// Token: 0x06003552 RID: 13650 RVA: 0x000E8298 File Offset: 0x000E6498
		protected virtual void OnValueChanged(object component, EventArgs e)
		{
			if (component != null && this.valueChangedHandlers != null)
			{
				EventHandler eventHandler = (EventHandler)this.valueChangedHandlers[component];
				if (eventHandler != null)
				{
					eventHandler(component, e);
				}
			}
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000E82D0 File Offset: 0x000E64D0
		public virtual void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this.valueChangedHandlers != null)
			{
				object obj = this.SyncObject;
				lock (obj)
				{
					EventHandler eventHandler = (EventHandler)this.valueChangedHandlers[component];
					eventHandler = (EventHandler)Delegate.Remove(eventHandler, handler);
					if (eventHandler != null)
					{
						this.valueChangedHandlers[component] = eventHandler;
					}
					else
					{
						this.valueChangedHandlers.Remove(component);
					}
				}
			}
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000E836C File Offset: 0x000E656C
		protected internal EventHandler GetValueChangedHandler(object component)
		{
			if (component != null && this.valueChangedHandlers != null)
			{
				return (EventHandler)this.valueChangedHandlers[component];
			}
			return null;
		}

		// Token: 0x06003555 RID: 13653
		public abstract void ResetValue(object component);

		// Token: 0x06003556 RID: 13654
		public abstract void SetValue(object component, object value);

		// Token: 0x06003557 RID: 13655
		public abstract bool ShouldSerializeValue(object component);

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x000E838C File Offset: 0x000E658C
		public virtual bool SupportsChangeEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04002A44 RID: 10820
		private TypeConverter converter;

		// Token: 0x04002A45 RID: 10821
		private Hashtable valueChangedHandlers;

		// Token: 0x04002A46 RID: 10822
		private object[] editors;

		// Token: 0x04002A47 RID: 10823
		private Type[] editorTypes;

		// Token: 0x04002A48 RID: 10824
		private int editorCount;

		// Token: 0x04002A49 RID: 10825
		private object syncObject;
	}
}
