using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B3F RID: 2879
	public class EditorFormatSet : EditorNameValueItem
	{
		// Token: 0x06006CA3 RID: 27811 RVA: 0x001937FB File Offset: 0x001919FB
		public EditorFormatSet()
		{
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x00193803 File Offset: 0x00191A03
		public EditorFormatSet(string tag, string title)
		{
			this.Tag = tag;
			this.Title = title;
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x00193819 File Offset: 0x00191A19
		public EditorFormatSet(string tag, string title, EditorFormatSetAttributeCollection attributes)
		{
			this.Tag = tag;
			this.Title = title;
			this.Attributes = attributes;
		}

		// Token: 0x170023A9 RID: 9129
		// (get) Token: 0x06006CA6 RID: 27814 RVA: 0x00193836 File Offset: 0x00191A36
		// (set) Token: 0x06006CA7 RID: 27815 RVA: 0x0019383E File Offset: 0x00191A3E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x170023AA RID: 9130
		// (get) Token: 0x06006CA8 RID: 27816 RVA: 0x00193847 File Offset: 0x00191A47
		// (set) Token: 0x06006CA9 RID: 27817 RVA: 0x0019384F File Offset: 0x00191A4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x170023AB RID: 9131
		// (get) Token: 0x06006CAA RID: 27818 RVA: 0x00193858 File Offset: 0x00191A58
		// (set) Token: 0x06006CAB RID: 27819 RVA: 0x00193860 File Offset: 0x00191A60
		public string Title
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x170023AC RID: 9132
		// (get) Token: 0x06006CAC RID: 27820 RVA: 0x00193869 File Offset: 0x00191A69
		// (set) Token: 0x06006CAD RID: 27821 RVA: 0x00193871 File Offset: 0x00191A71
		public string Tag
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x170023AD RID: 9133
		// (get) Token: 0x06006CAE RID: 27822 RVA: 0x0019387A File Offset: 0x00191A7A
		// (set) Token: 0x06006CAF RID: 27823 RVA: 0x00193895 File Offset: 0x00191A95
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EditorFormatSetAttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new EditorFormatSetAttributeCollection();
				}
				return this._attributes;
			}
			set
			{
				this._attributes = value;
			}
		}

		// Token: 0x04001D38 RID: 7480
		private EditorFormatSetAttributeCollection _attributes;
	}
}
