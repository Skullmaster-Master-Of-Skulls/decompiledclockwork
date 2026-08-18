using System;
using System.ComponentModel.Design;

namespace AjaxControlToolkit
{
	// Token: 0x020000AB RID: 171
	public class HtmlEditorExtenderButtonCollectionEditor : CollectionEditor
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x0000E2C1 File Offset: 0x0000C4C1
		public HtmlEditorExtenderButtonCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000E2CC File Offset: 0x0000C4CC
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(Undo),
				typeof(Redo),
				typeof(Bold),
				typeof(Italic),
				typeof(Underline),
				typeof(StrikeThrough),
				typeof(Subscript),
				typeof(Superscript),
				typeof(JustifyLeft),
				typeof(JustifyCenter),
				typeof(JustifyRight),
				typeof(JustifyFull),
				typeof(InsertOrderedList),
				typeof(InsertUnorderedList),
				typeof(RemoveFormat),
				typeof(SelectAll),
				typeof(UnSelect),
				typeof(Delete),
				typeof(Cut),
				typeof(Copy),
				typeof(Paste),
				typeof(BackgroundColorSelector),
				typeof(ForeColorSelector),
				typeof(FontNameSelector),
				typeof(FontSizeSelector),
				typeof(Indent),
				typeof(Outdent),
				typeof(InsertHorizontalRule),
				typeof(HorizontalSeparator),
				typeof(InsertImage)
			};
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000E47D File Offset: 0x0000C67D
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}
	}
}
