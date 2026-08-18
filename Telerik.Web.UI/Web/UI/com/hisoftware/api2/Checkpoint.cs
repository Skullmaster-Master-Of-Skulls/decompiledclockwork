using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001354 RID: 4948
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Checkpoint", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[Serializable]
	public class Checkpoint : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17004287 RID: 17031
		// (get) Token: 0x0600CECE RID: 52942 RVA: 0x002DF5BC File Offset: 0x002DD7BC
		// (set) Token: 0x0600CECF RID: 52943 RVA: 0x002DF5C4 File Offset: 0x002DD7C4
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

		// Token: 0x17004288 RID: 17032
		// (get) Token: 0x0600CED0 RID: 52944 RVA: 0x002DF5CD File Offset: 0x002DD7CD
		// (set) Token: 0x0600CED1 RID: 52945 RVA: 0x002DF5D5 File Offset: 0x002DD7D5
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

		// Token: 0x17004289 RID: 17033
		// (get) Token: 0x0600CED2 RID: 52946 RVA: 0x002DF5F7 File Offset: 0x002DD7F7
		// (set) Token: 0x0600CED3 RID: 52947 RVA: 0x002DF5FF File Offset: 0x002DD7FF
		[DataMember]
		public string InformationURL
		{
			get
			{
				return this.InformationURLField;
			}
			set
			{
				if (!object.ReferenceEquals(this.InformationURLField, value))
				{
					this.InformationURLField = value;
					this.RaisePropertyChanged("InformationURL");
				}
			}
		}

		// Token: 0x1700428A RID: 17034
		// (get) Token: 0x0600CED4 RID: 52948 RVA: 0x002DF621 File Offset: 0x002DD821
		// (set) Token: 0x0600CED5 RID: 52949 RVA: 0x002DF629 File Offset: 0x002DD829
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

		// Token: 0x1700428B RID: 17035
		// (get) Token: 0x0600CED6 RID: 52950 RVA: 0x002DF64B File Offset: 0x002DD84B
		// (set) Token: 0x0600CED7 RID: 52951 RVA: 0x002DF653 File Offset: 0x002DD853
		[DataMember]
		public string Module
		{
			get
			{
				return this.ModuleField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ModuleField, value))
				{
					this.ModuleField = value;
					this.RaisePropertyChanged("Module");
				}
			}
		}

		// Token: 0x1700428C RID: 17036
		// (get) Token: 0x0600CED8 RID: 52952 RVA: 0x002DF675 File Offset: 0x002DD875
		// (set) Token: 0x0600CED9 RID: 52953 RVA: 0x002DF67D File Offset: 0x002DD87D
		[DataMember]
		public string Number
		{
			get
			{
				return this.NumberField;
			}
			set
			{
				if (!object.ReferenceEquals(this.NumberField, value))
				{
					this.NumberField = value;
					this.RaisePropertyChanged("Number");
				}
			}
		}

		// Token: 0x1700428D RID: 17037
		// (get) Token: 0x0600CEDA RID: 52954 RVA: 0x002DF69F File Offset: 0x002DD89F
		// (set) Token: 0x0600CEDB RID: 52955 RVA: 0x002DF6A7 File Offset: 0x002DD8A7
		[DataMember]
		public int Priority
		{
			get
			{
				return this.PriorityField;
			}
			set
			{
				if (!this.PriorityField.Equals(value))
				{
					this.PriorityField = value;
					this.RaisePropertyChanged("Priority");
				}
			}
		}

		// Token: 0x1700428E RID: 17038
		// (get) Token: 0x0600CEDC RID: 52956 RVA: 0x002DF6C9 File Offset: 0x002DD8C9
		// (set) Token: 0x0600CEDD RID: 52957 RVA: 0x002DF6D1 File Offset: 0x002DD8D1
		[DataMember]
		public string Rule
		{
			get
			{
				return this.RuleField;
			}
			set
			{
				if (!object.ReferenceEquals(this.RuleField, value))
				{
					this.RuleField = value;
					this.RaisePropertyChanged("Rule");
				}
			}
		}

		// Token: 0x1700428F RID: 17039
		// (get) Token: 0x0600CEDE RID: 52958 RVA: 0x002DF6F3 File Offset: 0x002DD8F3
		// (set) Token: 0x0600CEDF RID: 52959 RVA: 0x002DF6FB File Offset: 0x002DD8FB
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

		// Token: 0x140001AE RID: 430
		// (add) Token: 0x0600CEE0 RID: 52960 RVA: 0x002DF720 File Offset: 0x002DD920
		// (remove) Token: 0x0600CEE1 RID: 52961 RVA: 0x002DF758 File Offset: 0x002DD958
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CEE2 RID: 52962 RVA: 0x002DF790 File Offset: 0x002DD990
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003746 RID: 14150
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003747 RID: 14151
		[OptionalField]
		private string IDField;

		// Token: 0x04003748 RID: 14152
		[OptionalField]
		private string InformationURLField;

		// Token: 0x04003749 RID: 14153
		[OptionalField]
		private string LongDescriptionField;

		// Token: 0x0400374A RID: 14154
		[OptionalField]
		private string ModuleField;

		// Token: 0x0400374B RID: 14155
		[OptionalField]
		private string NumberField;

		// Token: 0x0400374C RID: 14156
		[OptionalField]
		private int PriorityField;

		// Token: 0x0400374D RID: 14157
		[OptionalField]
		private string RuleField;

		// Token: 0x0400374E RID: 14158
		[OptionalField]
		private string ShortDescriptionField;
	}
}
