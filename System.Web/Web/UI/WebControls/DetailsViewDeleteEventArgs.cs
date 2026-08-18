using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200055C RID: 1372
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewDeleteEventArgs : CancelEventArgs
	{
		// Token: 0x06004408 RID: 17416 RVA: 0x001197DF File Offset: 0x001187DF
		public DetailsViewDeleteEventArgs(int rowIndex) : base(false)
		{
			this._rowIndex = rowIndex;
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06004409 RID: 17417 RVA: 0x001197EF File Offset: 0x001187EF
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x001197F7 File Offset: 0x001187F7
		public IOrderedDictionary Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new OrderedDictionary();
				}
				return this._keys;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x0600440B RID: 17419 RVA: 0x00119812 File Offset: 0x00118812
		public IOrderedDictionary Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new OrderedDictionary();
				}
				return this._values;
			}
		}

		// Token: 0x04002992 RID: 10642
		private int _rowIndex;

		// Token: 0x04002993 RID: 10643
		private OrderedDictionary _keys;

		// Token: 0x04002994 RID: 10644
		private OrderedDictionary _values;
	}
}
