using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000547 RID: 1351
	[Serializable]
	public class Room
	{
		// Token: 0x06002B3C RID: 11068 RVA: 0x0002E95C File Offset: 0x0002CB5C
		public Room(int rid, string title, RoomType roomType, int priorityNumber)
		{
			this.Campuses = new List<string>();
			this.assets = new List<Asset>();
			this.givePriorityToStudentsWithTheseAccommodations = new List<Accommodation>();
			this.roomType = roomType;
			this.rid = rid;
			this.title = title;
			this.priorityNumber = priorityNumber;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0002E9B0 File Offset: 0x0002CBB0
		public Room()
		{
			this.Campuses = new List<string>();
			this.assets = new List<Asset>();
			this.givePriorityToStudentsWithTheseAccommodations = new List<Accommodation>();
			this.roomType = RoomType.unknown;
			this.rid = 0;
			this.title = "";
			this.priorityNumber = 0;
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x0002EA07 File Offset: 0x0002CC07
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x0002EA0F File Offset: 0x0002CC0F
		public List<string> Campuses { get; set; }

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x0002EA18 File Offset: 0x0002CC18
		// (set) Token: 0x06002B41 RID: 11073 RVA: 0x0002EA30 File Offset: 0x0002CC30
		public List<Asset> Assets
		{
			get
			{
				return this.assets;
			}
			set
			{
				this.assets = value;
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x0002EA3C File Offset: 0x0002CC3C
		// (set) Token: 0x06002B43 RID: 11075 RVA: 0x0002EA54 File Offset: 0x0002CC54
		[XmlElement("roomtype")]
		public RoomType RoomType
		{
			get
			{
				return this.roomType;
			}
			set
			{
				this.roomType = value;
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x0002EA60 File Offset: 0x0002CC60
		// (set) Token: 0x06002B45 RID: 11077 RVA: 0x0002EA78 File Offset: 0x0002CC78
		[XmlElement("roomid")]
		public int RoomId
		{
			get
			{
				return this.rid;
			}
			set
			{
				this.rid = value;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x0002EA84 File Offset: 0x0002CC84
		// (set) Token: 0x06002B47 RID: 11079 RVA: 0x0002EA9C File Offset: 0x0002CC9C
		[XmlElement("title")]
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

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x0002EAA8 File Offset: 0x0002CCA8
		// (set) Token: 0x06002B49 RID: 11081 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
		[XmlElement("prioritynumber")]
		public int PriorityNumber
		{
			get
			{
				return this.priorityNumber;
			}
			set
			{
				this.priorityNumber = value;
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x0002EACC File Offset: 0x0002CCCC
		// (set) Token: 0x06002B4B RID: 11083 RVA: 0x0002EAE4 File Offset: 0x0002CCE4
		[XmlIgnore]
		public IList<Accommodation> GivePriorityToStudentsWithTheseAccommodations
		{
			get
			{
				return this.givePriorityToStudentsWithTheseAccommodations;
			}
			set
			{
				this.givePriorityToStudentsWithTheseAccommodations = value;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x0002EAF0 File Offset: 0x0002CCF0
		public bool IsVirtualRoom
		{
			get
			{
				return this.roomType == RoomType.VirtualRoom || this.roomType == RoomType.SuperVirtualRoom;
			}
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0002EB18 File Offset: 0x0002CD18
		public string ToStringDebug()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.title);
			stringBuilder.AppendFormat(" ({0})", this.rid.ToString());
			stringBuilder.AppendFormat(" [roomType={0}", this.roomType.ToString());
			stringBuilder.AppendFormat("; campus={0}", (this.Campuses == null) ? "" : string.Join(",", this.Campuses.ToArray()));
			stringBuilder.AppendFormat("; priorityNumber={0}", this.priorityNumber.ToString());
			stringBuilder.Append("; assets={");
			foreach (Asset asset in this.assets)
			{
				stringBuilder.Append(asset.AssetId);
				stringBuilder.Append(",");
			}
			stringBuilder.Append("}]");
			return stringBuilder.ToString();
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0002EC34 File Offset: 0x0002CE34
		public static string ToStringDebug(string title, List<Room> rooms)
		{
			StringBuilder stringBuilder = new StringBuilder(title);
			stringBuilder.Append("<br />");
			foreach (Room room in rooms)
			{
				stringBuilder.Append(room.ToStringDebug());
				stringBuilder.Append("<br />");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0002ECB8 File Offset: 0x0002CEB8
		public bool IsAvailable(Appointment currAppToBook, DataTable studentSchedule, DataTable courseTimetable, DataTable requiredResourceSchedule, DataTable roomsAvailability)
		{
			return true;
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x0002ECCC File Offset: 0x0002CECC
		private int TotalAssetScore
		{
			get
			{
				int num = 0;
				foreach (Asset asset in this.assets)
				{
					num += asset.Score;
				}
				return num;
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0002ED2C File Offset: 0x0002CF2C
		public bool SupportsRequiredAssets(IList<Asset> requiredAssets, out int score)
		{
			int num = this.TotalAssetScore;
			foreach (Asset asset in requiredAssets)
			{
				bool flag = !Asset.ContainsAsset(this.assets, asset);
				if (flag)
				{
					score = 0;
					return false;
				}
				num -= asset.Score;
			}
			score = num;
			return true;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
		public void AddAsset(Asset asset)
		{
			this.assets.Add(asset);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x0002EDB8 File Offset: 0x0002CFB8
		public static bool RoomsEqual(Room r1, Room r2)
		{
			bool flag = r1 == null && r2 == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = r1 == null || r2 == null;
				result = (!flag2 && r1.RoomId == r2.RoomId);
			}
			return result;
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x0002EDFC File Offset: 0x0002CFFC
		public bool SupportsCampus(string campusToMatch)
		{
			string text = (this.Campuses == null || campusToMatch == null || campusToMatch.Trim().Length < 1) ? null : this.Campuses.FirstOrDefault((string g) => g.Equals(campusToMatch, StringComparison.OrdinalIgnoreCase));
			return text != null;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x0002EE60 File Offset: 0x0002D060
		public static IList<Room> LoadRooms(string xml, IList<Asset> availableAssets)
		{
			List<Room> list = new List<Room>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			foreach (object obj in xmlDocument.LastChild.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				int num = 0;
				string text = "";
				string text2 = "";
				int num2 = 0;
				string text3 = "";
				bool flag = true;
				string text4 = "";
				foreach (object obj2 in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					string text5 = xmlNode2.Name.ToLower();
					bool flag2 = text5.Equals("personid");
					if (flag2)
					{
						int.TryParse(xmlNode2.InnerText, out num);
					}
					else
					{
						bool flag3 = text5.Equals("title");
						if (flag3)
						{
							text = xmlNode2.InnerText;
						}
						else
						{
							bool flag4 = text5.Equals("type");
							if (flag4)
							{
								text2 = xmlNode2.InnerText;
							}
							else
							{
								bool flag5 = text5.Equals("ordernum");
								if (flag5)
								{
									int.TryParse(xmlNode2.InnerText, out num2);
								}
								else
								{
									bool flag6 = text5.Equals("campus");
									if (flag6)
									{
										text4 = xmlNode2.InnerText;
									}
									else
									{
										bool flag7 = text5.Equals("assets");
										if (flag7)
										{
											text3 = xmlNode2.InnerText;
										}
										else
										{
											bool flag8 = text5.Equals("isactive") && xmlNode2.InnerText.Equals("0");
											if (flag8)
											{
												flag = false;
											}
										}
									}
								}
							}
						}
					}
				}
				bool flag9 = flag;
				if (flag9)
				{
					bool flag10 = text2.Equals("regularroom");
					RoomType roomType;
					if (flag10)
					{
						roomType = RoomType.RegularRoom;
					}
					else
					{
						bool flag11 = text2.Equals("virtualroom");
						if (flag11)
						{
							roomType = RoomType.VirtualRoom;
						}
						else
						{
							bool flag12 = text2.Equals("supervirtualroom");
							if (flag12)
							{
								roomType = RoomType.SuperVirtualRoom;
							}
							else
							{
								roomType = RoomType.unknown;
							}
						}
					}
					Room room = new Room(num, text, roomType, num2);
					room.Campuses = ((text4 == null) ? new List<string>() : text4.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries).ToList<string>());
					string[] array = text3.Split(new char[]
					{
						','
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string id2 = array2[i];
						string id = id2;
						Asset asset = availableAssets.FirstOrDefault((Asset g) => g.AssetId == id);
						bool flag13 = asset != null;
						if (flag13)
						{
							room.AddAsset(asset);
						}
					}
					list.Add(room);
				}
			}
			list.Sort((Room r1, Room r2) => r1.PriorityNumber.CompareTo(r2.PriorityNumber));
			return list;
		}

		// Token: 0x04001EAF RID: 7855
		private int rid;

		// Token: 0x04001EB0 RID: 7856
		private string title;

		// Token: 0x04001EB1 RID: 7857
		private int priorityNumber;

		// Token: 0x04001EB2 RID: 7858
		private RoomType roomType;

		// Token: 0x04001EB3 RID: 7859
		private IList<Accommodation> givePriorityToStudentsWithTheseAccommodations;

		// Token: 0x04001EB4 RID: 7860
		private List<Asset> assets;
	}
}
