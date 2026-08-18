using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000087 RID: 135
	public sealed class ClientScriptResourceAttribute : ClientResourceAttribute
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public ClientScriptResourceAttribute(string componentType, string resourcePath) : base(resourcePath)
		{
			this._componentType = componentType;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000CA98 File Offset: 0x0000AC98
		public string ComponentType
		{
			get
			{
				return this._componentType;
			}
		}

		// Token: 0x04000148 RID: 328
		private string _componentType;
	}
}
