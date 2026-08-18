using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001353 RID: 4947
	[DataContract(Name = "CheckpointResults", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[Serializable]
	public class CheckpointResults : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17004284 RID: 17028
		// (get) Token: 0x0600CEC4 RID: 52932 RVA: 0x002DF4B8 File Offset: 0x002DD6B8
		// (set) Token: 0x0600CEC5 RID: 52933 RVA: 0x002DF4C0 File Offset: 0x002DD6C0
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

		// Token: 0x17004285 RID: 17029
		// (get) Token: 0x0600CEC6 RID: 52934 RVA: 0x002DF4C9 File Offset: 0x002DD6C9
		// (set) Token: 0x0600CEC7 RID: 52935 RVA: 0x002DF4D1 File Offset: 0x002DD6D1
		[DataMember]
		public Checkpoint Properties
		{
			get
			{
				return this.PropertiesField;
			}
			set
			{
				if (!object.ReferenceEquals(this.PropertiesField, value))
				{
					this.PropertiesField = value;
					this.RaisePropertyChanged("Properties");
				}
			}
		}

		// Token: 0x17004286 RID: 17030
		// (get) Token: 0x0600CEC8 RID: 52936 RVA: 0x002DF4F3 File Offset: 0x002DD6F3
		// (set) Token: 0x0600CEC9 RID: 52937 RVA: 0x002DF4FB File Offset: 0x002DD6FB
		[DataMember]
		public List<Result> Results
		{
			get
			{
				return this.ResultsField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ResultsField, value))
				{
					this.ResultsField = value;
					this.RaisePropertyChanged("Results");
				}
			}
		}

		// Token: 0x140001AD RID: 429
		// (add) Token: 0x0600CECA RID: 52938 RVA: 0x002DF520 File Offset: 0x002DD720
		// (remove) Token: 0x0600CECB RID: 52939 RVA: 0x002DF558 File Offset: 0x002DD758
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CECC RID: 52940 RVA: 0x002DF590 File Offset: 0x002DD790
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003742 RID: 14146
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003743 RID: 14147
		[OptionalField]
		private Checkpoint PropertiesField;

		// Token: 0x04003744 RID: 14148
		[OptionalField]
		private List<Result> ResultsField;
	}
}
