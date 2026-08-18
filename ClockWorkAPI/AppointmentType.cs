using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000081 RID: 129
	public class AppointmentType
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00024464 File Offset: 0x00023464
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0002447C File Offset: 0x0002347C
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00024488 File Offset: 0x00023488
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x000244A0 File Offset: 0x000234A0
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

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x000244AC File Offset: 0x000234AC
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x000244C4 File Offset: 0x000234C4
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

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000244D0 File Offset: 0x000234D0
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x000244E8 File Offset: 0x000234E8
		public int AppTypeId
		{
			get
			{
				return this.appTypeId;
			}
			set
			{
				this.appTypeId = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x000244F4 File Offset: 0x000234F4
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0002450C File Offset: 0x0002350C
		public List<int> PerAppScreenNums
		{
			get
			{
				return this.perAppScreenNums;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00024524 File Offset: 0x00023524
		public int PerJustAppScreenNum
		{
			get
			{
				return this.perJustAppScreenNum;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0002453C File Offset: 0x0002353C
		public AppointmentType()
		{
			this.color = Color.LightBlue;
			this.title = "New appointment type";
			this.caption = this.title;
			this.description = "Describe your appointment type here.";
			this.appTypeId = 0;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0002457B File Offset: 0x0002357B
		public AppointmentType(int appTypeId, string title, string description, Color color)
		{
			this.color = color;
			this.appTypeId = appTypeId;
			this.title = title;
			this.caption = title;
			this.description = description;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000245AC File Offset: 0x000235AC
		public AppointmentType(DataRow dr)
		{
			int argb = (dr["defaultColour"] == DBNull.Value) ? 0 : ((int)dr["defaultColour"]);
			this.color = Color.FromArgb(argb);
			this.appTypeId = (int)dr["apptypeid"];
			this.caption = dr["caption"].ToString();
			this.title = dr["description"].ToString();
			this.groupTitle = ((dr["title"] == DBNull.Value) ? "" : ((string)dr["title"]));
			this.appointmentTypeGroupId = ((dr["appointmenttypegroupid"] == DBNull.Value) ? 0 : ((int)dr["appointmenttypegroupid"]));
			string commaSeparatedNumbers = dr["perappscreennumsfortabs"].ToString();
			this.perAppScreenNums = Utility.IntListFromString(commaSeparatedNumbers);
			this.perJustAppScreenNum = ((dr["perjustappscreennum"] == DBNull.Value) ? 0 : ((int)dr["perjustappscreennum"]));
			this.description = "And this is a description of what this appointment type is actually supposed to be used for.";
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000246E4 File Offset: 0x000236E4
		public static List<AppointmentType> LoadAppointmentTypes(UnivDataAdapter da, bool includeInactiveItems)
		{
			return AppointmentType.LoadAppointmentTypes(da, false, null, includeInactiveItems);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00024700 File Offset: 0x00023700
		public static List<AppointmentType> LoadAppointmentTypes(UnivDataAdapter da, List<int> restrictedAppTypeIds, bool includeInactiveItems)
		{
			return AppointmentType.LoadAppointmentTypes(da, true, restrictedAppTypeIds, includeInactiveItems);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0002471C File Offset: 0x0002371C
		private static List<AppointmentType> LoadAppointmentTypes(UnivDataAdapter da, bool restrictByAppTypeId, List<int> restrictedAppTypeIds, bool includeInactiveItems)
		{
			string commandText = "SELECT at.apptypeid,at.[description],at.defaultColour,at.isworkshop,at.iscourse\r\n        ,at.defaulticon,at.perappscreennumsfortabs,at.perjustappscreennum,at.iconindex\r\n        ,coalesce(atg.title + ': ', '') + at.description AS caption\r\n        ,at.appointmenttypegroupid,atg.title\r\nFROM appointmenttypes at LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\nWHERE   (@restrictedapptypeids='' OR at.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@restrictedapptypeids,',')))\r\n        AND (@includeinactive=1 OR at.isactive=1)\r\nORDER BY atg.title,at.description";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			if (restrictedAppTypeIds == null || restrictedAppTypeIds.Count < 1)
			{
				restrictedAppTypeIds = new List<int>();
				restrictedAppTypeIds.Add(0);
			}
			da.SelectCommand.Parameters.Add("@restrictedapptypeids", restrictByAppTypeId ? Utility.ListToString(restrictedAppTypeIds) : "");
			da.SelectCommand.Parameters.Add("@includeinactive", includeInactiveItems);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			List<AppointmentType> list = new List<AppointmentType>(dataTable.Rows.Count);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				AppointmentType item = new AppointmentType(dr);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x04000358 RID: 856
		private Color color;

		// Token: 0x04000359 RID: 857
		private string title;

		// Token: 0x0400035A RID: 858
		private string description;

		// Token: 0x0400035B RID: 859
		private int appTypeId;

		// Token: 0x0400035C RID: 860
		private int appointmentTypeGroupId;

		// Token: 0x0400035D RID: 861
		private string caption;

		// Token: 0x0400035E RID: 862
		private string groupTitle;

		// Token: 0x0400035F RID: 863
		private List<int> perAppScreenNums;

		// Token: 0x04000360 RID: 864
		private int perJustAppScreenNum;

		// Token: 0x04000361 RID: 865
		private bool isWorkshop;

		// Token: 0x04000362 RID: 866
		private bool isCourse;
	}
}
