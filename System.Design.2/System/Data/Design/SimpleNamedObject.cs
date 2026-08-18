using System;

namespace System.Data.Design
{
	// Token: 0x0200025F RID: 607
	internal class SimpleNamedObject : INamedObject
	{
		// Token: 0x0600175D RID: 5981 RVA: 0x000814C8 File Offset: 0x0007F6C8
		public SimpleNamedObject(object obj)
		{
			this._obj = obj;
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x000814D8 File Offset: 0x0007F6D8
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00081527 File Offset: 0x0007F727
		public string Name
		{
			get
			{
				if (this._obj is INamedObject)
				{
					return (this._obj as INamedObject).Name;
				}
				if (this._obj is string)
				{
					return this._obj as string;
				}
				return this._obj.ToString();
			}
			set
			{
				if (this._obj is INamedObject)
				{
					(this._obj as INamedObject).Name = value;
					return;
				}
				if (this._obj is string)
				{
					this._obj = value;
				}
			}
		}

		// Token: 0x04000BF1 RID: 3057
		private object _obj;
	}
}
