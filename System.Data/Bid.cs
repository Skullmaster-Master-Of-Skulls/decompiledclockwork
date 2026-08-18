using System;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

// Token: 0x02000109 RID: 265
[ComVisible(false)]
internal static class Bid
{
	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06001083 RID: 4227 RVA: 0x00230CD8 File Offset: 0x002300D8
	internal static bool DefaultOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Default) != Bid.ApiGroup.Off;
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06001084 RID: 4228 RVA: 0x00230CF8 File Offset: 0x002300F8
	internal static bool TraceOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off;
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06001085 RID: 4229 RVA: 0x00230D18 File Offset: 0x00230118
	internal static bool ScopeOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off;
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06001086 RID: 4230 RVA: 0x00230D38 File Offset: 0x00230138
	internal static bool PerfOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Perf) != Bid.ApiGroup.Off;
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06001087 RID: 4231 RVA: 0x00230D58 File Offset: 0x00230158
	internal static bool ResourceOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Resource) != Bid.ApiGroup.Off;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06001088 RID: 4232 RVA: 0x00230D78 File Offset: 0x00230178
	internal static bool MemoryOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Memory) != Bid.ApiGroup.Off;
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06001089 RID: 4233 RVA: 0x00230D98 File Offset: 0x00230198
	internal static bool StatusOkOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.StatusOk) != Bid.ApiGroup.Off;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x0600108A RID: 4234 RVA: 0x00230DB8 File Offset: 0x002301B8
	internal static bool AdvancedOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.Advanced) != Bid.ApiGroup.Off;
		}
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x0600108B RID: 4235 RVA: 0x00230DD8 File Offset: 0x002301D8
	internal static bool StateDumpOn
	{
		get
		{
			return (Bid.modFlags & Bid.ApiGroup.StateDump) != Bid.ApiGroup.Off;
		}
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x00230DF8 File Offset: 0x002301F8
	internal static bool IsOn(Bid.ApiGroup flag)
	{
		return (Bid.modFlags & flag) != Bid.ApiGroup.Off;
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x00230E18 File Offset: 0x00230218
	internal static bool AreOn(Bid.ApiGroup flags)
	{
		return (Bid.modFlags & flags) == flags;
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x0600108E RID: 4238 RVA: 0x00230E38 File Offset: 0x00230238
	internal static IntPtr NoData
	{
		get
		{
			return Bid.__noData;
		}
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x0600108F RID: 4239 RVA: 0x00230E58 File Offset: 0x00230258
	internal static IntPtr ID
	{
		get
		{
			return Bid.modID;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06001090 RID: 4240 RVA: 0x00230E78 File Offset: 0x00230278
	internal static bool IsInitialized
	{
		get
		{
			return Bid.modID != Bid.NoData;
		}
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x00230E98 File Offset: 0x00230298
	internal static void PutStr(string str)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)0U, str);
		}
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x00230ED8 File Offset: 0x002302D8
	internal static void PutStrLine(string str)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)1U, str);
		}
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x00230F18 File Offset: 0x00230318
	internal static void PutNewLine()
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)2U, string.Empty);
		}
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x00230F68 File Offset: 0x00230368
	internal static void PutStrEx(uint flags, string str)
	{
		if (Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)flags, str);
		}
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x00230FA8 File Offset: 0x002303A8
	internal static void PutSmartNewLine()
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)1U, string.Empty);
		}
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x00230FF8 File Offset: 0x002303F8
	internal static uint NewLineEx(bool addNewLine)
	{
		if (!addNewLine)
		{
			return 0U;
		}
		return 1U;
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x00231018 File Offset: 0x00230418
	internal static void Trace(string strConst)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, strConst);
		}
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x00231058 File Offset: 0x00230458
	internal static void TraceEx(uint flags, string strConst)
	{
		if (Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, (UIntPtr)flags, strConst);
		}
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x00231098 File Offset: 0x00230498
	internal static void Trace(string fmtPrintfW, string a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x002310D8 File Offset: 0x002304D8
	internal static void TraceEx(uint flags, string fmtPrintfW, string a1)
	{
		if (Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, (UIntPtr)flags, fmtPrintfW, a1);
		}
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x00231118 File Offset: 0x00230518
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

	// Token: 0x0600109C RID: 4252 RVA: 0x00231178 File Offset: 0x00230578
	internal static void ScopeEnter(out IntPtr hScp, string strConst)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, strConst);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x002311C8 File Offset: 0x002305C8
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, string a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x00231218 File Offset: 0x00230618
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, IntPtr a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x00231268 File Offset: 0x00230668
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x002312B8 File Offset: 0x002306B8
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x00231308 File Offset: 0x00230708
	internal static bool Enabled(string traceControlString)
	{
		return (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && !(Bid.modID == Bid.NoData) && Bid.NativeMethods.Enabled(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, traceControlString);
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x00231348 File Offset: 0x00230748
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
			Bid.NativeMethods.TraceBin(Bid.modID, UIntPtr.Zero, (UIntPtr)16U, "<Trace|BLOB> %p %u\n", buff, length);
		}
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x002313B8 File Offset: 0x002307B8
	internal static void TraceBinEx(byte[] buff, ushort length)
	{
		if (Bid.modID != Bid.NoData)
		{
			if ((ushort)buff.Length < length)
			{
				length = (ushort)buff.Length;
			}
			Bid.NativeMethods.TraceBin(Bid.modID, UIntPtr.Zero, (UIntPtr)16U, "<Trace|BLOB> %p %u\n", buff, length);
		}
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x00231408 File Offset: 0x00230808
	internal static Bid.ApiGroup GetApiGroupBits(Bid.ApiGroup mask)
	{
		return Bid.modFlags & mask;
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x00231428 File Offset: 0x00230828
	internal static Bid.ApiGroup SetApiGroupBits(Bid.ApiGroup mask, Bid.ApiGroup bits)
	{
		Bid.ApiGroup result;
		lock (Bid._setBitsLock)
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

	// Token: 0x060010A6 RID: 4262 RVA: 0x00231488 File Offset: 0x00230888
	internal static bool AddMetaText(string metaStr)
	{
		if (Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.AddMetaText(Bid.modID, Bid.DefaultCmdSpace, Bid.CtlCmd.AddMetaText, IntPtr.Zero, metaStr, IntPtr.Zero);
		}
		return true;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x002314C8 File Offset: 0x002308C8
	[Conditional("DEBUG")]
	internal static void DTRACE(string strConst)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)1U, strConst);
		}
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x00231508 File Offset: 0x00230908
	[Conditional("DEBUG")]
	internal static void DTRACE(string clrFormatString, params object[] args)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.PutStr(Bid.modID, UIntPtr.Zero, (UIntPtr)1U, string.Format(CultureInfo.CurrentCulture, clrFormatString, args));
		}
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x00231558 File Offset: 0x00230958
	[Conditional("DEBUG")]
	internal static void DASSERT(bool condition)
	{
		if (!condition)
		{
			System.Diagnostics.Trace.Assert(false);
		}
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x00231578 File Offset: 0x00230978
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

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x060010AB RID: 4267 RVA: 0x002315D8 File Offset: 0x002309D8
	internal static IntPtr DefaultCmdSpace
	{
		get
		{
			return Bid.__defaultCmdSpace;
		}
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x002315F8 File Offset: 0x002309F8
	internal static IntPtr GetCmdSpaceID(string textID)
	{
		if (!(Bid.modID != Bid.NoData))
		{
			return IntPtr.Zero;
		}
		return Bid.NativeMethods.GetCmdSpaceID(Bid.modID, Bid.DefaultCmdSpace, Bid.CtlCmd.CmdSpaceQuery, 0U, textID, IntPtr.Zero);
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x00231638 File Offset: 0x00230A38
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

	// Token: 0x060010AE RID: 4270 RVA: 0x00231678 File Offset: 0x00230A78
	private static string getAppDomainFriendlyName()
	{
		string text = AppDomain.CurrentDomain.FriendlyName;
		if (text == null || text.Length <= 0)
		{
			text = "AppDomain.H" + AppDomain.CurrentDomain.GetHashCode();
		}
		return text;
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x002316B8 File Offset: 0x00230AB8
	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	private static string getModulePath(Module mod)
	{
		return mod.FullyQualifiedName;
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x002316D8 File Offset: 0x00230AD8
	private static void initEntryPoint()
	{
		Bid.NativeMethods.DllBidInitialize();
		Module manifestModule = Assembly.GetExecutingAssembly().ManifestModule;
		Bid.modIdentity = Bid.getIdentity(manifestModule);
		Bid.modID = Bid.NoData;
		Bid.BIDEXTINFO bidextinfo = new Bid.BIDEXTINFO(Marshal.GetHINSTANCE(manifestModule), Bid.getModulePath(manifestModule), Bid.getAppDomainFriendlyName(), Bid.hCookie.AddrOfPinnedObject());
		Bid.NativeMethods.DllBidEntryPoint(ref Bid.modID, 9210, Bid.modIdentity, 3489660928U, ref Bid.modFlags, Bid.ctrlCallback, ref bidextinfo, IntPtr.Zero, IntPtr.Zero);
		if (Bid.modID != Bid.NoData)
		{
			object[] customAttributes = manifestModule.GetCustomAttributes(typeof(BidMetaTextAttribute), true);
			foreach (object obj in customAttributes)
			{
				Bid.AddMetaText(((BidMetaTextAttribute)obj).MetaText);
			}
		}
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x002317B8 File Offset: 0x00230BB8
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

	// Token: 0x060010B2 RID: 4274 RVA: 0x00231878 File Offset: 0x00230C78
	private static IntPtr internalInitialize()
	{
		Bid.deterministicStaticInit();
		Bid.ai = new Bid.AutoInit();
		return Bid.modID;
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x002318A8 File Offset: 0x00230CA8
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void PoolerTrace(string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x002318F8 File Offset: 0x00230CF8
	internal static void PoolerTrace(string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x00231948 File Offset: 0x00230D48
	internal static void PoolerTrace(string fmtPrintfW, int a1, int a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x00231998 File Offset: 0x00230D98
	internal static void PoolerTrace(string fmtPrintfW, int a1, int a2, int a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x002319E8 File Offset: 0x00230DE8
	internal static void PoolerScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Pooling) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x00231A38 File Offset: 0x00230E38
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x00231A88 File Offset: 0x00230E88
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, fmtPrintfW2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x00231AD8 File Offset: 0x00230ED8
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x00231B28 File Offset: 0x00230F28
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, string fmtPrintfW2, string fmtPrintfW3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, fmtPrintfW2, fmtPrintfW3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x00231B78 File Offset: 0x00230F78
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, fmtPrintfW2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x00231BC8 File Offset: 0x00230FC8
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x00231C18 File Offset: 0x00231018
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, string fmtPrintfW2, string fmtPrintfW3, string fmtPrintfW4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, fmtPrintfW2, fmtPrintfW3, fmtPrintfW4);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x00231C78 File Offset: 0x00231078
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string fmtPrintfW2, string fmtPrintfW3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, fmtPrintfW2, fmtPrintfW3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x00231CD8 File Offset: 0x002310D8
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string fmtPrintfW2, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, fmtPrintfW2, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00231D38 File Offset: 0x00231138
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, fmtPrintfW2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x00231D98 File Offset: 0x00231198
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string fmtPrintfW2, string fmtPrintfW3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, fmtPrintfW2, fmtPrintfW3, a4);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x00231DF8 File Offset: 0x002311F8
	internal static void NotificationsScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string fmtPrintfW2, string fmtPrintfW3, string fmtPrintfW4, int a5)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, fmtPrintfW2, fmtPrintfW3, fmtPrintfW4, a5);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x00231E58 File Offset: 0x00231258
	internal static void NotificationsTrace(string fmtPrintfW)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW);
		}
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x00231E98 File Offset: 0x00231298
	internal static void NotificationsTrace(string fmtPrintfW, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, fmtPrintfW2);
		}
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x00231EE8 File Offset: 0x002312E8
	internal static void NotificationsTrace(string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x00231F38 File Offset: 0x00231338
	internal static void NotificationsTrace(string fmtPrintfW, bool a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x00231F88 File Offset: 0x00231388
	internal static void NotificationsTrace(string fmtPrintfW, string fmtPrintfW2, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, fmtPrintfW2, a1);
		}
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x00231FD8 File Offset: 0x002313D8
	internal static void NotificationsTrace(string fmtPrintfW, int a1, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, fmtPrintfW2);
		}
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x00232028 File Offset: 0x00231428
	internal static void NotificationsTrace(string fmtPrintfW, bool a1, string fmtPrintfW2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, fmtPrintfW2);
		}
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x00232078 File Offset: 0x00231478
	internal static void NotificationsTrace(string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x002320C8 File Offset: 0x002314C8
	internal static void NotificationsTrace(string fmtPrintfW, bool a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x00232118 File Offset: 0x00231518
	internal static void NotificationsTrace(string fmtPrintfW, int a1, bool a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x00232168 File Offset: 0x00231568
	internal static void NotificationsTrace(string fmtPrintfW, string fmtPrintfW2, string fmtPrintfW3, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, fmtPrintfW2, fmtPrintfW3, (long)a1);
		}
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x002321B8 File Offset: 0x002315B8
	internal static void NotificationsTrace(string fmtPrintfW, bool a1, string fmtPrintfW2, string fmtPrintfW3, string fmtPrintfW4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, fmtPrintfW2, fmtPrintfW3, fmtPrintfW4);
		}
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x00232208 File Offset: 0x00231608
	internal static void NotificationsTrace(string fmtPrintfW, int a1, string fmtPrintfW2, string fmtPrintfW3, string fmtPrintfW4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Dependency) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, fmtPrintfW2, fmtPrintfW3, fmtPrintfW4);
		}
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x00232258 File Offset: 0x00231658
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void TraceSqlReturn(string fmtPrintfW, ODBC32.RetCode a1)
	{
		if ((a1 != ODBC32.RetCode.SUCCESS || (Bid.modFlags & Bid.ApiGroup.StatusOk) != Bid.ApiGroup.Off) && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1);
		}
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x002322A8 File Offset: 0x002316A8
	internal static void TraceSqlReturn(string fmtPrintfW, ODBC32.RetCode a1, string a2)
	{
		if ((a1 != ODBC32.RetCode.SUCCESS || (Bid.modFlags & Bid.ApiGroup.StatusOk) != Bid.ApiGroup.Off) && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1, a2);
		}
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x002322F8 File Offset: 0x002316F8
	internal static void TraceSqlReturn(string fmtPrintfW, ODBC32.RetCode a1, string a2, string a3)
	{
		if ((a1 != ODBC32.RetCode.SUCCESS || (Bid.modFlags & Bid.ApiGroup.StatusOk) != Bid.ApiGroup.Off) && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1, a2, a3);
		}
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x00232348 File Offset: 0x00231748
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void Trace(string fmtPrintfW, OleDbHResult a1)
	{
		if ((a1 != OleDbHResult.S_OK || (Bid.modFlags & Bid.ApiGroup.StatusOk) != Bid.ApiGroup.Off) && (Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1);
		}
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x00232398 File Offset: 0x00231798
	internal static void Trace(string fmtPrintfW, OleDbHResult a1, string a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1, a2);
		}
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x002323D8 File Offset: 0x002317D8
	internal static void Trace(string fmtPrintfW, OleDbHResult a1, IntPtr a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1, a2);
		}
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x00232418 File Offset: 0x00231818
	internal static void Trace(string fmtPrintfW, OleDbHResult a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, (int)a1, a2);
		}
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x00232458 File Offset: 0x00231858
	internal static void Trace(string fmtPrintfW, string a1, string a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x00232498 File Offset: 0x00231898
	internal static void Trace(string fmtPrintfW, int a1, string a2, bool a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x002324D8 File Offset: 0x002318D8
	internal static void Trace(string fmtPrintfW, int a1, int a2, string a3, string a4, int a5)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5);
		}
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x00232528 File Offset: 0x00231928
	internal static void Trace(string fmtPrintfW, int a1, int a2, long a3, uint a4, int a5, uint a6, uint a7)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6, a7);
		}
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x00232578 File Offset: 0x00231978
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, Guid a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x002325C8 File Offset: 0x002319C8
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x00232618 File Offset: 0x00231A18
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, bool a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x00232668 File Offset: 0x00231A68
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void Trace(string fmtPrintfW, int a1, string a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x002326A8 File Offset: 0x00231AA8
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void Trace(string fmtPrintfW, IntPtr a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x002326E8 File Offset: 0x00231AE8
	internal static void Trace(string fmtPrintfW, int a1)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1);
		}
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x00232728 File Offset: 0x00231B28
	internal static void Trace(string fmtPrintfW, int a1, int a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x00232768 File Offset: 0x00231B68
	internal static void Trace(string fmtPrintfW, int a1, IntPtr a2, IntPtr a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x002327A8 File Offset: 0x00231BA8
	internal static void Trace(string fmtPrintfW, int a1, IntPtr a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x002327E8 File Offset: 0x00231BE8
	internal static void Trace(string fmtPrintfW, int a1, string a2, string a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x00232828 File Offset: 0x00231C28
	internal static void Trace(string fmtPrintfW, int a1, string a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x00232868 File Offset: 0x00231C68
	internal static void Trace(string fmtPrintfW, int a1, string a2, string a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x002328B8 File Offset: 0x00231CB8
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3, string a4, string a5, int a6)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6);
		}
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x00232908 File Offset: 0x00231D08
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x00232948 File Offset: 0x00231D48
	internal static void Trace(string fmtPrintfW, int a1, bool a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x00232988 File Offset: 0x00231D88
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x002329D8 File Offset: 0x00231DD8
	internal static void Trace(string fmtPrintfW, int a1, int a2, bool a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x00232A18 File Offset: 0x00231E18
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3, int a4, int a5, int a6, int a7)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6, a7);
		}
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x00232A68 File Offset: 0x00231E68
	internal static void Trace(string fmtPrintfW, int a1, string a2, int a3, int a4, bool a5)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5);
		}
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x00232AB8 File Offset: 0x00231EB8
	internal static void Trace(string fmtPrintfW, int a1, long a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2);
		}
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x00232AF8 File Offset: 0x00231EF8
	internal static void Trace(string fmtPrintfW, int a1, int a2, long a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3);
		}
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x00232B38 File Offset: 0x00231F38
	internal static void Trace(string fmtPrintfW, int a1, string a2, string a3, string a4, int a5, long a6)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6);
		}
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x00232B88 File Offset: 0x00231F88
	internal static void Trace(string fmtPrintfW, int a1, long a2, int a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x00232BD8 File Offset: 0x00231FD8
	internal static void Trace(string fmtPrintfW, int a1, int a2, long a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x00232C28 File Offset: 0x00232028
	internal static void Trace(string fmtPrintfW, int a1, int a2, int a3, int a4, string a5, string a6, string a7, int a8)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4, a5, a6, a7, a8);
		}
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x00232C78 File Offset: 0x00232078
	internal static void Trace(string fmtPrintfW, int a1, int a2, string a3, string a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Trace) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.Trace(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, fmtPrintfW, a1, a2, a3, a4);
		}
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x00232CC8 File Offset: 0x002320C8
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x00232D18 File Offset: 0x00232118
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, bool a2)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x00232D68 File Offset: 0x00232168
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, string a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x00232DB8 File Offset: 0x002321B8
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, string a2, bool a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x00232E08 File Offset: 0x00232208
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, bool a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x00232E58 File Offset: 0x00232258
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, int a3, string a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3, a4);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x00232EA8 File Offset: 0x002322A8
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, int a3)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x00232EF8 File Offset: 0x002322F8
	internal static void ScopeEnter(out IntPtr hScp, string fmtPrintfW, int a1, int a2, bool a3, int a4)
	{
		if ((Bid.modFlags & Bid.ApiGroup.Scope) != Bid.ApiGroup.Off && Bid.modID != Bid.NoData)
		{
			Bid.NativeMethods.ScopeEnter(Bid.modID, UIntPtr.Zero, UIntPtr.Zero, out hScp, fmtPrintfW, a1, a2, a3, a4);
			return;
		}
		hScp = Bid.NoData;
	}

	// Token: 0x04000B31 RID: 2865
	private const int BidVer = 9210;

	// Token: 0x04000B32 RID: 2866
	private const uint configFlags = 3489660928U;

	// Token: 0x04000B33 RID: 2867
	private const string dllName = "System.Data.dll";

	// Token: 0x04000B34 RID: 2868
	private static IntPtr __noData;

	// Token: 0x04000B35 RID: 2869
	private static object _setBitsLock = new object();

	// Token: 0x04000B36 RID: 2870
	private static IntPtr modID = Bid.internalInitialize();

	// Token: 0x04000B37 RID: 2871
	private static Bid.ApiGroup modFlags;

	// Token: 0x04000B38 RID: 2872
	private static string modIdentity;

	// Token: 0x04000B39 RID: 2873
	private static Bid.CtrlCB ctrlCallback;

	// Token: 0x04000B3A RID: 2874
	private static Bid.BindingCookie cookieObject;

	// Token: 0x04000B3B RID: 2875
	private static GCHandle hCookie;

	// Token: 0x04000B3C RID: 2876
	private static IntPtr __defaultCmdSpace;

	// Token: 0x04000B3D RID: 2877
	private static Bid.AutoInit ai;

	// Token: 0x0200010A RID: 266
	internal enum ApiGroup : uint
	{
		// Token: 0x04000B3F RID: 2879
		Off,
		// Token: 0x04000B40 RID: 2880
		Default,
		// Token: 0x04000B41 RID: 2881
		Trace,
		// Token: 0x04000B42 RID: 2882
		Scope = 4U,
		// Token: 0x04000B43 RID: 2883
		Perf = 8U,
		// Token: 0x04000B44 RID: 2884
		Resource = 16U,
		// Token: 0x04000B45 RID: 2885
		Memory = 32U,
		// Token: 0x04000B46 RID: 2886
		StatusOk = 64U,
		// Token: 0x04000B47 RID: 2887
		Advanced = 128U,
		// Token: 0x04000B48 RID: 2888
		Pooling = 4096U,
		// Token: 0x04000B49 RID: 2889
		Dependency = 8192U,
		// Token: 0x04000B4A RID: 2890
		StateDump = 16384U,
		// Token: 0x04000B4B RID: 2891
		MaskBid = 4095U,
		// Token: 0x04000B4C RID: 2892
		MaskUser = 4294963200U,
		// Token: 0x04000B4D RID: 2893
		MaskAll = 4294967295U
	}

	// Token: 0x0200010B RID: 267
	// (Invoke) Token: 0x06001100 RID: 4352
	private delegate Bid.ApiGroup CtrlCB(Bid.ApiGroup mask, Bid.ApiGroup bits);

	// Token: 0x0200010C RID: 268
	[StructLayout(LayoutKind.Sequential)]
	private class BindingCookie
	{
		// Token: 0x06001103 RID: 4355 RVA: 0x00232F78 File Offset: 0x00232378
		internal BindingCookie()
		{
			this._data = (IntPtr)(-1);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00232F98 File Offset: 0x00232398
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Invalidate()
		{
			this._data = (IntPtr)(-1);
		}

		// Token: 0x04000B4E RID: 2894
		internal IntPtr _data;
	}

	// Token: 0x0200010D RID: 269
	private enum CtlCmd : uint
	{
		// Token: 0x04000B50 RID: 2896
		Reverse = 1U,
		// Token: 0x04000B51 RID: 2897
		Unicode,
		// Token: 0x04000B52 RID: 2898
		DcsBase = 1073741824U,
		// Token: 0x04000B53 RID: 2899
		DcsMax = 1610612732U,
		// Token: 0x04000B54 RID: 2900
		CplBase = 1610612736U,
		// Token: 0x04000B55 RID: 2901
		CplMax = 2147483644U,
		// Token: 0x04000B56 RID: 2902
		CmdSpaceCount = 1073741824U,
		// Token: 0x04000B57 RID: 2903
		CmdSpaceEnum = 1073741828U,
		// Token: 0x04000B58 RID: 2904
		CmdSpaceQuery = 1073741832U,
		// Token: 0x04000B59 RID: 2905
		GetEventID = 1073741846U,
		// Token: 0x04000B5A RID: 2906
		ParseString = 1073741850U,
		// Token: 0x04000B5B RID: 2907
		AddExtension = 1073741854U,
		// Token: 0x04000B5C RID: 2908
		AddMetaText = 1073741858U,
		// Token: 0x04000B5D RID: 2909
		AddResHandle = 1073741862U,
		// Token: 0x04000B5E RID: 2910
		Shutdown = 1073741866U,
		// Token: 0x04000B5F RID: 2911
		LastItem
	}

	// Token: 0x0200010E RID: 270
	private struct BIDEXTINFO
	{
		// Token: 0x06001105 RID: 4357 RVA: 0x00232FB8 File Offset: 0x002323B8
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

		// Token: 0x04000B60 RID: 2912
		private IntPtr hModule;

		// Token: 0x04000B61 RID: 2913
		[MarshalAs(UnmanagedType.LPWStr)]
		private string DomainName;

		// Token: 0x04000B62 RID: 2914
		private int Reserved2;

		// Token: 0x04000B63 RID: 2915
		private int Reserved;

		// Token: 0x04000B64 RID: 2916
		[MarshalAs(UnmanagedType.LPWStr)]
		private string ModulePath;

		// Token: 0x04000B65 RID: 2917
		private IntPtr ModulePathA;

		// Token: 0x04000B66 RID: 2918
		private IntPtr pBindCookie;
	}

	// Token: 0x0200010F RID: 271
	private sealed class AutoInit : SafeHandle
	{
		// Token: 0x06001106 RID: 4358 RVA: 0x00233008 File Offset: 0x00232408
		internal AutoInit() : base(IntPtr.Zero, true)
		{
			Bid.initEntryPoint();
			this._bInitialized = true;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00233038 File Offset: 0x00232438
		protected override bool ReleaseHandle()
		{
			this._bInitialized = false;
			Bid.doneEntryPoint();
			return true;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00233058 File Offset: 0x00232458
		public override bool IsInvalid
		{
			get
			{
				return !this._bInitialized;
			}
		}

		// Token: 0x04000B67 RID: 2919
		private bool _bInitialized;
	}

	// Token: 0x02000110 RID: 272
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	private static class NativeMethods
	{
		// Token: 0x06001109 RID: 4361
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DllBidPutStrW")]
		internal static extern void PutStr(IntPtr hID, UIntPtr src, UIntPtr info, string str);

		// Token: 0x0600110A RID: 4362
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string strConst);

		// Token: 0x0600110B RID: 4363
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, string a1);

		// Token: 0x0600110C RID: 4364
		[DllImport("System.Data.dll", EntryPoint = "DllBidScopeLeave")]
		internal static extern void ScopeLeave(IntPtr hID, UIntPtr src, UIntPtr info, ref IntPtr hScp);

		// Token: 0x0600110D RID: 4365
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string strConst);

		// Token: 0x0600110E RID: 4366
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, string a1);

		// Token: 0x0600110F RID: 4367
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1);

		// Token: 0x06001110 RID: 4368
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, IntPtr a1);

		// Token: 0x06001111 RID: 4369
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2);

		// Token: 0x06001112 RID: 4370
		[DllImport("System.Data.dll", CharSet = CharSet.Unicode, EntryPoint = "DllBidEnabledW")]
		internal static extern bool Enabled(IntPtr hID, UIntPtr src, UIntPtr info, string tcs);

		// Token: 0x06001113 RID: 4371
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void TraceBin(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, byte[] buff, ushort len);

		// Token: 0x06001114 RID: 4372
		[DllImport("System.Data.dll", CharSet = CharSet.Unicode, EntryPoint = "DllBidCtlProc")]
		internal static extern void AddMetaText(IntPtr hID, IntPtr cmdSpace, Bid.CtlCmd cmd, IntPtr nop1, string txtID, IntPtr nop2);

		// Token: 0x06001115 RID: 4373
		[DllImport("System.Data.dll", BestFitMapping = false, CharSet = CharSet.Ansi, EntryPoint = "DllBidCtlProc")]
		internal static extern IntPtr GetCmdSpaceID(IntPtr hID, IntPtr cmdSpace, Bid.CtlCmd cmd, uint noOp, string txtID, IntPtr NoOp2);

		// Token: 0x06001116 RID: 4374
		[DllImport("System.Data.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		internal static extern void DllBidEntryPoint(ref IntPtr hID, int bInitAndVer, string sIdentity, uint propBits, ref Bid.ApiGroup pGblFlags, Bid.CtrlCB fAddr, ref Bid.BIDEXTINFO pExtInfo, IntPtr pHooks, IntPtr pHdr);

		// Token: 0x06001117 RID: 4375
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll")]
		internal static extern void DllBidEntryPoint(ref IntPtr hID, int bInitAndVer, IntPtr unused1, uint propBits, ref Bid.ApiGroup pGblFlags, IntPtr unused2, IntPtr unused3, IntPtr unused4, IntPtr unused5);

		// Token: 0x06001118 RID: 4376
		[DllImport("System.Data.dll")]
		internal static extern void DllBidInitialize();

		// Token: 0x06001119 RID: 4377
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll")]
		internal static extern void DllBidFinalize();

		// Token: 0x0600111A RID: 4378
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, string a3, string a4, int a5);

		// Token: 0x0600111B RID: 4379
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, bool a3);

		// Token: 0x0600111C RID: 4380
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, long a3, uint a4, int a5, uint a6, uint a7);

		// Token: 0x0600111D RID: 4381
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, string a1, string a2);

		// Token: 0x0600111E RID: 4382
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid a2);

		// Token: 0x0600111F RID: 4383
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, string a1, string a2);

		// Token: 0x06001120 RID: 4384
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2, int a3);

		// Token: 0x06001121 RID: 4385
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, bool a2, int a3);

		// Token: 0x06001122 RID: 4386
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2, string a3);

		// Token: 0x06001123 RID: 4387
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, string a1, string a2, string a3);

		// Token: 0x06001124 RID: 4388
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2, string a3, int a4);

		// Token: 0x06001125 RID: 4389
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2, string a3, string a4, int a5);

		// Token: 0x06001126 RID: 4390
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, IntPtr a1);

		// Token: 0x06001127 RID: 4391
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1);

		// Token: 0x06001128 RID: 4392
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, bool a1);

		// Token: 0x06001129 RID: 4393
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, string fmtPrintfW2, int a1);

		// Token: 0x0600112A RID: 4394
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, bool a1, string fmtPrintfW2);

		// Token: 0x0600112B RID: 4395
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, bool a1, int a2);

		// Token: 0x0600112C RID: 4396
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2);

		// Token: 0x0600112D RID: 4397
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2);

		// Token: 0x0600112E RID: 4398
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, IntPtr a2, IntPtr a3);

		// Token: 0x0600112F RID: 4399
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, IntPtr a2);

		// Token: 0x06001130 RID: 4400
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, string a3);

		// Token: 0x06001131 RID: 4401
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, int a3);

		// Token: 0x06001132 RID: 4402
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, string a3, int a4);

		// Token: 0x06001133 RID: 4403
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3, string a4, string a5, int a6);

		// Token: 0x06001134 RID: 4404
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3);

		// Token: 0x06001135 RID: 4405
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, bool a2);

		// Token: 0x06001136 RID: 4406
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, string a3, string a4);

		// Token: 0x06001137 RID: 4407
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, bool a1, string a2, string a3, string a4);

		// Token: 0x06001138 RID: 4408
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3, int a4);

		// Token: 0x06001139 RID: 4409
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, bool a3);

		// Token: 0x0600113A RID: 4410
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3, int a4, int a5, int a6, int a7);

		// Token: 0x0600113B RID: 4411
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, int a3, int a4, bool a5);

		// Token: 0x0600113C RID: 4412
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, long a2);

		// Token: 0x0600113D RID: 4413
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, long a3);

		// Token: 0x0600113E RID: 4414
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW1, string fmtPrintfW2, string fmtPrintfW3, long a4);

		// Token: 0x0600113F RID: 4415
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, string a2, string a3, string a4, int a5, long a6);

		// Token: 0x06001140 RID: 4416
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, long a2, int a3, int a4);

		// Token: 0x06001141 RID: 4417
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, long a3, int a4);

		// Token: 0x06001142 RID: 4418
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, int a3, int a4, string a5, string a6, string a7, int a8);

		// Token: 0x06001143 RID: 4419
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidTraceCW")]
		internal static extern void Trace(IntPtr hID, UIntPtr src, UIntPtr info, string fmtPrintfW, int a1, int a2, string a3, string a4);

		// Token: 0x06001144 RID: 4420
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2);

		// Token: 0x06001145 RID: 4421
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, bool a2);

		// Token: 0x06001146 RID: 4422
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, string a3);

		// Token: 0x06001147 RID: 4423
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, string a2, bool a3);

		// Token: 0x06001148 RID: 4424
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, bool a3);

		// Token: 0x06001149 RID: 4425
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, int a3, string a4);

		// Token: 0x0600114A RID: 4426
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, int a3);

		// Token: 0x0600114B RID: 4427
		[DllImport("System.Data.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "DllBidScopeEnterCW")]
		internal static extern void ScopeEnter(IntPtr hID, UIntPtr src, UIntPtr info, out IntPtr hScp, string fmtPrintfW, int a1, int a2, bool a3, int a4);
	}
}
