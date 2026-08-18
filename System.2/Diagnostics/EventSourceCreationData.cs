using System;

namespace System.Diagnostics
{
	// Token: 0x020004D6 RID: 1238
	public class EventSourceCreationData
	{
		// Token: 0x06002EAC RID: 11948 RVA: 0x000D23B6 File Offset: 0x000D05B6
		private EventSourceCreationData()
		{
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000D23D4 File Offset: 0x000D05D4
		public EventSourceCreationData(string source, string logName)
		{
			this._source = source;
			this._logName = logName;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000D2400 File Offset: 0x000D0600
		internal EventSourceCreationData(string source, string logName, string machineName)
		{
			this._source = source;
			this._logName = logName;
			this._machineName = machineName;
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000D2434 File Offset: 0x000D0634
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

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000D2492 File Offset: 0x000D0692
		// (set) Token: 0x06002EB1 RID: 11953 RVA: 0x000D249A File Offset: 0x000D069A
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

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000D24A3 File Offset: 0x000D06A3
		// (set) Token: 0x06002EB3 RID: 11955 RVA: 0x000D24AB File Offset: 0x000D06AB
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

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x000D24B4 File Offset: 0x000D06B4
		// (set) Token: 0x06002EB5 RID: 11957 RVA: 0x000D24BC File Offset: 0x000D06BC
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

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000D24C5 File Offset: 0x000D06C5
		// (set) Token: 0x06002EB7 RID: 11959 RVA: 0x000D24CD File Offset: 0x000D06CD
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

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x000D24D6 File Offset: 0x000D06D6
		// (set) Token: 0x06002EB9 RID: 11961 RVA: 0x000D24DE File Offset: 0x000D06DE
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

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002EBA RID: 11962 RVA: 0x000D24E7 File Offset: 0x000D06E7
		// (set) Token: 0x06002EBB RID: 11963 RVA: 0x000D24EF File Offset: 0x000D06EF
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

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000D24F8 File Offset: 0x000D06F8
		// (set) Token: 0x06002EBD RID: 11965 RVA: 0x000D2500 File Offset: 0x000D0700
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

		// Token: 0x04002786 RID: 10118
		private string _logName = "Application";

		// Token: 0x04002787 RID: 10119
		private string _machineName = ".";

		// Token: 0x04002788 RID: 10120
		private string _source;

		// Token: 0x04002789 RID: 10121
		private string _messageResourceFile;

		// Token: 0x0400278A RID: 10122
		private string _parameterResourceFile;

		// Token: 0x0400278B RID: 10123
		private string _categoryResourceFile;

		// Token: 0x0400278C RID: 10124
		private int _categoryCount;
	}
}
