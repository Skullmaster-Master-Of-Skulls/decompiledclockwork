using System;
using System.CodeDom;
using System.Collections;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C4 RID: 708
	internal class DataGridViewRowCollectionCodeDomSerializer : CollectionCodeDomSerializer
	{
		// Token: 0x06001C29 RID: 7209 RVA: 0x000AA030 File Offset: 0x000A8230
		private DataGridViewRowCollectionCodeDomSerializer()
		{
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x000AA038 File Offset: 0x000A8238
		internal static DataGridViewRowCollectionCodeDomSerializer DefaultSerializer
		{
			get
			{
				if (DataGridViewRowCollectionCodeDomSerializer.defaultSerializer == null)
				{
					DataGridViewRowCollectionCodeDomSerializer.defaultSerializer = new DataGridViewRowCollectionCodeDomSerializer();
				}
				return DataGridViewRowCollectionCodeDomSerializer.defaultSerializer;
			}
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000AA050 File Offset: 0x000A8250
		protected override object SerializeCollection(IDesignerSerializationManager manager, CodeExpression targetExpression, Type targetType, ICollection originalCollection, ICollection valuesToSerialize)
		{
			return new CodeStatementCollection();
		}

		// Token: 0x040016CB RID: 5835
		private static DataGridViewRowCollectionCodeDomSerializer defaultSerializer;
	}
}
