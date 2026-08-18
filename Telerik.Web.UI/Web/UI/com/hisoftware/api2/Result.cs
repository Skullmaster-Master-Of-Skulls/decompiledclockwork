using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001355 RID: 4949
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Result", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[Serializable]
	public class Result : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17004290 RID: 17040
		// (get) Token: 0x0600CEE4 RID: 52964 RVA: 0x002DF7BC File Offset: 0x002DD9BC
		// (set) Token: 0x0600CEE5 RID: 52965 RVA: 0x002DF7C4 File Offset: 0x002DD9C4
		[Browsable(false)]
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17004291 RID: 17041
		// (get) Token: 0x0600CEE6 RID: 52966 RVA: 0x002DF7CD File Offset: 0x002DD9CD
		// (set) Token: 0x0600CEE7 RID: 52967 RVA: 0x002DF7D5 File Offset: 0x002DD9D5
		[DataMember]
		public string CachedUrl
		{
			get
			{
				return this.CachedUrlField;
			}
			set
			{
				if (!object.ReferenceEquals(this.CachedUrlField, value))
				{
					this.CachedUrlField = value;
					this.RaisePropertyChanged("CachedUrl");
				}
			}
		}

		// Token: 0x17004292 RID: 17042
		// (get) Token: 0x0600CEE8 RID: 52968 RVA: 0x002DF7F7 File Offset: 0x002DD9F7
		// (set) Token: 0x0600CEE9 RID: 52969 RVA: 0x002DF7FF File Offset: 0x002DD9FF
		[DataMember]
		public int Count
		{
			get
			{
				return this.CountField;
			}
			set
			{
				if (!this.CountField.Equals(value))
				{
					this.CountField = value;
					this.RaisePropertyChanged("Count");
				}
			}
		}

		// Token: 0x17004293 RID: 17043
		// (get) Token: 0x0600CEEA RID: 52970 RVA: 0x002DF821 File Offset: 0x002DDA21
		// (set) Token: 0x0600CEEB RID: 52971 RVA: 0x002DF829 File Offset: 0x002DDA29
		[DataMember]
		public string FileUrl
		{
			get
			{
				return this.FileUrlField;
			}
			set
			{
				if (!object.ReferenceEquals(this.FileUrlField, value))
				{
					this.FileUrlField = value;
					this.RaisePropertyChanged("FileUrl");
				}
			}
		}

		// Token: 0x17004294 RID: 17044
		// (get) Token: 0x0600CEEC RID: 52972 RVA: 0x002DF84B File Offset: 0x002DDA4B
		// (set) Token: 0x0600CEED RID: 52973 RVA: 0x002DF853 File Offset: 0x002DDA53
		[DataMember]
		public string HowToFixUrl
		{
			get
			{
				return this.HowToFixUrlField;
			}
			set
			{
				if (!object.ReferenceEquals(this.HowToFixUrlField, value))
				{
					this.HowToFixUrlField = value;
					this.RaisePropertyChanged("HowToFixUrl");
				}
			}
		}

		// Token: 0x17004295 RID: 17045
		// (get) Token: 0x0600CEEE RID: 52974 RVA: 0x002DF875 File Offset: 0x002DDA75
		// (set) Token: 0x0600CEEF RID: 52975 RVA: 0x002DF87D File Offset: 0x002DDA7D
		[DataMember]
		public int ID
		{
			get
			{
				return this.IDField;
			}
			set
			{
				if (!this.IDField.Equals(value))
				{
					this.IDField = value;
					this.RaisePropertyChanged("ID");
				}
			}
		}

		// Token: 0x17004296 RID: 17046
		// (get) Token: 0x0600CEF0 RID: 52976 RVA: 0x002DF89F File Offset: 0x002DDA9F
		// (set) Token: 0x0600CEF1 RID: 52977 RVA: 0x002DF8A7 File Offset: 0x002DDAA7
		[DataMember]
		public List<ResultInstance> Instances
		{
			get
			{
				return this.InstancesField;
			}
			set
			{
				if (!object.ReferenceEquals(this.InstancesField, value))
				{
					this.InstancesField = value;
					this.RaisePropertyChanged("Instances");
				}
			}
		}

		// Token: 0x17004297 RID: 17047
		// (get) Token: 0x0600CEF2 RID: 52978 RVA: 0x002DF8C9 File Offset: 0x002DDAC9
		// (set) Token: 0x0600CEF3 RID: 52979 RVA: 0x002DF8D1 File Offset: 0x002DDAD1
		[DataMember]
		public int PageId
		{
			get
			{
				return this.PageIdField;
			}
			set
			{
				if (!this.PageIdField.Equals(value))
				{
					this.PageIdField = value;
					this.RaisePropertyChanged("PageId");
				}
			}
		}

		// Token: 0x17004298 RID: 17048
		// (get) Token: 0x0600CEF4 RID: 52980 RVA: 0x002DF8F3 File Offset: 0x002DDAF3
		// (set) Token: 0x0600CEF5 RID: 52981 RVA: 0x002DF8FB File Offset: 0x002DDAFB
		[DataMember]
		public string ResultText
		{
			get
			{
				return this.ResultTextField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ResultTextField, value))
				{
					this.ResultTextField = value;
					this.RaisePropertyChanged("ResultText");
				}
			}
		}

		// Token: 0x17004299 RID: 17049
		// (get) Token: 0x0600CEF6 RID: 52982 RVA: 0x002DF91D File Offset: 0x002DDB1D
		// (set) Token: 0x0600CEF7 RID: 52983 RVA: 0x002DF925 File Offset: 0x002DDB25
		[DataMember]
		public ResultType ResultType
		{
			get
			{
				return this.ResultTypeField;
			}
			set
			{
				if (!this.ResultTypeField.Equals(value))
				{
					this.ResultTypeField = value;
					this.RaisePropertyChanged("ResultType");
				}
			}
		}

		// Token: 0x1700429A RID: 17050
		// (get) Token: 0x0600CEF8 RID: 52984 RVA: 0x002DF951 File Offset: 0x002DDB51
		// (set) Token: 0x0600CEF9 RID: 52985 RVA: 0x002DF959 File Offset: 0x002DDB59
		[DataMember]
		public int RunId
		{
			get
			{
				return this.RunIdField;
			}
			set
			{
				if (!this.RunIdField.Equals(value))
				{
					this.RunIdField = value;
					this.RaisePropertyChanged("RunId");
				}
			}
		}

		// Token: 0x140001AF RID: 431
		// (add) Token: 0x0600CEFA RID: 52986 RVA: 0x002DF97C File Offset: 0x002DDB7C
		// (remove) Token: 0x0600CEFB RID: 52987 RVA: 0x002DF9B4 File Offset: 0x002DDBB4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CEFC RID: 52988 RVA: 0x002DF9EC File Offset: 0x002DDBEC
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003750 RID: 14160
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003751 RID: 14161
		[OptionalField]
		private string CachedUrlField;

		// Token: 0x04003752 RID: 14162
		[OptionalField]
		private int CountField;

		// Token: 0x04003753 RID: 14163
		[OptionalField]
		private string FileUrlField;

		// Token: 0x04003754 RID: 14164
		[OptionalField]
		private string HowToFixUrlField;

		// Token: 0x04003755 RID: 14165
		[OptionalField]
		private int IDField;

		// Token: 0x04003756 RID: 14166
		[OptionalField]
		private List<ResultInstance> InstancesField;

		// Token: 0x04003757 RID: 14167
		[OptionalField]
		private int PageIdField;

		// Token: 0x04003758 RID: 14168
		[OptionalField]
		private string ResultTextField;

		// Token: 0x04003759 RID: 14169
		[OptionalField]
		private ResultType ResultTypeField;

		// Token: 0x0400375A RID: 14170
		[OptionalField]
		private int RunIdField;
	}
}
