using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001351 RID: 4945
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "CheckpointGroupResults", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[DebuggerStepThrough]
	[Serializable]
	public class CheckpointGroupResults : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700427B RID: 17019
		// (get) Token: 0x0600CEAA RID: 52906 RVA: 0x002DF234 File Offset: 0x002DD434
		// (set) Token: 0x0600CEAB RID: 52907 RVA: 0x002DF23C File Offset: 0x002DD43C
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

		// Token: 0x1700427C RID: 17020
		// (get) Token: 0x0600CEAC RID: 52908 RVA: 0x002DF245 File Offset: 0x002DD445
		// (set) Token: 0x0600CEAD RID: 52909 RVA: 0x002DF24D File Offset: 0x002DD44D
		[DataMember]
		public CheckpointGroup Properties
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

		// Token: 0x1700427D RID: 17021
		// (get) Token: 0x0600CEAE RID: 52910 RVA: 0x002DF26F File Offset: 0x002DD46F
		// (set) Token: 0x0600CEAF RID: 52911 RVA: 0x002DF277 File Offset: 0x002DD477
		[DataMember]
		public List<CheckpointResults> Results
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

		// Token: 0x140001AB RID: 427
		// (add) Token: 0x0600CEB0 RID: 52912 RVA: 0x002DF29C File Offset: 0x002DD49C
		// (remove) Token: 0x0600CEB1 RID: 52913 RVA: 0x002DF2D4 File Offset: 0x002DD4D4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CEB2 RID: 52914 RVA: 0x002DF30C File Offset: 0x002DD50C
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003737 RID: 14135
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003738 RID: 14136
		[OptionalField]
		private CheckpointGroup PropertiesField;

		// Token: 0x04003739 RID: 14137
		[OptionalField]
		private List<CheckpointResults> ResultsField;
	}
}
