using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public class Room
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00017E4C File Offset: 0x0001604C
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00017E64 File Offset: 0x00016064
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00017E70 File Offset: 0x00016070
		public bool IsVirtualRoom
		{
			get
			{
				return this.roomType == RoomType.VirtualRoom || this.roomType == RoomType.SuperVirtualRoom;
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00017E98 File Offset: 0x00016098
		public string ToStringDebug()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.title);
			stringBuilder.AppendFormat(" ({0})", this.rid.ToString());
			stringBuilder.AppendFormat(" [roomType={0}", this.roomType.ToString());
			stringBuilder.AppendFormat("; campus={0}", this.campus);
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

		// Token: 0x06000333 RID: 819 RVA: 0x00017F94 File Offset: 0x00016194
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00018018 File Offset: 0x00016218
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00018030 File Offset: 0x00016230
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0001803C File Offset: 0x0001623C
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00018054 File Offset: 0x00016254
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00018060 File Offset: 0x00016260
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00018078 File Offset: 0x00016278
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

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00018084 File Offset: 0x00016284
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0001809C File Offset: 0x0001629C
		[XmlElement("campus")]
		public string Campus
		{
			get
			{
				return this.campus;
			}
			set
			{
				this.campus = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000180A8 File Offset: 0x000162A8
		// (set) Token: 0x0600033D RID: 829 RVA: 0x000180C0 File Offset: 0x000162C0
		[XmlIgnore]
		public AccommodationCollection GivePriorityToStudentsWithTheseAccommodations
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

		// Token: 0x0600033E RID: 830 RVA: 0x000180CC File Offset: 0x000162CC
		public bool IsAvailable(Appointment currAppToBook, DataTable studentSchedule, DataTable courseTimetable, DataTable requiredResourceSchedule, DataTable roomsAvailability)
		{
			return true;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000180E0 File Offset: 0x000162E0
		public Room(int rid, string title, RoomType roomType, int priorityNumber)
		{
			this.assets = new List<Asset>();
			this.givePriorityToStudentsWithTheseAccommodations = new AccommodationCollection();
			this.roomType = roomType;
			this.rid = rid;
			this.title = title;
			this.priorityNumber = priorityNumber;
			this.campus = "";
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001813C File Offset: 0x0001633C
		public Room()
		{
			this.assets = new List<Asset>();
			this.givePriorityToStudentsWithTheseAccommodations = new AccommodationCollection();
			this.roomType = RoomType.unknown;
			this.rid = 0;
			this.title = "";
			this.priorityNumber = 0;
			this.campus = "";
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0001819C File Offset: 0x0001639C
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

		// Token: 0x06000342 RID: 834 RVA: 0x00018200 File Offset: 0x00016400
		public bool SupportsRequiredAssets(List<Asset> requiredAssets, out int score)
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

		// Token: 0x06000343 RID: 835 RVA: 0x00018284 File Offset: 0x00016484
		public void AddAsset(Asset asset)
		{
			this.assets.Add(asset);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00018294 File Offset: 0x00016494
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

		// Token: 0x06000345 RID: 837 RVA: 0x000182D8 File Offset: 0x000164D8
		public static string SerializeToXml(Room room)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Room));
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				xmlSerializer.Serialize(stringWriter, room);
				string s = stringWriter.ToString();
				result = HttpUtility.HtmlEncode(s);
			}
			return result;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00018334 File Offset: 0x00016534
		public static Room DeserializeFromXml(string xml)
		{
			string s = HttpUtility.HtmlDecode(xml);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Room));
			Room result;
			using (StringReader stringReader = new StringReader(s))
			{
				result = (Room)xmlSerializer.Deserialize(stringReader);
			}
			return result;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0001838C File Offset: 0x0001658C
		private List<string> Campuses
		{
			get
			{
				bool flag = this.campuses == null;
				if (flag)
				{
					bool flag2 = string.IsNullOrEmpty(this.campus);
					if (flag2)
					{
						this.campuses = new List<string>();
					}
					else
					{
						this.campuses = new List<string>();
						string[] array = this.campus.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries);
						foreach (string text in array)
						{
							string text2 = text.Trim();
							bool flag3 = text2.Length > 0;
							if (flag3)
							{
								this.campuses.Add(text2);
							}
						}
					}
				}
				return this.campuses;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00018440 File Offset: 0x00016640
		public bool SupportsCampus(string campusToMatch)
		{
			bool flag = string.IsNullOrEmpty(campusToMatch) || string.IsNullOrEmpty(this.campus);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				List<string> list = this.Campuses;
				bool flag2 = list.Find((string c) => c.Equals(campusToMatch, StringComparison.OrdinalIgnoreCase)) != null;
				result = flag2;
			}
			return result;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000184AC File Offset: 0x000166AC
		public static List<Room> LoadRooms(string xml, List<Asset> availableAssets)
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
						try
						{
							num = int.Parse(xmlNode2.InnerText);
						}
						catch
						{
						}
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
									try
									{
										num2 = int.Parse(xmlNode2.InnerText);
									}
									catch
									{
									}
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
					room.Campus = text4;
					string[] array = text3.Split(new char[]
					{
						','
					});
					foreach (string text6 in array)
					{
						string value = text6;
						Asset asset = null;
						foreach (Asset asset2 in availableAssets)
						{
							bool flag13 = asset2.AssetId.Equals(value);
							if (flag13)
							{
								asset = asset2;
								break;
							}
						}
						bool flag14 = asset != null;
						if (flag14)
						{
							room.AddAsset(asset);
						}
					}
					list.Add(room);
				}
			}
			return list;
		}

		// Token: 0x0400019B RID: 411
		private int rid;

		// Token: 0x0400019C RID: 412
		private string title;

		// Token: 0x0400019D RID: 413
		private int priorityNumber;

		// Token: 0x0400019E RID: 414
		private RoomType roomType;

		// Token: 0x0400019F RID: 415
		private string campus;

		// Token: 0x040001A0 RID: 416
		private List<Asset> assets;

		// Token: 0x040001A1 RID: 417
		private AccommodationCollection givePriorityToStudentsWithTheseAccommodations;

		// Token: 0x040001A2 RID: 418
		private List<string> campuses = null;
	}
}
