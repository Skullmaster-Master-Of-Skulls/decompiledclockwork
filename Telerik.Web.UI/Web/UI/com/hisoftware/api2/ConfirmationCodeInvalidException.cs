using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x0200135E RID: 4958
	[DataContract(Name = "ConfirmationCodeInvalidException", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[Serializable]
	public class ConfirmationCodeInvalidException : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170042AA RID: 17066
		// (get) Token: 0x0600CF38 RID: 53048 RVA: 0x002E0030 File Offset: 0x002DE230
		// (set) Token: 0x0600CF39 RID: 53049 RVA: 0x002E0038 File Offset: 0x002DE238
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

		// Token: 0x140001B7 RID: 439
		// (add) Token: 0x0600CF3A RID: 53050 RVA: 0x002E0044 File Offset: 0x002DE244
		// (remove) Token: 0x0600CF3B RID: 53051 RVA: 0x002E007C File Offset: 0x002DE27C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF3C RID: 53052 RVA: 0x002E00B4 File Offset: 0x002DE2B4
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003778 RID: 14200
		[NonSerialized]
		private ExtensionDataObject extensionDataField;
	}
}
