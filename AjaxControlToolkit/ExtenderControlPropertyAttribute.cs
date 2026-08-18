using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000093 RID: 147
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ExtenderControlPropertyAttribute : Attribute
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x0000CD60 File Offset: 0x0000AF60
		public ExtenderControlPropertyAttribute() : this(true)
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000CD69 File Offset: 0x0000AF69
		public ExtenderControlPropertyAttribute(bool isScriptProperty) : this(isScriptProperty, false)
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000CD73 File Offset: 0x0000AF73
		public ExtenderControlPropertyAttribute(bool isScriptProperty, bool useJsonSerialization)
		{
			this._isScriptProperty = isScriptProperty;
			this._useJsonSerialization = useJsonSerialization;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000CD89 File Offset: 0x0000AF89
		public bool IsScriptProperty
		{
			get
			{
				return this._isScriptProperty;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000CD91 File Offset: 0x0000AF91
		public bool UseJsonSerialization
		{
			get
			{
				return this._useJsonSerialization;
			}
		}

		// Token: 0x040002A2 RID: 674
		private bool _useJsonSerialization;

		// Token: 0x040002A3 RID: 675
		private bool _isScriptProperty;
	}
}
