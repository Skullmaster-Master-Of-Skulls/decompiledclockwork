using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001352 RID: 4946
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "CheckpointGroup", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[Serializable]
	public class CheckpointGroup : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700427E RID: 17022
		// (get) Token: 0x0600CEB4 RID: 52916 RVA: 0x002DF338 File Offset: 0x002DD538
		// (set) Token: 0x0600CEB5 RID: 52917 RVA: 0x002DF340 File Offset: 0x002DD540
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

		// Token: 0x1700427F RID: 17023
		// (get) Token: 0x0600CEB6 RID: 52918 RVA: 0x002DF349 File Offset: 0x002DD549
		// (set) Token: 0x0600CEB7 RID: 52919 RVA: 0x002DF351 File Offset: 0x002DD551
		[DataMember]
		public List<string> CheckpointIds
		{
			get
			{
				return this.CheckpointIdsField;
			}
			set
			{
				if (!object.ReferenceEquals(this.CheckpointIdsField, value))
				{
					this.CheckpointIdsField = value;
					this.RaisePropertyChanged("CheckpointIds");
				}
			}
		}

		// Token: 0x17004280 RID: 17024
		// (get) Token: 0x0600CEB8 RID: 52920 RVA: 0x002DF373 File Offset: 0x002DD573
		// (set) Token: 0x0600CEB9 RID: 52921 RVA: 0x002DF37B File Offset: 0x002DD57B
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

		// Token: 0x17004281 RID: 17025
		// (get) Token: 0x0600CEBA RID: 52922 RVA: 0x002DF39D File Offset: 0x002DD59D
		// (set) Token: 0x0600CEBB RID: 52923 RVA: 0x002DF3A5 File Offset: 0x002DD5A5
		[DataMember]
		public string LongDescription
		{
			get
			{
				return this.LongDescriptionField;
			}
			set
			{
				if (!object.ReferenceEquals(this.LongDescriptionField, value))
				{
					this.LongDescriptionField = value;
					this.RaisePropertyChanged("LongDescription");
				}
			}
		}

		// Token: 0x17004282 RID: 17026
		// (get) Token: 0x0600CEBC RID: 52924 RVA: 0x002DF3C7 File Offset: 0x002DD5C7
		// (set) Token: 0x0600CEBD RID: 52925 RVA: 0x002DF3CF File Offset: 0x002DD5CF
		[DataMember]
		public string ShortDescription
		{
			get
			{
				return this.ShortDescriptionField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ShortDescriptionField, value))
				{
					this.ShortDescriptionField = value;
					this.RaisePropertyChanged("ShortDescription");
				}
			}
		}

		// Token: 0x17004283 RID: 17027
		// (get) Token: 0x0600CEBE RID: 52926 RVA: 0x002DF3F1 File Offset: 0x002DD5F1
		// (set) Token: 0x0600CEBF RID: 52927 RVA: 0x002DF3F9 File Offset: 0x002DD5F9
		[DataMember]
		public List<string> SubGroupIds
		{
			get
			{
				return this.SubGroupIdsField;
			}
			set
			{
				if (!object.ReferenceEquals(this.SubGroupIdsField, value))
				{
					this.SubGroupIdsField = value;
					this.RaisePropertyChanged("SubGroupIds");
				}
			}
		}

		// Token: 0x140001AC RID: 428
		// (add) Token: 0x0600CEC0 RID: 52928 RVA: 0x002DF41C File Offset: 0x002DD61C
		// (remove) Token: 0x0600CEC1 RID: 52929 RVA: 0x002DF454 File Offset: 0x002DD654
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CEC2 RID: 52930 RVA: 0x002DF48C File Offset: 0x002DD68C
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400373B RID: 14139
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400373C RID: 14140
		[OptionalField]
		private List<string> CheckpointIdsField;

		// Token: 0x0400373D RID: 14141
		[OptionalField]
		private string IDField;

		// Token: 0x0400373E RID: 14142
		[OptionalField]
		private string LongDescriptionField;

		// Token: 0x0400373F RID: 14143
		[OptionalField]
		private string ShortDescriptionField;

		// Token: 0x04003740 RID: 14144
		[OptionalField]
		private List<string> SubGroupIdsField;
	}
}
