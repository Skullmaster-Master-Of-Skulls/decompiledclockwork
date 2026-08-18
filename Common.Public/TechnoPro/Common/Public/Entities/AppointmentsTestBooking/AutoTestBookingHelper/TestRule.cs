using System;
using System.Collections.Generic;
using System.Xml;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000548 RID: 1352
	[Serializable]
	public class TestRule
	{
		// Token: 0x06002B56 RID: 11094 RVA: 0x0000D55A File Offset: 0x0000B75A
		public TestRule()
		{
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0002F19C File Offset: 0x0002D39C
		public TestRule(int orderNum, bool includeNonVirtualRooms, bool includeVirtualRooms, int minutesPre, int minutesPost, bool shiftTimeToMatchEndOfDay, bool shiftTimeToMatchStartOfDay, bool enforceOverlapWithClassTime, bool stopLookingIfFoundAtLeastOne, bool ShiftTimeAroundTimetable, bool ignoreAssetRules, int enforceOverlapWithClassTime_firstXMinutes, int timetableShiftMaxNumMinutesBeforeClassTime, int timetableShiftMaxNumMinutesAfterClassTime)
		{
			this.Init(orderNum, includeNonVirtualRooms, includeVirtualRooms, minutesPre, minutesPost, shiftTimeToMatchEndOfDay, shiftTimeToMatchStartOfDay, enforceOverlapWithClassTime, stopLookingIfFoundAtLeastOne, ShiftTimeAroundTimetable, ignoreAssetRules, enforceOverlapWithClassTime_firstXMinutes, timetableShiftMaxNumMinutesBeforeClassTime, timetableShiftMaxNumMinutesAfterClassTime);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0002F1D4 File Offset: 0x0002D3D4
		private void Init(int orderNum, bool includeNonVirtualRooms, bool includeVirtualRooms, int minutesPre, int minutesPost, bool shiftTimeToMatchEndOfDay, bool shiftTimeToMatchStartOfDay, bool enforceOverlapWithClassTime, bool stopLookingIfFoundAtLeastOne, bool ShiftTimeAroundTimetable, bool ignoreAssetRules, int enforceOverlapWithClassTime_firstXMinutes, int timetableShiftMaxNumMinutesBeforeClassTime, int timetableShiftMaxNumMinutesAfterClassTime)
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
			this.EnforceOverlapWithClassTime_firstXMinutes = enforceOverlapWithClassTime_firstXMinutes;
			this.TimetableShiftMaxNumMinutesBeforeClassTime = timetableShiftMaxNumMinutesBeforeClassTime;
			this.TimetableShiftMaxNumMinutesAfterClassTime = timetableShiftMaxNumMinutesAfterClassTime;
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x0002F260 File Offset: 0x0002D460
		// (set) Token: 0x06002B5A RID: 11098 RVA: 0x0002F278 File Offset: 0x0002D478
		public int EnforceOverlapWithClassTime_firstXMinutes
		{
			get
			{
				return this.enforceOverlapWithClassTime_firstXMinutes;
			}
			set
			{
				this.enforceOverlapWithClassTime_firstXMinutes = value;
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06002B5B RID: 11099 RVA: 0x0002F284 File Offset: 0x0002D484
		// (set) Token: 0x06002B5C RID: 11100 RVA: 0x0002F29C File Offset: 0x0002D49C
		public int OrderNum
		{
			get
			{
				return this.orderNum;
			}
			set
			{
				this.orderNum = value;
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x0002F2A8 File Offset: 0x0002D4A8
		// (set) Token: 0x06002B5E RID: 11102 RVA: 0x0002F2C0 File Offset: 0x0002D4C0
		public bool IncludeNonVirtualRooms
		{
			get
			{
				return this.includeNonVirtualRooms;
			}
			set
			{
				this.includeNonVirtualRooms = value;
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x0002F2CC File Offset: 0x0002D4CC
		// (set) Token: 0x06002B60 RID: 11104 RVA: 0x0002F2E4 File Offset: 0x0002D4E4
		public bool IncludeVirtualRooms
		{
			get
			{
				return this.includeVirtualRooms;
			}
			set
			{
				this.includeVirtualRooms = value;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
		// (set) Token: 0x06002B62 RID: 11106 RVA: 0x0002F308 File Offset: 0x0002D508
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

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x0002F314 File Offset: 0x0002D514
		// (set) Token: 0x06002B64 RID: 11108 RVA: 0x0002F32C File Offset: 0x0002D52C
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

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x0002F338 File Offset: 0x0002D538
		// (set) Token: 0x06002B66 RID: 11110 RVA: 0x0002F350 File Offset: 0x0002D550
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

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x0002F35C File Offset: 0x0002D55C
		// (set) Token: 0x06002B68 RID: 11112 RVA: 0x0002F374 File Offset: 0x0002D574
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

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x0002F380 File Offset: 0x0002D580
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x0002F398 File Offset: 0x0002D598
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

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x0002F3A4 File Offset: 0x0002D5A4
		// (set) Token: 0x06002B6C RID: 11116 RVA: 0x0002F3BC File Offset: 0x0002D5BC
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

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x0002F3C8 File Offset: 0x0002D5C8
		// (set) Token: 0x06002B6E RID: 11118 RVA: 0x0002F3E0 File Offset: 0x0002D5E0
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

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x0002F3EC File Offset: 0x0002D5EC
		// (set) Token: 0x06002B70 RID: 11120 RVA: 0x0002F404 File Offset: 0x0002D604
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

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x0002F410 File Offset: 0x0002D610
		// (set) Token: 0x06002B72 RID: 11122 RVA: 0x0002F428 File Offset: 0x0002D628
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

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x0002F432 File Offset: 0x0002D632
		// (set) Token: 0x06002B74 RID: 11124 RVA: 0x0002F43A File Offset: 0x0002D63A
		public int TimetableShiftMaxNumMinutesBeforeClassTime { get; private set; }

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x0002F443 File Offset: 0x0002D643
		// (set) Token: 0x06002B76 RID: 11126 RVA: 0x0002F44B File Offset: 0x0002D64B
		public int TimetableShiftMaxNumMinutesAfterClassTime { get; private set; }

		// Token: 0x06002B77 RID: 11127 RVA: 0x0002F454 File Offset: 0x0002D654
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

		// Token: 0x06002B78 RID: 11128 RVA: 0x0002F4F8 File Offset: 0x0002D6F8
		private static TestRule FromXml(XmlNode node)
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
			int num4 = 0;
			bool flag10 = false;
			int timetableShiftMaxNumMinutesBeforeClassTime = 0;
			int timetableShiftMaxNumMinutesAfterClassTime = 0;
			foreach (object obj in node.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text = xmlAttribute.Name.ToLower();
				string value = xmlAttribute.Value;
				string text2 = text;
				string text3 = text2;
				uint num5 = <PrivateImplementationDetails>.ComputeStringHash(text3);
				if (num5 <= 1476832427U)
				{
					if (num5 <= 917586064U)
					{
						if (num5 <= 104735609U)
						{
							if (num5 != 73515729U)
							{
								if (num5 == 104735609U)
								{
									if (text3 == "shifttimetomatchstartofday")
									{
										flag5 = TestRule.ParseBooleanAttribute(value, false);
										flag10 = true;
									}
								}
							}
							else if (text3 == "stoplookingiffoundatleastone")
							{
								flag7 = TestRule.ParseBooleanAttribute(value, true);
							}
						}
						else if (num5 != 168905150U)
						{
							if (num5 == 917586064U)
							{
								if (text3 == "ignoreassetrules")
								{
									flag9 = TestRule.ParseBooleanAttribute(value, false);
								}
							}
						}
						else if (text3 == "shifttimetomatchendofday")
						{
							flag4 = TestRule.ParseBooleanAttribute(value, false);
						}
					}
					else if (num5 <= 1232045989U)
					{
						if (num5 != 1162379814U)
						{
							if (num5 == 1232045989U)
							{
								if (text3 == "ordernum")
								{
									num = TestRule.ParseIntWithDefaultValue(value, 0);
								}
							}
						}
						else if (text3 == "includevirtualrooms")
						{
							flag3 = TestRule.ParseBooleanAttribute(value, false);
						}
					}
					else if (num5 != 1304893410U)
					{
						if (num5 == 1476832427U)
						{
							if (text3 == "includenonvirtualrooms")
							{
								flag2 = TestRule.ParseBooleanAttribute(value, false);
							}
						}
					}
					else if (text3 == "allowedminutesafter")
					{
						num3 = TestRule.ParseIntWithDefaultValue(value, 0);
					}
				}
				else if (num5 <= 3062644515U)
				{
					if (num5 <= 2788725782U)
					{
						if (num5 != 2183805104U)
						{
							if (num5 == 2788725782U)
							{
								if (text3 == "shifttimearoundtimetable")
								{
									flag8 = TestRule.ParseBooleanAttribute(value, false);
								}
							}
						}
						else if (text3 == "roomstoexclude")
						{
							bool flag11 = value != null;
							if (flag11)
							{
								roomIdsToExclud = TestRule.StringToIntList(value);
							}
						}
					}
					else if (num5 != 2849628880U)
					{
						if (num5 == 3062644515U)
						{
							if (text3 == "allowedminutesbefore")
							{
								num2 = TestRule.ParseIntWithDefaultValue(value, 0);
							}
						}
					}
					else if (text3 == "firstxminutes")
					{
						num4 = TestRule.ParseIntWithDefaultValue(value, 0);
					}
				}
				else if (num5 <= 3648362799U)
				{
					if (num5 != 3196147280U)
					{
						if (num5 == 3648362799U)
						{
							if (text3 == "active")
							{
								flag = TestRule.ParseBooleanAttribute(value, true);
							}
						}
					}
					else if (text3 == "timetableshiftmaxnumminutesafterclasstime")
					{
						timetableShiftMaxNumMinutesAfterClassTime = TestRule.ParseIntWithDefaultValue(value, 0);
					}
				}
				else if (num5 != 3828851417U)
				{
					if (num5 == 4169229981U)
					{
						if (text3 == "enforceoverlapwithclasstime")
						{
							flag6 = TestRule.ParseBooleanAttribute(value, false);
						}
					}
				}
				else if (text3 == "timetableshiftmaxnumminutesbeforeclasstime")
				{
					timetableShiftMaxNumMinutesBeforeClassTime = TestRule.ParseIntWithDefaultValue(value, 0);
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
			TestRule result;
			if (flag14)
			{
				TestRule testRule = new TestRule(num, flag2, flag3, num2, num3, flag4, flag5, flag6, flag7, flag8, flag9, num4, timetableShiftMaxNumMinutesBeforeClassTime, timetableShiftMaxNumMinutesAfterClassTime)
				{
					RoomIdsToExclud = roomIdsToExclud
				};
				result = testRule;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0002F990 File Offset: 0x0002DB90
		private static List<int> StringToIntList(string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			foreach (string text in array)
			{
				bool flag = string.IsNullOrEmpty(text);
				if (!flag)
				{
					int item;
					bool flag2 = int.TryParse(text, out item);
					if (flag2)
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0002F9FC File Offset: 0x0002DBFC
		private static bool ParseBooleanAttribute(string s, bool defaultValue)
		{
			bool flag = string.IsNullOrEmpty(s);
			bool result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				bool flag2 = s.Equals("0");
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = s.Equals("1");
					bool flag4;
					result = (flag3 || (bool.TryParse(s, out flag4) ? flag4 : defaultValue));
				}
			}
			return result;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0002FA54 File Offset: 0x0002DC54
		private static int ParseIntWithDefaultValue(string s, int defaultValue)
		{
			bool flag = string.IsNullOrEmpty(s);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int num;
				result = ((!int.TryParse(s, out num)) ? defaultValue : num);
			}
			return result;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0002FA84 File Offset: 0x0002DC84
		public static List<TestRule> FromXml(string xml)
		{
			List<TestRule> list = new List<TestRule>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			int num = 0;
			foreach (object obj in xmlDocument.LastChild.ChildNodes)
			{
				XmlNode node = (XmlNode)obj;
				TestRule testRule = TestRule.FromXml(node);
				bool flag = testRule == null;
				if (!flag)
				{
					bool flag2 = testRule.OrderNum > num;
					if (flag2)
					{
						testRule.OrderNum = num;
					}
					num++;
					list.Add(testRule);
				}
			}
			list.Sort((TestRule g1, TestRule g2) => g1.OrderNum.CompareTo(g2.OrderNum));
			return list;
		}

		// Token: 0x04001EB6 RID: 7862
		private int orderNum;

		// Token: 0x04001EB7 RID: 7863
		private bool includeNonVirtualRooms;

		// Token: 0x04001EB8 RID: 7864
		private bool includeVirtualRooms;

		// Token: 0x04001EB9 RID: 7865
		private int minutesPre;

		// Token: 0x04001EBA RID: 7866
		private int minutesPost;

		// Token: 0x04001EBB RID: 7867
		private List<int> roomIdsToExclude;

		// Token: 0x04001EBC RID: 7868
		private bool shiftTimeToMatchEndOfDay;

		// Token: 0x04001EBD RID: 7869
		private bool shiftTimeToMatchStartOfDay;

		// Token: 0x04001EBE RID: 7870
		private bool enforceOverlapWithClassTime;

		// Token: 0x04001EBF RID: 7871
		private bool stopLookingIfFoundAtLeastOne;

		// Token: 0x04001EC0 RID: 7872
		private bool shiftTimeAroundTimetable;

		// Token: 0x04001EC1 RID: 7873
		private bool ignoreAssetRules;

		// Token: 0x04001EC2 RID: 7874
		private int enforceOverlapWithClassTime_firstXMinutes;
	}
}
