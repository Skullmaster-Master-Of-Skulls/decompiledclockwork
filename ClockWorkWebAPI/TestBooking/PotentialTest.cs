using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public class PotentialTest : IComparable<PotentialTest>
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00011259 File Offset: 0x0000F459
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00011261 File Offset: 0x0000F461
		public virtual List<PotentialTestMethodFoundNote> MethodFoundNotes { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0001126C File Offset: 0x0000F46C
		public string MethodFoundNotesSummary
		{
			get
			{
				return string.Join(Environment.NewLine, this.MethodFoundNotes.ConvertAll<string>((PotentialTestMethodFoundNote n) => n.ToString()).ToArray());
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000112B8 File Offset: 0x0000F4B8
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x000112D0 File Offset: 0x0000F4D0
		[XmlElement("id")]
		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000112DC File Offset: 0x0000F4DC
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x000112F4 File Offset: 0x0000F4F4
		[XmlElement("oktodoublebook")]
		public bool OkToDoubleBook
		{
			get
			{
				return this.okToDoubleBook;
			}
			set
			{
				this.okToDoubleBook = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00011300 File Offset: 0x0000F500
		public DateTime? PotentialTestDate
		{
			get
			{
				bool flag = this.test == null;
				DateTime? result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = new DateTime?(this.test.StartDate.Date);
				}
				return result;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00011344 File Offset: 0x0000F544
		public DateTime? PotentialTestStartTime
		{
			get
			{
				bool flag = this.test == null;
				DateTime? result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = new DateTime?(this.test.StartDate);
				}
				return result;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00011380 File Offset: 0x0000F580
		public DateTime? PotentialTestEndTime
		{
			get
			{
				bool flag = this.test == null;
				DateTime? result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = new DateTime?(this.test.EndDate);
				}
				return result;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000113BC File Offset: 0x0000F5BC
		public string ToStringDebug()
		{
			DateTime dateTime = (this.PotentialTestDate == null) ? DateTime.MinValue : this.PotentialTestDate.Value;
			DateTime dateTime2 = (this.PotentialTestStartTime == null) ? DateTime.MinValue : this.PotentialTestStartTime.Value;
			DateTime dateTime3 = (this.PotentialTestEndTime == null) ? DateTime.MinValue : this.PotentialTestEndTime.Value;
			return string.Format("{0}.{1}[{2} . {3} to {4}]", new object[]
			{
				this.PotentialRoom,
				this.PotentialRoomPid.ToString(),
				dateTime.ToString("yyyy-MM-dd"),
				dateTime2.ToString("H:mm"),
				dateTime3.ToString("H:mm")
			});
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0001149A File Offset: 0x0000F69A
		public void AddMethodFoundNote(string note)
		{
			this.MethodFoundNotes.Add(new PotentialTestMethodFoundNote(note));
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000114B0 File Offset: 0x0000F6B0
		public void AddMethodFoundNote(string note, params string[] formatItems)
		{
			this.MethodFoundNotes.Add(new PotentialTestMethodFoundNote(string.Format(note, formatItems)));
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000114D8 File Offset: 0x0000F6D8
		public string PotentialRoom
		{
			get
			{
				bool flag = this.test == null || this.test.Room == null;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = this.test.Room.Title;
				}
				return result;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00011520 File Offset: 0x0000F720
		public int PotentialRoomPid
		{
			get
			{
				return (this.test == null || this.test.Room == null) ? 0 : this.test.Room.RoomId;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0001155A File Offset: 0x0000F75A
		public PotentialTest(int id, Test test, bool okToDoubleBook)
		{
			this.okToDoubleBook = okToDoubleBook;
			this.id = id;
			this.test = test;
			this.MethodFoundNotes = new List<PotentialTestMethodFoundNote>();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00011585 File Offset: 0x0000F785
		public PotentialTest()
		{
			this.okToDoubleBook = false;
			this.id = 0;
			this.test = null;
			this.MethodFoundNotes = new List<PotentialTestMethodFoundNote>();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000115B0 File Offset: 0x0000F7B0
		public int CompareTo(PotentialTest obj)
		{
			return this.test.CompareTo(obj.Test);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000115D3 File Offset: 0x0000F7D3
		public PotentialTest(int id, DateTime startDate, DateTime endDate, Room room, bool okToDoubleBook)
		{
			this.okToDoubleBook = okToDoubleBook;
			this.id = id;
			this.test = new Test(startDate, endDate, room);
			this.MethodFoundNotes = new List<PotentialTestMethodFoundNote>();
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00011608 File Offset: 0x0000F808
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00011620 File Offset: 0x0000F820
		[XmlElement("test")]
		public Test Test
		{
			get
			{
				return this.test;
			}
			set
			{
				this.test = value;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0001162C File Offset: 0x0000F82C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.test.StartDate.ToString("dddd MMMM d . h:mm tt"));
			stringBuilder.Append(" to ");
			stringBuilder.Append(this.test.EndDate.ToString("h:mm tt"));
			return stringBuilder.ToString();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011694 File Offset: 0x0000F894
		public static PotentialTest Find(List<PotentialTest> ptests, int id)
		{
			foreach (PotentialTest potentialTest in ptests)
			{
				bool flag = potentialTest.Id == id;
				if (flag)
				{
					return potentialTest;
				}
			}
			return null;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000116F8 File Offset: 0x0000F8F8
		public static string SerializeToXml(PotentialTest ptest)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(PotentialTest));
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				xmlSerializer.Serialize(stringWriter, ptest);
				string s = stringWriter.ToString();
				result = HttpUtility.HtmlEncode(s);
			}
			return result;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00011754 File Offset: 0x0000F954
		public static PotentialTest DeserializeFromXml(string xml)
		{
			string s = HttpUtility.HtmlDecode(xml);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(PotentialTest));
			PotentialTest result;
			using (StringReader stringReader = new StringReader(s))
			{
				result = (PotentialTest)xmlSerializer.Deserialize(stringReader);
			}
			return result;
		}

		// Token: 0x04000179 RID: 377
		private Test test;

		// Token: 0x0400017A RID: 378
		private int id;

		// Token: 0x0400017B RID: 379
		private bool okToDoubleBook;
	}
}
