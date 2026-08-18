using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000532 RID: 1330
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class CustomTypeDescriptor : ICustomTypeDescriptor
	{
		// Token: 0x06003245 RID: 12869 RVA: 0x000E18F5 File Offset: 0x000DFAF5
		protected CustomTypeDescriptor()
		{
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000E18FD File Offset: 0x000DFAFD
		protected CustomTypeDescriptor(ICustomTypeDescriptor parent)
		{
			this._parent = parent;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000E190C File Offset: 0x000DFB0C
		public virtual AttributeCollection GetAttributes()
		{
			if (this._parent != null)
			{
				return this._parent.GetAttributes();
			}
			return AttributeCollection.Empty;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000E1927 File Offset: 0x000DFB27
		public virtual string GetClassName()
		{
			if (this._parent != null)
			{
				return this._parent.GetClassName();
			}
			return null;
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000E193E File Offset: 0x000DFB3E
		public virtual string GetComponentName()
		{
			if (this._parent != null)
			{
				return this._parent.GetComponentName();
			}
			return null;
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000E1955 File Offset: 0x000DFB55
		public virtual TypeConverter GetConverter()
		{
			if (this._parent != null)
			{
				return this._parent.GetConverter();
			}
			return new TypeConverter();
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000E1970 File Offset: 0x000DFB70
		public virtual EventDescriptor GetDefaultEvent()
		{
			if (this._parent != null)
			{
				return this._parent.GetDefaultEvent();
			}
			return null;
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000E1987 File Offset: 0x000DFB87
		public virtual PropertyDescriptor GetDefaultProperty()
		{
			if (this._parent != null)
			{
				return this._parent.GetDefaultProperty();
			}
			return null;
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000E199E File Offset: 0x000DFB9E
		public virtual object GetEditor(Type editorBaseType)
		{
			if (this._parent != null)
			{
				return this._parent.GetEditor(editorBaseType);
			}
			return null;
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000E19B6 File Offset: 0x000DFBB6
		public virtual EventDescriptorCollection GetEvents()
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents();
			}
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000E19D1 File Offset: 0x000DFBD1
		public virtual EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents(attributes);
			}
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000E19ED File Offset: 0x000DFBED
		public virtual PropertyDescriptorCollection GetProperties()
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties();
			}
			return PropertyDescriptorCollection.Empty;
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000E1A08 File Offset: 0x000DFC08
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties(attributes);
			}
			return PropertyDescriptorCollection.Empty;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000E1A24 File Offset: 0x000DFC24
		public virtual object GetPropertyOwner(PropertyDescriptor pd)
		{
			if (this._parent != null)
			{
				return this._parent.GetPropertyOwner(pd);
			}
			return null;
		}

		// Token: 0x0400296D RID: 10605
		private ICustomTypeDescriptor _parent;
	}
}
