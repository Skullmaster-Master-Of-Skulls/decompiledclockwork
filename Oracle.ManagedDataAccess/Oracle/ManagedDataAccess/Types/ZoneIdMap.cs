using System;
using System.Collections;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000260 RID: 608
	internal class ZoneIdMap
	{
		// Token: 0x0600187C RID: 6268 RVA: 0x001025A8 File Offset: 0x001007A8
		static ZoneIdMap()
		{
			ZoneIdMap.zoneid.Add("Afghanistan Standard Time", 240);
			ZoneIdMap.zoneid.Add("Alaskan Standard Time", 106);
			ZoneIdMap.zoneid.Add("Arab Standard Time", 288);
			ZoneIdMap.zoneid.Add("Arabian Standard Time", 298);
			ZoneIdMap.zoneid.Add("Arabic Standard Time", 265);
			ZoneIdMap.zoneid.Add("Argentina Standard Time", 175);
			ZoneIdMap.zoneid.Add("Armenian Standard Time", 241);
			ZoneIdMap.zoneid.Add("Atlantic Standard Time", 120);
			ZoneIdMap.zoneid.Add("AUS Central Standard Time", 345);
			ZoneIdMap.zoneid.Add("AUS Eastern Standard Time", 352);
			ZoneIdMap.zoneid.Add("Azerbaijan Standard Time", 242);
			ZoneIdMap.zoneid.Add("Azores Standard Time", 336);
			ZoneIdMap.zoneid.Add("Bangladesh Standard Time", 756);
			ZoneIdMap.zoneid.Add("Canada Central Standard Time", 127);
			ZoneIdMap.zoneid.Add("Cape Verde Standard Time", 339);
			ZoneIdMap.zoneid.Add("Caucasus Standard Time", 241);
			ZoneIdMap.zoneid.Add("Cen. Australia Standard Time", 349);
			ZoneIdMap.zoneid.Add("Central America Standard Time", 159);
			ZoneIdMap.zoneid.Add("Central Asia Standard Time", 269);
			ZoneIdMap.zoneid.Add("Central Brazilian Standard Time", 189);
			ZoneIdMap.zoneid.Add("Central Europe Standard Time", 386);
			ZoneIdMap.zoneid.Add("Central European Standard Time", 398);
			ZoneIdMap.zoneid.Add("Central Pacific Standard Time", 481);
			ZoneIdMap.zoneid.Add("Central Standard Time (Mexico) ", 141);
			ZoneIdMap.zoneid.Add("Central Standard Time", 101);
			ZoneIdMap.zoneid.Add("China Standard Time", 250);
			ZoneIdMap.zoneid.Add("Dateline Standard Time", 27);
			ZoneIdMap.zoneid.Add("E. Africa Standard Time", 53);
			ZoneIdMap.zoneid.Add("E. Australia Standard Time", 347);
			ZoneIdMap.zoneid.Add("E. Europe Standard Time", 375);
			ZoneIdMap.zoneid.Add("E. South America Standard Time", 188);
			ZoneIdMap.zoneid.Add("Eastern Standard Time", 100);
			ZoneIdMap.zoneid.Add("Egypt Standard Time", 44);
			ZoneIdMap.zoneid.Add("Ekaterinburg Standard Time", 303);
			ZoneIdMap.zoneid.Add("Fiji Standard Time", 454);
			ZoneIdMap.zoneid.Add("FLE Standard Time", 408);
			ZoneIdMap.zoneid.Add("Georgian Standard Time", 258);
			ZoneIdMap.zoneid.Add("GMT Standard Time", 369);
			ZoneIdMap.zoneid.Add("Greenland Standard Time", 207);
			ZoneIdMap.zoneid.Add("Greenwich Standard Time", 334);
			ZoneIdMap.zoneid.Add("GTB Standard Time", 407);
			ZoneIdMap.zoneid.Add("Hawaiian Standard Time", 450);
			ZoneIdMap.zoneid.Add("India Standard Time", 260);
			ZoneIdMap.zoneid.Add("Iran Standard Time", 264);
			ZoneIdMap.zoneid.Add("Israel Standard Time", 266);
			ZoneIdMap.zoneid.Add("Jordan Standard Time", 268);
			ZoneIdMap.zoneid.Add("Kamchatka Standard Time", 311);
			ZoneIdMap.zoneid.Add("Korea Standard Time", 273);
			ZoneIdMap.zoneid.Add("Magadan Standard Time", 310);
			ZoneIdMap.zoneid.Add("Mauritius Standard Time", 443);
			ZoneIdMap.zoneid.Add("Mexico Standard Time 2 ", 142);
			ZoneIdMap.zoneid.Add("Mexico Standard Time", 141);
			ZoneIdMap.zoneid.Add("Mid-Atlantic Standard Time", 17);
			ZoneIdMap.zoneid.Add("Middle East Standard Time", 277);
			ZoneIdMap.zoneid.Add("Montevideo Standard Time", 204);
			ZoneIdMap.zoneid.Add("Morocco Standard Time", 61);
			ZoneIdMap.zoneid.Add("Mountain Standard Time (Mexico) ", 142);
			ZoneIdMap.zoneid.Add("Mountain Standard Time", 102);
			ZoneIdMap.zoneid.Add("Myanmar Standard Time", 247);
			ZoneIdMap.zoneid.Add("N. Central Asia Standard Time", 305);
			ZoneIdMap.zoneid.Add("Namibia Standard Time", 64);
			ZoneIdMap.zoneid.Add("Nepal Standard Time", 282);
			ZoneIdMap.zoneid.Add("New Zealand Standard Time", 471);
			ZoneIdMap.zoneid.Add("Newfoundland Standard Time", 118);
			ZoneIdMap.zoneid.Add("North Asia East Standard Time", 307);
			ZoneIdMap.zoneid.Add("North Asia Standard Time", 306);
			ZoneIdMap.zoneid.Add("Pacific SA Standard Time", 194);
			ZoneIdMap.zoneid.Add("Pacific Standard Time (Mexico) ", 96);
			ZoneIdMap.zoneid.Add("Pacific Standard Time", 103);
			ZoneIdMap.zoneid.Add("Pakistan Standard Time", 284);
			ZoneIdMap.zoneid.Add("Paraguay Standard Time", 200);
			ZoneIdMap.zoneid.Add("Romance Standard Time", 382);
			ZoneIdMap.zoneid.Add("Russian Standard Time", 402);
			ZoneIdMap.zoneid.Add("SA Eastern Standard Time", 198);
			ZoneIdMap.zoneid.Add("SA Pacific Standard Time", 195);
			ZoneIdMap.zoneid.Add("SA Western Standard Time", 182);
			ZoneIdMap.zoneid.Add("Samoa Standard Time", 479);
			ZoneIdMap.zoneid.Add("SE Asia Standard Time", 296);
			ZoneIdMap.zoneid.Add("Singapore Standard Time", 292);
			ZoneIdMap.zoneid.Add("South Africa Standard Time", 72);
			ZoneIdMap.zoneid.Add("Sri Lanka Standard Time", 293);
			ZoneIdMap.zoneid.Add("Syria Standard Time", 294);
			ZoneIdMap.zoneid.Add("Taipei Standard Time", 255);
			ZoneIdMap.zoneid.Add("Tasmania Standard Time", 350);
			ZoneIdMap.zoneid.Add("Tokyo Standard Time", 267);
			ZoneIdMap.zoneid.Add("Tonga Standard Time", 483);
			ZoneIdMap.zoneid.Add("Ulaanbaatar Standard Time", 281);
			ZoneIdMap.zoneid.Add("US Eastern Standard Time", 111);
			ZoneIdMap.zoneid.Add("US Mountain Standard Time", 109);
			ZoneIdMap.zoneid.Add("UTC", 1);
			ZoneIdMap.zoneid.Add("UTC+12", 4);
			ZoneIdMap.zoneid.Add("UTC-02", 17);
			ZoneIdMap.zoneid.Add("UTC-11", 26);
			ZoneIdMap.zoneid.Add("Venezuela Standard Time", 205);
			ZoneIdMap.zoneid.Add("Vladivostok Standard Time", 309);
			ZoneIdMap.zoneid.Add("W. Australia Standard Time", 346);
			ZoneIdMap.zoneid.Add("W. Central Africa Standard Time", 66);
			ZoneIdMap.zoneid.Add("W. Europe Standard Time", 383);
			ZoneIdMap.zoneid.Add("West Asia Standard Time", 300);
			ZoneIdMap.zoneid.Add("West Pacific Standard Time", 476);
			ZoneIdMap.zoneid.Add("Yakutsk Standard Time", 308);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00102F5C File Offset: 0x0010115C
		internal static int GetRegionID(string regionName)
		{
			return (int)ZoneIdMap.zoneid[regionName.Trim()];
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00102F74 File Offset: 0x00101174
		internal static string GetRegionName(int regionId)
		{
			IDictionaryEnumerator enumerator = ZoneIdMap.zoneid.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if ((int)enumerator.Value == regionId)
				{
					return (string)enumerator.Key;
				}
			}
			return null;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00102FB4 File Offset: 0x001011B4
		internal static bool isValidID(int value)
		{
			return ZoneIdMap.zoneid.ContainsValue(value);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00102FC8 File Offset: 0x001011C8
		internal static bool isValidRegion(string key)
		{
			return ZoneIdMap.zoneid.ContainsKey(key);
		}

		// Token: 0x04001AE1 RID: 6881
		internal const int INV_ZONEID = -1;

		// Token: 0x04001AE2 RID: 6882
		internal static Hashtable zoneid = new Hashtable(544);
	}
}
