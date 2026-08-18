using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x0200135A RID: 4954
	[DebuggerStepThrough]
	[DataContract(Name = "RunsLimitReachedException", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[Serializable]
	public class RunsLimitReachedException : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170042A4 RID: 17060
		// (get) Token: 0x0600CF1C RID: 53020 RVA: 0x002DFD20 File Offset: 0x002DDF20
		// (set) Token: 0x0600CF1D RID: 53021 RVA: 0x002DFD28 File Offset: 0x002DDF28
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

		// Token: 0x170042A5 RID: 17061
		// (get) Token: 0x0600CF1E RID: 53022 RVA: 0x002DFD31 File Offset: 0x002DDF31
		// (set) Token: 0x0600CF1F RID: 53023 RVA: 0x002DFD39 File Offset: 0x002DDF39
		[DataMember]
		public int Limit
		{
			get
			{
				return this.LimitField;
			}
			set
			{
				if (!this.LimitField.Equals(value))
				{
					this.LimitField = value;
					this.RaisePropertyChanged("Limit");
				}
			}
		}

		// Token: 0x140001B3 RID: 435
		// (add) Token: 0x0600CF20 RID: 53024 RVA: 0x002DFD5C File Offset: 0x002DDF5C
		// (remove) Token: 0x0600CF21 RID: 53025 RVA: 0x002DFD94 File Offset: 0x002DDF94
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF22 RID: 53026 RVA: 0x002DFDCC File Offset: 0x002DDFCC
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400376E RID: 14190
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400376F RID: 14191
		[OptionalField]
		private int LimitField;
	}
}
