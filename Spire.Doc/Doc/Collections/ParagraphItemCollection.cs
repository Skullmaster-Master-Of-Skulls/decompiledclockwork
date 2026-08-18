using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x02000534 RID: 1332
	public class ParagraphItemCollection : DocumentObjectCollection
	{
		// Token: 0x1700053A RID: 1338
		public ParagraphBase this[int index]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base.InnerList[index] as ParagraphBase;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x0040905C File Offset: 0x0040805C
		protected Paragraph OwnerParagraph
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base.Owner as Paragraph;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06004580 RID: 17792 RVA: 0x004090A4 File Offset: 0x004080A4
		protected override Type[] TypesOfElement
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return ParagraphItemCollection.ᜀ;
			}
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x004090E4 File Offset: 0x004080E4
		public ParagraphItemCollection(Document doc) : base(doc)
		{
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x004090F8 File Offset: 0x004080F8
		internal ParagraphItemCollection(Paragraph A_0) : base(A_0.Document, A_0)
		{
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x00409114 File Offset: 0x00408114
		internal ParagraphItemCollection(spr\u1AD2 A_0) : base(A_0.Document, A_0)
		{
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x00409130 File Offset: 0x00408130
		internal new void ᜀ(ParagraphItemCollection A_0)
		{
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_BB;
					case 1:
						goto IL_BB;
					case 2:
						return;
					case 3:
						goto IL_6E;
					case 4:
					{
						if (true)
						{
						}
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						ParagraphBase paragraphBase = (ParagraphBase)this[num].Clone();
						num2 = 6;
						continue;
					}
					case 5:
						goto IL_3A;
					case 6:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase != null)
						{
							num2 = 3;
							continue;
						}
						goto IL_3A;
					}
					}
					break;
					IL_3A:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_6E:
						ParagraphBase paragraphBase;
						paragraphBase.ᜀ(A_0.Owner);
						A_0.ᜀ(paragraphBase);
						num2 = 5;
						continue;
					}
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					IL_BB:
					num2 = 4;
				}
			}
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x0040921C File Offset: 0x0040821C
		internal new void ᜀ(int A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.InnerList.RemoveAt(A_0);
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x00409264 File Offset: 0x00408264
		internal new void ᜀ(ParagraphBase A_0)
		{
			Field field;
			for (;;)
			{
				base.InnerList.Add(A_0);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 12;
						continue;
					case 1:
						goto IL_1E8;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 3:
						num = 8;
						continue;
					case 4:
						if (base.Document != null)
						{
							num = 9;
							continue;
						}
						return;
					case 5:
						goto IL_A8;
					case 6:
						if ((A_0 as Field).End != null)
						{
							num = 1;
							continue;
						}
						goto IL_AA;
					case 7:
						goto IL_1A9;
					case 8:
						if (base.Document.ClonedFields.Count > 0)
						{
							num = 11;
							continue;
						}
						return;
					case 9:
						num = 14;
						continue;
					case 10:
						if ((A_0 as FieldMark).Type == FieldMarkType.FieldSeparator)
						{
							num = 5;
							continue;
						}
						field = base.Document.ClonedFields.Pop();
						field.End = (A_0 as FieldMark);
						num = 7;
						continue;
					case 11:
						field = base.Document.ClonedFields.Peek();
						num = 10;
						continue;
					case 12:
						if (A_0 is Field)
						{
							num = 2;
							continue;
						}
						goto IL_AA;
					case 13:
						goto IL_BD;
					case 14:
						if (!base.Document.ᜇ)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
					IL_AA:
					if (true)
					{
					}
					num = 13;
					continue;
					IL_BD:
					if (!(A_0 is FieldMark))
					{
						return;
					}
					num = 3;
				}
			}
			IL_A8:
			field.Separator = (A_0 as FieldMark);
			return;
			IL_1A9:
			return;
			IL_1E8:
			base.Document.ClonedFields.Push(A_0 as Field);
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x0040945C File Offset: 0x0040845C
		protected override void OnInsertComplete(int index, DocumentObject entity)
		{
			ParagraphBase paragraphBase;
			int itemPos;
			for (;;)
			{
				base.OnInsertComplete(index, entity);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 10;
						continue;
					case 1:
						if (base.Joined)
						{
							num = 12;
							continue;
						}
						goto IL_65;
					case 2:
						(entity as ParagraphBase).ParaItemCharFormat.ApplyBase((entity.Owner as MergeField).CharacterFormat.BaseFormat);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_119;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 3:
						if (true)
						{
						}
						num = 5;
						continue;
					case 4:
						goto IL_181;
					case 5:
						if (entity.Owner is MergeField)
						{
							num = 2;
							continue;
						}
						return;
					case 6:
						paragraphBase = (ParagraphBase)entity;
						itemPos = 0;
						num = 9;
						continue;
					case 7:
						itemPos = this[index - 1].EndPos;
						num = 4;
						continue;
					case 8:
						goto IL_114;
					case 9:
						if (index > 0)
						{
							num = 7;
							continue;
						}
						goto IL_8D;
					case 10:
						if (this.OwnerParagraph != null)
						{
							num = 6;
							continue;
						}
						goto IL_65;
					case 11:
						if (base.Joined)
						{
							num = 3;
							continue;
						}
						return;
					case 12:
						goto IL_119;
					case 13:
						if (entity.Owner != null)
						{
							num = 0;
							continue;
						}
						goto IL_65;
					}
					break;
					IL_65:
					num = 11;
					continue;
					IL_119:
					num = 13;
				}
			}
			IL_8D:
			paragraphBase.Attach(this.OwnerParagraph, itemPos);
			return;
			IL_114:
			return;
			IL_181:
			goto IL_8D;
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x00409618 File Offset: 0x00408618
		protected override void OnRemove(int index)
		{
			DocumentObject entity;
			for (;;)
			{
				entity = this[index];
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_54;
					case 1:
						if (base.Joined)
						{
							num = 2;
							continue;
						}
						goto IL_54;
					case 2:
						goto IL_36;
					}
					break;
					IL_36:
					this[index].Detach();
					if (true)
					{
					}
					num = 0;
					continue;
					IL_54:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						goto IL_6A;
					}
				}
			}
			IL_6A:
			if (false)
			{
			}
			base.OnRemove(base.IndexOf(entity));
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x004096AC File Offset: 0x004086AC
		protected override void OnClear()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_84;
				case 1:
					goto IL_6B;
				case 2:
				{
					int num2;
					if (num2 >= base.Count)
					{
						num = 0;
						continue;
					}
					this[num2].Detach();
					num2++;
					num = 3;
					continue;
				}
				case 3:
					goto IL_6B;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_33;
					default:
					{
						if (false)
						{
						}
						int num2 = 0;
						num = 1;
						continue;
					}
					}
					break;
				}
				goto IL_28;
				IL_33:
				if (true)
				{
				}
				num = 5;
				continue;
				IL_28:
				if (base.Joined)
				{
					goto IL_33;
				}
				break;
				IL_6B:
				num = 2;
			}
			IL_84:
			base.OnClear();
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x00409774 File Offset: 0x00408774
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
		{
			Enum @enum;
			for (;;)
			{
				bool flag = reader.ParseElementType(typeof(ParagraphItemType), out @enum);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4D;
					case 1:
						if (!flag)
						{
							num = 2;
							continue;
						}
						goto IL_4D;
					case 2:
						goto IL_3C;
					}
					break;
					IL_3C:
					@enum = ParagraphItemType.TextRange;
					num = 0;
					continue;
					IL_4D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						goto IL_63;
					}
				}
			}
			IL_63:
			if (true)
			{
			}
			if (false)
			{
			}
			return base.Document.CreateParagraphItem((ParagraphItemType)@enum);
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x00409810 File Offset: 0x00408810
		protected override string GetTagItemName()
		{
			int a_ = 18;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return ClipboardData.b("ᅷ๹᥻፽", a_);
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x00409864 File Offset: 0x00408864
		// Note: this type is marked as 'beforefieldinit'.
		static ParagraphItemCollection()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			ParagraphItemCollection.ᜀ = new Type[]
			{
				typeof(ParagraphBase)
			};
		}

		// Token: 0x04003667 RID: 13927
		private float \u25D8\u0087\u00AE\u0085;

		// Token: 0x04003668 RID: 13928
		private float[] \u2593\u00A8\u009F\u0084;

		// Token: 0x04003669 RID: 13929
		private long \u2593\u00A7\u009D\u0092;

		// Token: 0x0400366A RID: 13930
		private float \u2460\u009C\u00A5\u0080;

		// Token: 0x0400366B RID: 13931
		private string \u2609\u0092\u008E\u0082;

		// Token: 0x0400366C RID: 13932
		private new static readonly Type[] ᜀ;
	}
}
