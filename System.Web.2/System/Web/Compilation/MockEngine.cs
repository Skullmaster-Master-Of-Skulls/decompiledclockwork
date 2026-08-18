using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace System.Web.Compilation
{
	// Token: 0x020007FA RID: 2042
	internal class MockEngine : IBuildEngine
	{
		// Token: 0x0600615A RID: 24922 RVA: 0x00150D03 File Offset: 0x0014EF03
		internal MockEngine()
		{
		}

		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x0600615B RID: 24923 RVA: 0x00150D37 File Offset: 0x0014EF37
		internal ICollection<BuildMessageEventArgs> Messages
		{
			get
			{
				return this.messages;
			}
		}

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x0600615C RID: 24924 RVA: 0x00150D3F File Offset: 0x0014EF3F
		internal ICollection<BuildWarningEventArgs> Warnings
		{
			get
			{
				return this.warnings;
			}
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x0600615D RID: 24925 RVA: 0x00150D47 File Offset: 0x0014EF47
		internal ICollection<BuildErrorEventArgs> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x0600615E RID: 24926 RVA: 0x00150D4F File Offset: 0x0014EF4F
		internal ICollection<CustomBuildEventArgs> CustomEvents
		{
			get
			{
				return this.customEvents;
			}
		}

		// Token: 0x0600615F RID: 24927 RVA: 0x00150D57 File Offset: 0x0014EF57
		public virtual void LogErrorEvent(BuildErrorEventArgs eventArgs)
		{
			this.errors.Add(eventArgs);
		}

		// Token: 0x06006160 RID: 24928 RVA: 0x00150D65 File Offset: 0x0014EF65
		public virtual void LogWarningEvent(BuildWarningEventArgs eventArgs)
		{
			this.warnings.Add(eventArgs);
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x00150D73 File Offset: 0x0014EF73
		public virtual void LogCustomEvent(CustomBuildEventArgs eventArgs)
		{
			this.customEvents.Add(eventArgs);
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x00150D81 File Offset: 0x0014EF81
		public virtual void LogMessageEvent(BuildMessageEventArgs eventArgs)
		{
			this.messages.Add(eventArgs);
		}

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x06006163 RID: 24931 RVA: 0x00007722 File Offset: 0x00005922
		public bool ContinueOnError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x06006164 RID: 24932 RVA: 0x00028752 File Offset: 0x00026952
		public string ProjectFileOfTaskNode
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x06006165 RID: 24933 RVA: 0x00007722 File Offset: 0x00005922
		public int LineNumberOfTaskNode
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001BB4 RID: 7092
		// (get) Token: 0x06006166 RID: 24934 RVA: 0x00007722 File Offset: 0x00005922
		public int ColumnNumberOfTaskNode
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x00003ABB File Offset: 0x00001CBB
		public bool BuildProjectFile(string projectFileName, string[] targetNames, IDictionary globalProperties, IDictionary targetOutputs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400329C RID: 12956
		private List<BuildMessageEventArgs> messages = new List<BuildMessageEventArgs>();

		// Token: 0x0400329D RID: 12957
		private List<BuildWarningEventArgs> warnings = new List<BuildWarningEventArgs>();

		// Token: 0x0400329E RID: 12958
		private List<BuildErrorEventArgs> errors = new List<BuildErrorEventArgs>();

		// Token: 0x0400329F RID: 12959
		private List<CustomBuildEventArgs> customEvents = new List<CustomBuildEventArgs>();
	}
}
