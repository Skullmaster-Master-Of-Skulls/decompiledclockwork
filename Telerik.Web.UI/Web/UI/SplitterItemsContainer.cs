using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200088F RID: 2191
	[PersistChildren(true)]
	[ParseChildren(false)]
	public abstract class SplitterItemsContainer : RadWebControl
	{
		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x06005162 RID: 20834 RVA: 0x000FD4C0 File Offset: 0x000FB6C0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("The collection of items in the splitter")]
		public SplitterItemsCollection Items
		{
			get
			{
				return (SplitterItemsCollection)this.Controls;
			}
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x000FD4D0 File Offset: 0x000FB6D0
		public RadPane GetParentPane()
		{
			Control parent = this.Parent;
			RadPane radPane = parent as RadPane;
			while (parent != null && radPane == null)
			{
				parent = parent.Parent;
				radPane = (parent as RadPane);
			}
			return radPane;
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x000FD504 File Offset: 0x000FB704
		public SplitterItem GetItemById(string itemId)
		{
			foreach (object obj in this.Items)
			{
				SplitterItem splitterItem = (SplitterItem)obj;
				if (splitterItem.ID.Equals(itemId))
				{
					return splitterItem;
				}
			}
			return null;
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x000FD56C File Offset: 0x000FB76C
		protected override ControlCollection CreateControlCollection()
		{
			return new SplitterItemsCollection(this);
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x000FD574 File Offset: 0x000FB774
		protected override void AddParsedSubObject(object obj)
		{
			SplitterItem splitterItem = obj as SplitterItem;
			if (splitterItem != null)
			{
				this.Items.Add(splitterItem);
			}
		}

		// Token: 0x06005167 RID: 20839
		protected abstract void RegisterInitializeScriptWithScriptManager();

		// Token: 0x06005168 RID: 20840 RVA: 0x000FD598 File Offset: 0x000FB798
		protected virtual void Page_PreRenderComplete(object sender, EventArgs e)
		{
			if (this.Page != null && this.Page.Form != null && this.RegisterWithScriptManager && base.ScriptManager != null && !base.ScriptManager.LoadScriptsBeforeUI)
			{
				this.RegisterInitializeScriptWithScriptManager();
				foreach (object obj in this.Items)
				{
					SplitterItem splitterItem = (SplitterItem)obj;
					if (splitterItem != null)
					{
						splitterItem.RegisterInitializeScriptWithScriptManager();
					}
				}
			}
		}
	}
}
