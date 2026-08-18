using System;
using System.Collections;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000380 RID: 896
	public class GlyphCollection : CollectionBase
	{
		// Token: 0x060024F2 RID: 9458 RVA: 0x00057954 File Offset: 0x00055B54
		public GlyphCollection()
		{
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000E6044 File Offset: 0x000E4244
		public GlyphCollection(GlyphCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x000E6053 File Offset: 0x000E4253
		public GlyphCollection(Glyph[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x170007CC RID: 1996
		public Glyph this[int index]
		{
			get
			{
				return (Glyph)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0005799D File Offset: 0x00055B9D
		public int Add(Glyph value)
		{
			return base.List.Add(value);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000E6078 File Offset: 0x000E4278
		public void AddRange(Glyph[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000E60A0 File Offset: 0x000E42A0
		public void AddRange(GlyphCollection value)
		{
			for (int i = 0; i < value.Count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(Glyph value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x00057A55 File Offset: 0x00055C55
		public void CopyTo(Glyph[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00057A2B File Offset: 0x00055C2B
		public int IndexOf(Glyph value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00057A1C File Offset: 0x00055C1C
		public void Insert(int index, Glyph value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(Glyph value)
		{
			base.List.Remove(value);
		}
	}
}
