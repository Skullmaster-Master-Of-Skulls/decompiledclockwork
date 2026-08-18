using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000035 RID: 53
	public class XlsDataValidationCollection : CollectionExtended<XlsValidation>
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x00021294 File Offset: 0x00020294
		internal XlsDataValidationCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜁ();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000212BC File Offset: 0x000202BC
		internal XlsDataValidationCollection(spr\u1DF5 A_0, object A_1, spr\u22CB A_2) : this(A_0, A_1)
		{
			this.ᜀ = (spr\u22CB)A_2.Clone();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000212E4 File Offset: 0x000202E4
		internal XlsDataValidationCollection(spr\u1DF5 A_0, object A_1, List<BiffRecordRaw> A_2, ref int A_3) : this(A_0, A_1)
		{
			this.ᜀ(A_2, ref A_3, false);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00021304 File Offset: 0x00020304
		private new void ᜁ()
		{
			int a_ = 4;
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
				this.ᜁ = (base.FindParent(typeof(XlsDataValidationTable)) as XlsDataValidationTable);
				if (this.ᜁ != null)
				{
					return;
				}
				break;
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("樹崻䰽┿ⱁぃ晅❇⡉♋⭍㍏♑瑓㕕㥗㑙㉛ㅝᑟ䉡٣ͥ䡧౩ͫ᭭ṯᙱ婳", a_));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00021384 File Offset: 0x00020384
		protected internal new XlsValidation Add(XlsValidation dv)
		{
			int num = 6;
			XlsValidation xlsValidation;
			sprᡣ key;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2 = 0;
					this.ᜀ(this.ᜃ, ref num2, this.ᜃ.Count);
					goto IL_9C;
				}
				case 1:
					xlsValidation.AddRange(dv);
					num = 2;
					continue;
				case 2:
					goto IL_54;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9C;
					default:
						if (false)
						{
						}
						goto IL_D1;
					}
					break;
				case 4:
					if (xlsValidation != dv)
					{
						num = 1;
						continue;
					}
					return xlsValidation;
				case 5:
					xlsValidation = this.ᜂ[key];
					num = 4;
					continue;
				case 7:
					if (this.ᜂ.ContainsKey(key))
					{
						num = 5;
						continue;
					}
					goto IL_101;
				}
				if (this.ᜄ)
				{
					num = 0;
					continue;
				}
				goto IL_D1;
				IL_9C:
				num = 3;
				continue;
				IL_D1:
				key = dv.DVRecord;
				num = 7;
			}
			IL_54:
			if (true)
			{
			}
			return xlsValidation;
			IL_101:
			this.ᜂ.Add(key, dv);
			base.Add(dv);
			return dv;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000214A8 File Offset: 0x000204A8
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 0;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_10C;
				case 1:
					num = 2;
					continue;
				case 2:
					if ((long)this.ᜃ.Count != (long)((ulong)this.ᜀ.ᜁ()))
					{
						num = 11;
						continue;
					}
					goto IL_197;
				case 4:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 6;
						continue;
					}
					XlsValidation xlsValidation = base.List[num2];
					xlsValidation.ᜀ(records);
					num2++;
					num = 0;
					continue;
				}
				case 5:
					return;
				case 6:
					return;
				case 7:
					if (this.ᜄ)
					{
						num = 1;
						continue;
					}
					num = 8;
					continue;
				case 8:
				{
					if (base.Count == 0)
					{
						num = 5;
						continue;
					}
					this.ᜀ.ᜁ((uint)base.Count);
					records.ᜀ(this.ᜀ);
					int num2 = 0;
					int count = base.Count;
					num = 9;
					continue;
				}
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10C;
					default:
						if (false)
						{
						}
						goto IL_12D;
					}
					break;
				case 10:
					goto IL_54;
				case 11:
					goto IL_195;
				}
				if (records == null)
				{
					num = 10;
					continue;
				}
				num = 7;
				continue;
				IL_12D:
				num = 4;
				continue;
				IL_10C:
				goto IL_12D;
			}
			IL_54:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵崷夹医䰽␿ㅁ", a_));
			IL_195:
			if (true)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("电夷吹刻儽㐿扁≃⽅♇⹉汋⩍ㅏ♑㕓癕⹗㭙せ㝝џ͡ၣཥݧѩ䱫୭ṯٱٳήᵷॹ剻", a_));
			IL_197:
			records.ᜀ(this.ᜀ);
			records.AddList(this.ᜃ);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00021668 File Offset: 0x00020668
		private new void ᜀ(List<BiffRecordRaw> A_0, ref int A_1, bool A_2)
		{
			int a_ = 4;
			int num = 5;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_98;
					default:
						goto IL_E5;
					}
					break;
				case 1:
					goto IL_173;
				case 2:
					if (A_1 > A_0.Count)
					{
						num = 1;
						continue;
					}
					this.ᜀ = (A_0[A_1] as spr\u22CB);
					A_1++;
					num = 9;
					continue;
				case 3:
					goto IL_A3;
				case 4:
					goto IL_124;
				case 6:
					num = 2;
					continue;
				case 7:
					if (A_2)
					{
						num = 4;
						continue;
					}
					goto IL_175;
				case 8:
					if (A_1 >= 0)
					{
						num = 6;
						continue;
					}
					goto IL_126;
				case 9:
					if (this.ᜀ == null)
					{
						goto IL_98;
					}
					num2 = (int)this.ᜀ.ᜁ();
					num = 7;
					continue;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 8;
				continue;
				IL_98:
				num = 3;
			}
			IL_A3:
			throw new ArgumentNullException(RecordTableEnumerator.b("縹樻弽ⰿ扁㙃⍅⭇╉㹋⩍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣eݧὩɫ੭幯", a_));
			IL_E5:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃㕅", a_));
			IL_124:
			this.ᜀ(A_0, ref A_1, num2);
			return;
			IL_126:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唹娻堽㌿❁ぃ", a_), RecordTableEnumerator.b("氹崻刽㔿❁摃╅⥇⑉≋⅍⑏牑㙓㍕硗㙙㥛ⵝ፟䉡ၣ๥१ѩ䱫幭偯ᵱٳ噵ίࡹ᥻ώꚅﲇ낏늑ﮗ瞧펟芡잣즥\udda7쒩\ud8ab肭", a_));
			IL_173:
			goto IL_126;
			IL_175:
			this.ᜃ = new List<BiffRecordRaw>(num2);
			this.ᜃ.AddRange(A_0.GetRange(A_1, num2));
			this.ᜄ = true;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00021814 File Offset: 0x00020814
		protected internal new void Remove(XlsValidation dv)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2 = 0;
					this.ᜀ(this.ᜃ, ref num2, this.ᜃ.Count);
					num = 3;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9C;
					default:
						goto IL_72;
					}
					break;
				case 3:
					goto IL_7A;
				case 4:
					goto IL_9C;
				case 5:
					base.Remove(dv);
					this.ᜂ.Remove(dv.DVRecord);
					num = 1;
					continue;
				}
				if (this.ᜄ)
				{
					num = 0;
					continue;
				}
				IL_7A:
				int num3 = base.List.IndexOf(dv);
				num = 4;
				continue;
				IL_9C:
				if (true)
				{
				}
				if (num3 < 0)
				{
					return;
				}
				num = 5;
			}
			IL_72:
			if (false)
			{
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00021900 File Offset: 0x00020900
		public void Remove(Rectangle[] rectangles)
		{
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
						goto IL_B4;
					default:
						goto IL_70;
					}
					break;
				case 1:
					goto IL_46;
				case 3:
				{
					if (true)
					{
					}
					int num2 = base.Count - 1;
					num = 5;
					continue;
				}
				case 4:
				{
					int num2;
					if (num2 < 0)
					{
						num = 0;
						continue;
					}
					this[num2].RemoveRange(rectangles);
					num2--;
					num = 1;
					continue;
				}
				case 5:
					goto IL_B4;
				}
				if (base.Count > 0)
				{
					num = 3;
					continue;
				}
				return;
				IL_46:
				num = 4;
				continue;
				IL_B4:
				goto IL_46;
			}
			IL_70:
			if (false)
			{
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000219C4 File Offset: 0x000209C4
		public override object Clone(object parent)
		{
			switch (0)
			{
			default:
			{
				XlsDataValidationCollection xlsDataValidationCollection;
				for (;;)
				{
					xlsDataValidationCollection = (DataValidationCollection)base.Clone(parent);
					xlsDataValidationCollection.ᜀ = (spr\u22CB)spr\u1CD3.ᜀ(this.ᜀ);
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_71;
							default:
								if (false)
								{
								}
								goto IL_118;
							}
							break;
						case 1:
							return xlsDataValidationCollection;
						case 2:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 6;
								continue;
							}
							List<XlsValidation> innerList;
							XlsValidation xlsValidation = innerList[num2];
							xlsDataValidationCollection.ᜂ.Add(xlsValidation.DVRecord, xlsValidation);
							num2++;
							num = 4;
							continue;
						}
						case 3:
							xlsDataValidationCollection.ᜄ = this.ᜄ;
							xlsDataValidationCollection.ᜃ = spr\u1CD3.ᜀ(this.ᜃ);
							num = 1;
							continue;
						case 4:
							goto IL_118;
						case 5:
						{
							if (this.ᜄ)
							{
								goto IL_71;
							}
							List<XlsValidation> innerList = xlsDataValidationCollection.InnerList;
							int num2 = 0;
							int count = innerList.Count;
							num = 0;
							continue;
						}
						case 6:
							return xlsDataValidationCollection;
						}
						break;
						IL_71:
						if (true)
						{
						}
						num = 3;
						continue;
						IL_118:
						num = 2;
					}
				}
				return xlsDataValidationCollection;
			}
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00021B0C File Offset: 0x00020B0C
		protected internal XlsValidation FindByCellIndex(long iCellIndex)
		{
			switch (0)
			{
			default:
			{
				XlsValidation xlsValidation;
				for (;;)
				{
					IL_0E:
					for (;;)
					{
						List<XlsValidation> innerList = base.InnerList;
						int num = 7;
						for (;;)
						{
							int num2;
							int count;
							switch (num)
							{
							case 0:
								if (xlsValidation.ContainsCell(iCellIndex))
								{
									num = 1;
									continue;
								}
								num2++;
								num = 3;
								continue;
							case 1:
								return xlsValidation;
							case 2:
								goto IL_CE;
							case 3:
								goto IL_CE;
							case 4:
								goto IL_113;
							case 5:
								if (true)
								{
								}
								goto IL_115;
							case 6:
							{
								int num3 = 0;
								this.ᜀ(this.ᜃ, ref num3, this.ᜃ.Count);
								num = 5;
								continue;
							}
							case 7:
								if (this.ᜄ)
								{
									num = 6;
									continue;
								}
								goto IL_115;
							case 8:
								if (num2 >= count)
								{
									num = 4;
									continue;
								}
								xlsValidation = innerList[num2];
								num = 0;
								continue;
							}
							break;
							IL_CE:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_0E;
							}
							if (false)
							{
							}
							num = 8;
							continue;
							IL_115:
							num2 = 0;
							count = base.Count;
							num = 2;
						}
					}
				}
				return xlsValidation;
				IL_113:
				return null;
			}
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00021C4C File Offset: 0x00020C4C
		public void UpdateNamedRangeIndexes(int[] arrNewIndex)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num3;
					int count2;
					switch (num)
					{
					case 0:
						goto IL_65;
					case 2:
						goto IL_B2;
					case 3:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 6;
							continue;
						}
						sprᡣ sprᡣ = (sprᡣ)this.ᜃ[num2];
						FormulaUtil formulaUtil;
						formulaUtil.ᜁ(sprᡣ.\u1713(), arrNewIndex);
						formulaUtil.ᜁ(sprᡣ.\u1714(), arrNewIndex);
						num2++;
						num = 9;
						continue;
					}
					case 4:
					{
						FormulaUtil formulaUtil = this.Workbook.FormulaUtil;
						int num2 = 0;
						int count = this.ᜃ.Count;
						num = 2;
						continue;
					}
					case 5:
						goto IL_F8;
					case 6:
						goto IL_6A;
					case 7:
						if (this.ᜄ)
						{
							num = 4;
							continue;
						}
						goto IL_6A;
					case 8:
					{
						IL_104:
						if (num3 >= count2)
						{
							num = 10;
							continue;
						}
						XlsValidation xlsValidation = base.InnerList[num3];
						xlsValidation.UpdateNamedRangeIndexes(arrNewIndex);
						this.ᜂ.Add(xlsValidation.DVRecord, xlsValidation);
						num3++;
						num = 11;
						continue;
					}
					case 9:
						goto IL_B2;
					case 10:
						return;
					case 11:
						goto IL_F8;
					}
					if (arrNewIndex == null)
					{
						num = 0;
						continue;
					}
					num = 7;
					continue;
					IL_6A:
					this.ᜂ.Clear();
					num3 = 0;
					count2 = base.Count;
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_104;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					IL_B2:
					num = 3;
					continue;
					IL_F8:
					num = 8;
				}
				IL_65:
				throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似焾⑀㑂ౄ⥆ⵈ⹊㕌", a_));
			}
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00021E40 File Offset: 0x00020E40
		public void UpdateNamedRangeIndexes(IDictionary<int, int> dicNewIndex)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_8E;
					case 1:
						return;
					case 2:
						goto IL_8E;
					case 3:
					{
						int count;
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						goto IL_D1;
					}
					case 4:
					{
						if (this.ᜄ)
						{
							num = 7;
							continue;
						}
						this.ᜂ.Clear();
						num2 = 0;
						int count = base.Count;
						num = 5;
						continue;
					}
					case 5:
						goto IL_125;
					case 6:
					{
						int num3;
						int count2;
						if (num3 >= count2)
						{
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D1;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							sprᡣ sprᡣ = (sprᡣ)this.ᜃ[num3];
							FormulaUtil formulaUtil;
							formulaUtil.ᜀ(sprᡣ.\u1713(), dicNewIndex);
							formulaUtil.ᜀ(sprᡣ.\u1714(), dicNewIndex);
							num3++;
							num = 2;
							continue;
						}
						}
						break;
					}
					case 7:
					{
						FormulaUtil formulaUtil = this.Workbook.FormulaUtil;
						int num3 = 0;
						int count2 = this.ᜃ.Count;
						num = 0;
						continue;
					}
					case 8:
						goto IL_125;
					case 9:
						return;
					case 11:
						goto IL_65;
					}
					if (dicNewIndex == null)
					{
						num = 11;
						continue;
					}
					num = 4;
					continue;
					IL_8E:
					num = 6;
					continue;
					IL_D1:
					XlsValidation xlsValidation = base.InnerList[num2];
					xlsValidation.UpdateNamedRangeIndexes(dicNewIndex);
					this.ᜂ.Add(xlsValidation.DVRecord, xlsValidation);
					num2++;
					num = 8;
					continue;
					IL_125:
					num = 3;
				}
				IL_65:
				throw new ArgumentNullException(RecordTableEnumerator.b("␿⭁❃ࡅⵇ㵉Ջ⁍㑏㝑ⱓ", a_));
			}
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00022034 File Offset: 0x00021034
		internal new XlsValidation ᜁ(sprᡣ A_0)
		{
			int a_ = 16;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2 = 0;
					this.ᜀ(this.ᜃ, ref num2, this.ᜃ.Count);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AA;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 1:
					goto IL_4A;
				case 2:
					if (this.ᜄ)
					{
						num = 0;
						continue;
					}
					goto IL_BE;
				case 3:
					goto IL_8B;
				case 4:
					if (true)
					{
					}
					break;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			IL_4A:
			goto IL_AA;
			IL_8B:
			goto IL_BE;
			IL_AA:
			throw new ArgumentNullException(RecordTableEnumerator.b("≅㹇", a_));
			IL_BE:
			return this.ᜀ(A_0);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00022108 File Offset: 0x00021108
		public void MarkUsedReferences(bool[] usedItems)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_27:
					List<XlsValidation> innerList;
					int num;
					int count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_A7:
						goto IL_70;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						innerList = base.InnerList;
						num = 0;
						count = innerList.Count;
						num2 = 1;
						break;
					}
					for (;;)
					{
						IL_10:
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							XlsValidation xlsValidation = innerList[num];
							xlsValidation.MarkUsedReferences(usedItems);
							num++;
							num2 = 3;
							continue;
						}
						case 1:
							goto IL_6E;
						case 2:
							return;
						case 3:
							goto IL_A7;
						}
						goto IL_27;
					}
					IL_6E:
					IL_70:
					num2 = 0;
					goto IL_10;
				}
				return;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000221C0 File Offset: 0x000211C0
		public void UpdateReferenceIndexes(int[] arrUpdatedIndexes)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_27:
					List<XlsValidation> innerList;
					int num;
					int count;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_A7:
						goto IL_70;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						innerList = base.InnerList;
						num = 0;
						count = innerList.Count;
						num2 = 0;
						break;
					}
					for (;;)
					{
						IL_10:
						switch (num2)
						{
						case 0:
							goto IL_6E;
						case 1:
							goto IL_A7;
						case 2:
							return;
						case 3:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							XlsValidation xlsValidation = innerList[num];
							xlsValidation.UpdateReferenceIndexes(arrUpdatedIndexes);
							num++;
							num2 = 1;
							continue;
						}
						}
						goto IL_27;
					}
					IL_6E:
					IL_70:
					num2 = 3;
					goto IL_10;
				}
				return;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00022278 File Offset: 0x00021278
		private new void ᜀ(IList A_0, ref int A_1, int A_2)
		{
			int a_ = 9;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_FF;
				case 1:
				{
					uint num2;
					if ((ulong)num2 >= (ulong)((long)A_2))
					{
						num = 0;
						continue;
					}
					sprᡣ sprᡣ = A_0[A_1] as sprᡣ;
					num = 3;
					continue;
				}
				case 3:
				{
					if (true)
					{
					}
					sprᡣ sprᡣ;
					if (sprᡣ == null)
					{
						num = 4;
						continue;
					}
					this.ᜀ(sprᡣ);
					uint num2;
					num2 += 1U;
					A_1++;
					num = 6;
					continue;
				}
				case 4:
					goto IL_9E;
				case 5:
					if (A_0.Count <= A_2 + A_1)
					{
						uint num2 = 0U;
						num = 9;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_143;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 6:
					goto IL_E0;
				case 7:
					goto IL_56;
				case 8:
					goto IL_DE;
				case 9:
					goto IL_E0;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 5;
				continue;
				IL_E0:
				num = 1;
			}
			IL_56:
			throw new ArgumentNullException(RecordTableEnumerator.b("帾㍀ㅂᝄ≆⩈⑊㽌⭎≐", a_));
			IL_9E:
			throw new ArgumentNullException(RecordTableEnumerator.b("焾⹀㝂敄≆❈⑊㡌⡎㥐獒ᅔŖ୘㹚㹜ぞ፠ݢᙤ", a_));
			IL_DE:
			goto IL_143;
			IL_FF:
			this.ᜃ = null;
			this.ᜄ = false;
			return;
			IL_143:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾ɀⱂい⥆㵈", a_));
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000223EC File Offset: 0x000213EC
		private new XlsValidation ᜀ(sprᡣ A_0)
		{
			int a_ = 4;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_58;
				case 2:
					if (!this.ᜂ.ContainsKey(A_0))
					{
						num = 3;
						continue;
					}
					goto IL_B5;
				case 3:
					goto IL_9F;
				}
				if (A_0 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9F;
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
				}
				else
				{
					num = 2;
				}
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("帹䨻", a_));
			IL_9F:
			XlsValidation xlsValidation = base.AppImplementation.ᜀ(this, A_0);
			this.ᜂ.Add(A_0, xlsValidation);
			base.Add(xlsValidation);
			return xlsValidation;
			IL_B5:
			return this.ᜂ[A_0];
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x000224C8 File Offset: 0x000214C8
		internal List<BiffRecordRaw> DataValidations
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
				return this.ᜃ;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0002250C File Offset: 0x0002150C
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00022554 File Offset: 0x00021554
		public int PromptBoxHPosition
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
				return this.ᜀ.ᜈ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0002259C File Offset: 0x0002159C
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x000225E4 File Offset: 0x000215E4
		public int PromptBoxVPosition
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
				return this.ᜀ.ᜇ();
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
				this.ᜀ.ᜁ(value);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0002262C File Offset: 0x0002162C
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00022674 File Offset: 0x00021674
		public bool IsPromptBoxVisible
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
				return this.ᜀ.ᜄ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000226BC File Offset: 0x000216BC
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00022704 File Offset: 0x00021704
		public bool IsPromptBoxPositionFixed
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
				return this.ᜀ.ᜆ();
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
				this.ᜀ.ᜂ(value);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0002274C File Offset: 0x0002174C
		public XlsWorkbook Workbook
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
				return this.ᜁ.Workbook;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00022794 File Offset: 0x00021794
		public XlsWorksheet Worksheet
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
				return this.ᜁ.Worksheet;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000227DC File Offset: 0x000217DC
		public XlsDataValidationTable ParentTable
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
				return this.ᜁ;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00022820 File Offset: 0x00021820
		internal spr\u22CB Record
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000149 RID: 329
		public new XlsValidation this[int index]
		{
			get
			{
				int a_ = 15;
				int num = 0;
				for (;;)
				{
					IL_13:
					switch (num)
					{
					case 1:
						goto IL_A3;
					case 2:
						if (index > base.Count)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						goto IL_A5;
					case 3:
						num = 2;
						continue;
					}
					while (index >= 0)
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
							goto IL_13;
						}
					}
					break;
				}
				IL_5D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⱄ⥆ⵈ⹊㕌", a_), RecordTableEnumerator.b("ፄ♆╈㹊⡌潎㉐㉒㭔㥖㙘⽚絜㵞Ѡ䍢।ɦᩨᡪ䵬᭮ᥰቲ᭴坶䥸孺ቼൾꆀﾊﶎ놐ﶔ뮚\ude9c풠춢톤覦", a_));
				IL_A3:
				goto IL_5D;
				IL_A5:
				return base.List[index];
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00022924 File Offset: 0x00021924
		public int ShapesCount
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
						int num2 = 2;
						for (;;)
						{
							int num3;
							switch (num2)
							{
							case 0:
							{
								int count;
								if (num3 >= count)
								{
									num2 = 4;
									continue;
								}
								sprᡣ sprᡣ = this.ᜃ[num3] as sprᡣ;
								num2 = 8;
								continue;
							}
							case 1:
								return num;
							case 2:
							{
								if (!this.ᜄ)
								{
									if (true)
									{
									}
									num2 = 13;
									continue;
								}
								num3 = 0;
								int count = this.ᜃ.Count;
								num2 = 7;
								continue;
							}
							case 3:
								goto IL_181;
							case 4:
								return num;
							case 5:
								goto IL_12E;
							case 6:
								goto IL_CE;
							case 7:
								goto IL_12E;
							case 8:
							{
								sprᡣ sprᡣ;
								if (sprᡣ != null)
								{
									num2 = 9;
									continue;
								}
								goto IL_CE;
							}
							case 9:
							{
								sprᡣ sprᡣ;
								num += (int)sprᡣ.ᜌ();
								num2 = 6;
								continue;
							}
							case 10:
							{
								int num4;
								int count2;
								if (num4 >= count2)
								{
									num2 = 11;
									continue;
								}
								num += this[num4].ShapesCount;
								num4++;
								num2 = 3;
								continue;
							}
							case 11:
								goto IL_173;
							case 12:
								goto IL_181;
							case 13:
							{
								int num4 = 0;
								int count2 = base.Count;
								num2 = 12;
								continue;
							}
							}
							break;
							IL_CE:
							num3++;
							num2 = 5;
							continue;
							IL_12E:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_173:
								num2 = 1;
								continue;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							IL_181:
							num2 = 10;
						}
					}
					return num;
				}
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00022AD4 File Offset: 0x00021AD4
		internal new void ᜀ(XlsDataValidationCollection A_0, int A_1, int A_2, int A_3, int A_4, int A_5, int A_6)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int a_ = A_3 - A_1;
				int a_2 = A_4 - A_2;
				this.ᜀ();
				A_0.ᜀ();
				using (Dictionary<sprᡣ, XlsValidation>.Enumerator enumerator = A_0.ᜂ.GetEnumerator())
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_EA;
						case 2:
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_DC;
							default:
							{
								if (false)
								{
								}
								KeyValuePair<sprᡣ, XlsValidation> keyValuePair = enumerator.Current;
								XlsValidation value = keyValuePair.Value;
								XlsValidation dv = value.ᜀ(this, A_1, A_2, a_, a_2, A_5, A_6);
								this.Add(dv);
								num = 3;
								continue;
							}
							}
							break;
						case 4:
							goto IL_DC;
						}
						IL_BE:
						num = 2;
						continue;
						goto IL_BE;
						IL_DC:
						num = 0;
					}
					IL_EA:;
				}
				return;
			}
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00022BEC File Offset: 0x00021BEC
		private new void ᜀ()
		{
			int num = 2;
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
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						if (true)
						{
						}
						int num2 = 0;
						this.ᜀ(this.ᜃ, ref num2, this.ᜃ.Count);
						num = 0;
						continue;
					}
					}
					break;
				}
				if (!this.ᜄ)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x040000A0 RID: 160
		private new spr\u22CB ᜀ;

		// Token: 0x040000A1 RID: 161
		private new XlsDataValidationTable ᜁ;

		// Token: 0x040000A2 RID: 162
		private new Dictionary<sprᡣ, XlsValidation> ᜂ = new Dictionary<sprᡣ, XlsValidation>();

		// Token: 0x040000A3 RID: 163
		private List<BiffRecordRaw> ᜃ;

		// Token: 0x040000A4 RID: 164
		private byte \u25D8\u00A5\u009B\u0086;

		// Token: 0x040000A5 RID: 165
		private bool ᜄ;
	}
}
