using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B7A RID: 7034
	internal class RequestStateStore
	{
		// Token: 0x1700532E RID: 21294
		// (get) Token: 0x0601109F RID: 69791 RVA: 0x003C2D0C File Offset: 0x003C0F0C
		// (set) Token: 0x060110A0 RID: 69792 RVA: 0x003C2D14 File Offset: 0x003C0F14
		public bool UploadComplete
		{
			get
			{
				return this._uploadComplete;
			}
			set
			{
				this._uploadComplete = value;
			}
		}

		// Token: 0x1700532F RID: 21295
		// (get) Token: 0x060110A1 RID: 69793 RVA: 0x003C2D1D File Offset: 0x003C0F1D
		public int CurrentRequestBytesCount
		{
			get
			{
				return this._currentRequestBytesCount;
			}
		}

		// Token: 0x060110A2 RID: 69794 RVA: 0x003C2D25 File Offset: 0x003C0F25
		public RequestStateStore(Encoding encoding)
		{
			this._encoding = encoding;
		}

		// Token: 0x17005330 RID: 21296
		// (get) Token: 0x060110A3 RID: 69795 RVA: 0x003C2D34 File Offset: 0x003C0F34
		public List<RequestField> Fields
		{
			get
			{
				if (this._fields == null)
				{
					this._fields = new List<RequestField>();
				}
				return this._fields;
			}
		}

		// Token: 0x17005331 RID: 21297
		// (get) Token: 0x060110A4 RID: 69796 RVA: 0x003C2D4F File Offset: 0x003C0F4F
		public bool HasOpenField
		{
			get
			{
				return this._currentField != null;
			}
		}

		// Token: 0x17005332 RID: 21298
		// (get) Token: 0x060110A5 RID: 69797 RVA: 0x003C2D5D File Offset: 0x003C0F5D
		public RequestField LastHeaderCompleteField
		{
			get
			{
				if (this.Fields.Count > 0)
				{
					return this.Fields[this.Fields.Count - 1];
				}
				return null;
			}
		}

		// Token: 0x060110A6 RID: 69798 RVA: 0x003C2D88 File Offset: 0x003C0F88
		public void Record(byte[] fieldContent, bool isFinal)
		{
			if (this._currentField == null)
			{
				this._currentField = new RequestField(this._encoding);
			}
			bool flag = this._currentField.Header != null;
			this._currentField.AddData(fieldContent, isFinal);
			if (!flag && this._currentField.Header != null)
			{
				this.Fields.Add(this._currentField);
			}
			if (isFinal)
			{
				this._currentField = null;
			}
		}

		// Token: 0x060110A7 RID: 69799 RVA: 0x003C2DF8 File Offset: 0x003C0FF8
		public void UpdateCurrentRequestBytesCount(int parsedBytesCount)
		{
			this._currentRequestBytesCount += parsedBytesCount;
		}

		// Token: 0x04004C40 RID: 19520
		private int _currentRequestBytesCount;

		// Token: 0x04004C41 RID: 19521
		private Encoding _encoding;

		// Token: 0x04004C42 RID: 19522
		private List<RequestField> _fields;

		// Token: 0x04004C43 RID: 19523
		private RequestField _currentField;

		// Token: 0x04004C44 RID: 19524
		private bool _uploadComplete;
	}
}
