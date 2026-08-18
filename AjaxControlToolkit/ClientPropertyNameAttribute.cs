using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000086 RID: 134
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ClientPropertyNameAttribute : Attribute
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x0000CA71 File Offset: 0x0000AC71
		public ClientPropertyNameAttribute(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000CA80 File Offset: 0x0000AC80
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x04000147 RID: 327
		private string _propertyName;
	}
}
