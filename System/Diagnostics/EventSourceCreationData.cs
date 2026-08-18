using System;

namespace System.Diagnostics
{
	// Token: 0x0200075C RID: 1884
	public class EventSourceCreationData
	{
		// Token: 0x060039CC RID: 14796 RVA: 0x000F4EC6 File Offset: 0x000F3EC6
		private EventSourceCreationData()
		{
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000F4EE4 File Offset: 0x000F3EE4
		public EventSourceCreationData(string source, string logName)
		{
			this._source = source;
			this._logName = logName;
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000F4F10 File Offset: 0x000F3F10
		internal EventSourceCreationData(string source, string logName, string machineName)
		{
			this._source = source;
			this._logName = logName;
			this._machineName = machineName;
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000F4F44 File Offset: 0x000F3F44
		private EventSourceCreationData(string source, string logName, string machineName, string messageResourceFile, string parameterResourceFile, string categoryResourceFile, short categoryCount)
		{
			this._source = source;
			this._logName = logName;
			this._machineName = machineName;
			this._messageResourceFile = messageResourceFile;
			this._parameterResourceFile = parameterResourceFile;
			this._categoryResourceFile = categoryResourceFile;
			this.CategoryCount = (int)categoryCount;
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000F4FA2 File Offset: 0x000F3FA2
		// (set) Token: 0x060039D1 RID: 14801 RVA: 0x000F4FAA File Offset: 0x000F3FAA
		public string LogName
		{
			get
			{
				return this._logName;
			}
			set
			{
				this._logName = value;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060039D2 RID: 14802 RVA: 0x000F4FB3 File Offset: 0x000F3FB3
		// (set) Token: 0x060039D3 RID: 14803 RVA: 0x000F4FBB File Offset: 0x000F3FBB
		public string MachineName
		{
			get
			{
				return this._machineName;
			}
			set
			{
				this._machineName = value;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000F4FC4 File Offset: 0x000F3FC4
		// (set) Token: 0x060039D5 RID: 14805 RVA: 0x000F4FCC File Offset: 0x000F3FCC
		public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000F4FD5 File Offset: 0x000F3FD5
		// (set) Token: 0x060039D7 RID: 14807 RVA: 0x000F4FDD File Offset: 0x000F3FDD
		public string MessageResourceFile
		{
			get
			{
				return this._messageResourceFile;
			}
			set
			{
				this._messageResourceFile = value;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000F4FE6 File Offset: 0x000F3FE6
		// (set) Token: 0x060039D9 RID: 14809 RVA: 0x000F4FEE File Offset: 0x000F3FEE
		public string ParameterResourceFile
		{
			get
			{
				return this._parameterResourceFile;
			}
			set
			{
				this._parameterResourceFile = value;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x000F4FF7 File Offset: 0x000F3FF7
		// (set) Token: 0x060039DB RID: 14811 RVA: 0x000F4FFF File Offset: 0x000F3FFF
		public string CategoryResourceFile
		{
			get
			{
				return this._categoryResourceFile;
			}
			set
			{
				this._categoryResourceFile = value;
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000F5008 File Offset: 0x000F4008
		// (set) Token: 0x060039DD RID: 14813 RVA: 0x000F5010 File Offset: 0x000F4010
		public int CategoryCount
		{
			get
			{
				return this._categoryCount;
			}
			set
			{
				if (value > 65535 || value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryCount = value;
			}
		}

		// Token: 0x040032DE RID: 13022
		private string _logName = "Application";

		// Token: 0x040032DF RID: 13023
		private string _machineName = ".";

		// Token: 0x040032E0 RID: 13024
		private string _source;

		// Token: 0x040032E1 RID: 13025
		private string _messageResourceFile;

		// Token: 0x040032E2 RID: 13026
		private string _parameterResourceFile;

		// Token: 0x040032E3 RID: 13027
		private string _categoryResourceFile;

		// Token: 0x040032E4 RID: 13028
		private int _categoryCount;
	}
}
