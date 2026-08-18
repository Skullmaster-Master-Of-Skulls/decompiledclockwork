using System;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram.DataBinding
{
	// Token: 0x02000228 RID: 552
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[ParseChildren(ChildrenAsProperties = true)]
	public class BindingSettings
	{
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x000467F9 File Offset: 0x000449F9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ShapeSettings ShapeSettings
		{
			get
			{
				if (this._shape == null)
				{
					this._shape = new ShapeSettings();
				}
				return this._shape;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00046814 File Offset: 0x00044A14
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectionSettings ConnectionSettings
		{
			get
			{
				if (this._connection == null)
				{
					this._connection = new ConnectionSettings();
				}
				return this._connection;
			}
		}

		// Token: 0x0400059C RID: 1436
		private ShapeSettings _shape;

		// Token: 0x0400059D RID: 1437
		private ConnectionSettings _connection;
	}
}
