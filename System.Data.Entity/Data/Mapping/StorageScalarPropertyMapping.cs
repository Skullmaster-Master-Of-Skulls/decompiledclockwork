using System;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x02000250 RID: 592
	internal class StorageScalarPropertyMapping : StoragePropertyMapping
	{
		// Token: 0x060024FD RID: 9469 RVA: 0x0008A027 File Offset: 0x00088227
		internal StorageScalarPropertyMapping(EdmProperty member, EdmProperty columnMember) : base(member)
		{
			this.m_columnMember = columnMember;
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x0008A037 File Offset: 0x00088237
		internal EdmProperty ColumnProperty
		{
			get
			{
				return this.m_columnMember;
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0008A040 File Offset: 0x00088240
		internal override void Print(int index)
		{
			StorageEntityContainerMapping.GetPrettyPrintString(ref index);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ScalarPropertyMapping");
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
			}
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x0400110E RID: 4366
		private EdmProperty m_columnMember;
	}
}
