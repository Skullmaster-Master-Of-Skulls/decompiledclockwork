using System;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;

// Token: 0x0200002D RID: 45
[ComVisible(false)]
internal static class Bid
{
	// Token: 0x060000AE RID: 174 RVA: 0x00036140 File Offset: 0x00035540
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void PoolerTrace(string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00036184 File Offset: 0x00035584
	internal static void PoolerTrace(string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000361C8 File Offset: 0x000355C8
	internal static void PoolerTrace(string fmtPrintfW, int a1, int a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0003620C File Offset: 0x0003560C
	internal static void PoolerTrace(string fmtPrintfW, int a1, int a2, int a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00036254 File Offset: 0x00035654
	internal static void PoolerTrace(string fmtPrintfW, int a1, Exception a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2.ToString());
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0003629C File Offset: 0x0003569C
	internal static void PoolerScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x000362E8 File Offset: 0x000356E8
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, fmtPrintfW2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00036334 File Offset: 0x00035734
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00036380 File Offset: 0x00035780
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, string fmtPrintfW2, string fmtPrintfW3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, fmtPrintfW2, fmtPrintfW3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000363CC File Offset: 0x000357CC
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, fmtPrintfW2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00036418 File Offset: 0x00035818
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00036464 File Offset: 0x00035864
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, string fmtPrintfW2, string fmtPrintfW3, string fmtPrintfW4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, fmtPrintfW2, fmtPrintfW3, fmtPrintfW4);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x000364B4 File Offset: 0x000358B4
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string fmtPrintfW2, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, fmtPrintfW2, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00036504 File Offset: 0x00035904
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string fmtPrintfW2, string fmtPrintfW3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, fmtPrintfW2, fmtPrintfW3, a4);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00036554 File Offset: 0x00035954
	internal static void NotificationsTrace(string fmtPrintfW)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW);
		}
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00036594 File Offset: 0x00035994
	internal static void NotificationsTrace(string fmtPrintfW, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, fmtPrintfW2);
		}
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000365D8 File Offset: 0x000359D8
	internal static void NotificationsTrace(string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x0003661C File Offset: 0x00035A1C
	internal static void NotificationsTrace(string fmtPrintfW, bool a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00036660 File Offset: 0x00035A60
	internal static void NotificationsTrace(string fmtPrintfW, string fmtPrintfW2, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, fmtPrintfW2, a1);
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x000366A4 File Offset: 0x00035AA4
	internal static void NotificationsTrace(string fmtPrintfW, int a1, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, fmtPrintfW2);
		}
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x000366E8 File Offset: 0x00035AE8
	internal static void NotificationsTrace(string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0003672C File Offset: 0x00035B2C
	internal static void NotificationsTrace(string fmtPrintfW, int a1, bool a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00036770 File Offset: 0x00035B70
	internal static void NotificationsTrace(string fmtPrintfW, string a1, string a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x000367B4 File Offset: 0x00035BB4
	internal static void NotificationsTrace(string fmtPrintfW, string fmtPrintfW2, string fmtPrintfW3, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, fmtPrintfW2, fmtPrintfW3, (long)a1);
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x000367F8 File Offset: 0x00035BF8
	internal static void NotificationsTrace(string fmtPrintfW, bool a1, string fmtPrintfW2, string fmtPrintfW3, string fmtPrintfW4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, fmtPrintfW2, fmtPrintfW3, fmtPrintfW4);
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00036840 File Offset: 0x00035C40
	internal static void NotificationsTrace(string fmtPrintfW, int a1, string fmtPrintfW2, string fmtPrintfW3, string fmtPrintfW4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, fmtPrintfW2, fmtPrintfW3, fmtPrintfW4);
		}
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00036888 File Offset: 0x00035C88
	internal static void CorrelationTrace(string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Correlation) != Bid.ApiGroup.Off && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			ActivityCorrelator.ActivityId activityId = ActivityCorrelator.Next();
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, activityId.ToString());
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x000368E0 File Offset: 0x00035CE0
	internal static void CorrelationTrace(string fmtPrintfW)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Correlation) != Bid.ApiGroup.Off && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			ActivityCorrelator.ActivityId activityId = ActivityCorrelator.Next();
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, activityId.ToString());
		}
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00036938 File Offset: 0x00035D38
	internal static void CorrelationTrace(string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Correlation) != Bid.ApiGroup.Off && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			ActivityCorrelator.ActivityId activityId = ActivityCorrelator.Next();
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, activityId.ToString());
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00036990 File Offset: 0x00035D90
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void TraceSqlReturn(string fmtPrintfW, ODBC32.RetCode a1)
	{
		if ((a1 != ODBC32.RetCode.SUCCESS || (Bid.modFlags & Bid.ApiGroup.StatusOk) != Bid.ApiGroup.Off) && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1);
		}
	}

	// Token: 0x060000CC RID: 204 RVA: 0x000369DC File Offset: 0x00035DDC
	internal static void TraceSqlReturn(string fmtPrintfW, ODBC32.RetCode a1, string a2)
	{
		if ((a1 != ODBC32.RetCode.SUCCESS || (Bid.modFlags & Bid.ApiGroup.StatusOk) != Bid.ApiGroup.Off) && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1, a2);
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00036A28 File Offset: 0x00035E28
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void Trace(string fmtPrintfW, OleDbHResult a1)
	{
		if ((a1 != OleDbHResult.S_OK || (Bid.modFlags & Bid.ApiGroup.StatusOk) != Bid.ApiGroup.Off) && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1);
		}
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00036A74 File Offset: 0x00035E74
	internal static void Trace(string fmtPrintfW, OleDbHResult a1, string a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1, a2);
		}
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00036AB4 File Offset: 0x00035EB4
	internal static void Trace(string fmtPrintfW, OleDbHResult a1, IntPtr a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1, a2);
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00036AF4 File Offset: 0x00035EF4
	internal static void Trace(string fmtPrintfW, OleDbHResult a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1, a2);
		}
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00036B34 File Offset: 0x00035F34
	internal static void Trace(string fmtPrintfW, string a1, string a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00036B74 File Offset: 0x00035F74
	internal static void Trace(string fmtPrintfW, int a1, string a2, bool a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00036BB4 File Offset: 0x00035FB4
	internal static void Trace(string fmtPrintfW, int a1, int a2, string a3, string a4, int a5)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5);
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00036BF8 File Offset: 0x00035FF8
	internal static void Trace(string fmtPrintfW, int a1, int a2, long a3, uint a4, int a5, uint a6, uint a7)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6, a7);
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00036C40 File Offset: 0x00036040
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, Guid a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2.ToString());
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00036C94 File Offset: 0x00036094
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00036CE0 File Offset: 0x000360E0
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, bool a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00036D2C File Offset: 0x0003612C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void Trace(string fmtPrintfW, int a1, string a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00036D6C File Offset: 0x0003616C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void Trace(string fmtPrintfW, IntPtr a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00036DAC File Offset: 0x000361AC
	internal static void Trace(string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00036DEC File Offset: 0x000361EC
	internal static void Trace(string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00036E2C File Offset: 0x0003622C
	internal static void Trace(string fmtPrintfW, int a1, IntPtr a2, IntPtr a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00036E6C File Offset: 0x0003626C
	internal static void Trace(string fmtPrintfW, int a1, IntPtr a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00036EAC File Offset: 0x000362AC
	internal static void Trace(string fmtPrintfW, int a1, string a2, string a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00036EEC File Offset: 0x000362EC
	internal static void Trace(string fmtPrintfW, int a1, string a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00036F2C File Offset: 0x0003632C
	internal static void Trace(string fmtPrintfW, int a1, string a2, string a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00036F70 File Offset: 0x00036370
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3, string a4, string a5, int a6)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6);
		}
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00036FB8 File Offset: 0x000363B8
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00036FF8 File Offset: 0x000363F8
	internal static void Trace(string fmtPrintfW, int a1, bool a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00037038 File Offset: 0x00036438
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x0003707C File Offset: 0x0003647C
	internal static void Trace(string fmtPrintfW, int a1, int a2, bool a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x000370BC File Offset: 0x000364BC
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3, int a4, int a5, int a6, int a7)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6, a7);
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00037104 File Offset: 0x00036504
	internal static void Trace(string fmtPrintfW, int a1, string a2, int a3, int a4, bool a5)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5);
		}
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00037148 File Offset: 0x00036548
	internal static void Trace(string fmtPrintfW, int a1, long a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00037188 File Offset: 0x00036588
	internal static void Trace(string fmtPrintfW, int a1, int a2, long a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060000EA RID: 234 RVA: 0x000371C8 File Offset: 0x000365C8
	internal static void Trace(string fmtPrintfW, int a1, string a2, string a3, string a4, int a5, long a6)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6);
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00037210 File Offset: 0x00036610
	internal static void Trace(string fmtPrintfW, int a1, long a2, int a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00037254 File Offset: 0x00036654
	internal static void Trace(string fmtPrintfW, int a1, int a2, long a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00037298 File Offset: 0x00036698
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3, int a4, string a5, string a6, string a7, int a8)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6, a7, a8);
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x000372E4 File Offset: 0x000366E4
	internal static void Trace(string fmtPrintfW, int a1, int a2, string a3, string a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00037328 File Offset: 0x00036728
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00037370 File Offset: 0x00036770
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, bool a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x000373B8 File Offset: 0x000367B8
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, string a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00037404 File Offset: 0x00036804
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string a2, bool a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00037450 File Offset: 0x00036850
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, bool a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x0003749C File Offset: 0x0003689C
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, int a3, string a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3, a4);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x000374E8 File Offset: 0x000368E8
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00037534 File Offset: 0x00036934
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, bool a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3, a4);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x060000F7 RID: 247 RVA: 0x00037580 File Offset: 0x00036980
	internal static bool TraceOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Trace) > Bid.ApiGroup.Off;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x060000F8 RID: 248 RVA: 0x00037598 File Offset: 0x00036998
	internal static bool ScopeOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Scope) > Bid.ApiGroup.Off;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x060000F9 RID: 249 RVA: 0x000375B0 File Offset: 0x000369B0
	internal static bool AdvancedOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Advanced) > Bid.ApiGroup.Off;
		}
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000375CC File Offset: 0x000369CC
	internal static bool IsOn(Bid.ApiGroup flag)
	{
		return (Bid.modFlags & flag) > Bid.ApiGroup.Off;
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060000FB RID: 251 RVA: 0x000375E4 File Offset: 0x000369E4
	internal static IntPtr NoData
	{
		get
		{
			return Bid.__noData;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000FC RID: 252 RVA: 0x000375F8 File Offset: 0x000369F8
	internal static IntPtr ID
	{
		get
		{
			return Bid.modID;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060000FD RID: 253 RVA: 0x0003760C File Offset: 0x00036A0C
	internal static bool IsInitialized
	{
		get
		{
			return Bid.modID != Bid.NoData;
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00037628 File Offset: 0x00036A28
	internal static void PutStr(string str)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)0U, str);
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00037668 File Offset: 0x00036A68
	internal static void Trace(string strConst)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, strConst);
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x000376A4 File Offset: 0x00036AA4
	internal static void TraceEx(uint flags, string strConst)
	{
		if (Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, (UIntPtr)flags, strConst);
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x000376D8 File Offset: 0x00036AD8
	internal static void Trace(string fmtPrintfW, string a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00037718 File Offset: 0x00036B18
	internal static void TraceEx(uint flags, string fmtPrintfW, string a1)
	{
		if (Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, (UIntPtr)flags, fmtPrintfW, a1);
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00037750 File Offset: 0x00036B50
	internal static void ScopeLeave(ref IntPtr hScp)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			if (hScp != Bid.NoData)
			{
				Bid.NativeMethods.ScopeLeave(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, ref hScp);
				return;
			}
		}
		else
		{
			hScp = Bid.NoData;
		}
	}

	// Token: 0x06000104 RID: 260 RVA: 0x000377A4 File Offset: 0x00036BA4
	internal static void ScopeEnter(out IntPtr hScp, string strConst)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, strConst);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x000377EC File Offset: 0x00036BEC
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00037834 File Offset: 0x00036C34
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0003787C File Offset: 0x00036C7C
	internal static void TraceBin(string constStrHeader, byte[] buff, ushort length)
	{
		if (Bid.modID != Bid.NoData)
		{
			if (constStrHeader != null && constStrHeader.Length > 0)
			{
				Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)1U, constStrHeader);
			}
			if ((ushort)buff.Length < length)
			{
				length = (ushort)buff.Length;
			}
			Bid.NativeMethods.TraceBin(Bid.modID, UIntPtr.Zero, (UIntPtr)16U, "<Trace|BLOB> %p %u\n", buff, (uint)length);
		}
	}

	// Token: 0x06000108 RID: 264 RVA: 0x000378E8 File Offset: 0x00036CE8
	internal static void TraceBinEx(byte[] buff, ushort length)
	{
		if (Bid.modID != Bid.NoData)
		{
			if ((ushort)buff.Length < length)
			{
				length = (ushort)buff.Length;
			}
			Bid.NativeMethods.TraceBin(Bid.modID, UIntPtr.Zero, (UIntPtr)16U, "<Trace|BLOB> %p %u\n", buff, (uint)length);
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00037930 File Offset: 0x00036D30
	internal static Bid.ApiGroup SetApiGroupBits(Bid.ApiGroup mask, Bid.ApiGroup bits)
	{
		object setBitsLock = Bid._setBitsLock;
		Bid.ApiGroup result;
		lock (setBitsLock)
		{
			Bid.ApiGroup apiGroup = Bid.modFlags;
			if (mask != Bid.ApiGroup.Off)
			{
				Bid.modFlags ^= ((bits ^ apiGroup) & mask);
			}
			result = apiGroup;
		}
		return result;
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00037994 File Offset: 0x00036D94
	internal static bool AddMetaText(string metaStr)
	{
		if (Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.AddMetaText(Bid.modID, Bid.DefaultCmdSpace, Bid.CtlCmd.AddMetaText, IntPtr.Zero, metaStr, IntPtr.Zero);
		}
		return true;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x000379D4 File Offset: 0x00036DD4
	[Conditional("DEBUG")]
	internal static void DTRACE(string strConst)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)1U, strConst);
		}
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00037A14 File Offset: 0x00036E14
	[Conditional("DEBUG")]
	internal static void DTRACE(string clrFormatString, params object[] args)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)1U, string.Format(CultureInfo.CurrentCulture, clrFormatString, args));
		}
	}

	// Token: 0x0600010D RID: 269 RVA: 0x00037A5C File Offset: 0x00036E5C
	[Conditional("DEBUG")]
	internal static void DASSERT(bool condition)
	{
		if (!condition)
		{
			System.Diagnostics.Trace.Assert(false);
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00037A74 File Offset: 0x00036E74
	private static void deterministicStaticInit()
	{
		Bid.__noData = (IntPtr)(-1);
		Bid.__defaultCmdSpace = (IntPtr)(-1);
		Bid.modFlags = Bid.ApiGroup.Off;
		Bid.modIdentity = string.Empty;
		Bid.ctrlCallback = new Bid.CtrlCB(Bid.SetApiGroupBits);
		Bid.cookieObject = new Bid.BindingCookie();
		Bid.hCookie = GCHandle.Alloc(Bid.cookieObject, GCHandleType.Pinned);
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600010F RID: 271 RVA: 0x00037AD4 File Offset: 0x00036ED4
	internal static IntPtr DefaultCmdSpace
	{
		get
		{
			return Bid.__defaultCmdSpace;
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00037AE8 File Offset: 0x00036EE8
	private static string getIdentity(Module mod)
	{
		object[] customAttributes = mod.GetCustomAttributes(typeof(BidIdentityAttribute), true);
		string result;
		if (customAttributes.Length == 0)
		{
			result = mod.Name;
		}
		else
		{
			result = ((BidIdentityAttribute)customAttributes[0]).IdentityString;
		}
		return result;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00037B24 File Offset: 0x00036F24
	private static string getAppDomainFriendlyName()
	{
		string text = AppDomain.CurrentDomain.FriendlyName;
		if (text == null || text.Length <= 0)
		{
			text = "AppDomain.H" + AppDomain.CurrentDomain.GetHashCode().ToString();
		}
		return VersioningHelper.MakeVersionSafeName(text, ResourceScope.Machine, ResourceScope.AppDomain);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00037B70 File Offset: 0x00036F70
	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	private static string getModulePath(Module mod)
	{
		return mod.FullyQualifiedName;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00037B84 File Offset: 0x00036F84
	private static void initEntryPoint()
	{
		Bid.NativeMethods.DllBidInitialize();
		Module manifestModule = Assembly.GetExecutingAssembly().ManifestModule;
		Bid.modIdentity = Bid.getIdentity(manifestModule);
		Bid.modID = Bid.NoData;
		string appDomainFriendlyName = Bid.getAppDomainFriendlyName();
		Bid.BIDEXTINFO bidextinfo = new Bid.BIDEXTINFO(Marshal.GetHINSTANCE(manifestModule), Bid.getModulePath(manifestModule), appDomainFriendlyName, Bid.hCookie.AddrOfPinnedObject());
		Bid.NativeMethods.DllBidEntryPoint(ref Bid.modID, 9210, Bid.modIdentity, 3489660928U, ref Bid.modFlags, Bid.ctrlCallback, ref bidextinfo, IntPtr.Zero, IntPtr.Zero);
		if (Bid.modID != Bid.NoData)
		{
			object[] customAttributes = manifestModule.GetCustomAttributes(typeof(BidMetaTextAttribute), true);
			foreach (object obj in customAttributes)
			{
				Bid.AddMetaText(((BidMetaTextAttribute)obj).MetaText);
			}
			Bid.Trace("<ds.Bid|Info> VersionSafeName='%ls'\n", appDomainFriendlyName);
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00037C60 File Offset: 0x00037060
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	private static void doneEntryPoint()
	{
		if (Bid.modID == Bid.NoData)
		{
			Bid.modFlags = Bid.ApiGroup.Off;
			return;
		}
		try
		{
			Bid.NativeMethods.DllBidEntryPoint(ref Bid.modID, 0, IntPtr.Zero, 3489660928U, ref Bid.modFlags, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			Bid.NativeMethods.DllBidFinalize();
		}
		catch
		{
			Bid.modFlags = Bid.ApiGroup.Off;
		}
		finally
		{
			Bid.cookieObject.Invalidate();
			Bid.modID = Bid.NoData;
			Bid.modFlags = Bid.ApiGroup.Off;
		}
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00037D18 File Offset: 0x00037118
	private static IntPtr internalInitialize()
	{
		Bid.deterministicStaticInit();
		Bid.ai = new Bid.AutoInit();
		return Bid.modID;
	}

	// Token: 0x040000AD RID: 173
	private const string dllName = "System.Data.dll";

	// Token: 0x040000AE RID: 174
	private static IntPtr __noData;

	// Token: 0x040000AF RID: 175
	private static object _setBitsLock = new object();

	// Token: 0x040000B0 RID: 176
	private static IntPtr modID = Bid.internalInitialize();

	// Token: 0x040000B1 RID: 177
	private static Bid.ApiGroup modFlags;

	// Token: 0x040000B2 RID: 178
	private static string modIdentity;

	// Token: 0x040000B3 RID: 179
	private static Bid.CtrlCB ctrlCallback;

	// Token: 0x040000B4 RID: 180
	private static Bid.BindingCookie cookieObject;

	// Token: 0x040000B5 RID: 181
	private static GCHandle hCookie;

	// Token: 0x040000B6 RID: 182
	private static IntPtr __defaultCmdSpace;

	// Token: 0x040000B7 RID: 183
	private const int BidVer = 9210;

	// Token: 0x040000B8 RID: 184
	private const uint configFlags = 3489660928U;

	// Token: 0x040000B9 RID: 185
	private static Bid.AutoInit ai;

	// Token: 0x02000336 RID: 822
	[SuppressUnmanagedCodeSecurity]
	[ComVisible(false)]
	private static class NativeMethods
	{
		// Token: 0x0600337A RID: 13178
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, string a3, string a4, int a5);

		// Token: 0x0600337B RID: 13179
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, bool a3);

		// Token: 0x0600337C RID: 13180
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, long a3, uint a4, int a5, uint a6, uint a7);

		// Token: 0x0600337D RID: 13181
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, string a1, string a2);

		// Token: 0x0600337E RID: 13182
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, string a1, string a2);

		// Token: 0x0600337F RID: 13183
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2, int a3);

		// Token: 0x06003380 RID: 13184
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, bool a2, int a3);

		// Token: 0x06003381 RID: 13185
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, string a1, string a2, string a3);

		// Token: 0x06003382 RID: 13186
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2, string a3, int a4);

		// Token: 0x06003383 RID: 13187
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, IntPtr a1);

		// Token: 0x06003384 RID: 13188
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1);

		// Token: 0x06003385 RID: 13189
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, bool a1);

		// Token: 0x06003386 RID: 13190
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, string fmtPrintfW2, int a1);

		// Token: 0x06003387 RID: 13191
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2);

		// Token: 0x06003388 RID: 13192
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, string a3);

		// Token: 0x06003389 RID: 13193
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2);

		// Token: 0x0600338A RID: 13194
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, IntPtr a2, IntPtr a3);

		// Token: 0x0600338B RID: 13195
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, IntPtr a2);

		// Token: 0x0600338C RID: 13196
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, string a3);

		// Token: 0x0600338D RID: 13197
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, int a3);

		// Token: 0x0600338E RID: 13198
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, string a3, int a4);

		// Token: 0x0600338F RID: 13199
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3, string a4, string a5, int a6);

		// Token: 0x06003390 RID: 13200
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3);

		// Token: 0x06003391 RID: 13201
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, bool a2);

		// Token: 0x06003392 RID: 13202
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, string a3, string a4);

		// Token: 0x06003393 RID: 13203
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, bool a1, string a2, string a3, string a4);

		// Token: 0x06003394 RID: 13204
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3, int a4);

		// Token: 0x06003395 RID: 13205
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, bool a3);

		// Token: 0x06003396 RID: 13206
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3, int a4, int a5, int a6, int a7);

		// Token: 0x06003397 RID: 13207
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, int a3, int a4, bool a5);

		// Token: 0x06003398 RID: 13208
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, long a2);

		// Token: 0x06003399 RID: 13209
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, long a3);

		// Token: 0x0600339A RID: 13210
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW1, string fmtPrintfW2, string fmtPrintfW3, long a4);

		// Token: 0x0600339B RID: 13211
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, string a3, string a4, int a5, long a6);

		// Token: 0x0600339C RID: 13212
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, long a2, int a3, int a4);

		// Token: 0x0600339D RID: 13213
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, long a3, int a4);

		// Token: 0x0600339E RID: 13214
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3, int a4, string a5, string a6, string a7, int a8);

		// Token: 0x0600339F RID: 13215
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, string a3, string a4);

		// Token: 0x060033A0 RID: 13216
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, string a1);

		// Token: 0x060033A1 RID: 13217
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2);

		// Token: 0x060033A2 RID: 13218
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, bool a2);

		// Token: 0x060033A3 RID: 13219
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, string a3);

		// Token: 0x060033A4 RID: 13220
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2, bool a3);

		// Token: 0x060033A5 RID: 13221
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, bool a3);

		// Token: 0x060033A6 RID: 13222
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, int a3, string a4);

		// Token: 0x060033A7 RID: 13223
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, int a3);

		// Token: 0x060033A8 RID: 13224
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, bool a3, int a4);

		// Token: 0x060033A9 RID: 13225
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DllBidPutStrW")]
		internal static extern void PutStr(IntPtr hID, UIntPtr src, UIntPtr info, string str);

		// Token: 0x060033AA RID: 13226
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string strConst);

		// Token: 0x060033AB RID: 13227
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, string a1);

		// Token: 0x060033AC RID: 13228
		[DllImport("System.Data.dll", EntryPoint = "DllBidScopeLeave")]
		internal static extern void ScopeLeave(IntPtr hID, UIntPtr src, UIntPtr info, ref IntPtr hScp);

		// Token: 0x060033AD RID: 13229
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string strConst);

		// Token: 0x060033AE RID: 13230
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1);

		// Token: 0x060033AF RID: 13231
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2);

		// Token: 0x060033B0 RID: 13232
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void TraceBin(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, byte[] buff, uint len);

		// Token: 0x060033B1 RID: 13233
		[DllImport("System.Data.dll", CharSet = CharSet.Unicode, EntryPoint = "DllBidCtlProc")]
		internal static extern void AddMetaText(IntPtr hID, IntPtr cmdSpace, Bid.CtlCmd cmd, IntPtr nop1, string txtID, IntPtr nop2);

		// Token: 0x060033B2 RID: 13234
		[DllImport("System.Data.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern void DllBidEntryPoint(ref IntPtr hID, int bInitAndVer, string sIdentity, uint propBits, ref Bid.ApiGroup pGblFlags, Bid.CtrlCB fAddr, ref Bid.BIDEXTINFO pExtInfo, IntPtr pHooks, IntPtr pHdr);

		// Token: 0x060033B3 RID: 13235
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll")]
		internal static extern void DllBidEntryPoint(ref IntPtr hID, int bInitAndVer, IntPtr unused1, uint propBits, ref Bid.ApiGroup pGblFlags, IntPtr unused2, IntPtr unused3, IntPtr unused4, IntPtr unused5);

		// Token: 0x060033B4 RID: 13236
		[DllImport("System.Data.dll")]
		internal static extern void DllBidInitialize();

		// Token: 0x060033B5 RID: 13237
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll")]
		internal static extern void DllBidFinalize();
	}

	// Token: 0x02000337 RID: 823
	internal enum ApiGroup : uint
	{
		// Token: 0x04001E31 RID: 7729
		Off,
		// Token: 0x04001E32 RID: 7730
		Default,
		// Token: 0x04001E33 RID: 7731
		Trace,
		// Token: 0x04001E34 RID: 7732
		Scope = 4U,
		// Token: 0x04001E35 RID: 7733
		Perf = 8U,
		// Token: 0x04001E36 RID: 7734
		Resource = 16U,
		// Token: 0x04001E37 RID: 7735
		Memory = 32U,
		// Token: 0x04001E38 RID: 7736
		StatusOk = 64U,
		// Token: 0x04001E39 RID: 7737
		Advanced = 128U,
		// Token: 0x04001E3A RID: 7738
		Pooling = 4096U,
		// Token: 0x04001E3B RID: 7739
		Dependency = 8192U,
		// Token: 0x04001E3C RID: 7740
		StateDump = 16384U,
		// Token: 0x04001E3D RID: 7741
		Correlation = 262144U,
		// Token: 0x04001E3E RID: 7742
		MaskBid = 4095U,
		// Token: 0x04001E3F RID: 7743
		MaskUser = 4294963200U,
		// Token: 0x04001E40 RID: 7744
		MaskAll = 4294967295U
	}

	// Token: 0x02000338 RID: 824
	// (Invoke) Token: 0x060033B7 RID: 13239
	private delegate Bid.ApiGroup CtrlCB(Bid.ApiGroup mask, Bid.ApiGroup bits);

	// Token: 0x02000339 RID: 825
	[StructLayout(LayoutKind.Sequential)]
	private class BindingCookie
	{
		// Token: 0x060033BA RID: 13242 RVA: 0x0013D8B4 File Offset: 0x0013CCB4
		internal BindingCookie()
		{
			this._data = (IntPtr)(-1);
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x0013D8D4 File Offset: 0x0013CCD4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Invalidate()
		{
			this._data = (IntPtr)(-1);
		}

		// Token: 0x04001E41 RID: 7745
		internal IntPtr _data;
	}

	// Token: 0x0200033A RID: 826
	private enum CtlCmd : uint
	{
		// Token: 0x04001E43 RID: 7747
		Reverse = 1U,
		// Token: 0x04001E44 RID: 7748
		Unicode,
		// Token: 0x04001E45 RID: 7749
		DcsBase = 1073741824U,
		// Token: 0x04001E46 RID: 7750
		DcsMax = 1610612732U,
		// Token: 0x04001E47 RID: 7751
		CplBase = 1610612736U,
		// Token: 0x04001E48 RID: 7752
		CplMax = 2147483644U,
		// Token: 0x04001E49 RID: 7753
		CmdSpaceCount = 1073741824U,
		// Token: 0x04001E4A RID: 7754
		CmdSpaceEnum = 1073741828U,
		// Token: 0x04001E4B RID: 7755
		CmdSpaceQuery = 1073741832U,
		// Token: 0x04001E4C RID: 7756
		GetEventID = 1073741846U,
		// Token: 0x04001E4D RID: 7757
		ParseString = 1073741850U,
		// Token: 0x04001E4E RID: 7758
		AddExtension = 1073741854U,
		// Token: 0x04001E4F RID: 7759
		AddMetaText = 1073741858U,
		// Token: 0x04001E50 RID: 7760
		AddResHandle = 1073741862U,
		// Token: 0x04001E51 RID: 7761
		Shutdown = 1073741866U,
		// Token: 0x04001E52 RID: 7762
		LastItem
	}

	// Token: 0x0200033B RID: 827
	private struct BIDEXTINFO
	{
		// Token: 0x060033BC RID: 13244 RVA: 0x0013D8F0 File Offset: 0x0013CCF0
		internal BIDEXTINFO(IntPtr hMod, string modPath, string friendlyName, IntPtr cookiePtr)
		{
			this.hModule = hMod;
			this.DomainName = friendlyName;
			this.Reserved2 = 0;
			this.Reserved = 0;
			this.ModulePath = modPath;
			this.ModulePathA = IntPtr.Zero;
			this.pBindCookie = cookiePtr;
		}

		// Token: 0x04001E53 RID: 7763
		private IntPtr hModule;

		// Token: 0x04001E54 RID: 7764
		[MarshalAs(UnmanagedType.LPWStr)]
		private string DomainName;

		// Token: 0x04001E55 RID: 7765
		private int Reserved2;

		// Token: 0x04001E56 RID: 7766
		private int Reserved;

		// Token: 0x04001E57 RID: 7767
		[MarshalAs(UnmanagedType.LPWStr)]
		private string ModulePath;

		// Token: 0x04001E58 RID: 7768
		private IntPtr ModulePathA;

		// Token: 0x04001E59 RID: 7769
		private IntPtr pBindCookie;
	}

	// Token: 0x0200033C RID: 828
	private sealed class AutoInit : SafeHandle
	{
		// Token: 0x060033BD RID: 13245 RVA: 0x0013D934 File Offset: 0x0013CD34
		internal AutoInit() : base(IntPtr.Zero, true)
		{
			Bid.initEntryPoint();
			this._bInitialized = true;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x0013D95C File Offset: 0x0013CD5C
		protected override bool ReleaseHandle()
		{
			this._bInitialized = false;
			Bid.doneEntryPoint();
			return true;
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x0013D978 File Offset: 0x0013CD78
		public override bool IsInvalid
		{
			get
			{
				return !this._bInitialized;
			}
		}

		// Token: 0x04001E5A RID: 7770
		private bool _bInitialized;
	}
}
