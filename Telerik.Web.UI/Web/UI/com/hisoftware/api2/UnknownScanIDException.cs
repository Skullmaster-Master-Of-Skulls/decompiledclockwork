using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001361 RID: 4961
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UnknownScanIDException", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[DebuggerStepThrough]
	[Serializable]
	public class UnknownScanIDException : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170042B0 RID: 17072
		// (get) Token: 0x0600CF4C RID: 53068 RVA: 0x002E0240 File Offset: 0x002DE440
		// (set) Token: 0x0600CF4D RID: 53069 RVA: 0x002E0248 File Offset: 0x002DE448
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

		// Token: 0x170042B1 RID: 17073
		// (get) Token: 0x0600CF4E RID: 53070 RVA: 0x002E0251 File Offset: 0x002DE451
		// (set) Token: 0x0600CF4F RID: 53071 RVA: 0x002E0259 File Offset: 0x002DE459
		[DataMember]
		public string ScanID
		{
			get
			{
				return this.ScanIDField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ScanIDField, value))
				{
					this.ScanIDField = value;
					this.RaisePropertyChanged("ScanID");
				}
			}
		}

		// Token: 0x140001B9 RID: 441
		// (add) Token: 0x0600CF50 RID: 53072 RVA: 0x002E027C File Offset: 0x002DE47C
		// (remove) Token: 0x0600CF51 RID: 53073 RVA: 0x002E02B4 File Offset: 0x002DE4B4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF52 RID: 53074 RVA: 0x002E02EC File Offset: 0x002DE4EC
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003785 RID: 14213
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003786 RID: 14214
		[OptionalField]
		private string ScanIDField;
	}
}
