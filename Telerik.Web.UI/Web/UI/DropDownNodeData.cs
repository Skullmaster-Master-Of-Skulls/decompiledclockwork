using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200045F RID: 1119
	[DataContract]
	[Serializable]
	public class DropDownNodeData
	{
		// Token: 0x0600286C RID: 10348 RVA: 0x00083160 File Offset: 0x00081360
		public DropDownNodeData()
		{
			this.Text = string.Empty;
			this.Value = string.Empty;
			this._attributes = new Dictionary<string, object>();
			this._expandMode = DropDownTreeNodeExpandMode.ClientSide;
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x00083190 File Offset: 0x00081390
		// (set) Token: 0x0600286E RID: 10350 RVA: 0x00083198 File Offset: 0x00081398
		[DataMember]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x000831A1 File Offset: 0x000813A1
		// (set) Token: 0x06002870 RID: 10352 RVA: 0x000831A9 File Offset: 0x000813A9
		[DataMember]
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x000831B2 File Offset: 0x000813B2
		// (set) Token: 0x06002872 RID: 10354 RVA: 0x000831BA File Offset: 0x000813BA
		[DataMember]
		public DropDownTreeNodeExpandMode ExpandMode
		{
			get
			{
				return this._expandMode;
			}
			set
			{
				this._expandMode = value;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x000831C3 File Offset: 0x000813C3
		// (set) Token: 0x06002874 RID: 10356 RVA: 0x000831CB File Offset: 0x000813CB
		[DataMember]
		public IDictionary<string, object> Attributes
		{
			get
			{
				return this._attributes;
			}
			set
			{
				this._attributes = value;
			}
		}

		// Token: 0x04000A37 RID: 2615
		private string _text;

		// Token: 0x04000A38 RID: 2616
		private string _value;

		// Token: 0x04000A39 RID: 2617
		private IDictionary<string, object> _attributes;

		// Token: 0x04000A3A RID: 2618
		private DropDownTreeNodeExpandMode _expandMode;
	}
}
