using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001105 RID: 4357
	public class GridGroupByField
	{
		// Token: 0x170039B5 RID: 14773
		// (get) Token: 0x0600B22E RID: 45614 RVA: 0x0026CDE2 File Offset: 0x0026AFE2
		// (set) Token: 0x0600B22F RID: 45615 RVA: 0x0026CDEA File Offset: 0x0026AFEA
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string FieldName
		{
			get
			{
				return this._fieldName;
			}
			set
			{
				this._fieldName = value;
				if (string.IsNullOrEmpty(this._fieldAlias))
				{
					this._fieldAlias = value;
				}
			}
		}

		// Token: 0x170039B6 RID: 14774
		// (get) Token: 0x0600B230 RID: 45616 RVA: 0x0026CE07 File Offset: 0x0026B007
		// (set) Token: 0x0600B231 RID: 45617 RVA: 0x0026CE0F File Offset: 0x0026B00F
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string FieldAlias
		{
			get
			{
				return this._fieldAlias;
			}
			set
			{
				this._fieldAlias = value;
			}
		}

		// Token: 0x170039B7 RID: 14775
		// (get) Token: 0x0600B232 RID: 45618 RVA: 0x0026CE18 File Offset: 0x0026B018
		// (set) Token: 0x0600B233 RID: 45619 RVA: 0x0026CE20 File Offset: 0x0026B020
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridAggregateFunction), "None")]
		public GridAggregateFunction Aggregate
		{
			get
			{
				return this._aggregate;
			}
			set
			{
				this._aggregate = value;
			}
		}

		// Token: 0x0600B234 RID: 45620 RVA: 0x0026CE2C File Offset: 0x0026B02C
		public void SetAggregate(string value)
		{
			try
			{
				this.Aggregate = (GridAggregateFunction)Enum.Parse(typeof(GridAggregateFunction), value, true);
			}
			catch
			{
				throw new GridGroupByException("Aggregate function " + value + " is unknown. Please check the expression syntax.");
			}
		}

		// Token: 0x170039B8 RID: 14776
		// (get) Token: 0x0600B235 RID: 45621 RVA: 0x0026CE80 File Offset: 0x0026B080
		// (set) Token: 0x0600B236 RID: 45622 RVA: 0x0026CE88 File Offset: 0x0026B088
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridSortOrder), "Ascending")]
		public GridSortOrder SortOrder
		{
			get
			{
				return this._sortOrder;
			}
			set
			{
				this._sortOrder = value;
			}
		}

		// Token: 0x0600B237 RID: 45623 RVA: 0x0026CE94 File Offset: 0x0026B094
		public void SetSortOrder(string SortOrder)
		{
			try
			{
				this.SortOrder = (GridSortOrder)Enum.Parse(typeof(GridSortOrder), SortOrder);
			}
			catch
			{
				throw new GridGroupByException("Sort order " + SortOrder + " is unknown. Please check the expression syntax.");
			}
		}

		// Token: 0x170039B9 RID: 14777
		// (get) Token: 0x0600B238 RID: 45624 RVA: 0x0026CEE8 File Offset: 0x0026B0E8
		// (set) Token: 0x0600B239 RID: 45625 RVA: 0x0026CEF0 File Offset: 0x0026B0F0
		internal string RelationName
		{
			get
			{
				return this._relationName;
			}
			set
			{
				this._relationName = value;
			}
		}

		// Token: 0x170039BA RID: 14778
		// (get) Token: 0x0600B23A RID: 45626 RVA: 0x0026CEF9 File Offset: 0x0026B0F9
		// (set) Token: 0x0600B23B RID: 45627 RVA: 0x0026CF01 File Offset: 0x0026B101
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string FormatString
		{
			get
			{
				return this._formatString;
			}
			set
			{
				this._formatString = value;
			}
		}

		// Token: 0x170039BB RID: 14779
		// (get) Token: 0x0600B23C RID: 45628 RVA: 0x0026CF0A File Offset: 0x0026B10A
		// (set) Token: 0x0600B23D RID: 45629 RVA: 0x0026CF12 File Offset: 0x0026B112
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string HeaderText
		{
			get
			{
				return this._headerText;
			}
			set
			{
				this._headerText = value;
			}
		}

		// Token: 0x0600B23E RID: 45630 RVA: 0x0026CF1B File Offset: 0x0026B11B
		public string GetHeaderText()
		{
			this.Validate();
			if (!string.IsNullOrEmpty(this.HeaderText))
			{
				return this.HeaderText;
			}
			return this.FieldAlias;
		}

		// Token: 0x170039BC RID: 14780
		// (get) Token: 0x0600B23F RID: 45631 RVA: 0x0026CF3D File Offset: 0x0026B13D
		// (set) Token: 0x0600B240 RID: 45632 RVA: 0x0026CF45 File Offset: 0x0026B145
		[DefaultValue(": ")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string HeaderValueSeparator
		{
			get
			{
				return this._headerValueSeparator;
			}
			set
			{
				this._headerValueSeparator = value;
			}
		}

		// Token: 0x0600B241 RID: 45633 RVA: 0x0026CF50 File Offset: 0x0026B150
		public string GetFormatString()
		{
			this.Validate();
			if (!string.IsNullOrEmpty(this.FormatString))
			{
				return this.GetHeaderText() + this.HeaderValueSeparator + this.FormatString;
			}
			return this.GetHeaderText() + this.HeaderValueSeparator + "{0}";
		}

		// Token: 0x0600B242 RID: 45634 RVA: 0x0026CFA0 File Offset: 0x0026B1A0
		public void Validate()
		{
			if (string.IsNullOrEmpty(this.FieldName))
			{
				throw new GridGroupByException("Field definition is not valid. Field name cannot be null or empty.");
			}
			if (this.FieldName.IndexOfAny(GridGroupByField.InvalidChars) >= 0)
			{
				throw new GridGroupByException("Field definition is not valid. FieldName contains invalid characters: " + this.FieldName);
			}
			if (string.IsNullOrEmpty(this.FieldAlias))
			{
				throw new GridGroupByException("Field definition is not valid. FieldAlias cannot be null or empty.");
			}
			if (this.FieldAlias.IndexOfAny(GridGroupByField.InvalidChars) >= 0)
			{
				throw new GridGroupByException("Field definition is not valid. FieldAlias contains invalid characters: " + this.FieldAlias);
			}
		}

		// Token: 0x0600B243 RID: 45635 RVA: 0x0026D030 File Offset: 0x0026B230
		public override string ToString()
		{
			this.Validate();
			string str = "";
			if (this.Aggregate != GridAggregateFunction.None)
			{
				str = str + this.Aggregate.ToString().ToLower() + "(";
			}
			str += this.FieldName;
			if (this.Aggregate != GridAggregateFunction.None)
			{
				str += ")";
			}
			if (this.FieldAlias != this.FieldName)
			{
				string str2 = this.FieldAlias.Replace(" ", "");
				str = str + " " + str2;
			}
			return str + " " + this.GetSortOrderAsString();
		}

		// Token: 0x0600B244 RID: 45636 RVA: 0x0026D0DC File Offset: 0x0026B2DC
		internal string GetSortOrderAsString()
		{
			if (this.SortOrder != GridSortOrder.Ascending)
			{
				return GridSortExpression.SortOrderAsString(this.SortOrder);
			}
			return string.Empty;
		}

		// Token: 0x0600B245 RID: 45637 RVA: 0x0026D0F8 File Offset: 0x0026B2F8
		public void CopyFrom(GridGroupByField field)
		{
			this.FieldName = field.FieldName;
			this.FieldAlias = field.FieldAlias;
			this.Aggregate = field.Aggregate;
			this.FormatString = field.FormatString;
			this.HeaderText = field.HeaderText;
			this.HeaderValueSeparator = field.HeaderValueSeparator;
			this.RelationName = field.RelationName;
			this.SortOrder = field.SortOrder;
		}

		// Token: 0x04002EF2 RID: 12018
		private string _fieldName;

		// Token: 0x04002EF3 RID: 12019
		private string _fieldAlias;

		// Token: 0x04002EF4 RID: 12020
		private GridAggregateFunction _aggregate;

		// Token: 0x04002EF5 RID: 12021
		private GridSortOrder _sortOrder = GridSortOrder.Ascending;

		// Token: 0x04002EF6 RID: 12022
		private string _relationName;

		// Token: 0x04002EF7 RID: 12023
		private string _formatString;

		// Token: 0x04002EF8 RID: 12024
		private string _headerText;

		// Token: 0x04002EF9 RID: 12025
		private string _headerValueSeparator = ": ";

		// Token: 0x04002EFA RID: 12026
		private static readonly char[] InvalidChars = new char[]
		{
			' ',
			'!',
			',',
			'|',
			'"',
			'\'',
			';',
			'\\',
			'/',
			'(',
			')',
			'[',
			']'
		};
	}
}
