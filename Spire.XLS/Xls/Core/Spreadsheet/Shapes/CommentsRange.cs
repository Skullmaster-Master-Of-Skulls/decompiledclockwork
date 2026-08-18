using System;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x0200021C RID: 540
	public class CommentsRange : XlsObject, ICommentShape
	{
		// Token: 0x0600204B RID: 8267 RVA: 0x00122164 File Offset: 0x00121164
		internal CommentsRange(spr\u1DF5 A_0, IXLSRange A_1) : base(A_0, A_1)
		{
			this.ᜀ = A_1;
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x00122180 File Offset: 0x00121180
		public string Author
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						string text = null;
						bool flag = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 1;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_D7;
							case 1:
								goto IL_D7;
							case 2:
								if (cells[num].Comment != null)
								{
									num3 = 8;
									continue;
								}
								goto IL_127;
							case 3:
								text = cells[num].Comment.Author;
								flag = false;
								num3 = 4;
								continue;
							case 4:
								goto IL_127;
							case 5:
								goto IL_E3;
							case 6:
								goto IL_9C;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_E3;
								default:
									if (false)
									{
									}
									if (flag)
									{
										num3 = 3;
										continue;
									}
									num3 = 10;
									continue;
								}
								break;
							case 8:
								num3 = 7;
								continue;
							case 9:
								return text;
							case 10:
								if (text != cells[num].Comment.Author)
								{
									if (true)
									{
									}
									num3 = 6;
									continue;
								}
								goto IL_127;
							}
							break;
							IL_D7:
							num3 = 5;
							continue;
							IL_E3:
							if (num >= num2)
							{
								num3 = 9;
								continue;
							}
							num3 = 2;
							continue;
							IL_127:
							num++;
							num3 = 0;
						}
					}
					IL_9C:
					return null;
				}
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x001222E8 File Offset: 0x001212E8
		// (set) Token: 0x0600204E RID: 8270 RVA: 0x00122448 File Offset: 0x00121448
		public bool IsVisible
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_D2;
							case 1:
								flag = cells[num].Comment.IsVisible;
								flag2 = false;
								num3 = 5;
								continue;
							case 2:
								goto IL_D2;
							case 3:
								return flag;
							case 4:
								goto IL_DE;
							case 5:
								goto IL_122;
							case 6:
								num3 = 7;
								continue;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_DE;
								default:
									if (false)
									{
									}
									if (flag2)
									{
										num3 = 1;
										continue;
									}
									num3 = 9;
									continue;
								}
								break;
							case 8:
								if (cells[num].Comment != null)
								{
									num3 = 6;
									continue;
								}
								goto IL_122;
							case 9:
								if (flag != cells[num].Comment.IsVisible)
								{
									if (true)
									{
									}
									num3 = 10;
									continue;
								}
								goto IL_122;
							case 10:
								return false;
							}
							break;
							IL_D2:
							num3 = 4;
							continue;
							IL_DE:
							if (num >= num2)
							{
								num3 = 3;
								continue;
							}
							num3 = 8;
							continue;
							IL_122:
							num++;
							num3 = 2;
						}
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int num2 = this.ᜀ.Cells.Length;
					int num3 = 3;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (num >= num2)
							{
								num3 = 1;
								continue;
							}
							if (true)
							{
							}
							this.ᜀ.Cells[num].AddComment().IsVisible = value;
							num++;
							num3 = 2;
							continue;
						case 1:
							return;
						case 2:
							goto IL_4E;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								goto IL_4E;
							}
							break;
						}
						break;
						IL_4E:
						num3 = 0;
					}
				}
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x001224F4 File Offset: 0x001214F4
		public int Row
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						int num = int.MinValue;
						bool flag = true;
						int num2 = 0;
						int num3 = cells.Length;
						int num4 = 6;
						for (;;)
						{
							switch (num4)
							{
							case 0:
								num4 = 4;
								continue;
							case 1:
								num = cells[num2].Comment.Row;
								flag = false;
								num4 = 10;
								continue;
							case 2:
								return num;
							case 3:
								if (cells[num2].Comment != null)
								{
									num4 = 0;
									continue;
								}
								goto IL_122;
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_DE;
								default:
									if (false)
									{
									}
									if (flag)
									{
										num4 = 1;
										continue;
									}
									num4 = 5;
									continue;
								}
								break;
							case 5:
								if (num != cells[num2].Comment.Row)
								{
									num4 = 9;
									continue;
								}
								goto IL_122;
							case 6:
								goto IL_D2;
							case 7:
								goto IL_D2;
							case 8:
								goto IL_DE;
							case 9:
								return int.MinValue;
							case 10:
								goto IL_122;
							}
							break;
							IL_D2:
							num4 = 8;
							continue;
							IL_DE:
							if (num2 >= num3)
							{
								num4 = 2;
								continue;
							}
							num4 = 3;
							continue;
							IL_122:
							num2++;
							if (true)
							{
							}
							num4 = 7;
						}
					}
					return int.MinValue;
				}
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0012265C File Offset: 0x0012165C
		public int Column
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						int num = int.MinValue;
						bool flag = true;
						int num2 = 0;
						int num3 = cells.Length;
						int num4 = 0;
						for (;;)
						{
							switch (num4)
							{
							case 0:
								goto IL_D2;
							case 1:
								goto IL_D2;
							case 2:
								num = cells[num2].Comment.Column;
								flag = false;
								num4 = 10;
								continue;
							case 3:
								if (num != cells[num2].Comment.Column)
								{
									num4 = 8;
									continue;
								}
								goto IL_12A;
							case 4:
								num4 = 7;
								continue;
							case 5:
								if (cells[num2].Comment != null)
								{
									num4 = 4;
									continue;
								}
								goto IL_12A;
							case 6:
								goto IL_DE;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_DE;
								default:
									if (false)
									{
									}
									if (flag)
									{
										num4 = 2;
										continue;
									}
									num4 = 3;
									continue;
								}
								break;
							case 8:
								return int.MinValue;
							case 9:
								return num;
							case 10:
								goto IL_12A;
							}
							break;
							IL_D2:
							num4 = 6;
							continue;
							IL_DE:
							if (num2 >= num3)
							{
								num4 = 9;
								continue;
							}
							if (true)
							{
							}
							num4 = 5;
							continue;
							IL_12A:
							num2++;
							num4 = 1;
						}
					}
					return int.MinValue;
				}
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x001227C4 File Offset: 0x001217C4
		public IRichTextString RichText
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
				return new RTFCommentArray(base.ReservedHandle, this);
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x0012280C File Offset: 0x0012180C
		// (set) Token: 0x06002053 RID: 8275 RVA: 0x00122854 File Offset: 0x00121854
		public string Text
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
				return this.RichText.Text;
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
				this.RichText.Text = value;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x0012289C File Offset: 0x0012189C
		// (set) Token: 0x06002055 RID: 8277 RVA: 0x001228DC File Offset: 0x001218DC
		public bool IsMoveWithCell
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
				throw new NotImplementedException();
			}
			set
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x0012291C File Offset: 0x0012191C
		// (set) Token: 0x06002057 RID: 8279 RVA: 0x00122A7C File Offset: 0x00121A7C
		public bool AutoSize
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 4;
						for (;;)
						{
							if (true)
							{
							}
							switch (num3)
							{
							case 0:
								return false;
							case 1:
								flag = cells[num].Comment.AutoSize;
								flag2 = false;
								num3 = 5;
								continue;
							case 2:
								if (cells[num].Comment != null)
								{
									num3 = 3;
									continue;
								}
								goto IL_122;
							case 3:
								num3 = 8;
								continue;
							case 4:
								goto IL_D2;
							case 5:
								goto IL_122;
							case 6:
								goto IL_D2;
							case 7:
								if (flag != cells[num].Comment.AutoSize)
								{
									num3 = 0;
									continue;
								}
								goto IL_122;
							case 8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_DE;
								default:
									if (false)
									{
									}
									if (flag2)
									{
										num3 = 1;
										continue;
									}
									num3 = 7;
									continue;
								}
								break;
							case 9:
								goto IL_DE;
							case 10:
								return flag;
							}
							break;
							IL_D2:
							num3 = 9;
							continue;
							IL_DE:
							if (num >= num2)
							{
								num3 = 10;
								continue;
							}
							num3 = 2;
							continue;
							IL_122:
							num++;
							num3 = 6;
						}
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int num2 = this.ᜀ.Cells.Length;
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (num >= num2)
							{
								num3 = 2;
								continue;
							}
							this.ᜀ.Cells[num].AddComment().AutoSize = value;
							num++;
							num3 = 3;
							continue;
						case 1:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								goto IL_56;
							}
							break;
						case 2:
							return;
						case 3:
							goto IL_56;
						}
						break;
						IL_56:
						num3 = 0;
					}
				}
			}
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00122B28 File Offset: 0x00121B28
		public void Remove()
		{
			for (;;)
			{
				int num = 0;
				int num2 = this.ᜀ.Cells.Length;
				int num3 = 3;
				for (;;)
				{
					ICommentShape comment;
					switch (num3)
					{
					case 0:
						goto IL_3E;
					case 1:
						if (comment != null)
						{
							num3 = 4;
							continue;
						}
						goto IL_3E;
					case 2:
						goto IL_C6;
					case 3:
						goto IL_BB;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C6;
						default:
							if (false)
							{
							}
							comment.Remove();
							num3 = 0;
							continue;
						}
						break;
					case 5:
						goto IL_BB;
					case 6:
						return;
					}
					break;
					IL_3E:
					num++;
					if (true)
					{
					}
					num3 = 5;
					continue;
					IL_C6:
					if (num >= num2)
					{
						num3 = 6;
						continue;
					}
					comment = this.ᜀ.Cells[num].Comment;
					num3 = 1;
					continue;
					IL_BB:
					num3 = 2;
				}
			}
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x00122C0C File Offset: 0x00121C0C
		public void Scale(int scaleWidth, int scaleHeight)
		{
			for (;;)
			{
				int num = 0;
				int num2 = this.ᜀ.Cells.Length;
				int num3 = 4;
				for (;;)
				{
					ICommentShape comment;
					switch (num3)
					{
					case 0:
						if (true)
						{
						}
						goto IL_C0;
					case 1:
						if (comment != null)
						{
							num3 = 3;
							continue;
						}
						goto IL_41;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CB;
						default:
							if (false)
							{
							}
							comment.Scale(scaleWidth, scaleHeight);
							num3 = 6;
							continue;
						}
						break;
					case 4:
						goto IL_C0;
					case 5:
						goto IL_CB;
					case 6:
						goto IL_41;
					}
					break;
					IL_41:
					num++;
					num3 = 0;
					continue;
					IL_CB:
					if (num >= num2)
					{
						num3 = 2;
						continue;
					}
					comment = this.ᜀ.Cells[num].Comment;
					num3 = 1;
					continue;
					IL_C0:
					num3 = 5;
				}
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x00122CF8 File Offset: 0x00121CF8
		// (set) Token: 0x0600205B RID: 8283 RVA: 0x00122E58 File Offset: 0x00121E58
		public bool Visible
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_CF;
							case 1:
								return flag;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_DB;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									if (flag2)
									{
										num3 = 6;
										continue;
									}
									num3 = 8;
									continue;
								}
								break;
							case 3:
								num3 = 2;
								continue;
							case 4:
								goto IL_DB;
							case 5:
								return false;
							case 6:
								flag = cells[num].Comment.Visible;
								flag2 = false;
								num3 = 7;
								continue;
							case 7:
								goto IL_11F;
							case 8:
								if (flag != cells[num].Comment.Visible)
								{
									num3 = 5;
									continue;
								}
								goto IL_11F;
							case 9:
								if (cells[num].Comment != null)
								{
									num3 = 3;
									continue;
								}
								goto IL_11F;
							case 10:
								goto IL_CF;
							}
							break;
							IL_CF:
							num3 = 4;
							continue;
							IL_DB:
							if (num >= num2)
							{
								num3 = 1;
								continue;
							}
							num3 = 9;
							continue;
							IL_11F:
							num++;
							num3 = 10;
						}
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int num2 = this.ᜀ.Cells.Length;
					int num3 = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num3)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								goto IL_56;
							}
							break;
						case 1:
							if (num >= num2)
							{
								num3 = 2;
								continue;
							}
							this.ᜀ.Cells[num].AddComment().Visible = value;
							num++;
							num3 = 3;
							continue;
						case 2:
							return;
						case 3:
							goto IL_56;
						}
						break;
						IL_56:
						num3 = 1;
					}
				}
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00122F04 File Offset: 0x00121F04
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x0012306C File Offset: 0x0012206C
		public int Height
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						int num = int.MinValue;
						bool flag = true;
						int num2 = 0;
						int num3 = cells.Length;
						int num4 = 0;
						for (;;)
						{
							switch (num4)
							{
							case 0:
								goto IL_D2;
							case 1:
								num4 = 6;
								continue;
							case 2:
								if (num != cells[num2].Comment.Height)
								{
									num4 = 9;
									continue;
								}
								goto IL_122;
							case 3:
								num = cells[num2].Comment.Height;
								flag = false;
								num4 = 8;
								continue;
							case 4:
								return num;
							case 5:
								if (cells[num2].Comment != null)
								{
									num4 = 1;
									continue;
								}
								goto IL_122;
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_DE;
								default:
									if (false)
									{
									}
									if (flag)
									{
										num4 = 3;
										continue;
									}
									num4 = 2;
									continue;
								}
								break;
							case 7:
								goto IL_DE;
							case 8:
								goto IL_122;
							case 9:
								return int.MinValue;
							case 10:
								goto IL_D2;
							}
							break;
							IL_D2:
							num4 = 7;
							continue;
							IL_DE:
							if (num2 >= num3)
							{
								num4 = 4;
								continue;
							}
							num4 = 5;
							continue;
							IL_122:
							if (true)
							{
							}
							num2++;
							num4 = 10;
						}
					}
					return int.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					IXLSRange[] cells = this.ᜀ.Cells;
					int num = 0;
					int num2 = cells.Length;
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (true)
							{
							}
							goto IL_B3;
						case 1:
							goto IL_4A;
						case 2:
							goto IL_B3;
						case 3:
							return;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6E;
							default:
								if (false)
								{
								}
								if (cells[num].Comment != null)
								{
									num3 = 6;
									continue;
								}
								goto IL_4A;
							}
							break;
						case 5:
							if (num >= num2)
							{
								num3 = 3;
								continue;
							}
							num3 = 4;
							continue;
						case 6:
							cells[num].Comment.Height = value;
							goto IL_6E;
						}
						break;
						IL_4A:
						num++;
						num3 = 0;
						continue;
						IL_6E:
						num3 = 1;
						continue;
						IL_B3:
						num3 = 5;
					}
				}
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x00123148 File Offset: 0x00122148
		public int ID
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
				return 0;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00123184 File Offset: 0x00122184
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x001232EC File Offset: 0x001222EC
		public int Left
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_67:
						IXLSRange[] cells = this.ᜀ.Cells;
						int num = int.MinValue;
						bool flag = true;
						int num2 = 0;
						int num3 = cells.Length;
						int num4 = 0;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								switch (num4)
								{
								case 0:
									goto IL_E4;
								case 1:
									return num;
								case 2:
									goto IL_E4;
								case 3:
									return int.MinValue;
								case 4:
									if (num2 >= num3)
									{
										num4 = 1;
										continue;
									}
									num4 = 10;
									continue;
								case 5:
									if (num != cells[num2].Comment.Left)
									{
										num4 = 3;
										continue;
									}
									goto IL_127;
								case 6:
									num4 = 9;
									continue;
								case 7:
									num = cells[num2].Comment.Left;
									flag = false;
									num4 = 8;
									continue;
								case 8:
									goto IL_127;
								case 9:
									goto IL_C9;
								case 10:
									if (cells[num2].Comment != null)
									{
										num4 = 6;
										continue;
									}
									goto IL_127;
								}
								goto IL_67;
								IL_E4:
								num4 = 4;
								continue;
								IL_127:
								num2++;
								num4 = 2;
								continue;
							}
							IL_C9:
							if (flag)
							{
								num4 = 7;
							}
							else
							{
								num4 = 5;
							}
						}
					}
					return int.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					IXLSRange[] cells = this.ᜀ.Cells;
					int num = 0;
					int num2 = cells.Length;
					int num3 = 5;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_4A;
						case 1:
							return;
						case 2:
							if (num >= num2)
							{
								num3 = 1;
								continue;
							}
							num3 = 4;
							continue;
						case 3:
							goto IL_B3;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_66;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								if (cells[num].Comment != null)
								{
									num3 = 6;
									continue;
								}
								goto IL_4A;
							}
							break;
						case 5:
							goto IL_B3;
						case 6:
							cells[num].Comment.Left = value;
							goto IL_66;
						}
						break;
						IL_4A:
						num++;
						num3 = 3;
						continue;
						IL_66:
						num3 = 0;
						continue;
						IL_B3:
						num3 = 2;
					}
				}
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x001233C8 File Offset: 0x001223C8
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x00123528 File Offset: 0x00122528
		public string Name
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_5F:
						IXLSRange[] cells = this.ᜀ.Cells;
						string text = null;
						bool flag = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 9;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								switch (num3)
								{
								case 0:
									if (cells[num].Comment != null)
									{
										num3 = 1;
										continue;
									}
									goto IL_119;
								case 1:
									num3 = 2;
									continue;
								case 2:
									goto IL_BB;
								case 3:
									goto IL_119;
								case 4:
									return text;
								case 5:
									text = cells[num].Comment.Name;
									flag = false;
									num3 = 3;
									continue;
								case 6:
									goto IL_D6;
								case 7:
									goto IL_AD;
								case 8:
									if (num >= num2)
									{
										num3 = 4;
										continue;
									}
									num3 = 0;
									continue;
								case 9:
									goto IL_D6;
								case 10:
									if (text != cells[num].Comment.Name)
									{
										num3 = 7;
										continue;
									}
									goto IL_119;
								}
								goto IL_5F;
								IL_D6:
								num3 = 8;
								continue;
								IL_119:
								num++;
								if (true)
								{
								}
								num3 = 6;
								continue;
							}
							IL_BB:
							if (flag)
							{
								num3 = 5;
							}
							else
							{
								num3 = 10;
							}
						}
					}
					IL_AD:
					return null;
				}
			}
			set
			{
				for (;;)
				{
					IXLSRange[] cells = this.ᜀ.Cells;
					int num = 0;
					int num2 = cells.Length;
					int num3 = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num3)
						{
						case 0:
							goto IL_B3;
						case 1:
							cells[num].Comment.Name = value;
							goto IL_6E;
						case 2:
							goto IL_52;
						case 3:
							return;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6E;
							default:
								if (false)
								{
								}
								if (cells[num].Comment != null)
								{
									num3 = 1;
									continue;
								}
								goto IL_52;
							}
							break;
						case 5:
							goto IL_B3;
						case 6:
							if (num >= num2)
							{
								num3 = 3;
								continue;
							}
							num3 = 4;
							continue;
						}
						break;
						IL_52:
						num++;
						num3 = 5;
						continue;
						IL_6E:
						num3 = 2;
						continue;
						IL_B3:
						num3 = 6;
					}
				}
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x00123604 File Offset: 0x00122604
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x0012376C File Offset: 0x0012276C
		public int Top
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_5F:
						IXLSRange[] cells = this.ᜀ.Cells;
						int num = int.MinValue;
						bool flag = true;
						int num2 = 0;
						int num3 = cells.Length;
						int num4 = 7;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								switch (num4)
								{
								case 0:
									if (cells[num2].Comment != null)
									{
										num4 = 6;
										continue;
									}
									goto IL_12A;
								case 1:
									goto IL_12A;
								case 2:
									goto IL_C4;
								case 3:
									if (num != cells[num2].Comment.Top)
									{
										num4 = 10;
										continue;
									}
									goto IL_12A;
								case 4:
									if (num2 >= num3)
									{
										num4 = 5;
										continue;
									}
									num4 = 0;
									continue;
								case 5:
									return num;
								case 6:
									if (true)
									{
									}
									num4 = 2;
									continue;
								case 7:
									goto IL_DF;
								case 8:
									num = cells[num2].Comment.Top;
									flag = false;
									num4 = 1;
									continue;
								case 9:
									goto IL_DF;
								case 10:
									return int.MinValue;
								}
								goto IL_5F;
								IL_DF:
								num4 = 4;
								continue;
								IL_12A:
								num2++;
								num4 = 9;
								continue;
							}
							IL_C4:
							if (flag)
							{
								num4 = 8;
							}
							else
							{
								num4 = 3;
							}
						}
					}
					return int.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					IXLSRange[] cells = this.ᜀ.Cells;
					int num = 0;
					int num2 = cells.Length;
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_AB;
						case 1:
							goto IL_AB;
						case 2:
							cells[num].Comment.Top = value;
							goto IL_66;
						case 3:
							goto IL_C5;
						case 4:
							goto IL_4A;
						case 5:
							if (num >= num2)
							{
								num3 = 3;
								continue;
							}
							num3 = 6;
							continue;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_66;
							default:
								if (false)
								{
								}
								if (cells[num].Comment != null)
								{
									num3 = 2;
									continue;
								}
								goto IL_4A;
							}
							break;
						}
						break;
						IL_4A:
						num++;
						num3 = 0;
						continue;
						IL_66:
						num3 = 4;
						continue;
						IL_AB:
						num3 = 5;
					}
				}
				IL_C5:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x00123848 File Offset: 0x00122848
		// (set) Token: 0x06002066 RID: 8294 RVA: 0x001239B0 File Offset: 0x001229B0
		public int Width
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_5F:
						IXLSRange[] cells = this.ᜀ.Cells;
						int num = int.MinValue;
						bool flag = true;
						int num2 = 0;
						int num3 = cells.Length;
						int num4 = 5;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								switch (num4)
								{
								case 0:
									goto IL_11F;
								case 1:
									goto IL_C1;
								case 2:
									return num;
								case 3:
									if (num != cells[num2].Comment.Width)
									{
										num4 = 4;
										continue;
									}
									goto IL_11F;
								case 4:
									return int.MinValue;
								case 5:
									goto IL_DC;
								case 6:
									if (num2 >= num3)
									{
										num4 = 2;
										continue;
									}
									num4 = 10;
									continue;
								case 7:
									if (true)
									{
									}
									goto IL_DC;
								case 8:
									num4 = 1;
									continue;
								case 9:
									num = cells[num2].Comment.Width;
									flag = false;
									num4 = 0;
									continue;
								case 10:
									if (cells[num2].Comment != null)
									{
										num4 = 8;
										continue;
									}
									goto IL_11F;
								}
								goto IL_5F;
								IL_DC:
								num4 = 6;
								continue;
								IL_11F:
								num2++;
								num4 = 7;
								continue;
							}
							IL_C1:
							if (flag)
							{
								num4 = 9;
							}
							else
							{
								num4 = 3;
							}
						}
					}
					return int.MinValue;
				}
			}
			set
			{
				for (;;)
				{
					IXLSRange[] cells = this.ᜀ.Cells;
					int num = 0;
					int num2 = cells.Length;
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6E;
							default:
								if (false)
								{
								}
								if (cells[num].Comment != null)
								{
									num3 = 6;
									continue;
								}
								goto IL_4A;
							}
							break;
						case 2:
							goto IL_B3;
						case 3:
							goto IL_B3;
						case 4:
							if (num >= num2)
							{
								num3 = 0;
								continue;
							}
							num3 = 1;
							continue;
						case 5:
							goto IL_4A;
						case 6:
							cells[num].Comment.Width = value;
							goto IL_6E;
						}
						break;
						IL_4A:
						if (true)
						{
						}
						num++;
						num3 = 3;
						continue;
						IL_6E:
						num3 = 5;
						continue;
						IL_B3:
						num3 = 4;
					}
				}
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x00123A8C File Offset: 0x00122A8C
		// (set) Token: 0x06002068 RID: 8296 RVA: 0x00123AC8 File Offset: 0x00122AC8
		public ExcelShapeType ShapeType
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
				return ExcelShapeType.Comment;
			}
			set
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
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x00123B04 File Offset: 0x00122B04
		// (set) Token: 0x0600206A RID: 8298 RVA: 0x00123C68 File Offset: 0x00122C68
		public string AlternativeText
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_67:
						IXLSRange[] cells = this.ᜀ.Cells;
						string text = null;
						bool flag = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 5;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								switch (num3)
								{
								case 0:
									goto IL_C6;
								case 1:
									goto IL_124;
								case 2:
									goto IL_E1;
								case 3:
									if (cells[num].Comment != null)
									{
										num3 = 9;
										continue;
									}
									goto IL_124;
								case 4:
									goto IL_B8;
								case 5:
									goto IL_E1;
								case 6:
									return text;
								case 7:
									text = cells[num].Comment.AlternativeText;
									flag = false;
									num3 = 1;
									continue;
								case 8:
									if (text != cells[num].Comment.AlternativeText)
									{
										num3 = 4;
										continue;
									}
									goto IL_124;
								case 9:
									num3 = 0;
									continue;
								case 10:
									if (num >= num2)
									{
										num3 = 6;
										continue;
									}
									num3 = 3;
									continue;
								}
								goto IL_67;
								IL_E1:
								num3 = 10;
								continue;
								IL_124:
								num++;
								num3 = 2;
								continue;
							}
							IL_C6:
							if (flag)
							{
								num3 = 7;
							}
							else
							{
								num3 = 8;
							}
						}
					}
					IL_B8:
					return null;
				}
			}
			set
			{
				for (;;)
				{
					IXLSRange[] cells = this.ᜀ.Cells;
					int num = 0;
					int num2 = cells.Length;
					int num3 = 5;
					for (;;)
					{
						if (true)
						{
						}
						switch (num3)
						{
						case 0:
							goto IL_B8;
						case 1:
							if (num >= num2)
							{
								num3 = 2;
								continue;
							}
							num3 = 3;
							continue;
						case 2:
							return;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_73;
							default:
								if (false)
								{
								}
								if (cells[num].Comment != null)
								{
									num3 = 6;
									continue;
								}
								goto IL_52;
							}
							break;
						case 4:
							goto IL_52;
						case 5:
							goto IL_B8;
						case 6:
							((XlsComment)cells[num].Comment).AlternativeText = value;
							goto IL_73;
						}
						break;
						IL_52:
						num++;
						num3 = 0;
						continue;
						IL_73:
						num3 = 4;
						continue;
						IL_B8:
						num3 = 1;
					}
				}
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x00123D4C File Offset: 0x00122D4C
		public IShapeFill Fill
		{
			get
			{
				int a_ = 4;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("渹吻圽㌿扁㑃㑅❇㩉⥋㱍⑏⭑瑓㉕㝗㽙⽛そ䝟ᙡ䑣ᕥᵧᩩᱫŭɯٱ味ήᙷ婹ࡻᙽꒃﾋﶍ", a_));
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x00123DA4 File Offset: 0x00122DA4
		public IShapeLineFormat Line
		{
			get
			{
				int a_ = 11;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ᕀ⭂ⱄ㑆楈㭊㽌⁎⅐㙒❔⍖⁘筚㥜ぞѠၢ୤䁦ᵨ䭪ṬᩮŰͲᩴն൸孺ᑼᅾꆀ愈ꮊ", a_));
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x00123DFC File Offset: 0x00122DFC
		// (set) Token: 0x0600206E RID: 8302 RVA: 0x00123E3C File Offset: 0x00122E3C
		public string OnAction
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
				throw new NotSupportedException();
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x00123E7C File Offset: 0x00122E7C
		public IFormat3D ThreeD
		{
			get
			{
				int a_ = 6;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("栻嘽⤿ㅁ摃㙅㩇╉㱋⭍≏♑ⵓ癕㭗㭙㉛繝๟ൡၣ䙥੧ཀྵ䱫ᵭկɱѳ᥵੷๹᥻᩽깿", a_));
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x00123ED4 File Offset: 0x00122ED4
		public IShadow Shadow
		{
			get
			{
				int a_ = 6;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("栻嘽⤿ㅁ摃㙅㩇╉㱋⭍≏♑ⵓ癕㭗㭙㉛繝๟ൡၣ䙥੧ཀྵ䱫ᵭկɱѳ᥵੷๹᥻᩽깿", a_));
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x00123F2C File Offset: 0x00122F2C
		// (set) Token: 0x06002072 RID: 8306 RVA: 0x00123F84 File Offset: 0x00122F84
		public int Rotation
		{
			get
			{
				int a_ = 7;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("椼圾⡀あ敄㝆㭈⑊㵌⩎⍐❒ⱔ睖㵘㑚㡜ⱞའ䑢ᅤ䝦ᩨṪᵬὮṰŲŴ坶ၸᕺ嵼୾Ꞇﲎ", a_));
			}
			set
			{
				int a_ = 11;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ᕀ⭂ⱄ㑆楈㭊㽌⁎⅐㙒❔⍖⁘筚㥜ぞѠၢ୤䁦ᵨ䭪ṬᩮŰͲᩴն൸孺ᑼᅾꆀ愈ꮊ", a_));
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x00123FDC File Offset: 0x00122FDC
		// (set) Token: 0x06002074 RID: 8308 RVA: 0x00124138 File Offset: 0x00123138
		public CommentHAlignType HAlignment
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IL_5F:
						IXLSRange[] cells = this.ᜀ.Cells;
						CommentHAlignType commentHAlignType = CommentHAlignType.Left;
						bool flag = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 10;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								switch (num3)
								{
								case 0:
									if (commentHAlignType != cells[num].Comment.HAlignment)
									{
										num3 = 3;
										continue;
									}
									goto IL_114;
								case 1:
									return commentHAlignType;
								case 2:
									if (true)
									{
									}
									commentHAlignType = cells[num].Comment.HAlignment;
									flag = false;
									num3 = 5;
									continue;
								case 3:
									return CommentHAlignType.Left;
								case 4:
									num3 = 7;
									continue;
								case 5:
									goto IL_114;
								case 6:
									if (num >= num2)
									{
										num3 = 1;
										continue;
									}
									num3 = 8;
									continue;
								case 7:
									goto IL_B6;
								case 8:
									if (cells[num].Comment != null)
									{
										num3 = 4;
										continue;
									}
									goto IL_114;
								case 9:
									goto IL_D1;
								case 10:
									goto IL_D1;
								}
								goto IL_5F;
								IL_D1:
								num3 = 6;
								continue;
								IL_114:
								num++;
								num3 = 9;
								continue;
							}
							IL_B6:
							if (flag)
							{
								num3 = 2;
							}
							else
							{
								num3 = 0;
							}
						}
					}
					return CommentHAlignType.Left;
				}
			}
			set
			{
				for (;;)
				{
					if (true)
					{
					}
					int num = 0;
					int num2 = this.ᜀ.Cells.Length;
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							return;
						case 1:
							if (num >= num2)
							{
								num3 = 0;
								continue;
							}
							this.ᜀ.Cells[num].AddComment().HAlignment = value;
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num3 = 3;
								continue;
							}
							break;
						case 2:
							goto IL_3A;
						case 3:
							goto IL_3A;
						}
						break;
						IL_3A:
						num3 = 1;
					}
				}
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x001241E4 File Offset: 0x001231E4
		// (set) Token: 0x06002076 RID: 8310 RVA: 0x0012433C File Offset: 0x0012333C
		public CommentVAlignType VAlignment
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						CommentVAlignType commentVAlignType = CommentVAlignType.Top;
						bool flag = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_A8;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_F3;
								default:
									if (false)
									{
									}
									goto IL_F3;
								}
								break;
							case 2:
								if (num >= num2)
								{
									if (true)
									{
									}
									num3 = 8;
									continue;
								}
								num3 = 3;
								continue;
							case 3:
								if (cells[num].Comment != null)
								{
									num3 = 10;
									continue;
								}
								goto IL_F3;
							case 4:
								if (flag)
								{
									num3 = 7;
									continue;
								}
								num3 = 6;
								continue;
							case 5:
								return CommentVAlignType.Top;
							case 6:
								if (commentVAlignType != cells[num].Comment.VAlignment)
								{
									num3 = 5;
									continue;
								}
								goto IL_F3;
							case 7:
								commentVAlignType = cells[num].Comment.VAlignment;
								flag = false;
								num3 = 1;
								continue;
							case 8:
								return commentVAlignType;
							case 9:
								goto IL_A8;
							case 10:
								num3 = 4;
								continue;
							}
							break;
							IL_A8:
							num3 = 2;
							continue;
							IL_F3:
							num++;
							num3 = 9;
						}
					}
					return CommentVAlignType.Top;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int num2 = this.ᜀ.Cells.Length;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						int num3 = 3;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (num >= num2)
								{
									num3 = 2;
									continue;
								}
								this.ᜀ.Cells[num].AddComment().VAlignment = value;
								num++;
								num3 = 1;
								continue;
							case 1:
								goto IL_58;
							case 2:
								return;
							case 3:
								goto IL_58;
							}
							break;
							IL_58:
							if (true)
							{
							}
							num3 = 0;
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x001243E8 File Offset: 0x001233E8
		// (set) Token: 0x06002078 RID: 8312 RVA: 0x00124544 File Offset: 0x00123544
		public TextRotationType TextRotation
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						TextRotationType textRotationType = TextRotationType.LeftToRight;
						bool flag = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 10;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								textRotationType = cells[num].Comment.TextRotation;
								flag = false;
								num3 = 2;
								continue;
							case 1:
								goto IL_A8;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_F6;
								default:
									if (false)
									{
									}
									goto IL_F6;
								}
								break;
							case 3:
								if (cells[num].Comment != null)
								{
									num3 = 4;
									continue;
								}
								goto IL_F6;
							case 4:
								num3 = 9;
								continue;
							case 5:
								if (textRotationType != cells[num].Comment.TextRotation)
								{
									num3 = 6;
									continue;
								}
								goto IL_F6;
							case 6:
								return TextRotationType.LeftToRight;
							case 7:
								return textRotationType;
							case 8:
								if (num >= num2)
								{
									num3 = 7;
									continue;
								}
								if (true)
								{
								}
								num3 = 3;
								continue;
							case 9:
								if (flag)
								{
									num3 = 0;
									continue;
								}
								num3 = 5;
								continue;
							case 10:
								goto IL_A8;
							}
							break;
							IL_A8:
							num3 = 8;
							continue;
							IL_F6:
							num++;
							num3 = 1;
						}
					}
					return TextRotationType.LeftToRight;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int num2 = this.ᜀ.Cells.Length;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						int num3 = 3;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								return;
							case 1:
								goto IL_60;
							case 2:
								if (num >= num2)
								{
									num3 = 0;
									continue;
								}
								this.ᜀ.Cells[num].AddComment().TextRotation = value;
								num++;
								num3 = 1;
								continue;
							case 3:
								if (true)
								{
								}
								goto IL_60;
							}
							break;
							IL_60:
							num3 = 2;
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x001245F0 File Offset: 0x001235F0
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x00124748 File Offset: 0x00123748
		public bool IsTextLocked
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IXLSRange[] cells = this.ᜀ.Cells;
						bool flag = false;
						bool flag2 = true;
						int num = 0;
						int num2 = cells.Length;
						int num3 = 1;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (cells[num].Comment != null)
								{
									num3 = 2;
									continue;
								}
								goto IL_F3;
							case 1:
								goto IL_B0;
							case 2:
								num3 = 5;
								continue;
							case 3:
								flag = cells[num].Comment.IsTextLocked;
								flag2 = false;
								num3 = 9;
								continue;
							case 4:
								if (flag != cells[num].Comment.IsTextLocked)
								{
									num3 = 7;
									continue;
								}
								goto IL_F3;
							case 5:
								if (true)
								{
								}
								if (flag2)
								{
									num3 = 3;
									continue;
								}
								num3 = 4;
								continue;
							case 6:
								if (num >= num2)
								{
									num3 = 10;
									continue;
								}
								num3 = 0;
								continue;
							case 7:
								return false;
							case 8:
								goto IL_B0;
							case 9:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_F3;
								default:
									if (false)
									{
									}
									goto IL_F3;
								}
								break;
							case 10:
								return flag;
							}
							break;
							IL_B0:
							num3 = 6;
							continue;
							IL_F3:
							num++;
							num3 = 8;
						}
					}
					return false;
				}
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int num2 = this.ᜀ.Cells.Length;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_60;
							case 1:
								goto IL_60;
							case 2:
								return;
							case 3:
								if (num >= num2)
								{
									num3 = 2;
									continue;
								}
								this.ᜀ.Cells[num].AddComment().IsTextLocked = value;
								num++;
								num3 = 1;
								continue;
							}
							break;
							IL_60:
							num3 = 3;
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x0400112D RID: 4397
		private long \u25D9\u00AC\u00A3\u0090;

		// Token: 0x0400112E RID: 4398
		private bool[] \u2609\u00A9\u007F\u0082;

		// Token: 0x0400112F RID: 4399
		private IXLSRange ᜀ;
	}
}
