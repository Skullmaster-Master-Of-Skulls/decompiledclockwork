using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x0200054A RID: 1354
	[Serializable]
	public class SpecialAccommodation
	{
		// Token: 0x06002B7D RID: 11133 RVA: 0x0002FB64 File Offset: 0x0002DD64
		public SpecialAccommodation()
		{
			this.specialAccommodationType = SpecialAccommodationType.Unknown;
			this.args = new StringDictionary();
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x0002FB80 File Offset: 0x0002DD80
		// (set) Token: 0x06002B7F RID: 11135 RVA: 0x0002FB98 File Offset: 0x0002DD98
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

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06002B80 RID: 11136 RVA: 0x0002FBA2 File Offset: 0x0002DDA2
		// (set) Token: 0x06002B81 RID: 11137 RVA: 0x0002FBAA File Offset: 0x0002DDAA
		public string ControlIdSpecificValue { get; set; }

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x0002FBB4 File Offset: 0x0002DDB4
		// (set) Token: 0x06002B83 RID: 11139 RVA: 0x0002FBCC File Offset: 0x0002DDCC
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

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x0002FBD8 File Offset: 0x0002DDD8
		// (set) Token: 0x06002B85 RID: 11141 RVA: 0x0002FBF0 File Offset: 0x0002DDF0
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

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x0002FBFC File Offset: 0x0002DDFC
		// (set) Token: 0x06002B87 RID: 11143 RVA: 0x0002FC14 File Offset: 0x0002DE14
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

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x0002FC20 File Offset: 0x0002DE20
		// (set) Token: 0x06002B89 RID: 11145 RVA: 0x0002FC38 File Offset: 0x0002DE38
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

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x0002FC44 File Offset: 0x0002DE44
		// (set) Token: 0x06002B8B RID: 11147 RVA: 0x0002FC5C File Offset: 0x0002DE5C
		public SpecialAccommodationType SpecialAccommodationType
		{
			get
			{
				return this.specialAccommodationType;
			}
			set
			{
				this.specialAccommodationType = value;
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x0002FC68 File Offset: 0x0002DE68
		public int SpecialAccommodationTypeOrder
		{
			get
			{
				return SpecialAccommodation.GetSpecialAccommodationTypeOrder(this.specialAccommodationType);
			}
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x0002FC88 File Offset: 0x0002DE88
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

		// Token: 0x06002B8E RID: 11150 RVA: 0x0002FCD4 File Offset: 0x0002DED4
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

		// Token: 0x06002B8F RID: 11151 RVA: 0x0002FEDC File Offset: 0x0002E0DC
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

		// Token: 0x06002B90 RID: 11152 RVA: 0x0002FF0C File Offset: 0x0002E10C
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

		// Token: 0x06002B91 RID: 11153 RVA: 0x0002FF58 File Offset: 0x0002E158
		public static List<SpecialAccommodation> LoadSpecialAccommodations(string xml, string specialAccommodationsToIgnore = "")
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

		// Token: 0x04001ED1 RID: 7889
		private SpecialAccommodationType specialAccommodationType;

		// Token: 0x04001ED2 RID: 7890
		private int controlId;

		// Token: 0x04001ED3 RID: 7891
		private string title;

		// Token: 0x04001ED4 RID: 7892
		private string description;

		// Token: 0x04001ED5 RID: 7893
		private bool isActive;

		// Token: 0x04001ED6 RID: 7894
		private StringDictionary args;
	}
}
