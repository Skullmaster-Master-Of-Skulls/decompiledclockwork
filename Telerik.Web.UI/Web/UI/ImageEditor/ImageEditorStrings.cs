using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E89 RID: 3721
	[ParseChildren(true)]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ImageEditorStrings : StateManager
	{
		// Token: 0x06008CFF RID: 36095 RVA: 0x002001AC File Offset: 0x001FE3AC
		internal ImageEditorStrings(RadImageEditor imageEditor)
		{
			bool isInRadEditor = imageEditor.IsInRadEditor;
			string classKey = isInRadEditor ? "RadEditor.Dialogs" : "RadImageEditor.Main";
			string classKey2 = isInRadEditor ? "RadEditor.Dialogs" : "RadImageEditor.Dialogs";
			this._main = new MainStrings(new LocalizationProvider(classKey, imageEditor, imageEditor.LocalizationPath), false, isInRadEditor);
			this._dialogs = new DialogsStrings(new LocalizationProvider(classKey2, imageEditor, imageEditor.LocalizationPath), false, isInRadEditor);
		}

		// Token: 0x17002C88 RID: 11400
		// (get) Token: 0x06008D00 RID: 36096 RVA: 0x0020021A File Offset: 0x001FE41A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MainStrings Main
		{
			get
			{
				return this._main;
			}
		}

		// Token: 0x17002C89 RID: 11401
		// (get) Token: 0x06008D01 RID: 36097 RVA: 0x00200222 File Offset: 0x001FE422
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DialogsStrings Dialogs
		{
			get
			{
				return this._dialogs;
			}
		}

		// Token: 0x06008D02 RID: 36098 RVA: 0x0020022C File Offset: 0x001FE42C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Main).LoadViewState(array[1]);
			((IStateManager)this.Dialogs).LoadViewState(array[2]);
		}

		// Token: 0x06008D03 RID: 36099 RVA: 0x00200268 File Offset: 0x001FE468
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Main).SaveViewState(),
				((IStateManager)this.Dialogs).SaveViewState()
			};
		}

		// Token: 0x06008D04 RID: 36100 RVA: 0x002002A4 File Offset: 0x001FE4A4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Main).TrackViewState();
			((IStateManager)this.Dialogs).TrackViewState();
		}

		// Token: 0x0400279A RID: 10138
		private MainStrings _main;

		// Token: 0x0400279B RID: 10139
		private DialogsStrings _dialogs;
	}
}
