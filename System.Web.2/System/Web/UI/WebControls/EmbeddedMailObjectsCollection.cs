using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003F1 RID: 1009
	[Editor("System.Web.UI.Design.WebControls.EmbeddedMailObjectCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class EmbeddedMailObjectsCollection : CollectionBase
	{
		// Token: 0x17000E12 RID: 3602
		public EmbeddedMailObject this[int index]
		{
			get
			{
				return (EmbeddedMailObject)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000170DB File Offset: 0x000152DB
		public int Add(EmbeddedMailObject value)
		{
			return base.List.Add(value);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x00017184 File Offset: 0x00015384
		public bool Contains(EmbeddedMailObject value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x00017192 File Offset: 0x00015392
		public void CopyTo(EmbeddedMailObject[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000171A1 File Offset: 0x000153A1
		public int IndexOf(EmbeddedMailObject value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000171AF File Offset: 0x000153AF
		public void Insert(int index, EmbeddedMailObject value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x0009EC8C File Offset: 0x0009CE8C
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.GetString("Collection_CantAddNull"));
			}
			if (!(value is EmbeddedMailObject))
			{
				throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
				{
					"EmbeddedMailObject"
				}), "value");
			}
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000171BE File Offset: 0x000153BE
		public void Remove(EmbeddedMailObject value)
		{
			base.List.Remove(value);
		}
	}
}
