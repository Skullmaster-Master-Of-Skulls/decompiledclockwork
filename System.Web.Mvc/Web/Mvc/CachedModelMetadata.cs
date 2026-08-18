using System;

namespace System.Web.Mvc
{
	// Token: 0x02000070 RID: 112
	public abstract class CachedModelMetadata<TPrototypeCache> : ModelMetadata
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000A626 File Offset: 0x00008826
		protected CachedModelMetadata(CachedModelMetadata<TPrototypeCache> prototype, Func<object> modelAccessor) : base(prototype.Provider, prototype.ContainerType, modelAccessor, prototype.ModelType, prototype.PropertyName)
		{
			this.PrototypeCache = prototype.PrototypeCache;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000A653 File Offset: 0x00008853
		protected CachedModelMetadata(CachedDataAnnotationsModelMetadataProvider provider, Type containerType, Type modelType, string propertyName, TPrototypeCache prototypeCache) : base(provider, containerType, null, modelType, propertyName)
		{
			this.PrototypeCache = prototypeCache;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000A669 File Offset: 0x00008869
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000A68C File Offset: 0x0000888C
		public sealed override bool ConvertEmptyStringToNull
		{
			get
			{
				if (!this._convertEmptyStringToNullComputed)
				{
					this._convertEmptyStringToNull = this.ComputeConvertEmptyStringToNull();
					this._convertEmptyStringToNullComputed = true;
				}
				return this._convertEmptyStringToNull;
			}
			set
			{
				this._convertEmptyStringToNull = value;
				this._convertEmptyStringToNullComputed = true;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000A69C File Offset: 0x0000889C
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000A6BF File Offset: 0x000088BF
		public sealed override string DataTypeName
		{
			get
			{
				if (!this._dataTypeNameComputed)
				{
					this._dataTypeName = this.ComputeDataTypeName();
					this._dataTypeNameComputed = true;
				}
				return this._dataTypeName;
			}
			set
			{
				this._dataTypeName = value;
				this._dataTypeNameComputed = true;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000A6CF File Offset: 0x000088CF
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000A6F2 File Offset: 0x000088F2
		public sealed override string Description
		{
			get
			{
				if (!this._descriptionComputed)
				{
					this._description = this.ComputeDescription();
					this._descriptionComputed = true;
				}
				return this._description;
			}
			set
			{
				this._description = value;
				this._descriptionComputed = true;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000A702 File Offset: 0x00008902
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000A725 File Offset: 0x00008925
		public sealed override string DisplayFormatString
		{
			get
			{
				if (!this._displayFormatStringComputed)
				{
					this._displayFormatString = this.ComputeDisplayFormatString();
					this._displayFormatStringComputed = true;
				}
				return this._displayFormatString;
			}
			set
			{
				this._displayFormatString = value;
				this._displayFormatStringComputed = true;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000A735 File Offset: 0x00008935
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000A758 File Offset: 0x00008958
		public sealed override string DisplayName
		{
			get
			{
				if (!this._displayNameComputed)
				{
					this._displayName = this.ComputeDisplayName();
					this._displayNameComputed = true;
				}
				return this._displayName;
			}
			set
			{
				this._displayName = value;
				this._displayNameComputed = true;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000A768 File Offset: 0x00008968
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000A78B File Offset: 0x0000898B
		public sealed override string EditFormatString
		{
			get
			{
				if (!this._editFormatStringComputed)
				{
					this._editFormatString = this.ComputeEditFormatString();
					this._editFormatStringComputed = true;
				}
				return this._editFormatString;
			}
			set
			{
				this._editFormatString = value;
				this._editFormatStringComputed = true;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000A79B File Offset: 0x0000899B
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000A7BE File Offset: 0x000089BE
		internal sealed override bool HasNonDefaultEditFormat
		{
			get
			{
				if (!this._hasNonDefaultEditFormatComputed)
				{
					this._hasNonDefaultEditFormat = this.ComputeHasNonDefaultEditFormat();
					this._hasNonDefaultEditFormatComputed = true;
				}
				return this._hasNonDefaultEditFormat;
			}
			set
			{
				this._hasNonDefaultEditFormat = value;
				this._hasNonDefaultEditFormatComputed = true;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000A7CE File Offset: 0x000089CE
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000A7F1 File Offset: 0x000089F1
		public sealed override bool HideSurroundingHtml
		{
			get
			{
				if (!this._hideSurroundingHtmlComputed)
				{
					this._hideSurroundingHtml = this.ComputeHideSurroundingHtml();
					this._hideSurroundingHtmlComputed = true;
				}
				return this._hideSurroundingHtml;
			}
			set
			{
				this._hideSurroundingHtml = value;
				this._hideSurroundingHtmlComputed = true;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000A801 File Offset: 0x00008A01
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000A824 File Offset: 0x00008A24
		public sealed override bool HtmlEncode
		{
			get
			{
				if (!this._htmlEncodeComputed)
				{
					this._htmlEncode = this.ComputeHtmlEncode();
					this._htmlEncodeComputed = true;
				}
				return this._htmlEncode;
			}
			set
			{
				this._htmlEncode = value;
				this._htmlEncodeComputed = true;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000A834 File Offset: 0x00008A34
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000A857 File Offset: 0x00008A57
		public sealed override bool IsReadOnly
		{
			get
			{
				if (!this._isReadOnlyComputed)
				{
					this._isReadOnly = this.ComputeIsReadOnly();
					this._isReadOnlyComputed = true;
				}
				return this._isReadOnly;
			}
			set
			{
				this._isReadOnly = value;
				this._isReadOnlyComputed = true;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000A867 File Offset: 0x00008A67
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000A88A File Offset: 0x00008A8A
		public sealed override bool IsRequired
		{
			get
			{
				if (!this._isRequiredComputed)
				{
					this._isRequired = this.ComputeIsRequired();
					this._isRequiredComputed = true;
				}
				return this._isRequired;
			}
			set
			{
				this._isRequired = value;
				this._isRequiredComputed = true;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000A89A File Offset: 0x00008A9A
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000A8BD File Offset: 0x00008ABD
		public sealed override string NullDisplayText
		{
			get
			{
				if (!this._nullDisplayTextComputed)
				{
					this._nullDisplayText = this.ComputeNullDisplayText();
					this._nullDisplayTextComputed = true;
				}
				return this._nullDisplayText;
			}
			set
			{
				this._nullDisplayText = value;
				this._nullDisplayTextComputed = true;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000A8CD File Offset: 0x00008ACD
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public sealed override int Order
		{
			get
			{
				if (!this._orderComputed)
				{
					this._order = this.ComputeOrder();
					this._orderComputed = true;
				}
				return this._order;
			}
			set
			{
				this._order = value;
				this._orderComputed = true;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000A900 File Offset: 0x00008B00
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000A908 File Offset: 0x00008B08
		protected TPrototypeCache PrototypeCache { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000A911 File Offset: 0x00008B11
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000A934 File Offset: 0x00008B34
		public sealed override string ShortDisplayName
		{
			get
			{
				if (!this._shortDisplayNameComputed)
				{
					this._shortDisplayName = this.ComputeShortDisplayName();
					this._shortDisplayNameComputed = true;
				}
				return this._shortDisplayName;
			}
			set
			{
				this._shortDisplayName = value;
				this._shortDisplayNameComputed = true;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000A944 File Offset: 0x00008B44
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000A967 File Offset: 0x00008B67
		public sealed override bool ShowForDisplay
		{
			get
			{
				if (!this._showForDisplayComputed)
				{
					this._showForDisplay = this.ComputeShowForDisplay();
					this._showForDisplayComputed = true;
				}
				return this._showForDisplay;
			}
			set
			{
				this._showForDisplay = value;
				this._showForDisplayComputed = true;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000A977 File Offset: 0x00008B77
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000A99A File Offset: 0x00008B9A
		public sealed override bool ShowForEdit
		{
			get
			{
				if (!this._showForEditComputed)
				{
					this._showForEdit = this.ComputeShowForEdit();
					this._showForEditComputed = true;
				}
				return this._showForEdit;
			}
			set
			{
				this._showForEdit = value;
				this._showForEditComputed = true;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000A9AA File Offset: 0x00008BAA
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000A9B2 File Offset: 0x00008BB2
		public sealed override string SimpleDisplayText
		{
			get
			{
				return base.SimpleDisplayText;
			}
			set
			{
				base.SimpleDisplayText = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000A9BB File Offset: 0x00008BBB
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000A9DE File Offset: 0x00008BDE
		public sealed override string TemplateHint
		{
			get
			{
				if (!this._templateHintComputed)
				{
					this._templateHint = this.ComputeTemplateHint();
					this._templateHintComputed = true;
				}
				return this._templateHint;
			}
			set
			{
				this._templateHint = value;
				this._templateHintComputed = true;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000A9EE File Offset: 0x00008BEE
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000AA11 File Offset: 0x00008C11
		public sealed override string Watermark
		{
			get
			{
				if (!this._watermarkComputed)
				{
					this._watermark = this.ComputeWatermark();
					this._watermarkComputed = true;
				}
				return this._watermark;
			}
			set
			{
				this._watermark = value;
				this._watermarkComputed = true;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000AA21 File Offset: 0x00008C21
		protected virtual bool ComputeConvertEmptyStringToNull()
		{
			return base.ConvertEmptyStringToNull;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000AA29 File Offset: 0x00008C29
		protected virtual string ComputeDataTypeName()
		{
			return base.DataTypeName;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000AA31 File Offset: 0x00008C31
		protected virtual string ComputeDescription()
		{
			return base.Description;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000AA39 File Offset: 0x00008C39
		protected virtual string ComputeDisplayFormatString()
		{
			return base.DisplayFormatString;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000AA41 File Offset: 0x00008C41
		protected virtual string ComputeDisplayName()
		{
			return base.DisplayName;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000AA49 File Offset: 0x00008C49
		protected virtual string ComputeEditFormatString()
		{
			return base.EditFormatString;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000AA51 File Offset: 0x00008C51
		protected virtual bool ComputeHasNonDefaultEditFormat()
		{
			return base.HasNonDefaultEditFormat;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000AA59 File Offset: 0x00008C59
		protected virtual bool ComputeHideSurroundingHtml()
		{
			return base.HideSurroundingHtml;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000AA61 File Offset: 0x00008C61
		protected virtual bool ComputeHtmlEncode()
		{
			return base.HtmlEncode;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000AA69 File Offset: 0x00008C69
		protected virtual bool ComputeIsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000AA71 File Offset: 0x00008C71
		protected virtual bool ComputeIsRequired()
		{
			return base.IsRequired;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000AA79 File Offset: 0x00008C79
		protected virtual string ComputeNullDisplayText()
		{
			return base.NullDisplayText;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000AA81 File Offset: 0x00008C81
		protected virtual int ComputeOrder()
		{
			return base.Order;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000AA89 File Offset: 0x00008C89
		protected virtual string ComputeShortDisplayName()
		{
			return base.ShortDisplayName;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000AA91 File Offset: 0x00008C91
		protected virtual bool ComputeShowForDisplay()
		{
			return base.ShowForDisplay;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000AA99 File Offset: 0x00008C99
		protected virtual bool ComputeShowForEdit()
		{
			return base.ShowForEdit;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000AAA1 File Offset: 0x00008CA1
		protected virtual string ComputeSimpleDisplayText()
		{
			return base.GetSimpleDisplayText();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000AAA9 File Offset: 0x00008CA9
		protected virtual string ComputeTemplateHint()
		{
			return base.TemplateHint;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000AAB1 File Offset: 0x00008CB1
		protected virtual string ComputeWatermark()
		{
			return base.Watermark;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000AAB9 File Offset: 0x00008CB9
		protected sealed override string GetSimpleDisplayText()
		{
			return this.ComputeSimpleDisplayText();
		}

		// Token: 0x040000DE RID: 222
		private bool _convertEmptyStringToNull;

		// Token: 0x040000DF RID: 223
		private string _dataTypeName;

		// Token: 0x040000E0 RID: 224
		private string _description;

		// Token: 0x040000E1 RID: 225
		private string _displayFormatString;

		// Token: 0x040000E2 RID: 226
		private string _displayName;

		// Token: 0x040000E3 RID: 227
		private string _editFormatString;

		// Token: 0x040000E4 RID: 228
		private bool _hasNonDefaultEditFormat;

		// Token: 0x040000E5 RID: 229
		private bool _hideSurroundingHtml;

		// Token: 0x040000E6 RID: 230
		private bool _htmlEncode;

		// Token: 0x040000E7 RID: 231
		private bool _isReadOnly;

		// Token: 0x040000E8 RID: 232
		private bool _isRequired;

		// Token: 0x040000E9 RID: 233
		private string _nullDisplayText;

		// Token: 0x040000EA RID: 234
		private int _order;

		// Token: 0x040000EB RID: 235
		private string _shortDisplayName;

		// Token: 0x040000EC RID: 236
		private bool _showForDisplay;

		// Token: 0x040000ED RID: 237
		private bool _showForEdit;

		// Token: 0x040000EE RID: 238
		private string _templateHint;

		// Token: 0x040000EF RID: 239
		private string _watermark;

		// Token: 0x040000F0 RID: 240
		private bool _convertEmptyStringToNullComputed;

		// Token: 0x040000F1 RID: 241
		private bool _dataTypeNameComputed;

		// Token: 0x040000F2 RID: 242
		private bool _descriptionComputed;

		// Token: 0x040000F3 RID: 243
		private bool _displayFormatStringComputed;

		// Token: 0x040000F4 RID: 244
		private bool _displayNameComputed;

		// Token: 0x040000F5 RID: 245
		private bool _editFormatStringComputed;

		// Token: 0x040000F6 RID: 246
		private bool _hasNonDefaultEditFormatComputed;

		// Token: 0x040000F7 RID: 247
		private bool _hideSurroundingHtmlComputed;

		// Token: 0x040000F8 RID: 248
		private bool _htmlEncodeComputed;

		// Token: 0x040000F9 RID: 249
		private bool _isReadOnlyComputed;

		// Token: 0x040000FA RID: 250
		private bool _isRequiredComputed;

		// Token: 0x040000FB RID: 251
		private bool _nullDisplayTextComputed;

		// Token: 0x040000FC RID: 252
		private bool _orderComputed;

		// Token: 0x040000FD RID: 253
		private bool _shortDisplayNameComputed;

		// Token: 0x040000FE RID: 254
		private bool _showForDisplayComputed;

		// Token: 0x040000FF RID: 255
		private bool _showForEditComputed;

		// Token: 0x04000100 RID: 256
		private bool _templateHintComputed;

		// Token: 0x04000101 RID: 257
		private bool _watermarkComputed;
	}
}
