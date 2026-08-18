using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200062A RID: 1578
	public class XlsWorkbookShapeData : XlsObject, ICloneParent
	{
		// Token: 0x06006084 RID: 24708 RVA: 0x003D0D40 File Offset: 0x003CFD40
		static XlsWorkbookShapeData()
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
			XlsWorkbookShapeData.ᜀ = new MsoBlipType[]
			{
				MsoBlipType.msoblipEMF,
				MsoBlipType.msoblipWMF,
				MsoBlipType.msoblipPICT
			};
			XlsWorkbookShapeData.ᜇ = new Dictionary<MsoBlipType, XlsWorkbookShapeData.ᜀ>();
			XlsWorkbookShapeData.ᜇ.Add(MsoBlipType.msoblipEMF, new XlsWorkbookShapeData.ᜀ(980, 4, 2, 61466));
			XlsWorkbookShapeData.ᜇ.Add(MsoBlipType.msoblipWMF, new XlsWorkbookShapeData.ᜀ(534, 4, 3, 61467));
			XlsWorkbookShapeData.ᜇ.Add(MsoBlipType.msoblipPNG, new XlsWorkbookShapeData.ᜀ(1760, 6, 6, 61470));
			XlsWorkbookShapeData.ᜇ.Add(MsoBlipType.msoblipJPEG, new XlsWorkbookShapeData.ᜀ(1130, 5, 5, 61469));
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x003D0E10 File Offset: 0x003CFE10
		internal XlsWorkbookShapeData(spr\u1DF5 A_0, object A_1, XlsWorkbook.ᜁ A_2)
		{
			int a_ = 1;
			this.ᜁ = new List<sprᜪ>();
			this.ᜂ = new List<spr\u1D3B>();
			this.ᜄ = new Dictionary<spr\u1DD2, sprᜪ>();
			base..ctor(A_0, A_1);
			if (A_2 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("䐶儸娺䴼娾ـ♂ㅄ㍆ⱈ㥊", a_));
			}
			this.ᜅ = A_2;
			this.ᜀ();
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x003D0E78 File Offset: 0x003CFE78
		private void ᜀ()
		{
			int a_ = 12;
			for (;;)
			{
				this.ᜃ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
				if (this.ᜃ == null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_78;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㉁╃㑅ⵇ⑉㡋", a_), RecordTableEnumerator.b("ቁ╃㑅ⵇ⑉㡋湍㽏け㹓㍕㭗⹙籛㵝şౡ੣॥ᱧ䩩๫୭偯ᑱ᭳͵ᙷṹ剻", a_));
			IL_78:
			if (false)
			{
			}
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x003D0F04 File Offset: 0x003CFF04
		internal void ᜀ(spr\u23E6 A_0)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 8;
				for (;;)
				{
					int num2;
					int count;
					List<spr\u1D3B> list;
					switch (num)
					{
					case 0:
						goto IL_75;
					case 1:
						goto IL_8D;
					case 2:
					{
						MsoRecords msoRecords;
						if (msoRecords != MsoRecords.msofbtOPT)
						{
							num = 7;
							continue;
						}
						goto IL_CA;
					}
					case 3:
						num = 2;
						continue;
					case 4:
					{
						MsoRecords msoRecords;
						if (msoRecords != MsoRecords.msofbtBstoreContainer)
						{
							num = 11;
							continue;
						}
						spr\u1D3B spr_u1D3B;
						spr\u1C27 a_2 = (spr\u1C27)spr_u1D3B;
						this.ᜀ(a_2);
						num = 14;
						continue;
					}
					case 5:
					{
						if (num2 >= count)
						{
							num = 15;
							continue;
						}
						spr\u1D3B spr_u1D3B = list[num2];
						MsoRecords msoRecords = spr_u1D3B.\u1717();
						num = 4;
						continue;
					}
					case 6:
						goto IL_CA;
					case 7:
						num = 9;
						continue;
					case 9:
					{
						spr\u1D3B spr_u1D3B;
						this.ᜂ.Add(spr_u1D3B);
						num = 6;
						continue;
					}
					case 10:
						goto IL_115;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8D;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 12;
							continue;
						}
						break;
					case 12:
					{
						MsoRecords msoRecords;
						if (msoRecords != MsoRecords.msofbtDgg)
						{
							num = 3;
							continue;
						}
						spr\u1D3B spr_u1D3B;
						this.ᜈ = (spr\u2412)spr_u1D3B;
						num = 1;
						continue;
					}
					case 13:
						goto IL_115;
					case 14:
						goto IL_CA;
					case 15:
						return;
					}
					if (A_0 == null)
					{
						num = 0;
						continue;
					}
					sprᬈ sprᬈ = (sprᬈ)A_0.ᜃ()[0];
					list = sprᬈ.ᜀ();
					num2 = 0;
					count = list.Count;
					num = 10;
					continue;
					IL_CA:
					num2++;
					num = 13;
					continue;
					IL_8D:
					goto IL_CA;
					IL_115:
					num = 5;
				}
				IL_75:
				throw new ArgumentNullException(RecordTableEnumerator.b("娽㈿⍁㍃Ņ㩇╉㥋㹍", a_));
			}
			}
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x003D1114 File Offset: 0x003D0114
		private void ᜀ(spr\u1C27 A_0)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					int num2;
					int count;
					List<spr\u1D3B> list;
					switch (num)
					{
					case 0:
						goto IL_C2;
					case 1:
						goto IL_69;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E4;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 4:
						goto IL_C2;
					case 5:
					{
						if (true)
						{
						}
						if (num2 >= count)
						{
							goto IL_E4;
						}
						sprᜪ item = list[num2] as sprᜪ;
						this.ᜁ.Add(item);
						num2++;
						num = 0;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					list = A_0.ᜀ();
					num2 = 0;
					count = list.Count;
					num = 4;
					continue;
					IL_C2:
					num = 5;
					continue;
					IL_E4:
					num = 2;
				}
				IL_69:
				throw new ArgumentNullException(RecordTableEnumerator.b("❄ᑆ㵈⑊㽌⩎", a_));
			}
			}
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x003D1214 File Offset: 0x003D0214
		internal void ᜀ(RecordArrayList A_0, TBIFFRecord A_1, sprᦎ A_2)
		{
			switch (0)
			{
			default:
			{
				sprᬈ sprᬈ;
				spr\u23E6 spr_u23E;
				for (;;)
				{
					bool needMsoDrawingGroup = this.NeedMsoDrawingGroup;
					int num = 1;
					for (;;)
					{
						int num2;
						int num3;
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (!needMsoDrawingGroup)
							{
								num = 11;
								continue;
							}
							goto IL_7C;
						case 2:
							goto IL_1FB;
						case 3:
							if (num2 > 0)
							{
								num = 9;
								continue;
							}
							goto IL_CD;
						case 4:
							if (this.ᜁ == null)
							{
								num = 10;
								continue;
							}
							num = 5;
							continue;
						case 5:
							num3 = this.ᜁ.Count;
							goto IL_1B8;
						case 6:
							this.ᜀ(sprᬈ);
							num = 8;
							continue;
						case 7:
							if (needMsoDrawingGroup)
							{
								num = 6;
								continue;
							}
							goto IL_221;
						case 8:
							goto IL_1F9;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_13D;
							default:
							{
								if (false)
								{
								}
								if (true)
								{
								}
								spr\u1C27 spr_u1C = new spr\u1C27(sprᬈ);
								sprᬈ.ᜀ(spr_u1C);
								int num4 = 0;
								num = 2;
								continue;
							}
							}
							break;
						case 10:
							num = 15;
							continue;
						case 11:
							num = 16;
							continue;
						case 12:
							goto IL_1FB;
						case 13:
						{
							int num4;
							if (num4 >= num2)
							{
								num = 14;
								continue;
							}
							sprᜪ a_ = this.ᜁ[num4];
							spr\u1C27 spr_u1C;
							spr_u1C.ᜀ(a_);
							num4++;
							goto IL_13D;
						}
						case 14:
							goto IL_CD;
						case 15:
							num3 = 0;
							goto IL_1B8;
						case 16:
							if (this.ᜈ == null)
							{
								num = 0;
								continue;
							}
							goto IL_7C;
						}
						break;
						IL_7C:
						spr_u23E = (spr\u23E6)spr\u175E.ᜀ(A_1);
						sprᬈ = new sprᬈ(null);
						spr\u2412 a_2 = new spr\u2412(sprᬈ);
						sprᬈ.ᜀ(a_2);
						this.ᜀ(a_2, this.ᜅ, A_2);
						num = 4;
						continue;
						IL_CD:
						num = 7;
						continue;
						IL_13D:
						num = 12;
						continue;
						IL_1B8:
						num2 = num3;
						num = 3;
						continue;
						IL_1FB:
						num = 13;
					}
				}
				return;
				IL_1F9:
				IL_221:
				sprᬈ.ᜀ(this.ᜂ);
				spr_u23E.ᜀ(sprᬈ);
				A_0.ᜀ(spr_u23E);
				return;
			}
			}
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x003D1460 File Offset: 0x003D0460
		private void ᜀ(sprᬈ A_0)
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
			spr\u23E7 a_ = new spr\u23E7(A_0);
			this.ᜀ(a_);
			A_0.ᜀ(a_);
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x003D14B0 File Offset: 0x003D04B0
		private void ᜀ(spr\u23E7 A_0)
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
			ᜀ.ᜀ(MsoOptions.SizeTextToFitShape);
			ᜀ.ᜁ(false);
			ᜀ.ᜀ(false);
			ᜀ.ᜀ(524296U);
			A_0.ᜁ(ᜀ);
			ᜀ = new spr\u23E7.ᜀ();
			ᜀ.ᜀ(MsoOptions.ForeColor);
			ᜀ.ᜁ(false);
			ᜀ.ᜀ(false);
			ᜀ.ᜀ(134217793U);
			A_0.ᜁ(ᜀ);
			ᜀ = new spr\u23E7.ᜀ();
			ᜀ.ᜀ(MsoOptions.LineColor);
			ᜀ.ᜁ(false);
			ᜀ.ᜀ(false);
			ᜀ.ᜀ(134217792U);
			A_0.ᜁ(ᜀ);
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x003D1580 File Offset: 0x003D0580
		internal void ᜀ(spr\u2412 A_0, XlsWorkbook.ᜁ A_1, sprᦎ A_2)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 20;
				uint num2;
				uint num8;
				uint num9;
				for (;;)
				{
					XlsWorksheet worksheet;
					uint num3;
					uint num4;
					int num7;
					XlsWorkbookObjectsCollection objects;
					ShapeCollectionBase shapeCollectionBase;
					switch (num)
					{
					case 0:
						if (A_2 == null)
						{
							num = 21;
							continue;
						}
						goto IL_C7;
					case 1:
						if (worksheet.InnerDVTable != null)
						{
							num = 5;
							continue;
						}
						goto IL_385;
					case 2:
						num2 = num2 - num3 + 1024U;
						num = 12;
						continue;
					case 3:
						goto IL_256;
					case 4:
						if (worksheet != null)
						{
							num = 10;
							continue;
						}
						goto IL_385;
					case 5:
						num4 += (uint)(worksheet.InnerDVTable.Count + 1);
						num = 24;
						continue;
					case 6:
						goto IL_31D;
					case 7:
					{
						int num5 = A_2.ᜀ();
						num2 = (uint)num5;
						int num6 = 1024;
						num = 11;
						continue;
					}
					case 8:
						if (num3 != 0U)
						{
							num = 2;
							continue;
						}
						goto IL_165;
					case 9:
						goto IL_AB;
					case 10:
						num = 1;
						continue;
					case 11:
						goto IL_256;
					case 12:
						goto IL_165;
					case 13:
						num = 27;
						continue;
					case 14:
						goto IL_B0;
					case 15:
					{
						int count;
						if (num7 >= count)
						{
							num = 13;
							continue;
						}
						goto IL_1EA;
					}
					case 16:
						goto IL_343;
					case 17:
					{
						if (this.ᜈ != null)
						{
							num = 6;
							continue;
						}
						num8 = 0U;
						num9 = 0U;
						num2 = 1024U;
						uint num10 = 0U;
						objects = this.ᜃ.Objects;
						num7 = 0;
						int count = objects.Count;
						num = 16;
						continue;
					}
					case 18:
						goto IL_277;
					case 19:
						goto IL_C7;
					case 21:
					{
						uint num10;
						A_0.ᜀ(num10, num4);
						num = 19;
						continue;
					}
					case 22:
					{
						uint num10;
						num10 += 1U;
						num9 += 1U;
						num8 += (uint)shapeCollectionBase.ShapesCount;
						num4 = (uint)shapeCollectionBase.ShapesCount;
						num = 4;
						continue;
					}
					case 23:
						goto IL_343;
					case 24:
						goto IL_385;
					case 25:
					{
						int num5;
						int num6;
						if (num6 >= num5)
						{
							num = 18;
							continue;
						}
						int a_2 = A_2.ᜄ(num6);
						int a_3 = A_2.ᜈ(a_2);
						A_0.ᜀ((uint)a_2, (uint)a_3);
						num6 += 1024;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1EA;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					case 26:
						if (shapeCollectionBase.ShapesCount != 0)
						{
							num = 22;
							continue;
						}
						goto IL_B0;
					case 27:
						if (A_2 != null)
						{
							num = 7;
							continue;
						}
						goto IL_3A8;
					}
					if (A_0 == null)
					{
						num = 9;
						continue;
					}
					num = 17;
					continue;
					IL_B0:
					num7++;
					num = 23;
					continue;
					IL_C7:
					num3 = num2 % 1024U;
					num = 8;
					continue;
					IL_165:
					num2 += num4;
					num = 14;
					continue;
					IL_1EA:
					XlsWorksheetBase a_4 = objects[num7] as XlsWorksheetBase;
					shapeCollectionBase = A_1(a_4);
					worksheet = shapeCollectionBase.Worksheet;
					num = 26;
					continue;
					IL_256:
					num = 25;
					continue;
					IL_343:
					num = 15;
					continue;
					IL_385:
					num = 0;
				}
				IL_AB:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⵈⱊ⩌", a_));
				IL_277:
				if (true)
				{
				}
				goto IL_3A8;
				IL_31D:
				this.ᜀ(this.ᜈ, A_0);
				return;
				IL_3A8:
				A_0.ᜂ(num8);
				A_0.ᜁ(num9);
				A_0.ᜀ(num2);
				return;
			}
			}
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x003D194C File Offset: 0x003D094C
		private void ᜀ(spr\u2412 A_0, spr\u2412 A_1)
		{
			for (;;)
			{
				spr\u2412.ᜀ[] array = A_0.ᜄ();
				int num = 0;
				if (true)
				{
				}
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_33;
					case 1:
						goto IL_33;
					case 2:
					{
						if (num >= array.Length)
						{
							num2 = 3;
							continue;
						}
						spr\u2412.ᜀ ᜀ = array[num];
						A_1.ᜀ(ᜀ.ᜄ(), ᜀ.ᜂ());
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
							num2 = 0;
							continue;
						}
						break;
					}
					case 3:
						goto IL_49;
					}
					break;
					IL_33:
					num2 = 2;
				}
			}
			IL_49:
			A_1.ᜀ(A_0.ᜁ());
			A_1.ᜁ(A_0.ᜃ());
			A_1.ᜂ(A_0.ᜆ());
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x003D1A18 File Offset: 0x003D0A18
		public int AddPicture(Image image, ImageFormatType imageFormat, string strPictureName)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_70:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					num = 8;
					break;
				}
				sprᜪ sprᜪ;
				sprᜪ sprᜪ2;
				sprẫ sprẫ3;
				spr\u1DD2 key;
				for (;;)
				{
					sprẫ sprẫ2;
					switch (num)
					{
					case 0:
					{
						sprẫ sprẫ = new spr\u17B7(sprᜪ);
						num = 2;
						continue;
					}
					case 1:
						if (!XlsWorkbookShapeData.ᜀ(sprᜪ.ᜉ()))
						{
							num = 0;
							continue;
						}
						num = 6;
						continue;
					case 2:
					{
						sprẫ sprẫ;
						sprẫ2 = sprẫ;
						goto IL_191;
					}
					case 3:
						goto IL_79;
					case 4:
						if (sprᜪ2.ᜉ() == sprᜪ.ᜉ())
						{
							num = 7;
							continue;
						}
						goto IL_201;
					case 5:
						num = 4;
						continue;
					case 6:
						sprẫ2 = new spr៣(sprᜪ);
						goto IL_191;
					case 7:
						goto IL_15F;
					case 9:
						if (sprᜪ2 != null)
						{
							num = 5;
							continue;
						}
						goto IL_201;
					}
					if (image == null)
					{
						goto IL_70;
					}
					sprᜪ = new sprᜪ(null);
					sprᜪ.ᜁ(strPictureName);
					sprᜪ.ᜁ(XlsWorkbookShapeData.ᜀ(image.RawFormat, imageFormat));
					sprᜪ.ᜀ(MsoBlipUsage.msoblipUsageDefault);
					XlsWorkbookShapeData.ᜀ ᜀ = XlsWorkbookShapeData.ᜀ(sprᜪ);
					sprᜪ.ᜁ(ᜀ.ᜁ);
					spr\u1D3B spr_u1D3B = sprᜪ;
					byte a_2;
					sprᜪ.ᜀ(a_2 = ᜀ.ᜂ);
					spr_u1D3B.ᜈ((int)a_2);
					sprᜪ.ᜉ(2);
					sprᜪ.ᜂ(1U);
					num = 1;
					continue;
					IL_191:
					sprẫ3 = sprẫ2;
					(sprẫ3 as spr\u1D3B).ᜈ(ᜀ.ᜀ);
					(sprẫ3 as spr\u1D3B).ᜀ((MsoRecords)ᜀ.ᜃ);
					sprẫ3.ᜀ(image);
					key = new spr\u1DD2(sprẫ3.ᜁ());
					this.ᜄ.TryGetValue(key, out sprᜪ2);
					num = 9;
				}
				IL_79:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⹆⑈⩊⩌⩎", a_));
				IL_15F:
				sprᜪ sprᜪ3 = sprᜪ2;
				sprᜪ3.ᜂ(sprᜪ3.\u170D() + 1U);
				return sprᜪ2.ᜎ() + 1;
				IL_201:
				sprᜪ.ᜀ(sprẫ3);
				this.ᜄ.Add(key, sprᜪ);
				return this.ᜁ(sprᜪ);
			}
			}
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x003D1C44 File Offset: 0x003D0C44
		internal int ᜁ(sprᜪ A_0)
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
			int count = this.ᜁ.Count;
			A_0.ᜀ(count);
			this.ᜁ.Add(A_0);
			return count + 1;
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x003D1CA4 File Offset: 0x003D0CA4
		internal sprᜪ ᜀ(int A_0)
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
			return this.ᜁ[A_0 - 1];
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x003D1CF0 File Offset: 0x003D0CF0
		[CLSCompliant(false)]
		public void RemovePicture(uint id, bool removeImage)
		{
			switch (0)
			{
			default:
			{
				int num = 26;
				for (;;)
				{
					int num3;
					int num4;
					int count3;
					switch (num)
					{
					case 0:
						goto IL_A8;
					case 1:
					{
						XlsWorkbookObjectsCollection objects = this.ᜃ.Objects;
						int num2 = 0;
						int count = objects.Count;
						num = 12;
						continue;
					}
					case 2:
						goto IL_1AF;
					case 3:
					{
						XlsBitmapShape xlsBitmapShape;
						if (xlsBitmapShape.BlipId > id)
						{
							num = 6;
							continue;
						}
						goto IL_220;
					}
					case 4:
					{
						spr\u1DD2 key;
						this.ᜄ.Remove(key);
						num = 23;
						continue;
					}
					case 5:
						goto IL_1AF;
					case 6:
					{
						XlsBitmapShape xlsBitmapShape;
						xlsBitmapShape.SetBlipId(xlsBitmapShape.BlipId - 1U);
						num = 7;
						continue;
					}
					case 7:
						goto IL_220;
					case 8:
						num = 24;
						continue;
					case 9:
					{
						int num2;
						num2++;
						num = 10;
						continue;
					}
					case 10:
						goto IL_237;
					case 11:
						goto IL_143;
					case 12:
						goto IL_237;
					case 13:
						if (!removeImage)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A8;
						default:
							if (false)
							{
							}
							num = 15;
							continue;
						}
						break;
					case 14:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 27;
							continue;
						}
						XlsWorkbookObjectsCollection objects;
						XlsWorksheetBase a_ = objects[num2] as XlsWorksheetBase;
						ShapeCollectionBase shapeCollectionBase = this.ᜅ(a_);
						num3 = 0;
						int count2 = shapeCollectionBase.Count;
						num = 11;
						continue;
					}
					case 15:
					{
						if (true)
						{
						}
						this.ᜁ.RemoveAt((int)(id - 1U));
						sprᜪ sprᜪ;
						byte[] a_2 = sprᜪ.ᜄ().ᜁ();
						spr\u1DD2 key = new spr\u1DD2(a_2);
						num = 25;
						continue;
					}
					case 16:
					{
						sprᜪ sprᜪ;
						if (sprᜪ.\u170D() <= 0U)
						{
							num = 20;
							continue;
						}
						return;
					}
					case 17:
						goto IL_2A7;
					case 18:
						goto IL_143;
					case 19:
					{
						if (num4 >= count3)
						{
							num = 1;
							continue;
						}
						sprᜪ sprᜪ2 = this.ᜁ[num4];
						sprᜪ sprᜪ3 = sprᜪ2;
						sprᜪ3.ᜀ(sprᜪ3.ᜎ() - 1);
						num4++;
						num = 2;
						continue;
					}
					case 20:
						num = 13;
						continue;
					case 21:
					{
						XlsBitmapShape xlsBitmapShape;
						if (xlsBitmapShape != null)
						{
							num = 0;
							continue;
						}
						goto IL_220;
					}
					case 22:
					{
						int count2;
						if (num3 >= count2)
						{
							num = 9;
							continue;
						}
						ShapeCollectionBase shapeCollectionBase;
						XlsBitmapShape xlsBitmapShape = shapeCollectionBase[num3] as XlsBitmapShape;
						num = 21;
						continue;
					}
					case 23:
						goto IL_25D;
					case 24:
					{
						if ((ulong)id > (ulong)((long)this.ᜁ.Count))
						{
							num = 17;
							continue;
						}
						sprᜪ sprᜪ = this.ᜁ[(int)(id - 1U)];
						sprᜪ sprᜪ4 = sprᜪ;
						sprᜪ4.ᜂ(sprᜪ4.\u170D() - 1U);
						num = 16;
						continue;
					}
					case 25:
					{
						spr\u1DD2 key;
						if (this.ᜄ.ContainsKey(key))
						{
							num = 4;
							continue;
						}
						goto IL_25D;
					}
					case 27:
						goto IL_258;
					}
					if (id >= 1U)
					{
						num = 8;
						continue;
					}
					break;
					IL_A8:
					num = 3;
					continue;
					IL_143:
					num = 22;
					continue;
					IL_1AF:
					num = 19;
					continue;
					IL_220:
					num3++;
					num = 18;
					continue;
					IL_237:
					num = 14;
					continue;
					IL_25D:
					num4 = (int)(id - 1U);
					count3 = this.ᜁ.Count;
					num = 5;
				}
				return;
				IL_258:
				return;
				IL_2A7:
				return;
			}
			}
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x003D20A8 File Offset: 0x003D10A8
		public void Clear()
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
			this.ᜁ.Clear();
			this.ᜂ.Clear();
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x003D20FC File Offset: 0x003D10FC
		public object Clone(object parent)
		{
			int a_ = 13;
			if (parent == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("㍂⑄㕆ⱈ╊㥌", a_));
				}
			}
			XlsWorkbookShapeData xlsWorkbookShapeData = (XlsWorkbookShapeData)base.MemberwiseClone();
			xlsWorkbookShapeData.SetParent(parent);
			xlsWorkbookShapeData.ᜀ();
			xlsWorkbookShapeData.ᜁ = spr\u1CD3.ᜀ<sprᜪ>(this.ᜁ);
			xlsWorkbookShapeData.ᜂ = spr\u1CD3.ᜀ<spr\u1D3B>(this.ᜂ);
			xlsWorkbookShapeData.ᜄ = spr\u1CD3.ᜀ<spr\u1DD2, sprᜪ>(this.ᜄ);
			return xlsWorkbookShapeData;
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x003D21A8 File Offset: 0x003D11A8
		internal int ᜃ()
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
			this.ᜆ++;
			return this.ᜆ;
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06006095 RID: 24725 RVA: 0x003D21F8 File Offset: 0x003D11F8
		internal List<sprᜪ> Pictures
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
				return this.ᜁ;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06006096 RID: 24726 RVA: 0x003D223C File Offset: 0x003D123C
		protected bool NeedMsoDrawingGroup
		{
			get
			{
				XlsWorkbookObjectsCollection objects = this.ᜃ.Objects;
				IEnumerator<ShapeCollectionBase> enumerator = this.ᜃ.ᜃ(this.ᜅ).GetEnumerator();
				bool result;
				try
				{
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							result = true;
							num = 4;
							continue;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 8;
								continue;
							}
							ShapeCollectionBase shapeCollectionBase = enumerator.Current;
							num = 7;
							continue;
						}
						case 2:
							goto IL_FC;
						case 3:
							num = 5;
							continue;
						case 4:
							goto IL_CE;
						case 5:
						{
							ShapeCollectionBase shapeCollectionBase;
							if (shapeCollectionBase.Count > 0)
							{
								num = 0;
								continue;
							}
							break;
						}
						case 7:
						{
							ShapeCollectionBase shapeCollectionBase;
							if (shapeCollectionBase != null)
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
									num = 3;
									continue;
								}
							}
							break;
						}
						case 8:
							num = 2;
							continue;
						}
						IL_6F:
						num = 1;
						continue;
						goto IL_6F;
					}
					IL_CE:
					return result;
					IL_FC:
					goto IL_25;
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							enumerator.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_138;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 0;
					}
					IL_138:;
				}
				return result;
				IL_25:
				if (true)
				{
				}
				return false;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06006097 RID: 24727 RVA: 0x003D2398 File Offset: 0x003D1398
		internal spr\u2412 PreservedClusters
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
				return this.ᜈ;
			}
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x003D23DC File Offset: 0x003D13DC
		internal void ᜄ()
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
			this.ᜈ = null;
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x003D2420 File Offset: 0x003D1420
		internal static MsoBlipType ᜀ(ImageFormat A_0)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return MsoBlipType.msoblipEMF;
				case 1:
					return MsoBlipType.msoblipJPEG;
				case 2:
					if (A_0.Equals(ImageFormat.Jpeg))
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					num = 6;
					continue;
				case 3:
					return MsoBlipType.msoblipDIB;
				case 4:
					if (A_0.Equals(ImageFormat.Emf))
					{
						num = 0;
						continue;
					}
					return MsoBlipType.msoblipPNG;
				case 6:
					if (A_0.Equals(ImageFormat.Png))
					{
						num = 7;
						continue;
					}
					num = 4;
					continue;
				case 7:
					return MsoBlipType.msoblipPNG;
				}
				IL_30:
				if (!A_0.Equals(ImageFormat.Bmp))
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
					num = 3;
					continue;
				}
				goto IL_30;
			}
			return MsoBlipType.msoblipDIB;
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x003D2514 File Offset: 0x003D1514
		internal static MsoBlipType ᜀ(ImageFormat A_0, ImageFormatType A_1)
		{
			MsoBlipType result;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				result = XlsWorkbookShapeData.ᜀ(A_0);
				if (A_1 != ImageFormatType.Original)
				{
					return (MsoBlipType)A_1;
				}
				break;
			}
			if (true)
			{
			}
			return result;
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x003D2560 File Offset: 0x003D1560
		internal static bool ᜀ(MsoBlipType A_0)
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
			return Array.IndexOf<MsoBlipType>(XlsWorkbookShapeData.ᜀ, A_0) == -1;
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x003D25AC File Offset: 0x003D15AC
		internal static XlsWorkbookShapeData.ᜀ ᜀ(sprᜪ A_0)
		{
			MsoBlipType key;
			for (;;)
			{
				key = A_0.ᜉ();
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
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_6C;
						case 1:
							num = 3;
							continue;
						case 2:
							if (!XlsWorkbookShapeData.ᜇ.ContainsKey(key))
							{
								num = 1;
								continue;
							}
							num = 0;
							continue;
						case 3:
							goto IL_8C;
						}
						break;
					}
					break;
				}
				}
			}
			IL_6C:
			if (true)
			{
			}
			return XlsWorkbookShapeData.ᜇ[key];
			IL_8C:
			return XlsWorkbookShapeData.ᜇ[MsoBlipType.msoblipPNG];
		}

		// Token: 0x04002E45 RID: 11845
		private static readonly MsoBlipType[] ᜀ;

		// Token: 0x04002E46 RID: 11846
		private List<sprᜪ> ᜁ;

		// Token: 0x04002E47 RID: 11847
		private List<spr\u1D3B> ᜂ;

		// Token: 0x04002E48 RID: 11848
		private XlsWorkbook ᜃ;

		// Token: 0x04002E49 RID: 11849
		private Dictionary<spr\u1DD2, sprᜪ> ᜄ;

		// Token: 0x04002E4A RID: 11850
		private XlsWorkbook.ᜁ ᜅ;

		// Token: 0x04002E4B RID: 11851
		private int ᜆ;

		// Token: 0x04002E4C RID: 11852
		private static readonly Dictionary<MsoBlipType, XlsWorkbookShapeData.ᜀ> ᜇ;

		// Token: 0x04002E4D RID: 11853
		private spr\u2412 ᜈ;

		// Token: 0x0200062B RID: 1579
		internal class ᜀ
		{
			// Token: 0x0600609D RID: 24733 RVA: 0x003D2654 File Offset: 0x003D1654
			public ᜀ(int A_0, byte A_1, byte A_2, int A_3)
			{
				this.ᜀ = A_0;
				this.ᜁ = A_1;
				this.ᜂ = A_2;
				this.ᜃ = A_3;
			}

			// Token: 0x04002E4E RID: 11854
			public int ᜀ;

			// Token: 0x04002E4F RID: 11855
			public byte ᜁ;

			// Token: 0x04002E50 RID: 11856
			public byte ᜂ;

			// Token: 0x04002E51 RID: 11857
			public int ᜃ;
		}
	}
}
