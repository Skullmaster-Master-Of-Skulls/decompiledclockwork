using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B18 RID: 2840
	[DataContract]
	[Serializable]
	public class ControlItemData
	{
		// Token: 0x06006A44 RID: 27204 RVA: 0x0018EA61 File Offset: 0x0018CC61
		public ControlItemData()
		{
			this.Text = string.Empty;
			this.Value = string.Empty;
			this.Enabled = true;
			this._attributes = new Dictionary<string, object>();
		}

		// Token: 0x170022C5 RID: 8901
		// (get) Token: 0x06006A45 RID: 27205 RVA: 0x0018EA91 File Offset: 0x0018CC91
		// (set) Token: 0x06006A46 RID: 27206 RVA: 0x0018EA99 File Offset: 0x0018CC99
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

		// Token: 0x170022C6 RID: 8902
		// (get) Token: 0x06006A47 RID: 27207 RVA: 0x0018EAA2 File Offset: 0x0018CCA2
		// (set) Token: 0x06006A48 RID: 27208 RVA: 0x0018EAAA File Offset: 0x0018CCAA
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

		// Token: 0x170022C7 RID: 8903
		// (get) Token: 0x06006A49 RID: 27209 RVA: 0x0018EAB3 File Offset: 0x0018CCB3
		// (set) Token: 0x06006A4A RID: 27210 RVA: 0x0018EABB File Offset: 0x0018CCBB
		[DataMember]
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}

		// Token: 0x170022C8 RID: 8904
		// (get) Token: 0x06006A4B RID: 27211 RVA: 0x0018EAC4 File Offset: 0x0018CCC4
		// (set) Token: 0x06006A4C RID: 27212 RVA: 0x0018EACC File Offset: 0x0018CCCC
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

		// Token: 0x04001CC0 RID: 7360
		private string _text;

		// Token: 0x04001CC1 RID: 7361
		private string _value;

		// Token: 0x04001CC2 RID: 7362
		private bool _enabled;

		// Token: 0x04001CC3 RID: 7363
		private IDictionary<string, object> _attributes;
	}
}
