using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x0200135C RID: 4956
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "EmailIsInvalidException", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[DebuggerStepThrough]
	[Serializable]
	public class EmailIsInvalidException : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170042A8 RID: 17064
		// (get) Token: 0x0600CF2C RID: 53036 RVA: 0x002DFED0 File Offset: 0x002DE0D0
		// (set) Token: 0x0600CF2D RID: 53037 RVA: 0x002DFED8 File Offset: 0x002DE0D8
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

		// Token: 0x140001B5 RID: 437
		// (add) Token: 0x0600CF2E RID: 53038 RVA: 0x002DFEE4 File Offset: 0x002DE0E4
		// (remove) Token: 0x0600CF2F RID: 53039 RVA: 0x002DFF1C File Offset: 0x002DE11C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF30 RID: 53040 RVA: 0x002DFF54 File Offset: 0x002DE154
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003774 RID: 14196
		[NonSerialized]
		private ExtensionDataObject extensionDataField;
	}
}
