using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x020001A1 RID: 417
	public class XlsChartWrappedFrameFormat : XlsChartFrameFormat
	{
		// Token: 0x06001503 RID: 5379 RVA: 0x000C7A80 File Offset: 0x000C6A80
		internal XlsChartWrappedFrameFormat(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000C7A98 File Offset: 0x000C6A98
		internal override bool CheckBegin(BiffRecordRaw record)
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
			record = this.UnwrapRecord(record);
			return base.CheckBegin(record);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000C7AE4 File Offset: 0x000C6AE4
		internal override void ParseRecord(BiffRecordRaw record, ref int iBeginCounter)
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
			record = this.UnwrapRecord(record);
			base.ParseRecord(record, ref iBeginCounter);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x000C7B30 File Offset: 0x000C6B30
		internal override BiffRecordRaw UnwrapRecord(BiffRecordRaw record)
		{
			int a_ = 7;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8F;
				case 2:
					goto IL_34;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (false)
						{
						}
						if (record.TypeCode == TBIFFRecord.ChartWrapper)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						return record;
					}
					break;
				}
				if (record == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("似娾≀ⱂ㝄⍆", a_));
			IL_8F:
			spr\u23F0 spr_u23F = (spr\u23F0)record;
			return spr_u23F.ᜀ();
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x000C7BE4 File Offset: 0x000C6BE4
		internal override void SerializeRecord(IList<IRecordStorage> list, BiffRecordRaw record)
		{
			int a_ = 10;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_83;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (false)
						{
						}
						if (record == null)
						{
							num = 0;
							continue;
						}
						goto IL_A1;
					}
					break;
				case 3:
					goto IL_34;
				}
				if (list == null)
				{
					num = 3;
				}
				else
				{
					num = 2;
				}
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⰿ⭁㝃㉅", a_));
			IL_83:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁❃⥅㩇⹉", a_));
			IL_A1:
			spr\u23F0 spr_u23F = (spr\u23F0)spr\u175E.ᜀ(TBIFFRecord.ChartWrapper);
			spr_u23F.ᜀ(record);
			list.Add((BiffRecordRaw)spr_u23F.ᜁ());
		}
	}
}
