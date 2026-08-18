using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Diagnostics.Design
{
	// Token: 0x02000212 RID: 530
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ProcessDesigner : ComponentDesigner
	{
		// Token: 0x06001390 RID: 5008 RVA: 0x0006FC00 File Offset: 0x0006DE00
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			ICollection makeReadWrite = null;
			ICollection makeBrowsable = new string[]
			{
				"SynchronizingObject",
				"EnableRaisingEvents",
				"StartInfo",
				"BasePriority",
				"HandleCount",
				"Id",
				"MainWindowHandle",
				"MainWindowTitle",
				"MaxWorkingSet",
				"MinWorkingSet",
				"NonpagedSystemMemorySize",
				"PagedMemorySize",
				"PagedSystemMemorySize",
				"PeakPagedMemorySize",
				"PeakWorkingSet",
				"PeakVirtualMemorySize",
				"PriorityBoostEnabled",
				"PriorityClass",
				"PrivateMemorySize",
				"PrivilegedProcessorTime",
				"ProcessName",
				"ProcessorAffinity",
				"Responding",
				"StartTime",
				"TotalProcessorTime",
				"UserProcessorTime",
				"VirtualMemorySize",
				"WorkingSet"
			};
			bool[] array = new bool[28];
			array[1] = true;
			array[2] = true;
			RuntimeComponentFilter.FilterProperties(properties, makeReadWrite, makeBrowsable, array);
		}
	}
}
