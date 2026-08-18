using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000078 RID: 120
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class TypeFieldSchema : IDataSourceFieldSchema
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0001237E File Offset: 0x0001057E
		public TypeFieldSchema(PropertyDescriptor fieldDescriptor)
		{
			if (fieldDescriptor == null)
			{
				throw new ArgumentNullException("fieldDescriptor");
			}
			this._fieldDescriptor = fieldDescriptor;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000123A4 File Offset: 0x000105A4
		public Type DataType
		{
			get
			{
				Type propertyType = this._fieldDescriptor.PropertyType;
				if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					return propertyType.GetGenericArguments()[0];
				}
				return propertyType;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000123E6 File Offset: 0x000105E6
		public bool Identity
		{
			get
			{
				this.EnsureMetaData();
				return this._isIdentity;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000123F4 File Offset: 0x000105F4
		public bool IsReadOnly
		{
			get
			{
				return this._fieldDescriptor.IsReadOnly;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000445B File Offset: 0x0000265B
		public bool IsUnique
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00012401 File Offset: 0x00010601
		public int Length
		{
			get
			{
				this.EnsureMetaData();
				return this._length;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001240F File Offset: 0x0001060F
		public string Name
		{
			get
			{
				return this._fieldDescriptor.Name;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0001241C File Offset: 0x0001061C
		public bool Nullable
		{
			get
			{
				this.EnsureMetaData();
				Type propertyType = this._fieldDescriptor.PropertyType;
				return !propertyType.IsValueType || this._isNullable || (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>));
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		public int Precision
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0001246C File Offset: 0x0001066C
		public bool PrimaryKey
		{
			get
			{
				this.EnsureMetaData();
				return this._primaryKey;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		public int Scale
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001247C File Offset: 0x0001067C
		private void EnsureMetaData()
		{
			if (this._retrievedMetaData)
			{
				return;
			}
			DataObjectFieldAttribute dataObjectFieldAttribute = (DataObjectFieldAttribute)this._fieldDescriptor.Attributes[typeof(DataObjectFieldAttribute)];
			if (dataObjectFieldAttribute != null)
			{
				this._primaryKey = dataObjectFieldAttribute.PrimaryKey;
				this._isIdentity = dataObjectFieldAttribute.IsIdentity;
				this._isNullable = dataObjectFieldAttribute.IsNullable;
				this._length = dataObjectFieldAttribute.Length;
			}
			this._retrievedMetaData = true;
		}

		// Token: 0x04000195 RID: 405
		private PropertyDescriptor _fieldDescriptor;

		// Token: 0x04000196 RID: 406
		private bool _retrievedMetaData;

		// Token: 0x04000197 RID: 407
		private bool _primaryKey;

		// Token: 0x04000198 RID: 408
		private bool _isIdentity;

		// Token: 0x04000199 RID: 409
		private bool _isNullable;

		// Token: 0x0400019A RID: 410
		private int _length = -1;
	}
}
