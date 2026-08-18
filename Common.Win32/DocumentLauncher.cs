using System;
using System.Diagnostics;
using System.Reflection;
using ClockWorkLogger;

namespace TechnoPro.Common.Win32
{
	// Token: 0x02000004 RID: 4
	public static class DocumentLauncher
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000243C File Offset: 0x0000063C
		public static int LaunchFile(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				return 0;
			}
			string text = "\"" + fileName + "\"";
			if (string.IsNullOrEmpty(text))
			{
				return 0;
			}
			try
			{
				Process process = Process.Start(new ProcessStartInfo
				{
					FileName = text
				});
				return (process != null) ? process.Id : 0;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.Win32.LaunchFile:{0}:{1}", fileName ?? "NULL", ex.ToString());
			}
			return 0;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024C8 File Offset: 0x000006C8
		public static bool LaunchWordFileViaAutomation(string fileName)
		{
			try
			{
				Type typeFromProgID = Type.GetTypeFromProgID("Word.Application");
				object target = Activator.CreateInstance(typeFromProgID);
				object obj = typeFromProgID.InvokeMember("Documents", BindingFlags.GetProperty, null, target, null);
				Type.GetTypeFromHandle(Type.GetTypeHandle(obj)).InvokeMember("Open", BindingFlags.InvokeMethod, null, obj, new object[]
				{
					fileName
				});
				typeFromProgID.InvokeMember("Visible", BindingFlags.SetProperty, null, target, new object[]
				{
					true
				});
				return true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.Win32.LaunchWordFile:{0}:{1}", fileName ?? "NULL", ex.ToString());
			}
			return false;
		}
	}
}
