using System;
using System.Data;
using System.Drawing;
using DynamicScreens;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI.SummaryManagement
{
	// Token: 0x0200005A RID: 90
	public class SummaryManagementView
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00017B14 File Offset: 0x00016B14
		public int ReportId
		{
			get
			{
				return this.reportId;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00017B2C File Offset: 0x00016B2C
		public int UpdateWhenScreenIsChanged_screennum
		{
			get
			{
				return this.updateWhenScreenIsChanged_screennum;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00017B44 File Offset: 0x00016B44
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00017B5C File Offset: 0x00016B5C
		public int Emailsentcidpm
		{
			get
			{
				return this.emailsentcidpm;
			}
			set
			{
				this.emailsentcidpm = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00017B68 File Offset: 0x00016B68
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00017B80 File Offset: 0x00016B80
		public Screen[] Screens
		{
			get
			{
				return this.screens;
			}
			set
			{
				this.screens = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00017B8C File Offset: 0x00016B8C
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00017BA4 File Offset: 0x00016BA4
		public SummaryManagementView(int id, string title, int reportId, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int updateWhenScreenIsChanged_screennum, params Screen[] screens)
		{
			this.updateWhenScreenIsChanged_screennum = updateWhenScreenIsChanged_screennum;
			this.id = id;
			this.screens = screens;
			this.title = title;
			this.reportId = reportId;
			this.da = da;
			this.tripleDES = tripleDES;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017C00 File Offset: 0x00016C00
		public SummaryManagementView(int id, string title, int reportId, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, params Screen[] screens)
		{
			this.updateWhenScreenIsChanged_screennum = 0;
			this.id = id;
			this.screens = screens;
			this.title = title;
			this.reportId = reportId;
			this.da = da;
			this.tripleDES = tripleDES;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00017C58 File Offset: 0x00016C58
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00017C70 File Offset: 0x00016C70
		public DataView DataSource
		{
			get
			{
				return this.dataView;
			}
			set
			{
				this.dataView = value;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00017C7C File Offset: 0x00016C7C
		public DataTable LoadData(DateTime startDate, DateTime endDate, int pid)
		{
			DataTable dataTable = new DataTable();
			DataTable result;
			if (this.id == 0)
			{
				result = this.dataView.Table;
			}
			else
			{
				if (this.id == 1)
				{
					dataTable.Columns.Add("Org Name");
					dataTable.Columns.Add("Org ID");
					dataTable.Columns.Add("Status");
					dataTable.Columns.Add("Todo Status");
					dataTable.Columns.Add("Contact type");
					dataTable.Columns.Add("Contact name");
					dataTable.Columns.Add("Contact phone");
					dataTable.Columns.Add("Contact email");
					dataTable.Rows.Add(new object[]
					{
						"Alphabet Club",
						"12443",
						"Active",
						"",
						"Student",
						"Jane Doe",
						"905-123-4566",
						"jane@doe.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"AXMN",
						"5664432",
						"Expired",
						"",
						"Staff",
						"John Smith",
						"905-444-4566",
						"jane@abc.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Flying Club",
						"59283",
						"Expired",
						"Review renewal app",
						"Student",
						"Jake Black",
						"905-213-4326",
						"jake@black.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Fight Club",
						"23948",
						"Active",
						"",
						"Faculty",
						"Ralph Line",
						"416-123-4566",
						"jane@doe.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Blue Group",
						"339281",
						"Expired",
						"Review renewal app",
						"Student",
						"Harry Hamilton",
						"416-555-4445",
						"abc@def.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Book Club",
						"30239",
						"Expired",
						"Review renewal app",
						"Student",
						"George Yellow",
						"519-345-3213",
						"george1@george.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Movie group",
						"2309432049",
						"Expired",
						"",
						"Student",
						"Bart Black",
						"416-805-8057",
						"bartb@yaooh.com"
					});
				}
				else if (this.id == 2)
				{
					dataTable.Columns.Add("Org Name");
					dataTable.Columns.Add("Org ID");
					dataTable.Columns.Add("Status");
					dataTable.Columns.Add("Todo Status");
					dataTable.Columns.Add("Event Name");
					dataTable.Columns.Add("Event Date");
					dataTable.Columns.Add("Additional dates");
					dataTable.Columns.Add("Submitted");
					dataTable.Columns.Add("UTSC attendance");
					dataTable.Columns.Add("NonUTSC attendance");
					dataTable.Columns.Add("Amplified Sound");
					dataTable.Columns.Add("Alcohol");
					dataTable.Columns.Add("Non-UTSC Community");
					dataTable.Columns.Add("Classroom space required");
					dataTable.Columns.Add("Meeting Space required");
					dataTable.Columns.Add("Food / Drink");
					dataTable.Columns.Add("Facilities Management support required");
					dataTable.Columns.Add("Audio Visual Services required");
					dataTable.Columns.Add("Checked");
					dataTable.Columns.Add("Contact type");
					dataTable.Columns.Add("Contact name");
					dataTable.Columns.Add("Contact phone");
					dataTable.Columns.Add("Contact email");
					dataTable.Rows.Add(new object[]
					{
						"Alphabet Club",
						"12443",
						"Active",
						"",
						"Concert",
						"2008-08-08",
						"2008-08-09 to 2008-08-15",
						"2008-07-22",
						"21",
						"100",
						"Yes",
						"No",
						"Yes",
						"Yes",
						"No",
						"Yes",
						"Yes",
						"Yes",
						"No",
						"Student",
						"Jane Doe",
						"905-123-4566",
						"jane@doe.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"AXMN",
						"5664432",
						"Expired",
						"",
						"Fun Play",
						"2008-08-08",
						"",
						"2008-07-22",
						"21",
						"100",
						"No",
						"Yes",
						"Yes",
						"Yes",
						"No",
						"Yes",
						"No",
						"Yes",
						"Yes",
						"Staff",
						"John Smith",
						"905-444-4566",
						"jane@abc.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Flying Club",
						"59283",
						"Expired",
						"Review renewal app",
						"Campfire sing marathon",
						"2008-09-03",
						"",
						"2008-06-22",
						"55",
						"0",
						"No",
						"Yes",
						"Yes",
						"Yes",
						"No",
						"Yes",
						"Yes",
						"No",
						"Yes",
						"Student",
						"Jake Black",
						"905-213-4326",
						"jake@black.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Fight Club",
						"23948",
						"Active",
						"",
						"Movie night",
						"2008-11-23",
						"",
						"2008-07-11",
						"1000",
						"0",
						"No",
						"Yes",
						"No",
						"Yes",
						"Yes",
						"Yes",
						"Yes",
						"No",
						"Yes",
						"Faculty",
						"Ralph Line",
						"416-123-4566",
						"jane@doe.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Blue Group",
						"339281",
						"Expired",
						"Review renewal app",
						"Pool Party",
						"2008-12-24",
						"",
						"2008-07-03",
						"0",
						"23",
						"No",
						"Yes",
						"Yes",
						"Yes",
						"No",
						"Yes",
						"No",
						"Yes",
						"Yes",
						"Student",
						"Harry Hamilton",
						"416-555-4445",
						"abc@def.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Book Club",
						"30239",
						"Expired",
						"Review renewal app",
						"Book readings",
						"2008-07-30",
						"",
						"2008-07-24",
						"0",
						"2",
						"No",
						"No",
						"Yes",
						"No",
						"Yes",
						"Yes",
						"Yes",
						"Yes",
						"Yes",
						"Student",
						"George Yellow",
						"519-345-3213",
						"george1@george.com"
					});
					dataTable.Rows.Add(new object[]
					{
						"Movie group",
						"2309432049",
						"Expired",
						"",
						"Nap time",
						"2008-08-22",
						"",
						"2008-07-21",
						"50",
						"50",
						"No",
						"Yes",
						"Yes",
						"No",
						"Yes",
						"No",
						"Yes",
						"Yes",
						"Yes",
						"Student",
						"Bart Black",
						"416-805-8057",
						"bartb@yaooh.com"
					});
				}
				else if (this.id == 3)
				{
					dataTable.Columns.Add("Name");
					dataTable.Columns.Add("ID");
					dataTable.Columns.Add("Credits category 1");
					dataTable.Columns.Add("Credits category 2");
					dataTable.Columns.Add("Credits category 3");
					dataTable.Columns.Add("Credits category 4");
					dataTable.Columns.Add("Credits category 5");
					dataTable.Columns.Add("Credits type electives");
					dataTable.Columns.Add("Credits type core");
					dataTable.Columns.Add("Credits type specialist");
					dataTable.Columns.Add("Certificate 1 granted");
					dataTable.Columns.Add("Certificate 2 granted");
					dataTable.Columns.Add("Certificate 3 granted");
					dataTable.Rows.Add(new object[]
					{
						"Bob Smith",
						"12345655",
						"0",
						"0",
						"0",
						"0",
						"0",
						"2",
						"0",
						"5",
						"Yes",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Jane Doe",
						"555432",
						"0",
						"0",
						"3",
						"0",
						"0",
						"0",
						"0",
						"0",
						"",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Mike Dinunzio",
						"55333221",
						"1",
						"2",
						"0",
						"0",
						"0",
						"0",
						"0",
						"0",
						"",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"John Smith",
						"3322111",
						"0",
						"0",
						"0",
						"0",
						"0",
						"0",
						"0",
						"4",
						"",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Michelle Richardson",
						"3253321",
						"0",
						"0",
						"0",
						"0",
						"3",
						"3",
						"0",
						"0",
						"",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Nick Mayers",
						"33235421",
						"0",
						"0",
						"2",
						"0",
						"0",
						"0",
						"2",
						"0",
						"",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Ralph Roads",
						"32344234",
						"0",
						"1",
						"2",
						"3",
						"4",
						"0",
						"0",
						"2",
						"Yes",
						"Yes",
						"Yes"
					});
				}
				else if (this.id == 4)
				{
					dataTable.Columns.Add("Name");
					dataTable.Columns.Add("Student #");
					dataTable.Columns.Add("Status");
					dataTable.Columns.Add("Action required");
					dataTable.Columns.Add("Due date");
					dataTable.Columns.Add("Assigned notetaker");
					dataTable.Columns.Add("Subject");
					dataTable.Columns.Add("Course");
					dataTable.Columns.Add("Instructor");
					dataTable.Columns.Add("Instructor Email");
					dataTable.Columns.Add("Instructor Phone");
					dataTable.Rows.Add(new object[]
					{
						"Bob Smith",
						"2343242",
						"ASSIGNED",
						"4. none",
						"",
						"Al Richardson . 1233321",
						"Biology",
						"1244",
						"Smith, R.",
						"smithr@school.com",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Angela Roberts",
						"2316532",
						"Gave up",
						"4. none",
						"",
						"",
						"Mathematics",
						"3221",
						"Jones, B.",
						"sdf@school.com",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Jane Doe",
						"2344563",
						"ASSIGNED",
						"4. none",
						"",
						"Jane Doe . 333999228",
						"Art",
						"5532",
						"Richards, Q.",
						"332ewewsd@school.com",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Richard Bold",
						"3244113",
						"",
						"1. Review",
						"",
						"",
						"Mathematics",
						"2231",
						"Jane, F.",
						"ffssd@school.com",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Alex Ron",
						"2344322",
						"Pending",
						"2. Contact1 instructor",
						"yesterday",
						"",
						"Chemistry",
						"4332",
						"Roades, B.",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Michelle Atkinson",
						"234432",
						"",
						"1. Review",
						"",
						"",
						"Astronomy",
						"3325",
						"Smith, R.",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Joan Rivers",
						"2343332",
						"Pending",
						"2. Contact1 instructor",
						"Aug 3",
						"",
						"Children's literature",
						"4321",
						"Smith, S.",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Earl Green",
						"3243243",
						"ASSIGNED",
						"4. none",
						"",
						"Jane Doe . 333999228",
						"Biology",
						"5543",
						"Richards, T.",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Samantha Click",
						"324324",
						"ASSIGNED",
						"4. none",
						"",
						"Jane Doe . 333999228",
						"History",
						"2213",
						"Jones, D.",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Bart Roades",
						"3243243",
						"ASSIGNED",
						"4. none",
						"",
						"Jane Doe . 333999228",
						"Geography",
						"3342",
						"Richards, S.",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Ronald Greene",
						"3232432",
						"Pending",
						"3. Contact2 instructor",
						"Tomorrow",
						"",
						"Political Science",
						"3322",
						"Jones, F.",
						"",
						""
					});
					dataTable.Rows.Add(new object[]
					{
						"Sarah Brickles",
						"323433",
						"Gave up",
						"4. none",
						"",
						"",
						"Computer Science",
						"7765",
						"Jacke, R.",
						"",
						""
					});
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x040001D4 RID: 468
		private int id;

		// Token: 0x040001D5 RID: 469
		private string title;

		// Token: 0x040001D6 RID: 470
		private int reportId;

		// Token: 0x040001D7 RID: 471
		private Image buttonImage;

		// Token: 0x040001D8 RID: 472
		private int updateWhenScreenIsChanged_screennum = 0;

		// Token: 0x040001D9 RID: 473
		private int emailsentcidpm = 0;

		// Token: 0x040001DA RID: 474
		private Screen[] screens;

		// Token: 0x040001DB RID: 475
		private UnivDataAdapter da;

		// Token: 0x040001DC RID: 476
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040001DD RID: 477
		private DataView dataView;
	}
}
