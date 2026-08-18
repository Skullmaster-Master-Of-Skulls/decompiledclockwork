using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x0200135F RID: 4959
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Account", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[DebuggerStepThrough]
	[Serializable]
	public class Account : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170042AB RID: 17067
		// (get) Token: 0x0600CF3E RID: 53054 RVA: 0x002E00E0 File Offset: 0x002DE2E0
		// (set) Token: 0x0600CF3F RID: 53055 RVA: 0x002E00E8 File Offset: 0x002DE2E8
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

		// Token: 0x170042AC RID: 17068
		// (get) Token: 0x0600CF40 RID: 53056 RVA: 0x002E00F1 File Offset: 0x002DE2F1
		// (set) Token: 0x0600CF41 RID: 53057 RVA: 0x002E00F9 File Offset: 0x002DE2F9
		[DataMember]
		public AccountType AccountType
		{
			get
			{
				return this.AccountTypeField;
			}
			set
			{
				if (!this.AccountTypeField.Equals(value))
				{
					this.AccountTypeField = value;
					this.RaisePropertyChanged("AccountType");
				}
			}
		}

		// Token: 0x170042AD RID: 17069
		// (get) Token: 0x0600CF42 RID: 53058 RVA: 0x002E0125 File Offset: 0x002DE325
		// (set) Token: 0x0600CF43 RID: 53059 RVA: 0x002E012D File Offset: 0x002DE32D
		[DataMember]
		public string ApiKey
		{
			get
			{
				return this.ApiKeyField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ApiKeyField, value))
				{
					this.ApiKeyField = value;
					this.RaisePropertyChanged("ApiKey");
				}
			}
		}

		// Token: 0x170042AE RID: 17070
		// (get) Token: 0x0600CF44 RID: 53060 RVA: 0x002E014F File Offset: 0x002DE34F
		// (set) Token: 0x0600CF45 RID: 53061 RVA: 0x002E0157 File Offset: 0x002DE357
		[DataMember(IsRequired = true)]
		public string EmailAddress
		{
			get
			{
				return this.EmailAddressField;
			}
			set
			{
				if (!object.ReferenceEquals(this.EmailAddressField, value))
				{
					this.EmailAddressField = value;
					this.RaisePropertyChanged("EmailAddress");
				}
			}
		}

		// Token: 0x170042AF RID: 17071
		// (get) Token: 0x0600CF46 RID: 53062 RVA: 0x002E0179 File Offset: 0x002DE379
		// (set) Token: 0x0600CF47 RID: 53063 RVA: 0x002E0181 File Offset: 0x002DE381
		[DataMember]
		public int RunsRemaining
		{
			get
			{
				return this.RunsRemainingField;
			}
			set
			{
				if (!this.RunsRemainingField.Equals(value))
				{
					this.RunsRemainingField = value;
					this.RaisePropertyChanged("RunsRemaining");
				}
			}
		}

		// Token: 0x140001B8 RID: 440
		// (add) Token: 0x0600CF48 RID: 53064 RVA: 0x002E01A4 File Offset: 0x002DE3A4
		// (remove) Token: 0x0600CF49 RID: 53065 RVA: 0x002E01DC File Offset: 0x002DE3DC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF4A RID: 53066 RVA: 0x002E0214 File Offset: 0x002DE414
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400377A RID: 14202
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400377B RID: 14203
		[OptionalField]
		private AccountType AccountTypeField;

		// Token: 0x0400377C RID: 14204
		[OptionalField]
		private string ApiKeyField;

		// Token: 0x0400377D RID: 14205
		private string EmailAddressField;

		// Token: 0x0400377E RID: 14206
		[OptionalField]
		private int RunsRemainingField;
	}
}
