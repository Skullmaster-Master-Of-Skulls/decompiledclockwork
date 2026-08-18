using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001FD RID: 509
	public abstract class ShapeCollectionBase : CollectionExtended<IShape>
	{
		// Token: 0x06001CA5 RID: 7333 RVA: 0x000F7774 File Offset: 0x000F6774
		internal ShapeCollectionBase(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.InitializeCollection();
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000F779C File Offset: 0x000F679C
		internal ShapeCollectionBase(spr\u1DF5 A_0, object A_1, spr\u21EB A_2, ExcelParseOptions A_3) : this(A_0, A_1)
		{
			this.ᜀ(A_2, A_3);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x000F77BC File Offset: 0x000F67BC
		protected virtual void InitializeCollection()
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
			this.SetParents();
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000F7800 File Offset: 0x000F6800
		protected void SetParents()
		{
			int a_ = 3;
			this.m_sheet = (base.FindParent(typeof(XlsWorksheetBase), true) as XlsWorksheetBase);
			if (this.m_sheet != null)
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
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("椸娺似娾⽀㝂敄⡆⭈⅊⡌ⱎ═獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ཨѪᡬŮᕰ嵲", a_));
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x000F7880 File Offset: 0x000F6880
		public int ShapesCount
		{
			get
			{
				int count = base.Count;
				if (count > 0)
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
						return count + 1;
					}
				}
				return 0;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x000F78D0 File Offset: 0x000F68D0
		public int ShapesTotalCount
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num;
					for (;;)
					{
						num = 0;
						int num2 = 0;
						int count = base.Count;
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_CC;
								default:
									if (false)
									{
									}
									goto IL_83;
								}
								break;
							case 1:
								num3 = 5;
								continue;
							case 2:
								goto IL_CC;
							case 3:
							{
								if (num2 >= count)
								{
									num3 = 1;
									continue;
								}
								XlsShape xlsShape = (XlsShape)this[num2];
								num += xlsShape.ShapeCount;
								num2++;
								if (true)
								{
								}
								num3 = 2;
								continue;
							}
							case 4:
								return 0;
							case 5:
								if (num <= 0)
								{
									num3 = 4;
									continue;
								}
								goto IL_D0;
							}
							break;
							IL_83:
							num3 = 3;
							continue;
							IL_CC:
							goto IL_83;
						}
					}
					return 0;
					IL_D0:
					return num + 1;
				}
				}
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x000F79B0 File Offset: 0x000F69B0
		public XlsWorksheetBase WorksheetBase
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
				return this.m_sheet;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x000F79F4 File Offset: 0x000F69F4
		public XlsWorksheet Worksheet
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
				return this.m_sheet as XlsWorksheet;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x000F7A3C File Offset: 0x000F6A3C
		public XlsWorkbook Workbook
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
				return this.m_sheet.ParentWorkbook;
			}
		}

		// Token: 0x17000AAA RID: 2730
		public new IShape this[int index]
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
				return base.List[index];
			}
		}

		// Token: 0x17000AAB RID: 2731
		public IShape this[string strShapeName]
		{
			get
			{
				int a_ = 19;
				switch (0)
				{
				default:
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 9;
								continue;
							}
							if (true)
							{
							}
							IList<IShape> list;
							IShape shape = list[num2];
							num = 3;
							continue;
						}
						case 2:
						{
							IShape shape;
							return shape;
						}
						case 3:
						{
							IShape shape;
							if (shape.Name == strShapeName)
							{
								num = 2;
								continue;
							}
							int num2;
							num2++;
							num = 5;
							continue;
						}
						case 4:
							goto IL_5D;
						case 5:
							goto IL_E1;
						case 6:
							goto IL_DF;
						case 7:
							if (strShapeName.Length != 0)
							{
								IList<IShape> list = base.List;
								int num2 = 0;
								int count = list.Count;
								num = 8;
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
								num = 6;
								continue;
							}
							break;
						case 8:
							goto IL_E1;
						case 9:
							goto IL_100;
						}
						if (strShapeName == null)
						{
							num = 4;
							continue;
						}
						num = 7;
						continue;
						IL_E1:
						num = 1;
					}
					IL_5D:
					throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌ᱎ㥐㉒╔㉖᝘㩚ぜ㩞", a_));
					IL_DF:
					throw new ArgumentException(RecordTableEnumerator.b("ᩈ⍊ⱌ㽎㑐獒㭔㙖㑘㹚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰ᵲt᭶ᕸ啺", a_));
					IL_100:
					return null;
				}
				}
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x000F7C34 File Offset: 0x000F6C34
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x000F7C78 File Offset: 0x000F6C78
		internal Stream ShapeLayoutStream
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06001CB2 RID: 7346
		internal abstract TBIFFRecord RecordCode { get; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06001CB3 RID: 7347
		public abstract XlsWorkbookShapeData ShapeData { get; }

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x000F7CBC File Offset: 0x000F6CBC
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x000F7D00 File Offset: 0x000F6D00
		internal int CollectionIndex
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x000F7D44 File Offset: 0x000F6D44
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x000F7D88 File Offset: 0x000F6D88
		internal int LastId
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
				return this.ᜄ;
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x000F7DCC File Offset: 0x000F6DCC
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x000F7E10 File Offset: 0x000F6E10
		internal int StartId
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x000F7E54 File Offset: 0x000F6E54
		private new void ᜀ(spr\u21EB A_0, ExcelParseOptions A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					List<spr\u1D3B> list = A_0.ᜀ();
					sprὙ a_ = (sprὙ)list[0];
					this.ᜀ(a_);
					int num = 1;
					int count = list.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							this.ᜀ(list[num], A_1);
							num++;
							num2 = 1;
							continue;
						case 1:
							goto IL_84;
						case 2:
							return;
						case 3:
							goto IL_84;
						}
						break;
						IL_84:
						num2 = 0;
					}
				}
				return;
			}
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000F7F24 File Offset: 0x000F6F24
		private new void ᜀ(sprὙ A_0)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8C;
				default:
				{
					if (false)
					{
					}
					this.ᜂ = (sprὙ)A_0.Clone();
					sprἼ sprἼ = this.ᜂ.ᜁ()[1] as sprἼ;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜅ = sprἼ.ᜄ();
							num = 2;
							continue;
						case 1:
							if (sprἼ != null)
							{
								num = 0;
								continue;
							}
							goto IL_8C;
						case 2:
							goto IL_8A;
						}
						break;
					}
					break;
				}
				}
			}
			IL_8A:
			IL_8C:
			if (true)
			{
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000F7FC8 File Offset: 0x000F6FC8
		internal new void ᜀ(List<spr\u1D3B> A_0, ExcelParseOptions A_1)
		{
			int a_ = 3;
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					int num = 0;
					int count = A_0.Count;
					int num2 = 1;
					for (;;)
					{
						MsoRecords msoRecords;
						spr\u1D3B spr_u1D3B;
						switch (num2)
						{
						case 0:
							goto IL_C8;
						case 1:
							goto IL_C8;
						case 2:
							if (msoRecords == MsoRecords.msofbtDgContainer)
							{
								num2 = 4;
								continue;
							}
							goto IL_83;
						case 3:
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D4;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						case 4:
							this.ᜀ((spr\u20A0)spr_u1D3B, A_1);
							num2 = 3;
							continue;
						case 5:
							goto IL_D4;
						case 6:
							return;
						}
						break;
						IL_D4:
						if (num >= count)
						{
							num2 = 6;
							continue;
						}
						spr_u1D3B = A_0[num];
						msoRecords = spr_u1D3B.\u1717();
						num2 = 2;
						continue;
						IL_C8:
						num2 = 5;
					}
				}
				IL_83:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("永唺嘼儾⹀㑂⭄杆⑈㡊≌⭎⍐㉒≔㹖㝘㱚絜ⵞѠb੤ᕦ൨", a_));
			}
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x000F80E4 File Offset: 0x000F70E4
		private new void ᜀ(spr\u20A0 A_0, ExcelParseOptions A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					List<spr\u1D3B> list = A_0.ᜀ();
					int num = 0;
					int count = list.Count;
					int num2 = 6;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 9;
							continue;
						case 1:
						{
							MsoRecords msoRecords;
							if (msoRecords != MsoRecords.msofbtRegroupItems)
							{
								num2 = 13;
								continue;
							}
							spr\u1D3B spr_u1D3B;
							this.ᜆ.Add((spr\u262B)spr_u1D3B);
							num2 = 11;
							continue;
						}
						case 2:
							num2 = 1;
							continue;
						case 3:
							return;
						case 4:
							goto IL_F2;
						case 5:
							goto IL_F2;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B0;
							default:
								if (false)
								{
								}
								goto IL_15A;
							}
							break;
						case 7:
							goto IL_F2;
						case 8:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							spr\u1D3B spr_u1D3B = list[num];
							MsoRecords msoRecords = spr_u1D3B.\u1717();
							num2 = 10;
							continue;
						}
						case 9:
						{
							MsoRecords msoRecords;
							if (msoRecords != MsoRecords.msofbtDg)
							{
								num2 = 2;
								continue;
							}
							spr\u1D3B spr_u1D3B;
							this.ᜀ((spr\u2608)spr_u1D3B);
							num2 = 4;
							continue;
						}
						case 10:
						{
							MsoRecords msoRecords;
							if (msoRecords != MsoRecords.msofbtSpgrContainer)
							{
								if (true)
								{
								}
								num2 = 0;
								continue;
							}
							spr\u1D3B spr_u1D3B;
							this.ᜀ((spr\u21EB)spr_u1D3B, A_1);
							num2 = 7;
							continue;
						}
						case 11:
							goto IL_B0;
						case 12:
							goto IL_15A;
						case 13:
							num2 = 5;
							continue;
						}
						break;
						IL_F2:
						num++;
						num2 = 12;
						continue;
						IL_B0:
						goto IL_F2;
						IL_15A:
						num2 = 8;
					}
				}
				return;
			}
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x000F82A0 File Offset: 0x000F72A0
		private new void ᜀ(spr\u2608 A_0)
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
			this.CollectionIndex = A_0.\u1714();
			this.ᜄ = A_0.ᜀ();
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000F82F4 File Offset: 0x000F72F4
		public IShape AddCopy(XlsShape sourceXlsShape)
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
			return this.AddCopy(sourceXlsShape, null, null);
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x000F8338 File Offset: 0x000F7338
		public IShape AddCopy(XlsShape sourceXlsShape, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes)
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
			IShape shape = sourceXlsShape.Clone(this, hashNewNames, dicFontIndexes, true);
			(shape as XlsShape).ShapeId = 0;
			return shape;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x000F838C File Offset: 0x000F738C
		public IShape AddCopy(IShape sourceShape)
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
			return this.AddCopy((XlsShape)sourceShape);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000F83D4 File Offset: 0x000F73D4
		public IShape AddCopy(IShape sourceShape, Dictionary<string, string> hashNewNames, List<int> arrFontIndexes)
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
			return this.AddCopy((XlsShape)sourceShape, hashNewNames, arrFontIndexes);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000F8420 File Offset: 0x000F7420
		public XlsShape AddShape(XlsShape newXlsShape)
		{
			int a_ = 5;
			if (newXlsShape == null)
			{
				if (true)
				{
				}
			}
			else
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
					base.Add(newXlsShape);
					return newXlsShape;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("唺堼䠾ቀ⭂⑄㝆ⱈ", a_));
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x000F8488 File Offset: 0x000F7488
		internal new XlsShape ᜀ(spr\u1D3B A_0, ExcelParseOptions A_1)
		{
			int num = 1;
			for (;;)
			{
				IL_0A:
				switch (num)
				{
				case 0:
					goto IL_30;
				case 2:
					goto IL_86;
				case 3:
					if (true)
					{
					}
					if (A_0 is spr\u21EB)
					{
						num = 2;
						continue;
					}
					goto IL_96;
				}
				while (!(A_0 is sprὙ))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					num = 3;
					goto IL_0A;
				}
				num = 0;
			}
			IL_30:
			return this.AddShape(A_0 as sprὙ, A_1);
			IL_86:
			return this.ᜁ(A_0 as spr\u21EB, A_1);
			IL_96:
			XlsShape newXlsShape = new XlsShape(base.ReservedHandle, this, A_0, A_1);
			return this.AddShape(newXlsShape);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000F8544 File Offset: 0x000F7544
		internal new XlsShape ᜁ(spr\u21EB A_0, ExcelParseOptions A_1)
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
			XlsShape newXlsShape = this.ᜂ(A_0, A_1);
			return this.AddShape(newXlsShape);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x000F8590 File Offset: 0x000F7590
		internal new XlsShape ᜂ(spr\u21EB A_0, ExcelParseOptions A_1)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				XlsShape xlsShape = new XlsShape(base.AppImplementation, this, A_0, A_1);
				List<spr\u1D3B> list = A_0.ᜀ();
				using (List<spr\u1D3B>.Enumerator enumerator = list.GetEnumerator())
				{
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8D;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 1:
							num = 8;
							continue;
						case 2:
						{
							spr\u1D3B spr_u1D3B;
							if (spr_u1D3B is sprὙ)
							{
								num = 4;
								continue;
							}
							num = 10;
							continue;
						}
						case 4:
						{
							spr\u1D3B spr_u1D3B;
							xlsShape.ChildShapes.Add(this.ᜀ(spr_u1D3B as sprὙ, A_1));
							goto IL_8D;
						}
						case 5:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							spr\u1D3B spr_u1D3B = enumerator.Current;
							num = 2;
							continue;
						}
						case 8:
							goto IL_17C;
						case 9:
						{
							spr\u1D3B spr_u1D3B;
							xlsShape.ChildShapes.Add(this.ᜂ(spr_u1D3B as spr\u21EB, A_1));
							num = 0;
							continue;
						}
						case 10:
						{
							spr\u1D3B spr_u1D3B;
							if (spr_u1D3B is spr\u21EB)
							{
								num = 9;
								continue;
							}
							xlsShape.ChildShapes.Add(new XlsShape(base.AppImplementation, this, spr_u1D3B, A_1));
							num = 6;
							continue;
						}
						}
						goto IL_73;
						IL_8D:
						num = 3;
						continue;
						IL_98:
						num = 5;
						continue;
						IL_73:
						goto IL_98;
					}
					IL_17C:;
				}
				return xlsShape;
			}
			}
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x000F8750 File Offset: 0x000F7750
		internal new XlsShape ᜀ(sprὙ A_0, ExcelParseOptions A_1)
		{
			switch (0)
			{
			default:
			{
				XlsShape xlsShape;
				for (;;)
				{
					xlsShape = null;
					List<spr\u1D3B> list = A_0.ᜀ();
					int num = 0;
					int count = list.Count;
					int num2 = 11;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_110;
						case 1:
							goto IL_B4;
						case 2:
							goto IL_110;
						case 3:
							if (xlsShape == null)
							{
								num2 = 10;
								continue;
							}
							return xlsShape;
						case 4:
							goto IL_B4;
						case 5:
							goto IL_168;
						case 6:
						{
							spr᪙ spr᪙ = list[num] as spr᪙;
							List<spr\u25AD> list2 = spr᪙.ᜁ().ᜃ();
							int num3 = 0;
							int count2 = list2.Count;
							num2 = 4;
							continue;
						}
						case 7:
							goto IL_110;
						case 8:
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							num2 = 9;
							continue;
						case 9:
							if (list[num] is spr᪙)
							{
								num2 = 6;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1AF;
							default:
								if (false)
								{
								}
								num++;
								num2 = 5;
								continue;
							}
							break;
						case 10:
							xlsShape = new XlsShape(base.AppImplementation, this, A_0, ExcelParseOptions.Default);
							num2 = 14;
							continue;
						case 11:
							goto IL_168;
						case 12:
							num2 = 2;
							continue;
						case 13:
						{
							List<spr\u25AD> list2;
							int num3;
							spr\u2223 spr_u = list2[num3] as spr\u2223;
							xlsShape = this.CreateShape(spr_u.ᜄ(), A_0, A_1, list2, (int)spr_u.ᜈ());
							num2 = 7;
							continue;
						}
						case 14:
							return xlsShape;
						case 15:
						{
							int num3;
							int count2;
							if (num3 >= count2)
							{
								num2 = 12;
								continue;
							}
							num2 = 16;
							continue;
						}
						case 16:
						{
							List<spr\u25AD> list2;
							int num3;
							if (list2[num3].ᜏ() == TObjSubRecordType.ftCmo)
							{
								num2 = 13;
								continue;
							}
							num3++;
							goto IL_1AF;
						}
						}
						break;
						IL_B4:
						num2 = 15;
						continue;
						IL_110:
						num2 = 3;
						continue;
						IL_168:
						num2 = 8;
						continue;
						IL_1AF:
						num2 = 1;
					}
				}
				return xlsShape;
			}
			}
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x000F8998 File Offset: 0x000F7998
		internal virtual XlsShape AddShape(sprὙ shapeContainer, ExcelParseOptions options)
		{
			switch (0)
			{
			default:
			{
				XlsShape xlsShape;
				for (;;)
				{
					xlsShape = null;
					List<spr\u1D3B> list = shapeContainer.ᜀ();
					int num = 0;
					int count = list.Count;
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_1A7;
						case 1:
							goto IL_108;
						case 2:
							goto IL_AC;
						case 3:
							if (xlsShape == null)
							{
								num2 = 4;
								continue;
							}
							goto IL_235;
						case 4:
							xlsShape = new XlsShape(base.ReservedHandle, this, shapeContainer, ExcelParseOptions.Default);
							num2 = 0;
							continue;
						case 5:
							goto IL_156;
						case 6:
						{
							spr᪙ spr᪙ = list[num] as spr᪙;
							List<spr\u25AD> list2 = spr᪙.ᜁ().ᜃ();
							int num3 = 0;
							int count2 = list2.Count;
							num2 = 10;
							continue;
						}
						case 7:
						{
							List<spr\u25AD> list2;
							int num3;
							spr\u2223 spr_u = list2[num3] as spr\u2223;
							xlsShape = this.CreateShape(spr_u.ᜄ(), shapeContainer, options, list2, num3);
							num2 = 13;
							continue;
						}
						case 8:
						{
							List<spr\u25AD> list2;
							int num3;
							if (list2[num3].ᜏ() == TObjSubRecordType.ftCmo)
							{
								num2 = 7;
								continue;
							}
							num3++;
							goto IL_1B2;
						}
						case 9:
							if (list[num] is spr᪙)
							{
								num2 = 6;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1B2;
							default:
								if (false)
								{
								}
								num++;
								num2 = 14;
								continue;
							}
							break;
						case 10:
							goto IL_AC;
						case 11:
							if (num >= count)
							{
								num2 = 16;
								continue;
							}
							num2 = 9;
							continue;
						case 12:
						{
							int num3;
							int count2;
							if (num3 >= count2)
							{
								num2 = 15;
								continue;
							}
							num2 = 8;
							continue;
						}
						case 13:
							goto IL_108;
						case 14:
							goto IL_156;
						case 15:
							num2 = 1;
							continue;
						case 16:
							if (true)
							{
							}
							goto IL_108;
						}
						break;
						IL_AC:
						num2 = 12;
						continue;
						IL_108:
						num2 = 3;
						continue;
						IL_156:
						num2 = 11;
						continue;
						IL_1B2:
						num2 = 2;
					}
				}
				IL_1A7:
				IL_235:
				return this.AddShape(xlsShape);
			}
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x000F8BE4 File Offset: 0x000F7BE4
		internal virtual XlsShape CreateShape(TObjType objType, sprὙ shapeContainer, ExcelParseOptions options, List<spr\u25AD> subRecords, int cmoIndex)
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

		// Token: 0x06001CCA RID: 7370 RVA: 0x000F8C24 File Offset: 0x000F7C24
		public new void Remove(IShape shape)
		{
			int num;
			for (;;)
			{
				num = 0;
				int count = base.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_96;
					case 1:
						goto IL_8A;
					case 2:
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						goto IL_49;
					case 3:
						return;
					case 4:
						goto IL_96;
					case 5:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_49;
						default:
							if (false)
							{
							}
							if (this[num] == shape)
							{
								num2 = 1;
								continue;
							}
							num++;
							num2 = 4;
							continue;
						}
						break;
					}
					break;
					IL_49:
					num2 = 5;
					continue;
					IL_96:
					num2 = 2;
				}
			}
			IL_8A:
			base.RemoveAt(num);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x000F8CE4 File Offset: 0x000F7CE4
		public override object Clone(object parent)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 12;
				ShapeCollectionBase shapeCollectionBase;
				for (;;)
				{
					ConstructorInfo constructor;
					int num2;
					switch (num)
					{
					case 0:
						if (constructor == null)
						{
							num = 16;
							continue;
						}
						goto IL_2AE;
					case 1:
						goto IL_86;
					case 2:
						goto IL_123;
					case 3:
						goto IL_1C6;
					case 4:
					{
						IShape shape;
						ICloneable cloneable = (ICloneable)shape;
						shape = (IShape)cloneable.Clone();
						num = 2;
						continue;
					}
					case 5:
						goto IL_2AE;
					case 6:
						goto IL_1E7;
					case 7:
						goto IL_2E8;
					case 8:
					{
						int count;
						if (num2 >= count)
						{
							num = 6;
							continue;
						}
						List<IShape> innerList;
						IShape shape = innerList[num2];
						num = 14;
						continue;
					}
					case 9:
						if (constructor != null)
						{
							shapeCollectionBase = (constructor.Invoke(new object[]
							{
								base.AppImplementation,
								parent
							}) as ShapeCollectionBase);
							shapeCollectionBase.ᜃ = this.ᜃ;
							shapeCollectionBase.ᜄ = this.ᜄ;
							shapeCollectionBase.ᜅ = this.ᜅ;
							shapeCollectionBase.RegisterInWorksheet();
							List<IShape> innerList = base.InnerList;
							num2 = 0;
							int count = innerList.Count;
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_190;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 10:
						goto IL_123;
					case 11:
						goto IL_1C6;
					case 13:
					{
						IShape shape;
						if (shape is ICloneParent)
						{
							num = 4;
							continue;
						}
						goto IL_123;
					}
					case 14:
					{
						IShape shape;
						if (shape is XlsShape)
						{
							num = 15;
							continue;
						}
						num = 13;
						continue;
					}
					case 15:
					{
						if (true)
						{
						}
						IShape shape;
						XlsShape xlsShape = (XlsShape)shape;
						shape = (IShape)xlsShape.Clone(shapeCollectionBase);
						num = 10;
						continue;
					}
					case 16:
						goto IL_190;
					}
					if (parent == null)
					{
						num = 1;
						continue;
					}
					Type type = base.GetType();
					constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(spr\u2158),
						typeof(object)
					}, null);
					num = 0;
					continue;
					IL_123:
					num2++;
					num = 11;
					continue;
					IL_190:
					constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
					{
						typeof(spr\u1DF5),
						typeof(object)
					}, null);
					num = 5;
					continue;
					IL_1C6:
					num = 8;
					continue;
					IL_2AE:
					num = 9;
				}
				IL_86:
				throw new ArgumentNullException(RecordTableEnumerator.b("㕄♆㭈⹊⍌㭎", a_));
				IL_1E7:
				shapeCollectionBase.SetParent(parent);
				shapeCollectionBase.SetParents();
				shapeCollectionBase.ᜂ = (sprὙ)spr\u1CD3.ᜀ(this.ᜂ);
				return shapeCollectionBase;
				IL_2E8:
				throw new ApplicationException(RecordTableEnumerator.b("ل♆❈╊≌㭎煐㕒㱔㥖㵘筚⽜㩞ၠᙢ౤ᕦ౨ཪ䵬౮ṰᵲٴͶ୸๺Ṽ୾ꮄ", a_));
			}
			}
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x000F9004 File Offset: 0x000F8004
		protected virtual void RegisterInWorksheet()
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
			this.WorksheetBase.InnerShapesBase = this;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000F904C File Offset: 0x000F804C
		[CLSCompliant(false)]
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				BiffRecordRaw biffRecordRaw;
				MemoryStream memoryStream;
				TBIFFRecord recordCode;
				for (;;)
				{
					IL_17:
					int num = 22;
					for (;;)
					{
						int num4;
						List<spr\u262B>.Enumerator enumerator;
						spr\u20A0 spr_u20A;
						spr\u2608 spr_u;
						spr\u21EB spr_u21EB;
						sprὙ sprὙ;
						spr\u1B5C a_3;
						sprἼ sprἼ;
						int num6;
						List<int> list;
						List<List<BiffRecordRaw>> list2;
						int num7;
						List<BiffRecordRaw> value;
						switch (num)
						{
						case 0:
							return;
						case 1:
						{
							int num2;
							if (num2 <= 0)
							{
								num = 11;
								continue;
							}
							int num3 = Math.Min(num2, 8224);
							biffRecordRaw = spr\u175E.ᜀ(TBIFFRecord.Continue);
							(biffRecordRaw as spr\u2553).ᜀ(num3);
							this.ᜀ(biffRecordRaw, memoryStream, num4, num3);
							num2 -= num3;
							num4 += num3;
							records.ᜀ(biffRecordRaw);
							num = 12;
							continue;
						}
						case 2:
						{
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 2;
										continue;
									case 1:
									{
										if (!enumerator.MoveNext())
										{
											num = 0;
											continue;
										}
										spr\u262B a_2 = enumerator.Current;
										spr_u20A.ᜀ(a_2);
										num = 3;
										continue;
									}
									case 2:
										goto IL_214;
									}
									IL_1EE:
									num = 1;
									continue;
									goto IL_1EE;
								}
								IL_214:
								goto IL_125;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_227;
							IL_125:
							spr_u20A.ᜀ(spr_u);
							spr_u20A.ᜀ(spr_u21EB);
							spr_u21EB.ᜀ(sprὙ);
							sprὙ.ᜀ(a_3);
							sprὙ.ᜀ(sprἼ);
							List<IShape> innerList = base.InnerList;
							int num5 = 0;
							int count = innerList.Count;
							num = 25;
							continue;
						}
						case 3:
							if (this.ᜃ > 0)
							{
								num = 9;
								continue;
							}
							goto IL_38E;
						case 4:
							return;
						case 5:
							goto IL_238;
						case 6:
						{
							num6 = 0;
							int count2 = list.Count;
							num = 5;
							continue;
						}
						case 7:
						{
							int num5;
							int count;
							if (num5 >= count)
							{
								num = 18;
								continue;
							}
							List<IShape> innerList;
							XlsShape xlsShape = innerList[num5] as XlsShape;
							xlsShape.\u1715();
							xlsShape.ᜁ(spr_u21EB);
							num5++;
							num = 20;
							continue;
						}
						case 8:
							goto IL_2A2;
						case 9:
							spr_u.ᜈ(this.ᜃ);
							num = 19;
							continue;
						case 10:
							goto IL_227;
						case 11:
							num = 14;
							continue;
						case 12:
							goto IL_36C;
						case 13:
							if (list.Count != list2.Count)
							{
								num = 23;
								continue;
							}
							num4 = 0;
							recordCode = this.RecordCode;
							num = 17;
							continue;
						case 14:
							goto IL_2A2;
						case 15:
						{
							if (num7 > 8224)
							{
								num = 10;
								continue;
							}
							biffRecordRaw = spr\u175E.ᜀ(recordCode);
							int num2;
							this.ᜀ(biffRecordRaw, memoryStream, num4, num2);
							records.ᜀ(biffRecordRaw);
							num = 8;
							continue;
						}
						case 16:
						{
							int count2;
							if (num6 >= count2)
							{
								num = 4;
								continue;
							}
							num7 = list[num6];
							value = list2[num6];
							int num2 = num7 - num4;
							num = 15;
							continue;
						}
						case 17:
							if (list.Count <= 0)
							{
								goto IL_50C;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
								if (false)
								{
								}
								num = 6;
								continue;
							}
							break;
						case 18:
							list = new List<int>();
							list2 = new List<List<BiffRecordRaw>>();
							spr_u.ᜀ((uint)this.ShapesTotalCount);
							spr_u.ᜀ(this.ᜄ);
							num = 3;
							continue;
						case 19:
							if (true)
							{
							}
							goto IL_38E;
						case 20:
							goto IL_2EC;
						case 21:
							goto IL_36C;
						case 23:
							goto IL_3D3;
						case 24:
							goto IL_238;
						case 25:
							goto IL_2EC;
						}
						if (this.ShapesCount == 0)
						{
							num = 0;
							continue;
						}
						spr_u20A = (spr\u20A0)spr\u231F.ᜀ(MsoRecords.msofbtDgContainer);
						spr_u = (spr\u2608)spr\u231F.ᜀ(MsoRecords.msofbtDg);
						spr_u21EB = (spr\u21EB)spr\u231F.ᜀ(MsoRecords.msofbtSpgrContainer);
						sprὙ = (sprὙ)spr\u231F.ᜀ(MsoRecords.msofbtSpContainer);
						a_3 = (spr\u1B5C)spr\u231F.ᜀ(MsoRecords.msofbtSpgr);
						sprἼ = (sprἼ)spr\u231F.ᜀ(MsoRecords.msofbtSp);
						sprἼ.ᜈ(true);
						sprἼ.ᜄ(true);
						sprἼ.ᜀ(this.ᜅ);
						enumerator = this.ᜆ.GetEnumerator();
						num = 2;
						continue;
						IL_227:
						num = 21;
						continue;
						IL_238:
						num = 16;
						continue;
						IL_2A2:
						num4 = num7;
						records.AddList(value);
						num6++;
						num = 24;
						continue;
						IL_2EC:
						num = 7;
						continue;
						IL_36C:
						num = 1;
						continue;
						IL_38E:
						memoryStream = new MemoryStream();
						memoryStream.Position = 8L;
						this.CreateData(memoryStream, spr_u20A, list, list2);
						num = 13;
					}
				}
				return;
				IL_3D3:
				throw new ArgumentException(RecordTableEnumerator.b("琵䨷弹崻唽㌿扁╃⡅ⱇ橉㹋⭍㍏㵑♓㉕⭗穙㡛ㅝ䁟ౡୣብ䡧ݩ൫ᩭ፯ᩱ婳", a_));
				IL_50C:
				biffRecordRaw = spr\u175E.ᜀ(recordCode);
				int num8 = (int)(memoryStream.Length - 8L);
				byte[] array = new byte[num8];
				memoryStream.Position = 8L;
				memoryStream.Read(array, 0, num8);
				biffRecordRaw.Data = array;
				((spr\u21D9)biffRecordRaw).ᜀ(num8);
				records.ᜀ(biffRecordRaw);
				return;
			}
			}
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x000F95CC File Offset: 0x000F85CC
		private new void ᜀ(BiffRecordRaw A_0, MemoryStream A_1, int A_2, int A_3)
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
			byte[] array = new byte[A_3];
			A_1.Position = (long)(A_2 + 8);
			A_1.Read(array, 0, A_3);
			A_0.Data = array;
			((spr\u21D9)A_0).ᜀ(A_3);
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000F9638 File Offset: 0x000F8638
		internal virtual void CreateData(Stream stream, spr\u20A0 dgContainer, List<int> arrBreaks, List<List<BiffRecordRaw>> arrRecords)
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
			dgContainer.ᜁ(stream, 8, arrBreaks, arrRecords);
		}

		// Token: 0x04001092 RID: 4242
		internal new const int ᜀ = 1024;

		// Token: 0x04001093 RID: 4243
		internal new const int ᜁ = 1024;

		// Token: 0x04001094 RID: 4244
		private bool \u25D9\u0083\u0084\u0099;

		// Token: 0x04001095 RID: 4245
		private new sprὙ ᜂ;

		// Token: 0x04001096 RID: 4246
		private int[] \u2593\u009B\u0091\u00A3;

		// Token: 0x04001097 RID: 4247
		private string[] \u25D9\u00AC\u00AD\u00A4;

		// Token: 0x04001098 RID: 4248
		protected XlsWorksheetBase m_sheet;

		// Token: 0x04001099 RID: 4249
		private int ᜃ;

		// Token: 0x0400109A RID: 4250
		private int ᜄ;

		// Token: 0x0400109B RID: 4251
		private byte \u2609\u00A3\u00A5\u0096;

		// Token: 0x0400109C RID: 4252
		private int ᜅ;

		// Token: 0x0400109D RID: 4253
		private List<spr\u262B> ᜆ = new List<spr\u262B>();

		// Token: 0x0400109E RID: 4254
		private Stream ᜇ;
	}
}
