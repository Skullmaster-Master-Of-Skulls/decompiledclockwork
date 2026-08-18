using System;
using System.Collections.Generic;
using System.Xml;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class Rule
	{
		// Token: 0x0600034D RID: 845 RVA: 0x000188A0 File Offset: 0x00016AA0
		public Rule(int orderNum, bool includeNonVirtualRooms, bool includeVirtualRooms, int minutesPre, int minutesPost, bool shiftTimeToMatchEndOfDay, bool shiftTimeToMatchStartOfDay, bool enforceOverlapWithClassTime, bool stopLookingIfFoundAtLeastOne, bool ShiftTimeAroundTimetable, bool ignoreAssetRules)
		{
			this.orderNum = orderNum;
			this.includeNonVirtualRooms = includeNonVirtualRooms;
			this.includeVirtualRooms = includeVirtualRooms;
			this.minutesPre = minutesPre;
			this.minutesPost = minutesPost;
			this.roomIdsToExclude = new List<int>();
			this.shiftTimeToMatchEndOfDay = shiftTimeToMatchEndOfDay;
			this.shiftTimeToMatchStartOfDay = shiftTimeToMatchStartOfDay;
			this.enforceOverlapWithClassTime = enforceOverlapWithClassTime;
			this.stopLookingIfFoundAtLeastOne = stopLookingIfFoundAtLeastOne;
			this.shiftTimeAroundTimetable = ShiftTimeAroundTimetable;
			this.ignoreAssetRules = ignoreAssetRules;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00018918 File Offset: 0x00016B18
		public int OrderNum
		{
			get
			{
				return this.orderNum;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00018930 File Offset: 0x00016B30
		public bool IncludeNonVirtualRooms
		{
			get
			{
				return this.includeNonVirtualRooms;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00018948 File Offset: 0x00016B48
		public bool IncludeVirtualRooms
		{
			get
			{
				return this.includeVirtualRooms;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00018960 File Offset: 0x00016B60
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00018978 File Offset: 0x00016B78
		public int MinutesPre
		{
			get
			{
				return this.minutesPre;
			}
			set
			{
				this.minutesPre = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00018984 File Offset: 0x00016B84
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0001899C File Offset: 0x00016B9C
		public int MinutesPost
		{
			get
			{
				return this.minutesPost;
			}
			set
			{
				this.minutesPost = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000189A8 File Offset: 0x00016BA8
		// (set) Token: 0x06000356 RID: 854 RVA: 0x000189C0 File Offset: 0x00016BC0
		public List<int> RoomIdsToExclud
		{
			get
			{
				return this.roomIdsToExclude;
			}
			set
			{
				this.roomIdsToExclude = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000189CC File Offset: 0x00016BCC
		// (set) Token: 0x06000358 RID: 856 RVA: 0x000189E4 File Offset: 0x00016BE4
		public bool ShiftTimeToMatchEndOfDay
		{
			get
			{
				return this.shiftTimeToMatchEndOfDay;
			}
			set
			{
				this.shiftTimeToMatchEndOfDay = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000189F0 File Offset: 0x00016BF0
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00018A08 File Offset: 0x00016C08
		public bool ShiftTimeToMatchStartOfDay
		{
			get
			{
				return this.shiftTimeToMatchStartOfDay;
			}
			set
			{
				this.shiftTimeToMatchStartOfDay = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00018A14 File Offset: 0x00016C14
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00018A2C File Offset: 0x00016C2C
		public bool EnforceOverlapWithClassTime
		{
			get
			{
				return this.enforceOverlapWithClassTime;
			}
			set
			{
				this.enforceOverlapWithClassTime = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00018A38 File Offset: 0x00016C38
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00018A50 File Offset: 0x00016C50
		public bool StopLookingIfFoundAtLeastOne
		{
			get
			{
				return this.stopLookingIfFoundAtLeastOne;
			}
			set
			{
				this.stopLookingIfFoundAtLeastOne = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00018A5C File Offset: 0x00016C5C
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00018A74 File Offset: 0x00016C74
		public bool ShiftTimeAroundTimetable
		{
			get
			{
				return this.shiftTimeAroundTimetable;
			}
			set
			{
				this.shiftTimeAroundTimetable = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00018A80 File Offset: 0x00016C80
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00018A98 File Offset: 0x00016C98
		public bool IgnoreAssetRules
		{
			get
			{
				return this.ignoreAssetRules;
			}
			set
			{
				this.ignoreAssetRules = value;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00018AA4 File Offset: 0x00016CA4
		public override string ToString()
		{
			string format = "ordernum={0}; nonvirtual={1}; virtual={2}; minspre={3}; minspost={4}; rmexclude={5};";
			object[] array = new object[6];
			array[0] = this.orderNum.ToString();
			array[1] = this.includeNonVirtualRooms.ToString();
			array[2] = this.includeVirtualRooms.ToString();
			array[3] = this.minutesPre.ToString();
			array[4] = this.minutesPost.ToString();
			array[5] = string.Join(",", this.roomIdsToExclude.ConvertAll<string>((int id) => id.ToString()).ToArray());
			return string.Format(format, array);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00018B48 File Offset: 0x00016D48
		private static Rule FromXml(XmlNode node)
		{
			bool flag = true;
			int num = 0;
			bool flag2 = true;
			bool flag3 = true;
			int num2 = 0;
			int num3 = 0;
			List<int> roomIdsToExclud = new List<int>();
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = true;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			foreach (object obj in node.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text = xmlAttribute.Name.ToLower();
				string value = xmlAttribute.Value;
				string text2 = text;
				string text3 = text2;
				uint num4 = <PrivateImplementationDetails>.ComputeStringHash(text3);
				if (num4 <= 1232045989U)
				{
					if (num4 <= 168905150U)
					{
						if (num4 != 73515729U)
						{
							if (num4 != 104735609U)
							{
								if (num4 == 168905150U)
								{
									if (text3 == "shifttimetomatchendofday")
									{
										flag4 = Core.ParseBooleanAttribute(value, false);
									}
								}
							}
							else if (text3 == "shifttimetomatchstartofday")
							{
								flag5 = Core.ParseBooleanAttribute(value, false);
								flag10 = true;
							}
						}
						else if (text3 == "stoplookingiffoundatleastone")
						{
							flag7 = Core.ParseBooleanAttribute(value, true);
						}
					}
					else if (num4 != 917586064U)
					{
						if (num4 != 1162379814U)
						{
							if (num4 == 1232045989U)
							{
								if (text3 == "ordernum")
								{
									num = Core.ParseIntWithDefaultValue(value, 0);
								}
							}
						}
						else if (text3 == "includevirtualrooms")
						{
							flag3 = Core.ParseBooleanAttribute(value, false);
						}
					}
					else if (text3 == "ignoreassetrules")
					{
						flag9 = Core.ParseBooleanAttribute(value, false);
					}
				}
				else if (num4 <= 2183805104U)
				{
					if (num4 != 1304893410U)
					{
						if (num4 != 1476832427U)
						{
							if (num4 == 2183805104U)
							{
								if (text3 == "roomstoexclude")
								{
									bool flag11 = value != null;
									if (flag11)
									{
										roomIdsToExclud = Core.StringToIntList(value);
									}
								}
							}
						}
						else if (text3 == "includenonvirtualrooms")
						{
							flag2 = Core.ParseBooleanAttribute(value, false);
						}
					}
					else if (text3 == "allowedminutesafter")
					{
						num3 = Core.ParseIntWithDefaultValue(value, 0);
					}
				}
				else if (num4 <= 3062644515U)
				{
					if (num4 != 2788725782U)
					{
						if (num4 == 3062644515U)
						{
							if (text3 == "allowedminutesbefore")
							{
								num2 = Core.ParseIntWithDefaultValue(value, 0);
							}
						}
					}
					else if (text3 == "shifttimearoundtimetable")
					{
						flag8 = Core.ParseBooleanAttribute(value, false);
					}
				}
				else if (num4 != 3648362799U)
				{
					if (num4 == 4169229981U)
					{
						if (text3 == "enforceoverlapwithclasstime")
						{
							flag6 = Core.ParseBooleanAttribute(value, false);
						}
					}
				}
				else if (text3 == "active")
				{
					flag = Core.ParseBooleanAttribute(value, true);
				}
				bool flag12 = !flag;
				if (flag12)
				{
					break;
				}
			}
			bool flag13 = !flag10;
			if (flag13)
			{
				flag5 = flag4;
			}
			bool flag14 = flag;
			Rule result;
			if (flag14)
			{
				result = new Rule(num, flag2, flag3, num2, num3, flag4, flag5, flag6, flag7, flag8, flag9)
				{
					RoomIdsToExclud = roomIdsToExclud
				};
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00018F0C File Offset: 0x0001710C
		public static List<Rule> FromXml(string xml)
		{
			List<Rule> list = new List<Rule>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			foreach (object obj in xmlDocument.LastChild.ChildNodes)
			{
				XmlNode node = (XmlNode)obj;
				Rule rule = Rule.FromXml(node);
				bool flag = rule != null;
				if (flag)
				{
					list.Add(rule);
				}
			}
			return list;
		}

		// Token: 0x040001A4 RID: 420
		private int orderNum;

		// Token: 0x040001A5 RID: 421
		private bool includeNonVirtualRooms;

		// Token: 0x040001A6 RID: 422
		private bool includeVirtualRooms;

		// Token: 0x040001A7 RID: 423
		private int minutesPre;

		// Token: 0x040001A8 RID: 424
		private int minutesPost;

		// Token: 0x040001A9 RID: 425
		private List<int> roomIdsToExclude;

		// Token: 0x040001AA RID: 426
		private bool shiftTimeToMatchEndOfDay;

		// Token: 0x040001AB RID: 427
		private bool shiftTimeToMatchStartOfDay;

		// Token: 0x040001AC RID: 428
		private bool enforceOverlapWithClassTime;

		// Token: 0x040001AD RID: 429
		private bool stopLookingIfFoundAtLeastOne;

		// Token: 0x040001AE RID: 430
		private bool shiftTimeAroundTimetable;

		// Token: 0x040001AF RID: 431
		private bool ignoreAssetRules;
	}
}
