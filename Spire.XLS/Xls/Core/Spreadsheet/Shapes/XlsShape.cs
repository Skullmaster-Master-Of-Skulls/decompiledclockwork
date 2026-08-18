using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x02000219 RID: 537
	public class XlsShape : XlsObject, IShape, IDisposable, ICloneParent, INamedObject
	{
		// Token: 0x06001F5D RID: 8029 RVA: 0x00118C18 File Offset: 0x00117C18
		[CLSCompliant(false)]
		internal static void ᜀ(sprᡍ A_0, MsoOptions A_1, byte[] A_2)
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
			XlsShape.ᜀ(A_0, A_1, A_2, null, false);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00118C60 File Offset: 0x00117C60
		[CLSCompliant(false)]
		internal static void ᜀ(sprᡍ A_0, MsoOptions A_1, byte[] A_2, byte[] A_3, bool A_4)
		{
			int a_ = 4;
			while (A_2 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽", a_));
				}
			}
			XlsShape.ᜀ(A_0, A_1, BitConverter.ToInt32(A_2, 0), A_3, A_4);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00118CD0 File Offset: 0x00117CD0
		[CLSCompliant(false)]
		internal static void ᜀ(sprᡍ A_0, MsoOptions A_1, int A_2)
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
			XlsShape.ᜀ(A_0, A_1, A_2, null, false);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00118D18 File Offset: 0x00117D18
		[CLSCompliant(false)]
		internal static void ᜀ(sprᡍ A_0, MsoOptions A_1, int A_2, byte[] A_3, bool A_4)
		{
			int a_ = 11;
			int num = 3;
			spr\u23E7.ᜀ ᜀ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7F;
				case 1:
					goto IL_5E;
				case 2:
					if (A_3 != null)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					goto IL_DB;
				case 4:
					ᜀ.ᜁ(true);
					ᜀ.ᜀ(A_3);
					ᜀ.ᜀ((uint)A_3.Length);
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C7;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
				else
				{
					ᜀ = new spr\u23E7.ᜀ();
					ᜀ.ᜀ(A_1);
					ᜀ.ᜀ((uint)A_2);
					ᜀ.ᜀ(A_4);
					ᜀ.ᜁ(false);
					num = 2;
				}
			}
			IL_5E:
			goto IL_C7;
			IL_7F:
			goto IL_DB;
			IL_C7:
			throw new ArgumentNullException(RecordTableEnumerator.b("⹀㍂ㅄ⹆♈╊㹌", a_));
			IL_DB:
			A_0.ᜀ(ᜀ);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00118E08 File Offset: 0x00117E08
		internal XlsShape(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.SetParents();
			this.AttachEvents();
			this.ᜨ = true;
			if (this.m_shapes.Worksheet == null)
			{
				this.\u171B = false;
			}
			this.ᜐ = (sprᮋ)spr\u231F.ᜀ(MsoRecords.msofbtClientAnchor);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00118E94 File Offset: 0x00117E94
		internal XlsShape(spr\u1DF5 A_0, object A_1, XlsShape A_2) : this(A_0, A_1)
		{
			this.m_bIsDisposed = A_2.m_bIsDisposed;
			this.m_bSupportOptions = A_2.m_bSupportOptions;
			this.\u1713 = A_2.\u1713;
			this.ᜎ = A_2.ᜎ;
			this.ᜋ = A_2.ᜋ;
			this.ᜊ = A_2.ᜊ;
			spr\u1D3B spr_u1D3B = A_2.ᜌ;
			if (spr_u1D3B != null)
			{
				this.ᜌ = (spr\u1D3B)spr\u1CD3.ᜀ(spr_u1D3B, null);
			}
			else if (this.ᜏ != null)
			{
				this.ᜏ = (sprἼ)spr\u1CD3.ᜀ(A_2.ᜏ, null);
			}
			this.ᜀ(A_2.ᜐ);
			this.ᜑ = (spr\u2003)spr\u1CD3.ᜀ(A_2.ᜑ);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00118F58 File Offset: 0x00117F58
		internal XlsShape(spr\u1DF5 A_0, object A_1, spr\u1D3B[] A_2, int A_3) : this(A_0, A_1)
		{
			this.ᜌ = (sprὙ)A_2[A_3];
			this.ᜊ();
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00118F84 File Offset: 0x00117F84
		internal XlsShape(spr\u1DF5 A_0, object A_1, sprὙ A_2) : this(A_0, A_1, A_2, ExcelParseOptions.Default)
		{
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00118F9C File Offset: 0x00117F9C
		internal XlsShape(spr\u1DF5 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3) : this(A_0, A_1)
		{
			this.ᜌ = A_2;
			this.ᜀ(A_3);
			this.m_bSupportOptions = true;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x00118FC8 File Offset: 0x00117FC8
		internal XlsShape(spr\u1DF5 A_0, object A_1, spr\u1D3B A_2) : this(A_0, A_1, A_2, ExcelParseOptions.Default)
		{
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00118FE0 File Offset: 0x00117FE0
		internal XlsShape(spr\u1DF5 A_0, object A_1, spr\u1D3B A_2, ExcelParseOptions A_3) : this(A_0, A_1)
		{
			this.ᜌ = A_2;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00118FFC File Offset: 0x00117FFC
		protected virtual void CreateDefaultFillLineFormats()
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

		// Token: 0x06001F69 RID: 8041 RVA: 0x00119038 File Offset: 0x00118038
		private void ᜊ()
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
			this.ᜀ(ExcelParseOptions.Default);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0011907C File Offset: 0x0011807C
		private void ᜀ(ExcelParseOptions A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜎ = ExcelShapeType.Unknown;
					sprὙ sprὙ = this.ᜌ as sprὙ;
					this.CreateDefaultFillLineFormats();
					int num = 3;
					for (;;)
					{
						int num2;
						spr\u1D3B spr_u1D3B;
						switch (num)
						{
						case 0:
							goto IL_13C;
						case 1:
							num = 19;
							continue;
						case 2:
							if (true)
							{
							}
							goto IL_222;
						case 3:
						{
							if (sprὙ == null)
							{
								num = 15;
								continue;
							}
							List<spr\u1D3B> list = sprὙ.ᜀ();
							num2 = 0;
							int count = list.Count;
							num = 4;
							continue;
						}
						case 4:
							goto IL_222;
						case 5:
						{
							if (this.m_bUpdateLineFill)
							{
								num = 23;
								continue;
							}
							spr\u23E7 spr_u23E = spr_u1D3B as spr\u23E7;
							this.\u1712 = spr_u23E;
							this.ᜂ(spr_u23E);
							num = 24;
							continue;
						}
						case 6:
						{
							MsoRecords msoRecords;
							switch (msoRecords)
							{
							case MsoRecords.msofbtSpgr:
								this.ParseShapeGroup((spr\u1B5C)spr_u1D3B);
								num = 17;
								continue;
							case MsoRecords.msofbtSp:
								this.ParseShape((sprἼ)spr_u1D3B);
								num = 10;
								continue;
							case MsoRecords.msofbtOPT:
								num = 5;
								continue;
							case MsoRecords.msofbtTextbox:
							case MsoRecords.msofbtClientTextbox:
							case MsoRecords.msofbtAnchor:
								goto IL_2F6;
							case MsoRecords.msofbtChildAnchor:
								this.ParseChildAnchor((spr\u23CF)spr_u1D3B);
								num = 21;
								continue;
							case MsoRecords.msofbtClientAnchor:
								this.ParseClientAnchor((sprᮋ)spr_u1D3B);
								num = 12;
								continue;
							case MsoRecords.msofbtClientData:
								this.ParseClientData((spr᪙)spr_u1D3B, A_0);
								num = 22;
								continue;
							default:
								num = 1;
								continue;
							}
							break;
						}
						case 7:
							return;
						case 8:
						{
							MsoRecords msoRecords;
							if (msoRecords != MsoRecords.msofbtSpgrContainer)
							{
								num = 20;
								continue;
							}
							this.ParseShapeGroupContainer((spr\u21EB)spr_u1D3B);
							num = 13;
							continue;
						}
						case 9:
							goto IL_13C;
						case 10:
							goto IL_13C;
						case 11:
							if (this.ID != 0)
							{
								num = 14;
								continue;
							}
							return;
						case 12:
							goto IL_13C;
						case 13:
							goto IL_329;
						case 14:
							this.\u171D = this.ID;
							num = 7;
							continue;
						case 15:
							return;
						case 16:
							num = 11;
							continue;
						case 17:
							goto IL_13C;
						case 18:
						{
							int count;
							if (num2 >= count)
							{
								num = 16;
								continue;
							}
							List<spr\u1D3B> list;
							spr_u1D3B = list[num2];
							MsoRecords msoRecords = spr_u1D3B.\u1717();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_329;
							default:
								if (false)
								{
								}
								num = 8;
								continue;
							}
							break;
						}
						case 19:
							goto IL_2F6;
						case 20:
							num = 6;
							continue;
						case 21:
							goto IL_13C;
						case 22:
							goto IL_13C;
						case 23:
							this.ᜃ((spr\u23E7)spr_u1D3B);
							num = 9;
							continue;
						case 24:
							goto IL_13C;
						}
						break;
						IL_13C:
						num2++;
						num = 2;
						continue;
						IL_222:
						num = 18;
						continue;
						IL_2F6:
						this.ParseOtherRecords(spr_u1D3B, A_0);
						num = 0;
						continue;
						IL_329:
						goto IL_13C;
					}
				}
				return;
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x001193D8 File Offset: 0x001183D8
		internal virtual void ParseClientData(spr᪙ clientData, ExcelParseOptions options)
		{
			switch (0)
			{
			default:
			{
				spr\u25AD spr_u25AD;
				for (;;)
				{
					IL_4B:
					this.ᜑ = clientData.ᜁ();
					List<spr\u25AD> list = this.ᜑ.ᜃ();
					this.\u170D.CurrentObjectId = Math.Max(this.\u170D.CurrentObjectId, (int)(list[0] as spr\u2223).ᜈ());
					int num = 1;
					int count = list.Count;
					int num2 = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							switch (num2)
							{
							case 0:
								goto IL_109;
							case 1:
								goto IL_10B;
							case 2:
								if (num >= count)
								{
									num2 = 5;
									continue;
								}
								if (true)
								{
								}
								spr_u25AD = list[num];
								num2 = 4;
								continue;
							case 3:
								goto IL_10B;
							case 4:
								if (spr_u25AD.ᜏ() == TObjSubRecordType.ftMacro)
								{
									num2 = 0;
									continue;
								}
								num++;
								num2 = 3;
								continue;
							case 5:
								return;
							}
							goto IL_4B;
							IL_10B:
							num2 = 2;
							break;
						}
					}
				}
				IL_109:
				sprᥰ sprᥰ = (sprᥰ)spr_u25AD;
				this.\u171E = sprᥰ.ᜀ();
				return;
			}
			}
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00119510 File Offset: 0x00118510
		internal virtual void ParseOtherRecords(spr\u1D3B subRecord, ExcelParseOptions options)
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

		// Token: 0x06001F6D RID: 8045 RVA: 0x0011954C File Offset: 0x0011854C
		private void ᜃ(spr\u23E7 A_0)
		{
			int a_ = 16;
			int num = 0;
			for (;;)
			{
				int num2;
				int num3;
				spr\u23E7.ᜀ[] array;
				switch (num)
				{
				case 1:
					if (num2 >= num3)
					{
						num = 4;
						continue;
					}
					this.ParseOption(array[num2]);
					num2++;
					num = 2;
					continue;
				case 2:
					goto IL_54;
				case 3:
					goto IL_88;
				case 4:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_54;
					default:
						goto IL_C7;
					}
					break;
				case 5:
					goto IL_3C;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				this.\u1712 = A_0;
				array = A_0.ᜀ();
				num2 = 0;
				num3 = array.Length;
				num = 3;
				continue;
				IL_88:
				num = 1;
				continue;
				IL_54:
				goto IL_88;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⥅㡇㹉╋⅍㹏⅑", a_));
			IL_C7:
			if (false)
			{
			}
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00119628 File Offset: 0x00118628
		private bool ᜁ(spr\u23E7.ᜀ A_0)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1714 == null)
					{
						num = 2;
						continue;
					}
					goto IL_D7;
				case 1:
					goto IL_46;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_CF;
					}
					break;
				case 3:
					if (Array.IndexOf<MsoOptions>(XlsShape.ᜈ, A_0.ᜈ()) >= 0)
					{
						num = 6;
						continue;
					}
					goto IL_46;
				case 4:
					if (true)
					{
					}
					break;
				case 5:
					num = 3;
					continue;
				case 6:
					this.\u1714 = new XlsShapeFill((spr\u2158)base.ReservedHandle, this);
					num = 1;
					continue;
				}
				if (this.\u1714 == null)
				{
					num = 5;
					continue;
				}
				IL_46:
				num = 0;
			}
			IL_CF:
			if (false)
			{
			}
			return false;
			IL_D7:
			return this.\u1714.ᜀ(A_0);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00119718 File Offset: 0x00118718
		private bool ᜀ(spr\u23E7.ᜀ A_0)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3E;
				case 1:
					this.\u1715 = new XlsShapeLineFormat(base.AppImplementation, this);
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_C7;
					}
					break;
				case 3:
					num = 5;
					continue;
				case 5:
					if (Array.IndexOf<MsoOptions>(XlsShape.ᜉ, A_0.ᜈ()) >= 0)
					{
						num = 1;
						continue;
					}
					goto IL_3E;
				case 6:
					if (this.\u1715 == null)
					{
						num = 2;
						continue;
					}
					goto IL_CF;
				}
				if (this.\u1715 == null)
				{
					num = 3;
					continue;
				}
				IL_3E:
				if (true)
				{
				}
				num = 6;
			}
			IL_C7:
			if (false)
			{
			}
			return false;
			IL_CF:
			return this.\u1715.ᜂ(A_0);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x00119800 File Offset: 0x00118800
		[CLSCompliant(false)]
		internal virtual bool ParseOption(spr\u23E7.ᜀ option)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return true;
				case 1:
					num = 3;
					continue;
				case 2:
					num = 6;
					continue;
				case 3:
					return false;
				case 5:
					return false;
				case 6:
				{
					if (true)
					{
					}
					MsoOptions msoOptions;
					switch (msoOptions)
					{
					case MsoOptions.ShapeName:
						goto IL_4E;
					case MsoOptions.AlternativeText:
						this.ᜋ = this.ᜃ(option);
						num = 5;
						continue;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 7:
					if (!this.ᜀ(option))
					{
						MsoOptions msoOptions = option.ᜈ();
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_12D;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 8:
				{
					MsoOptions msoOptions;
					if (msoOptions != MsoOptions.SizeTextToFitShape)
					{
						goto IL_12D;
					}
					goto IL_D5;
				}
				case 9:
					return true;
				}
				if (this.ᜁ(option))
				{
					num = 9;
					continue;
				}
				num = 7;
				continue;
				IL_12D:
				num = 2;
			}
			return true;
			IL_4E:
			this.ᜊ = this.ᜃ(option);
			return true;
			IL_D5:
			this.\u1716 = (option.ᜆ() == 655370U);
			return true;
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x00119950 File Offset: 0x00118950
		internal virtual void ParseShape(sprἼ shapeRecord)
		{
			int a_ = 5;
			while (shapeRecord == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼帾ㅀ♂ᝄ≆⩈⑊㽌⭎", a_));
				}
			}
			this.ᜏ = (sprἼ)shapeRecord.Clone();
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x001199C0 File Offset: 0x001189C0
		internal virtual void ParseClientAnchor(sprᮋ clientAnchor)
		{
			int a_ = 19;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.ᜐ.ᜈ())
					{
						num = 4;
						continue;
					}
					return;
				case 1:
					goto IL_5E;
				case 3:
					goto IL_82;
				case 4:
					this.EvaluateTopLeftPosition();
					this.UpdateHeight();
					this.UpdateWidth();
					num = 3;
					continue;
				}
				if (clientAnchor == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AD;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
				}
				else
				{
					this.ᜐ = clientAnchor;
					num = 0;
				}
			}
			IL_5E:
			if (true)
			{
			}
			goto IL_AD;
			IL_82:
			return;
			IL_AD:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩈❊⑌⩎㽐❒ᑔ㥖㩘㍚㉜ⵞ", a_));
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x00119A90 File Offset: 0x00118A90
		protected virtual void SetParents()
		{
			int a_ = 4;
			for (;;)
			{
				if (true)
				{
				}
				this.m_shapes = (base.FindParent(typeof(ShapeCollectionBase), true) as ShapeCollectionBase);
				if (this.m_shapes != null)
				{
					goto IL_71;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_55;
				}
			}
			IL_55:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("礹崻倽朿㙁摃⁅ⅇ⑉⡋湍⁏㍑♓㍕㙗⹙籛㵝ཟ๡ࡣͥ୧ṩիŭṯ山", a_));
			IL_71:
			this.\u170D = this.m_shapes.Workbook;
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x00119B20 File Offset: 0x00118B20
		protected void AttachEvents()
		{
			int a_ = 9;
			for (;;)
			{
				XlsWorksheet xlsWorksheet;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5C:
					if (xlsWorksheet == null)
					{
						return;
					}
					num = 1;
					break;
				default:
					if (false)
					{
					}
					xlsWorksheet = (this.m_shapes.WorksheetBase as XlsWorksheet);
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
						(this.\u170D.Styles[RecordTableEnumerator.b("焾⹀ㅂ⡄♆╈", a_)].Font as FontWrapper).AfterChangeEvent += this.ᜀ;
						xlsWorksheet.ColumnWidthChanged += this.ᜁ;
						xlsWorksheet.RowHeightChanged += this.ᜀ;
						if (true)
						{
						}
						num = 0;
						continue;
					case 2:
						goto IL_5C;
					}
					break;
				}
			}
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00119C08 File Offset: 0x00118C08
		protected void DetachEvents()
		{
			int a_ = 11;
			for (;;)
			{
				XlsWorksheet xlsWorksheet = this.m_shapes.WorksheetBase as XlsWorksheet;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5A;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7E;
						default:
							if (false)
							{
							}
							(this.\u170D.Styles[RecordTableEnumerator.b("ཀⱂ㝄⩆⡈❊", a_)].Font as FontWrapper).AfterChangeEvent -= this.ᜀ;
							num = 0;
							continue;
						}
						break;
					case 2:
						if (this.\u170D.Styles.Contains(RecordTableEnumerator.b("ཀⱂ㝄⩆⡈❊", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_5A;
					case 3:
						if (xlsWorksheet != null)
						{
							num = 5;
							continue;
						}
						return;
					case 4:
						return;
					case 5:
						if (true)
						{
						}
						num = 2;
						continue;
					}
					break;
					IL_7E:
					num = 4;
					continue;
					IL_5A:
					xlsWorksheet.ColumnWidthChanged -= this.ᜁ;
					xlsWorksheet.RowHeightChanged -= this.ᜀ;
					goto IL_7E;
				}
			}
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00119D4C File Offset: 0x00118D4C
		internal virtual void ParseShapeGroup(spr\u1B5C shapeGroup)
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

		// Token: 0x06001F77 RID: 8055 RVA: 0x00119D88 File Offset: 0x00118D88
		internal virtual void ParseShapeGroupContainer(spr\u21EB subRecord)
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

		// Token: 0x06001F78 RID: 8056 RVA: 0x00119DC4 File Offset: 0x00118DC4
		internal virtual void ParseChildAnchor(spr\u23CF childAnchor)
		{
			int a_ = 19;
			while (childAnchor == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("⩈⍊⑌⍎㕐ቒ㭔㑖ㅘ㑚⽜", a_));
				}
			}
			this.ᜤ = childAnchor;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00119E28 File Offset: 0x00118E28
		internal Color ᜂ(spr\u23E7.ᜀ A_0)
		{
			byte[] bytes;
			for (;;)
			{
				bytes = BitConverter.GetBytes(A_0.ᜆ());
				if (bytes[3] != 8)
				{
					goto IL_53;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_32;
				}
			}
			IL_32:
			if (false)
			{
			}
			if (true)
			{
			}
			ExcelColors color = (ExcelColors)bytes[0];
			return this.\u170D.GetPaletteColor(color);
			IL_53:
			return Color.FromArgb(0, (int)bytes[0], (int)bytes[1], (int)bytes[2]);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x00119E98 File Offset: 0x00118E98
		private byte ᜀ(spr\u23E7.ᜀ A_0, int A_1)
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
			byte[] bytes = BitConverter.GetBytes(A_0.ᜆ());
			return bytes[A_1];
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00119EE4 File Offset: 0x00118EE4
		internal string ᜃ(spr\u23E7.ᜀ A_0)
		{
			int a_ = 7;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
				{
					if (true)
					{
					}
					string text = null;
					num = 4;
					continue;
				}
				case 2:
				{
					byte[] array;
					if (array == null)
					{
						num = 1;
						continue;
					}
					string text = Encoding.Unicode.GetString(array, 0, array.Length);
					text = text.Substring(0, text.Length - 1);
					num = 5;
					continue;
				}
				case 3:
					goto IL_3C;
				case 4:
				{
					string text;
					return text;
				}
				case 5:
				{
					string text;
					return text;
				}
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
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
						byte[] array = A_0.ᜄ();
						string text = null;
						num = 2;
						break;
					}
					}
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("刼伾㕀⩂⩄⥆", a_));
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00119FCC File Offset: 0x00118FCC
		private void ᜂ(spr\u23E7 A_0)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					int num2;
					int count;
					IList<spr\u23E7.ᜀ> list;
					switch (num)
					{
					case 1:
						goto IL_C6;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							goto IL_C6;
						}
						break;
					case 3:
						goto IL_E5;
					case 4:
						goto IL_4D;
					case 5:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						spr\u23E7.ᜀ option = list[num2];
						this.ExtractNecessaryOption(option);
						num2++;
						num = 2;
						continue;
					}
					}
					IL_41:
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					list = A_0.ᜁ();
					num2 = 0;
					count = list.Count;
					num = 1;
					continue;
					goto IL_41;
					IL_C6:
					num = 5;
				}
				IL_4D:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⱂ㕄㍆⁈⑊⍌㱎", a_));
				IL_E5:
				if (true)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0011A0C8 File Offset: 0x001190C8
		[CLSCompliant(false)]
		internal virtual bool ExtractNecessaryOption(spr\u23E7.ᜀ option)
		{
			int a_ = 1;
			int num = 0;
			for (;;)
			{
				MsoOptions msoOptions;
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7A;
					default:
						goto IL_6F;
					}
					break;
				case 2:
					if (true)
					{
					}
					switch (msoOptions)
					{
					case MsoOptions.ShapeName:
						goto IL_42;
					case MsoOptions.AlternativeText:
						goto IL_A8;
					default:
						num = 4;
						continue;
					}
					break;
				case 3:
					if (msoOptions != MsoOptions.SizeTextToFitShape)
					{
						num = 6;
						continue;
					}
					goto IL_CB;
				case 4:
					num = 1;
					continue;
				case 5:
					goto IL_40;
				case 6:
					num = 2;
					continue;
				}
				if (option == null)
				{
					num = 5;
					continue;
				}
				IL_7A:
				msoOptions = option.ᜈ();
				num = 3;
			}
			IL_40:
			throw new ArgumentNullException(RecordTableEnumerator.b("堶䤸伺吼倾⽀", a_));
			IL_42:
			this.ᜊ = this.ᜃ(option);
			return true;
			IL_6F:
			if (false)
			{
			}
			return false;
			IL_A8:
			this.ᜋ = this.ᜃ(option);
			return true;
			IL_CB:
			this.\u1716 = (option.ᜆ() == 655370U);
			return true;
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x0011A1F0 File Offset: 0x001191F0
		// (set) Token: 0x06001F7F RID: 8063 RVA: 0x0011A238 File Offset: 0x00119238
		public int Height
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
				return this.\u1713.Height;
			}
			set
			{
				int a_ = 19;
				int num = 4;
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
						if (this.\u1713.Height != value)
						{
							num = 3;
							continue;
						}
						return;
					case 2:
						goto IL_39;
					case 3:
						this.\u1713.Height = value;
						this.UpdateBottomRow();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7D;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (value < 0)
					{
						num = 2;
						continue;
					}
					IL_7D:
					num = 1;
				}
				IL_39:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ň⹊⑌⡎㥐❒", a_));
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x0011A304 File Offset: 0x00119304
		public int ID
		{
			get
			{
				sprἼ sprἼ;
				for (;;)
				{
					sprὙ sprὙ = this.ᜌ as sprὙ;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (sprἼ != null)
							{
								num = 2;
								continue;
							}
							return 0;
						case 1:
							if (sprὙ != null)
							{
								num = 3;
								continue;
							}
							return 0;
						case 2:
							goto IL_96;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								sprἼ = (sprὙ.ᜀ()[0] as sprἼ);
								break;
							}
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_96:
				return sprἼ.ᜄ();
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0011A3AC File Offset: 0x001193AC
		public IFormat3D ThreeD
		{
			get
			{
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						goto IL_5A;
					case 2:
						this.ᜡ = new Format3D(base.AppImplementation, this);
						num = 1;
						continue;
					}
					if (this.ᜡ != null)
					{
						break;
					}
					num = 2;
				}
				IL_5A:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5A;
				default:
					if (false)
					{
					}
					return this.ᜡ;
				}
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0011A438 File Offset: 0x00119438
		public IShadow Shadow
		{
			get
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5A;
					case 1:
						this.ᜠ = new ChartShadow(base.AppImplementation, this);
						num = 0;
						continue;
					}
					if (this.ᜠ != null)
					{
						break;
					}
					num = 1;
				}
				IL_5A:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5A;
				default:
					if (false)
					{
					}
					return this.ᜠ;
				}
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x0011A4C4 File Offset: 0x001194C4
		// (set) Token: 0x06001F84 RID: 8068 RVA: 0x0011A50C File Offset: 0x0011950C
		public int Left
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
				return this.\u1713.X;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_66;
					case 1:
						this.\u1713.X = value;
						this.ᜇ();
						this.UpdateRightColumn();
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (this.\u1713.X == value)
					{
						break;
					}
					num = 1;
				}
				IL_66:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_66;
				default:
					if (false)
					{
					}
					return;
				}
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x0011A5A0 File Offset: 0x001195A0
		// (set) Token: 0x06001F86 RID: 8070 RVA: 0x0011A5E4 File Offset: 0x001195E4
		internal bool EnableAlternateContent
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
				return this.ᜢ;
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
				this.ᜢ = value;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x0011A628 File Offset: 0x00119628
		// (set) Token: 0x06001F88 RID: 8072 RVA: 0x0011A66C File Offset: 0x0011966C
		public virtual string Name
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x0011A6B0 File Offset: 0x001196B0
		// (set) Token: 0x06001F8A RID: 8074 RVA: 0x0011A6F8 File Offset: 0x001196F8
		public int Top
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
				return this.\u1713.Y;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1713.Y = value;
						this.ᜆ();
						this.UpdateBottomRow();
						num = 1;
						continue;
					case 1:
						goto IL_66;
					}
					if (true)
					{
					}
					if (this.\u1713.Y == value)
					{
						break;
					}
					num = 0;
				}
				IL_66:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_66;
				default:
					if (false)
					{
					}
					return;
				}
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x0011A78C File Offset: 0x0011978C
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x0011A7D4 File Offset: 0x001197D4
		public int Width
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
				return this.\u1713.Width;
			}
			set
			{
				int a_ = 18;
				int num = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_41;
					case 1:
						if (this.\u1713.Width != value)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						this.\u1713.Width = value;
						this.UpdateRightColumn();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_85;
						}
						if (false)
						{
						}
						num = 4;
						continue;
					case 4:
						return;
					}
					if (value < 0)
					{
						num = 0;
						continue;
					}
					IL_85:
					num = 1;
				}
				IL_41:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("὇⍉⡋㩍㡏", a_));
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x0011A8A0 File Offset: 0x001198A0
		// (set) Token: 0x06001F8E RID: 8078 RVA: 0x0011A8E4 File Offset: 0x001198E4
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
				return this.ᜎ;
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
				this.ᜎ = value;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x0011A928 File Offset: 0x00119928
		// (set) Token: 0x06001F90 RID: 8080 RVA: 0x0011A96C File Offset: 0x0011996C
		public bool Visible
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
				return this.\u171F;
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
				this.\u171F = value;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x0011A9B0 File Offset: 0x001199B0
		// (set) Token: 0x06001F92 RID: 8082 RVA: 0x0011A9F4 File Offset: 0x001199F4
		public string AlternativeText
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
				return this.ᜋ;
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
				this.ᜋ = value;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x0011AA38 File Offset: 0x00119A38
		// (set) Token: 0x06001F94 RID: 8084 RVA: 0x0011AA80 File Offset: 0x00119A80
		protected internal bool IsMoveWithCell
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
				return this.ClientAnchor.ᜊ();
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
				this.ClientAnchor.ᜀ(value);
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x0011AAC8 File Offset: 0x00119AC8
		// (set) Token: 0x06001F96 RID: 8086 RVA: 0x0011AB10 File Offset: 0x00119B10
		protected internal bool IsSizeWithCell
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
				return this.ClientAnchor.ᜌ();
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
				this.ClientAnchor.ᜃ(value);
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x0011AB58 File Offset: 0x00119B58
		public IShapeFill Fill
		{
			get
			{
				int a_ = 0;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4C;
					case 1:
						goto IL_E8;
					case 2:
						if (this.\u1714 != null)
						{
							goto IL_10D;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_85;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 3:
						if (!this.m_bUpdateLineFill)
						{
							num = 6;
							continue;
						}
						num = 2;
						continue;
					case 5:
						goto IL_85;
					case 6:
						this.ᜀ(this.\u1712);
						num = 7;
						continue;
					case 7:
						goto IL_A6;
					}
					if (!this.m_bSupportOptions)
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
					IL_85:
					if (true)
					{
					}
					this.\u1714 = new XlsShapeFill((spr\u2158)base.ReservedHandle, this);
					num = 1;
				}
				IL_4C:
				throw new NotSupportedException(RecordTableEnumerator.b("戵倷匹伻ḽ㌿⩁╃㙅ⵇ橉⡋⅍㕏⅑㩓煕ⱗ穙⽛⭝ၟቡୣᑥᱧ䩩੫ݭᱯṱ味ٵ੷ᕹ౻᭽ﮇꒉ", a_));
				IL_A6:
				IL_E8:
				IL_10D:
				return this.\u1714;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0011AC78 File Offset: 0x00119C78
		public IShapeLineFormat Line
		{
			get
			{
				int a_ = 15;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_54;
					case 2:
						this.ᜀ(this.\u1712);
						num = 6;
						continue;
					case 3:
						if (this.\u1715 != null)
						{
							goto IL_10D;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8D;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 4:
						goto IL_8D;
					case 5:
						goto IL_E8;
					case 6:
						goto IL_A6;
					case 7:
						if (!this.m_bUpdateLineFill)
						{
							num = 2;
							continue;
						}
						num = 3;
						continue;
					}
					if (true)
					{
					}
					if (!this.m_bSupportOptions)
					{
						num = 0;
						continue;
					}
					num = 7;
					continue;
					IL_8D:
					this.\u1715 = new XlsShapeLineFormat((spr\u2158)base.ReservedHandle, this);
					num = 5;
				}
				IL_54:
				throw new NotSupportedException(RecordTableEnumerator.b("ᅄ⽆⁈㡊浌㱎㥐㉒╔㉖祘㽚㉜㩞በൢ䉤፦䥨ᡪᡬὮŰᱲݴͶ奸᝺ᑼᅾꎂﮊﶎ朗래", a_));
				IL_A6:
				IL_E8:
				IL_10D:
				return this.\u1715;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x0011AD98 File Offset: 0x00119D98
		// (set) Token: 0x06001F9A RID: 8090 RVA: 0x0011ADDC File Offset: 0x00119DDC
		public bool AutoSize
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
				return this.\u1716;
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
				this.\u1716 = value;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x0011AE20 File Offset: 0x00119E20
		// (set) Token: 0x06001F9C RID: 8092 RVA: 0x0011AE64 File Offset: 0x00119E64
		public Stream XmlDataStream
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
				return this.\u1717;
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
				this.\u1717 = value;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x0011AEA8 File Offset: 0x00119EA8
		// (set) Token: 0x06001F9E RID: 8094 RVA: 0x0011AEEC File Offset: 0x00119EEC
		public Stream XmlTypeStream
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
				return this.\u1718;
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
				this.\u1718 = value;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x0011AF30 File Offset: 0x00119F30
		// (set) Token: 0x06001FA0 RID: 8096 RVA: 0x0011AF74 File Offset: 0x00119F74
		public bool VmlShape
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
				return this.\u171C;
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
				this.\u171C = value;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x0011AFB8 File Offset: 0x00119FB8
		// (set) Token: 0x06001FA2 RID: 8098 RVA: 0x0011B018 File Offset: 0x0011A018
		public string OnAction
		{
			get
			{
				if (this.\u171E == null)
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
						return null;
					}
				}
				return this.\u170D.FormulaUtil.ᜁ(this.\u171E);
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
				this.\u171E = ((value != null) ? this.\u170D.FormulaUtil.ᜃ(value) : null);
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0011B074 File Offset: 0x0011A074
		// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x0011B0B8 File Offset: 0x0011A0B8
		internal string ImageRelationId
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
				return this.\u171A;
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
				this.\u171A = value;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0011B0FC File Offset: 0x0011A0FC
		// (set) Token: 0x06001FA6 RID: 8102 RVA: 0x0011B140 File Offset: 0x0011A140
		internal sprᦨ ImageRelation
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
				return this.\u1719;
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
				this.\u1719 = value;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x0011B184 File Offset: 0x0011A184
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x0011B1C8 File Offset: 0x0011A1C8
		public int Rotation
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
				return this.ᜧ;
			}
			set
			{
				int a_ = 18;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_87;
						}
						break;
					case 1:
						if (value < -3600)
						{
							num = 0;
							continue;
						}
						goto IL_97;
					case 3:
						num = 1;
						continue;
					}
					if (value <= 3600)
					{
						goto IL_97;
					}
					num = 3;
				}
				IL_87:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentException(RecordTableEnumerator.b("᱇≉⥋湍≏㵑⁓㝕ⱗ㍙㍛そ䁟ᑡգ੥ᵧཀྵ䱫ᵭᡯᵱų᩵ᱷ婹ṻ᭽ꁿﾇ낏뾑ꞓꂕꢗꪙ벛ﾝ캟욡蒣閥麧骩鲫", a_));
				IL_97:
				this.ᜧ = value;
			}
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0011B274 File Offset: 0x0011A274
		public void Remove()
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
			this.OnDelete();
			this.m_shapes.Remove(this);
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0011B2C4 File Offset: 0x0011A2C4
		public void Scale(int scaleWidth, int scaleHeight)
		{
			int a_ = 4;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_53:
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
					goto IL_8D;
				case 2:
					goto IL_5B;
				case 3:
					if (scaleHeight < 0)
					{
						num = 1;
						continue;
					}
					goto IL_A3;
				}
				if (scaleWidth < 0)
				{
					goto IL_53;
				}
				num = 3;
			}
			IL_5B:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤹弻弽ⰿ❁ፃ⽅ⱇ㹉⑋", a_));
			IL_8D:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤹弻弽ⰿ❁ృ⍅ⅇⵉ⑋㩍", a_));
			IL_A3:
			this.Width = (int)((double)(this.Width * scaleWidth) / 100.0);
			this.Height = (int)((double)(this.Height * scaleHeight) / 100.0);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0011B3A8 File Offset: 0x0011A3A8
		protected override void OnDispose()
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
			base.OnDispose();
			this.DetachEvents();
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0011B3F0 File Offset: 0x0011A3F0
		internal void ᜁ(spr\u21EB A_0)
		{
			if (true)
			{
			}
			if (this.ChildShapes.Count > 0)
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
					this.SerializeShape(A_0, true);
					return;
				}
			}
			this.SerializeShape(A_0);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0011B44C File Offset: 0x0011A44C
		internal void ᜀ(spr\u21EB A_0, bool A_1)
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
			this.SerializeShape(A_0, A_1);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0011B490 File Offset: 0x0011A490
		internal virtual void SerializeShape(spr\u21EB spgrContainer)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					spgrContainer.ᜀ(this.ᜌ);
					num = 1;
					continue;
				case 1:
					goto IL_63;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_63;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (spgrContainer == null)
				{
					break;
				}
				num = 0;
			}
			IL_63:
			if (true)
			{
			}
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0011B50C File Offset: 0x0011A50C
		internal virtual void SerializeShape(spr\u21EB spgrContainer, bool isGroupShape)
		{
			switch (0)
			{
			default:
			{
				spr\u21EB a_;
				List<XlsShape>.Enumerator enumerator;
				for (;;)
				{
					List<XlsShape> list = this.ChildShapes;
					int num = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (list.Count > 0)
							{
								num = 4;
								continue;
							}
							num = 3;
							continue;
						case 2:
							goto IL_BE;
						case 3:
							if (this.ᜌ != null)
							{
								num = 5;
								continue;
							}
							return;
						case 4:
							this.Worksheet.TypedOptionButtons.ᜀ();
							a_ = (spr\u21EB)spr\u231F.ᜀ(MsoRecords.msofbtSpgrContainer);
							spgrContainer.ᜀ(a_);
							enumerator = list.GetEnumerator();
							num = 2;
							continue;
						case 5:
							spgrContainer.ᜀ(this.ᜌ);
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_BE:
				try
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_121:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						num = 4;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							XlsShape xlsShape = enumerator.Current;
							xlsShape.ᜀ(a_, true);
							num = 0;
							continue;
						}
						case 2:
							goto IL_162;
						case 3:
							num = 2;
							continue;
						}
						break;
					}
					goto IL_121;
					IL_162:;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return;
			}
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0011B69C File Offset: 0x0011A69C
		private void ᜀ(sprὙ A_0)
		{
			int a_ = 11;
			int num = 0;
			int num2;
			List<spr\u1D3B> list;
			for (;;)
			{
				int count;
				switch (num)
				{
				case 1:
					if (true)
					{
					}
					this.\u1712 = this.ᜆ(this.\u1712);
					num = 3;
					continue;
				case 2:
					return;
				case 3:
					goto IL_18D;
				case 4:
					if (this.\u1712 == null)
					{
						num = 7;
						continue;
					}
					goto IL_1AD;
				case 5:
					goto IL_91;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E7;
					default:
						if (false)
						{
						}
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						num = 15;
						continue;
					}
					break;
				case 7:
					this.\u1712 = this.CreateDefaultOptions();
					num = 12;
					continue;
				case 8:
					if (this.ᜎ != ExcelShapeType.Unknown)
					{
						num = 9;
						continue;
					}
					return;
				case 9:
					num = 13;
					continue;
				case 10:
					goto IL_116;
				case 11:
					goto IL_116;
				case 12:
					goto IL_1AD;
				case 13:
					if (this.m_bUpdateLineFill)
					{
						num = 1;
						continue;
					}
					goto IL_18D;
				case 14:
					goto IL_64;
				case 15:
					if (list[num2] is spr\u23E7)
					{
						num = 5;
						continue;
					}
					num2++;
					goto IL_E7;
				}
				if (A_0 == null)
				{
					num = 14;
					continue;
				}
				num = 4;
				continue;
				IL_E7:
				num = 10;
				continue;
				IL_116:
				num = 6;
				continue;
				IL_18D:
				list = A_0.ᜀ();
				num2 = 0;
				count = list.Count;
				num = 11;
				continue;
				IL_1AD:
				num = 8;
			}
			IL_64:
			throw new ArgumentNullException(RecordTableEnumerator.b("≀ⱂ⭄㍆⡈≊⍌⩎⍐", a_));
			IL_91:
			list[num2] = this.\u1712;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0011B878 File Offset: 0x0011A878
		internal spr\u23E7 ᜆ(spr\u23E7 A_0)
		{
			int a_ = 0;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1715 != null)
					{
						num = 4;
						continue;
					}
					goto IL_136;
				case 1:
					if (this.\u1714 != null)
					{
						num = 8;
						continue;
					}
					goto IL_113;
				case 3:
					num = 0;
					continue;
				case 4:
					this.\u1715.ᜅ(A_0);
					num = 9;
					continue;
				case 5:
					goto IL_113;
				case 6:
					if (this.m_bSupportOptions)
					{
						num = 3;
						continue;
					}
					goto IL_136;
				case 7:
					goto IL_4F;
				case 8:
					this.ᜁ(A_0);
					A_0 = (spr\u23E7)this.\u1714.\u170D(A_0);
					goto IL_88;
				case 9:
					goto IL_C8;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_88;
				}
				if (false)
				{
				}
				num = 1;
				continue;
				IL_88:
				num = 5;
				continue;
				IL_113:
				num = 6;
			}
			IL_4F:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("夵䠷丹", a_));
			IL_C8:
			IL_136:
			this.SerializeCommentShadow(A_0);
			return A_0;
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0011B9C4 File Offset: 0x0011A9C4
		private void ᜀ(spr\u23E7 A_0, int A_1)
		{
			int a_ = 18;
			if (A_0 == null)
			{
				if (true)
				{
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
					throw new ArgumentNullException(RecordTableEnumerator.b("❇㩉㡋", a_));
				}
			}
			int num = 100 - A_1;
			XlsShape.ᜀ(A_0, MsoOptions.Transparency, (int)((double)num * 655.0));
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0011BA40 File Offset: 0x0011AA40
		internal virtual spr\u23E7 SerializeOptions(spr\u1D3B parent)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.\u1712 = this.CreateDefaultOptions();
					num = 2;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					goto IL_70;
				}
				if (true)
				{
				}
				if (this.\u1712 != null)
				{
					break;
				}
				num = 0;
			}
			IL_70:
			return this.\u1712;
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0011BAC8 File Offset: 0x0011AAC8
		internal void ᜄ(spr\u23E7 A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_77;
				case 1:
					goto IL_68;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					goto IL_6F;
				}
				if (true)
				{
				}
				if (!this.\u1716)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
				IL_6F:
				num = 0;
			}
			IL_68:
			int num2 = 655370;
			goto IL_7E;
			IL_77:
			num2 = 524296;
			IL_7E:
			int a_ = num2;
			this.ᜀ(A_0, MsoOptions.SizeTextToFitShape, (uint)a_);
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0011BB64 File Offset: 0x0011AB64
		internal void ᜅ(spr\u23E7 A_0)
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
			this.ᜀ(A_0, MsoOptions.NoFillHitTest, 1048592U);
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0011BBB0 File Offset: 0x0011ABB0
		internal spr\u23E7.ᜀ ᜁ(spr\u23E7 A_0, MsoOptions A_1, uint A_2)
		{
			int a_ = 16;
			if (A_0 == null)
			{
				if (true)
				{
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
					throw new ArgumentNullException(RecordTableEnumerator.b("⥅㡇㹉╋⅍㹏⅑", a_));
				}
			}
			spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
			ᜀ.ᜀ(A_1);
			ᜀ.ᜀ(A_2);
			ᜀ.ᜀ(false);
			ᜀ.ᜁ(false);
			A_0.ᜁ(ᜀ);
			A_0.ᜂ(ᜀ);
			return ᜀ;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0011BC40 File Offset: 0x0011AC40
		internal void ᜀ(spr\u23E7 A_0, MsoOptions A_1, int A_2)
		{
			int a_ = 1;
			if (true)
			{
			}
			if (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("堶䤸伺吼倾⽀あ", a_));
				}
			}
			spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
			ᜀ.ᜀ(A_1);
			ᜀ.ᜀ(A_2);
			ᜀ.ᜀ(false);
			ᜀ.ᜁ(false);
			A_0.ᜁ(ᜀ);
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0011BCC8 File Offset: 0x0011ACC8
		internal void ᜀ(spr\u23E7 A_0, MsoOptions A_1, uint A_2)
		{
			int a_ = 5;
			if (A_0 == null)
			{
				if (true)
				{
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
					throw new ArgumentNullException(RecordTableEnumerator.b("吺䴼䬾⡀ⱂ⭄㑆", a_));
				}
			}
			spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
			ᜀ.ᜀ(A_1);
			ᜀ.ᜀ(A_2);
			ᜀ.ᜀ(false);
			ᜀ.ᜁ(false);
			A_0.ᜂ(ᜀ);
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0011BD50 File Offset: 0x0011AD50
		internal void ᜇ(spr\u23E7 A_0)
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
			this.ᜀ(A_0, MsoOptions.ShapeName, this.ᜊ);
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0011BDA0 File Offset: 0x0011ADA0
		internal void ᜀ(spr\u23E7 A_0, MsoOptions A_1, string A_2)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 0;
				string text;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_F1;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_87;
						}
						if (false)
						{
						}
						if (A_2.Length == 0)
						{
							num = 1;
							continue;
						}
						if (true)
						{
						}
						num = 7;
						continue;
					case 3:
						goto IL_87;
					case 4:
						num = 2;
						continue;
					case 5:
						goto IL_10C;
					case 6:
						goto IL_A9;
					case 7:
					{
						if (A_0 == null)
						{
							num = 6;
							continue;
						}
						int length = A_2.Length;
						text = A_2;
						num = 8;
						continue;
					}
					case 8:
					{
						int length;
						if (A_2[length - 1] != '\0')
						{
							num = 3;
							continue;
						}
						goto IL_123;
					}
					}
					if (A_2 != null)
					{
						num = 4;
						continue;
					}
					return;
					IL_87:
					text += '\0';
					num = 5;
				}
				IL_A9:
				throw new ArgumentNullException(RecordTableEnumerator.b("⼿㉁ぃ⽅❇⑉㽋", a_));
				IL_F1:
				return;
				IL_10C:
				IL_123:
				byte[] bytes = Encoding.Unicode.GetBytes(text);
				spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
				ᜀ.ᜀ(A_1);
				ᜀ.ᜀ((uint)bytes.Length);
				ᜀ.ᜀ(true);
				ᜀ.ᜁ(true);
				ᜀ.ᜀ(bytes);
				A_0.ᜂ(ᜀ);
				return;
			}
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0011BF10 File Offset: 0x0011AF10
		[CLSCompliant(false)]
		internal virtual spr\u23E7 CreateDefaultOptions()
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
			return (spr\u23E7)spr\u231F.ᜀ(MsoRecords.msofbtOPT);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0011BF5C File Offset: 0x0011AF5C
		private void ᜁ(spr\u23E7 A_0)
		{
			int a_ = 4;
			for (;;)
			{
				int num = 5;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_81;
					case 1:
						goto IL_81;
					case 2:
						goto IL_3C;
					case 3:
						goto IL_9C;
					case 4:
						if (num2 > 412)
						{
							num = 3;
							continue;
						}
						A_0.ᜀ(num2);
						num2++;
						num = 1;
						continue;
					}
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					num2 = 384;
					num = 0;
					continue;
					IL_81:
					num = 4;
				}
				IL_9C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_B4;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("唹䰻䨽", a_));
			IL_B4:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0011C02C File Offset: 0x0011B02C
		[CLSCompliant(false)]
		internal virtual void SerializeCommentShadow(spr\u23E7 option)
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

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x0011C068 File Offset: 0x0011B068
		// (set) Token: 0x06001FBF RID: 8127 RVA: 0x0011C0AC File Offset: 0x0011B0AC
		internal bool HasBorder
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
				return this.ᜨ;
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
				this.ᜨ = value;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0011C0F0 File Offset: 0x0011B0F0
		protected internal IWorkbook Workbook
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
				return this.\u170D;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0011C134 File Offset: 0x0011B134
		protected internal XlsWorkbook ParentWorkbook
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
				return this.\u170D;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x0011C178 File Offset: 0x0011B178
		protected internal ShapeCollectionBase ParentShapes
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
				return this.m_shapes;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0011C1BC File Offset: 0x0011B1BC
		protected internal XlsWorksheetBase Worksheet
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
				return this.m_shapes.WorksheetBase;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x0011C204 File Offset: 0x0011B204
		internal spr\u2003 Obj
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
				return this.ᜑ;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0011C248 File Offset: 0x0011B248
		internal sprᮋ ClientAnchor
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
				return this.ᜐ;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x0011C28C File Offset: 0x0011B28C
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x0011C2D4 File Offset: 0x0011B2D4
		public int TopRow
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
				return this.ClientAnchor.ᜉ() + 1;
			}
			set
			{
				for (;;)
				{
					for (;;)
					{
						this.ClientAnchor.ᜆ(value - 1);
						int num = 2;
						for (;;)
						{
							if (true)
							{
							}
							switch (num)
							{
							case 0:
								this.ᜁ();
								num = 1;
								continue;
							case 1:
								goto IL_5C;
							case 2:
								if (this.\u171B)
								{
									num = 0;
									continue;
								}
								return;
							}
							break;
						}
					}
					IL_5C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_72;
					}
				}
				IL_72:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x0011C35C File Offset: 0x0011B35C
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x0011C3A4 File Offset: 0x0011B3A4
		public int LeftColumn
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
				return this.ClientAnchor.ᜃ() + 1;
			}
			set
			{
				for (;;)
				{
					this.ClientAnchor.ᜇ(value - 1);
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.\u171B)
							{
								num = 2;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
							this.ᜂ();
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
								num = 1;
								continue;
							}
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x0011C42C File Offset: 0x0011B42C
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x0011C474 File Offset: 0x0011B474
		public int BottomRow
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
				return this.ClientAnchor.ᜇ() + 1;
			}
			set
			{
				for (;;)
				{
					this.ClientAnchor.ᜅ(value - 1);
					int num = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							this.UpdateHeight();
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
						case 1:
							if (this.\u171B)
							{
								num = 0;
								continue;
							}
							return;
						case 2:
							return;
						}
						break;
					}
				}
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0011C4FC File Offset: 0x0011B4FC
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x0011C544 File Offset: 0x0011B544
		public int RightColumn
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
				return this.ClientAnchor.ᜎ() + 1;
			}
			set
			{
				for (;;)
				{
					this.ClientAnchor.ᜂ(value - 1);
					int num = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (this.\u171B)
							{
								num = 2;
								continue;
							}
							return;
						case 2:
							this.UpdateWidth();
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
						}
						break;
					}
				}
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0011C5CC File Offset: 0x0011B5CC
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x0011C614 File Offset: 0x0011B614
		public int TopRowOffset
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
				return this.ClientAnchor.ᜁ();
			}
			set
			{
				int a_ = 2;
				if (true)
				{
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
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("氷唹䰻氽⼿㕁ୃ⁅⹇㥉⥋㩍", a_));
					}
					break;
				}
				this.ClientAnchor.ᜁ(value);
				this.ᜃ();
				this.ᜁ();
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x0011C68C File Offset: 0x0011B68C
		// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x0011C6D4 File Offset: 0x0011B6D4
		public int LeftColumnOffset
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
				return this.ClientAnchor.ᜀ();
			}
			set
			{
				int a_ = 4;
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
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("瘹夻堽㐿Ł⭃⩅㵇❉≋ō㙏㑑❓㍕ⱗ", a_));
					}
					break;
				}
				this.ClientAnchor.ᜀ(value);
				this.ᜅ();
				this.ᜂ();
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x0011C74C File Offset: 0x0011B74C
		// (set) Token: 0x06001FD3 RID: 8147 RVA: 0x0011C794 File Offset: 0x0011B794
		public int BottomRowOffset
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
				return this.ClientAnchor.ᜆ();
			}
			set
			{
				int a_ = 11;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (value < 0)
					{
						if (true)
						{
						}
						throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("̀ⱂㅄ㍆♈♊Ὄ⁎♐᱒㍔ㅖ⩘㹚⥜", a_));
					}
					break;
				}
				this.ClientAnchor.ᜄ(value);
				this.UpdateHeight();
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x0011C804 File Offset: 0x0011B804
		// (set) Token: 0x06001FD5 RID: 8149 RVA: 0x0011C84C File Offset: 0x0011B84C
		public int RightColumnOffset
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
				return this.ClientAnchor.ᜄ();
			}
			set
			{
				int a_ = 3;
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
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("欸刺娼圾㕀B⩄⭆㱈♊⍌N㝐㕒♔㉖ⵘ", a_));
					}
					break;
				}
				this.ClientAnchor.ᜃ(value);
				this.ᜄ();
				this.UpdateWidth();
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x0011C8C4 File Offset: 0x0011B8C4
		// (set) Token: 0x06001FD7 RID: 8151 RVA: 0x0011C928 File Offset: 0x0011B928
		[CLSCompliant(false)]
		protected internal uint OldObjId
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
					if (this.ᜑ != null)
					{
						return (uint)(this.ᜑ.ᜃ()[0] as spr\u2223).ᜈ();
					}
					break;
				}
				return 0U;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4A;
					case 2:
						goto IL_79;
					}
					if (this.ᜑ == null)
					{
						break;
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
						num = 0;
						continue;
					}
					IL_4A:
					(this.ᜑ.ᜃ()[0] as spr\u2223).ᜁ((ushort)value);
					num = 2;
				}
				IL_79:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x0011C9B8 File Offset: 0x0011B9B8
		internal spr\u1D3B Record
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
				return this.ᜌ;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0011C9FC File Offset: 0x0011B9FC
		internal sprἼ InnerSpRecord
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
				return this.ᜏ;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x0011CA40 File Offset: 0x0011BA40
		// (set) Token: 0x06001FDB RID: 8155 RVA: 0x0011CA88 File Offset: 0x0011BA88
		public bool IsShortVersion
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
				return this.ᜐ.ᜈ();
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
				this.ᜐ.ᜁ(value);
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x0011CAD0 File Offset: 0x0011BAD0
		public int ShapeCount
		{
			get
			{
				spr\u21EB spr_u21EB;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					spr_u21EB = (this.ᜌ as spr\u21EB);
					if (spr_u21EB == null)
					{
						return 1;
					}
					break;
				}
				if (true)
				{
				}
				return spr_u21EB.ᜀ().Count;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x0011CB2C File Offset: 0x0011BB2C
		// (set) Token: 0x06001FDE RID: 8158 RVA: 0x0011CB70 File Offset: 0x0011BB70
		public bool UpdatePositions
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
				return this.\u171B;
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
				this.\u171B = value;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0011CBB4 File Offset: 0x0011BBB4
		public virtual int Instance
		{
			get
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
					if (this.ᜏ == null)
					{
						if (true)
						{
						}
						return -1;
					}
					break;
				}
				return this.ᜏ.\u1714();
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x0011CC08 File Offset: 0x0011BC08
		// (set) Token: 0x06001FE1 RID: 8161 RVA: 0x0011CC50 File Offset: 0x0011BC50
		public bool HasFill
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
				return this.\u1714 != null;
			}
			internal set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						goto IL_4D;
					case 2:
						return;
					}
					if (value)
					{
						break;
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
						num = 1;
						continue;
					}
					IL_4D:
					this.\u1714 = null;
					num = 2;
				}
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0011CCC8 File Offset: 0x0011BCC8
		// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x0011CD10 File Offset: 0x0011BD10
		public bool HasLineFormat
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
				return this.\u1715 != null;
			}
			internal set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4D;
					case 2:
						return;
					}
					if (value)
					{
						break;
					}
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
						num = 0;
						continue;
					}
					IL_4D:
					this.\u1715 = null;
					num = 2;
				}
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x0011CD88 File Offset: 0x0011BD88
		// (set) Token: 0x06001FE5 RID: 8165 RVA: 0x0011CDCC File Offset: 0x0011BDCC
		public int ShapeId
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
				return this.\u171D;
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
				this.\u171D = value;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x0011CE10 File Offset: 0x0011BE10
		[CLSCompliant(false)]
		internal sprἼ ShapeRecord
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_79;
					case 2:
						goto IL_52;
					}
					if (true)
					{
					}
					if (this.ᜏ != null)
					{
						break;
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
						num = 2;
						continue;
					}
					IL_52:
					this.ᜏ = (sprἼ)spr\u231F.ᜀ(MsoRecords.msofbtSp);
					num = 0;
				}
				IL_79:
				return this.ᜏ;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0011CEA0 File Offset: 0x0011BEA0
		internal bool IsActiveX
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
					spr᮴ spr᮴ = (spr᮴)this.Obj.ᜀ(TObjSubRecordType.ftPioGrbit);
					if (spr᮴ != null)
					{
						if (true)
						{
						}
						return spr᮴.ᜀ();
					}
					break;
				}
				}
				return false;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x0011CEFC File Offset: 0x0011BEFC
		// (set) Token: 0x06001FE9 RID: 8169 RVA: 0x0011CF50 File Offset: 0x0011BF50
		internal Dictionary<string, string> StyleProperties
		{
			get
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
					if (this.ᜥ == null)
					{
						if (true)
						{
						}
						return new Dictionary<string, string>();
					}
					break;
				}
				return this.ᜥ;
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
				this.ᜥ = value;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x0011CF94 File Offset: 0x0011BF94
		// (set) Token: 0x06001FEB RID: 8171 RVA: 0x0011CFD8 File Offset: 0x0011BFD8
		internal bool IsHyperlink
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
				return this.ᜦ;
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
				this.ᜦ = value;
			}
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0011D01C File Offset: 0x0011C01C
		internal virtual void GenerateDefaultName()
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.Name = CollectionExtended<IShape>.GenerateDefaultName(this.m_shapes, RecordTableEnumerator.b("洽⠿⍁㑃⍅桇", a_));
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0011D080 File Offset: 0x0011C080
		protected virtual void OnDelete()
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

		// Token: 0x06001FEE RID: 8174 RVA: 0x0011D0BC File Offset: 0x0011C0BC
		internal void ᜀ(spr\u2003 A_0)
		{
			int a_ = 5;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("䴺尼匾㑀♂", a_));
				}
				break;
			}
			this.ᜑ = A_0;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0011D120 File Offset: 0x0011C120
		public virtual IShape Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes, bool addToCollections)
		{
			XlsShape xlsShape;
			for (;;)
			{
				if (true)
				{
				}
				xlsShape = (XlsShape)base.MemberwiseClone();
				xlsShape.SetParent(parent);
				xlsShape.SetParents();
				xlsShape.CopyFrom(this, hashNewNames, dicFontIndexes);
				xlsShape.CloneLineFill(this);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_99;
					case 1:
						if (addToCollections)
						{
							num = 2;
							continue;
						}
						goto IL_9B;
					case 2:
						xlsShape.m_shapes.AddShape(xlsShape);
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
					}
					break;
				}
			}
			IL_99:
			IL_9B:
			xlsShape.AttachEvents();
			xlsShape.OldObjId = 0U;
			return xlsShape;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0011D1D8 File Offset: 0x0011C1D8
		public object Clone(object parent)
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
			return this.Clone(parent, null, null, true);
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0011D220 File Offset: 0x0011C220
		protected internal virtual void CopyFrom(XlsShape xlsShape, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes)
		{
			for (;;)
			{
				spr\u1D3B spr_u1D3B = xlsShape.ᜌ;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_77;
					case 1:
						this.ᜌ = (spr\u1D3B)spr_u1D3B.Clone();
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
					case 2:
						if (true)
						{
						}
						if (spr_u1D3B != null)
						{
							num = 1;
							continue;
						}
						goto IL_79;
					}
					break;
				}
			}
			IL_77:
			IL_79:
			this.ᜀ(xlsShape.ClientAnchor);
			this.\u171D = xlsShape.\u171D;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0011D2C0 File Offset: 0x0011C2C0
		public bool CanInsertRowColumn(int index, int count, bool isRowMode, int maxIndex)
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜅ(index, isRowMode))
					{
						num = 11;
						continue;
					}
					return true;
				case 1:
					num = 4;
					continue;
				case 2:
					goto IL_D4;
				case 3:
					if (this.ᜂ(isRowMode) + count >= 0)
					{
						num = 5;
						continue;
					}
					return false;
				case 4:
					if (!this.IsSizeWithCell)
					{
						num = 10;
						continue;
					}
					goto IL_129;
				case 5:
					goto IL_17A;
				case 6:
					num = 3;
					continue;
				case 7:
					if (this.IsMoveWithCell)
					{
						num = 6;
						continue;
					}
					return true;
				case 8:
					if (this.IsSizeWithCell)
					{
						num = 2;
						continue;
					}
					return true;
				case 10:
					goto IL_F4;
				case 11:
					num = 8;
					continue;
				case 12:
					num = 7;
					continue;
				case 13:
					if (this.ᜆ(index, isRowMode))
					{
						num = 12;
						continue;
					}
					num = 0;
					continue;
				}
				if (!this.IsMoveWithCell)
				{
					num = 1;
					continue;
				}
				IL_129:
				num = 13;
			}
			IL_D4:
			goto IL_F8;
			IL_F4:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_F8:
				return this.ᜁ(isRowMode) + count <= maxIndex;
			default:
				if (false)
				{
				}
				return true;
			}
			return false;
			IL_17A:
			if (true)
			{
			}
			return this.ᜁ(isRowMode) + count <= maxIndex;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0011D450 File Offset: 0x0011C450
		private int ᜂ(bool A_0)
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
				if (!A_0)
				{
					return this.LeftColumn;
				}
				break;
			}
			return this.TopRow;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0011D4A0 File Offset: 0x0011C4A0
		private int ᜁ(bool A_0)
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
				if (!A_0)
				{
					return this.RightColumn;
				}
				break;
			}
			if (true)
			{
			}
			return this.BottomRow;
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0011D4F0 File Offset: 0x0011C4F0
		public void Remove(int index, int count, bool isRow)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					int num2;
					bool flag;
					int num3;
					int num4;
					switch (num)
					{
					case 0:
						if (!this.IsSizeWithCell)
						{
							num = 19;
							continue;
						}
						goto IL_16B;
					case 1:
						if (num2 > 0)
						{
							num = 24;
							continue;
						}
						goto IL_1E1;
					case 2:
						num = 0;
						continue;
					case 3:
						goto IL_1E1;
					case 4:
						return;
					case 5:
						if (flag)
						{
							num = 16;
							continue;
						}
						return;
					case 6:
						num = 17;
						continue;
					case 8:
						if (flag)
						{
							num = 14;
							continue;
						}
						goto IL_283;
					case 9:
						goto IL_E7;
					case 10:
						num = 9;
						continue;
					case 11:
						goto IL_283;
					case 12:
						this.ᜁ(-num3, isRow);
						num = 13;
						continue;
					case 13:
						goto IL_2BE;
					case 14:
						if (true)
						{
						}
						num2 = this.ᜁ(index, count, isRow);
						num = 1;
						continue;
					case 15:
						if (flag)
						{
							num = 29;
							continue;
						}
						goto IL_B0;
					case 16:
						this.ᜀ(isRow);
						num = 4;
						continue;
					case 17:
						if (!this.IsMoveWithCell)
						{
							num = 2;
							continue;
						}
						goto IL_16B;
					case 18:
						num4 = this.BottomRow;
						goto IL_2EE;
					case 19:
						this.UpdateNotSizeNotMoveShape(isRow, index, -count);
						flag = false;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E7;
						default:
							if (false)
							{
							}
							num = 22;
							continue;
						}
						break;
					case 20:
						goto IL_B0;
					case 21:
						this.ᜀ(isRow, -1);
						count--;
						flag = (count > 0);
						num = 20;
						continue;
					case 22:
						goto IL_16B;
					case 23:
						num3 = this.ᜂ(index, count, isRow);
						num = 30;
						continue;
					case 24:
						this.ᜀ(-num2, isRow);
						num = 3;
						continue;
					case 25:
						if (flag)
						{
							num = 23;
							continue;
						}
						goto IL_109;
					case 26:
						if (flag)
						{
							num = 6;
							continue;
						}
						goto IL_16B;
					case 27:
						goto IL_109;
					case 28:
					{
						bool flag2;
						if (flag2)
						{
							num = 21;
							continue;
						}
						goto IL_B0;
					}
					case 29:
					{
						bool flag2 = this.ᜀ(index, count, isRow);
						num = 28;
						continue;
					}
					case 30:
						if (num3 > 0)
						{
							num = 12;
							continue;
						}
						goto IL_2BE;
					}
					if (!isRow)
					{
						num = 10;
						continue;
					}
					num = 18;
					continue;
					IL_B0:
					num = 8;
					continue;
					IL_109:
					num = 15;
					continue;
					IL_16B:
					num = 25;
					continue;
					IL_1E1:
					count -= num2;
					flag = (count > 0);
					num = 11;
					continue;
					IL_283:
					num = 5;
					continue;
					IL_2BE:
					count -= num3;
					flag = (count > 0);
					num = 27;
					continue;
					IL_2EE:
					int num5 = num4;
					flag = (index <= num5);
					num = 26;
					continue;
					IL_E7:
					num4 = this.RightColumn;
					goto IL_2EE;
				}
				return;
			}
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0011D840 File Offset: 0x0011C840
		public void InsertRowColumn(int iIndex, int iCount, bool bRow)
		{
			int num = 8;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (this.IsMoveWithCell)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						num = 0;
						continue;
					case 2:
						if (this.ᜅ(iIndex, bRow))
						{
							num = 12;
							continue;
						}
						return;
					case 3:
						goto IL_128;
					case 4:
						if (true)
						{
						}
						if (this.IsSizeWithCell)
						{
							num = 7;
							continue;
						}
						return;
					case 5:
						if (this.ᜆ(iIndex, bRow))
						{
							num = 1;
							continue;
						}
						num = 2;
						continue;
					case 6:
						goto IL_BD;
					case 7:
						this.ᜂ(iCount, bRow);
						num = 6;
						continue;
					case 9:
						if (!this.IsMoveWithCell)
						{
							num = 11;
							continue;
						}
						goto IL_12D;
					case 10:
						num = 9;
						continue;
					case 11:
						goto IL_E0;
					case 12:
						num = 4;
						continue;
					}
					if (!this.IsSizeWithCell)
					{
						num = 10;
						break;
					}
					IL_12D:
					num = 5;
					break;
				}
			}
			IL_BD:
			return;
			IL_E0:
			this.UpdateNotSizeNotMoveShape(bRow, iIndex, iCount);
			return;
			IL_128:
			this.ᜃ(iCount, bRow);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x0011D9AC File Offset: 0x0011C9AC
		public virtual void UpdateFormula(int currentIndex, int sourceIndex, Rectangle sourceRect, int destIndex, Rectangle destRect)
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

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0011D9E8 File Offset: 0x0011C9E8
		public void SetName(string name)
		{
			int a_ = 16;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_80;
				case 2:
					goto IL_46;
				case 3:
					goto IL_90;
				}
				if (true)
				{
				}
				if (name == null)
				{
					num = 2;
					continue;
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
					num = 0;
					continue;
				}
				IL_80:
				if (name.Length != 0)
				{
					goto IL_A6;
				}
				num = 3;
			}
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("⡅⥇❉⥋", a_));
			IL_90:
			throw new ArgumentException(RecordTableEnumerator.b("ࡅ⥇❉⥋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ţ୥ᡧṩᕫ䁭", a_));
			IL_A6:
			this.ᜊ = name;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0011DAA4 File Offset: 0x0011CAA4
		internal virtual void RegisterInSubCollection()
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

		// Token: 0x06001FFA RID: 8186 RVA: 0x0011DAE0 File Offset: 0x0011CAE0
		internal virtual bool CanCopyShapesOnRangeCopy(Rectangle sourceRec, Rectangle destRec, out Rectangle newPosition)
		{
			switch (0)
			{
			default:
			{
				bool flag5;
				for (;;)
				{
					int leftColumn = this.LeftColumn;
					int topRow = this.TopRow;
					int num = 53;
					for (;;)
					{
						bool flag2;
						bool flag;
						bool flag3;
						int num2;
						bool flag4;
						bool flag6;
						bool flag7;
						int num3;
						bool flag8;
						bool flag9;
						switch (num)
						{
						case 0:
							flag = !flag2;
							goto IL_65E;
						case 1:
							goto IL_3FE;
						case 2:
							flag3 = (newPosition.Left > 0);
							goto IL_241;
						case 3:
							flag4 = (num2 <= this.\u170D.MaxColumnCount);
							goto IL_5F4;
						case 4:
							if (flag5)
							{
								num = 12;
								continue;
							}
							return flag5;
						case 5:
							if (this.\u170D.Version != ExcelVersion.Version2007)
							{
								num = 39;
								continue;
							}
							goto IL_3FE;
						case 6:
							if (this.\u1717.Length > 0L)
							{
								num = 17;
								continue;
							}
							return flag5;
						case 7:
							num = 36;
							continue;
						case 8:
							flag6 = (topRow < sourceRec.Top);
							goto IL_1C9;
						case 9:
							IL_25F:
							if (flag5)
							{
								num = 57;
								continue;
							}
							goto IL_1A9;
						case 10:
							num = 6;
							continue;
						case 11:
							if (this.\u170D.Version == ExcelVersion.Version2010)
							{
								num = 1;
								continue;
							}
							return flag5;
						case 12:
							num = 5;
							continue;
						case 13:
							if (!flag2)
							{
								num = 30;
								continue;
							}
							num = 52;
							continue;
						case 14:
							goto IL_174;
						case 15:
							flag7 = false;
							goto IL_3DA;
						case 16:
							flag8 = (num3 <= this.\u170D.MaxRowCount);
							goto IL_342;
						case 17:
							flag5 = false;
							num = 59;
							continue;
						case 18:
							flag9 = false;
							goto IL_3C8;
						case 19:
							num = 49;
							continue;
						case 20:
							if (topRow <= sourceRec.Bottom)
							{
								num = 46;
								continue;
							}
							num = 43;
							continue;
						case 21:
							goto IL_1A9;
						case 22:
							num = 24;
							continue;
						case 23:
							goto IL_59C;
						case 24:
							flag7 = (leftColumn < sourceRec.Left);
							goto IL_3DA;
						case 25:
							flag7 = true;
							goto IL_3DA;
						case 26:
							if (sourceRec.Bottom + 1 >= this.BottomRow)
							{
								num = 38;
								continue;
							}
							num = 32;
							continue;
						case 27:
							if (topRow == this.BottomRow)
							{
								num = 50;
								continue;
							}
							num = 40;
							continue;
						case 28:
							newPosition.X = leftColumn - sourceRec.Left + destRec.Left;
							num = 42;
							continue;
						case 29:
							flag4 = false;
							goto IL_5F4;
						case 30:
							num = 27;
							continue;
						case 31:
							num3 = destRec.Bottom - (sourceRec.Bottom - this.BottomRow);
							num = 26;
							continue;
						case 32:
							flag8 = false;
							goto IL_342;
						case 33:
							if (flag5)
							{
								num = 45;
								continue;
							}
							goto IL_174;
						case 34:
							flag = false;
							goto IL_65E;
						case 35:
							if (flag5)
							{
								num = 31;
								continue;
							}
							goto IL_253;
						case 36:
							if (leftColumn <= sourceRec.Right)
							{
								num = 22;
								continue;
							}
							num = 25;
							continue;
						case 37:
							num = 3;
							continue;
						case 38:
							num = 16;
							continue;
						case 39:
							num = 11;
							continue;
						case 40:
							flag6 = false;
							goto IL_1C9;
						case 41:
							if (sourceRec.Right + 1 >= this.RightColumn)
							{
								num = 37;
								continue;
							}
							num = 29;
							continue;
						case 42:
							if (sourceRec.Left - 1 <= leftColumn)
							{
								num = 47;
								continue;
							}
							num = 48;
							continue;
						case 43:
							flag6 = true;
							goto IL_1C9;
						case 44:
							if (this.IsMoveWithCell)
							{
								num = 56;
								continue;
							}
							num = 34;
							continue;
						case 45:
							newPosition.Y = topRow - sourceRec.Top + destRec.Top;
							num = 58;
							continue;
						case 46:
							num = 8;
							continue;
						case 47:
							num = 2;
							continue;
						case 48:
							if (true)
							{
							}
							flag3 = false;
							goto IL_241;
						case 49:
							flag9 = (newPosition.Top > 0);
							goto IL_3C8;
						case 50:
							num = 20;
							continue;
						case 51:
							goto IL_253;
						case 52:
							flag6 = true;
							goto IL_1C9;
						case 53:
							if (leftColumn == this.RightColumn)
							{
								num = 7;
								continue;
							}
							num = 15;
							continue;
						case 54:
							if (this.\u1717 != null)
							{
								num = 10;
								continue;
							}
							return flag5;
						case 55:
							if (flag5)
							{
								num = 28;
								continue;
							}
							goto IL_59C;
						case 56:
							num = 0;
							continue;
						case 57:
							num2 = destRec.Right - (sourceRec.Right - this.RightColumn);
							num = 41;
							continue;
						case 58:
							if (sourceRec.Top - 1 <= topRow)
							{
								num = 19;
								continue;
							}
							num = 18;
							continue;
						case 59:
							return flag5;
						}
						break;
						IL_174:
						num = 55;
						continue;
						IL_1A9:
						num = 4;
						continue;
						IL_342:
						flag5 = flag8;
						newPosition.Height = num3 - newPosition.Y;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_25F;
						default:
							if (false)
							{
							}
							num = 51;
							continue;
						}
						IL_1C9:
						flag2 = flag6;
						num = 44;
						continue;
						IL_241:
						flag5 = flag3;
						num = 23;
						continue;
						IL_253:
						num = 9;
						continue;
						IL_3C8:
						flag5 = flag9;
						num = 14;
						continue;
						IL_3DA:
						flag2 = flag7;
						num = 13;
						continue;
						IL_3FE:
						num = 54;
						continue;
						IL_59C:
						num = 35;
						continue;
						IL_5F4:
						flag5 = flag4;
						newPosition.Width = num2 - newPosition.X;
						num = 21;
						continue;
						IL_65E:
						flag5 = flag;
						newPosition = new Rectangle(0, 0, 0, 0);
						num = 33;
					}
				}
				return flag5;
			}
			}
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x0011E19C File Offset: 0x0011D19C
		public virtual XlsShape CopyMoveShape(XlsWorksheet sheet, Rectangle destRec, bool bIsCopy)
		{
			int a_ = 17;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					XlsShape xlsShape;
					int height;
					int width;
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
						spr\u1D9B spr_u1D9B;
						switch (num)
						{
						case 0:
							xlsShape.ClientAnchor.ᜅ(destRec.Bottom - 1);
							xlsShape.ClientAnchor.ᜂ(destRec.Right - 1);
							xlsShape.UpdateWidth();
							xlsShape.UpdateHeight();
							num = 7;
							continue;
						case 1:
							if (sheet != this.Worksheet)
							{
								num = 8;
								continue;
							}
							goto IL_9B;
						case 2:
							if (bIsCopy)
							{
								num = 3;
								continue;
							}
							num = 1;
							continue;
						case 3:
							xlsShape = (XlsShape)xlsShape.Clone(spr_u1D9B, null, null, true);
							num = 9;
							continue;
						case 5:
							goto IL_DE;
						case 6:
							return xlsShape;
						case 7:
							return xlsShape;
						case 8:
						{
							spr\u1D9B spr_u1D9B2 = (spr\u1D9B)this.Worksheet.Shapes;
							spr_u1D9B2.Remove(this);
							spr_u1D9B.AddShape(xlsShape);
							num = 11;
							continue;
						}
						case 9:
							goto IL_9B;
						case 10:
							goto IL_96;
						case 11:
							goto IL_9B;
						}
						if (sheet == null)
						{
							num = 10;
							continue;
						}
						xlsShape = this;
						spr_u1D9B = (spr\u1D9B)sheet.Shapes;
						num = 2;
						continue;
						IL_9B:
						height = this.Height;
						width = this.Width;
						xlsShape.ClientAnchor.ᜆ(destRec.Top - 1);
						xlsShape.ClientAnchor.ᜇ(destRec.Left - 1);
						num = 5;
						continue;
					}
					}
					IL_DE:
					if (this.IsSizeWithCell)
					{
						num = 0;
					}
					else
					{
						xlsShape.Height = height;
						xlsShape.Width = width;
						xlsShape.UpdateBottomRow();
						xlsShape.UpdateRightColumn();
						num = 6;
					}
				}
				IL_96:
				throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
			}
			}
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0011E3D0 File Offset: 0x0011D3D0
		internal void ᜀ(XlsShape A_0, IDictionary A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.\u1714 = A_0.\u1714.Clone(this);
					num = 4;
					continue;
				case 1:
					this.\u1715 = A_0.\u1715.Clone(this);
					num = 2;
					continue;
				case 2:
					goto IL_7C;
				case 3:
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
					break;
				case 4:
					return;
				case 5:
					if (true)
					{
					}
					if (A_0.\u1714 != null)
					{
						num = 0;
						continue;
					}
					return;
				}
				if (A_0.\u1715 != null)
				{
					num = 1;
					continue;
				}
				IL_7C:
				num = 5;
			}
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0011E49C File Offset: 0x0011D49C
		internal void \u1715()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.\u170D.CurrentObjectId++;
					this.OldObjId = (uint)this.\u170D.CurrentObjectId;
					if (true)
					{
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
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_88;
				}
				IL_1C:
				if (this.OldObjId == 0U)
				{
					num = 1;
					continue;
				}
				break;
				goto IL_1C;
			}
			IL_88:
			this.OnPrepareForSerialization();
			this.ShapeRecord.ᜀ(this.\u171D);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x0011E54C File Offset: 0x0011D54C
		protected virtual void OnPrepareForSerialization()
		{
			for (;;)
			{
				if (true)
				{
				}
				sprὙ sprὙ = this.ᜌ as sprὙ;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9D;
						default:
							if (false)
							{
							}
							if (sprὙ != null)
							{
								num = 5;
								continue;
							}
							goto IL_7F;
						}
						break;
					case 1:
						return;
					case 2:
						goto IL_9D;
					case 3:
						if (this.ᜑ != null)
						{
							num = 2;
							continue;
						}
						return;
					case 4:
						goto IL_7F;
					case 5:
						this.ᜀ(sprὙ);
						num = 4;
						continue;
					}
					break;
					IL_7F:
					num = 3;
					continue;
					IL_9D:
					this.ᜉ();
					num = 1;
				}
			}
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x0011E60C File Offset: 0x0011D60C
		private void ᜉ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					List<spr\u25AD> list = this.ᜑ.ᜃ();
					sprᥰ sprᥰ = null;
					int num = 0;
					int count = list.Count;
					int num2 = 11;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							num2 = 14;
							continue;
						case 1:
							goto IL_F0;
						case 2:
							sprᥰ.ᜀ(this.\u171E);
							num2 = 10;
							continue;
						case 3:
							sprᥰ = new sprᥰ();
							list.Insert(list.Count - 2, sprᥰ);
							num2 = 1;
							continue;
						case 4:
							goto IL_1BD;
						case 5:
							goto IL_A3;
						case 6:
							if (sprᥰ == null)
							{
								num2 = 0;
								continue;
							}
							goto IL_F0;
						case 7:
							list.RemoveAt(num);
							num2 = 5;
							continue;
						case 8:
							if (this.\u171E == null)
							{
								num2 = 7;
								continue;
							}
							goto IL_A3;
						case 9:
						{
							spr\u25AD spr_u25AD;
							sprᥰ = (sprᥰ)spr_u25AD;
							goto IL_CB;
						}
						case 10:
							return;
						case 11:
							goto IL_1BD;
						case 12:
							goto IL_A3;
						case 13:
							if (this.\u171E != null)
							{
								num2 = 2;
								continue;
							}
							return;
						case 14:
							if (this.\u171E != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_F0;
						case 15:
						{
							spr\u25AD spr_u25AD;
							if (spr_u25AD.ᜏ() == TObjSubRecordType.ftMacro)
							{
								num2 = 9;
								continue;
							}
							num++;
							num2 = 4;
							continue;
						}
						case 16:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CB;
							default:
							{
								if (false)
								{
								}
								if (num >= count)
								{
									num2 = 12;
									continue;
								}
								spr\u25AD spr_u25AD = list[num];
								num2 = 15;
								continue;
							}
							}
							break;
						}
						break;
						IL_A3:
						num2 = 6;
						continue;
						IL_CB:
						num2 = 8;
						continue;
						IL_F0:
						num2 = 13;
						continue;
						IL_1BD:
						num2 = 16;
					}
				}
				return;
			}
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0011E818 File Offset: 0x0011D818
		internal void ᜄ(int A_0)
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
			this.ShapeRecord.ᜈ(A_0);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x0011E860 File Offset: 0x0011D860
		internal void ᜀ(MsoOptions A_0, int A_1)
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
			spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
			ᜀ.ᜀ(A_0);
			ᜀ.ᜀ(A_1);
			this.ShapeOptions.ᜁ(ᜀ);
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0011E8BC File Offset: 0x0011D8BC
		private spr\u23E7 ShapeOptions
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_38;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					IL_26:
					switch (num)
					{
					case 0:
						goto IL_66;
					case 2:
						if (true)
						{
						}
						this.\u1712 = this.CreateDefaultOptions();
						num = 0;
						continue;
					}
					goto IL_38;
				}
				IL_66:
				goto IL_72;
				IL_38:
				if (this.\u1712 == null)
				{
					num = 2;
					goto IL_26;
				}
				IL_72:
				return this.\u1712;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x0011E944 File Offset: 0x0011D944
		internal List<XlsShape> ChildShapes
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_40;
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
					IL_2E:
					switch (num)
					{
					case 0:
						goto IL_65;
					case 1:
						this.ᜣ = new List<XlsShape>();
						num = 0;
						continue;
					}
					goto IL_40;
				}
				IL_65:
				goto IL_71;
				IL_40:
				if (this.ᜣ == null)
				{
					num = 1;
					goto IL_2E;
				}
				IL_71:
				return this.ᜣ;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x0011E9C8 File Offset: 0x0011D9C8
		internal spr\u23CF ChildAnchor
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
				return this.ᜤ;
			}
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x0011EA0C File Offset: 0x0011DA0C
		internal void ᜀ(int[] A_0)
		{
			if (true)
			{
			}
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_40;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				IL_2E:
				switch (num)
				{
				case 0:
					this.\u170D.FormulaUtil.ᜁ(this.\u171E, A_0);
					num = 1;
					continue;
				case 1:
					goto IL_72;
				}
				goto IL_40;
			}
			IL_72:
			return;
			IL_40:
			if (this.\u171E != null)
			{
				num = 0;
				goto IL_2E;
			}
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0011EA98 File Offset: 0x0011DA98
		internal void ᜀ(IDictionary<int, int> A_0)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_38;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				IL_26:
				switch (num)
				{
				case 1:
					this.\u170D.FormulaUtil.ᜀ(this.\u171E, A_0);
					num = 2;
					continue;
				case 2:
					goto IL_6A;
				}
				goto IL_38;
			}
			IL_6A:
			if (true)
			{
			}
			return;
			IL_38:
			if (this.\u171E != null)
			{
				num = 1;
				goto IL_26;
			}
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0011EB24 File Offset: 0x0011DB24
		private void ᜇ()
		{
			switch (0)
			{
			default:
			{
				XlsWorksheet worksheet;
				int num2;
				int num3;
				for (;;)
				{
					worksheet = this.m_shapes.Worksheet;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_115;
					default:
					{
						if (false)
						{
						}
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (true)
								{
								}
								if (num2 > this.\u170D.MaxColumnCount)
								{
									num = 4;
									continue;
								}
								num3 += worksheet.GetColumnWidthPixels(num2);
								num = 3;
								continue;
							case 1:
								if (num3 > this.\u1713.Left)
								{
									num = 2;
									continue;
								}
								num2++;
								num = 0;
								continue;
							case 2:
								goto IL_115;
							case 3:
								goto IL_EF;
							case 4:
								goto IL_CA;
							case 5:
								goto IL_77;
							case 6:
								if (worksheet == null)
								{
									num = 5;
									continue;
								}
								num3 = 0;
								num2 = 0;
								num = 7;
								continue;
							case 7:
								goto IL_EF;
							}
							break;
							IL_EF:
							num = 1;
						}
						break;
					}
					}
				}
				IL_77:
				this.ClientAnchor.ᜇ(this.\u1713.Left);
				this.ClientAnchor.ᜀ(0);
				return;
				IL_CA:
				this.ClientAnchor.ᜇ(this.\u170D.MaxColumnCount - 1);
				this.ClientAnchor.ᜀ(1024);
				return;
				IL_115:
				int columnWidthPixels = worksheet.GetColumnWidthPixels(num2);
				num3 -= columnWidthPixels;
				int a_ = this.\u1713.Left - num3;
				this.ClientAnchor.ᜇ(num2 - 1);
				this.ClientAnchor.ᜀ(this.ᜁ(a_, columnWidthPixels));
				return;
			}
			}
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0011ECC0 File Offset: 0x0011DCC0
		internal void ᜆ(bool A_0)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_40;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				IL_2E:
				switch (num)
				{
				case 0:
					this.ClientAnchor.ᜀ(0);
					this.ClientAnchor.ᜃ(0);
					this.ClientAnchor.ᜄ(0);
					this.ClientAnchor.ᜁ(0);
					num = 2;
					continue;
				case 2:
					goto IL_85;
				}
				goto IL_40;
			}
			IL_85:
			return;
			IL_40:
			if (A_0)
			{
				num = 0;
				goto IL_2E;
			}
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x0011ED60 File Offset: 0x0011DD60
		protected internal void UpdateRightColumn(int iCount)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num2;
				int num3;
				int num4;
				for (;;)
				{
					XlsWorksheet worksheet = this.m_shapes.Worksheet;
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_180;
						case 1:
							goto IL_71;
						case 2:
							goto IL_1D7;
						case 3:
							goto IL_22C;
						case 4:
							if (worksheet == null)
							{
								num = 1;
								continue;
							}
							num2 = this.Width;
							num3 = this.LeftColumn;
							num4 = this.LeftColumnOffset;
							num = 10;
							continue;
						case 5:
							return;
						case 6:
							goto IL_1F1;
						case 7:
							if (num2 < 0)
							{
								num = 5;
								continue;
							}
							num = 2;
							continue;
						case 8:
						{
							int num5;
							if (num5 < 0)
							{
								num = 3;
								continue;
							}
							if (true)
							{
							}
							num = 9;
							continue;
						}
						case 9:
						{
							int num5;
							if (num5 > num2)
							{
								num = 11;
								continue;
							}
							num2 -= num5;
							num3++;
							num4 = 0;
							num = 0;
							continue;
						}
						case 10:
							goto IL_180;
						case 11:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1D7;
							default:
								goto IL_175;
							}
							break;
						}
						break;
						IL_180:
						num = 7;
						continue;
						IL_1D7:
						if (num3 > this.\u170D.MaxColumnCount)
						{
							num = 6;
						}
						else
						{
							int columnWidthPixels = worksheet.GetColumnWidthPixels(num3 + iCount);
							int num5 = columnWidthPixels - this.ᜅ(num3, num4, true);
							num = 8;
						}
					}
				}
				IL_71:
				this.\u1713.Location = new Point(this.ClientAnchor.ᜃ(), this.\u1713.Top);
				this.ClientAnchor.ᜂ(this.\u1713.Left + this.\u1713.Width);
				this.ClientAnchor.ᜃ(0);
				return;
				IL_175:
				if (false)
				{
				}
				this.RightColumn = num3;
				this.RightColumnOffset = num4 + this.ᜆ(num3, num2, true);
				return;
				IL_1F1:
				this.RightColumn = this.\u170D.MaxColumnCount;
				this.RightColumnOffset = 1024;
				return;
				IL_22C:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("电夷嘹弻䬽ⰿ⍁ぃ⍅ⱇ橉㩋⽍㱏❑ㅓ癕㭗㭙㉛祝ᑟ䉡٣ͥ䡧٩५ᵭͯ剱sṵ᥷ᑹ屻ѽꪅꢇﺋﲍﾏ뒓ﾕ몙ﾛ쾟킡삣쾥욧쮩\ud8ab쮭쎯銱솳욵\udcb7\udbb9좻\udbbd", a_));
			}
			}
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0011EFA0 File Offset: 0x0011DFA0
		protected internal void UpdateRightColumn()
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
			this.UpdateRightColumn(0);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0011EFE4 File Offset: 0x0011DFE4
		private void ᜆ()
		{
			switch (0)
			{
			default:
			{
				XlsWorksheet worksheet;
				int num2;
				int num3;
				for (;;)
				{
					worksheet = this.m_shapes.Worksheet;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_F7;
						case 1:
							goto IL_9F;
						case 2:
							if (worksheet == null)
							{
								num = 5;
								continue;
							}
							num2 = 0;
							num3 = 0;
							num = 1;
							continue;
						case 3:
							goto IL_C4;
						case 4:
							if (num2 > this.\u1713.Top)
							{
								num = 0;
								continue;
							}
							num3++;
							num2 += worksheet.GetRowHeightPixels(num3);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9F;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num = 3;
								continue;
							}
							break;
						case 5:
							goto IL_50;
						}
						break;
						IL_C4:
						num = 4;
						continue;
						IL_9F:
						goto IL_C4;
					}
				}
				IL_50:
				this.ClientAnchor.ᜆ(this.\u1713.Y);
				this.ClientAnchor.ᜁ(0);
				return;
				IL_F7:
				int rowHeightPixels = worksheet.GetRowHeightPixels(num3);
				num2 -= rowHeightPixels;
				int a_ = this.\u1713.Top - num2;
				this.ClientAnchor.ᜆ(num3 - 1);
				this.ClientAnchor.ᜁ(this.ᜀ(a_, rowHeightPixels));
				return;
			}
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0011F128 File Offset: 0x0011E128
		protected internal void UpdateBottomRow()
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num2;
				int num4;
				int num5;
				for (;;)
				{
					if (true)
					{
					}
					XlsWorksheet worksheet = this.m_shapes.Worksheet;
					int num = 9;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (num2 < 0)
							{
								num = 10;
								continue;
							}
							num = 8;
							continue;
						case 1:
							goto IL_1EE;
						case 2:
						{
							int num3;
							if (num3 < 0)
							{
								num = 11;
								continue;
							}
							num = 7;
							continue;
						}
						case 3:
							goto IL_180;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1D4;
							default:
								goto IL_175;
							}
							break;
						case 5:
							goto IL_79;
						case 6:
							goto IL_180;
						case 7:
						{
							int num3;
							if (num3 > num2)
							{
								num = 4;
								continue;
							}
							num2 -= num3;
							num4++;
							num5 = 0;
							num = 3;
							continue;
						}
						case 8:
							goto IL_1D4;
						case 9:
							if (worksheet == null)
							{
								num = 5;
								continue;
							}
							num2 = this.Height;
							num4 = this.TopRow;
							num5 = this.TopRowOffset;
							num = 6;
							continue;
						case 10:
							return;
						case 11:
							goto IL_227;
						}
						break;
						IL_180:
						num = 0;
						continue;
						IL_1D4:
						if (num4 > this.\u170D.MaxRowCount)
						{
							num = 1;
						}
						else
						{
							int rowHeightPixels = worksheet.GetRowHeightPixels(num4);
							int num3 = rowHeightPixels - this.ᜅ(num4, num5, false);
							num = 2;
						}
					}
				}
				IL_79:
				this.\u1713.Location = new Point(this.\u1713.Left, this.ClientAnchor.ᜉ());
				this.ClientAnchor.ᜅ(this.\u1713.Top + this.\u1713.Height);
				this.ClientAnchor.ᜄ(0);
				return;
				IL_175:
				if (false)
				{
				}
				this.BottomRow = num4;
				this.BottomRowOffset = num5 + this.ᜆ(num4, num2, false);
				return;
				IL_1EE:
				this.BottomRow = this.\u170D.MaxRowCount;
				this.BottomRowOffset = 256;
				return;
				IL_227:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("笷嬹倻崽㔿⹁╃㉅ⵇ⹉汋㡍ㅏ㹑⅓㍕硗㥙㵛そ䝟ᙡ䑣ѥ൧䩩k୭ͯű味ɵၷ᭹ቻ幽奔ꒇꪉﲍ﶑뚕벛ﶝ쾟춡횣슥솧쒩춫\udaad햯솱钳쎵좷\udeb9\uddbb쪽ꖿ", a_));
			}
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0011F364 File Offset: 0x0011E364
		internal void \u1718()
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
			this.ᜆ();
			this.UpdateBottomRow();
			this.ᜇ();
			this.UpdateRightColumn();
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0011F3B8 File Offset: 0x0011E3B8
		protected internal void UpdateWidth()
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
			this.\u1713.Width = this.ᜁ(this.LeftColumn, this.LeftColumnOffset, this.RightColumn, this.RightColumnOffset, false);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0011F420 File Offset: 0x0011E420
		protected internal void UpdateHeight()
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
			this.\u1713.Height = this.ᜀ(this.TopRow, this.TopRowOffset, this.BottomRow, this.BottomRowOffset, false);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0011F488 File Offset: 0x0011E488
		internal int ᜅ(int A_0, int A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				double a;
				for (;;)
				{
					XlsWorksheet worksheet = this.m_shapes.Worksheet;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_8E;
						case 1:
							return 0;
						case 2:
							if (worksheet == null)
							{
								num = 1;
								continue;
							}
							num = 5;
							continue;
						case 3:
							goto IL_DD;
						case 4:
						{
							int columnWidthPixels = worksheet.GetColumnWidthPixels(A_0);
							a = (double)(A_1 * columnWidthPixels) / 1024.0;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9F;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						case 5:
						{
							if (A_2)
							{
								goto IL_9F;
							}
							double num2 = (double)worksheet.GetRowHeightPixels(A_0);
							a = (double)A_1 * num2 / 256.0;
							num = 3;
							continue;
						}
						}
						break;
						IL_9F:
						num = 4;
					}
				}
				return 0;
				IL_8E:
				goto IL_E7;
				IL_DD:
				if (true)
				{
				}
				IL_E7:
				return (int)Math.Round(a);
			}
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0011F584 File Offset: 0x0011E584
		internal int ᜆ(int A_0, int A_1, bool A_2)
		{
			int a_ = 14;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8B:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				goto IL_55;
			}
			XlsWorksheet worksheet;
			int columnWidthPixels;
			int rowHeightPixels;
			for (;;)
			{
				IL_27:
				switch (num)
				{
				case 0:
					if (A_1 < 0)
					{
						num = 6;
						continue;
					}
					num = 8;
					continue;
				case 1:
					columnWidthPixels = worksheet.GetColumnWidthPixels(A_0);
					num = 4;
					continue;
				case 2:
					goto IL_93;
				case 3:
					if (worksheet == null)
					{
						num = 9;
						continue;
					}
					num = 0;
					continue;
				case 4:
					goto IL_88;
				case 5:
					if (rowHeightPixels == 0)
					{
						num = 7;
						continue;
					}
					goto IL_135;
				case 6:
					goto IL_AC;
				case 7:
					return 256;
				case 8:
					if (A_2)
					{
						num = 1;
						continue;
					}
					rowHeightPixels = worksheet.GetRowHeightPixels(A_0);
					num = 5;
					continue;
				case 9:
					return 0;
				}
				goto IL_55;
			}
			return 0;
			IL_88:
			if (columnWidthPixels != 0)
			{
				goto IL_8B;
			}
			return columnWidthPixels;
			IL_93:
			return A_1 * 1024 / columnWidthPixels;
			IL_AC:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᑃ⽅ぇ⽉⁋㵍", a_), RecordTableEnumerator.b("ᑃ⽅ぇ⽉⁋㵍灏⅑㱓㥕ⵗ㙙㡛繝ɟݡ䑣ťᩧཀྵ൫ᩭᕯq味ɵၷ᭹ቻ幽奔ꚇ", a_));
			IL_135:
			return A_1 * 256 / rowHeightPixels;
			IL_55:
			worksheet = this.m_shapes.Worksheet;
			num = 3;
			goto IL_27;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0011F6D0 File Offset: 0x0011E6D0
		private int ᜁ(int A_0, int A_1, int A_2, int A_3, bool A_4)
		{
			int a_ = 1;
			switch (0)
			{
			default:
				for (;;)
				{
					XlsWorksheet worksheet = this.m_shapes.Worksheet;
					int num = 19;
					for (;;)
					{
						int num2;
						int num3;
						switch (num)
						{
						case 0:
							num = 21;
							continue;
						case 1:
							if (A_2 > this.\u170D.MaxColumnCount)
							{
								num = 16;
								continue;
							}
							num = 24;
							continue;
						case 2:
							goto IL_2F7;
						case 3:
							if (A_0 >= 1)
							{
								num = 26;
								continue;
							}
							goto IL_25F;
						case 4:
							if (A_0 > A_2)
							{
								num = 18;
								continue;
							}
							num = 10;
							continue;
						case 5:
							return 0;
						case 6:
							goto IL_B6;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_18F;
							default:
								if (false)
								{
								}
								A_2--;
								A_3 = 1024;
								num = 12;
								continue;
							}
							break;
						case 8:
							if (A_3 < 0)
							{
								num = 2;
								continue;
							}
							num = 25;
							continue;
						case 9:
							if (A_0 > this.\u170D.MaxColumnCount)
							{
								num = 15;
								continue;
							}
							num = 14;
							continue;
						case 10:
							if (A_2 >= 1)
							{
								num = 13;
								continue;
							}
							goto IL_194;
						case 11:
							goto IL_18F;
						case 12:
							goto IL_273;
						case 13:
							num = 1;
							continue;
						case 14:
							if (A_3 == 0)
							{
								num = 7;
								continue;
							}
							goto IL_273;
						case 15:
							goto IL_147;
						case 16:
							goto IL_325;
						case 17:
							return num2;
						case 18:
							return 0;
						case 19:
							if (worksheet == null)
							{
								num = 6;
								continue;
							}
							num = 3;
							continue;
						case 20:
							goto IL_F6;
						case 21:
							if (A_1 > A_3)
							{
								num = 5;
								continue;
							}
							goto IL_1E1;
						case 22:
							if (num3 >= A_2)
							{
								num = 17;
								continue;
							}
							num2 += worksheet.GetColumnWidthPixels(num3);
							num3++;
							num = 11;
							continue;
						case 23:
							goto IL_234;
						case 24:
							if (A_1 < 0)
							{
								if (true)
								{
								}
								num = 20;
								continue;
							}
							num = 8;
							continue;
						case 25:
							if (A_0 == A_2)
							{
								num = 0;
								continue;
							}
							goto IL_1E1;
						case 26:
							num = 9;
							continue;
						}
						break;
						IL_1E1:
						int columnWidthPixels = worksheet.GetColumnWidthPixels(A_0);
						int columnWidthPixels2 = worksheet.GetColumnWidthPixels(A_2);
						int num4 = this.ᜄ(columnWidthPixels, Math.Min(A_1, 1024), A_4);
						int num5 = this.ᜄ(columnWidthPixels2, Math.Min(A_3, 1024), A_4);
						num2 = num5 - num4;
						num3 = A_0;
						num = 23;
						continue;
						IL_234:
						num = 22;
						continue;
						IL_18F:
						goto IL_234;
						IL_273:
						num = 4;
					}
				}
				IL_B6:
				return A_2 - A_0;
				IL_F6:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶瘸崺嬼䰾⑀㝂瑄", a_));
				IL_147:
				goto IL_25F;
				IL_194:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶稸吺儼䨾ⱀⵂ睄", a_));
				IL_25F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶稸吺儼䨾ⱀⵂ瑄", a_));
				IL_2F7:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶瘸崺嬼䰾⑀㝂睄", a_));
				IL_325:
				goto IL_194;
			}
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0011FA4C File Offset: 0x0011EA4C
		private int ᜀ(int A_0, int A_1, int A_2, int A_3, bool A_4)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num3;
				for (;;)
				{
					XlsWorksheet worksheet = this.m_shapes.Worksheet;
					int num = 12;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_126;
						case 1:
						{
							if (true)
							{
							}
							int num2;
							if (num2 >= A_2)
							{
								num = 16;
								continue;
							}
							num3 += worksheet.GetRowHeightPixels(num2);
							num2++;
							num = 22;
							continue;
						}
						case 2:
							if (A_1 > A_3)
							{
								num = 6;
								continue;
							}
							goto IL_299;
						case 3:
							num = 18;
							continue;
						case 4:
							if (A_2 >= 1)
							{
								num = 3;
								continue;
							}
							goto IL_283;
						case 5:
							goto IL_21D;
						case 6:
							return 0;
						case 7:
							return 0;
						case 8:
							goto IL_1A8;
						case 9:
							goto IL_DC;
						case 10:
							if (A_0 >= 1)
							{
								num = 24;
								continue;
							}
							goto IL_26F;
						case 11:
							if (A_0 == A_2)
							{
								num = 13;
								continue;
							}
							goto IL_299;
						case 12:
							if (worksheet != null)
							{
								num = 10;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_E1;
							default:
								if (false)
								{
								}
								num = 9;
								continue;
							}
							break;
						case 13:
							goto IL_E1;
						case 14:
							if (A_3 < 0)
							{
								num = 0;
								continue;
							}
							num = 11;
							continue;
						case 15:
							goto IL_20A;
						case 16:
							goto IL_245;
						case 17:
							goto IL_332;
						case 18:
							if (A_2 > this.\u170D.MaxRowCount)
							{
								num = 15;
								continue;
							}
							num = 21;
							continue;
						case 19:
							if (A_0 > this.\u170D.MaxRowCount)
							{
								num = 8;
								continue;
							}
							num = 4;
							continue;
						case 20:
							num3 += worksheet.RowHeightHelper.ᜁ(A_2 - 1) - worksheet.RowHeightHelper.ᜁ(A_0 - 1);
							num = 26;
							continue;
						case 21:
							if (A_2 < A_0)
							{
								num = 7;
								continue;
							}
							num = 23;
							continue;
						case 22:
							goto IL_21D;
						case 23:
							if (A_1 < 0)
							{
								num = 17;
								continue;
							}
							num = 14;
							continue;
						case 24:
							num = 19;
							continue;
						case 25:
						{
							if (this.\u170D.Loading)
							{
								num = 20;
								continue;
							}
							int num2 = A_0;
							num = 5;
							continue;
						}
						case 26:
							goto IL_17D;
						}
						break;
						IL_E1:
						num = 2;
						continue;
						IL_21D:
						num = 1;
						continue;
						IL_299:
						int rowHeightPixels = worksheet.GetRowHeightPixels(A_0);
						int rowHeightPixels2 = worksheet.GetRowHeightPixels(A_2);
						int num4 = XlsShape.ᜃ(rowHeightPixels, A_1, A_4);
						int num5 = XlsShape.ᜃ(rowHeightPixels2, A_3, A_4);
						num3 = num5 - num4;
						num = 25;
					}
				}
				IL_DC:
				return A_2 - A_0;
				IL_126:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇՉ⩋⡍⍏㝑⁓摕", a_));
				IL_17D:
				return num3;
				IL_1A8:
				goto IL_26F;
				IL_20A:
				goto IL_283;
				IL_245:
				return num3;
				IL_26F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇᡉ⍋㥍慏", a_));
				IL_283:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇᡉ⍋㥍扏", a_));
				IL_332:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇՉ⩋⡍⍏㝑⁓杕", a_));
			}
			}
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0011FDCC File Offset: 0x0011EDCC
		private int ᜄ(int A_0, int A_1, bool A_2)
		{
			int result;
			for (;;)
			{
				result = A_1;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (false)
							{
							}
							result = (int)Math.Round((double)(A_1 * A_0) / 1024.0);
							num = 0;
							continue;
						}
						break;
					case 2:
						if (!A_2)
						{
							num = 1;
							continue;
						}
						return result;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0011FE54 File Offset: 0x0011EE54
		private static int ᜃ(int A_0, int A_1, bool A_2)
		{
			int result;
			for (;;)
			{
				result = A_1;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!A_2)
						{
							num = 2;
							continue;
						}
						return result;
					case 1:
						return result;
					case 2:
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
							result = (int)Math.Round((double)(A_1 * A_0) / 256.0);
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0011FEDC File Offset: 0x0011EEDC
		private int ᜁ(int A_0, int A_1)
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
			return A_0 * 1024 / A_1;
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0011FF20 File Offset: 0x0011EF20
		private int ᜀ(int A_0, int A_1)
		{
			int a_ = 11;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (A_0 > A_1)
					{
						num = 2;
						continue;
					}
					goto IL_8F;
				case 2:
					goto IL_8D;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3F;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				goto IL_33;
				IL_3F:
				num = 3;
				continue;
				IL_33:
				if (true)
				{
				}
				if (A_0 >= 0)
				{
					goto IL_3F;
				}
				break;
			}
			IL_49:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀ፂⱄ㽆ⱈ❊㹌", a_));
			IL_8D:
			goto IL_49;
			IL_8F:
			return A_0 * 256 / A_1;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0011FFC8 File Offset: 0x0011EFC8
		protected internal void EvaluateTopLeftPosition()
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
			this.ᜅ();
			this.ᜃ();
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00120010 File Offset: 0x0011F010
		private void ᜅ()
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
			this.\u1713.X = this.ᜁ(1, 0, this.LeftColumn, this.LeftColumnOffset, false);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0012006C File Offset: 0x0011F06C
		private void ᜄ()
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
			this.\u1713.Y = this.ᜀ(1, 0, this.TopRow, this.TopRowOffset, false);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x001200C8 File Offset: 0x0011F0C8
		private void ᜃ()
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
			this.\u1713.Y = this.ᜀ(1, 0, this.TopRow, this.TopRowOffset, false);
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00120124 File Offset: 0x0011F124
		internal void ᜁ(sprᮋ A_0)
		{
			int a_ = 11;
			if (true)
			{
			}
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_50;
				}
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⁀ⵂ♄⽆♈㥊", a_));
			}
			IL_50:
			this.ᜐ = A_0;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00120188 File Offset: 0x0011F188
		private void ᜂ()
		{
			if (this.IsSizeWithCell)
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
					this.UpdateWidth();
					return;
				}
			}
			this.UpdateRightColumn();
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x001201DC File Offset: 0x0011F1DC
		private void ᜁ()
		{
			if (true)
			{
			}
			this.ᜃ();
			if (this.IsSizeWithCell)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_45;
				}
				if (false)
				{
				}
				this.UpdateHeight();
				return;
			}
			IL_45:
			this.UpdateBottomRow();
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x00120234 File Offset: 0x0011F234
		private bool ᜆ(int A_0, bool A_1)
		{
			if (!A_1)
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
					return A_0 <= this.LeftColumn;
				}
			}
			if (true)
			{
			}
			return A_0 <= this.TopRow;
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x00120290 File Offset: 0x0011F290
		private bool ᜅ(int A_0, bool A_1)
		{
			if (true)
			{
			}
			if (!A_1)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_40;
				}
				if (false)
				{
				}
				return A_0 <= this.RightColumn;
			}
			IL_40:
			return A_0 <= this.BottomRow;
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x001202EC File Offset: 0x0011F2EC
		private bool ᜄ(int A_0, bool A_1)
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

		// Token: 0x06002022 RID: 8226 RVA: 0x0012032C File Offset: 0x0011F32C
		private void ᜃ(int A_0, bool A_1)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_AD;
				case 1:
					goto IL_12A;
				case 2:
				{
					sprᮋ sprᮋ = this.ClientAnchor;
					sprᮋ.ᜆ(sprᮋ.ᜉ() + A_0);
					num = 0;
					continue;
				}
				case 3:
					goto IL_14D;
				case 5:
					goto IL_E2;
				case 6:
				{
					sprᮋ sprᮋ2 = this.ClientAnchor;
					sprᮋ2.ᜇ(sprᮋ2.ᜃ() + A_0);
					num = 1;
					continue;
				}
				case 7:
					if (this.ClientAnchor.ᜃ() + A_0 >= 0)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14D;
					default:
						if (false)
						{
						}
						this.ClientAnchor.ᜀ(0);
						num = 5;
						continue;
					}
					break;
				case 8:
					if (this.ClientAnchor.ᜉ() + A_0 >= 0)
					{
						num = 2;
						continue;
					}
					this.ClientAnchor.ᜁ(0);
					num = 3;
					continue;
				case 9:
					num = 8;
					continue;
				}
				if (true)
				{
				}
				if (A_1)
				{
					num = 9;
				}
				else
				{
					num = 7;
				}
			}
			IL_75:
			sprᮋ sprᮋ3 = this.ClientAnchor;
			sprᮋ3.ᜅ(sprᮋ3.ᜇ() + A_0);
			this.ᜃ();
			return;
			IL_AD:
			goto IL_75;
			IL_E2:
			IL_12A:
			goto IL_152;
			IL_14D:
			goto IL_75;
			IL_152:
			sprᮋ sprᮋ4 = this.ClientAnchor;
			sprᮋ4.ᜂ(sprᮋ4.ᜎ() + A_0);
			this.ᜅ();
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x001204A4 File Offset: 0x0011F4A4
		private void ᜂ(int A_0, bool A_1)
		{
			if (A_1)
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
					sprᮋ sprᮋ = this.ClientAnchor;
					sprᮋ.ᜅ(sprᮋ.ᜇ() + A_0);
					this.UpdateHeight();
					return;
				}
				}
			}
			if (true)
			{
			}
			sprᮋ sprᮋ2 = this.ClientAnchor;
			sprᮋ2.ᜂ(sprᮋ2.ᜎ() + A_0);
			this.UpdateWidth();
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00120518 File Offset: 0x0011F518
		private int ᜂ(int A_0, int A_1, bool A_2)
		{
			int num = 4;
			int num3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_69;
				default:
				{
					if (false)
					{
					}
					int num2;
					switch (num)
					{
					case 0:
						num2 = this.LeftColumn;
						goto IL_73;
					case 1:
						goto IL_88;
					case 2:
						num2 = this.TopRow;
						goto IL_73;
					case 3:
						if (A_0 < num3)
						{
							num = 1;
							continue;
						}
						return 0;
					case 5:
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (!A_2)
					{
						num = 5;
						break;
					}
					num = 2;
					break;
					IL_73:
					num3 = num2;
					num = 3;
					break;
				}
				}
			}
			IL_69:
			return Math.Min(A_1, num3 - A_0);
			IL_88:
			goto IL_69;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x001205D0 File Offset: 0x0011F5D0
		private int ᜁ(int A_0, int A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					int num5;
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						num2 = this.RightColumn - 1;
						goto IL_13F;
					case 2:
						num = 1;
						continue;
					case 3:
						num3 = this.TopRow + 1;
						goto IL_B1;
					case 4:
						num2 = this.BottomRow - 1;
						goto IL_13F;
					case 5:
						return 0;
					case 6:
						if (A_0 > num4)
						{
							num = 7;
							continue;
						}
						num = 11;
						continue;
					case 7:
						return 0;
					case 8:
						if (num5 <= 0)
						{
							num = 5;
							continue;
						}
						return num5;
					case 9:
						num = 10;
						continue;
					case 10:
						num3 = this.LeftColumn + 1;
						goto IL_B1;
					case 11:
						if (!A_2)
						{
							num = 9;
							continue;
						}
						num = 3;
						continue;
					}
					if (!A_2)
					{
						num = 2;
						continue;
					}
					num = 4;
					continue;
					IL_B1:
					int val = num3;
					int val2 = A_0 + A_1 - 1;
					num5 = Math.Max(val, A_0) - Math.Min(num4, val2) + 1;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return 0;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					IL_13F:
					num4 = num2;
					num = 6;
				}
				return 0;
			}
			}
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x00120740 File Offset: 0x0011F740
		private bool ᜀ(int A_0, int A_1, bool A_2)
		{
			int num = 5;
			int num2;
			for (;;)
			{
				int num3;
				int num4;
				int num5;
				switch (num)
				{
				case 0:
					if (!A_2)
					{
						num = 6;
						continue;
					}
					num = 9;
					continue;
				case 1:
					if (num2 == num3)
					{
						num = 10;
						continue;
					}
					num2 -= A_0;
					num = 7;
					continue;
				case 2:
					num4 = this.TopRow;
					goto IL_10B;
				case 3:
					num4 = this.LeftColumn;
					goto IL_10B;
				case 4:
					num = 3;
					continue;
				case 6:
					goto IL_128;
				case 7:
					if (num2 >= 0)
					{
						num = 8;
						continue;
					}
					return false;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_128;
					default:
						goto IL_B0;
					}
					break;
				case 9:
					num5 = this.BottomRow;
					goto IL_CB;
				case 10:
					return false;
				case 11:
					if (true)
					{
					}
					num5 = this.RightColumn;
					goto IL_CB;
				}
				if (!A_2)
				{
					num = 4;
					continue;
				}
				num = 2;
				continue;
				IL_CB:
				num3 = num5;
				num = 1;
				continue;
				IL_10B:
				num2 = num4;
				num = 0;
				continue;
				IL_128:
				num = 11;
			}
			IL_B0:
			if (false)
			{
			}
			return num2 < A_1;
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00120878 File Offset: 0x0011F878
		private void ᜁ(int A_0, bool A_1)
		{
			if (true)
			{
			}
			if (A_1)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_60;
				}
				if (false)
				{
				}
				sprᮋ sprᮋ = this.ClientAnchor;
				sprᮋ.ᜆ(sprᮋ.ᜉ() + A_0);
				sprᮋ sprᮋ2 = this.ClientAnchor;
				sprᮋ2.ᜅ(sprᮋ2.ᜇ() + A_0);
				this.ᜃ();
				return;
			}
			IL_60:
			sprᮋ sprᮋ3 = this.ClientAnchor;
			sprᮋ3.ᜇ(sprᮋ3.ᜃ() + A_0);
			sprᮋ sprᮋ4 = this.ClientAnchor;
			sprᮋ4.ᜂ(sprᮋ4.ᜎ() + A_0);
			this.ᜅ();
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00120914 File Offset: 0x0011F914
		private void ᜀ(bool A_0, int A_1)
		{
			int num = 1;
			for (;;)
			{
				int height;
				switch (num)
				{
				case 0:
					if (!this.IsSizeWithCell)
					{
						num = 6;
						continue;
					}
					goto IL_49;
				case 2:
					goto IL_49;
				case 3:
				{
					int width;
					this.Width = width;
					num = 2;
					continue;
				}
				case 4:
					return;
				case 5:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_174;
					default:
					{
						if (false)
						{
						}
						this.ClientAnchor.ᜁ(0);
						sprᮋ sprᮋ = this.ClientAnchor;
						sprᮋ.ᜅ(sprᮋ.ᜇ() + A_1);
						int width = this.Width;
						this.ᜃ();
						num = 10;
						continue;
					}
					}
					break;
				case 6:
					this.Height = height;
					num = 8;
					continue;
				case 7:
					this.UpdateNotSizeNotMoveShape(A_0, 0, A_1);
					num = 4;
					continue;
				case 8:
					goto IL_174;
				case 9:
					if (!this.IsSizeWithCell)
					{
						num = 7;
						continue;
					}
					return;
				case 10:
					if (!this.IsSizeWithCell)
					{
						num = 3;
						continue;
					}
					goto IL_49;
				}
				if (A_0)
				{
					num = 5;
					continue;
				}
				this.ClientAnchor.ᜀ(0);
				sprᮋ sprᮋ2 = this.ClientAnchor;
				sprᮋ2.ᜂ(sprᮋ2.ᜎ() + A_1);
				height = this.Height;
				this.ᜅ();
				num = 0;
				continue;
				IL_49:
				num = 9;
				continue;
				IL_174:
				goto IL_49;
			}
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00120A9C File Offset: 0x0011FA9C
		protected virtual void UpdateNotSizeNotMoveShape(bool bRow, int iIndex, int iCount)
		{
			if (bRow)
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
					this.ᜆ();
					this.UpdateBottomRow();
					return;
				}
			}
			this.ᜇ();
			this.UpdateRightColumn();
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00120AF8 File Offset: 0x0011FAF8
		private void ᜀ(int A_0, bool A_1)
		{
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_AD;
				case 1:
					if (A_1)
					{
						goto IL_A2;
					}
					goto IL_56;
				case 2:
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_A2:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					if (!this.IsSizeWithCell)
					{
						goto IL_AF;
					}
					num = 2;
					break;
				}
			}
			IL_56:
			sprᮋ sprᮋ = this.ClientAnchor;
			sprᮋ.ᜂ(sprᮋ.ᜎ() + A_0);
			this.ᜄ();
			return;
			IL_AD:
			sprᮋ sprᮋ2 = this.ClientAnchor;
			sprᮋ2.ᜅ(sprᮋ2.ᜇ() + A_0);
			this.ᜃ();
			return;
			IL_AF:
			this.UpdateNotSizeNotMoveShape(A_1, 0, A_0);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00120BC0 File Offset: 0x0011FBC0
		private void ᜀ(bool A_0)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0)
					{
						goto IL_94;
					}
					goto IL_56;
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_9F;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_94:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					if (!this.IsSizeWithCell)
					{
						goto IL_A1;
					}
					num = 2;
					break;
				}
			}
			IL_56:
			this.ClientAnchor.ᜃ(0);
			this.ᜅ();
			return;
			IL_9F:
			this.ClientAnchor.ᜄ(0);
			this.ᜃ();
			return;
			IL_A1:
			this.UpdateNotSizeNotMoveShape(A_0, 0, 1);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00120C78 File Offset: 0x0011FC78
		private void ᜀ(sprᮋ A_0)
		{
			int a_ = 9;
			switch (0)
			{
			default:
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
					int num = 6;
					for (;;)
					{
						sprὙ sprὙ;
						switch (num)
						{
						case 0:
							goto IL_E5;
						case 1:
							goto IL_7B;
						case 2:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 0;
								continue;
							}
							IList list;
							spr\u1D3B mso = list[num2] as spr\u1D3B;
							this.UpdateMso(mso);
							num2++;
							num = 3;
							continue;
						}
						case 3:
							goto IL_C9;
						case 4:
							goto IL_10E;
						case 5:
							goto IL_C9;
						case 7:
						{
							if (sprὙ == null)
							{
								num = 4;
								continue;
							}
							IList list = sprὙ.ᜀ();
							int num2 = 0;
							int count = list.Count;
							num = 5;
							continue;
						}
						}
						if (A_0 == null)
						{
							num = 1;
							continue;
						}
						sprὙ = (this.ᜌ as sprὙ);
						num = 7;
						continue;
						IL_C9:
						num = 2;
					}
					IL_E5:
					if (true)
					{
					}
					return;
					IL_10E:
					this.ᜐ = (sprᮋ)A_0.Clone();
					return;
				}
				}
				IL_7B:
				throw new ArgumentNullException(RecordTableEnumerator.b("帾⽀⁂ⵄ⡆㭈", a_));
			}
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00120DBC File Offset: 0x0011FDBC
		private void ᜀ(spr\u23E7 A_0)
		{
			for (;;)
			{
				this.\u1714 = new XlsShapeFill((spr\u2158)base.ReservedHandle, this);
				this.\u1714.Visible = false;
				this.\u1715 = new XlsShapeLineFormat((spr\u2158)base.ReservedHandle, this);
				this.\u1715.Visible = false;
				this.m_bUpdateLineFill = true;
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (A_0 != null)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						this.ᜃ(A_0);
						num = 2;
						continue;
					case 2:
						goto IL_90;
					}
					break;
				}
			}
			IL_90:
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
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00120E84 File Offset: 0x0011FE84
		[CLSCompliant(false)]
		internal virtual bool UpdateMso(spr\u1D3B mso)
		{
			int a_ = 10;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (mso is sprἼ)
					{
						num = 1;
						continue;
					}
					return false;
				case 1:
					goto IL_B7;
				case 2:
					goto IL_EB;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B9;
					default:
						if (false)
						{
						}
						if (mso is spr᪙)
						{
							num = 4;
							continue;
						}
						num = 5;
						continue;
					}
					break;
				case 4:
					goto IL_140;
				case 5:
					if (mso is spr\u23E7)
					{
						num = 2;
						continue;
					}
					num = 0;
					continue;
				case 6:
					goto IL_91;
				case 7:
					goto IL_4C;
				case 8:
					if (mso is sprᮋ)
					{
						num = 6;
						continue;
					}
					num = 3;
					continue;
				}
				if (mso == null)
				{
					num = 7;
				}
				else
				{
					num = 8;
				}
			}
			IL_4C:
			goto IL_B9;
			IL_91:
			this.ᜐ = (mso as sprᮋ);
			return true;
			IL_B7:
			if (true)
			{
			}
			this.ᜏ = (mso as sprἼ);
			return true;
			IL_B9:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⴿㅁ⭃", a_));
			IL_EB:
			this.\u1712 = (mso as spr\u23E7);
			return true;
			IL_140:
			this.ᜑ = (mso as spr᪙).ᜁ();
			return true;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00120FE8 File Offset: 0x0011FFE8
		protected void CloneLineFill(XlsShape sourceShape)
		{
			int a_ = 8;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E2;
				case 1:
					goto IL_6D;
				case 2:
					this.\u1715 = sourceShape.\u1715.Clone(this);
					num = 0;
					continue;
				case 3:
					num = 4;
					continue;
				case 4:
					if (sourceShape.\u1714 != null)
					{
						num = 5;
						continue;
					}
					goto IL_6D;
				case 5:
					this.\u1714 = sourceShape.\u1714.Clone(this);
					num = 1;
					continue;
				case 6:
					goto IL_75;
				case 7:
					goto IL_6B;
				case 8:
					if (this.m_bUpdateLineFill)
					{
						num = 3;
						continue;
					}
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_75:
					if (sourceShape.\u1714 != null)
					{
						num = 2;
						continue;
					}
					return;
				default:
					if (false)
					{
					}
					if (sourceShape == null)
					{
						num = 7;
						continue;
					}
					num = 8;
					continue;
				}
				IL_6D:
				num = 6;
			}
			IL_6B:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽⼿㝁㙃╅ⵇ᥉⑋⽍⁏㝑", a_));
			IL_E2:
			if (true)
			{
			}
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00121128 File Offset: 0x00120128
		private void ᜁ(object A_0, XlsEventArgs A_1)
		{
			int num = 23;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 < this.LeftColumn)
					{
						num = 2;
						continue;
					}
					num = 14;
					continue;
				}
				case 1:
					flag = (0 == this.ᜐ.ᜁ());
					goto IL_1BD;
				case 2:
					num = 18;
					continue;
				case 3:
					flag = true;
					goto IL_1BD;
				case 4:
					goto IL_311;
				case 5:
					num = 1;
					continue;
				case 6:
				{
					if (flag2)
					{
						num = 4;
						continue;
					}
					int num2 = (int)A_1.oldValue;
					num = 21;
					continue;
				}
				case 7:
					if (this.ᜐ.ᜁ() == this.ᜐ.ᜆ())
					{
						num = 5;
						continue;
					}
					goto IL_DA;
				case 8:
					flag = false;
					goto IL_1BD;
				case 9:
					num = 6;
					continue;
				case 10:
					return;
				case 11:
					goto IL_195;
				case 12:
					goto IL_1B8;
				case 13:
				{
					int num2;
					if (num2 == this.RightColumn)
					{
						num = 27;
						continue;
					}
					num = 15;
					continue;
				}
				case 14:
				{
					int num2;
					if (num2 == this.LeftColumn)
					{
						num = 30;
						continue;
					}
					num = 13;
					continue;
				}
				case 15:
					if (this.IsSizeWithCell)
					{
						goto IL_18A;
					}
					goto IL_37D;
				case 16:
					if (true)
					{
					}
					num = 26;
					continue;
				case 17:
					if (!this.\u170D.Loading)
					{
						num = 9;
						continue;
					}
					return;
				case 18:
					if (this.IsMoveWithCell)
					{
						num = 12;
						continue;
					}
					goto IL_370;
				case 19:
					num = 7;
					continue;
				case 20:
					if (this.IsSizeWithCell)
					{
						num = 22;
						continue;
					}
					goto IL_298;
				case 21:
				{
					int num2;
					if (num2 > this.RightColumn)
					{
						num = 10;
						continue;
					}
					num = 0;
					continue;
				}
				case 22:
					goto IL_10C;
				case 24:
					if (this.ᜐ.ᜀ() == this.ᜐ.ᜄ())
					{
						num = 16;
						continue;
					}
					goto IL_DA;
				case 25:
					goto IL_16F;
				case 26:
					if (this.ᜐ.ᜄ() == this.ᜐ.ᜁ())
					{
						num = 19;
						continue;
					}
					goto IL_DA;
				case 27:
					num = 20;
					continue;
				case 28:
					num = 24;
					continue;
				case 29:
					if (this.IsSizeWithCell)
					{
						num = 25;
						continue;
					}
					goto IL_29F;
				case 30:
					num = 29;
					continue;
				}
				if (this.ᜐ == null)
				{
					num = 3;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_18A;
				default:
					if (false)
					{
					}
					num = 28;
					continue;
				}
				IL_DA:
				num = 8;
				continue;
				IL_18A:
				num = 11;
				continue;
				IL_1BD:
				flag2 = flag;
				num = 17;
			}
			IL_10C:
			this.ᜀ();
			return;
			IL_16F:
			this.ᜇ();
			this.UpdateWidth();
			return;
			IL_195:
			this.UpdateWidth();
			return;
			IL_1B8:
			this.ᜅ();
			return;
			IL_298:
			this.UpdateRightColumn();
			return;
			IL_29F:
			this.ᜇ();
			this.UpdateRightColumn();
			return;
			IL_311:
			return;
			IL_370:
			this.ᜇ();
			this.UpdateRightColumn();
			return;
			IL_37D:
			this.UpdateRightColumn();
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x001214B8 File Offset: 0x001204B8
		private void ᜀ()
		{
			int num = 3;
			XlsWorksheet worksheet;
			for (;;)
			{
				IL_0A:
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (worksheet == null)
					{
						num = 0;
						continue;
					}
					goto IL_88;
				case 2:
					return;
				}
				while (this.\u170D.Loading)
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
						num = 2;
						goto IL_0A;
					}
				}
				worksheet = this.m_shapes.Worksheet;
				num = 1;
			}
			return;
			IL_88:
			int num2 = this.ᜁ(this.LeftColumn, this.LeftColumnOffset, this.RightColumn, this.RightColumnOffset, false);
			int columnWidthPixels = worksheet.GetColumnWidthPixels(this.RightColumn);
			this.RightColumnOffset += this.ᜁ(Math.Min(this.Width - num2, columnWidthPixels), columnWidthPixels);
			this.UpdateWidth();
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x001215A4 File Offset: 0x001205A4
		private void ᜀ(object A_0, EventArgs A_1)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜇ();
					this.ᜆ();
					num = 4;
					continue;
				case 1:
					goto IL_57;
				case 2:
					goto IL_8D;
				case 3:
					goto IL_75;
				case 4:
					goto IL_75;
				case 5:
					if (true)
					{
					}
					break;
				case 6:
					if (!this.IsMoveWithCell)
					{
						num = 0;
						continue;
					}
					this.EvaluateTopLeftPosition();
					num = 3;
					continue;
				case 7:
					if (!this.IsSizeWithCell)
					{
						num = 2;
						continue;
					}
					goto IL_E9;
				}
				if (this.\u170D.Loading)
				{
					num = 1;
					continue;
				}
				num = 6;
				continue;
				IL_75:
				num = 7;
			}
			IL_57:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A9:
				this.UpdateRightColumn();
				this.UpdateBottomRow();
				return;
			default:
				if (false)
				{
				}
				return;
			}
			IL_8D:
			goto IL_A9;
			IL_E9:
			this.UpdateWidth();
			this.UpdateHeight();
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x001216A8 File Offset: 0x001206A8
		private void ᜀ(object A_0, XlsEventArgs A_1)
		{
			int num = 15;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_1B7;
				case 2:
					if (this.IsSizeWithCell)
					{
						num = 14;
						continue;
					}
					goto IL_F1;
				case 3:
				{
					int num2;
					if (num2 < this.TopRow)
					{
						num = 4;
						continue;
					}
					num = 17;
					continue;
				}
				case 4:
					num = 6;
					continue;
				case 5:
					num = 8;
					continue;
				case 6:
					if (this.IsMoveWithCell)
					{
						num = 1;
						continue;
					}
					goto IL_1D0;
				case 7:
					return;
				case 8:
					if (this.IsSizeWithCell)
					{
						num = 10;
						continue;
					}
					goto IL_200;
				case 9:
					num = 2;
					continue;
				case 10:
					goto IL_1FB;
				case 11:
					if (this.IsSizeWithCell)
					{
						num = 13;
						continue;
					}
					goto IL_20D;
				case 12:
				{
					int num2;
					if (num2 == this.BottomRow)
					{
						num = 9;
						continue;
					}
					num = 11;
					continue;
				}
				case 13:
					goto IL_173;
				case 14:
					goto IL_150;
				case 16:
				{
					int num2;
					if (num2 > this.BottomRow)
					{
						num = 7;
						continue;
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
						num = 3;
						continue;
					}
					break;
				}
				case 17:
				{
					int num2;
					if (num2 == this.TopRow)
					{
						num = 5;
						continue;
					}
					num = 12;
					continue;
				}
				}
				if (true)
				{
				}
				if (this.\u170D.Loading)
				{
					num = 0;
				}
				else
				{
					int num2 = (int)A_1.oldValue;
					num = 16;
				}
			}
			return;
			IL_F1:
			this.UpdateBottomRow();
			return;
			IL_150:
			this.ᜀ();
			return;
			IL_173:
			this.UpdateHeight();
			return;
			IL_1B7:
			this.ᜃ();
			return;
			IL_1D0:
			this.ᜆ();
			this.UpdateBottomRow();
			return;
			IL_1FB:
			this.ᜆ();
			this.UpdateHeight();
			return;
			IL_200:
			this.ᜆ();
			this.UpdateBottomRow();
			return;
			IL_20D:
			this.UpdateBottomRow();
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x001218C8 File Offset: 0x001208C8
		internal void ᜐ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int a_ = this.ClientAnchor.ᜀ();
					int num = this.ClientAnchor.ᜃ() + 1;
					int columnWidthPixels = this.m_shapes.Worksheet.GetColumnWidthPixels(num);
					int num2 = this.ᜅ(num, a_, true);
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							sprᮋ sprᮋ = this.ClientAnchor;
							sprᮋ.ᜇ(sprᮋ.ᜃ() + 1);
							this.ClientAnchor.ᜀ(num2 - num);
							goto IL_C3;
						}
						case 1:
							return;
						case 2:
							if (columnWidthPixels >= num2)
							{
								return;
							}
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C3;
							default:
								if (false)
								{
								}
								num3 = 0;
								continue;
							}
							break;
						}
						break;
						IL_C3:
						num3 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x001219A8 File Offset: 0x001209A8
		// Note: this type is marked as 'beforefieldinit'.
		static XlsShape()
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
			XlsShape.DEF_FORE_COLOR = spr\u1D39.ᜁ;
			XlsShape.DEF_BACK_COLOR = spr\u1D39.ᜁ;
			XlsShape.ᜇ = new Type[]
			{
				typeof(spr\u1D9B),
				typeof(XlsWorkbook)
			};
			XlsShape.ᜈ = new MsoOptions[]
			{
				MsoOptions.FillType,
				MsoOptions.ForeColor,
				MsoOptions.BackColor,
				MsoOptions.NoFillHitTest
			};
			XlsShape.ᜉ = new MsoOptions[]
			{
				MsoOptions.NoLineDrawDash,
				MsoOptions.LineStyle,
				MsoOptions.LineWeight,
				MsoOptions.LineDashStyle,
				MsoOptions.ContainRoundDot,
				MsoOptions.LineTransparency,
				MsoOptions.LineColor,
				MsoOptions.LineBackColor,
				MsoOptions.ContainLinePattern,
				MsoOptions.LinePattern,
				MsoOptions.LineStartArrow,
				MsoOptions.LineEndArrow,
				MsoOptions.StartArrowLength,
				MsoOptions.EndArrowLength,
				MsoOptions.StartArrowWidth,
				MsoOptions.EndArrowWidth
			};
		}

		// Token: 0x040010EE RID: 4334
		private const int ᜀ = 8;

		// Token: 0x040010EF RID: 4335
		protected const int DEF_SIZETEXTTOFITSHAPE_FALSE_VALUE = 524296;

		// Token: 0x040010F0 RID: 4336
		protected const int DEF_SIZETEXTTOFITSHAPE_TRUE_VALUE = 655370;

		// Token: 0x040010F1 RID: 4337
		protected const int DEF_NOFILLHITTEST_VALUE = 1048592;

		// Token: 0x040010F2 RID: 4338
		internal const int ᜁ = 1024;

		// Token: 0x040010F3 RID: 4339
		internal const int ᜂ = 256;

		// Token: 0x040010F4 RID: 4340
		private const int ᜃ = 9525;

		// Token: 0x040010F5 RID: 4341
		internal const double ᜄ = 655.0;

		// Token: 0x040010F6 RID: 4342
		internal const double ᜅ = 65500.0;

		// Token: 0x040010F7 RID: 4343
		internal const double ᜆ = 12700.0;

		// Token: 0x040010F8 RID: 4344
		protected static readonly Color DEF_FORE_COLOR;

		// Token: 0x040010F9 RID: 4345
		protected static readonly Color DEF_BACK_COLOR;

		// Token: 0x040010FA RID: 4346
		private static readonly Type[] ᜇ;

		// Token: 0x040010FB RID: 4347
		private static readonly MsoOptions[] ᜈ;

		// Token: 0x040010FC RID: 4348
		private static readonly MsoOptions[] ᜉ;

		// Token: 0x040010FD RID: 4349
		protected bool m_bSupportOptions;

		// Token: 0x040010FE RID: 4350
		private string ᜊ = string.Empty;

		// Token: 0x040010FF RID: 4351
		private string ᜋ = string.Empty;

		// Token: 0x04001100 RID: 4352
		private spr\u1D3B ᜌ;

		// Token: 0x04001101 RID: 4353
		private XlsWorkbook \u170D;

		// Token: 0x04001102 RID: 4354
		private ExcelShapeType ᜎ;

		// Token: 0x04001103 RID: 4355
		internal sprἼ ᜏ;

		// Token: 0x04001104 RID: 4356
		private sprᮋ ᜐ;

		// Token: 0x04001105 RID: 4357
		protected ShapeCollectionBase m_shapes;

		// Token: 0x04001106 RID: 4358
		private spr\u2003 ᜑ;

		// Token: 0x04001107 RID: 4359
		internal spr\u23E7 \u1712;

		// Token: 0x04001108 RID: 4360
		private Rectangle \u1713 = default(Rectangle);

		// Token: 0x04001109 RID: 4361
		private XlsShapeFill \u1714;

		// Token: 0x0400110A RID: 4362
		private XlsShapeLineFormat \u1715;

		// Token: 0x0400110B RID: 4363
		protected bool m_bUpdateLineFill = true;

		// Token: 0x0400110C RID: 4364
		private bool \u1716;

		// Token: 0x0400110D RID: 4365
		private Stream \u1717;

		// Token: 0x0400110E RID: 4366
		private Stream \u1718;

		// Token: 0x0400110F RID: 4367
		private sprᦨ \u1719;

		// Token: 0x04001110 RID: 4368
		private string \u171A;

		// Token: 0x04001111 RID: 4369
		private bool[] \u2609\u0097\u0099\u008A;

		// Token: 0x04001112 RID: 4370
		private bool \u171B = true;

		// Token: 0x04001113 RID: 4371
		private byte \u25D9\u0095\u0080\u0093;

		// Token: 0x04001114 RID: 4372
		private bool \u171C;

		// Token: 0x04001115 RID: 4373
		private int \u171D;

		// Token: 0x04001116 RID: 4374
		private Ptg[] \u171E;

		// Token: 0x04001117 RID: 4375
		private float[] \u25D8\u0086\u00A8\u0083;

		// Token: 0x04001118 RID: 4376
		private bool \u171F = true;

		// Token: 0x04001119 RID: 4377
		private ChartShadow ᜠ;

		// Token: 0x0400111A RID: 4378
		private Format3D ᜡ;

		// Token: 0x0400111B RID: 4379
		private bool ᜢ;

		// Token: 0x0400111C RID: 4380
		private List<XlsShape> ᜣ;

		// Token: 0x0400111D RID: 4381
		private spr\u23CF ᜤ;

		// Token: 0x0400111E RID: 4382
		private Dictionary<string, string> ᜥ;

		// Token: 0x0400111F RID: 4383
		private bool ᜦ;

		// Token: 0x04001120 RID: 4384
		private int ᜧ;

		// Token: 0x04001121 RID: 4385
		private bool ᜨ;

		// Token: 0x04001122 RID: 4386
		internal List<Stream> ᜩ;

		// Token: 0x04001123 RID: 4387
		internal List<Stream> ᜪ;

		// Token: 0x04001124 RID: 4388
		internal List<Stream> ᜫ;
	}
}
