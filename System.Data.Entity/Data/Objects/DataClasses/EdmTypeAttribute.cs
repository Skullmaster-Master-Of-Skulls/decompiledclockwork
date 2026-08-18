using System;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x0200018A RID: 394
	public abstract class EdmTypeAttribute : Attribute
	{
		// Token: 0x06001C2F RID: 7215 RVA: 0x0005FBC4 File Offset: 0x0005DDC4
		internal EdmTypeAttribute()
		{
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0005FC13 File Offset: 0x0005DE13
		// (set) Token: 0x06001C31 RID: 7217 RVA: 0x0005FC1B File Offset: 0x0005DE1B
		public string Name
		{
			get
			{
				return this._typeName;
			}
			set
			{
				this._typeName = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0005FC24 File Offset: 0x0005DE24
		// (set) Token: 0x06001C33 RID: 7219 RVA: 0x0005FC2C File Offset: 0x0005DE2C
		public string NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
			set
			{
				this._namespaceName = value;
			}
		}

		// Token: 0x04000BA6 RID: 2982
		private string _typeName;

		// Token: 0x04000BA7 RID: 2983
		private string _namespaceName;
	}
}
