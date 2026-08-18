using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x0200135B RID: 4955
	[DataContract(Name = "InvalidApiKeyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[Serializable]
	public class InvalidApiKeyException : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170042A6 RID: 17062
		// (get) Token: 0x0600CF24 RID: 53028 RVA: 0x002DFDF8 File Offset: 0x002DDFF8
		// (set) Token: 0x0600CF25 RID: 53029 RVA: 0x002DFE00 File Offset: 0x002DE000
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

		// Token: 0x170042A7 RID: 17063
		// (get) Token: 0x0600CF26 RID: 53030 RVA: 0x002DFE09 File Offset: 0x002DE009
		// (set) Token: 0x0600CF27 RID: 53031 RVA: 0x002DFE11 File Offset: 0x002DE011
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

		// Token: 0x140001B4 RID: 436
		// (add) Token: 0x0600CF28 RID: 53032 RVA: 0x002DFE34 File Offset: 0x002DE034
		// (remove) Token: 0x0600CF29 RID: 53033 RVA: 0x002DFE6C File Offset: 0x002DE06C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF2A RID: 53034 RVA: 0x002DFEA4 File Offset: 0x002DE0A4
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003771 RID: 14193
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003772 RID: 14194
		[OptionalField]
		private string ApiKeyField;
	}
}
