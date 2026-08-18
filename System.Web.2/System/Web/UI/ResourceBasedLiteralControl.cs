using System;

namespace System.Web.UI
{
	// Token: 0x020002C1 RID: 705
	internal sealed class ResourceBasedLiteralControl : LiteralControl
	{
		// Token: 0x06001FEB RID: 8171 RVA: 0x000658A4 File Offset: 0x00063AA4
		internal ResourceBasedLiteralControl(TemplateControl tplControl, int offset, int size, bool fAsciiOnly)
		{
			if (offset < 0 || offset + size > tplControl.MaxResourceOffset)
			{
				throw new ArgumentException();
			}
			this._tplControl = tplControl;
			this._offset = offset;
			this._size = size;
			this._fAsciiOnly = fAsciiOnly;
			base.PreventAutoID();
			this.EnableViewState = false;
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x000658F6 File Offset: 0x00063AF6
		// (set) Token: 0x06001FED RID: 8173 RVA: 0x00065923 File Offset: 0x00063B23
		public override string Text
		{
			get
			{
				if (this._size == 0)
				{
					return base.Text;
				}
				return StringResourceManager.ResourceToString(this._tplControl.StringResourcePointer, this._offset, this._size);
			}
			set
			{
				this._size = 0;
				base.Text = value;
			}
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x00065933 File Offset: 0x00063B33
		protected internal override void Render(HtmlTextWriter output)
		{
			if (this._size == 0)
			{
				base.Render(output);
				return;
			}
			output.WriteUTF8ResourceString(this._tplControl.StringResourcePointer, this._offset, this._size, this._fAsciiOnly);
		}

		// Token: 0x04001ABB RID: 6843
		private TemplateControl _tplControl;

		// Token: 0x04001ABC RID: 6844
		private int _offset;

		// Token: 0x04001ABD RID: 6845
		private int _size;

		// Token: 0x04001ABE RID: 6846
		private bool _fAsciiOnly;
	}
}
