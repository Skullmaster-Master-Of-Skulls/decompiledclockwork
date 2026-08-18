using System;

namespace System.ComponentModel
{
	// Token: 0x02000534 RID: 1332
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataObjectAttribute : Attribute
	{
		// Token: 0x06003255 RID: 12885 RVA: 0x000E1A53 File Offset: 0x000DFC53
		public DataObjectAttribute() : this(true)
		{
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000E1A5C File Offset: 0x000DFC5C
		public DataObjectAttribute(bool isDataObject)
		{
			this._isDataObject = isDataObject;
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000E1A6B File Offset: 0x000DFC6B
		public bool IsDataObject
		{
			get
			{
				return this._isDataObject;
			}
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000E1A74 File Offset: 0x000DFC74
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectAttribute dataObjectAttribute = obj as DataObjectAttribute;
			return dataObjectAttribute != null && dataObjectAttribute.IsDataObject == this.IsDataObject;
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000E1AA1 File Offset: 0x000DFCA1
		public override int GetHashCode()
		{
			return this._isDataObject.GetHashCode();
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000E1AAE File Offset: 0x000DFCAE
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DataObjectAttribute.Default);
		}

		// Token: 0x0400296F RID: 10607
		public static readonly DataObjectAttribute DataObject = new DataObjectAttribute(true);

		// Token: 0x04002970 RID: 10608
		public static readonly DataObjectAttribute NonDataObject = new DataObjectAttribute(false);

		// Token: 0x04002971 RID: 10609
		public static readonly DataObjectAttribute Default = DataObjectAttribute.NonDataObject;

		// Token: 0x04002972 RID: 10610
		private bool _isDataObject;
	}
}
