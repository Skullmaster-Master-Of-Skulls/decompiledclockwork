using System;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x020009AC RID: 2476
	public class AutoCompleteBoxEntryCollection : GenericStateManagedCollection<AutoCompleteBoxEntry>
	{
		// Token: 0x06005EFD RID: 24317 RVA: 0x00121E1C File Offset: 0x0012001C
		public AutoCompleteBoxEntryCollection(RadAutoCompleteBox parent)
		{
			this._parent = parent;
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x00121E2C File Offset: 0x0012002C
		public override string ToString()
		{
			if (this._parent.TextSettings.SelectionMode != RadAutoCompleteSelectionMode.Single)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in base.List)
				{
					AutoCompleteBoxEntry autoCompleteBoxEntry = (AutoCompleteBoxEntry)obj;
					stringBuilder.Append(autoCompleteBoxEntry.Text);
					stringBuilder.Append(this._parent.Delimiter);
					stringBuilder.Append(" ");
				}
				return stringBuilder.ToString();
			}
			if (base.List.Count != 0)
			{
				return (base.List[0] as AutoCompleteBoxEntry).Text;
			}
			return string.Empty;
		}

		// Token: 0x040016D8 RID: 5848
		private RadAutoCompleteBox _parent;
	}
}
