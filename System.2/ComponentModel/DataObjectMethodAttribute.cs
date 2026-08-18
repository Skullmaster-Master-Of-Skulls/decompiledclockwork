using System;

namespace System.ComponentModel
{
	// Token: 0x02000536 RID: 1334
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class DataObjectMethodAttribute : Attribute
	{
		// Token: 0x06003266 RID: 12902 RVA: 0x000E1BA7 File Offset: 0x000DFDA7
		public DataObjectMethodAttribute(DataObjectMethodType methodType) : this(methodType, false)
		{
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000E1BB1 File Offset: 0x000DFDB1
		public DataObjectMethodAttribute(DataObjectMethodType methodType, bool isDefault)
		{
			this._methodType = methodType;
			this._isDefault = isDefault;
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x000E1BC7 File Offset: 0x000DFDC7
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x000E1BCF File Offset: 0x000DFDCF
		public DataObjectMethodType MethodType
		{
			get
			{
				return this._methodType;
			}
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000E1BD8 File Offset: 0x000DFDD8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectMethodAttribute dataObjectMethodAttribute = obj as DataObjectMethodAttribute;
			return dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == this.MethodType && dataObjectMethodAttribute.IsDefault == this.IsDefault;
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000E1C14 File Offset: 0x000DFE14
		public override int GetHashCode()
		{
			int methodType = (int)this._methodType;
			return methodType.GetHashCode() ^ this._isDefault.GetHashCode();
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000E1C3C File Offset: 0x000DFE3C
		public override bool Match(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectMethodAttribute dataObjectMethodAttribute = obj as DataObjectMethodAttribute;
			return dataObjectMethodAttribute != null && dataObjectMethodAttribute.MethodType == this.MethodType;
		}

		// Token: 0x04002977 RID: 10615
		private bool _isDefault;

		// Token: 0x04002978 RID: 10616
		private DataObjectMethodType _methodType;
	}
}
