using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000626 RID: 1574
	public class XlsPageSetupBase : XlsObject, IPageSetupBase, IRecordStorage
	{
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06005FCB RID: 24523 RVA: 0x003C868C File Offset: 0x003C768C
		// (set) Token: 0x06005FCC RID: 24524 RVA: 0x003C86D0 File Offset: 0x003C76D0
		public virtual bool IsFitToPage
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06005FCD RID: 24525 RVA: 0x003C8714 File Offset: 0x003C7714
		// (set) Token: 0x06005FCE RID: 24526 RVA: 0x003C875C File Offset: 0x003C775C
		public int FitToPagesTall
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
				return (int)this.ᜄ.\u1712();
			}
			set
			{
				for (;;)
				{
					IL_3C:
					ushort num = (ushort)value;
					int num2 = 4;
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
							switch (num2)
							{
							case 0:
								if (!this.ᜅ.ParentWorkbook.Loading)
								{
									num2 = 5;
									continue;
								}
								return;
							case 1:
								return;
							case 2:
								goto IL_C6;
							case 3:
								if (true)
								{
								}
								this.ᜄ.ᜅ(num);
								this.SetChanged();
								num2 = 2;
								continue;
							case 4:
								if (this.ᜄ.\u1712() != num)
								{
									num2 = 3;
									continue;
								}
								goto IL_70;
							case 5:
								this.IsFitToPage = true;
								num2 = 1;
								continue;
							}
							goto IL_3C;
						}
						IL_70:
						num2 = 0;
						continue;
						IL_C6:
						goto IL_70;
					}
				}
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005FCF RID: 24527 RVA: 0x003C8834 File Offset: 0x003C7834
		// (set) Token: 0x06005FD0 RID: 24528 RVA: 0x003C887C File Offset: 0x003C787C
		public int FitToPagesWide
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
				return (int)this.ᜄ.ᜌ();
			}
			set
			{
				for (;;)
				{
					IL_3C:
					ushort num = (ushort)value;
					if (true)
					{
					}
					int num2 = 4;
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
							switch (num2)
							{
							case 0:
								return;
							case 1:
								goto IL_C6;
							case 2:
								this.ᜄ.ᜄ(num);
								this.SetChanged();
								num2 = 1;
								continue;
							case 3:
								if (!this.ᜅ.ParentWorkbook.Loading)
								{
									num2 = 5;
									continue;
								}
								return;
							case 4:
								if (this.ᜄ.ᜌ() != num)
								{
									num2 = 2;
									continue;
								}
								goto IL_78;
							case 5:
								this.IsFitToPage = true;
								num2 = 0;
								continue;
							}
							goto IL_3C;
						}
						IL_78:
						num2 = 3;
						continue;
						IL_C6:
						goto IL_78;
					}
				}
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06005FD1 RID: 24529 RVA: 0x003C8954 File Offset: 0x003C7954
		// (set) Token: 0x06005FD2 RID: 24530 RVA: 0x003C899C File Offset: 0x003C799C
		public bool IsSettingsNotValid
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
				return this.ᜄ.ᜅ();
			}
			internal set
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
				this.ᜄ.ᜆ(value);
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06005FD3 RID: 24531 RVA: 0x003C89E4 File Offset: 0x003C79E4
		// (set) Token: 0x06005FD4 RID: 24532 RVA: 0x003C8A30 File Offset: 0x003C7A30
		public bool AutoFirstPageNumber
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
				return !this.ᜄ.ᜎ();
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
				this.ᜄ.ᜇ(!value);
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06005FD5 RID: 24533 RVA: 0x003C8A7C File Offset: 0x003C7A7C
		// (set) Token: 0x06005FD6 RID: 24534 RVA: 0x003C8AC4 File Offset: 0x003C7AC4
		public bool BlackAndWhite
		{
			get
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
				return this.ᜄ.ᜋ();
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜄ.ᜁ(value);
								this.SetChanged();
								num = 0;
								continue;
							}
							break;
						}
						if (true)
						{
						}
						if (this.ᜄ.ᜋ() == value)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06005FD7 RID: 24535 RVA: 0x003C8B50 File Offset: 0x003C7B50
		// (set) Token: 0x06005FD8 RID: 24536 RVA: 0x003C8B94 File Offset: 0x003C7B94
		public double BottomMargin
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
				return this.m_dBottomMargin;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6A:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_5B;
					case 2:
						if (true)
						{
						}
						break;
					}
					if (this.m_dBottomMargin == value)
					{
						return;
					}
					num = 1;
				}
				IL_5B:
				this.m_dBottomMargin = value;
				this.SetChanged();
				goto IL_6A;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06005FD9 RID: 24537 RVA: 0x003C8C18 File Offset: 0x003C7C18
		// (set) Token: 0x06005FDA RID: 24538 RVA: 0x003C8C5C File Offset: 0x003C7C5C
		public string CenterFooter
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
				return this.m_arrFooters[1];
			}
			set
			{
				if (true)
				{
				}
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_73:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						goto IL_62;
					}
					if (!(this.m_arrFooters[1] != value))
					{
						return;
					}
					num = 2;
				}
				IL_62:
				this.m_arrFooters[1] = value;
				this.SetChanged();
				goto IL_73;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06005FDB RID: 24539 RVA: 0x003C8CE8 File Offset: 0x003C7CE8
		// (set) Token: 0x06005FDC RID: 24540 RVA: 0x003C8D50 File Offset: 0x003C7D50
		public Image CenterFooterImage
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					XlsBitmapShape xlsBitmapShape = (XlsBitmapShape)this.ᜅ.HeaderFooterShapes[XlsPageSetupBase.ᜁ[1]];
					if (xlsBitmapShape != null)
					{
						return xlsBitmapShape.Picture;
					}
					break;
				}
				}
				if (true)
				{
				}
				return null;
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
				this.ᜅ.HeaderFooterShapes.SetPicture(XlsPageSetupBase.ᜁ[1], value);
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06005FDD RID: 24541 RVA: 0x003C8DA4 File Offset: 0x003C7DA4
		// (set) Token: 0x06005FDE RID: 24542 RVA: 0x003C8E0C File Offset: 0x003C7E0C
		public Image CenterHeaderImage
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					XlsBitmapShape xlsBitmapShape = (XlsBitmapShape)this.ᜅ.HeaderFooterShapes[XlsPageSetupBase.ᜀ[1]];
					if (xlsBitmapShape != null)
					{
						if (true)
						{
						}
						return xlsBitmapShape.Picture;
					}
					break;
				}
				}
				return null;
			}
			set
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
				this.ᜅ.HeaderFooterShapes.SetPicture(XlsPageSetupBase.ᜀ[1], value);
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06005FDF RID: 24543 RVA: 0x003C8E60 File Offset: 0x003C7E60
		// (set) Token: 0x06005FE0 RID: 24544 RVA: 0x003C8EA4 File Offset: 0x003C7EA4
		public string CenterHeader
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
				return this.m_arrHeaders[1];
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6B:
					if (true)
					{
					}
					num = 0;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_5A;
					}
					if (!(this.m_arrHeaders[1] != value))
					{
						return;
					}
					num = 1;
				}
				IL_5A:
				this.m_arrHeaders[1] = value;
				this.SetChanged();
				goto IL_6B;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06005FE1 RID: 24545 RVA: 0x003C8F30 File Offset: 0x003C7F30
		// (set) Token: 0x06005FE2 RID: 24546 RVA: 0x003C8F74 File Offset: 0x003C7F74
		public bool CenterHorizontally
		{
			get
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
				return this.m_bHCenter;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6A:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_53;
					}
					if (this.m_bHCenter == value)
					{
						return;
					}
					num = 1;
				}
				IL_53:
				if (true)
				{
				}
				this.m_bHCenter = value;
				this.SetChanged();
				goto IL_6A;
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06005FE3 RID: 24547 RVA: 0x003C8FF8 File Offset: 0x003C7FF8
		// (set) Token: 0x06005FE4 RID: 24548 RVA: 0x003C903C File Offset: 0x003C803C
		public bool CenterVertically
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
				return this.m_bVCenter;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_62:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_53;
					case 1:
						goto IL_6A;
					}
					if (this.m_bVCenter == value)
					{
						return;
					}
					num = 0;
				}
				IL_53:
				this.m_bVCenter = value;
				this.SetChanged();
				goto IL_62;
				IL_6A:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06005FE5 RID: 24549 RVA: 0x003C90C0 File Offset: 0x003C80C0
		// (set) Token: 0x06005FE6 RID: 24550 RVA: 0x003C9108 File Offset: 0x003C8108
		public int Copies
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
				return (int)this.ᜄ.ᜄ();
			}
			set
			{
				int a_ = 19;
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						this.ᜄ.ᜃ((ushort)value);
						this.ᜄ.ᜆ(false);
						this.SetChanged();
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_89;
						default:
							if (false)
							{
							}
							if (((XlsWorkbook)this.ᜅ.Workbook).Loading)
							{
								num = 5;
								continue;
							}
							goto IL_75;
						}
						break;
					case 3:
						goto IL_51;
					case 4:
						goto IL_89;
					case 5:
						value = 1;
						num = 3;
						continue;
					case 6:
						if (this.ᜄ.ᜄ() != (ushort)value)
						{
							num = 1;
							continue;
						}
						return;
					}
					if (value < 1)
					{
						num = 4;
						continue;
					}
					IL_51:
					num = 6;
					continue;
					IL_89:
					num = 2;
				}
				IL_75:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("݈㹊⁌ⵎ㑐⅒畔㡖㽘筚㹜ぞᅠ੢dᑦ䥨ࡪ౬Ů兰ᵲᩴͶ奸᥺᡼彾ꦈﾊﾐ뎒꒔", a_));
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06005FE7 RID: 24551 RVA: 0x003C9230 File Offset: 0x003C8230
		// (set) Token: 0x06005FE8 RID: 24552 RVA: 0x003C9278 File Offset: 0x003C8278
		public bool Draft
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
				return this.ᜄ.ᜊ();
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_74:
					num = 2;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_60;
					case 2:
						return;
					}
					if (this.ᜄ.ᜊ() == value)
					{
						return;
					}
					num = 1;
				}
				IL_60:
				this.ᜄ.ᜂ(value);
				this.SetChanged();
				goto IL_74;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06005FE9 RID: 24553 RVA: 0x003C9304 File Offset: 0x003C8304
		// (set) Token: 0x06005FEA RID: 24554 RVA: 0x003C934C File Offset: 0x003C834C
		public int FirstPageNumber
		{
			get
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
				return (int)this.ᜄ.ᜐ();
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_7D:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_59;
					case 1:
						return;
					}
					if (this.ᜄ.ᜐ() == (short)value)
					{
						return;
					}
					num = 0;
				}
				IL_59:
				if (true)
				{
				}
				this.ᜄ.ᜀ((short)value);
				this.AutoFirstPageNumber = false;
				this.SetChanged();
				goto IL_7D;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06005FEB RID: 24555 RVA: 0x003C93E0 File Offset: 0x003C83E0
		// (set) Token: 0x06005FEC RID: 24556 RVA: 0x003C9428 File Offset: 0x003C8428
		public double FooterMarginInch
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
				return this.ᜄ.ᜁ();
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_74:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						goto IL_60;
					}
					if (true)
					{
					}
					if (this.ᜄ.ᜁ() == value)
					{
						return;
					}
					num = 2;
				}
				IL_60:
				this.ᜄ.ᜁ(value);
				this.SetChanged();
				goto IL_74;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06005FED RID: 24557 RVA: 0x003C94B4 File Offset: 0x003C84B4
		// (set) Token: 0x06005FEE RID: 24558 RVA: 0x003C94FC File Offset: 0x003C84FC
		public double HeaderMarginInch
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
				return this.ᜄ.ᜑ();
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_74:
					num = 1;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_60;
					case 1:
						return;
					}
					if (this.ᜄ.ᜑ() == value)
					{
						return;
					}
					num = 0;
				}
				IL_60:
				this.ᜄ.ᜀ(value);
				this.SetChanged();
				goto IL_74;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06005FEF RID: 24559 RVA: 0x003C9588 File Offset: 0x003C8588
		// (set) Token: 0x06005FF0 RID: 24560 RVA: 0x003C95CC File Offset: 0x003C85CC
		public string LeftFooter
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
				return this.m_arrFooters[0];
			}
			set
			{
				if (true)
				{
				}
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_73:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_62;
					case 2:
						return;
					}
					if (!(this.m_arrFooters[0] != value))
					{
						return;
					}
					num = 0;
				}
				IL_62:
				this.m_arrFooters[0] = value;
				this.SetChanged();
				goto IL_73;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06005FF1 RID: 24561 RVA: 0x003C9658 File Offset: 0x003C8658
		// (set) Token: 0x06005FF2 RID: 24562 RVA: 0x003C969C File Offset: 0x003C869C
		public string LeftHeader
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
				return this.m_arrHeaders[0];
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6B:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_73;
					case 1:
						goto IL_5A;
					}
					if (!(this.m_arrHeaders[0] != value))
					{
						return;
					}
					num = 1;
				}
				IL_5A:
				this.m_arrHeaders[0] = value;
				this.SetChanged();
				goto IL_6B;
				IL_73:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06005FF3 RID: 24563 RVA: 0x003C9728 File Offset: 0x003C8728
		// (set) Token: 0x06005FF4 RID: 24564 RVA: 0x003C9790 File Offset: 0x003C8790
		public Image LeftFooterImage
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					XlsBitmapShape xlsBitmapShape = (XlsBitmapShape)this.ᜅ.HeaderFooterShapes[XlsPageSetupBase.ᜁ[0]];
					if (xlsBitmapShape != null)
					{
						return xlsBitmapShape.Picture;
					}
					break;
				}
				}
				return null;
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
				this.ᜅ.HeaderFooterShapes.SetPicture(XlsPageSetupBase.ᜁ[0], value);
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06005FF5 RID: 24565 RVA: 0x003C97E4 File Offset: 0x003C87E4
		// (set) Token: 0x06005FF6 RID: 24566 RVA: 0x003C984C File Offset: 0x003C884C
		public Image LeftHeaderImage
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					XlsBitmapShape xlsBitmapShape = (XlsBitmapShape)this.ᜅ.HeaderFooterShapes[XlsPageSetupBase.ᜀ[0]];
					if (xlsBitmapShape != null)
					{
						return xlsBitmapShape.Picture;
					}
					if (true)
					{
					}
					break;
				}
				}
				return null;
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
				this.ᜅ.HeaderFooterShapes.SetPicture(XlsPageSetupBase.ᜀ[0], value);
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06005FF7 RID: 24567 RVA: 0x003C98A0 File Offset: 0x003C88A0
		// (set) Token: 0x06005FF8 RID: 24568 RVA: 0x003C98E4 File Offset: 0x003C88E4
		public double LeftMargin
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
				return this.m_dLeftMargin;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6A:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						return;
					case 2:
						goto IL_5B;
					}
					if (this.m_dLeftMargin == value)
					{
						return;
					}
					num = 2;
				}
				IL_5B:
				this.m_dLeftMargin = value;
				this.SetChanged();
				goto IL_6A;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06005FF9 RID: 24569 RVA: 0x003C9968 File Offset: 0x003C8968
		// (set) Token: 0x06005FFA RID: 24570 RVA: 0x003C99B8 File Offset: 0x003C89B8
		public OrderType Order
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (this.ᜄ.\u170D())
					{
						return OrderType.OverThenDown;
					}
					break;
				}
				return OrderType.DownThenOver;
			}
			set
			{
				bool flag;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_47:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							this.ᜄ.ᜄ(flag);
							this.SetChanged();
							num = 0;
							continue;
						case 2:
							if (this.ᜄ.\u170D() != flag)
							{
								num = 1;
								continue;
							}
							return;
						}
						goto IL_42;
					}
					return;
				}
				}
				if (true)
				{
				}
				if (false)
				{
				}
				IL_42:
				flag = (value == OrderType.OverThenDown);
				goto IL_47;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06005FFB RID: 24571 RVA: 0x003C9A48 File Offset: 0x003C8A48
		// (set) Token: 0x06005FFC RID: 24572 RVA: 0x003C9A98 File Offset: 0x003C8A98
		public PageOrientationType Orientation
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (this.ᜄ.ᜀ())
					{
						return PageOrientationType.Portrait;
					}
					break;
				}
				return PageOrientationType.Landscape;
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
				this.ᜄ.ᜅ(value == PageOrientationType.Portrait);
				this.ᜄ.ᜆ(false);
				this.ᜄ.ᜀ(false);
				this.SetChanged();
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06005FFD RID: 24573 RVA: 0x003C9B00 File Offset: 0x003C8B00
		// (set) Token: 0x06005FFE RID: 24574 RVA: 0x003C9B48 File Offset: 0x003C8B48
		public PaperSizeType PaperSize
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
				return (PaperSizeType)this.ᜄ.\u1715();
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
				this.ᜄ.ᜀ((ushort)value);
				this.ᜄ.ᜆ(false);
				this.SetChanged();
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06005FFF RID: 24575 RVA: 0x003C9BA4 File Offset: 0x003C8BA4
		// (set) Token: 0x06006000 RID: 24576 RVA: 0x003C9C3C File Offset: 0x003C8C3C
		public PrintCommentType PrintComments
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					if (true)
					{
					}
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return PrintCommentType.InPlace;
						case 1:
							goto IL_59;
						case 2:
							if (this.ᜄ.ᜇ())
							{
								num = 0;
								continue;
							}
							return PrintCommentType.SheetEnd;
						}
						if (!this.ᜄ.\u1714())
						{
							num = 1;
						}
						else
						{
							num = 2;
						}
					}
					IL_59:
					break;
				}
				}
				return PrintCommentType.NoComments;
			}
			set
			{
				for (;;)
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_7C;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_68;
							default:
								goto IL_A9;
							}
							break;
						case 2:
							num = 1;
							continue;
						case 3:
							goto IL_66;
						case 4:
							switch (value)
							{
							case PrintCommentType.InPlace:
								this.ᜄ.ᜃ(true);
								this.ᜄ.ᜈ(true);
								num = 3;
								continue;
							case PrintCommentType.NoComments:
								goto IL_68;
							case PrintCommentType.SheetEnd:
								this.ᜄ.ᜃ(true);
								this.ᜄ.ᜈ(false);
								num = 5;
								continue;
							default:
								num = 2;
								continue;
							}
							break;
						case 5:
							goto IL_D4;
						}
						break;
						IL_68:
						this.ᜄ.ᜃ(false);
						num = 0;
					}
				}
				IL_66:
				IL_7C:
				goto IL_D6;
				IL_A9:
				if (false)
				{
				}
				IL_D4:
				IL_D6:
				if (true)
				{
				}
				this.SetChanged();
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06006001 RID: 24577 RVA: 0x003C9D30 File Offset: 0x003C8D30
		// (set) Token: 0x06006002 RID: 24578 RVA: 0x003C9D78 File Offset: 0x003C8D78
		public PrintErrorsType PrintErrors
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
				return this.ᜄ.ᜈ();
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_74:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_60;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (this.ᜄ.ᜈ() == value)
					{
						return;
					}
					num = 1;
				}
				IL_60:
				this.ᜄ.ᜀ(value);
				this.SetChanged();
				goto IL_74;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06006003 RID: 24579 RVA: 0x003C9E04 File Offset: 0x003C8E04
		// (set) Token: 0x06006004 RID: 24580 RVA: 0x003C9E4C File Offset: 0x003C8E4C
		public bool PrintNotes
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
				return this.ᜄ.\u1714();
			}
			set
			{
				if (true)
				{
				}
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_74:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_60;
					case 1:
						return;
					}
					if (this.ᜄ.\u1714() == value)
					{
						return;
					}
					num = 0;
				}
				IL_60:
				this.ᜄ.ᜃ(value);
				this.SetChanged();
				goto IL_74;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06006005 RID: 24581 RVA: 0x003C9ED8 File Offset: 0x003C8ED8
		// (set) Token: 0x06006006 RID: 24582 RVA: 0x003C9F20 File Offset: 0x003C8F20
		public int PrintQuality
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
				return (int)this.ᜄ.ᜉ();
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
				this.ᜄ.ᜁ((ushort)value);
				this.ᜄ.ᜂ((ushort)value);
				this.ᜄ.ᜆ(false);
				this.SetChanged();
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06006007 RID: 24583 RVA: 0x003C9F88 File Offset: 0x003C8F88
		// (set) Token: 0x06006008 RID: 24584 RVA: 0x003C9FCC File Offset: 0x003C8FCC
		public string RightFooter
		{
			get
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
				return this.m_arrFooters[2];
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_73:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_62;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (!(this.m_arrFooters[2] != value))
					{
						return;
					}
					num = 1;
				}
				IL_62:
				this.m_arrFooters[2] = value;
				this.SetChanged();
				goto IL_73;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x003CA058 File Offset: 0x003C9058
		// (set) Token: 0x0600600A RID: 24586 RVA: 0x003CA0C0 File Offset: 0x003C90C0
		public Image RightFooterImage
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					XlsBitmapShape xlsBitmapShape = (XlsBitmapShape)this.ᜅ.HeaderFooterShapes[XlsPageSetupBase.ᜁ[2]];
					if (xlsBitmapShape != null)
					{
						return xlsBitmapShape.Picture;
					}
					break;
				}
				}
				if (true)
				{
				}
				return null;
			}
			set
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
				this.ᜅ.HeaderFooterShapes.SetPicture(XlsPageSetupBase.ᜁ[2], value);
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x0600600B RID: 24587 RVA: 0x003CA114 File Offset: 0x003C9114
		// (set) Token: 0x0600600C RID: 24588 RVA: 0x003CA158 File Offset: 0x003C9158
		public string RightHeader
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
				return this.m_arrHeaders[2];
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.m_arrHeaders[2] = value;
						this.SetChanged();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					if (!(this.m_arrHeaders[2] != value))
					{
						break;
					}
					if (true)
					{
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x003CA1E4 File Offset: 0x003C91E4
		// (set) Token: 0x0600600E RID: 24590 RVA: 0x003CA24C File Offset: 0x003C924C
		public Image RightHeaderImage
		{
			get
			{
				XlsBitmapShape xlsBitmapShape;
				for (;;)
				{
					if (true)
					{
					}
					xlsBitmapShape = (XlsBitmapShape)this.ᜅ.HeaderFooterShapes[XlsPageSetupBase.ᜀ[2]];
					if (xlsBitmapShape == null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					goto Block_1;
				}
				return null;
				Block_1:
				if (false)
				{
				}
				return xlsBitmapShape.Picture;
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
				this.ᜅ.HeaderFooterShapes.SetPicture(XlsPageSetupBase.ᜀ[2], value);
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x0600600F RID: 24591 RVA: 0x003CA2A0 File Offset: 0x003C92A0
		// (set) Token: 0x06006010 RID: 24592 RVA: 0x003CA2E4 File Offset: 0x003C92E4
		public double RightMargin
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
				return this.m_dRightMargin;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.m_dRightMargin = value;
						this.SetChanged();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (this.m_dRightMargin == value)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06006011 RID: 24593 RVA: 0x003CA368 File Offset: 0x003C9368
		// (set) Token: 0x06006012 RID: 24594 RVA: 0x003CA3AC File Offset: 0x003C93AC
		public double TopMargin
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
				return this.m_dTopMargin;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						this.m_dTopMargin = value;
						this.SetChanged();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (this.m_dTopMargin == value)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06006013 RID: 24595 RVA: 0x003CA430 File Offset: 0x003C9430
		// (set) Token: 0x06006014 RID: 24596 RVA: 0x003CA478 File Offset: 0x003C9478
		public int Zoom
		{
			get
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
				return (int)this.ᜄ.ᜆ();
			}
			set
			{
				int a_ = 19;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (value <= 400)
						{
							goto IL_94;
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
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_92;
					case 3:
						if (true)
						{
						}
						break;
					}
					if (value < 10)
					{
						break;
					}
					num = 0;
				}
				IL_4A:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ፈ⑊≌≎煐╒㑔㭖ⱘ㹚絜㉞ᑠၢᅤ䝦୨๪䵬൮ᑰᙲŴvᱸṺ፼彾낀뎂ꖄ권뮎ꆐꎒ떔ﲘﺜ爵쾠힢认", a_));
				IL_92:
				goto IL_4A;
				IL_94:
				this.ᜄ.ᜆ((ushort)value);
				this.ᜄ.ᜆ(false);
				this.SetChanged();
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06006015 RID: 24597 RVA: 0x003CA538 File Offset: 0x003C9538
		// (set) Token: 0x06006016 RID: 24598 RVA: 0x003CA58C File Offset: 0x003C958C
		public Bitmap BackgoundImage
		{
			get
			{
				while (this.ᜆ != null)
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
						return this.ᜆ.ᜊ();
					}
				}
				return null;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_99;
					case 1:
						goto IL_7C;
					case 2:
						if (this.ᜆ == null)
						{
							num = 0;
							continue;
						}
						goto IL_A3;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_99;
						default:
							goto IL_57;
						}
						break;
					case 4:
						if (true)
						{
						}
						break;
					}
					if (value == null)
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
					IL_99:
					this.ᜆ = (spr\u1DA6)spr\u175E.ᜀ(TBIFFRecord.Bitmap);
					num = 1;
				}
				IL_57:
				if (false)
				{
				}
				this.ᜆ = null;
				return;
				IL_7C:
				IL_A3:
				this.ᜆ.ᜀ(value);
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06006017 RID: 24599 RVA: 0x003CA648 File Offset: 0x003C9648
		public double PageWidth
		{
			get
			{
				int num = 5;
				XlsPageSetupBase.PaperSizeEntry paperSizeEntry2;
				for (;;)
				{
					XlsPageSetupBase.PaperSizeEntry paperSizeEntry;
					switch (num)
					{
					case 0:
						goto IL_72;
					case 1:
						paperSizeEntry = XlsPageSetupBase.ᜂ[(int)this.PaperSize];
						goto IL_8F;
					case 2:
						if (this.Orientation != PageOrientationType.Portrait)
						{
							num = 3;
							continue;
						}
						goto IL_CE;
					case 3:
						goto IL_AF;
					case 4:
						paperSizeEntry = XlsPageSetupBase.ᜂ[9];
						goto IL_8F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_72:
						num = 4;
						continue;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (!XlsPageSetupBase.ᜂ.ContainsKey((int)this.PaperSize))
						{
							num = 0;
							continue;
						}
						num = 1;
						continue;
					}
					IL_8F:
					paperSizeEntry2 = paperSizeEntry;
					num = 2;
				}
				IL_AF:
				return paperSizeEntry2.Height;
				IL_CE:
				return paperSizeEntry2.Width;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06006018 RID: 24600 RVA: 0x003CA72C File Offset: 0x003C972C
		public double PageHeight
		{
			get
			{
				int num = 2;
				XlsPageSetupBase.PaperSizeEntry paperSizeEntry2;
				for (;;)
				{
					XlsPageSetupBase.PaperSizeEntry paperSizeEntry;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_72;
					case 1:
						paperSizeEntry = XlsPageSetupBase.ᜂ[9];
						goto IL_8F;
					case 3:
						if (this.Orientation != PageOrientationType.Portrait)
						{
							num = 4;
							continue;
						}
						goto IL_CE;
					case 4:
						goto IL_AF;
					case 5:
						paperSizeEntry = XlsPageSetupBase.ᜂ[(int)this.PaperSize];
						goto IL_8F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_72:
						num = 1;
						continue;
					default:
						if (false)
						{
						}
						if (!XlsPageSetupBase.ᜂ.ContainsKey((int)this.PaperSize))
						{
							num = 0;
							continue;
						}
						num = 5;
						continue;
					}
					IL_8F:
					paperSizeEntry2 = paperSizeEntry;
					num = 3;
				}
				IL_AF:
				return paperSizeEntry2.Width;
				IL_CE:
				return paperSizeEntry2.Height;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06006019 RID: 24601 RVA: 0x003CA810 File Offset: 0x003C9810
		// (set) Token: 0x0600601A RID: 24602 RVA: 0x003CA858 File Offset: 0x003C9858
		public int HResolution
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
				return (int)this.ᜄ.ᜉ();
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
				this.ᜄ.ᜁ((ushort)value);
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x0600601B RID: 24603 RVA: 0x003CA8A0 File Offset: 0x003C98A0
		// (set) Token: 0x0600601C RID: 24604 RVA: 0x003CA8E8 File Offset: 0x003C98E8
		public int VResolution
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
				return (int)this.ᜄ.ᜂ();
			}
			set
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
				this.ᜄ.ᜂ((ushort)value);
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x0600601D RID: 24605 RVA: 0x003CA930 File Offset: 0x003C9930
		// (set) Token: 0x0600601E RID: 24606 RVA: 0x003CA978 File Offset: 0x003C9978
		public string FullHeaderString
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
				return this.CreateHeaderFooterString(this.m_arrHeaders);
			}
			set
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
				this.m_arrHeaders = this.ParseHeaderFooterString(value);
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x0600601F RID: 24607 RVA: 0x003CA9C0 File Offset: 0x003C99C0
		// (set) Token: 0x06006020 RID: 24608 RVA: 0x003CAA08 File Offset: 0x003C9A08
		public string FullFooterString
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
				return this.CreateHeaderFooterString(this.m_arrFooters);
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
				this.m_arrFooters = this.ParseHeaderFooterString(value);
			}
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x003CAA50 File Offset: 0x003C9A50
		static XlsPageSetupBase()
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			XlsPageSetupBase.ᜀ = new string[]
			{
				RecordTableEnumerator.b("གൄ", a_),
				RecordTableEnumerator.b("Bൄ", a_),
				RecordTableEnumerator.b("ᅂൄ", a_)
			};
			XlsPageSetupBase.ᜁ = new string[]
			{
				RecordTableEnumerator.b("ག̈́", a_),
				RecordTableEnumerator.b("B̈́", a_),
				RecordTableEnumerator.b("ᅂ̈́", a_)
			};
			XlsPageSetupBase.ᜂ = new Dictionary<int, XlsPageSetupBase.PaperSizeEntry>();
			XlsPageSetupBase.ᜂ.Add(1, new XlsPageSetupBase.PaperSizeEntry(8.5, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(2, new XlsPageSetupBase.PaperSizeEntry(8.5, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(3, new XlsPageSetupBase.PaperSizeEntry(11.0, 17.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(4, new XlsPageSetupBase.PaperSizeEntry(17.0, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(5, new XlsPageSetupBase.PaperSizeEntry(8.5, 14.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(6, new XlsPageSetupBase.PaperSizeEntry(5.5, 8.5, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(7, new XlsPageSetupBase.PaperSizeEntry(7.25, 10.5, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(8, new XlsPageSetupBase.PaperSizeEntry(297.0, 420.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(9, new XlsPageSetupBase.PaperSizeEntry(210.0, 297.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(10, new XlsPageSetupBase.PaperSizeEntry(210.0, 297.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(11, new XlsPageSetupBase.PaperSizeEntry(148.0, 210.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(12, new XlsPageSetupBase.PaperSizeEntry(257.0, 368.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(13, new XlsPageSetupBase.PaperSizeEntry(182.0, 257.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(14, new XlsPageSetupBase.PaperSizeEntry(8.5, 13.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(15, new XlsPageSetupBase.PaperSizeEntry(215.0, 275.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(16, new XlsPageSetupBase.PaperSizeEntry(10.0, 14.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(17, new XlsPageSetupBase.PaperSizeEntry(11.0, 17.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(18, new XlsPageSetupBase.PaperSizeEntry(8.5, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(19, new XlsPageSetupBase.PaperSizeEntry(3.875, 8.875, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(20, new XlsPageSetupBase.PaperSizeEntry(4.125, 9.5, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(21, new XlsPageSetupBase.PaperSizeEntry(4.5, 10.375, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(22, new XlsPageSetupBase.PaperSizeEntry(4.75, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(23, new XlsPageSetupBase.PaperSizeEntry(5.0, 11.5, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(24, new XlsPageSetupBase.PaperSizeEntry(17.0, 22.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(25, new XlsPageSetupBase.PaperSizeEntry(22.0, 34.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(26, new XlsPageSetupBase.PaperSizeEntry(34.0, 44.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(27, new XlsPageSetupBase.PaperSizeEntry(110.0, 220.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(28, new XlsPageSetupBase.PaperSizeEntry(162.0, 229.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(29, new XlsPageSetupBase.PaperSizeEntry(324.0, 458.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(30, new XlsPageSetupBase.PaperSizeEntry(229.0, 324.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(31, new XlsPageSetupBase.PaperSizeEntry(114.0, 162.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(32, new XlsPageSetupBase.PaperSizeEntry(114.0, 229.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(33, new XlsPageSetupBase.PaperSizeEntry(250.0, 353.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(34, new XlsPageSetupBase.PaperSizeEntry(176.0, 250.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(35, new XlsPageSetupBase.PaperSizeEntry(125.0, 176.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(36, new XlsPageSetupBase.PaperSizeEntry(110.0, 230.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(37, new XlsPageSetupBase.PaperSizeEntry(3.875, 7.5, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(38, new XlsPageSetupBase.PaperSizeEntry(3.625, 6.5, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(39, new XlsPageSetupBase.PaperSizeEntry(14.875, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(40, new XlsPageSetupBase.PaperSizeEntry(8.5, 12.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(41, new XlsPageSetupBase.PaperSizeEntry(8.5, 13.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(42, new XlsPageSetupBase.PaperSizeEntry(250.0, 353.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(43, new XlsPageSetupBase.PaperSizeEntry(100.0, 148.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(44, new XlsPageSetupBase.PaperSizeEntry(9.0, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(45, new XlsPageSetupBase.PaperSizeEntry(10.0, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(46, new XlsPageSetupBase.PaperSizeEntry(15.0, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(47, new XlsPageSetupBase.PaperSizeEntry(220.0, 220.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(50, new XlsPageSetupBase.PaperSizeEntry(9.5, 12.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(51, new XlsPageSetupBase.PaperSizeEntry(9.5, 15.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(52, new XlsPageSetupBase.PaperSizeEntry(11.6875, 18.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(53, new XlsPageSetupBase.PaperSizeEntry(235.0, 322.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(54, new XlsPageSetupBase.PaperSizeEntry(8.5, 11.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(55, new XlsPageSetupBase.PaperSizeEntry(210.0, 297.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(56, new XlsPageSetupBase.PaperSizeEntry(9.5, 12.0, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(57, new XlsPageSetupBase.PaperSizeEntry(227.0, 356.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(58, new XlsPageSetupBase.PaperSizeEntry(305.0, 487.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(59, new XlsPageSetupBase.PaperSizeEntry(8.5, 12.6875, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(60, new XlsPageSetupBase.PaperSizeEntry(210.0, 330.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(61, new XlsPageSetupBase.PaperSizeEntry(148.0, 210.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(62, new XlsPageSetupBase.PaperSizeEntry(182.0, 257.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(63, new XlsPageSetupBase.PaperSizeEntry(322.0, 445.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(64, new XlsPageSetupBase.PaperSizeEntry(174.0, 235.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(65, new XlsPageSetupBase.PaperSizeEntry(201.0, 276.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(66, new XlsPageSetupBase.PaperSizeEntry(420.0, 594.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(67, new XlsPageSetupBase.PaperSizeEntry(297.0, 420.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(68, new XlsPageSetupBase.PaperSizeEntry(322.0, 445.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(69, new XlsPageSetupBase.PaperSizeEntry(200.0, 148.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(70, new XlsPageSetupBase.PaperSizeEntry(105.0, 148.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(75, new XlsPageSetupBase.PaperSizeEntry(11.0, 8.5, MeasureUnits.Inch));
			XlsPageSetupBase.ᜂ.Add(76, new XlsPageSetupBase.PaperSizeEntry(420.0, 297.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(77, new XlsPageSetupBase.PaperSizeEntry(297.0, 210.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(78, new XlsPageSetupBase.PaperSizeEntry(210.0, 148.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(79, new XlsPageSetupBase.PaperSizeEntry(364.0, 257.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(80, new XlsPageSetupBase.PaperSizeEntry(257.0, 182.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(81, new XlsPageSetupBase.PaperSizeEntry(148.0, 100.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(82, new XlsPageSetupBase.PaperSizeEntry(148.0, 200.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(83, new XlsPageSetupBase.PaperSizeEntry(148.0, 105.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(88, new XlsPageSetupBase.PaperSizeEntry(128.0, 182.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(89, new XlsPageSetupBase.PaperSizeEntry(182.0, 128.0, MeasureUnits.Millimeter));
			XlsPageSetupBase.ᜂ.Add(90, new XlsPageSetupBase.PaperSizeEntry(12.0, 11.0, MeasureUnits.Inch));
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x003CB658 File Offset: 0x003CA658
		internal XlsPageSetupBase(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜄ = (spr\u1A56)spr\u175E.ᜀ(TBIFFRecord.PrintSetup);
			this.FindParents();
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x003CB710 File Offset: 0x003CA710
		protected virtual void FindParents()
		{
			int a_ = 8;
			for (;;)
			{
				this.ᜅ = (XlsObject.FindParent(base.Parent, typeof(XlsWorksheetBase), true) as XlsWorksheetBase);
				if (this.ᜅ == null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				goto Block_2;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("渽ℿぁ⅃⡅㱇橉⍋ⱍ㩏㝑㝓≕硗㥙㵛そ๟ൡၣ䙥੧ཀྵ䱫࡭Ὧݱᩳት噷", a_));
			Block_2:
			if (false)
			{
			}
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x003CB794 File Offset: 0x003CA794
		protected string[] ParseHeaderFooterString(string strToSplit)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 6;
				string[] array;
				for (;;)
				{
					int num3;
					int num4;
					int num5;
					int num6;
					int length;
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 <= num3)
						{
							goto IL_1DD;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_449;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					case 1:
					{
						int num2;
						num4 = num2;
						goto IL_449;
					}
					case 2:
						goto IL_1DD;
					case 3:
						if (num5 >= 0)
						{
							num = 18;
							continue;
						}
						goto IL_336;
					case 4:
						num = 26;
						continue;
					case 5:
						goto IL_261;
					case 7:
					{
						int num2;
						if (num2 == -1)
						{
							num = 33;
							continue;
						}
						goto IL_312;
					}
					case 8:
					{
						int num2;
						if (num2 >= 0)
						{
							num = 31;
							continue;
						}
						return array;
					}
					case 9:
						if (num5 > 0)
						{
							num = 4;
							continue;
						}
						goto IL_336;
					case 10:
						if (num5 > num3)
						{
							num = 22;
							continue;
						}
						num = 0;
						continue;
					case 11:
						goto IL_D6;
					case 12:
					{
						int num2;
						if (num2 > num5)
						{
							num = 15;
							continue;
						}
						goto IL_DB;
					}
					case 13:
					{
						int num2;
						array[1] = strToSplit.Substring(0, num2);
						num = 5;
						continue;
					}
					case 14:
						goto IL_336;
					case 15:
					{
						if (true)
						{
						}
						int num2;
						num6 = num2;
						num = 29;
						continue;
					}
					case 16:
						goto IL_1DD;
					case 17:
					{
						if (length == 0)
						{
							num = 35;
							continue;
						}
						num3 = strToSplit.IndexOf(RecordTableEnumerator.b("ᠽి", a_));
						num5 = strToSplit.IndexOf(RecordTableEnumerator.b("ᠽ̿", a_));
						int num2 = strToSplit.IndexOf(RecordTableEnumerator.b("ᠽሿ", a_));
						num = 36;
						continue;
					}
					case 18:
						num6 = length;
						num = 12;
						continue;
					case 19:
						if (num3 >= 0)
						{
							num = 32;
							continue;
						}
						goto IL_111;
					case 20:
					{
						int num2;
						if (num2 > 0)
						{
							num = 23;
							continue;
						}
						return array;
					}
					case 21:
						goto IL_111;
					case 22:
						num4 = num5;
						num = 2;
						continue;
					case 23:
						num = 37;
						continue;
					case 24:
						num = 34;
						continue;
					case 25:
						num = 27;
						continue;
					case 26:
						if (num3 < 0)
						{
							num = 28;
							continue;
						}
						goto IL_336;
					case 27:
					{
						int num2;
						if (num5 == num2)
						{
							num = 30;
							continue;
						}
						goto IL_312;
					}
					case 28:
						array[0] = strToSplit.Substring(0, num5);
						num = 14;
						continue;
					case 29:
						goto IL_DB;
					case 30:
						num = 7;
						continue;
					case 31:
					{
						int num7 = length;
						int num2;
						array[2] = strToSplit.Substring(num2 + 2, num7 - num2 - 2);
						num = 20;
						continue;
					}
					case 32:
						num4 = length;
						num = 10;
						continue;
					case 33:
						goto IL_220;
					case 34:
						if (num3 < 0)
						{
							num = 13;
							continue;
						}
						return array;
					case 35:
						return array;
					case 36:
						if (num3 == num5)
						{
							num = 25;
							continue;
						}
						goto IL_312;
					case 37:
						if (num5 < 0)
						{
							num = 24;
							continue;
						}
						return array;
					}
					if (strToSplit == null)
					{
						num = 11;
						continue;
					}
					array = new string[]
					{
						string.Empty,
						string.Empty,
						string.Empty
					};
					length = strToSplit.Length;
					num = 17;
					continue;
					IL_DB:
					array[1] = strToSplit.Substring(num5 + 2, num6 - num5 - 2);
					num = 9;
					continue;
					IL_111:
					num = 3;
					continue;
					IL_1DD:
					array[0] = strToSplit.Substring(num3 + 2, num4 - num3 - 2);
					num = 21;
					continue;
					IL_312:
					num = 19;
					continue;
					IL_336:
					num = 8;
					continue;
					IL_449:
					num = 16;
				}
				IL_D6:
				throw new ArgumentNullException(RecordTableEnumerator.b("䴽㐿ぁ၃⥅ᭇ㩉⁋❍⑏", a_));
				IL_220:
				array[1] = strToSplit;
				return array;
				IL_261:
				return array;
			}
			}
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x003CBC18 File Offset: 0x003CAC18
		protected string CreateHeaderFooterString(string[] parts)
		{
			int a_ = 15;
			int num = 16;
			string text;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					num = 10;
					continue;
				case 2:
					goto IL_1F8;
				case 3:
					num = 6;
					continue;
				case 4:
					if (parts[1].Length > 0)
					{
						num = 13;
						continue;
					}
					goto IL_1F8;
				case 5:
					if (parts[2] != null)
					{
						num = 12;
						continue;
					}
					return text;
				case 6:
					if (parts[0].Length > 0)
					{
						num = 15;
						continue;
					}
					goto IL_218;
				case 7:
					if (parts[1] != null)
					{
						num = 0;
						continue;
					}
					goto IL_1F8;
				case 8:
					goto IL_83;
				case 9:
					goto IL_F7;
				case 10:
					goto IL_E3;
				case 11:
					goto IL_18F;
				case 12:
					num = 14;
					continue;
				case 13:
					text = text + RecordTableEnumerator.b("捄ц", a_) + parts[1];
					num = 2;
					continue;
				case 14:
					if (parts[2].Length > 0)
					{
						num = 20;
						continue;
					}
					return text;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						if (false)
						{
						}
						text = text + RecordTableEnumerator.b("捄୆", a_) + parts[0];
						num = 17;
						continue;
					}
					break;
				case 17:
					goto IL_218;
				case 18:
					if (parts.Length >= 3)
					{
						num = 1;
						continue;
					}
					goto IL_1D6;
				case 19:
					if (parts[0] != null)
					{
						num = 3;
						continue;
					}
					goto IL_218;
				case 20:
					text = text + RecordTableEnumerator.b("捄ᕆ", a_) + parts[2];
					num = 11;
					continue;
				}
				if (parts == null)
				{
					if (true)
					{
					}
					num = 8;
					continue;
				}
				num = 18;
				continue;
				IL_E3:
				if (parts.Length > 3)
				{
					num = 9;
					continue;
				}
				text = string.Empty;
				num = 19;
				continue;
				IL_1F8:
				num = 5;
				continue;
				IL_218:
				num = 7;
			}
			IL_83:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕄♆㭈㽊㹌", a_));
			IL_F7:
			goto IL_1D6;
			IL_18F:
			return text;
			IL_1D6:
			throw new ArgumentException(RecordTableEnumerator.b("ᕄ♆㭈㽊㹌潎ぐ⅒❔㙖⁘筚⹜㝞๠ᙢ।ͦ䥨ͪ౬᥮ᑰ卲Ŵὶ୸Ṻ᡼彾歷ﲎ", a_), RecordTableEnumerator.b("㕄♆㭈㽊㹌", a_));
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x003CBEA4 File Offset: 0x003CAEA4
		internal virtual void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜃ != null)
						{
							num = 6;
							continue;
						}
						goto IL_235;
					case 1:
						goto IL_59;
					case 2:
					{
						spr\u1B3F spr_u1B3F;
						spr_u1B3F.ᜀ(this.m_bHCenter ? 1 : 0);
						records.ᜀ(spr_u1B3F);
						spr\u1B3F spr_u1B3F2 = (spr\u1B3F)spr\u175E.ᜀ(TBIFFRecord.VCenter);
						num = 7;
						continue;
					}
					case 4:
						goto IL_93;
					case 5:
						goto IL_233;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							break;
						}
						records.ᜀ(this.ᜃ);
						if (true)
						{
						}
						num = 5;
						continue;
					case 7:
					{
						spr\u1B3F spr_u1B3F2;
						spr_u1B3F2.ᜀ(this.m_bVCenter ? 1 : 0);
						records.ᜀ(spr_u1B3F2);
						this.ᜀ(records, TBIFFRecord.LeftMargin, this.m_dLeftMargin, 0.75);
						this.ᜀ(records, TBIFFRecord.RightMargin, this.m_dRightMargin, 0.75);
						this.ᜀ(records, TBIFFRecord.TopMargin, this.m_dTopMargin, 1.0);
						this.ᜀ(records, TBIFFRecord.BottomMargin, this.m_dBottomMargin, 1.0);
						num = 0;
						continue;
					}
					case 8:
					{
						if (this.ᜄ == null)
						{
							num = 4;
							continue;
						}
						this.SerializeStartRecords(records);
						sprᢔ sprᢔ = (sprᢔ)spr\u175E.ᜀ(TBIFFRecord.Header);
						sprᢔ.ᜀ(this.CreateHeaderFooterString(this.m_arrHeaders));
						records.ᜀ(sprᢔ);
						sprᢔ sprᢔ2 = (sprᢔ)spr\u175E.ᜀ(TBIFFRecord.Footer);
						sprᢔ2.ᜀ(this.CreateHeaderFooterString(this.m_arrFooters));
						records.ᜀ(sprᢔ2);
						spr\u1B3F spr_u1B3F = (spr\u1B3F)spr\u175E.ᜀ(TBIFFRecord.HCenter);
						num = 2;
						continue;
					}
					}
					if (records == null)
					{
						num = 1;
					}
					else
					{
						num = 8;
					}
				}
				IL_59:
				throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⭇╉㹋⩍⍏", a_));
				IL_93:
				throw new ArgumentNullException(RecordTableEnumerator.b("⥃᥅ᭇ⽉㡋㭍⁏", a_));
				IL_233:
				IL_235:
				records.ᜀ(this.ᜄ);
				this.SerializeEndRecords(records);
				return;
			}
			}
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x003CC0FC File Offset: 0x003CB0FC
		internal virtual void SerializeStartRecords(RecordArrayList records)
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
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x003CC138 File Offset: 0x003CB138
		internal virtual void SerializeEndRecords(RecordArrayList records)
		{
			int a_ = 7;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜆ != null)
					{
						num = 5;
						continue;
					}
					return;
				case 2:
					records.ᜀ(this.ᜆ);
					num = 6;
					continue;
				case 3:
					goto IL_48;
				case 4:
					if (this.ᜆ.ᜊ() != null)
					{
						num = 2;
						continue;
					}
					return;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 6:
					return;
				}
				if (true)
				{
				}
				if (records == null)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
			IL_48:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾≀ⱂ㝄⍆㩈", a_));
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x003CC220 File Offset: 0x003CB220
		internal virtual int Parse(IList<BiffRecordRaw> data, int position)
		{
			int a_ = 10;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (position >= 0)
					{
						goto IL_8F;
					}
					goto IL_11F;
				case 1:
					goto IL_11F;
				case 2:
					goto IL_103;
				case 3:
					num = 9;
					continue;
				case 5:
					goto IL_58;
				case 6:
					position--;
					num = 7;
					continue;
				case 7:
					goto IL_67;
				case 8:
					goto IL_103;
				case 9:
				{
					if (position > data.Count - 1)
					{
						num = 1;
						continue;
					}
					int count = data.Count;
					num = 8;
					continue;
				}
				case 10:
				{
					BiffRecordRaw record;
					if (!this.ParseRecord(record))
					{
						num = 6;
						continue;
					}
					position++;
					num = 2;
					continue;
				}
				case 11:
					goto IL_11D;
				case 12:
				{
					int count;
					if (position >= count)
					{
						num = 11;
						continue;
					}
					BiffRecordRaw record = data[position];
					num = 10;
					continue;
				}
				}
				if (data == null)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
				IL_8F:
				num = 3;
				continue;
				IL_11F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8F;
				default:
					goto IL_135;
				}
				IL_103:
				num = 12;
			}
			IL_58:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("␿⍁ぃ❅", a_));
			IL_67:
			IL_11D:
			return position;
			IL_135:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("〿ⵁ㝃⽅㱇⍉⍋⁍", a_), RecordTableEnumerator.b("ᘿ⍁⡃㍅ⵇ橉⽋⽍㹏㱑㭓≕硗㡙㥛繝౟ݡᝣᕥ䡧ṩѫ཭ṯ剱䑳噵᥷ᑹ᡻幽ﲇﺋ꺍晴뢗ﺙﶛ솟財즥\udda7쒩\ud8ab躭鶯銱薳", a_));
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x003CC3B4 File Offset: 0x003CB3B4
		internal virtual bool ParseRecord(BiffRecordRaw record)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 22;
				for (;;)
				{
					TBIFFRecord typeCode;
					switch (num)
					{
					case 0:
						goto IL_1E0;
					case 1:
						goto IL_1B2;
					case 2:
						num = 27;
						continue;
					case 3:
						if (typeCode != TBIFFRecord.PrinterSettings)
						{
							num = 18;
							continue;
						}
						this.ᜃ = (sprᾂ)record;
						num = 7;
						continue;
					case 4:
						num = 0;
						continue;
					case 5:
						goto IL_22D;
					case 6:
						num = 25;
						continue;
					case 7:
						goto IL_1CF;
					case 8:
						if (typeCode != TBIFFRecord.Bitmap)
						{
							num = 32;
							continue;
						}
						this.ᜆ = (spr\u1DA6)record;
						num = 29;
						continue;
					case 9:
						goto IL_C6;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3B6;
						default:
							goto IL_454;
						}
						break;
					case 11:
						if (typeCode <= TBIFFRecord.PrintSetup)
						{
							num = 2;
							continue;
						}
						num = 8;
						continue;
					case 12:
						goto IL_37F;
					case 13:
						goto IL_10D;
					case 14:
						goto IL_157;
					case 15:
						num = 28;
						continue;
					case 16:
						if (typeCode != TBIFFRecord.PrintSetup)
						{
							num = 33;
							continue;
						}
						this.ᜄ = (spr\u1A56)record;
						num = 23;
						continue;
					case 17:
						goto IL_240;
					case 18:
						num = 26;
						continue;
					case 19:
						if (typeCode != TBIFFRecord.HeaderFooter)
						{
							num = 4;
							continue;
						}
						return true;
					case 20:
						num = 16;
						continue;
					case 21:
						if (typeCode <= TBIFFRecord.PrinterSettings)
						{
							num = 15;
							continue;
						}
						num = 11;
						continue;
					case 23:
						goto IL_E3;
					case 24:
						num = 3;
						continue;
					case 25:
						switch (typeCode)
						{
						case TBIFFRecord.LeftMargin:
						{
							spr\u24EA spr_u24EA = (spr\u24EA)record;
							this.m_dLeftMargin = spr_u24EA.ᜁ();
							num = 30;
							continue;
						}
						case TBIFFRecord.RightMargin:
						{
							spr\u24EA spr_u24EA = (spr\u24EA)record;
							this.m_dRightMargin = spr_u24EA.ᜁ();
							num = 14;
							continue;
						}
						case TBIFFRecord.TopMargin:
						{
							spr\u24EA spr_u24EA = (spr\u24EA)record;
							this.m_dTopMargin = spr_u24EA.ᜁ();
							num = 5;
							continue;
						}
						case TBIFFRecord.BottomMargin:
						{
							spr\u24EA spr_u24EA = (spr\u24EA)record;
							this.m_dBottomMargin = spr_u24EA.ᜁ();
							num = 31;
							continue;
						}
						default:
							num = 24;
							continue;
						}
						break;
					case 26:
						goto IL_265;
					case 27:
						switch (typeCode)
						{
						case TBIFFRecord.HCenter:
						{
							spr\u1B3F spr_u1B3F = (spr\u1B3F)record;
							this.m_bHCenter = (spr_u1B3F.ᜀ() != 0);
							num = 12;
							continue;
						}
						case TBIFFRecord.VCenter:
						{
							spr\u1B3F spr_u1B3F2 = (spr\u1B3F)record;
							this.m_bVCenter = (spr_u1B3F2.ᜀ() != 0);
							num = 1;
							continue;
						}
						default:
							num = 20;
							continue;
						}
						break;
					case 28:
						switch (typeCode)
						{
						case TBIFFRecord.Header:
						{
							sprᢔ sprᢔ = (sprᢔ)record;
							this.m_arrHeaders = this.ParseHeaderFooterString(sprᢔ.ᜁ());
							num = 13;
							continue;
						}
						case TBIFFRecord.Footer:
						{
							sprᢔ sprᢔ2 = (sprᢔ)record;
							this.m_arrFooters = this.ParseHeaderFooterString(sprᢔ2.ᜁ());
							num = 10;
							continue;
						}
						default:
							num = 6;
							continue;
						}
						break;
					case 29:
						goto IL_306;
					case 30:
						goto IL_40D;
					case 31:
						goto IL_2E9;
					case 32:
						num = 19;
						continue;
					case 33:
						num = 17;
						continue;
					}
					if (record == null)
					{
						num = 9;
						continue;
					}
					IL_3B6:
					typeCode = record.TypeCode;
					if (true)
					{
					}
					num = 21;
				}
				IL_C6:
				throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃", a_));
				IL_E3:
				IL_10D:
				IL_157:
				IL_1B2:
				IL_1CF:
				return true;
				IL_1E0:
				return false;
				IL_22D:
				return true;
				IL_240:
				IL_265:
				return false;
				IL_2E9:
				IL_306:
				IL_37F:
				IL_40D:
				return true;
				IL_454:
				if (false)
				{
				}
				return true;
			}
			}
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x003CC824 File Offset: 0x003CB824
		internal BiffRecordRaw ᜀ(IList A_0, ref int A_1, TBIFFRecord A_2)
		{
			BiffRecordRaw biffRecordRaw;
			for (;;)
			{
				IL_18:
				biffRecordRaw = (BiffRecordRaw)A_0[A_1];
				for (;;)
				{
					IL_26:
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return biffRecordRaw;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_26;
							default:
								if (false)
								{
								}
								biffRecordRaw = spr\u175E.ᜀ(A_2);
								num = 0;
								continue;
							}
							break;
						case 2:
							goto IL_4F;
						case 3:
							if (biffRecordRaw.TypeCode != A_2)
							{
								num = 1;
								continue;
							}
							A_1++;
							num = 2;
							continue;
						}
						goto IL_18;
					}
				}
			}
			IL_4F:
			if (true)
			{
			}
			return biffRecordRaw;
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x003CC8C8 File Offset: 0x003CB8C8
		internal BiffRecordRaw ᜁ(IList A_0, ref int A_1)
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
			return (BiffRecordRaw)A_0[A_1++];
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x003CC918 File Offset: 0x003CB918
		internal BiffRecordRaw ᜁ(IList A_0, ref int A_1, TBIFFRecord A_2)
		{
			BiffRecordRaw biffRecordRaw;
			for (;;)
			{
				biffRecordRaw = null;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_65;
					case 1:
						if (true)
						{
						}
						biffRecordRaw = null;
						num = 3;
						continue;
					case 2:
						if (biffRecordRaw.TypeCode == A_2)
						{
							num = 4;
							continue;
						}
						goto IL_65;
					case 3:
						goto IL_98;
					case 4:
						goto IL_98;
					case 5:
						if (A_1 >= A_0.Count)
						{
							num = 1;
							continue;
						}
						goto IL_2C;
					}
					break;
					IL_2C:
					num = 2;
					continue;
					IL_65:
					biffRecordRaw = (BiffRecordRaw)A_0[A_1];
					A_1++;
					num = 5;
					continue;
					IL_98:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						goto IL_AE;
					}
				}
			}
			IL_AE:
			if (false)
			{
			}
			return biffRecordRaw;
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x003CC9DC File Offset: 0x003CB9DC
		private void ᜀ(RecordArrayList A_0, TBIFFRecord A_1, double A_2, double A_3)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					spr\u24EA spr_u24EA = (spr\u24EA)spr\u175E.ᜀ(A_1);
					spr_u24EA.ᜀ(A_2);
					A_0.ᜀ(spr_u24EA);
					if (true)
					{
					}
					num = 0;
					continue;
				}
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (A_2 == A_3)
					{
						return;
					}
					break;
				}
				num = 1;
			}
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x003CCA68 File Offset: 0x003CBA68
		protected void SetChanged()
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
			this.ᜅ.SetChanged();
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06006030 RID: 24624 RVA: 0x003CCAB0 File Offset: 0x003CBAB0
		public TBIFFRecord TypeCode
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
				return TBIFFRecord.Unknown;
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x003CCAEC File Offset: 0x003CBAEC
		public int RecordCode
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
				return 0;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06006032 RID: 24626 RVA: 0x003CCB28 File Offset: 0x003CBB28
		public bool NeedDataArray
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
				return false;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06006033 RID: 24627 RVA: 0x003CCB64 File Offset: 0x003CBB64
		// (set) Token: 0x06006034 RID: 24628 RVA: 0x003CCBA4 File Offset: 0x003CBBA4
		public long StreamPos
		{
			get
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
				return -1L;
			}
			set
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
			}
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x003CCBE0 File Offset: 0x003CBBE0
		public virtual int GetStoreSize(ExcelVersion version)
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_18B:
				if (this.ᜆ == null)
				{
					return num;
				}
				num2 = 2;
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				num2 = 9;
				break;
			}
			for (;;)
			{
				int length;
				int length2;
				switch (num2)
				{
				case 0:
					num += length * 2 + 3;
					num2 = 4;
					continue;
				case 1:
					num += length2 * 2 + 3;
					num2 = 6;
					continue;
				case 2:
					num += this.ᜆ.GetStoreSize(version) + 4;
					num2 = 7;
					continue;
				case 3:
					if (length2 > 0)
					{
						num2 = 1;
						continue;
					}
					goto IL_180;
				case 4:
					goto IL_14B;
				case 5:
					goto IL_18B;
				case 6:
					goto IL_180;
				case 7:
					return num;
				case 8:
					if (length > 0)
					{
						num2 = 0;
						continue;
					}
					goto IL_14B;
				}
				num = 12 + this.ᜄ.GetStoreSize(version) + 4 + ((this.ᜃ != null) ? (this.ᜃ.GetStoreSize(version) + 4) : 0) + ((this.m_dBottomMargin != 1.0) ? 12 : 0) + ((this.m_dTopMargin != 1.0) ? 12 : 0) + ((this.m_dRightMargin != 0.75) ? 12 : 0) + ((this.m_dLeftMargin != 0.75) ? 12 : 0);
				length = this.FullHeaderString.Length;
				length2 = this.FullFooterString.Length;
				num2 = 8;
				continue;
				IL_14B:
				num += 4;
				num2 = 3;
				continue;
				IL_180:
				num2 = 5;
			}
			return num;
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x003CCDAC File Offset: 0x003CBDAC
		public int FillStream(BinaryWriter writer, DataProvider provider, IEncryptor encryptor, int streamPosition)
		{
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_74:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1A9;
					case 1:
						num2 += this.ᜀ(writer, provider, encryptor, TBIFFRecord.HCenter, this.m_bHCenter ? 1 : 0, streamPosition + num2);
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						num2 += this.ᜀ(writer, provider, encryptor, TBIFFRecord.VCenter, this.m_bVCenter ? 1 : 0, streamPosition + num2);
						num2 += this.ᜀ(writer, provider, encryptor, TBIFFRecord.LeftMargin, this.m_dLeftMargin, 0.75, streamPosition + num2);
						num2 += this.ᜀ(writer, provider, encryptor, TBIFFRecord.RightMargin, this.m_dRightMargin, 0.75, streamPosition + num2);
						num2 += this.ᜀ(writer, provider, encryptor, TBIFFRecord.TopMargin, this.m_dTopMargin, 1.0, streamPosition + num2);
						num2 += this.ᜀ(writer, provider, encryptor, TBIFFRecord.BottomMargin, this.m_dBottomMargin, 1.0, streamPosition + num2);
						num = 3;
						continue;
					case 3:
						if (this.ᜃ != null)
						{
							num = 4;
							continue;
						}
						goto IL_1AB;
					case 4:
						num2 += this.ᜃ.FillStream(writer, provider, encryptor, streamPosition + num2);
						num = 0;
						continue;
					}
					goto IL_38;
				}
				IL_1A9:
				IL_1AB:
				num2 += this.ᜄ.FillStream(writer, provider, encryptor, streamPosition + num2);
				num2 += this.FillStreamEnd(writer, provider, encryptor, streamPosition + num2);
				return num2;
			}
			default:
				if (false)
				{
				}
				break;
			}
			IL_38:
			num2 = this.FillStreamStart(writer, provider, encryptor, streamPosition);
			num2 += this.ᜀ(writer, provider, encryptor, TBIFFRecord.Header, this.FullHeaderString, streamPosition + num2);
			num2 += this.ᜀ(writer, provider, encryptor, TBIFFRecord.Footer, this.FullFooterString, streamPosition + num2);
			goto IL_74;
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x003CCF8C File Offset: 0x003CBF8C
		private int ᜀ(BinaryWriter A_0, DataProvider A_1, IEncryptor A_2, TBIFFRecord A_3, string A_4, int A_5)
		{
			int num4;
			spr\u24E5 spr_u24E;
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
				for (;;)
				{
					int num = 0;
					A_1.WriteUInt16(num, (ushort)A_3);
					num += 2;
					int num2 = 2;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							goto IL_E9;
						case 1:
							num3 = 0;
							goto IL_A2;
						case 2:
							if (A_4 == null)
							{
								num2 = 6;
								continue;
							}
							num2 = 3;
							continue;
						case 3:
							num3 = A_4.Length * 2;
							goto IL_A2;
						case 4:
							goto IL_EB;
						case 5:
							if (A_2 != null)
							{
								num2 = 9;
								continue;
							}
							goto IL_128;
						case 6:
							num2 = 1;
							continue;
						case 7:
							num4 += 3;
							num2 = 4;
							continue;
						case 8:
							if (num4 > 0)
							{
								num2 = 7;
								continue;
							}
							goto IL_EB;
						case 9:
							A_2.Encrypt(A_1, 4, num4, (long)(A_5 + 4));
							num2 = 0;
							continue;
						}
						break;
						IL_A2:
						num4 = num3;
						num2 = 8;
						continue;
						IL_EB:
						A_1.WriteInt16(num, (short)num4);
						num += 2;
						A_1.WriteString16BitUpdateOffset(ref num, A_4);
						num4 += 4;
						spr_u24E = (spr\u24E5)A_1;
						num2 = 5;
					}
				}
				break;
			}
			IL_E9:
			IL_128:
			A_1.WriteInto(A_0, 0, num4, spr_u24E.ᜅ());
			return num4;
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x003CD0D4 File Offset: 0x003CC0D4
		[CLSCompliant(false)]
		internal int ᜀ(BinaryWriter A_0, DataProvider A_1, IEncryptor A_2, TBIFFRecord A_3, ushort A_4, int A_5)
		{
			for (;;)
			{
				for (;;)
				{
					A_1.WriteUInt16(0, (ushort)A_3);
					A_1.WriteUInt16(2, 2);
					A_1.WriteUInt16(4, A_4);
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							A_2.Encrypt(A_1, 4, 2, (long)(A_5 + 4));
							num = 2;
							continue;
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
								if (A_2 != null)
								{
									num = 0;
									continue;
								}
								goto IL_82;
							}
							break;
						case 2:
							goto IL_80;
						}
						break;
					}
				}
			}
			IL_80:
			IL_82:
			if (true)
			{
			}
			A_1.WriteInto(A_0, 0, 6, null);
			return 6;
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x003CD178 File Offset: 0x003CC178
		private int ᜀ(BinaryWriter A_0, DataProvider A_1, IEncryptor A_2, TBIFFRecord A_3, double A_4, double A_5, int A_6)
		{
			int result;
			for (;;)
			{
				result = 0;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						A_2.Encrypt(A_1, 4, 8, (long)(A_6 + 4));
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						goto IL_3D;
					case 2:
						if (A_2 == null)
						{
							goto IL_3D;
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
							num = 0;
							continue;
						}
						break;
					case 3:
						A_1.WriteUInt16(0, (ushort)A_3);
						A_1.WriteUInt16(2, 8);
						A_1.WriteDouble(4, A_4);
						num = 2;
						continue;
					case 4:
						return result;
					case 5:
						if (A_4 != A_5)
						{
							num = 3;
							continue;
						}
						return result;
					}
					break;
					IL_3D:
					A_1.WriteInto(A_0, 0, 12, null);
					result = 12;
					num = 4;
				}
			}
			return result;
		}

		// Token: 0x0600603A RID: 24634 RVA: 0x003CD258 File Offset: 0x003CC258
		internal virtual int FillStreamStart(BinaryWriter writer, DataProvider provider, IEncryptor encryptor, int streamPosition)
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
			return 0;
		}

		// Token: 0x0600603B RID: 24635 RVA: 0x003CD294 File Offset: 0x003CC294
		internal virtual int FillStreamEnd(BinaryWriter writer, DataProvider provider, IEncryptor encryptor, int streamPosition)
		{
			int result;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_3A:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						result = this.ᜆ.FillStream(writer, provider, encryptor, streamPosition);
						num = 2;
						continue;
					case 1:
						if (this.ᜆ != null)
						{
							num = 4;
							continue;
						}
						return result;
					case 2:
						return result;
					case 3:
						if (this.ᜆ.ᜊ() != null)
						{
							num = 0;
							continue;
						}
						return result;
					case 4:
						if (true)
						{
						}
						num = 3;
						continue;
					}
					goto IL_38;
				}
				return result;
			}
			default:
				if (false)
				{
				}
				break;
			}
			IL_38:
			result = 0;
			goto IL_3A;
		}

		// Token: 0x04002E26 RID: 11814
		public const double DEFAULT_TOP_MARGIN = 1.0;

		// Token: 0x04002E27 RID: 11815
		public const double DEFAULT_BOTTOM_MARGIN = 1.0;

		// Token: 0x04002E28 RID: 11816
		public const double DEFAULT_LEFT_MARGIN = 0.75;

		// Token: 0x04002E29 RID: 11817
		public const double DEFAULT_RIGHT_MARGIN = 0.75;

		// Token: 0x04002E2A RID: 11818
		private static readonly string[] ᜀ;

		// Token: 0x04002E2B RID: 11819
		private static readonly string[] ᜁ;

		// Token: 0x04002E2C RID: 11820
		private static readonly Dictionary<int, XlsPageSetupBase.PaperSizeEntry> ᜂ;

		// Token: 0x04002E2D RID: 11821
		protected bool m_bHCenter;

		// Token: 0x04002E2E RID: 11822
		protected bool m_bVCenter;

		// Token: 0x04002E2F RID: 11823
		[CLSCompliant(false)]
		internal sprᾂ ᜃ;

		// Token: 0x04002E30 RID: 11824
		[CLSCompliant(false)]
		internal spr\u1A56 ᜄ;

		// Token: 0x04002E31 RID: 11825
		[CLSCompliant(false)]
		protected double m_dBottomMargin = 1.0;

		// Token: 0x04002E32 RID: 11826
		[CLSCompliant(false)]
		protected double m_dLeftMargin = 0.75;

		// Token: 0x04002E33 RID: 11827
		[CLSCompliant(false)]
		protected double m_dRightMargin = 0.75;

		// Token: 0x04002E34 RID: 11828
		[CLSCompliant(false)]
		protected double m_dTopMargin = 1.0;

		// Token: 0x04002E35 RID: 11829
		protected string[] m_arrHeaders = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04002E36 RID: 11830
		protected string[] m_arrFooters = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04002E37 RID: 11831
		private XlsWorksheetBase ᜅ;

		// Token: 0x04002E38 RID: 11832
		internal spr\u1DA6 ᜆ;

		// Token: 0x04002E39 RID: 11833
		private bool ᜇ;

		// Token: 0x02000627 RID: 1575
		protected enum THeaderSide
		{
			// Token: 0x04002E3B RID: 11835
			Left,
			// Token: 0x04002E3C RID: 11836
			Center,
			// Token: 0x04002E3D RID: 11837
			Right
		}

		// Token: 0x02000628 RID: 1576
		public sealed class PaperSizeEntry
		{
			// Token: 0x0600603C RID: 24636 RVA: 0x003CD344 File Offset: 0x003CC344
			private PaperSizeEntry()
			{
			}

			// Token: 0x0600603D RID: 24637 RVA: 0x003CD358 File Offset: 0x003CC358
			public PaperSizeEntry(double width, double height, MeasureUnits units)
			{
				this.Width = spr\u17FF.ᜀ(width, units, MeasureUnits.Point);
				this.Height = spr\u17FF.ᜀ(height, units, MeasureUnits.Point);
			}

			// Token: 0x04002E3E RID: 11838
			private int \u25D9\u00AB\u00AC\u008F;

			// Token: 0x04002E3F RID: 11839
			public double Width;

			// Token: 0x04002E40 RID: 11840
			public double Height;
		}
	}
}
