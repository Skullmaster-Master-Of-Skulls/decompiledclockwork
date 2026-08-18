using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001359 RID: 4953
	[DataContract(Name = "OnDemandScanCouldNotRunException", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[Serializable]
	public class OnDemandScanCouldNotRunException : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170042A3 RID: 17059
		// (get) Token: 0x0600CF16 RID: 53014 RVA: 0x002DFC70 File Offset: 0x002DDE70
		// (set) Token: 0x0600CF17 RID: 53015 RVA: 0x002DFC78 File Offset: 0x002DDE78
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

		// Token: 0x140001B2 RID: 434
		// (add) Token: 0x0600CF18 RID: 53016 RVA: 0x002DFC84 File Offset: 0x002DDE84
		// (remove) Token: 0x0600CF19 RID: 53017 RVA: 0x002DFCBC File Offset: 0x002DDEBC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF1A RID: 53018 RVA: 0x002DFCF4 File Offset: 0x002DDEF4
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400376C RID: 14188
		[NonSerialized]
		private ExtensionDataObject extensionDataField;
	}
}
