using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200108F RID: 4239
	public class EditorModuleCollection : StronglyTypedStateManagedCollection<EditorModule>
	{
		// Token: 0x0600AC64 RID: 44132 RVA: 0x00250462 File Offset: 0x0024E662
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x0600AC65 RID: 44133 RVA: 0x00250470 File Offset: 0x0024E670
		public void Remove(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				int num = 0;
				while (num < base.Count && this[num].Name != name)
				{
					num++;
				}
				if (num < base.Count)
				{
					this.RemoveAt(num);
					return;
				}
			}
			throw new ArgumentException("Module not found!");
		}
	}
}
