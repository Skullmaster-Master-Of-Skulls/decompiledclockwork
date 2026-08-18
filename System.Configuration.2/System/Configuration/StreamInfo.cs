using System;

namespace System.Configuration
{
	// Token: 0x0200008D RID: 141
	internal class StreamInfo
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0001C563 File Offset: 0x0001A763
		internal StreamInfo(string sectionName, string configSource, string streamName)
		{
			this._sectionName = sectionName;
			this._configSource = configSource;
			this._streamName = streamName;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000115BE File Offset: 0x0000F7BE
		private StreamInfo()
		{
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001C580 File Offset: 0x0001A780
		internal StreamInfo Clone()
		{
			return new StreamInfo
			{
				_sectionName = this._sectionName,
				_configSource = this._configSource,
				_streamName = this._streamName,
				_isMonitored = this._isMonitored,
				_version = this._version
			};
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
		internal string SectionName
		{
			get
			{
				return this._sectionName;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		internal string ConfigSource
		{
			get
			{
				return this._configSource;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		internal string StreamName
		{
			get
			{
				return this._streamName;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001C5E8 File Offset: 0x0001A7E8
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		internal bool IsMonitored
		{
			get
			{
				return this._isMonitored;
			}
			set
			{
				this._isMonitored = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001C5F9 File Offset: 0x0001A7F9
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0001C601 File Offset: 0x0001A801
		internal object Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x04000344 RID: 836
		private string _sectionName;

		// Token: 0x04000345 RID: 837
		private string _configSource;

		// Token: 0x04000346 RID: 838
		private string _streamName;

		// Token: 0x04000347 RID: 839
		private bool _isMonitored;

		// Token: 0x04000348 RID: 840
		private object _version;
	}
}
