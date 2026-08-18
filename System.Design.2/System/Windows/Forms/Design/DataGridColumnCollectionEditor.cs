using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002B4 RID: 692
	internal class DataGridColumnCollectionEditor : CollectionEditor
	{
		// Token: 0x06001B73 RID: 7027 RVA: 0x00023ABB File Offset: 0x00021CBB
		public DataGridColumnCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000A32CC File Offset: 0x000A14CC
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(DataGridTextBoxColumn),
				typeof(DataGridBoolColumn)
			};
		}
	}
}
