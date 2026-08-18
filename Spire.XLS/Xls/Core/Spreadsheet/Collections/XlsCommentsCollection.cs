using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200003D RID: 61
	public class XlsCommentsCollection : CollectionExtended<XlsComment>, IComments
	{
		// Token: 0x17000157 RID: 343
		public ICommentShape this[int index]
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
				return base.InnerList[index];
			}
		}

		// Token: 0x17000158 RID: 344
		public ICommentShape this[int iRow, int iColumn]
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
				long key = sprṔ.ᜀ(iColumn, iRow);
				ICommentShape result;
				this.HashComments.TryGetValue(key, out result);
				return result;
			}
		}

		// Token: 0x17000159 RID: 345
		public ICommentShape this[string name]
		{
			get
			{
				switch (0)
				{
				default:
				{
					ICommentShape result;
					for (;;)
					{
						result = null;
						int num = 0;
						int count = base.Count;
						int num2 = 6;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								ICommentShape commentShape;
								if (commentShape.Name == name)
								{
									num2 = 3;
									continue;
								}
								num++;
								num2 = 4;
								continue;
							}
							case 1:
								if (num < count)
								{
									ICommentShape commentShape = this[num];
									num2 = 0;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return result;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									num2 = 2;
									continue;
								}
								break;
							case 2:
								return result;
							case 3:
							{
								ICommentShape commentShape;
								result = commentShape;
								num2 = 5;
								continue;
							}
							case 4:
								goto IL_9C;
							case 5:
								return result;
							case 6:
								goto IL_9C;
							}
							break;
							IL_9C:
							num2 = 1;
						}
					}
					return result;
				}
				}
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00025684 File Offset: 0x00024684
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x000256C8 File Offset: 0x000246C8
		public bool ReRegisterOnAccess
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
				return this.ᜆ;
			}
			set
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
				this.ᜆ = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0002570C File Offset: 0x0002470C
		private Dictionary<long, ICommentShape> HashComments
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜂ();
							num = 0;
							continue;
						}
						break;
					}
					IL_1C:
					if (true)
					{
					}
					if (this.ᜆ)
					{
						num = 1;
						continue;
					}
					break;
					goto IL_1C;
				}
				IL_6A:
				return this.ᜅ;
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0002578C File Offset: 0x0002478C
		internal XlsCommentsCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000257B4 File Offset: 0x000247B4
		protected internal ICommentShape AddComment(IXLSRange parentRange)
		{
			int a_ = 0;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_6E;
				case 2:
					goto IL_3C;
				case 3:
					while (!((XlsRange)parentRange).IsSingleCell)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_A4;
						}
					}
					num = 1;
					continue;
				}
				if (parentRange == null)
				{
					if (true)
					{
					}
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䘵夷䠹夻倽㐿၁╃⡅⽇⽉", a_));
			IL_6E:
			return this.AddComment(parentRange.Row, parentRange.Column);
			IL_A4:
			if (false)
			{
			}
			return null;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0002586C File Offset: 0x0002486C
		public ICommentShape AddComment(int iRow, int iColumn)
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
			return this.AddComment(iRow, iColumn, true);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000258B0 File Offset: 0x000248B0
		public ICommentShape AddComment(int iRow, int iColumn, bool bIsParseOptions)
		{
			switch (0)
			{
			default:
			{
				XlsComment xlsComment;
				for (;;)
				{
					xlsComment = (this.ᜄ.Shapes.AddComment(string.Empty, bIsParseOptions) as XlsComment);
					xlsComment.Column = iColumn;
					xlsComment.Row = iRow;
					sprᮋ sprᮋ = xlsComment.ClientAnchor;
					sprᮋ.ᜇ(iColumn - 1);
					sprᮋ.ᜆ(iRow - 1);
					sprᮋ.ᜂ(iColumn + 1);
					sprᮋ.ᜅ(iRow + 3);
					sprᮋ.ᜀ(240);
					sprᮋ.ᜃ(240);
					sprᮋ.ᜁ(240);
					sprᮋ.ᜄ(240);
					int num = this.ᜄ.Workbook.MaxColumnCount - 1;
					int num2 = 0;
					for (;;)
					{
						int maxRowCount;
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							if (sprᮋ.ᜎ() > num)
							{
								num2 = 1;
								continue;
							}
							goto IL_143;
						case 1:
						{
							int num3 = sprᮋ.ᜎ() - num;
							sprᮋ.ᜂ(num);
							sprᮋ sprᮋ2 = sprᮋ;
							sprᮋ2.ᜇ(sprᮋ2.ᜃ() - (num3 + 1));
							num2 = 5;
							continue;
						}
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_161;
							default:
							{
								if (false)
								{
								}
								int num4 = sprᮋ.ᜇ() - maxRowCount + 1;
								sprᮋ.ᜅ(maxRowCount - 1);
								sprᮋ sprᮋ3 = sprᮋ;
								sprᮋ3.ᜆ(sprᮋ3.ᜉ() - (num4 + 1));
								num2 = 3;
								continue;
							}
							}
							break;
						case 3:
							goto IL_141;
						case 4:
							goto IL_161;
						case 5:
							goto IL_143;
						}
						break;
						IL_143:
						maxRowCount = this.ᜄ.Workbook.MaxRowCount;
						num2 = 4;
						continue;
						IL_161:
						if (sprᮋ.ᜇ() < maxRowCount)
						{
							goto IL_1B4;
						}
						num2 = 2;
					}
				}
				IL_141:
				IL_1B4:
				xlsComment.UpdateWidth();
				xlsComment.UpdateHeight();
				base.Add(xlsComment);
				return xlsComment;
			}
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00025A88 File Offset: 0x00024A88
		internal new void ᜁ(ICommentShape A_0)
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
			base.Add(A_0 as XlsComment);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00025AD0 File Offset: 0x00024AD0
		private new void ᜀ()
		{
			int a_ = 19;
			if (true)
			{
			}
			this.ᜄ = (base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet);
			if (this.ᜄ == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("᥈⩊㽌⩎㽐❒畔㡖㭘ㅚ㡜㱞ᕠ䍢٤٦ݨժɬ᭮兰ᅲၴ坶ὸᑺࡼᅾ궂", a_));
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00025B50 File Offset: 0x00024B50
		protected internal void Remove(ICommentShape comment)
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
			base.Remove(comment as XlsComment);
			((spr\u1D9B)this.ᜄ.Shapes).ᜀ(comment);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00025BB0 File Offset: 0x00024BB0
		internal new void ᜀ(ICommentShape A_0)
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
			long key = sprṔ.ᜀ(A_0.Column, A_0.Row);
			this.ᜅ.Remove(key);
			this.Remove(A_0);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00025C14 File Offset: 0x00024C14
		internal new void ᜂ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_27:
					this.ᜅ.Clear();
					List<XlsComment> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					if (true)
					{
					}
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_CF:
						goto IL_71;
					default:
						if (false)
						{
						}
						num2 = 1;
						break;
					}
					for (;;)
					{
						IL_10:
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_6F;
						case 2:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							XlsComment xlsComment = innerList[num];
							long key = sprṔ.ᜀ(xlsComment.Column, xlsComment.Row);
							this.ᜅ.Add(key, xlsComment);
							num++;
							num2 = 3;
							continue;
						}
						case 3:
							goto IL_CF;
						}
						goto IL_27;
					}
					IL_6F:
					IL_71:
					num2 = 2;
					goto IL_10;
				}
				return;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00025CF4 File Offset: 0x00024CF4
		protected override void OnClear()
		{
			for (;;)
			{
				base.OnClear();
				int num = base.Count - 1;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_4D;
					case 1:
						goto IL_31;
					case 2:
						goto IL_31;
					case 3:
					{
						if (true)
						{
						}
						if (num < 0)
						{
							num2 = 0;
							continue;
						}
						ICommentShape a_ = this[num];
						((spr\u1D9B)this.ᜄ.Shapes).ᜀ(a_);
						num--;
						num2 = 1;
						continue;
					}
					}
					break;
					IL_31:
					num2 = 3;
				}
			}
			for (;;)
			{
				IL_4D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_9E;
				}
			}
			IL_9E:
			if (false)
			{
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00025DA8 File Offset: 0x00024DA8
		protected override void OnInsertComplete(int index, XlsComment value)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.OnInsertComplete(index, value);
			int row = value.Row;
			int column = value.Column;
			long key = sprṔ.ᜀ(column, row);
			this.HashComments[key] = value;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00025E10 File Offset: 0x00024E10
		protected override void OnRemoveComplete(int index, XlsComment value)
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
			base.OnRemoveComplete(index, value);
			int row = value.Row;
			int column = value.Column;
			long key = sprṔ.ᜀ(column, row);
			this.ᜅ.Remove(key);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00025E78 File Offset: 0x00024E78
		protected override void OnClearComplete()
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
			base.OnClearComplete();
			this.ᜅ.Clear();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00025EC4 File Offset: 0x00024EC4
		protected override void OnSetComplete(int index, XlsComment oldValue, XlsComment newValue)
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
			base.OnSetComplete(index, oldValue, newValue);
			int row = newValue.Row;
			int column = newValue.Column;
			long key = sprṔ.ᜀ(column, row);
			this.HashComments[key] = newValue;
		}

		// Token: 0x040000AE RID: 174
		private byte \u25D8ª\u0094\u0091;

		// Token: 0x040000AF RID: 175
		private new const int ᜀ = 200;

		// Token: 0x040000B0 RID: 176
		private byte[] \u25D9\u009Cª\u009F;

		// Token: 0x040000B1 RID: 177
		private new const int ᜁ = 100;

		// Token: 0x040000B2 RID: 178
		private new const int ᜂ = 1;

		// Token: 0x040000B3 RID: 179
		private const int ᜃ = 3;

		// Token: 0x040000B4 RID: 180
		private XlsWorksheet ᜄ;

		// Token: 0x040000B5 RID: 181
		private Dictionary<long, ICommentShape> ᜅ = new Dictionary<long, ICommentShape>();

		// Token: 0x040000B6 RID: 182
		private bool ᜆ;
	}
}
