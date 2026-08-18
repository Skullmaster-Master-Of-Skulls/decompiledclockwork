using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000370 RID: 880
	public class AccessDataSourceView : SqlDataSourceView
	{
		// Token: 0x0600288F RID: 10383 RVA: 0x000830B9 File Offset: 0x000812B9
		public AccessDataSourceView(AccessDataSource owner, string name, HttpContext context) : base(owner, name, context)
		{
			this._owner = owner;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000830CB File Offset: 0x000812CB
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			if (string.IsNullOrEmpty(this._owner.DataFile))
			{
				throw new InvalidOperationException(SR.GetString("AccessDataSourceView_SelectRequiresDataFile", new object[]
				{
					this._owner.ID
				}));
			}
			return base.ExecuteSelect(arguments);
		}

		// Token: 0x04001E01 RID: 7681
		private AccessDataSource _owner;
	}
}
