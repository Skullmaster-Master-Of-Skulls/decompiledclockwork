using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000483 RID: 1155
	public class ViewCollection : ControlCollection
	{
		// Token: 0x0600393B RID: 14651 RVA: 0x00061D30 File Offset: 0x0005FF30
		public ViewCollection(Control owner) : base(owner)
		{
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000BA48A File Offset: 0x000B868A
		public override void Add(Control v)
		{
			if (!(v is View))
			{
				throw new ArgumentException(SR.GetString("ViewCollection_must_contain_view"));
			}
			base.Add(v);
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000BA4AB File Offset: 0x000B86AB
		public override void AddAt(int index, Control v)
		{
			if (!(v is View))
			{
				throw new ArgumentException(SR.GetString("ViewCollection_must_contain_view"));
			}
			base.AddAt(index, v);
		}

		// Token: 0x170010AF RID: 4271
		public View this[int i]
		{
			get
			{
				return (View)base[i];
			}
		}
	}
}
