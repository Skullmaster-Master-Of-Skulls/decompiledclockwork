using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005B0 RID: 1456
	public sealed class WebPartTransformerCollection : CollectionBase
	{
		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x000F4D69 File Offset: 0x000F2F69
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x170015A3 RID: 5539
		public WebPartTransformer this[int index]
		{
			get
			{
				return (WebPartTransformer)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x000170DB File Offset: 0x000152DB
		public int Add(WebPartTransformer transformer)
		{
			return base.List.Add(transformer);
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x000F4D84 File Offset: 0x000F2F84
		private void CheckReadOnly()
		{
			if (this._readOnly)
			{
				throw new InvalidOperationException(SR.GetString("WebPartTransformerCollection_ReadOnly"));
			}
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x00017184 File Offset: 0x00015384
		public bool Contains(WebPartTransformer transformer)
		{
			return base.List.Contains(transformer);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x00017192 File Offset: 0x00015392
		public void CopyTo(WebPartTransformer[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x000171A1 File Offset: 0x000153A1
		public int IndexOf(WebPartTransformer transformer)
		{
			return base.List.IndexOf(transformer);
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x000171AF File Offset: 0x000153AF
		public void Insert(int index, WebPartTransformer transformer)
		{
			base.List.Insert(index, transformer);
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x000F4D9E File Offset: 0x000F2F9E
		protected override void OnClear()
		{
			this.CheckReadOnly();
			base.OnClear();
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x000F4DAC File Offset: 0x000F2FAC
		protected override void OnInsert(int index, object value)
		{
			this.CheckReadOnly();
			if (base.List.Count > 0)
			{
				throw new InvalidOperationException(SR.GetString("WebPartTransformerCollection_NotEmpty"));
			}
			base.OnInsert(index, value);
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x000F4DDA File Offset: 0x000F2FDA
		protected override void OnRemove(int index, object value)
		{
			this.CheckReadOnly();
			base.OnRemove(index, value);
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x000F4DEA File Offset: 0x000F2FEA
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.CheckReadOnly();
			base.OnSet(index, oldValue, newValue);
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x000F4DFC File Offset: 0x000F2FFC
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.GetString("Collection_CantAddNull"));
			}
			if (!(value is WebPartTransformer))
			{
				throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
				{
					"WebPartTransformer"
				}), "value");
			}
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x000171BE File Offset: 0x000153BE
		public void Remove(WebPartTransformer transformer)
		{
			base.List.Remove(transformer);
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x000F4E53 File Offset: 0x000F3053
		internal void SetReadOnly()
		{
			this._readOnly = true;
		}

		// Token: 0x040027B5 RID: 10165
		private bool _readOnly;
	}
}
