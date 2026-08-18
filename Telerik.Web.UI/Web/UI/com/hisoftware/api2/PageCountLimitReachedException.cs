using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001358 RID: 4952
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "PageCountLimitReachedException", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[Serializable]
	public class PageCountLimitReachedException : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170042A1 RID: 17057
		// (get) Token: 0x0600CF0E RID: 53006 RVA: 0x002DFB98 File Offset: 0x002DDD98
		// (set) Token: 0x0600CF0F RID: 53007 RVA: 0x002DFBA0 File Offset: 0x002DDDA0
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

		// Token: 0x170042A2 RID: 17058
		// (get) Token: 0x0600CF10 RID: 53008 RVA: 0x002DFBA9 File Offset: 0x002DDDA9
		// (set) Token: 0x0600CF11 RID: 53009 RVA: 0x002DFBB1 File Offset: 0x002DDDB1
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

		// Token: 0x140001B1 RID: 433
		// (add) Token: 0x0600CF12 RID: 53010 RVA: 0x002DFBD4 File Offset: 0x002DDDD4
		// (remove) Token: 0x0600CF13 RID: 53011 RVA: 0x002DFC0C File Offset: 0x002DDE0C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF14 RID: 53012 RVA: 0x002DFC44 File Offset: 0x002DDE44
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04003769 RID: 14185
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400376A RID: 14186
		[OptionalField]
		private int LimitField;
	}
}
