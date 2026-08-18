using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Parser.Biff_Records;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000031 RID: 49
	public class XlsWorksheetConditionalFormats : CollectionExtended<XlsConditionalFormats>, ICloneParent
	{
		// Token: 0x06000392 RID: 914 RVA: 0x00020300 File Offset: 0x0001F300
		internal XlsWorksheetConditionalFormats(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00020320 File Offset: 0x0001F320
		public XlsConditionalFormats Find(Rectangle[] arrRanges)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_EF:
				goto IL_BF;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					num = 4;
					break;
				}
				break;
			}
			for (;;)
			{
				IL_35:
				switch (num)
				{
				case 0:
				{
					XlsConditionalFormats xlsConditionalFormats;
					return xlsConditionalFormats;
				}
				case 1:
					goto IL_DB;
				case 2:
					goto IL_EF;
				case 3:
				{
					XlsConditionalFormats xlsConditionalFormats;
					if (xlsConditionalFormats.Contains(arrRanges))
					{
						num = 0;
						continue;
					}
					int num2;
					num2++;
					num = 2;
					continue;
				}
				case 5:
					goto IL_110;
				case 6:
				{
					int num3;
					if (num3 == 0)
					{
						num = 9;
						continue;
					}
					int num2 = 0;
					int count = base.Count;
					num = 5;
					continue;
				}
				case 7:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					XlsConditionalFormats xlsConditionalFormats = base[num2];
					num = 3;
					continue;
				}
				case 8:
					goto IL_70;
				case 9:
					goto IL_BD;
				}
				if (arrRanges == null)
				{
					num = 8;
				}
				else
				{
					int num3 = arrRanges.Length;
					num = 6;
				}
			}
			IL_70:
			if (true)
			{
			}
			return null;
			IL_BD:
			return null;
			IL_DB:
			return null;
			IL_110:
			IL_BF:
			num = 7;
			goto IL_35;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00020444 File Offset: 0x0001F444
		public new XlsConditionalFormats Contains(XlsConditionalFormats formats)
		{
			int a_ = 5;
			if (formats == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("崺刼䴾ⱀ≂ㅄ㑆", a_));
				}
			}
			XlsConditionalFormats result;
			this.ᜀ.TryGetValue(formats, out result);
			return result;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000204B4 File Offset: 0x0001F4B4
		public new XlsConditionalFormats Add(XlsConditionalFormats formats)
		{
			int a_ = 3;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					XlsConditionalFormats xlsConditionalFormats;
					if (this.ᜀ.TryGetValue(formats, out xlsConditionalFormats))
					{
						num = 2;
						continue;
					}
					base.Add(formats);
					this.ᜀ.Add(formats, formats);
					num = 3;
					continue;
				}
				case 1:
					goto IL_4E;
				case 2:
				{
					if (true)
					{
					}
					XlsConditionalFormats xlsConditionalFormats;
					xlsConditionalFormats.AddCells(formats);
					num = 5;
					continue;
				}
				case 3:
					goto IL_50;
				case 4:
				{
					XlsConditionalFormats xlsConditionalFormats;
					if (xlsConditionalFormats == null)
					{
						num = 7;
						continue;
					}
					return xlsConditionalFormats;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4E;
					default:
						if (false)
						{
						}
						goto IL_50;
					}
					break;
				case 7:
					return formats;
				}
				if (formats == null)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
				IL_50:
				num = 4;
			}
			IL_4E:
			throw new ArgumentNullException(RecordTableEnumerator.b("弸吺似刾⁀㝂㙄", a_));
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000205C4 File Offset: 0x0001F5C4
		public void Remove(Rectangle[] arrRanges)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					int num2;
					XlsConditionalFormats xlsConditionalFormats;
					int num3;
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						return;
					case 1:
						num = 15;
						continue;
					case 2:
					{
						List<XlsConditionalFormats> innerList;
						XlsConditionalFormats value = innerList[num2];
						innerList[num2] = xlsConditionalFormats;
						innerList[num3] = value;
						num = 5;
						continue;
					}
					case 3:
						goto IL_13E;
					case 4:
					{
						List<XlsConditionalFormats> innerList;
						innerList.RemoveRange(base.Count - num4, num4);
						num = 0;
						continue;
					}
					case 5:
						goto IL_1A4;
					case 6:
						goto IL_FD;
					case 8:
						goto IL_72;
					case 9:
						goto IL_FD;
					case 10:
						goto IL_FB;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_13E;
						default:
							if (false)
							{
							}
							if (num2 != num3)
							{
								num = 2;
								continue;
							}
							goto IL_1A4;
						}
						break;
					case 12:
						if (num4 > 0)
						{
							num = 4;
							continue;
						}
						return;
					case 13:
						if (num3 >= num5)
						{
							num = 14;
							continue;
						}
						xlsConditionalFormats = base[num3];
						xlsConditionalFormats.Remove(arrRanges);
						num = 16;
						continue;
					case 14:
						num = 12;
						continue;
					case 15:
					{
						if (arrRanges.Length == 0)
						{
							num = 10;
							continue;
						}
						num4 = 0;
						List<XlsConditionalFormats> innerList = base.InnerList;
						num3 = 0;
						num5 = base.Count;
						num = 9;
						continue;
					}
					case 16:
						if (xlsConditionalFormats.IsEmpty)
						{
							num = 3;
							continue;
						}
						goto IL_72;
					}
					if (arrRanges != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_72:
					num3++;
					num = 6;
					continue;
					IL_FD:
					num = 13;
					continue;
					IL_13E:
					num2 = num5 - 1;
					num = 11;
					continue;
					IL_1A4:
					num4++;
					num5--;
					num3--;
					this.ᜀ.Remove(xlsConditionalFormats);
					if (true)
					{
					}
					num = 8;
				}
				return;
				IL_FB:
				return;
			}
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000207E4 File Offset: 0x0001F7E4
		public void CopyFrom(XlsWorksheetConditionalFormats srcFormats)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 0:
						goto IL_BF;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BF;
						default:
							if (false)
							{
							}
							goto IL_D5;
						}
						break;
					case 2:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						XlsConditionalFormats a_2 = srcFormats[num2];
						XlsConditionalFormats formats = new XlsConditionalFormats(base.ReservedHandle, this, a_2);
						this.Add(formats);
						num2++;
						num = 1;
						continue;
					}
					case 3:
						return;
					case 4:
						goto IL_4D;
					}
					if (srcFormats == null)
					{
						num = 4;
						continue;
					}
					num2 = 0;
					count = srcFormats.Count;
					if (true)
					{
					}
					num = 0;
					continue;
					IL_D5:
					num = 2;
					continue;
					IL_BF:
					goto IL_D5;
				}
				IL_4D:
				throw new ArgumentNullException(RecordTableEnumerator.b("䬷䠹弻砽⼿ぁ⥃❅㱇㥉", a_));
			}
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000208E8 File Offset: 0x0001F8E8
		public override object Clone(object parent)
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
			XlsWorksheetConditionalFormats xlsWorksheetConditionalFormats = (XlsWorksheetConditionalFormats)base.Clone(parent);
			xlsWorksheetConditionalFormats.ᜀ = spr\u1CD3.ᜀ<XlsConditionalFormats, XlsConditionalFormats>(this.ᜀ, xlsWorksheetConditionalFormats);
			return xlsWorksheetConditionalFormats;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00020944 File Offset: 0x0001F944
		internal new void ᜀ(XlsConditionalFormats A_0)
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
			base.Remove(A_0);
			this.ᜀ.Remove(A_0);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00020994 File Offset: 0x0001F994
		internal new void ᜀ(bool[] A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					List<XlsConditionalFormats> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_42;
						case 1:
						{
							if (num >= count)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_88;
								}
								if (true)
								{
								}
								if (false)
								{
								}
								num2 = 2;
								continue;
							}
							IL_88:
							XlsConditionalFormats xlsConditionalFormats = innerList[num];
							xlsConditionalFormats.ᜀ(A_0);
							num++;
							num2 = 3;
							continue;
						}
						case 2:
							return;
						case 3:
							goto IL_42;
						}
						break;
						IL_42:
						num2 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00020A4C File Offset: 0x0001FA4C
		internal new void ᜀ(int[] A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					List<XlsConditionalFormats> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_4A;
						case 1:
							goto IL_4A;
						case 2:
						{
							if (num >= count)
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
									num2 = 3;
									continue;
								}
							}
							XlsConditionalFormats xlsConditionalFormats = innerList[num];
							xlsConditionalFormats.ᜀ(A_0);
							num++;
							num2 = 1;
							continue;
						}
						case 3:
							return;
						}
						break;
						IL_4A:
						num2 = 2;
					}
				}
				return;
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00020B04 File Offset: 0x0001FB04
		public void SerializeDataToList(RecordArrayList records)
		{
			int a_ = 6;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A6;
				case 1:
					goto IL_B9;
				case 3:
					return;
				case 4:
					goto IL_A6;
				case 5:
					goto IL_3C;
				}
				if (records == null)
				{
					num = 5;
					continue;
				}
				int num2;
				int count;
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
					count = base.Count;
					num = 0;
					continue;
				}
				IL_B9:
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				base[num2].SerializeDataToList(records);
				num2++;
				num = 4;
				continue;
				IL_A6:
				if (true)
				{
				}
				num = 1;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00020BDC File Offset: 0x0001FBDC
		internal new void ᜀ(int A_0, int A_1, Rectangle A_2, int A_3, Rectangle A_4)
		{
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_33;
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
							if (num < count)
							{
								base[num].ᜀ(A_0, A_1, A_2, A_3, A_4);
								num++;
								num2 = 2;
								continue;
							}
							break;
						}
						num2 = 3;
						continue;
					case 2:
						goto IL_33;
					case 3:
						return;
					}
					break;
					IL_33:
					num2 = 1;
				}
			}
		}

		// Token: 0x0400009C RID: 156
		private byte \u2609\u0082\u0091\u00AF;

		// Token: 0x0400009D RID: 157
		private new Dictionary<XlsConditionalFormats, XlsConditionalFormats> ᜀ = new Dictionary<XlsConditionalFormats, XlsConditionalFormats>();
	}
}
