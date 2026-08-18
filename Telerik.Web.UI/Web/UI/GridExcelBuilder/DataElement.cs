using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B19 RID: 6937
	public class DataElement : ElementBase
	{
		// Token: 0x170051C0 RID: 20928
		// (get) Token: 0x06010C94 RID: 68756 RVA: 0x003BA095 File Offset: 0x003B8295
		public DataType DataType
		{
			get
			{
				return this._dataType;
			}
		}

		// Token: 0x170051C1 RID: 20929
		// (get) Token: 0x06010C95 RID: 68757 RVA: 0x003BA09D File Offset: 0x003B829D
		// (set) Token: 0x06010C96 RID: 68758 RVA: 0x003BA0A5 File Offset: 0x003B82A5
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
				this._dataType = this._typeConvertor.ConvertToDataType(value);
			}
		}

		// Token: 0x06010C97 RID: 68759 RVA: 0x003BA0C0 File Offset: 0x003B82C0
		public DataElement() : this(new DataTypeConvertor(), new object())
		{
		}

		// Token: 0x06010C98 RID: 68760 RVA: 0x003BA0D2 File Offset: 0x003B82D2
		public DataElement(object dataItem) : this(new DataTypeConvertor(), dataItem)
		{
		}

		// Token: 0x06010C99 RID: 68761 RVA: 0x003BA0E0 File Offset: 0x003B82E0
		public DataElement(DataTypeConvertor typeConvertor, object dataItem)
		{
			this._dataType = typeConvertor.ConvertToDataType(dataItem);
			this._typeConvertor = typeConvertor;
			this._dataItem = dataItem;
		}

		// Token: 0x170051C2 RID: 20930
		// (get) Token: 0x06010C9A RID: 68762 RVA: 0x003BA103 File Offset: 0x003B8303
		public override IElementsCollection InnerElements
		{
			get
			{
				return new ElementsCollection();
			}
		}

		// Token: 0x06010C9B RID: 68763 RVA: 0x003BA10C File Offset: 0x003B830C
		protected override void RenderChildElements(StringBuilder sb)
		{
			if (this.DataItem != null && this._typeConvertor.CanConvert(this.DataItem))
			{
				string value = this._typeConvertor.Convert(this.DataItem);
				sb.Append(value);
			}
		}

		// Token: 0x170051C3 RID: 20931
		// (get) Token: 0x06010C9C RID: 68764 RVA: 0x003BA14E File Offset: 0x003B834E
		protected override string StartTag
		{
			get
			{
				return "<Data{0}>";
			}
		}

		// Token: 0x170051C4 RID: 20932
		// (get) Token: 0x06010C9D RID: 68765 RVA: 0x003BA155 File Offset: 0x003B8355
		protected override string EndTag
		{
			get
			{
				return "</Data>";
			}
		}

		// Token: 0x06010C9E RID: 68766 RVA: 0x003BA15C File Offset: 0x003B835C
		protected override void AppendAttributes(StringBuilder sb)
		{
			base.Attributes.Add("ss:Type", this._typeConvertor.ConvertDataEnumToString(this._dataType));
			base.Attributes.Remove("ss:StyleID");
			base.AppendAttributes(sb);
		}

		// Token: 0x04004AEA RID: 19178
		private DataType _dataType;

		// Token: 0x04004AEB RID: 19179
		private DataTypeConvertor _typeConvertor;

		// Token: 0x04004AEC RID: 19180
		private object _dataItem;
	}
}
