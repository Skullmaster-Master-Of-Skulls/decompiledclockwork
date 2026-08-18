using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000394 RID: 916
	[ToolboxItem(false)]
	[SupportsEventValidation]
	internal class ChildTable : Table
	{
		// Token: 0x06002BB4 RID: 11188 RVA: 0x0008ED38 File Offset: 0x0008CF38
		internal ChildTable() : this(1)
		{
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x0008ED41 File Offset: 0x0008CF41
		internal ChildTable(int parentLevel)
		{
			this._parentLevel = parentLevel;
			this._parentIDSet = false;
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x0008ED57 File Offset: 0x0008CF57
		internal ChildTable(string parentID)
		{
			this._parentID = parentID;
			this._parentIDSet = true;
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x0008ED70 File Offset: 0x0008CF70
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			string parentID = this._parentID;
			if (!this._parentIDSet)
			{
				parentID = this.GetParentID();
			}
			if (parentID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, parentID);
			}
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x0008EDA8 File Offset: 0x0008CFA8
		private string GetParentID()
		{
			if (this.ID != null)
			{
				return null;
			}
			Control control = this;
			for (int i = 0; i < this._parentLevel; i++)
			{
				control = control.Parent;
				if (control == null)
				{
					break;
				}
			}
			if (control != null)
			{
				string id = control.ID;
				if (!string.IsNullOrEmpty(id))
				{
					return control.ClientID;
				}
			}
			return null;
		}

		// Token: 0x04001F1E RID: 7966
		private int _parentLevel;

		// Token: 0x04001F1F RID: 7967
		private string _parentID;

		// Token: 0x04001F20 RID: 7968
		private bool _parentIDSet;
	}
}
