using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x02000193 RID: 403
	public class XlsChartWrappedTextArea : XlsChartTextArea, spr\u1B6D
	{
		// Token: 0x06001413 RID: 5139 RVA: 0x000C16CC File Offset: 0x000C06CC
		internal XlsChartWrappedTextArea(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x000C16E4 File Offset: 0x000C06E4
		internal XlsChartWrappedTextArea(spr\u1DF5 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1, A_2, ref A_3)
		{
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x000C16FC File Offset: 0x000C06FC
		internal XlsChartWrappedTextArea(spr\u1DF5 A_0, object A_1, ObjectTextLinkType A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000C1714 File Offset: 0x000C0714
		protected override XlsChartFrameFormat CreateFrameFormat()
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
			return new XlsChartWrappedFrameFormat(base.ReservedHandle, this);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x000C175C File Offset: 0x000C075C
		internal override void SerializeRecord(IList<IRecordStorage> records, BiffRecordRaw record)
		{
			int a_ = 4;
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (record == null)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						if (record.TypeCode == TBIFFRecord.ChartDataLabels)
						{
							num = 4;
							continue;
						}
						goto IL_BB;
					case 3:
						goto IL_B9;
					case 4:
						goto IL_6D;
					case 5:
						goto IL_46;
					}
					if (records == null)
					{
						num = 5;
					}
					else
					{
						num = 0;
					}
				}
				IL_BB:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_D1;
				}
			}
			IL_46:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃㕅", a_));
			IL_6D:
			base.SerializeRecord(records, record);
			return;
			IL_B9:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠹夻崽⼿ぁ⁃", a_));
			IL_D1:
			if (false)
			{
			}
			spr\u23F0 spr_u23F = (spr\u23F0)spr\u175E.ᜀ(TBIFFRecord.ChartWrapper);
			spr_u23F.ᜀ((BiffRecordRaw)record.Clone());
			records.Add(spr_u23F);
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x000C1868 File Offset: 0x000C0868
		protected override bool ShouldSerialize
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
				return true;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x000C18A4 File Offset: 0x000C08A4
		private void ᜀ(RecordArrayList A_0, byte[][] A_1)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5D;
					case 1:
						goto IL_CB;
					case 2:
						goto IL_92;
					case 3:
						if (A_0 != null)
						{
							int num2 = 0;
							int num3 = A_1.Length;
							num = 6;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15E;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 4:
						if (true)
						{
						}
						goto IL_D0;
					case 5:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 9;
							continue;
						}
						byte[] array = A_1[num2];
						num = 8;
						continue;
					}
					case 6:
						goto IL_D0;
					case 8:
					{
						byte[] array;
						if (array == null)
						{
							num = 2;
							continue;
						}
						int num4 = array.Length;
						sprᱬ sprᱬ = (sprᱬ)spr\u175E.ᜀ(TBIFFRecord.Unknown);
						sprᱬ.ᜀ((int)BitConverter.ToUInt16(array, 0));
						sprᱬ.ᜀ = new byte[num4];
						sprᱬ.ᜁ(num4);
						array.CopyTo(sprᱬ.ᜀ, 0);
						A_0.ᜀ(sprᱬ);
						int num2;
						num2++;
						goto IL_15E;
					}
					case 9:
						return;
					}
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
					IL_D0:
					num = 5;
					continue;
					IL_15E:
					num = 4;
				}
				IL_5D:
				throw new ArgumentNullException(RecordTableEnumerator.b("嘶䬸䤺格儾⩀ⵂ⩄う❈", a_));
				IL_92:
				throw new ArgumentNullException(RecordTableEnumerator.b("嘶䬸䤺礼帾㕀≂", a_));
				IL_CB:
				throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
			}
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x000C1A54 File Offset: 0x000C0A54
		// Note: this type is marked as 'beforefieldinit'.
		static XlsChartWrappedTextArea()
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
			byte[][] array = new byte[4][];
			array[0] = new byte[]
			{
				80,
				8,
				0,
				0,
				10,
				10,
				3,
				0,
				80,
				8,
				90,
				8,
				97,
				8,
				97,
				8,
				106,
				8,
				107,
				8
			};
			array[1] = new byte[]
			{
				82,
				8,
				0,
				0,
				13,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			byte[][] array2 = array;
			int num = 2;
			byte[] array3 = new byte[12];
			array3[0] = 106;
			array3[1] = 8;
			array2[num] = array3;
			array[3] = new byte[]
			{
				84,
				8,
				0,
				0,
				18,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			XlsChartWrappedTextArea.ᜀ = array;
			XlsChartWrappedTextArea.ᜁ = new byte[][]
			{
				new byte[]
				{
					85,
					8,
					0,
					0,
					18,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				new byte[]
				{
					83,
					8,
					0,
					0,
					13,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				}
			};
		}

		// Token: 0x04000EC8 RID: 3784
		private new static readonly byte[][] ᜀ;

		// Token: 0x04000EC9 RID: 3785
		private byte[] \u25D8\u0090\u008B\u009C;

		// Token: 0x04000ECA RID: 3786
		private int \u2593\u007F\u0087\u007F;

		// Token: 0x04000ECB RID: 3787
		private long[] \u25D8\u009C\u009D\u008F;

		// Token: 0x04000ECC RID: 3788
		private static readonly byte[][] ᜁ;
	}
}
