using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x0200134F RID: 4943
	[DebuggerStepThrough]
	[DataContract(Name = "NotAvailableForUse", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[Serializable]
	public class NotAvailableForUse : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17004269 RID: 17001
		// (get) Token: 0x0600CE7E RID: 52862 RVA: 0x002DEE34 File Offset: 0x002DD034
		// (set) Token: 0x0600CE7F RID: 52863 RVA: 0x002DEE3C File Offset: 0x002DD03C
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

		// Token: 0x140001A9 RID: 425
		// (add) Token: 0x0600CE80 RID: 52864 RVA: 0x002DEE48 File Offset: 0x002DD048
		// (remove) Token: 0x0600CE81 RID: 52865 RVA: 0x002DEE80 File Offset: 0x002DD080
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CE82 RID: 52866 RVA: 0x002DEEB8 File Offset: 0x002DD0B8
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003723 RID: 14115
		[NonSerialized]
		private ExtensionDataObject extensionDataField;
	}
}
