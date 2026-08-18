using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x0200006E RID: 110
	public class CachedDataAnnotationsMetadataAttributes
	{
		// Token: 0x06000301 RID: 769 RVA: 0x00009BB8 File Offset: 0x00007DB8
		public CachedDataAnnotationsMetadataAttributes(Attribute[] attributes)
		{
			this.DataType = attributes.OfType<DataTypeAttribute>().FirstOrDefault<DataTypeAttribute>();
			this.Display = attributes.OfType<DisplayAttribute>().FirstOrDefault<DisplayAttribute>();
			this.DisplayColumn = attributes.OfType<DisplayColumnAttribute>().FirstOrDefault<DisplayColumnAttribute>();
			this.DisplayFormat = attributes.OfType<DisplayFormatAttribute>().FirstOrDefault<DisplayFormatAttribute>();
			this.DisplayName = attributes.OfType<DisplayNameAttribute>().FirstOrDefault<DisplayNameAttribute>();
			this.Editable = attributes.OfType<EditableAttribute>().FirstOrDefault<EditableAttribute>();
			this.HiddenInput = attributes.OfType<HiddenInputAttribute>().FirstOrDefault<HiddenInputAttribute>();
			this.ReadOnly = attributes.OfType<ReadOnlyAttribute>().FirstOrDefault<ReadOnlyAttribute>();
			this.Required = attributes.OfType<RequiredAttribute>().FirstOrDefault<RequiredAttribute>();
			this.ScaffoldColumn = attributes.OfType<ScaffoldColumnAttribute>().FirstOrDefault<ScaffoldColumnAttribute>();
			IEnumerable<UIHintAttribute> source = attributes.OfType<UIHintAttribute>();
			UIHintAttribute uihint;
			if ((uihint = source.FirstOrDefault((UIHintAttribute a) => string.Equals(a.PresentationLayer, "MVC", StringComparison.OrdinalIgnoreCase))) == null)
			{
				uihint = source.FirstOrDefault((UIHintAttribute a) => string.IsNullOrEmpty(a.PresentationLayer));
			}
			this.UIHint = uihint;
			if (this.DisplayFormat == null && this.DataType != null)
			{
				this.DisplayFormat = this.DataType.DisplayFormat;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009CED File Offset: 0x00007EED
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00009CF5 File Offset: 0x00007EF5
		public DataTypeAttribute DataType { get; protected set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00009CFE File Offset: 0x00007EFE
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00009D06 File Offset: 0x00007F06
		public DisplayAttribute Display { get; protected set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00009D0F File Offset: 0x00007F0F
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00009D17 File Offset: 0x00007F17
		public DisplayColumnAttribute DisplayColumn { get; protected set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00009D20 File Offset: 0x00007F20
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00009D28 File Offset: 0x00007F28
		public DisplayFormatAttribute DisplayFormat { get; protected set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00009D31 File Offset: 0x00007F31
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00009D39 File Offset: 0x00007F39
		public DisplayNameAttribute DisplayName { get; protected set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00009D42 File Offset: 0x00007F42
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00009D4A File Offset: 0x00007F4A
		public EditableAttribute Editable { get; protected set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00009D53 File Offset: 0x00007F53
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00009D5B File Offset: 0x00007F5B
		public HiddenInputAttribute HiddenInput { get; protected set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00009D64 File Offset: 0x00007F64
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00009D6C File Offset: 0x00007F6C
		public ReadOnlyAttribute ReadOnly { get; protected set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00009D75 File Offset: 0x00007F75
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00009D7D File Offset: 0x00007F7D
		public RequiredAttribute Required { get; protected set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00009D86 File Offset: 0x00007F86
		// (set) Token: 0x06000315 RID: 789 RVA: 0x00009D8E File Offset: 0x00007F8E
		public ScaffoldColumnAttribute ScaffoldColumn { get; protected set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00009D97 File Offset: 0x00007F97
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00009D9F File Offset: 0x00007F9F
		public UIHintAttribute UIHint { get; protected set; }
	}
}
