using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001356 RID: 4950
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ResultInstance", Namespace = "urn:hisoftware:compliancesheriff:data")]
	[Serializable]
	public class ResultInstance : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700429B RID: 17051
		// (get) Token: 0x0600CEFE RID: 52990 RVA: 0x002DFA18 File Offset: 0x002DDC18
		// (set) Token: 0x0600CEFF RID: 52991 RVA: 0x002DFA20 File Offset: 0x002DDC20
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

		// Token: 0x1700429C RID: 17052
		// (get) Token: 0x0600CF00 RID: 52992 RVA: 0x002DFA29 File Offset: 0x002DDC29
		// (set) Token: 0x0600CF01 RID: 52993 RVA: 0x002DFA31 File Offset: 0x002DDC31
		[DataMember]
		public int Column
		{
			get
			{
				return this.ColumnField;
			}
			set
			{
				if (!this.ColumnField.Equals(value))
				{
					this.ColumnField = value;
					this.RaisePropertyChanged("Column");
				}
			}
		}

		// Token: 0x1700429D RID: 17053
		// (get) Token: 0x0600CF02 RID: 52994 RVA: 0x002DFA53 File Offset: 0x002DDC53
		// (set) Token: 0x0600CF03 RID: 52995 RVA: 0x002DFA5B File Offset: 0x002DDC5B
		[DataMember]
		public string Element
		{
			get
			{
				return this.ElementField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ElementField, value))
				{
					this.ElementField = value;
					this.RaisePropertyChanged("Element");
				}
			}
		}

		// Token: 0x1700429E RID: 17054
		// (get) Token: 0x0600CF04 RID: 52996 RVA: 0x002DFA7D File Offset: 0x002DDC7D
		// (set) Token: 0x0600CF05 RID: 52997 RVA: 0x002DFA85 File Offset: 0x002DDC85
		[DataMember]
		public string KeyAttribute
		{
			get
			{
				return this.KeyAttributeField;
			}
			set
			{
				if (!object.ReferenceEquals(this.KeyAttributeField, value))
				{
					this.KeyAttributeField = value;
					this.RaisePropertyChanged("KeyAttribute");
				}
			}
		}

		// Token: 0x1700429F RID: 17055
		// (get) Token: 0x0600CF06 RID: 52998 RVA: 0x002DFAA7 File Offset: 0x002DDCA7
		// (set) Token: 0x0600CF07 RID: 52999 RVA: 0x002DFAAF File Offset: 0x002DDCAF
		[DataMember]
		public string KeyAttributeValue
		{
			get
			{
				return this.KeyAttributeValueField;
			}
			set
			{
				if (!object.ReferenceEquals(this.KeyAttributeValueField, value))
				{
					this.KeyAttributeValueField = value;
					this.RaisePropertyChanged("KeyAttributeValue");
				}
			}
		}

		// Token: 0x170042A0 RID: 17056
		// (get) Token: 0x0600CF08 RID: 53000 RVA: 0x002DFAD1 File Offset: 0x002DDCD1
		// (set) Token: 0x0600CF09 RID: 53001 RVA: 0x002DFAD9 File Offset: 0x002DDCD9
		[DataMember]
		public int Line
		{
			get
			{
				return this.LineField;
			}
			set
			{
				if (!this.LineField.Equals(value))
				{
					this.LineField = value;
					this.RaisePropertyChanged("Line");
				}
			}
		}

		// Token: 0x140001B0 RID: 432
		// (add) Token: 0x0600CF0A RID: 53002 RVA: 0x002DFAFC File Offset: 0x002DDCFC
		// (remove) Token: 0x0600CF0B RID: 53003 RVA: 0x002DFB34 File Offset: 0x002DDD34
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600CF0C RID: 53004 RVA: 0x002DFB6C File Offset: 0x002DDD6C
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400375C RID: 14172
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400375D RID: 14173
		[OptionalField]
		private int ColumnField;

		// Token: 0x0400375E RID: 14174
		[OptionalField]
		private string ElementField;

		// Token: 0x0400375F RID: 14175
		[OptionalField]
		private string KeyAttributeField;

		// Token: 0x04003760 RID: 14176
		[OptionalField]
		private string KeyAttributeValueField;

		// Token: 0x04003761 RID: 14177
		[OptionalField]
		private int LineField;
	}
}
