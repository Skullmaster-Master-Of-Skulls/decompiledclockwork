using System;
using System.Data;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x02000084 RID: 132
	public class AuditLog
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x00024B3C File Offset: 0x00023B3C
		public static void LogAdmin(int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, int studentPid, int controlId, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			AuditLog.Log("AuditLogAdmin", pid, actionGroupCode, actionCode, studentPid, controlId, details, da, tripleDES);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00024B64 File Offset: 0x00023B64
		public static void LogAdmin(int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, int studentPid, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			AuditLog.Log("AuditLogAdmin", pid, actionGroupCode, actionCode, studentPid, 0, details, da, tripleDES);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00024B88 File Offset: 0x00023B88
		public static void LogAdmin(int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			AuditLog.Log("AuditLogAdmin", pid, actionGroupCode, actionCode, 0, 0, details, da, tripleDES);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00024BAC File Offset: 0x00023BAC
		public static void LogWeb(int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, int studentPid, int controlId, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			AuditLog.Log("AuditLogWeb", pid, actionGroupCode, actionCode, studentPid, controlId, details, da, tripleDES);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00024BD4 File Offset: 0x00023BD4
		public static void LogWeb(int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, int studentPid, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			AuditLog.Log("AuditLogWeb", pid, actionGroupCode, actionCode, studentPid, 0, details, da, tripleDES);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00024BF8 File Offset: 0x00023BF8
		public static void LogWeb(int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			AuditLog.Log("AuditLogWeb", pid, actionGroupCode, actionCode, 0, 0, details, da, tripleDES);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00024C1C File Offset: 0x00023C1C
		public static void LogStaff(int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, int studentPid, int controlId, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			AuditLog.Log("AuditLogStaff", pid, actionGroupCode, actionCode, studentPid, controlId, details, da, tripleDES);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00024C44 File Offset: 0x00023C44
		public static void LogStaff(int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, int studentPid, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			AuditLog.Log("AuditLogStaff", pid, actionGroupCode, actionCode, studentPid, 0, details, da, tripleDES);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00024C68 File Offset: 0x00023C68
		public static void LogStaff(int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			AuditLog.Log("AuditLogStaff", pid, actionGroupCode, actionCode, 0, 0, details, da, tripleDES);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00024C8C File Offset: 0x00023C8C
		private static void Log(string tableName, int pid, AuditLog.ActionGroupCode actionGroupCode, AuditLog.ActionCode actionCode, int studentPid, int controlId, string details, UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			if (AuditLog.databaseSupportsAuditLogs == 0)
			{
				AuditLog.databaseSupportsAuditLogs = (da.DoesTableExist("AuditLogStaff") ? 1 : -1);
			}
			if (AuditLog.databaseSupportsAuditLogs > 0)
			{
				da.SelectCommand.CommandText = "INSERT INTO " + tableName + " (actiongroupcode,actioncode,personid,studentpid,controlid,details) VALUES (@actiongroupcode,@actioncode,@personid,@studentpid,@controlid,@details)";
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@actiongroupcode", actionGroupCode);
				da.SelectCommand.Parameters.Add("@actioncode", actionCode);
				da.SelectCommand.Parameters.Add("@personid", pid);
				da.SelectCommand.Parameters.Add("@studentpid", studentPid);
				da.SelectCommand.Parameters.Add("@controlid", controlId);
				da.SelectCommand.Parameters.Add("@details", tripleDES.Encrypt(details));
				da.Fill(new DataTable());
			}
		}

		// Token: 0x04000363 RID: 867
		private static int databaseSupportsAuditLogs = 0;

		// Token: 0x02000085 RID: 133
		public enum ActionCode
		{
			// Token: 0x04000365 RID: 869
			Unknown,
			// Token: 0x04000366 RID: 870
			Admin_Settings_SettingsChange,
			// Token: 0x04000367 RID: 871
			Admin_Permissions_PermissionsChange,
			// Token: 0x04000368 RID: 872
			Admin_Login_Login,
			// Token: 0x04000369 RID: 873
			Admin_Forms_FormChange,
			// Token: 0x0400036A RID: 874
			Staff_StudentData_PerStudentDataChange = 10000,
			// Token: 0x0400036B RID: 875
			Staff_StudentData_PerAppointmentDataChange,
			// Token: 0x0400036C RID: 876
			Staff_StudentData_PerAnonymousDataChange,
			// Token: 0x0400036D RID: 877
			Staff_StudentData_PerDataDataChange,
			// Token: 0x0400036E RID: 878
			Staff_StudentData_AccommodationsDataChange,
			// Token: 0x0400036F RID: 879
			Staff_StudentData_SurveyDataChange
		}

		// Token: 0x02000086 RID: 134
		public enum ActionGroupCode
		{
			// Token: 0x04000371 RID: 881
			Unknown,
			// Token: 0x04000372 RID: 882
			AdminSettingsPermissions,
			// Token: 0x04000373 RID: 883
			AdminForms,
			// Token: 0x04000374 RID: 884
			AdminLogin,
			// Token: 0x04000375 RID: 885
			StudentData = 10000,
			// Token: 0x04000376 RID: 886
			Appointments
		}
	}
}
