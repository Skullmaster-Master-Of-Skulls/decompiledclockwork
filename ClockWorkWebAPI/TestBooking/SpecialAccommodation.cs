using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	public class SpecialAccommodation
	{
		// Token: 0x06000369 RID: 873 RVA: 0x00018FE6 File Offset: 0x000171E6
		public SpecialAccommodation()
		{
			this.specialAccommodationType = SpecialAccommodationType.Unknown;
			this.args = new StringDictionary();
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00019004 File Offset: 0x00017204
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0001901C File Offset: 0x0001721C
		public int ControlId
		{
			get
			{
				return this.controlId;
			}
			set
			{
				this.controlId = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00019026 File Offset: 0x00017226
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0001902E File Offset: 0x0001722E
		public string ControlIdSpecificValue { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00019038 File Offset: 0x00017238
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00019050 File Offset: 0x00017250
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0001905C File Offset: 0x0001725C
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00019074 File Offset: 0x00017274
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00019080 File Offset: 0x00017280
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00019098 File Offset: 0x00017298
		public StringDictionary Args
		{
			get
			{
				return this.args;
			}
			set
			{
				this.args = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000190A4 File Offset: 0x000172A4
		// (set) Token: 0x06000375 RID: 885 RVA: 0x000190BC File Offset: 0x000172BC
		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			set
			{
				this.isActive = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000190C8 File Offset: 0x000172C8
		public SpecialAccommodationType SpecialAccommodationType
		{
			get
			{
				return this.specialAccommodationType;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000190E0 File Offset: 0x000172E0
		public int SpecialAccommodationTypeOrder
		{
			get
			{
				return SpecialAccommodation.GetSpecialAccommodationTypeOrder(this.specialAccommodationType);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00019100 File Offset: 0x00017300
		public static int GetSpecialAccommodationTypeOrder(SpecialAccommodationType type)
		{
			int result;
			if (type != SpecialAccommodationType.Extra_Time)
			{
				if (type != SpecialAccommodationType.TimeOfDay)
				{
					if (type != SpecialAccommodationType.StartEndOfDaySlide)
					{
						result = 1000;
					}
					else
					{
						result = 99000;
					}
				}
				else
				{
					result = 2000;
				}
			}
			else
			{
				result = 100;
			}
			return result;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001914C File Offset: 0x0001734C
		public void SetSpecialType(string typeString)
		{
			string text = typeString.ToLower();
			string text2 = text;
			string text3 = text2;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
			if (num <= 2271212472U)
			{
				if (num <= 1338944401U)
				{
					if (num != 286870188U)
					{
						if (num == 1338944401U)
						{
							if (text3 == "email coordinator")
							{
								this.specialAccommodationType = SpecialAccommodationType.EmailCoordinator;
								return;
							}
						}
					}
					else if (text3 == "max per day")
					{
						this.specialAccommodationType = SpecialAccommodationType.MaxPerDay;
						return;
					}
				}
				else if (num != 1422990172U)
				{
					if (num != 1827597889U)
					{
						if (num == 2271212472U)
						{
							if (text3 == "start end of day slide")
							{
								this.specialAccommodationType = SpecialAccommodationType.StartEndOfDaySlide;
								return;
							}
						}
					}
					else if (text3 == "break time")
					{
						this.specialAccommodationType = SpecialAccommodationType.Breaks;
						return;
					}
				}
				else if (text3 == "days rest")
				{
					this.specialAccommodationType = SpecialAccommodationType.DaysRest;
					return;
				}
			}
			else if (num <= 2568964787U)
			{
				if (num != 2414099341U)
				{
					if (num == 2568964787U)
					{
						if (text3 == "time of day")
						{
							this.specialAccommodationType = SpecialAccommodationType.TimeOfDay;
							return;
						}
					}
				}
				else if (text3 == "add icon")
				{
					this.specialAccommodationType = SpecialAccommodationType.AddIcon;
					return;
				}
			}
			else if (num != 3222898326U)
			{
				if (num != 3653523112U)
				{
					if (num == 3813071874U)
					{
						if (text3 == "extra time")
						{
							this.specialAccommodationType = SpecialAccommodationType.Extra_Time;
							return;
						}
					}
				}
				else if (text3 == "no booking online")
				{
					this.specialAccommodationType = SpecialAccommodationType.CantBookOnline;
					return;
				}
			}
			else if (text3 == "snap time")
			{
				this.specialAccommodationType = SpecialAccommodationType.SnapTime;
				return;
			}
			this.specialAccommodationType = SpecialAccommodationType.Unknown;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00019354 File Offset: 0x00017554
		public string GetArg(string argName, string defaultValue)
		{
			string text = this.args[argName];
			bool flag = string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = text;
			}
			return result;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00019384 File Offset: 0x00017584
		public int GetArgInt(string argName, int defaultValue)
		{
			string text = this.args[argName];
			bool flag = string.IsNullOrEmpty(text);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				try
				{
					result = int.Parse(text);
				}
				catch
				{
					result = defaultValue;
				}
			}
			return result;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000193D0 File Offset: 0x000175D0
		public static List<SpecialAccommodation> LoadSpecialAccommodations(string xml)
		{
			return SpecialAccommodation.LoadSpecialAccommodations(xml, "");
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000193F0 File Offset: 0x000175F0
		public static List<SpecialAccommodation> LoadSpecialAccommodations(string xml, string specialAccommodationsToIgnore)
		{
			List<SpecialAccommodation> list = new List<SpecialAccommodation>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			foreach (object obj in xmlDocument.LastChild.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				SpecialAccommodation specialAccommodation = new SpecialAccommodation();
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					string text = xmlNode2.Name.ToLower();
					bool flag = text.Equals("controlid");
					if (flag)
					{
						string text2 = xmlNode2.InnerText.Trim();
						bool flag2 = text2.Length > 0;
						if (flag2)
						{
							int num = text2.IndexOf(':');
							bool flag3 = num < 0;
							if (flag3)
							{
								num = text2.IndexOf('.');
							}
							bool flag4 = num > 0;
							if (flag4)
							{
								specialAccommodation.ControlIdSpecificValue = text2.Substring(num + 1);
								text2 = text2.Substring(0, num);
							}
							int num2;
							bool flag5 = int.TryParse(text2, out num2);
							if (flag5)
							{
								specialAccommodation.ControlId = num2;
							}
						}
					}
					else
					{
						bool flag6 = text.Equals("isactive");
						if (flag6)
						{
							specialAccommodation.IsActive = xmlNode2.InnerText.Equals("1");
						}
						else
						{
							bool flag7 = text.Equals("title");
							if (flag7)
							{
								specialAccommodation.Title = xmlNode2.InnerText;
							}
							else
							{
								bool flag8 = text.Equals("description");
								if (flag8)
								{
									specialAccommodation.Description = xmlNode2.InnerText;
								}
								else
								{
									bool flag9 = text.Equals("specialtype");
									if (flag9)
									{
										specialAccommodation.SetSpecialType(xmlNode2.InnerText);
									}
									else
									{
										specialAccommodation.Args.Add(text, xmlNode2.InnerText);
									}
								}
							}
						}
					}
				}
				list.Add(specialAccommodation);
			}
			bool flag10 = !string.IsNullOrEmpty(specialAccommodationsToIgnore);
			if (flag10)
			{
				string[] array = specialAccommodationsToIgnore.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in array)
				{
					int num3;
					bool flag11 = int.TryParse(s, out num3);
					if (flag11)
					{
						bool flag12 = Enum.IsDefined(typeof(SpecialAccommodationType), num3);
						if (flag12)
						{
							SpecialAccommodationType specialType = (SpecialAccommodationType)num3;
							list.RemoveAll((SpecialAccommodation f) => f.SpecialAccommodationType == specialType);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x040001BC RID: 444
		private SpecialAccommodationType specialAccommodationType;

		// Token: 0x040001BD RID: 445
		private int controlId;

		// Token: 0x040001BE RID: 446
		private string title;

		// Token: 0x040001BF RID: 447
		private string description;

		// Token: 0x040001C0 RID: 448
		private bool isActive;

		// Token: 0x040001C1 RID: 449
		private StringDictionary args;
	}
}
