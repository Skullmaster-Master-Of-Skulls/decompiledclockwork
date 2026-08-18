using System;

namespace System.ComponentModel
{
	// Token: 0x02000535 RID: 1333
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DataObjectFieldAttribute : Attribute
	{
		// Token: 0x0600325C RID: 12892 RVA: 0x000E1ADD File Offset: 0x000DFCDD
		public DataObjectFieldAttribute(bool primaryKey) : this(primaryKey, false, false, -1)
		{
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000E1AE9 File Offset: 0x000DFCE9
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity) : this(primaryKey, isIdentity, false, -1)
		{
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x000E1AF5 File Offset: 0x000DFCF5
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable) : this(primaryKey, isIdentity, isNullable, -1)
		{
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x000E1B01 File Offset: 0x000DFD01
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable, int length)
		{
			this._primaryKey = primaryKey;
			this._isIdentity = isIdentity;
			this._isNullable = isNullable;
			this._length = length;
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000E1B26 File Offset: 0x000DFD26
		public bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000E1B2E File Offset: 0x000DFD2E
		public bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000E1B36 File Offset: 0x000DFD36
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000E1B3E File Offset: 0x000DFD3E
		public bool PrimaryKey
		{
			get
			{
				return this._primaryKey;
			}
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000E1B48 File Offset: 0x000DFD48
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectFieldAttribute dataObjectFieldAttribute = obj as DataObjectFieldAttribute;
			return dataObjectFieldAttribute != null && dataObjectFieldAttribute.IsIdentity == this.IsIdentity && dataObjectFieldAttribute.IsNullable == this.IsNullable && dataObjectFieldAttribute.Length == this.Length && dataObjectFieldAttribute.PrimaryKey == this.PrimaryKey;
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000E1B9F File Offset: 0x000DFD9F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04002973 RID: 10611
		private bool _primaryKey;

		// Token: 0x04002974 RID: 10612
		private bool _isIdentity;

		// Token: 0x04002975 RID: 10613
		private bool _isNullable;

		// Token: 0x04002976 RID: 10614
		private int _length;
	}
}
