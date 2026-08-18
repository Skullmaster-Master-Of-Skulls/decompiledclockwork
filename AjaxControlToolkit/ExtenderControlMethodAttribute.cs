using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000092 RID: 146
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ExtenderControlMethodAttribute : Attribute
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0000CD40 File Offset: 0x0000AF40
		public ExtenderControlMethodAttribute() : this(true)
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000CD49 File Offset: 0x0000AF49
		public ExtenderControlMethodAttribute(bool isScriptMethod)
		{
			this._isScriptMethod = isScriptMethod;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000CD58 File Offset: 0x0000AF58
		public bool IsScriptMethod
		{
			get
			{
				return this._isScriptMethod;
			}
		}

		// Token: 0x040002A1 RID: 673
		private bool _isScriptMethod;
	}
}
