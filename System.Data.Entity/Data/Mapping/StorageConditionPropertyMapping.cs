using System;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x0200023D RID: 573
	internal class StorageConditionPropertyMapping : StoragePropertyMapping
	{
		// Token: 0x0600242F RID: 9263 RVA: 0x00082FF8 File Offset: 0x000811F8
		internal StorageConditionPropertyMapping(EdmProperty cdmMember, EdmProperty columnMember, object value, bool? isNull) : base(cdmMember)
		{
			this.m_columnMember = columnMember;
			this.m_value = value;
			this.m_isNull = isNull;
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x00083017 File Offset: 0x00081217
		internal object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x0008301F File Offset: 0x0008121F
		internal bool? IsNull
		{
			get
			{
				return this.m_isNull;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x00083027 File Offset: 0x00081227
		internal EdmProperty ColumnProperty
		{
			get
			{
				return this.m_columnMember;
			}
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00083030 File Offset: 0x00081230
		internal override void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ConditionPropertyMapping");
			stringBuilder.Append("   ");
			if (this.EdmProperty != null)
			{
				stringBuilder.Append("Name:");
				stringBuilder.Append(this.EdmProperty.Name);
				stringBuilder.Append("   ");
			}
			if (this.ColumnProperty != null)
			{
				stringBuilder.Append("Column Name:");
				stringBuilder.Append(this.ColumnProperty.Name);
				stringBuilder.Append("   ");
			}
			if (this.Value != null)
			{
				stringBuilder.Append("Value:");
				StringBuilder stringBuilder2 = stringBuilder;
				string str = "'";
				object value = this.Value;
				stringBuilder2.Append(str + ((value != null) ? value.ToString() : null) + "'");
				stringBuilder.Append("   ");
				stringBuilder.Append("Value CLR Type:");
				StringBuilder stringBuilder3 = stringBuilder;
				string str2 = "'";
				Type type = this.Value.GetType();
				stringBuilder3.Append(str2 + ((type != null) ? type.ToString() : null) + "'");
				stringBuilder.Append("   ");
			}
			stringBuilder.Append("Value TypeMetadata:");
			EdmType edmType = (this.ColumnProperty != null) ? this.ColumnProperty.TypeUsage.EdmType : null;
			if (edmType != null)
			{
				stringBuilder.Append("'" + edmType.FullName + "'");
				stringBuilder.Append("   ");
			}
			if (this.IsNull != null)
			{
				stringBuilder.Append("IsNull:");
				stringBuilder.Append(this.IsNull);
				stringBuilder.Append("   ");
			}
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x04001007 RID: 4103
		private EdmProperty m_columnMember;

		// Token: 0x04001008 RID: 4104
		private object m_value;

		// Token: 0x04001009 RID: 4105
		private bool? m_isNull;
	}
}
