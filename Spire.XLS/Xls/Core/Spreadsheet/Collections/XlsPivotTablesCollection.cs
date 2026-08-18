using System;
using System.Collections;
using System.Collections.Generic;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.PivotTables;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001E6 RID: 486
	public class XlsPivotTablesCollection : CollectionExtended<object>, ICloneParent
	{
		// Token: 0x06001BB9 RID: 7097 RVA: 0x000EEB00 File Offset: 0x000EDB00
		internal XlsPivotTablesCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000EEB18 File Offset: 0x000EDB18
		public int SerilizeDataFromList(IList data, int pos)
		{
			int a_ = 16;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4C;
				case 1:
					goto IL_CA;
				case 2:
				{
					BiffRecordRaw biffRecordRaw;
					if (biffRecordRaw.TypeCode != TBIFFRecord.PivotViewDefinition)
					{
						num = 4;
						continue;
					}
					XlsPivotTable xlsPivotTable = new XlsPivotTable(base.ReservedHandle, this);
					pos = xlsPivotTable.Parse(data, pos);
					biffRecordRaw = (BiffRecordRaw)data[pos];
					base.List.Add(xlsPivotTable);
					num = 1;
					continue;
				}
				case 3:
					if (true)
					{
					}
					goto IL_CA;
				case 4:
					goto IL_ED;
				case 5:
				{
					if (pos > data.Count - 1)
					{
						num = 9;
						continue;
					}
					BiffRecordRaw biffRecordRaw = (BiffRecordRaw)data[pos];
					num = 3;
					continue;
				}
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return pos;
					default:
						if (false)
						{
						}
						if (pos >= 0)
						{
							num = 8;
							continue;
						}
						goto IL_70;
					}
					break;
				case 8:
					num = 5;
					continue;
				case 9:
					goto IL_171;
				}
				if (data == null)
				{
					num = 0;
					continue;
				}
				num = 7;
				continue;
				IL_CA:
				num = 2;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("≅⥇㹉ⵋ", a_));
			IL_70:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㙅❇㥉", a_), RecordTableEnumerator.b("၅⥇♉㥋⭍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣੥൧ᥩὫ乭ѯᩱᕳᡵ塷䩹屻ώꒃ慎揄뒓ﮙ뺝쒟쎡킣장袧용즫삭힯욱\udcb3颵", a_));
			IL_ED:
			return pos;
			IL_171:
			goto IL_70;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000EEC9C File Offset: 0x000EDC9C
		internal new void ᜀ(RecordArrayList A_0)
		{
			int a_ = 5;
			int num = 0;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 1:
					goto IL_3C;
				case 2:
					return;
				case 3:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					XlsPivotTable xlsPivotTable = (XlsPivotTable)base.InnerList[num2];
					xlsPivotTable.Serialize(A_0);
					num2++;
					num = 4;
					continue;
				}
				case 4:
					goto IL_94;
				case 5:
					goto IL_94;
				}
				IL_31:
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num2 = 0;
				count = base.Count;
				num = 5;
				continue;
				IL_94:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_31;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼尾⹀ㅂ⅄㑆", a_));
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000EED84 File Offset: 0x000EDD84
		public PivotTablesCollection Clone(XlsWorksheet worksheet, Dictionary<string, string> hashWorksheetNames)
		{
			switch (0)
			{
			default:
			{
				PivotTablesCollection pivotTablesCollection;
				for (;;)
				{
					pivotTablesCollection = new PivotTablesCollection((spr\u2158)worksheet.AppImplementation, worksheet);
					XlsWorkbook parentWorkbook = worksheet.ParentWorkbook;
					int num = 0;
					int count = base.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
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
								XlsPivotTable xlsPivotTable = (XlsPivotTable)base[num];
								xlsPivotTable = xlsPivotTable.ᜀ(pivotTablesCollection, hashWorksheetNames);
								pivotTablesCollection.ᜁ(xlsPivotTable);
								num++;
								num2 = 2;
								continue;
							}
							}
							break;
						case 1:
							return pivotTablesCollection;
						case 2:
							goto IL_54;
						case 3:
							goto IL_54;
						}
						break;
						IL_54:
						num2 = 0;
					}
				}
				return pivotTablesCollection;
			}
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x000EEE5C File Offset: 0x000EDE5C
		internal XlsWorksheet ParentWorksheet
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
				return base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet;
			}
		}
	}
}
