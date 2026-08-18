using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B01 RID: 2817
	[Obsolete]
	public class ODataEntityType
	{
		// Token: 0x17002295 RID: 8853
		// (get) Token: 0x06006999 RID: 27033 RVA: 0x0018D141 File Offset: 0x0018B341
		// (set) Token: 0x0600699A RID: 27034 RVA: 0x0018D149 File Offset: 0x0018B349
		public string DataValueField
		{
			get
			{
				return this._dataValueField;
			}
			set
			{
				this._dataValueField = value;
			}
		}

		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x0600699B RID: 27035 RVA: 0x0018D152 File Offset: 0x0018B352
		// (set) Token: 0x0600699C RID: 27036 RVA: 0x0018D15A File Offset: 0x0018B35A
		public string DataTextField
		{
			get
			{
				return this._dataTextField;
			}
			set
			{
				this._dataTextField = value;
			}
		}

		// Token: 0x17002297 RID: 8855
		// (get) Token: 0x0600699D RID: 27037 RVA: 0x0018D163 File Offset: 0x0018B363
		// (set) Token: 0x0600699E RID: 27038 RVA: 0x0018D16B File Offset: 0x0018B36B
		public string NavigationProperty
		{
			get
			{
				return this._navigationProperty;
			}
			set
			{
				this._navigationProperty = value;
			}
		}

		// Token: 0x17002298 RID: 8856
		// (get) Token: 0x0600699F RID: 27039 RVA: 0x0018D174 File Offset: 0x0018B374
		// (set) Token: 0x060069A0 RID: 27040 RVA: 0x0018D17C File Offset: 0x0018B37C
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17002299 RID: 8857
		// (get) Token: 0x060069A1 RID: 27041 RVA: 0x0018D185 File Offset: 0x0018B385
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		public ODataPropertiesCollection Properies
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ODataPropertiesCollection();
				}
				return this.properties;
			}
		}

		// Token: 0x04001C86 RID: 7302
		private string _name = "";

		// Token: 0x04001C87 RID: 7303
		private string _dataValueField = "";

		// Token: 0x04001C88 RID: 7304
		private string _dataTextField = "";

		// Token: 0x04001C89 RID: 7305
		private string _navigationProperty = "";

		// Token: 0x04001C8A RID: 7306
		private ODataPropertiesCollection properties;
	}
}
