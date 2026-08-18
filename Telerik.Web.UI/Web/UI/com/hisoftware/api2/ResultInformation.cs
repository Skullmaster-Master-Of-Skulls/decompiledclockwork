using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001350 RID: 4944
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ResultInformation", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[DebuggerStepThrough]
	[Serializable]
	public class ResultInformation : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700426A RID: 17002
		// (get) Token: 0x0600CE84 RID: 52868 RVA: 0x002DEEE4 File Offset: 0x002DD0E4
		// (set) Token: 0x0600CE85 RID: 52869 RVA: 0x002DEEEC File Offset: 0x002DD0EC
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

		// Token: 0x1700426B RID: 17003
		// (get) Token: 0x0600CE86 RID: 52870 RVA: 0x002DEEF5 File Offset: 0x002DD0F5
		// (set) Token: 0x0600CE87 RID: 52871 RVA: 0x002DEEFD File Offset: 0x002DD0FD
		[DataMember]
		public List<CheckpointGroupResults> CheckpointGroupResults
		{
			get
			{
				return this.CheckpointGroupResultsField;
			}
			set
			{
				if (!object.ReferenceEquals(this.CheckpointGroupResultsField, value))
				{
					this.CheckpointGroupResultsField = value;
					this.RaisePropertyChanged("CheckpointGroupResults");
				}
			}
		}

		// Token: 0x1700426C RID: 17004
		// (get) Token: 0x0600CE88 RID: 52872 RVA: 0x002DEF1F File Offset: 0x002DD11F
		// (set) Token: 0x0600CE89 RID: 52873 RVA: 0x002DEF27 File Offset: 0x002DD127
		[DataMember]
		public int Health
		{
			get
			{
				return this.HealthField;
			}
			set
			{
				if (!this.HealthField.Equals(value))
				{
					this.HealthField = value;
					this.RaisePropertyChanged("Health");
				}
			}
		}

		// Token: 0x1700426D RID: 17005
		// (get) Token: 0x0600CE8A RID: 52874 RVA: 0x002DEF49 File Offset: 0x002DD149
		// (set) Token: 0x0600CE8B RID: 52875 RVA: 0x002DEF51 File Offset: 0x002DD151
		[DataMember]
		public string ID
		{
			get
			{
				return this.IDField;
			}
			set
			{
				if (!object.ReferenceEquals(this.IDField, value))
				{
					this.IDField = value;
					this.RaisePropertyChanged("ID");
				}
			}
		}

		// Token: 0x1700426E RID: 17006
		// (get) Token: 0x0600CE8C RID: 52876 RVA: 0x002DEF73 File Offset: 0x002DD173
		// (set) Token: 0x0600CE8D RID: 52877 RVA: 0x002DEF7B File Offset: 0x002DD17B
		[DataMember]
		public DateTime LastFinished
		{
			get
			{
				return this.LastFinishedField;
			}
			set
			{
				if (!this.LastFinishedField.Equals(value))
				{
					this.LastFinishedField = value;
					this.RaisePropertyChanged("LastFinished");
				}
			}
		}

		// Token: 0x1700426F RID: 17007
		// (get) Token: 0x0600CE8E RID: 52878 RVA: 0x002DEF9D File Offset: 0x002DD19D
		// (set) Token: 0x0600CE8F RID: 52879 RVA: 0x002DEFA5 File Offset: 0x002DD1A5
		[DataMember]
		public int LastRunId
		{
			get
			{
				return this.LastRunIdField;
			}
			set
			{
				if (!this.LastRunIdField.Equals(value))
				{
					this.LastRunIdField = value;
					this.RaisePropertyChanged("LastRunId");
				}
			}
		}

		// Token: 0x17004270 RID: 17008
		// (get) Token: 0x0600CE90 RID: 52880 RVA: 0x002DEFC7 File Offset: 0x002DD1C7
		// (set) Token: 0x0600CE91 RID: 52881 RVA: 0x002DEFCF File Offset: 0x002DD1CF
		[DataMember]
		public DateTime LastStarted
		{
			get
			{
				return this.LastStartedField;
			}
			set
			{
				if (!this.LastStartedField.Equals(value))
				{
					this.LastStartedField = value;
					this.RaisePropertyChanged("LastStarted");
				}
			}
		}

		// Token: 0x17004271 RID: 17009
		// (get) Token: 0x0600CE92 RID: 52882 RVA: 0x002DEFF1 File Offset: 0x002DD1F1
		// (set) Token: 0x0600CE93 RID: 52883 RVA: 0x002DEFF9 File Offset: 0x002DD1F9
		[DataMember]
		public int Pages
		{
			get
			{
				return this.PagesField;
			}
			set
			{
				if (!this.PagesField.Equals(value))
				{
					this.PagesField = value;
					this.RaisePropertyChanged("Pages");
				}
			}
		}

		// Token: 0x17004272 RID: 17010
		// (get) Token: 0x0600CE94 RID: 52884 RVA: 0x002DF01B File Offset: 0x002DD21B
		// (set) Token: 0x0600CE95 RID: 52885 RVA: 0x002DF023 File Offset: 0x002DD223
		[DataMember]
		public string Title
		{
			get
			{
				return this.TitleField;
			}
			set
			{
				if (!object.ReferenceEquals(this.TitleField, value))
				{
					this.TitleField = value;
					this.RaisePropertyChanged("Title");
				}
			}
		}

		// Token: 0x17004273 RID: 17011
		// (get) Token: 0x0600CE96 RID: 52886 RVA: 0x002DF045 File Offset: 0x002DD245
		// (set) Token: 0x0600CE97 RID: 52887 RVA: 0x002DF04D File Offset: 0x002DD24D
		[DataMember]
		public int TotalCheckpointsTested
		{
			get
			{
				return this.TotalCheckpointsTestedField;
			}
			set
			{
				if (!this.TotalCheckpointsTestedField.Equals(value))
				{
					this.TotalCheckpointsTestedField = value;
					this.RaisePropertyChanged("TotalCheckpointsTested");
				}
			}
		}

		// Token: 0x17004274 RID: 17012
		// (get) Token: 0x0600CE98 RID: 52888 RVA: 0x002DF06F File Offset: 0x002DD26F
		// (set) Token: 0x0600CE99 RID: 52889 RVA: 0x002DF077 File Offset: 0x002DD277
		[DataMember]
		public int TotalFailures
		{
			get
			{
				return this.TotalFailuresField;
			}
			set
			{
				if (!this.TotalFailuresField.Equals(value))
				{
					this.TotalFailuresField = value;
					this.RaisePropertyChanged("TotalFailures");
				}
			}
		}

		// Token: 0x17004275 RID: 17013
		// (get) Token: 0x0600CE9A RID: 52890 RVA: 0x002DF099 File Offset: 0x002DD299
		// (set) Token: 0x0600CE9B RID: 52891 RVA: 0x002DF0A1 File Offset: 0x002DD2A1
		[DataMember]
		public int TotalNAs
		{
			get
			{
				return this.TotalNAsField;
			}
			set
			{
				if (!this.TotalNAsField.Equals(value))
				{
					this.TotalNAsField = value;
					this.RaisePropertyChanged("TotalNAs");
				}
			}
		}

		// Token: 0x17004276 RID: 17014
		// (get) Token: 0x0600CE9C RID: 52892 RVA: 0x002DF0C3 File Offset: 0x002DD2C3
		// (set) Token: 0x0600CE9D RID: 52893 RVA: 0x002DF0CB File Offset: 0x002DD2CB
		[DataMember]
		public int TotalPasses
		{
			get
			{
				return this.TotalPassesField;
			}
			set
			{
				if (!this.TotalPassesField.Equals(value))
				{
					this.TotalPassesField = value;
					this.RaisePropertyChanged("TotalPasses");
				}
			}
		}

		// Token: 0x17004277 RID: 17015
		// (get) Token: 0x0600CE9E RID: 52894 RVA: 0x002DF0ED File Offset: 0x002DD2ED
		// (set) Token: 0x0600CE9F RID: 52895 RVA: 0x002DF0F5 File Offset: 0x002DD2F5
		[DataMember]
		public long TotalPhysicalSize
		{
			get
			{
				return this.TotalPhysicalSizeField;
			}
			set
			{
				if (!this.TotalPhysicalSizeField.Equals(value))
				{
					this.TotalPhysicalSizeField = value;
					this.RaisePropertyChanged("TotalPhysicalSize");
				}
			}
		}

		// Token: 0x17004278 RID: 17016
		// (get) Token: 0x0600CEA0 RID: 52896 RVA: 0x002DF117 File Offset: 0x002DD317
		// (set) Token: 0x0600CEA1 RID: 52897 RVA: 0x002DF11F File Offset: 0x002DD31F
		[DataMember]
		public int TotalResults
		{
			get
			{
				return this.TotalResultsField;
			}
			set
			{
				if (!this.TotalResultsField.Equals(value))
				{
					this.TotalResultsField = value;
					this.RaisePropertyChanged("TotalResults");
				}
			}
		}

		// Token: 0x17004279 RID: 17017
		// (get) Token: 0x0600CEA2 RID: 52898 RVA: 0x002DF141 File Offset: 0x002DD341
		// (set) Token: 0x0600CEA3 RID: 52899 RVA: 0x002DF149 File Offset: 0x002DD349
		[DataMember]
		public int TotalVisuals
		{
			get
			{
				return this.TotalVisualsField;
			}
			set
			{
				if (!this.TotalVisualsField.Equals(value))
				{
					this.TotalVisualsField = value;
					this.RaisePropertyChanged("TotalVisuals");
				}
			}
		}

		// Token: 0x1700427A RID: 17018
		// (get) Token: 0x0600CEA4 RID: 52900 RVA: 0x002DF16B File Offset: 0x002DD36B
		// (set) Token: 0x0600CEA5 RID: 52901 RVA: 0x002DF173 File Offset: 0x002DD373
		[DataMember]
		public int TotalWarnings
		{
			get
			{
				return this.TotalWarningsField;
			}
			set
			{
				if (!this.TotalWarningsField.Equals(value))
				{
					this.TotalWarningsField = value;
					this.RaisePropertyChanged("TotalWarnings");
				}
			}
		}

		// Token: 0x140001AA RID: 426
		// (add) Token: 0x0600CEA6 RID: 52902 RVA: 0x002DF198 File Offset: 0x002DD398
		// (remove) Token: 0x0600CEA7 RID: 52903 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CEA8 RID: 52904 RVA: 0x002DF208 File Offset: 0x002DD408
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003725 RID: 14117
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003726 RID: 14118
		[OptionalField]
		private List<CheckpointGroupResults> CheckpointGroupResultsField;

		// Token: 0x04003727 RID: 14119
		[OptionalField]
		private int HealthField;

		// Token: 0x04003728 RID: 14120
		[OptionalField]
		private string IDField;

		// Token: 0x04003729 RID: 14121
		[OptionalField]
		private DateTime LastFinishedField;

		// Token: 0x0400372A RID: 14122
		[OptionalField]
		private int LastRunIdField;

		// Token: 0x0400372B RID: 14123
		[OptionalField]
		private DateTime LastStartedField;

		// Token: 0x0400372C RID: 14124
		[OptionalField]
		private int PagesField;

		// Token: 0x0400372D RID: 14125
		[OptionalField]
		private string TitleField;

		// Token: 0x0400372E RID: 14126
		[OptionalField]
		private int TotalCheckpointsTestedField;

		// Token: 0x0400372F RID: 14127
		[OptionalField]
		private int TotalFailuresField;

		// Token: 0x04003730 RID: 14128
		[OptionalField]
		private int TotalNAsField;

		// Token: 0x04003731 RID: 14129
		[OptionalField]
		private int TotalPassesField;

		// Token: 0x04003732 RID: 14130
		[OptionalField]
		private long TotalPhysicalSizeField;

		// Token: 0x04003733 RID: 14131
		[OptionalField]
		private int TotalResultsField;

		// Token: 0x04003734 RID: 14132
		[OptionalField]
		private int TotalVisualsField;

		// Token: 0x04003735 RID: 14133
		[OptionalField]
		private int TotalWarningsField;
	}
}
