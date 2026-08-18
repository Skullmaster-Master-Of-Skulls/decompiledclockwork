using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x0200135D RID: 4957
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "EmailIsEmptyException", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[Serializable]
	public class EmailIsEmptyException : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170042A9 RID: 17065
		// (get) Token: 0x0600CF32 RID: 53042 RVA: 0x002DFF80 File Offset: 0x002DE180
		// (set) Token: 0x0600CF33 RID: 53043 RVA: 0x002DFF88 File Offset: 0x002DE188
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

		// Token: 0x140001B6 RID: 438
		// (add) Token: 0x0600CF34 RID: 53044 RVA: 0x002DFF94 File Offset: 0x002DE194
		// (remove) Token: 0x0600CF35 RID: 53045 RVA: 0x002DFFCC File Offset: 0x002DE1CC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF36 RID: 53046 RVA: 0x002E0004 File Offset: 0x002DE204
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003776 RID: 14198
		[NonSerialized]
		private ExtensionDataObject extensionDataField;
	}
}
