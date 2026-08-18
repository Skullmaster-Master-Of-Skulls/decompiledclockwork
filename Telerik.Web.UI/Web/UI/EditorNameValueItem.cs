using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02000B3E RID: 2878
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class EditorNameValueItem : StateManager
	{
		// Token: 0x06006C9D RID: 27805 RVA: 0x00193759 File Offset: 0x00191959
		public EditorNameValueItem()
		{
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x00193761 File Offset: 0x00191961
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorNameValueItem(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170023A7 RID: 9127
		// (get) Token: 0x06006C9F RID: 27807 RVA: 0x00193777 File Offset: 0x00191977
		// (set) Token: 0x06006CA0 RID: 27808 RVA: 0x001937A6 File Offset: 0x001919A6
		public virtual string Name
		{
			get
			{
				if (base.ViewState["Name"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Name"];
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x170023A8 RID: 9128
		// (get) Token: 0x06006CA1 RID: 27809 RVA: 0x001937B9 File Offset: 0x001919B9
		// (set) Token: 0x06006CA2 RID: 27810 RVA: 0x001937E8 File Offset: 0x001919E8
		public virtual string Value
		{
			get
			{
				if (base.ViewState["Value"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Value"];
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}
	}
}
