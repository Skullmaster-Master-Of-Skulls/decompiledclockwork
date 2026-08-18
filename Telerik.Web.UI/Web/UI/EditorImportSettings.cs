using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Editor.Import;

namespace Telerik.Web.UI
{
	// Token: 0x020002A3 RID: 675
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class EditorImportSettings : ObjectWithState
	{
		// Token: 0x060017E3 RID: 6115 RVA: 0x0004F691 File Offset: 0x0004D891
		public EditorImportSettings(StateBag OwnerStateBag) : base("eis_", OwnerStateBag)
		{
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0004F69F File Offset: 0x0004D89F
		[Category("Rtf")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ImportRtfSettings Rtf
		{
			get
			{
				if (this._rtfSettings == null)
				{
					this._rtfSettings = new ImportRtfSettings(base.OwnerViewState);
				}
				return this._rtfSettings;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0004F6C0 File Offset: 0x0004D8C0
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Docx")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ImportDocxSettings Docx
		{
			get
			{
				if (this._docxSettings == null)
				{
					this._docxSettings = new ImportDocxSettings(base.OwnerViewState);
				}
				return this._docxSettings;
			}
		}

		// Token: 0x04000665 RID: 1637
		private ImportRtfSettings _rtfSettings;

		// Token: 0x04000666 RID: 1638
		private ImportDocxSettings _docxSettings;
	}
}
