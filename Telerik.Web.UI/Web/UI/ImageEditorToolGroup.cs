using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000EBE RID: 3774
	[ToolboxItem(false)]
	[ParseChildren(true, "Tools")]
	public class ImageEditorToolGroup : StateManager
	{
		// Token: 0x0600900B RID: 36875 RVA: 0x00206F68 File Offset: 0x00205168
		public List<ImageEditorToolBase> GetAllTools()
		{
			List<ImageEditorToolBase> list = new List<ImageEditorToolBase>();
			foreach (object obj in this.Tools)
			{
				ImageEditorToolBase imageEditorToolBase = (ImageEditorToolBase)obj;
				list.Add(imageEditorToolBase);
				ImageEditorToolStrip imageEditorToolStrip = imageEditorToolBase as ImageEditorToolStrip;
				if (imageEditorToolStrip != null)
				{
					foreach (object obj2 in imageEditorToolStrip.Tools)
					{
						ImageEditorToolBase item = (ImageEditorToolBase)obj2;
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x0600900C RID: 36876 RVA: 0x00207028 File Offset: 0x00205228
		public ImageEditorTool FindTool(string name)
		{
			return null;
		}

		// Token: 0x0600900D RID: 36877 RVA: 0x0020702B File Offset: 0x0020522B
		public bool Contains(string name)
		{
			return this.FindTool(name) != null;
		}

		// Token: 0x17002D9E RID: 11678
		// (get) Token: 0x0600900E RID: 36878 RVA: 0x0020703A File Offset: 0x0020523A
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ImageEditorToolBaseCollection Tools
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new ImageEditorToolBaseCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._tools).TrackViewState();
					}
				}
				return this._tools;
			}
		}

		// Token: 0x0600900F RID: 36879 RVA: 0x00207068 File Offset: 0x00205268
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Tools).LoadViewState(array[1]);
		}

		// Token: 0x06009010 RID: 36880 RVA: 0x00207094 File Offset: 0x00205294
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Tools).SaveViewState()
			};
		}

		// Token: 0x06009011 RID: 36881 RVA: 0x002070C2 File Offset: 0x002052C2
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Tools).TrackViewState();
		}

		// Token: 0x06009012 RID: 36882 RVA: 0x002070D5 File Offset: 0x002052D5
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Tools.SetDirty();
		}

		// Token: 0x0400280E RID: 10254
		private ImageEditorToolBaseCollection _tools;
	}
}
