using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000543 RID: 1347
	[Serializable]
	public class PotentialTest
	{
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x0002E421 File Offset: 0x0002C621
		// (set) Token: 0x06002B1D RID: 11037 RVA: 0x0002E429 File Offset: 0x0002C629
		public virtual List<PotentialTestMethodFoundNote> MethodFoundNotes { get; set; }

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x0002E434 File Offset: 0x0002C634
		public string MethodFoundNotesSummary
		{
			get
			{
				return string.Join(Environment.NewLine, this.MethodFoundNotes.ConvertAll<string>((PotentialTestMethodFoundNote n) => n.ToString()).ToArray());
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x0002E480 File Offset: 0x0002C680
		// (set) Token: 0x06002B20 RID: 11040 RVA: 0x0002E498 File Offset: 0x0002C698
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

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x0002E4A4 File Offset: 0x0002C6A4
		// (set) Token: 0x06002B22 RID: 11042 RVA: 0x0002E4BC File Offset: 0x0002C6BC
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

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x0002E4C8 File Offset: 0x0002C6C8
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

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x0002E50C File Offset: 0x0002C70C
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

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x0002E548 File Offset: 0x0002C748
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

		// Token: 0x06002B26 RID: 11046 RVA: 0x0002E584 File Offset: 0x0002C784
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

		// Token: 0x06002B27 RID: 11047 RVA: 0x0002E662 File Offset: 0x0002C862
		public void AddMethodFoundNote(string note)
		{
			this.MethodFoundNotes.Add(new PotentialTestMethodFoundNote(note));
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0002E678 File Offset: 0x0002C878
		public void AddMethodFoundNote(string note, params string[] formatItems)
		{
			this.MethodFoundNotes.Add(new PotentialTestMethodFoundNote(string.Format(note, formatItems)));
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x0002E6A0 File Offset: 0x0002C8A0
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

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x0002E6E8 File Offset: 0x0002C8E8
		public int PotentialRoomPid
		{
			get
			{
				return (this.test == null || this.test.Room == null) ? 0 : this.test.Room.RoomId;
			}
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x0002E722 File Offset: 0x0002C922
		public PotentialTest(int id, Test test, bool okToDoubleBook)
		{
			this.okToDoubleBook = okToDoubleBook;
			this.id = id;
			this.test = test;
			this.MethodFoundNotes = new List<PotentialTestMethodFoundNote>();
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x0002E74D File Offset: 0x0002C94D
		public PotentialTest()
		{
			this.okToDoubleBook = false;
			this.id = 0;
			this.test = null;
			this.MethodFoundNotes = new List<PotentialTestMethodFoundNote>();
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x0002E778 File Offset: 0x0002C978
		public int CompareTo(PotentialTest obj)
		{
			return this.test.CompareTo(obj.Test);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x0002E79B File Offset: 0x0002C99B
		public PotentialTest(int id, DateTime startDate, DateTime endDate, Room room, bool okToDoubleBook)
		{
			this.okToDoubleBook = okToDoubleBook;
			this.id = id;
			this.test = new Test(startDate, endDate, room);
			this.MethodFoundNotes = new List<PotentialTestMethodFoundNote>();
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x0002E7D0 File Offset: 0x0002C9D0
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x0002E7E8 File Offset: 0x0002C9E8
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

		// Token: 0x06002B31 RID: 11057 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.test.StartDate.ToString("dddd MMMM d . h:mm tt"));
			stringBuilder.Append(" to ");
			stringBuilder.Append(this.test.EndDate.ToString("h:mm tt"));
			return stringBuilder.ToString();
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x0002E85C File Offset: 0x0002CA5C
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

		// Token: 0x04001EA4 RID: 7844
		private Test test;

		// Token: 0x04001EA5 RID: 7845
		private int id;

		// Token: 0x04001EA6 RID: 7846
		private bool okToDoubleBook;
	}
}
