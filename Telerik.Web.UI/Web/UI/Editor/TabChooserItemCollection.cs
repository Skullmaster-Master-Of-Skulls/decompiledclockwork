using System;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D5 RID: 725
	public sealed class TabChooserItemCollection : StronglyTypedStateManagedCollection<TabChooserItem>
	{
		// Token: 0x0600193D RID: 6461 RVA: 0x00053066 File Offset: 0x00051266
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00053074 File Offset: 0x00051274
		internal string Serialize(JavaScriptSerializer serializer)
		{
			object[] array = new object[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = this[i].Name;
			}
			return serializer.Serialize(array);
		}
	}
}
