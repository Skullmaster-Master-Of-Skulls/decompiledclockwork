using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000084 RID: 132
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public abstract class ClientResourceAttribute : Attribute
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x0000CA3B File Offset: 0x0000AC3B
		public ClientResourceAttribute(string resourcePath)
		{
			if (resourcePath == null)
			{
				throw new ArgumentNullException("resourcePath");
			}
			this._resourcePath = resourcePath;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000CA58 File Offset: 0x0000AC58
		public string ResourcePath
		{
			get
			{
				return this._resourcePath;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public int LoadOrder
		{
			get
			{
				return this._loadOrder;
			}
		}

		// Token: 0x04000145 RID: 325
		private int _loadOrder;

		// Token: 0x04000146 RID: 326
		private string _resourcePath;
	}
}
